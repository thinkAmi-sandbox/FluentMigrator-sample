using FluentMigrator;

namespace FluentMigrator_sample.TableKeyMigrations
{
    /// <summary>
    /// 複合外部キーの作成
    /// </summary>
    [Migration(2014110706)]
    public class Mig_06_CompositeForeignKey : Migration
    {
        public override void Up()
        {
            // 参照先テーブルの準備
            if (!Schema.Table("RefFKs").Exists())
            {
                Create.Table("RefFKs")
                    .WithColumn("RefCol1").AsInt32().PrimaryKey()
                    .WithColumn("RefCol2").AsInt32().PrimaryKey()
                    .WithColumn("TextCol1").AsString();
            }

            // テーブル新規作成時に複合外部キー設定は、以下の書き方をしてもエラーになる
            //Create.Table("FKsNew")
            //    .WithColumn("PKCol").AsInt32().PrimaryKey()
            //    .WithColumn("FKCol1").AsInt32().ForeignKey("FKsNameNew", "RefFKs", "RefCol1")
            //    .WithColumn("FKCol2").AsInt32().ForeignKey("FKsNameNew", "RefFKs", "RefCol2")
            //    .WithColumn("TextCol").AsString();


            // 既存のテーブルの列に追加する場合
            if (!Schema.Table("FKsExist").Exists())
            {
                Create.Table("FKsExist")
                    .WithColumn("PKCol").AsInt32().PrimaryKey()
                    .WithColumn("FKCol1").AsInt32()
                    .WithColumn("FKCol2").AsInt32()
                    .WithColumn("TextCol").AsString();
            }

            Create.ForeignKey("FKsNameExist")
                .FromTable("FKsExist").ForeignColumns(new[] { "FKCol1", "FKCol2" })
                .ToTable("RefFKs").PrimaryColumns(new[] { "RefCol1", "RefCol2" });
        }

        public override void Down()
        {
            // 新規作成テーブルのロールバック用だが、エラーになるため削除しておく
            //Delete.Table("FKsNew");

            // 既存テーブルのロールバック用
            Delete.ForeignKey("FKsNameExist").OnTable("FKsExist");
        }
    }
}

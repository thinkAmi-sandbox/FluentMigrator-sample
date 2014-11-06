using FluentMigrator;

namespace FluentMigrator_sample.TableKeyMigrations
{
    /// <summary>
    /// 単一外部キーの作成
    /// </summary>
    [Migration(2014110705)]
    public class Mig_05_SingleForeignKey : Migration
    {
        public override void Up()
        {
            // 参照先テーブルの準備
            if (!Schema.Table("RefFK").Exists())
            {
                Create.Table("RefFK")
                    .WithColumn("RefCol").AsInt32().PrimaryKey() // 外部キーのためのPK設定
                    .WithColumn("TextCol").AsString();
            }


            // 新規テーブルの列に作成する場合
            Create.Table("FKNew")
                .WithColumn("PKCol").AsInt32().PrimaryKey()
                .WithColumn("FKCol").AsInt32().ForeignKey("FKNameNew", "RefFK", "RefCol")
                .WithColumn("TextCol").AsString();


            // 既存テーブルの列に追加する場合
            if (!Schema.Table("FKExist").Exists())
            {
                Create.Table("FKExist")
                    .WithColumn("PKCol").AsInt32().PrimaryKey()
                    .WithColumn("FKCol").AsInt32()
                    .WithColumn("TextCol").AsString();
            }

            Create.ForeignKey("FKNameExists")
                .FromTable("FKExist").ForeignColumn("FKCol")
                .ToTable("RefFK").PrimaryColumn("RefCol");
        }

        public override void Down()
        {
            // 新規テーブルのロールバック用
            Delete.Table("FKNew");

            // 既存テーブルのロールバック用
            Delete.ForeignKey("FKNameExists").OnTable("FKExist");
        }
    }
}

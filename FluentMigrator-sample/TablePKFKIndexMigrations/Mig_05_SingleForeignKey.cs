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
                Create.Table("RefFK").WithColumn("RefCol").AsInt32().PrimaryKey(); // 外部キーのためにPK用意
            }


            // 新規テーブルに作成
            Create.Table("FKNew").WithColumn("FKCol").AsInt32().ForeignKey("FKNameNew", "RefFK", "RefCol");


            // 既存テーブルに作成
            if (!Schema.Table("FKExist").Exists())
            {
                Create.Table("FKExist")
                    .WithColumn("FKCol").AsInt32();
            }

            Create.ForeignKey("FKNameExists")
                .FromTable("FKExist").ForeignColumn("FKCol")
                .ToTable("RefFK").PrimaryColumn("RefCol");
        }

        public override void Down()
        {
            Delete.Table("FKNew");                                  // 新規テーブル用
            Delete.ForeignKey("FKNameExists").OnTable("FKExist");   // 既存テーブル用
        }
    }
}

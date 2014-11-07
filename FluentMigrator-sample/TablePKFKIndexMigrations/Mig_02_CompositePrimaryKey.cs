using FluentMigrator;

namespace FluentMigrator_sample.TableKeyMigrations
{
    /// <summary>
    /// 複合主キーの作成
    /// </summary>
    [Migration(2014110702)]
    public class Mig_02_CompositePrimaryKey : Migration
    {
        public override void Up()
        {
            // 新規テーブルに作成
            Create.Table("PKsNew")
                .WithColumn("PKCol1").AsInt32().PrimaryKey()
                .WithColumn("PKCol2").AsString().PrimaryKey();


            // 既存テーブルに作成
            if (!Schema.Table("PKsExist").Exists())
            {
                Create.Table("PKsExist")
                    .WithColumn("PKCol1").AsInt32()
                    .WithColumn("PKCol2").AsString();
            }

            Create.PrimaryKey("cpk").OnTable("PKsExist").Columns(new[] { "PKCol1", "PKCol2" });
        }

        public override void Down()
        {
            Delete.Table("PKsNew");                         // 新規テーブル用
            Delete.PrimaryKey("cpk").FromTable("PKsExist"); // 既存テーブル用
        }
    }
}

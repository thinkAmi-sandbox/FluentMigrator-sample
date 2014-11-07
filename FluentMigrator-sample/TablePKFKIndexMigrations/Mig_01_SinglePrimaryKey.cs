using FluentMigrator;

namespace FluentMigrator_sample.TableKeyMigrations
{
    /// <summary>
    /// 単一主キーの作成
    /// </summary>
    [Migration(2014110701)]
    public class Mig_01_SinglePrimaryKey : Migration
    {
        public override void Up()
        {
            // 新規テーブルに作成
            Create.Table("PKNew").WithColumn("PKCol").AsInt32().PrimaryKey();


            // 既存テーブルに作成
            // Schema.Exists()を使って、既存テーブルとして存在するか判断
            if (!Schema.Table("PKExist").Exists())
            {
                Create.Table("PKExist").WithColumn("PKCol").AsInt32();
            }

            // Rollbackする際にPrimaryKey名が必要なので、引数として渡す
            Create.PrimaryKey("spk").OnTable("PKExist").Column("PKCol");
        }

        public override void Down()
        {
            Delete.Table("PKNew");                          // 新規テーブル用
            Delete.PrimaryKey("spk").FromTable("PKExist");  // 既存テーブル用
        }
    }
}

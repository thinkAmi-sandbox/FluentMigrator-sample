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
            // 新規作成テーブルに作成する場合
            Create.Table("PKNew")
                .WithColumn("PKCol").AsInt32().PrimaryKey()
                .WithColumn("TextCol").AsString();


            // 既存のテーブルの列に主キー追加する場合
            if (!Schema.Table("PKExist").Exists())
            {
                // 既存のテーブルとして存在しない場合のみ作成する
                Create.Table("PKExist")
                .WithColumn("PKCol").AsInt32()
                .WithColumn("TextCol").AsString();
            }

            // primaryKeyNameがなくても作成できるものの、
            // Deleteするときに必要になるので、指定しておく
            Create.PrimaryKey("spk")
                .OnTable("PKExist")
                .Column("PKCol");
        }

        public override void Down()
        {
            // 新規作成テーブルのロールバック用
            Delete.Table("PKNew");

            // 既存テーブルのロールバック用
            // PrimaryKey()の引数は必須
            Delete.PrimaryKey("spk").FromTable("PKExist");
        }
    }
}

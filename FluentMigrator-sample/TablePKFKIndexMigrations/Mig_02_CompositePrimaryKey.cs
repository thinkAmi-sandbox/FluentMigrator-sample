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
            // 新規作成テーブルに作成する場合
            Create.Table("PKsNew")
                .WithColumn("PKCol1").AsInt32().PrimaryKey()
                .WithColumn("PKCol2").AsString().PrimaryKey()
                .WithColumn("TextCol").AsString();



            // 既存のテーブルの列に主キー追加する場合
            if (!Schema.Table("PKsExist").Exists())
            {
                // 既存のテーブルとして存在しない場合のみ作成する
                Create.Table("PKsExist")
                    .WithColumn("PKCol1").AsInt32()
                    .WithColumn("PKCol2").AsString()
                    .WithColumn("TextCol").AsString();
            }


            // primaryKeyNameがなくても作成できるものの、
            // Deleteするときに必要になるので、指定しておく
            Create.PrimaryKey("cpk")
                .OnTable("PKsExist")
                .Columns(new[] { "PKCol1", "PKCol2" });
        }

        public override void Down()
        {
            // 新規作成テーブルのロールバック用
            Delete.Table("PKsNew");

            // 既存テーブルのロールバック用
            Delete.PrimaryKey("cpk").FromTable("PKsExist");
        }
    }
}

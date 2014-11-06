using FluentMigrator;

namespace FluentMigrator_sample.TableKeyMigrations
{
    /// <summary>
    /// 単一Indexの作成
    /// </summary>
    [Migration(2014110703)]
    public class Mig_03_SingleIndex : Migration
    {
        public override void Up()
        {
            // 新規作成テーブルに作成する場合
            // 新規作成の場合には昇順のみ可能っぽい
            Create.Table("IndexNew")
                .WithColumn("IndexCol").AsInt32().Indexed("New")
                .WithColumn("TextCol").AsString();


            // 既存のテーブルの列に追加する場合
            if (!Schema.Table("IndexExist").Exists())
            {
                Create.Table("IndexExist")
                    .WithColumn("AscIdxCol").AsInt32()
                    .WithColumn("DescIdxCol").AsInt32()
                    .WithColumn("TextCol").AsString();
            }

            Create.Index("Asc").OnTable("IndexExist").OnColumn("AscIdxCol")
                .Ascending().WithOptions().Unique(); // 昇順 + 重複不可のインデックス

            Create.Index("Desc").OnTable("IndexExist").OnColumn("DescIdxCol")
                .Descending();                       // 降順 + 重複可のインデックス
        }

        public override void Down()
        {
            // 新規作成テーブルのロールバック用
            Delete.Table("IndexNew");

            // 既存テーブルのロールバック用
            Delete.Index("Asc").OnTable("IndexExist");
            Delete.Index("Desc").OnTable("IndexExist");
        }
    }
}

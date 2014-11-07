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
            // 新規テーブルに作成
            // *昇順のみ作成可能っぽい
            Create.Table("IndexNew").WithColumn("IndexCol").AsInt32().Indexed("New");


            // 既存テーブルに作成
            if (!Schema.Table("IndexExist").Exists())
            {
                Create.Table("IndexExist")
                    .WithColumn("AscIdxCol").AsInt32()
                    .WithColumn("DescIdxCol").AsInt32()
                    .WithColumn("UniqueCol").AsInt32()
                    .WithColumn("DescUniqueCol").AsInt32()
                    .WithColumn("IgnoreNullCol").AsString();
            }

            Create.Index("Asc").OnTable("IndexExist").OnColumn("AscIdxCol").Ascending();            // 昇順
            Create.Index("Desc").OnTable("IndexExist").OnColumn("DescIdxCol").Descending();         // 降順
            Create.Index("Unique").OnTable("IndexExist").OnColumn("UniqueCol").Unique();            // 重複不可
            Create.Index("DescUnique").OnTable("IndexExist").OnColumn("DescUniqueCol")              // 降順 & 重複不可
                .Descending().WithOptions().Unique();

            // Create.Index()ではNull無視のIndexを作れないので、Execute.Sql()でDDLを直接記述する
            Execute.Sql("CREATE INDEX IgnoreNull ON IndexExist(IgnoreNullCol) WITH IGNORE NULL");   // Null無視
        }

        public override void Down()
        {
            // 新規テーブル用
            Delete.Table("IndexNew");

            // 既存テーブル用
            Delete.Index("Asc").OnTable("IndexExist");
            Delete.Index("Desc").OnTable("IndexExist");
            Delete.Index("Unique").OnTable("IndexExist");
            Delete.Index("DescUnique").OnTable("IndexExist");
            Delete.Index("IgnoreNull").OnTable("IndexExist");
        }
    }
}

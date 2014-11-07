using FluentMigrator;

namespace FluentMigrator_sample.TableKeyMigrations
{
    /// <summary>
    /// 複合Indexの作成
    /// </summary>
    [Migration(2014110704)]
    public class Mig_04_CompositeIndex : Migration
    {
        public override void Up()
        {
            // 新規テーブルに作成
            // *この場合、複合Indexが作れない
            // 以下をアンコメントすると「テーブルには既にIndexがあります」エラーとなる
            //Create.Table("IndexesNew")
            //    .WithColumn("IndexCol1").AsInt32().Indexed("New")
            //    .WithColumn("IndexCol2").AsInt32().Indexed("NewIndex");


            // 既存テーブルに作成
            if (!Schema.Table("IndexesExist").Exists())
            {
                Create.Table("IndexesExist")
                    .WithColumn("AscIdxCol").AsInt32()
                    .WithColumn("DescIdxCol").AsInt32();
            }

            // 列ごとに昇順・降順を指定
            Create.Index("AscDesc").OnTable("IndexesExist")
                .OnColumn("AscIdxCol").Ascending()
                .OnColumn("DescIdxCol").Descending();

            // 列ごとに昇順・降順を指定、全体では重複不可
            Create.Index("AscDescUnique").OnTable("IndexesExist")
                .OnColumn("AscIdxCol").Ascending()
                .OnColumn("DescIdxCol").Descending()
                .WithOptions().Unique();

            // 列ごとに昇順・降順を指定、全体では重複不可・Null無視
            Execute.Sql(
                "CREATE UNIQUE INDEX AscDescUniqueIgnoreNull ON IndexesExist" +
                "(AscIdxCol ASC, DescIdxCol DESC) WITH IGNORE NULL");
        }

        public override void Down()
        {
            // 新規テーブル用(作成していないので、コメントアウト)
            //Delete.Table("IndexesNew");

            // 既存テーブル用
            Delete.Index("AscDesc").OnTable("IndexesExist");
            Delete.Index("AscDescUnique").OnTable("IndexesExist");
            Delete.Index("AscDescUniqueIgnoreNull").OnTable("IndexesExist");
        }
    }
}

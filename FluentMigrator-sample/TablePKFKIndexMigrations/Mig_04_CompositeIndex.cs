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
            // 新規作成テーブルに作成する場合
            // 新規作成の場合には、複合Indexが作れない
            // 以下の場合では、「テーブルには既にIndexがあります」エラーとなる
            Create.Table("IndexesNew")
                .WithColumn("IndexCol1").AsInt32().Indexed("New")
                //.WithColumn("IndexCol2").AsInt32().Indexed("NewIndex") // アンコメントするとエラー
                .WithColumn("TextCol").AsString();


            // 既存のテーブルの列に追加する場合
            if (!Schema.Table("IndexesExist").Exists())
            {
                Create.Table("IndexesExist")
                    .WithColumn("AscIdxCol1").AsInt32()
                    .WithColumn("AscIdxCol2").AsInt32()
                    .WithColumn("DescIdxCol1").AsInt32()
                    .WithColumn("DescIdxCol2").AsInt32()
                    .WithColumn("TextCol").AsString();
            }

            Create.Index("Asc").OnTable("IndexesExist")
                .OnColumn("AscIdxCol1").Ascending()
                .OnColumn("AscIdxCol2").Ascending();      // 昇順：OnColumnでつなぐ

            Create.Index("Desc").OnTable("IndexesExist")
                .OnColumn("DescIdxCol1").Descending()
                .OnColumn("DescIdxCol2").Descending();    // 降順
        }

        public override void Down()
        {
            // 新規作成テーブルのロールバック用
            Delete.Table("IndexesNew");

            // 既存テーブルのロールバック用
            Delete.Index("Asc").OnTable("IndexesExist");
            Delete.Index("Desc").OnTable("IndexesExist");
        }
    }
}

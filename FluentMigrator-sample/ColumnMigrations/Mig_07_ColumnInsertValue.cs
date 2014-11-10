using FluentMigrator;

namespace FluentMigrator_sample.ColumnMigrations
{
    [Migration(2014110907)]
    public class Mig_07_ColumnInsertValue : Migration
    {
        public override void Up()
        {
            // 複数回INSERTも`;`で連結・実行されるので、複数回に分ける
            //Insert.IntoTable("Columns")
            //    .Row(new { ValueCol1 = "hoge", ValueCol2 = "hogehoge" })
            //    .Row(new { ValueCol1 = "fuga", ValueCol2 = "fugafuga" })
            //    .Row(new { ValueCol1 = "piyo" }); // ValueCol2はnull

            Insert.IntoTable("Columns")
                .Row(new { ValueCol1 = "hoge", ValueCol2 = "hogehoge" });
            Insert.IntoTable("Columns")
                .Row(new { ValueCol1 = "fuga", ValueCol2 = "fugafuga" });
            Insert.IntoTable("Columns")
                .Row(new { ValueCol1 = "piyo" }); // ValueCol2はnull
        }

        public override void Down()
        {
            Delete.FromTable("Columns").AllRows();
        }
    }
}

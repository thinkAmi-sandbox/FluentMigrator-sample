using FluentMigrator;

namespace FluentMigrator_sample.ColumnMigrations
{
    [Migration(2014110908)]
    public class Mig_08_ColumnDeleteValue : Migration
    {
        public override void Up()
        {
            // 複数回DELETEも`;`で連結・実行されるので、複数回に分ける
            Delete.FromTable("Columns").Row(new { ValueCOl1 = "hoge" });
            Delete.FromTable("Columns").IsNull("ValueCol2");
        }

        public override void Down()
        {
            // オートナンバー型の値は異なるが、仕方ない
            Insert.IntoTable("Columns").Row(new { ValueCol1 = "hoge", ValueCol2 = "hogehoge" });
            Insert.IntoTable("Columns").Row(new { ValueCol1 = "piyo" });
        }
    }
}

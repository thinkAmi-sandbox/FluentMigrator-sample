using FluentMigrator;

namespace FluentMigrator_sample.ColumnMigrations
{
    [Migration(2014110909)]
    public class Mig_09_ColumnUpdateValue : Migration
    {
        public override void Up()
        {
            Update.Table("Columns").Set(new { ValueCol1 = "piyopiyo" })
                .Where(new { ValueCol1 = "fuga" });
        }

        public override void Down()
        {
            Update.Table("Columns").Set(new { ValueCol1 = "fuga" })
                .Where(new { ValueCol1 = "piyopiyo" });
        }
    }
}

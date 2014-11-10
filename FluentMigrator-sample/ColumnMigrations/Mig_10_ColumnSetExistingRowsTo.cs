using FluentMigrator;

namespace FluentMigrator_sample.ColumnMigrations
{
    [Migration(2014110910)]
    public class Mig_10_ColumnSetExistingRowsTo : Migration
    {
        public override void Up()
        {
            Alter.Table("Columns").AddColumn("ExistingCol").AsString()
                .SetExistingRowsTo("DefalutValue").NotNullable();
        }

        public override void Down()
        {
            Delete.Column("ExistingCol").FromTable("Columns");
        }
    }
}

using FluentMigrator;

namespace FluentMigrator_sample.ColumnMigrations
{
    [Migration(2014110904)]
    public class Mig_04_ColumnAlter : Migration
    {
        public override void Up()
        {
            Alter.Table("Columns")
                .AddColumn("AddCol").AsString().Nullable()  // 一番最後に追加される
                .AlterColumn("AlterCol1").AsString(256).Nullable();

            Alter.Column("AlterCol2").OnTable("Columns").AsString(256).Nullable();
        }

        public override void Down()
        {
            Delete.Column("AddCol").FromTable("Columns");

            Alter.Table("Columns").AlterColumn("AlterCol1").AsString().Nullable();

            Alter.Column("AlterCol2").OnTable("Columns").AsString().Nullable();
        }
    }
}

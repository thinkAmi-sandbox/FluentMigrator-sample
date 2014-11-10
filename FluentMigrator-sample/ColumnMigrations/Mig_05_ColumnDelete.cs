using FluentMigrator;

namespace FluentMigrator_sample.ColumnMigrations
{
    [Migration(2014110905)]
    public class Mig_05_ColumnDelete : Migration
    {
        public override void Up()
        {
            // MS Accessは`;`で連結したDDLを実行できないので、エラーとなる
            //Delete.Column("DelCol1").Column("DelCol2").FromTable("Columns");

            // そのため、MS Accessで複数の列を削除する場合、複数回実行する
            Delete.Column("DelCol1").FromTable("Columns");
            Delete.Column("DelCol2").FromTable("Columns");
        }

        public override void Down()
        {
            Alter.Table("Columns")
                .AddColumn("DelCol1").AsInt32()
                .AddColumn("DelCol2").AsString();
        }
    }
}

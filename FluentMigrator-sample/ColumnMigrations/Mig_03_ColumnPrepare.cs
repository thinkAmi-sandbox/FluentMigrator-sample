using FluentMigrator;

namespace FluentMigrator_sample.ColumnMigrations
{
    [Migration(2014110903)]
    public class Mig_03_ColumnPrepare : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("Columns")
                // MS Accessでは、`WithIdColumn()`メソッドが出てこない
                .WithColumn("ID").AsInt32().PrimaryKey().Identity()
                .WithColumn("AlterCol1").AsString().Nullable()
                .WithColumn("AlterCol2").AsString().Nullable()
                .WithColumn("DelCol1").AsString().Nullable()
                .WithColumn("DelCol2").AsString().Nullable()
                .WithColumn("ReNameCol1").AsString().Nullable()
                .WithColumn("ReNameCol2").AsString().Nullable()
                .WithColumn("ValueCol1").AsString().Nullable()
                .WithColumn("ValueCol2").AsString().Nullable();
        }
    }
}

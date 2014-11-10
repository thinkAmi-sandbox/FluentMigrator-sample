using FluentMigrator;

namespace FluentMigrator_sample.ColumnMigrations
{
    [Migration(2014110906)]
    public class Mig_06_ColumnRename : AutoReversingMigration
    {
        public override void Up()
        {
            // MS Accessでは対応するDDLがないので、実行されない
            // ログに`No SQL statement executed.`と出力されるだけ
            Rename.Column("RenameCol1").OnTable("Columns").To("ReNamedCol1");
            Rename.Table("Columns").To("RenamedColumns");
        }
    }
}

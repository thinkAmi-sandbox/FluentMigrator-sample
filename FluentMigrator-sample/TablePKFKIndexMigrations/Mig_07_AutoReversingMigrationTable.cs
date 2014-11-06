using FluentMigrator;

namespace FluentMigrator_sample.TableKeyMigrations
{
    /// <summary>
    /// テーブルのAutoReversingMigration
    /// </summary>
    [Migration(2014110707)]
    public class Mig_07_AutoReversingMigrationTable : AutoReversingMigration
    {
        public override void Up()
        {
            // PrimaryKeyはAutoReversingMigrationに対応していないっぽいので、宣言してしまう
            // See: https://github.com/schambers/fluentmigrator/wiki/Auto-Reversing-Migrations

            Create.Table("AutoTable")
                .WithColumn("PKCol").AsInt32().PrimaryKey()
                .WithColumn("IdxCol").AsInt32()
                .WithColumn("FKCol").AsInt32()
                .WithColumn("TextCol").AsString();

            Create.Table("AutoRefTable")
                .WithColumn("RefCol").AsInt32().PrimaryKey()
                .WithColumn("TextCol").AsString();
        }
    }
}

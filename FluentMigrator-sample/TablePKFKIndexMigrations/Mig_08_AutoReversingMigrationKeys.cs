using FluentMigrator;

namespace FluentMigrator_sample.TableKeyMigrations
{
    /// <summary>
    /// インデックスや外部キーのAutoReversingMigration
    /// </summary>
    [Migration(2014110708)]
    public class Mig_08_AutoReversingMigrationKeys : AutoReversingMigration
    {
        public override void Up()
        {
            // インデックス
            Create.Index("AutoIndex").OnTable("AutoTable").OnColumn("IdxCol").Descending();

            // 外部キー
            Create.ForeignKey("AutoFK")
                .FromTable("AutoTable").ForeignColumn("FKCol")
                .ToTable("AutoRefTable").PrimaryColumn("RefCol");
        }
    }
}

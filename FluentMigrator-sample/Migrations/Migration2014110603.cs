using FluentMigrator;

namespace FluentMigrator_sample.Migrations
{
    [Migration(2014110603)]
    public class Migration2014110603 : Migration
    {
        public override void Up()
        {
            Alter.Table("Users")
                 .AddColumn("Description").AsString(256);
        }

        public override void Down()
        {
            Delete.Column("Description").FromTable("Users");
        }
    }
}

using FluentMigrator;

namespace FluentMigrator_sample.Migrations
{
    [Migration(2014110602)]
    public class Migration2014110602 : Migration
    {
        public override void Up()
        {
            Alter.Table("Users")
                 .AlterColumn("ID").AsInt32().PrimaryKey().Identity();
        }

        public override void Down()
        {
            Alter.Table("Users")
                 .AlterColumn("ID").AsInt32().PrimaryKey();
        }
    }
}

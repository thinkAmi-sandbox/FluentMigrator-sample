using FluentMigrator;

namespace FluentMigrator_sample.Migrations
{
    [Migration(2014110601)]
    public class Migration2014110601 : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("ID").AsInt32().PrimaryKey()
                .WithColumn("Name").AsString();
        }

        public override void Down()
        {
            Delete.Table("Users");
        }
    }
}

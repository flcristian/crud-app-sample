using FluentMigrator;

namespace CrudAppSample.Data.Migrations;


[Migration(110202023)]
public class CreateSchema:Migration
{
    public override void Up()
    {
        Create.Table("products")
            .WithColumn("id").AsInt32().PrimaryKey().Identity()
            .WithColumn("price").AsDouble().NotNullable()
            .WithColumn("name").AsString(128).NotNullable()
            .WithColumn("category").AsString(128).NotNullable()
            .WithColumn("date_of_fabrication").AsDateTime().NotNullable();
    }

    public override void Down()
    {
        
    }
} 
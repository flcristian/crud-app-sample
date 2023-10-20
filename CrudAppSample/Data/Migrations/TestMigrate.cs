using CrudAppSample.Products.Model;
using FluentMigrator;

namespace CrudAppSample.Data.Migrations;

[Migration(210202023)]
public class TestMigrate:Migration
{
    public override void Up()
    {
         Execute.Script(@"./Data/scripts/data.sql");
    }

    public override void Down()
    {
        
        
    }
}
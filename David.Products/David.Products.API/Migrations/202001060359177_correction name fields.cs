namespace David.Products.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correctionnamefields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Customers", "Neighborhood", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Customers", "FisrtName");
            DropColumn("dbo.Customers", "Neighboorhood");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "Neighboorhood", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Customers", "FisrtName", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Customers", "Neighborhood");
            DropColumn("dbo.Customers", "FirstName");
        }
    }
}

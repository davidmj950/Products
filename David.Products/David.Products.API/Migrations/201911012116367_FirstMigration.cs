namespace David.Products.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 50),
                        LastUpdate = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false, maxLength: 50),
                        Stock = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 200),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Available = c.Boolean(nullable: false),
                        categoryId = c.Int(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.categoryId)
                .Index(t => t.categoryId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DaneCode = c.String(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DaneCode = c.String(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClaimActions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        RoleId = c.Int(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ClaimTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ClaimActionId = c.Int(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClaimActions", t => t.ClaimActionId)
                .Index(t => t.ClaimActionId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        LastUpdate = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FisrtName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        UserName = c.String(nullable: false, maxLength: 10),
                        Password = c.String(nullable: false, maxLength: 15),
                        Email = c.String(nullable: false, maxLength: 50),
                        PhoneNumber = c.String(nullable: false, maxLength: 15),
                        RoleId = c.Int(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FisrtName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        DateOfBirth = c.DateTime(nullable: false),
                        Telephone = c.String(nullable: false, maxLength: 15),
                        PhoneNumber = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 100),
                        Neighboorhood = c.String(nullable: false, maxLength: 100),
                        DateOfRegistration = c.DateTime(nullable: false),
                        AuthorizationHabeasData = c.Boolean(nullable: false),
                        MaritalStatusId = c.Int(nullable: false),
                        GenderId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .ForeignKey("dbo.Genders", t => t.GenderId)
                .ForeignKey("dbo.MaritalStatus", t => t.MaritalStatusId)
                .Index(t => t.MaritalStatusId)
                .Index(t => t.GenderId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
                        LastUpdate = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MaritalStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
                        LastUpdate = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "MaritalStatusId", "dbo.MaritalStatus");
            DropForeignKey("dbo.Customers", "GenderId", "dbo.Genders");
            DropForeignKey("dbo.Customers", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.ClaimActions", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.ClaimTypes", "ClaimActionId", "dbo.ClaimActions");
            DropForeignKey("dbo.Cities", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Products", "categoryId", "dbo.Categories");
            DropIndex("dbo.Customers", new[] { "CityId" });
            DropIndex("dbo.Customers", new[] { "GenderId" });
            DropIndex("dbo.Customers", new[] { "MaritalStatusId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.ClaimTypes", new[] { "ClaimActionId" });
            DropIndex("dbo.ClaimActions", new[] { "RoleId" });
            DropIndex("dbo.Cities", new[] { "DepartmentId" });
            DropIndex("dbo.Products", new[] { "categoryId" });
            DropTable("dbo.MaritalStatus");
            DropTable("dbo.Genders");
            DropTable("dbo.Customers");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.ClaimTypes");
            DropTable("dbo.ClaimActions");
            DropTable("dbo.Departments");
            DropTable("dbo.Cities");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}

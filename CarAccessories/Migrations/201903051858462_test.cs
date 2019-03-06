namespace CarAccessories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Models",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ModelName = c.String(),
                        Year = c.String(),
                        Brand_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Brands", t => t.Brand_ID)
                .Index(t => t.Brand_ID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Int(nullable: false),
                        Image = c.String(),
                        MinDescription = c.String(),
                        Quantity = c.Int(nullable: false),
                        Type = c.String(),
                        State = c.String(),
                        Model_ID = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Models", t => t.Model_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.Model_ID)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.VendorProducts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Vendor_ID = c.String(maxLength: 128),
                        Product_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Vendors", t => t.Vendor_ID)
                .ForeignKey("dbo.Products", t => t.Product_ID)
                .Index(t => t.Vendor_ID)
                .Index(t => t.Product_ID);
            
            CreateTable(
                "dbo.Descriptions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Value = c.String(),
                        VendorProduct_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.VendorProducts", t => t.VendorProduct_ID)
                .Index(t => t.VendorProduct_ID);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Order_ID = c.Int(),
                        VendorProduct_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Orders", t => t.Order_ID)
                .ForeignKey("dbo.VendorProducts", t => t.VendorProduct_ID)
                .Index(t => t.Order_ID)
                .Index(t => t.VendorProduct_ID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        TotalPrice = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Customer_ID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Customers", t => t.Customer_ID)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Customer_ID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Address = c.String(),
                        NationalID = c.String(),
                        Photo = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Type = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        ComponyName = c.String(),
                        Address = c.String(),
                        Photo = c.String(),
                        Accept = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Rates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Vendor_ID = c.String(maxLength: 128),
                        Customer_ID = c.String(maxLength: 128),
                        RateNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customers", t => t.Customer_ID)
                .ForeignKey("dbo.Vendors", t => t.Vendor_ID)
                .Index(t => new { t.Vendor_ID, t.Customer_ID }, unique: true, name: "IX_UserVendor");
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.VendorProducts", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "VendorProduct_ID", "dbo.VendorProducts");
            DropForeignKey("dbo.OrderDetails", "Order_ID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "Customer_ID", "dbo.Customers");
            DropForeignKey("dbo.Customers", "ID", "dbo.AspNetUsers");
            DropForeignKey("dbo.VendorProducts", "Vendor_ID", "dbo.Vendors");
            DropForeignKey("dbo.Rates", "Vendor_ID", "dbo.Vendors");
            DropForeignKey("dbo.Rates", "Customer_ID", "dbo.Customers");
            DropForeignKey("dbo.Vendors", "ID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Products", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Descriptions", "VendorProduct_ID", "dbo.VendorProducts");
            DropForeignKey("dbo.Products", "Model_ID", "dbo.Models");
            DropForeignKey("dbo.Models", "Brand_ID", "dbo.Brands");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Rates", "IX_UserVendor");
            DropIndex("dbo.Vendors", new[] { "ID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Customers", new[] { "ID" });
            DropIndex("dbo.Orders", new[] { "Customer_ID" });
            DropIndex("dbo.Orders", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.OrderDetails", new[] { "VendorProduct_ID" });
            DropIndex("dbo.OrderDetails", new[] { "Order_ID" });
            DropIndex("dbo.Descriptions", new[] { "VendorProduct_ID" });
            DropIndex("dbo.VendorProducts", new[] { "Product_ID" });
            DropIndex("dbo.VendorProducts", new[] { "Vendor_ID" });
            DropIndex("dbo.Products", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Products", new[] { "Model_ID" });
            DropIndex("dbo.Models", new[] { "Brand_ID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Rates");
            DropTable("dbo.Vendors");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Customers");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Descriptions");
            DropTable("dbo.VendorProducts");
            DropTable("dbo.Products");
            DropTable("dbo.Models");
            DropTable("dbo.Brands");
        }
    }
}

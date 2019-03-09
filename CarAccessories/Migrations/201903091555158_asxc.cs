namespace CarAccessories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asxc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Name", c => c.String());
            AddColumn("dbo.Customers", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "PhoneNumber");
            DropColumn("dbo.Customers", "Name");
        }
    }
}

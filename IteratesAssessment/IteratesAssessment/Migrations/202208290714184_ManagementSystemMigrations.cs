namespace IteratesAssessment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManagementSystemMigrations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Beers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BreweryID = c.Int(nullable: false),
                        AlcoholContent = c.String(),
                        BeerName = c.String(),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Breweries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BreweryName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        WholesalerID = c.Int(nullable: false),
                        BeerID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Discount = c.Int(nullable: false),
                        OriginalAmout = c.Int(nullable: false),
                        TotalAmount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SaleDate = c.DateTime(nullable: false),
                        OrderID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BeerID = c.Int(nullable: false),
                        WholesalerID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Wholesalers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        WholesalerName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Wholesalers");
            DropTable("dbo.Stocks");
            DropTable("dbo.Sales");
            DropTable("dbo.Orders");
            DropTable("dbo.Breweries");
            DropTable("dbo.Beers");
        }
    }
}

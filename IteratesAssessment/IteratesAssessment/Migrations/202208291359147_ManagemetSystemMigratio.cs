namespace IteratesAssessment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManagemetSystemMigratio : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "TotalAmount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "TotalAmount", c => c.Int(nullable: false));
        }
    }
}

namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PackageMigrateOne : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Packages", "StartDate");
            DropColumn("dbo.Packages", "EndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Packages", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Packages", "StartDate", c => c.DateTime(nullable: false));
        }
    }
}

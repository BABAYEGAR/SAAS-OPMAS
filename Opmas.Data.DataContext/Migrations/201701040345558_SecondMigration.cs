namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Institutions", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Institutions", "EndDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Packages", "StartDate");
            DropColumn("dbo.Packages", "EndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Packages", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Packages", "StartDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Institutions", "EndDate");
            DropColumn("dbo.Institutions", "StartDate");
        }
    }
}

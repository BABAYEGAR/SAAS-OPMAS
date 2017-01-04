namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Institutions", "SubscriprionStartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Institutions", "SubscriptonEndDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Institutions", "StartDate");
            DropColumn("dbo.Institutions", "EndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Institutions", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Institutions", "StartDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Institutions", "SubscriptonEndDate");
            DropColumn("dbo.Institutions", "SubscriprionStartDate");
        }
    }
}

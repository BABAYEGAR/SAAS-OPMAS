namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FourthMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Institutions", "SubscriptionDuration", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Institutions", "SubscriptionDuration");
        }
    }
}

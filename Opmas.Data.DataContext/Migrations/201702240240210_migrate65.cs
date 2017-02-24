namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate65 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationStatistics",
                c => new
                    {
                        ApplicationStatisticId = c.Long(nullable: false, identity: true),
                        Action = c.String(),
                        DateOccured = c.DateTime(nullable: false),
                        InstitutionId = c.Long(),
                        LoggedInUserId = c.Long(),
                    })
                .PrimaryKey(t => t.ApplicationStatisticId);
            
            AddColumn("dbo.AppUsers", "DashboardTheme", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppUsers", "DashboardTheme");
            DropTable("dbo.ApplicationStatistics");
        }
    }
}

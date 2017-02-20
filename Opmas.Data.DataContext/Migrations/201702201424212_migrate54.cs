namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate54 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationNotifications", "InstitutionId", c => c.Long());
            CreateIndex("dbo.ApplicationNotifications", "InstitutionId");
            AddForeignKey("dbo.ApplicationNotifications", "InstitutionId", "dbo.Institutions", "InstitutionId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationNotifications", "InstitutionId", "dbo.Institutions");
            DropIndex("dbo.ApplicationNotifications", new[] { "InstitutionId" });
            DropColumn("dbo.ApplicationNotifications", "InstitutionId");
        }
    }
}

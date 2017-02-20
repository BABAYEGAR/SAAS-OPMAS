namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate53 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationNotifications",
                c => new
                    {
                        ApplicationNotificationId = c.Long(nullable: false, identity: true),
                        ItemId = c.Long(),
                        Description = c.String(),
                        NotificationType = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        CreatedBy = c.Long(),
                        AssignedTo = c.Long(),
                    })
                .PrimaryKey(t => t.ApplicationNotificationId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ApplicationNotifications");
        }
    }
}

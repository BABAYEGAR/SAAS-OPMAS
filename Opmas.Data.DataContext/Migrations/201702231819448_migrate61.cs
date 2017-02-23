namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate61 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Roles", "ManageAppointments", c => c.Boolean(nullable: false));
            DropColumn("dbo.Responsibilities", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Responsibilities", "Status", c => c.String());
            DropColumn("dbo.Roles", "ManageAppointments");
        }
    }
}

namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate39 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Roles", "ManageLeaveTypes", c => c.Boolean(nullable: false));
            AddColumn("dbo.Roles", "ManageLeave", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Roles", "ManageLeave");
            DropColumn("dbo.Roles", "ManageLeaveTypes");
        }
    }
}

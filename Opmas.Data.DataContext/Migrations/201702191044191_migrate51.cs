namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate51 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Roles", "ManageSeniorStaffLeave", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Roles", "ManageSeniorStaffLeave");
        }
    }
}

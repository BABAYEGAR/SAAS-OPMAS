namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Roles", "ManageAdminAppUsers", c => c.Boolean(nullable: false));
            AddColumn("dbo.Roles", "ManageAllInstitutions", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Roles", "ManageAllInstitutions");
            DropColumn("dbo.Roles", "ManageAdminAppUsers");
        }
    }
}

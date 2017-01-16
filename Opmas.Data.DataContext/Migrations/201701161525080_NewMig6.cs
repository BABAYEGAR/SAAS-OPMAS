namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMig6 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Roles");
            AlterColumn("dbo.Roles", "RoleId", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.Roles", "RoleId");
           
        }
        
        public override void Down()
        {
        }
    }
}

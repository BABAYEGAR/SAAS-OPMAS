namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMig5 : DbMigration
    {
        public override void Up()
        {

            DropPrimaryKey("dbo.Roles");
            AlterColumn("dbo.Employees", "RoleId", c => c.Long(nullable: true));
            AlterColumn("dbo.Roles", "RoleId", c => c.Long(nullable: true, identity: true));
            AddPrimaryKey("dbo.Roles", "RoleId");
            CreateIndex("dbo.Employees", "RoleId");
            AddForeignKey("dbo.Employees", "RoleId", "dbo.Roles", "RoleId", cascadeDelete: false);
        }
        
        public override void Down()
        {
           
        }
    }
}

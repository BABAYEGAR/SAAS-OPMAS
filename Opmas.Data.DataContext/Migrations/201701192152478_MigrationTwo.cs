namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationTwo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "RoleId", "dbo.Roles");
            DropIndex("dbo.Employees", new[] { "RoleId" });
            AlterColumn("dbo.Employees", "RoleId", c => c.Long());
            CreateIndex("dbo.Employees", "RoleId");
            AddForeignKey("dbo.Employees", "RoleId", "dbo.Roles", "RoleId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "RoleId", "dbo.Roles");
            DropIndex("dbo.Employees", new[] { "RoleId" });
            AlterColumn("dbo.Employees", "RoleId", c => c.Long(nullable: false));
            CreateIndex("dbo.Employees", "RoleId");
            AddForeignKey("dbo.Employees", "RoleId", "dbo.Roles", "RoleId", cascadeDelete: true);
        }
    }
}

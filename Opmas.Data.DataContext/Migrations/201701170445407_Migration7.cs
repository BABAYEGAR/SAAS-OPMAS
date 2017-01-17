namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "RoleId", c => c.Long(nullable: false));
            CreateIndex("dbo.Employees", "RoleId");
           AddForeignKey("dbo.Employees", "RoleId", "dbo.Roles", "RoleId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "RoleId", "dbo.Roles");
            DropIndex("dbo.Employees", new[] { "RoleId" });
            DropColumn("dbo.Employees", "RoleId");
        }
    }
}

namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AppUsers", "RoleId", "dbo.Roles");
            DropIndex("dbo.AppUsers", new[] { "RoleId" });
            AlterColumn("dbo.AppUsers", "RoleId", c => c.Long());
            CreateIndex("dbo.AppUsers", "RoleId");
            AddForeignKey("dbo.AppUsers", "RoleId", "dbo.Roles", "RoleId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppUsers", "RoleId", "dbo.Roles");
            DropIndex("dbo.AppUsers", new[] { "RoleId" });
            AlterColumn("dbo.AppUsers", "RoleId", c => c.Long(nullable: false));
            CreateIndex("dbo.AppUsers", "RoleId");
            AddForeignKey("dbo.AppUsers", "RoleId", "dbo.Roles", "RoleId", cascadeDelete: true);
        }
    }
}

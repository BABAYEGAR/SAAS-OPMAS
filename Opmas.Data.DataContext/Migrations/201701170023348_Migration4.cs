namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Roles", "InstitutionId", "dbo.Institutions");
            DropIndex("dbo.Roles", new[] { "InstitutionId" });
            AddColumn("dbo.Roles", "ManageRolePriviledges", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Roles", "InstitutionId", c => c.Long());
            CreateIndex("dbo.Roles", "InstitutionId");
            AddForeignKey("dbo.Roles", "InstitutionId", "dbo.Institutions", "InstitutionId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Roles", "InstitutionId", "dbo.Institutions");
            DropIndex("dbo.Roles", new[] { "InstitutionId" });
            AlterColumn("dbo.Roles", "InstitutionId", c => c.Long(nullable: false));
            DropColumn("dbo.Roles", "ManageRolePriviledges");
            CreateIndex("dbo.Roles", "InstitutionId");
            AddForeignKey("dbo.Roles", "InstitutionId", "dbo.Institutions", "InstitutionId", cascadeDelete: true);
        }
    }
}

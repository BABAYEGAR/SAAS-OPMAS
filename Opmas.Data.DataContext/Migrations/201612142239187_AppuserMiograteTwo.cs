namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppuserMiograteTwo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AppUsers", "InstitutionId", "dbo.Institutions");
            DropIndex("dbo.AppUsers", new[] { "InstitutionId" });
            AlterColumn("dbo.AppUsers", "InstitutionId", c => c.Long());
            CreateIndex("dbo.AppUsers", "InstitutionId");
            AddForeignKey("dbo.AppUsers", "InstitutionId", "dbo.Institutions", "InstitutionId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppUsers", "InstitutionId", "dbo.Institutions");
            DropIndex("dbo.AppUsers", new[] { "InstitutionId" });
            AlterColumn("dbo.AppUsers", "InstitutionId", c => c.Long(nullable: false));
            CreateIndex("dbo.AppUsers", "InstitutionId");
            AddForeignKey("dbo.AppUsers", "InstitutionId", "dbo.Institutions", "InstitutionId", cascadeDelete: true);
        }
    }
}

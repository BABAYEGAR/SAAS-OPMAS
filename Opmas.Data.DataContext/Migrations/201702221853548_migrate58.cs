namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate58 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppraisalPositionMappings", "InstitutionId", c => c.Long(nullable: false));
            CreateIndex("dbo.AppraisalPositionMappings", "InstitutionId");
            AddForeignKey("dbo.AppraisalPositionMappings", "InstitutionId", "dbo.Institutions", "InstitutionId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppraisalPositionMappings", "InstitutionId", "dbo.Institutions");
            DropIndex("dbo.AppraisalPositionMappings", new[] { "InstitutionId" });
            DropColumn("dbo.AppraisalPositionMappings", "InstitutionId");
        }
    }
}

namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate57 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AppraisalPositionMappings", "AppraisalId", "dbo.AppraisalFactors");
            DropIndex("dbo.AppraisalPositionMappings", new[] { "AppraisalId" });
            AddColumn("dbo.AppraisalPositionMappings", "AppraisalCategoryId", c => c.Long(nullable: false));
            CreateIndex("dbo.AppraisalPositionMappings", "AppraisalCategoryId");
            AddForeignKey("dbo.AppraisalPositionMappings", "AppraisalCategoryId", "dbo.AppraisalCategories", "AppraisalCategoryId", cascadeDelete: true);
            DropColumn("dbo.AppraisalPositionMappings", "AppraisalId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppraisalPositionMappings", "AppraisalId", c => c.Long(nullable: false));
            DropForeignKey("dbo.AppraisalPositionMappings", "AppraisalCategoryId", "dbo.AppraisalCategories");
            DropIndex("dbo.AppraisalPositionMappings", new[] { "AppraisalCategoryId" });
            DropColumn("dbo.AppraisalPositionMappings", "AppraisalCategoryId");
            CreateIndex("dbo.AppraisalPositionMappings", "AppraisalId");
            AddForeignKey("dbo.AppraisalPositionMappings", "AppraisalId", "dbo.AppraisalFactors", "AppraisalFactorId", cascadeDelete: true);
        }
    }
}

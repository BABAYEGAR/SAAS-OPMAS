namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate56 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppraisalCategories",
                c => new
                    {
                        AppraisalCategoryId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        MaximumScore = c.Long(),
                        CurrentScore = c.Long(),
                        InstitutionId = c.Long(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.AppraisalCategoryId)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: false)
                .Index(t => t.InstitutionId);
            
            CreateTable(
                "dbo.AppraisalFactors",
                c => new
                    {
                        AppraisalFactorId = c.Long(nullable: false, identity: true),
                        Factor = c.String(),
                        Description = c.String(),
                        FactorScore = c.Long(nullable: false),
                        AppraisalCategoryId = c.Long(nullable: false),
                        InstitutionId = c.Long(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.AppraisalFactorId)
                .ForeignKey("dbo.AppraisalCategories", t => t.AppraisalCategoryId, cascadeDelete: false)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: false)
                .Index(t => t.AppraisalCategoryId)
                .Index(t => t.InstitutionId);
            
            CreateTable(
                "dbo.AppraisalPositionMappings",
                c => new
                    {
                        AppraisalPositionMappingId = c.Long(nullable: false, identity: true),
                        AppraisalId = c.Long(nullable: false),
                        EmploymentPositionId = c.Long(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.AppraisalPositionMappingId)
                .ForeignKey("dbo.AppraisalFactors", t => t.AppraisalId, cascadeDelete: false)
                .ForeignKey("dbo.EmploymentPositions", t => t.EmploymentPositionId, cascadeDelete: false)
                .Index(t => t.AppraisalId)
                .Index(t => t.EmploymentPositionId);
            
            AddColumn("dbo.Roles", "ManageAppriasalSetting", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppraisalPositionMappings", "EmploymentPositionId", "dbo.EmploymentPositions");
            DropForeignKey("dbo.AppraisalPositionMappings", "AppraisalId", "dbo.AppraisalFactors");
            DropForeignKey("dbo.AppraisalFactors", "InstitutionId", "dbo.Institutions");
            DropForeignKey("dbo.AppraisalFactors", "AppraisalCategoryId", "dbo.AppraisalCategories");
            DropForeignKey("dbo.AppraisalCategories", "InstitutionId", "dbo.Institutions");
            DropIndex("dbo.AppraisalPositionMappings", new[] { "EmploymentPositionId" });
            DropIndex("dbo.AppraisalPositionMappings", new[] { "AppraisalId" });
            DropIndex("dbo.AppraisalFactors", new[] { "InstitutionId" });
            DropIndex("dbo.AppraisalFactors", new[] { "AppraisalCategoryId" });
            DropIndex("dbo.AppraisalCategories", new[] { "InstitutionId" });
            DropColumn("dbo.Roles", "ManageAppriasalSetting");
            DropTable("dbo.AppraisalPositionMappings");
            DropTable("dbo.AppraisalFactors");
            DropTable("dbo.AppraisalCategories");
        }
    }
}

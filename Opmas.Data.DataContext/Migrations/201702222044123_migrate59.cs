namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate59 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppraisalScoreCards",
                c => new
                    {
                        AppraisalScoreCardId = c.Long(nullable: false, identity: true),
                        EmployeeId = c.Long(),
                        AppraisalFactorId = c.Long(),
                        Score = c.Long(),
                        InstitutionId = c.Long(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.AppraisalScoreCardId)
                .ForeignKey("dbo.AppraisalFactors", t => t.AppraisalFactorId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.AppraisalFactorId)
                .Index(t => t.InstitutionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppraisalScoreCards", "InstitutionId", "dbo.Institutions");
            DropForeignKey("dbo.AppraisalScoreCards", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.AppraisalScoreCards", "AppraisalFactorId", "dbo.AppraisalFactors");
            DropIndex("dbo.AppraisalScoreCards", new[] { "InstitutionId" });
            DropIndex("dbo.AppraisalScoreCards", new[] { "AppraisalFactorId" });
            DropIndex("dbo.AppraisalScoreCards", new[] { "EmployeeId" });
            DropTable("dbo.AppraisalScoreCards");
        }
    }
}

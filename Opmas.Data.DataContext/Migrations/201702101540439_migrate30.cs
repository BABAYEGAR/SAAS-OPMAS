namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate30 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentAllowances",
                c => new
                    {
                        PaymentAllowanceId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Amount = c.Long(nullable: false),
                        InstitutionId = c.Long(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentAllowanceId)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: false)
                .Index(t => t.InstitutionId);
            
            CreateTable(
                "dbo.PaymentDeductions",
                c => new
                    {
                        PaymentDeductionId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Rate = c.Long(),
                        InstitutionId = c.Long(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentDeductionId)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: false)
                .Index(t => t.InstitutionId);
            
            CreateTable(
                "dbo.PositionAllowanceMappings",
                c => new
                    {
                        PositionAllowanceMappingId = c.Long(nullable: false, identity: true),
                        PaymentAllowanceId = c.Long(nullable: false),
                        EmploymentPositionId = c.Long(nullable: false),
                        InstitutionId = c.Long(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.PositionAllowanceMappingId)
                .ForeignKey("dbo.EmploymentPositions", t => t.EmploymentPositionId, cascadeDelete: false)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: false)
                .ForeignKey("dbo.PaymentAllowances", t => t.PaymentAllowanceId, cascadeDelete: false)
                .Index(t => t.PaymentAllowanceId)
                .Index(t => t.EmploymentPositionId)
                .Index(t => t.InstitutionId);
            
            CreateTable(
                "dbo.PositionDeductionMappings",
                c => new
                    {
                        PositionDeductionMappingId = c.Long(nullable: false, identity: true),
                        PaymentDeductionId = c.Long(nullable: false),
                        EmploymentPositionId = c.Long(nullable: false),
                        InstitutionId = c.Long(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.PositionDeductionMappingId)
                .ForeignKey("dbo.EmploymentPositions", t => t.EmploymentPositionId, cascadeDelete: false)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: false)
                .ForeignKey("dbo.PaymentDeductions", t => t.PaymentDeductionId, cascadeDelete: false)
                .Index(t => t.PaymentDeductionId)
                .Index(t => t.EmploymentPositionId)
                .Index(t => t.InstitutionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PositionDeductionMappings", "PaymentDeductionId", "dbo.PaymentDeductions");
            DropForeignKey("dbo.PositionDeductionMappings", "InstitutionId", "dbo.Institutions");
            DropForeignKey("dbo.PositionDeductionMappings", "EmploymentPositionId", "dbo.EmploymentPositions");
            DropForeignKey("dbo.PositionAllowanceMappings", "PaymentAllowanceId", "dbo.PaymentAllowances");
            DropForeignKey("dbo.PositionAllowanceMappings", "InstitutionId", "dbo.Institutions");
            DropForeignKey("dbo.PositionAllowanceMappings", "EmploymentPositionId", "dbo.EmploymentPositions");
            DropForeignKey("dbo.PaymentDeductions", "InstitutionId", "dbo.Institutions");
            DropForeignKey("dbo.PaymentAllowances", "InstitutionId", "dbo.Institutions");
            DropIndex("dbo.PositionDeductionMappings", new[] { "InstitutionId" });
            DropIndex("dbo.PositionDeductionMappings", new[] { "EmploymentPositionId" });
            DropIndex("dbo.PositionDeductionMappings", new[] { "PaymentDeductionId" });
            DropIndex("dbo.PositionAllowanceMappings", new[] { "InstitutionId" });
            DropIndex("dbo.PositionAllowanceMappings", new[] { "EmploymentPositionId" });
            DropIndex("dbo.PositionAllowanceMappings", new[] { "PaymentAllowanceId" });
            DropIndex("dbo.PaymentDeductions", new[] { "InstitutionId" });
            DropIndex("dbo.PaymentAllowances", new[] { "InstitutionId" });
            DropTable("dbo.PositionDeductionMappings");
            DropTable("dbo.PositionAllowanceMappings");
            DropTable("dbo.PaymentDeductions");
            DropTable("dbo.PaymentAllowances");
        }
    }
}

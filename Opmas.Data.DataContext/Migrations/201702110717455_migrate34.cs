namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate34 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InstitutionStructures",
                c => new
                    {
                        InstitutionStructureId = c.Long(nullable: false, identity: true),
                        Faculty = c.Boolean(nullable: false),
                        InstitutionId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.InstitutionStructureId)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: true)
                .Index(t => t.InstitutionId);
            
            CreateTable(
                "dbo.PaymentDeductionRequests",
                c => new
                    {
                        PaymentDeductionRequestId = c.Long(nullable: false, identity: true),
                        RequestTitle = c.String(),
                        Reason = c.String(),
                        Status = c.String(),
                        Comment = c.String(),
                        EmployeeId = c.Long(),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentDeductionRequestId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentDeductionRequests", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.InstitutionStructures", "InstitutionId", "dbo.Institutions");
            DropIndex("dbo.PaymentDeductionRequests", new[] { "EmployeeId" });
            DropIndex("dbo.InstitutionStructures", new[] { "InstitutionId" });
            DropTable("dbo.PaymentDeductionRequests");
            DropTable("dbo.InstitutionStructures");
        }
    }
}

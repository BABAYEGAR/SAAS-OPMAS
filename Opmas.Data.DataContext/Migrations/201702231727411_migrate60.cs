namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate60 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeResponsibilityMappings",
                c => new
                    {
                        EmployeeResponsibilityMappingId = c.Long(nullable: false, identity: true),
                        ResponsibilityId = c.Long(nullable: false),
                        EmployeeId = c.Long(nullable: false),
                        InstitutionId = c.Long(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeResponsibilityMappingId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: false)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: false)
                .ForeignKey("dbo.Responsibilities", t => t.ResponsibilityId, cascadeDelete: false)
                .Index(t => t.ResponsibilityId)
                .Index(t => t.EmployeeId)
                .Index(t => t.InstitutionId);
            
            CreateTable(
                "dbo.Responsibilities",
                c => new
                    {
                        ResponsibilityId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Status = c.String(),
                        InstitutionId = c.Long(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ResponsibilityId)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: false)
                .Index(t => t.InstitutionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeResponsibilityMappings", "ResponsibilityId", "dbo.Responsibilities");
            DropForeignKey("dbo.Responsibilities", "InstitutionId", "dbo.Institutions");
            DropForeignKey("dbo.EmployeeResponsibilityMappings", "InstitutionId", "dbo.Institutions");
            DropForeignKey("dbo.EmployeeResponsibilityMappings", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Responsibilities", new[] { "InstitutionId" });
            DropIndex("dbo.EmployeeResponsibilityMappings", new[] { "InstitutionId" });
            DropIndex("dbo.EmployeeResponsibilityMappings", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeeResponsibilityMappings", new[] { "ResponsibilityId" });
            DropTable("dbo.Responsibilities");
            DropTable("dbo.EmployeeResponsibilityMappings");
        }
    }
}

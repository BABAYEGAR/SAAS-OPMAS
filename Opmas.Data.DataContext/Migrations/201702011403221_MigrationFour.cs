namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationFour : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmploymentTypes",
                c => new
                    {
                        EmploymentTypeId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        InstitutionId = c.Long(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.EmploymentTypeId)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: true)
                .Index(t => t.InstitutionId);
            
            AddColumn("dbo.EmployeeMedicalDatas", "EmploymentTypeId", c => c.Long(nullable: false));
            AddColumn("dbo.Roles", "ManageEmploymentTypes", c => c.String());
            DropColumn("dbo.EmployeeMedicalDatas", "EmploymentType");
            DropColumn("dbo.EmployeeWorkDatas", "EmploymentType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmployeeWorkDatas", "EmploymentType", c => c.String());
            AddColumn("dbo.EmployeeMedicalDatas", "EmploymentType", c => c.String(nullable: false));
            DropForeignKey("dbo.EmployeeWorkDatas", "EmploymentTypeId", "dbo.EmploymentTypes");
            DropForeignKey("dbo.EmploymentTypes", "InstitutionId", "dbo.Institutions");
            DropIndex("dbo.EmploymentTypes", new[] { "InstitutionId" });
            DropIndex("dbo.EmployeeWorkDatas", new[] { "EmploymentTypeId" });
            DropColumn("dbo.Roles", "ManageEmploymentTypes");
            DropColumn("dbo.EmployeeWorkDatas", "EmploymentTypeId");
            DropColumn("dbo.EmployeeMedicalDatas", "EmploymentTypeId");
            DropTable("dbo.EmploymentTypes");
        }
    }
}

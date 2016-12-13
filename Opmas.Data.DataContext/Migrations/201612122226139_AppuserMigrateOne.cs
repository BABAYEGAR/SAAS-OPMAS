namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppuserMigrateOne : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Faculties", "InstitutionId", "dbo.Institutions");
            DropForeignKey("dbo.Departments", "FacultyId", "dbo.Faculties");
            DropForeignKey("dbo.Departments", "InstitutionId", "dbo.Institutions");
            DropIndex("dbo.Departments", new[] { "FacultyId" });
            DropIndex("dbo.Departments", new[] { "InstitutionId" });
            DropIndex("dbo.Faculties", new[] { "InstitutionId" });
            CreateTable(
                "dbo.AppUsers",
                c => new
                    {
                        AppUserId = c.Long(nullable: false, identity: true),
                        Firstname = c.String(nullable: false, maxLength: 100),
                        Middlename = c.String(maxLength: 100),
                        Lastname = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        Mobile = c.String(nullable: false, maxLength: 100),
                        Password = c.String(),
                        Role = c.String(),
                        AppUserImage = c.String(),
                        FingerPrint = c.String(),
                        RememberMe = c.Boolean(nullable: false),
                        EmployeeId = c.Long(),
                        InstitutionId = c.Long(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.AppUserId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.InstitutionId);
            
            DropTable("dbo.Departments");
            DropTable("dbo.Faculties");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Faculties",
                c => new
                    {
                        FacultyId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        InstitutionId = c.Long(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.FacultyId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        FacultyId = c.Long(nullable: false),
                        InstitutionId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            DropForeignKey("dbo.AppUsers", "InstitutionId", "dbo.Institutions");
            DropForeignKey("dbo.AppUsers", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.AppUsers", new[] { "InstitutionId" });
            DropIndex("dbo.AppUsers", new[] { "EmployeeId" });
            DropTable("dbo.AppUsers");
            CreateIndex("dbo.Faculties", "InstitutionId");
            CreateIndex("dbo.Departments", "InstitutionId");
            CreateIndex("dbo.Departments", "FacultyId");
            AddForeignKey("dbo.Departments", "InstitutionId", "dbo.Institutions", "InstitutionId", cascadeDelete: true);
            AddForeignKey("dbo.Departments", "FacultyId", "dbo.Faculties", "FacultyId", cascadeDelete: true);
            AddForeignKey("dbo.Faculties", "InstitutionId", "dbo.Institutions", "InstitutionId", cascadeDelete: true);
        }
    }
}

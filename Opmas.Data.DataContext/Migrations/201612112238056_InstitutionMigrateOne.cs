namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InstitutionMigrateOne : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        FacultyId = c.Long(nullable: false),
                        InstitutionId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentId)
                .ForeignKey("dbo.Faculties", t => t.FacultyId, cascadeDelete: false)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: false)
                .Index(t => t.FacultyId)
                .Index(t => t.InstitutionId);
            
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
                .PrimaryKey(t => t.FacultyId)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: true)
                .Index(t => t.InstitutionId);
            
            CreateTable(
                "dbo.Institutions",
                c => new
                    {
                        InstitutionId = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Motto = c.String(),
                        Logo = c.String(),
                        Location = c.String(),
                        InstitutionCategory = c.String(),
                        ContactNumber = c.String(nullable: false),
                        ContactEmail = c.String(nullable: false),
                        PackageId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.InstitutionId)
                .ForeignKey("dbo.Packages", t => t.PackageId, cascadeDelete: true)
                .Index(t => t.PackageId);
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        PackageId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Amount = c.Double(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.PackageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "InstitutionId", "dbo.Institutions");
            DropForeignKey("dbo.Departments", "FacultyId", "dbo.Faculties");
            DropForeignKey("dbo.Faculties", "InstitutionId", "dbo.Institutions");
            DropForeignKey("dbo.Institutions", "PackageId", "dbo.Packages");
            DropIndex("dbo.Institutions", new[] { "PackageId" });
            DropIndex("dbo.Faculties", new[] { "InstitutionId" });
            DropIndex("dbo.Departments", new[] { "InstitutionId" });
            DropIndex("dbo.Departments", new[] { "FacultyId" });
            DropTable("dbo.Packages");
            DropTable("dbo.Institutions");
            DropTable("dbo.Faculties");
            DropTable("dbo.Departments");
        }
    }
}

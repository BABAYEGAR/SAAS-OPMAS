namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeTrainings",
                c => new
                    {
                        EmployeeTrainingId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        EmployeeId = c.Long(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeTrainingId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.TrainingCategories",
                c => new
                    {
                        TrainingCategoryId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        InstitutionId = c.Long(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.TrainingCategoryId)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: true)
                .Index(t => t.InstitutionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrainingCategories", "InstitutionId", "dbo.Institutions");
            DropForeignKey("dbo.EmployeeTrainings", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.TrainingCategories", new[] { "InstitutionId" });
            DropIndex("dbo.EmployeeTrainings", new[] { "EmployeeId" });
            DropTable("dbo.TrainingCategories");
            DropTable("dbo.EmployeeTrainings");
        }
    }
}

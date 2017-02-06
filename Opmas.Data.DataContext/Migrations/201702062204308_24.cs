namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _24 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmploymentCategories",
                c => new
                    {
                        EmploymentCategoryId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        InstitutionId = c.Long(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.EmploymentCategoryId)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: true)
                .Index(t => t.InstitutionId);
            
            AddColumn("dbo.EmployeeWorkDatas", "EmploymentCategoryId", c => c.Long());
            CreateIndex("dbo.EmployeeWorkDatas", "EmploymentCategoryId");
            AddForeignKey("dbo.EmployeeWorkDatas", "EmploymentCategoryId", "dbo.EmploymentCategories", "EmploymentCategoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeWorkDatas", "EmploymentCategoryId", "dbo.EmploymentCategories");
            DropForeignKey("dbo.EmploymentCategories", "InstitutionId", "dbo.Institutions");
            DropIndex("dbo.EmploymentCategories", new[] { "InstitutionId" });
            DropIndex("dbo.EmployeeWorkDatas", new[] { "EmploymentCategoryId" });
            DropColumn("dbo.EmployeeWorkDatas", "EmploymentCategoryId");
            DropTable("dbo.EmploymentCategories");
        }
    }
}

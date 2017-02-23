namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate63 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PositionChanges",
                c => new
                    {
                        PositionChangeId = c.Long(nullable: false, identity: true),
                        Action = c.String(),
                        EmploymentPositionId = c.Long(nullable: false),
                        EmployeeId = c.Long(nullable: false),
                        InstitutionId = c.Long(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.PositionChangeId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: false)
                .ForeignKey("dbo.EmploymentPositions", t => t.EmploymentPositionId, cascadeDelete: false)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: false)
                .Index(t => t.EmploymentPositionId)
                .Index(t => t.EmployeeId)
                .Index(t => t.InstitutionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PositionChanges", "InstitutionId", "dbo.Institutions");
            DropForeignKey("dbo.PositionChanges", "EmploymentPositionId", "dbo.EmploymentPositions");
            DropForeignKey("dbo.PositionChanges", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.PositionChanges", new[] { "InstitutionId" });
            DropIndex("dbo.PositionChanges", new[] { "EmployeeId" });
            DropIndex("dbo.PositionChanges", new[] { "EmploymentPositionId" });
            DropTable("dbo.PositionChanges");
        }
    }
}

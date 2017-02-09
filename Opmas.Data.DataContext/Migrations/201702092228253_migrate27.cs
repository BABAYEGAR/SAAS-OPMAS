namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate27 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmploymentPositions",
                c => new
                    {
                        EmploymentPositionId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        InstitutionId = c.Long(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.EmploymentPositionId)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: true)
                .Index(t => t.InstitutionId);
            
            AddColumn("dbo.EmployeeMedicalDatas", "EmploymentPositionId", c => c.Long(nullable: false));
            AddColumn("dbo.EmployeeWorkDatas", "EmploymentPositionId", c => c.Long());
            AddColumn("dbo.Roles", "ManageEmploymentPositions", c => c.Boolean(nullable: false));
            CreateIndex("dbo.EmployeeWorkDatas", "EmploymentPositionId");
            AddForeignKey("dbo.EmployeeWorkDatas", "EmploymentPositionId", "dbo.EmploymentPositions", "EmploymentPositionId");
            DropColumn("dbo.EmployeeMedicalDatas", "EmploymentPosition");
            DropColumn("dbo.EmployeeWorkDatas", "PositionHeld");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmployeeWorkDatas", "PositionHeld", c => c.String());
            AddColumn("dbo.EmployeeMedicalDatas", "EmploymentPosition", c => c.String(nullable: false));
            DropForeignKey("dbo.EmployeeWorkDatas", "EmploymentPositionId", "dbo.EmploymentPositions");
            DropForeignKey("dbo.EmploymentPositions", "InstitutionId", "dbo.Institutions");
            DropIndex("dbo.EmploymentPositions", new[] { "InstitutionId" });
            DropIndex("dbo.EmployeeWorkDatas", new[] { "EmploymentPositionId" });
            DropColumn("dbo.Roles", "ManageEmploymentPositions");
            DropColumn("dbo.EmployeeWorkDatas", "EmploymentPositionId");
            DropColumn("dbo.EmployeeMedicalDatas", "EmploymentPositionId");
            DropTable("dbo.EmploymentPositions");
        }
    }
}

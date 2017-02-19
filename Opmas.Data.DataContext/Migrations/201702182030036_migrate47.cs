namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate47 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Leaves",
                c => new
                    {
                        LeaveId = c.Long(nullable: false, identity: true),
                        Reason = c.String(),
                        LeaveTypeId = c.Long(nullable: false),
                        EmployeeId = c.Long(nullable: false),
                        InstitutionId = c.Long(nullable: false),
                        Status = c.String(),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.LeaveId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: false)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: false)
                .ForeignKey("dbo.LeaveTypes", t => t.LeaveTypeId, cascadeDelete: false)
                .Index(t => t.LeaveTypeId)
                .Index(t => t.EmployeeId)
                .Index(t => t.InstitutionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leaves", "LeaveTypeId", "dbo.LeaveTypes");
            DropForeignKey("dbo.Leaves", "InstitutionId", "dbo.Institutions");
            DropForeignKey("dbo.Leaves", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Leaves", new[] { "InstitutionId" });
            DropIndex("dbo.Leaves", new[] { "EmployeeId" });
            DropIndex("dbo.Leaves", new[] { "LeaveTypeId" });
            DropTable("dbo.Leaves");
        }
    }
}

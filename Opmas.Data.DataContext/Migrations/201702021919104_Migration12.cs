namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration12 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmployeeTrainings", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.EmployeeTrainings", new[] { "EmployeeId" });
            CreateTable(
                "dbo.EmployeeTrainingMappings",
                c => new
                    {
                        EmployeeTrainingMappingId = c.Long(nullable: false, identity: true),
                        EmployeeId = c.Long(nullable: false),
                        EmployeeTrainingId = c.Long(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeTrainingMappingId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.EmployeeTrainings", t => t.EmployeeTrainingId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.EmployeeTrainingId);
            
            DropColumn("dbo.EmployeeTrainings", "EmployeeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmployeeTrainings", "EmployeeId", c => c.Long(nullable: false));
            DropForeignKey("dbo.EmployeeTrainingMappings", "EmployeeTrainingId", "dbo.EmployeeTrainings");
            DropForeignKey("dbo.EmployeeTrainingMappings", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.EmployeeTrainingMappings", new[] { "EmployeeTrainingId" });
            DropIndex("dbo.EmployeeTrainingMappings", new[] { "EmployeeId" });
            DropTable("dbo.EmployeeTrainingMappings");
            CreateIndex("dbo.EmployeeTrainings", "EmployeeId");
            AddForeignKey("dbo.EmployeeTrainings", "EmployeeId", "dbo.Employees", "EmployeeId", cascadeDelete: true);
        }
    }
}

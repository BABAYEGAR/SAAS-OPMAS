namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmployeeWorkDatas", "EmploymentTypeId", "dbo.EmploymentTypes");
            DropIndex("dbo.EmployeeWorkDatas", new[] { "EmploymentTypeId" });
            AlterColumn("dbo.EmployeeWorkDatas", "EmploymentTypeId", c => c.Long());
            CreateIndex("dbo.EmployeeWorkDatas", "EmploymentTypeId");
            AddForeignKey("dbo.EmployeeWorkDatas", "EmploymentTypeId", "dbo.EmploymentTypes", "EmploymentTypeId");
            DropColumn("dbo.EmploymentTypes", "CreatedBy");
            DropColumn("dbo.EmploymentTypes", "DateCreated");
            DropColumn("dbo.EmploymentTypes", "DateLastModified");
            DropColumn("dbo.EmploymentTypes", "LastModifiedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmploymentTypes", "LastModifiedBy", c => c.Long(nullable: false));
            AddColumn("dbo.EmploymentTypes", "DateLastModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmploymentTypes", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmploymentTypes", "CreatedBy", c => c.Long(nullable: false));
            DropForeignKey("dbo.EmployeeWorkDatas", "EmploymentTypeId", "dbo.EmploymentTypes");
            DropIndex("dbo.EmployeeWorkDatas", new[] { "EmploymentTypeId" });
            AlterColumn("dbo.EmployeeWorkDatas", "EmploymentTypeId", c => c.Long(nullable: false));
            CreateIndex("dbo.EmployeeWorkDatas", "EmploymentTypeId");
            AddForeignKey("dbo.EmployeeWorkDatas", "EmploymentTypeId", "dbo.EmploymentTypes", "EmploymentTypeId", cascadeDelete: true);
        }
    }
}

namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationThree : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeMedicalDatas", "EmploymentType", c => c.String(nullable: true));
            AddColumn("dbo.EmployeeMedicalDatas", "EmploymentCategory", c => c.String(nullable: true));
            AddColumn("dbo.EmployeeMedicalDatas", "EmploymentPosition", c => c.String(nullable: true));
            AddColumn("dbo.EmployeeMedicalDatas", "EmploymentDate", c => c.DateTime(nullable: true));
            AddColumn("dbo.EmployeeMedicalDatas", "RoleId", c => c.Long(nullable: true));
            AddColumn("dbo.EmployeeMedicalDatas", "DepartmentId", c => c.Long(nullable: true));
            AddColumn("dbo.EmployeeMedicalDatas", "FacultyId", c => c.Long(nullable: true));
            AddColumn("dbo.EmployeeMedicalDatas", "UnitId", c => c.Long(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmployeeMedicalDatas", "UnitId");
            DropColumn("dbo.EmployeeMedicalDatas", "FacultyId");
            DropColumn("dbo.EmployeeMedicalDatas", "DepartmentId");
            DropColumn("dbo.EmployeeMedicalDatas", "RoleId");
            DropColumn("dbo.EmployeeMedicalDatas", "EmploymentDate");
            DropColumn("dbo.EmployeeMedicalDatas", "EmploymentPosition");
            DropColumn("dbo.EmployeeMedicalDatas", "EmploymentCategory");
            DropColumn("dbo.EmployeeMedicalDatas", "EmploymentType");
        }
    }
}

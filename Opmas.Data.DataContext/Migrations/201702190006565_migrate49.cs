namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate49 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AppUsers", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Units", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Departments", "FacultyId", "dbo.Faculties");
            DropForeignKey("dbo.Employees", "FacultyId", "dbo.Faculties");
            DropForeignKey("dbo.AppUsers", "FacultyId", "dbo.Faculties");
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropIndex("dbo.Employees", new[] { "FacultyId" });
            AddColumn("dbo.Departments", "EmployeeId", c => c.Long());
            AddColumn("dbo.Faculties", "EmployeeId", c => c.Long());
            AddColumn("dbo.Leaves", "DepartmentId", c => c.Long(nullable: false));
            AddColumn("dbo.Leaves", "FacultyId", c => c.Long(nullable: false));
            AddColumn("dbo.Leaves", "StartDate", c => c.DateTime());
            AddColumn("dbo.Leaves", "EndDate", c => c.DateTime());
            AddColumn("dbo.Leaves", "Comment", c => c.String());
            AlterColumn("dbo.Departments", "DepartmentId", c => c.Long(nullable: false));
            AlterColumn("dbo.Faculties", "FacultyId", c => c.Long(nullable: false));
            CreateIndex("dbo.Leaves", "DepartmentId");
            CreateIndex("dbo.Leaves", "FacultyId");
            AddForeignKey("dbo.Leaves", "DepartmentId", "dbo.Departments", "DepartmentId", cascadeDelete: true);
            AddForeignKey("dbo.Leaves", "FacultyId", "dbo.Faculties", "FacultyId", cascadeDelete: true);
            AddForeignKey("dbo.Units", "DepartmentId", "dbo.Departments", "DepartmentId", cascadeDelete: true);
            AddForeignKey("dbo.AppUsers", "DepartmentId", "dbo.Departments", "DepartmentId");
            AddForeignKey("dbo.Departments", "FacultyId", "dbo.Faculties", "FacultyId");
            AddForeignKey("dbo.AppUsers", "FacultyId", "dbo.Faculties", "FacultyId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppUsers", "FacultyId", "dbo.Faculties");
            DropForeignKey("dbo.Departments", "FacultyId", "dbo.Faculties");
            DropForeignKey("dbo.AppUsers", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Units", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Leaves", "FacultyId", "dbo.Faculties");
            DropForeignKey("dbo.Leaves", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Leaves", new[] { "FacultyId" });
            DropIndex("dbo.Leaves", new[] { "DepartmentId" });
            DropIndex("dbo.Faculties", new[] { "FacultyId" });
            DropIndex("dbo.Departments", new[] { "DepartmentId" });
            DropPrimaryKey("dbo.Faculties");
            DropPrimaryKey("dbo.Departments");
            AlterColumn("dbo.Faculties", "FacultyId", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Departments", "DepartmentId", c => c.Long(nullable: false, identity: true));
            DropColumn("dbo.Leaves", "Comment");
            DropColumn("dbo.Leaves", "EndDate");
            DropColumn("dbo.Leaves", "StartDate");
            DropColumn("dbo.Leaves", "FacultyId");
            DropColumn("dbo.Leaves", "DepartmentId");
            DropColumn("dbo.Faculties", "EmployeeId");
            DropColumn("dbo.Departments", "EmployeeId");
            AddPrimaryKey("dbo.Faculties", "FacultyId");
            AddPrimaryKey("dbo.Departments", "DepartmentId");
            CreateIndex("dbo.Employees", "FacultyId");
            CreateIndex("dbo.Employees", "DepartmentId");
            AddForeignKey("dbo.AppUsers", "FacultyId", "dbo.Faculties", "FacultyId");
            AddForeignKey("dbo.Employees", "FacultyId", "dbo.Faculties", "FacultyId");
            AddForeignKey("dbo.Departments", "FacultyId", "dbo.Faculties", "FacultyId");
            AddForeignKey("dbo.Units", "DepartmentId", "dbo.Departments", "DepartmentId", cascadeDelete: true);
            AddForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments", "DepartmentId");
            AddForeignKey("dbo.AppUsers", "DepartmentId", "dbo.Departments", "DepartmentId");
        }
    }
}

namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration14 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AppUsers", "RoleId", "dbo.Roles");
            DropIndex("dbo.AppUsers", new[] { "RoleId" });
            AddColumn("dbo.AppUsers", "DepartmentId", c => c.Long());
            AddColumn("dbo.AppUsers", "FacultyId", c => c.Long());
            AlterColumn("dbo.AppUsers", "RoleId", c => c.Long());
            CreateIndex("dbo.AppUsers", "RoleId");
            CreateIndex("dbo.AppUsers", "DepartmentId");
            CreateIndex("dbo.AppUsers", "FacultyId");
            AddForeignKey("dbo.AppUsers", "DepartmentId", "dbo.Departments", "DepartmentId");
            AddForeignKey("dbo.AppUsers", "FacultyId", "dbo.Faculties", "FacultyId");
            AddForeignKey("dbo.AppUsers", "RoleId", "dbo.Roles", "RoleId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppUsers", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.AppUsers", "FacultyId", "dbo.Faculties");
            DropForeignKey("dbo.AppUsers", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.AppUsers", new[] { "FacultyId" });
            DropIndex("dbo.AppUsers", new[] { "DepartmentId" });
            DropIndex("dbo.AppUsers", new[] { "RoleId" });
            AlterColumn("dbo.AppUsers", "RoleId", c => c.Long(nullable: false));
            DropColumn("dbo.AppUsers", "FacultyId");
            DropColumn("dbo.AppUsers", "DepartmentId");
            CreateIndex("dbo.AppUsers", "RoleId");
            AddForeignKey("dbo.AppUsers", "RoleId", "dbo.Roles", "RoleId", cascadeDelete: true);
        }
    }
}

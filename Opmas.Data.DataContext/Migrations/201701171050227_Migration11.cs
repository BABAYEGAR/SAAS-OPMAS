namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AppUsers", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.AppUsers", "FacultyId", "dbo.Faculties");
            DropIndex("dbo.AppUsers", new[] { "DepartmentId" });
            DropIndex("dbo.AppUsers", new[] { "FacultyId" });
            DropColumn("dbo.AppUsers", "DepartmentId");
            DropColumn("dbo.AppUsers", "FacultyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppUsers", "FacultyId", c => c.Long(nullable: false));
            AddColumn("dbo.AppUsers", "DepartmentId", c => c.Long(nullable: false));
            CreateIndex("dbo.AppUsers", "FacultyId");
            CreateIndex("dbo.AppUsers", "DepartmentId");
            AddForeignKey("dbo.AppUsers", "FacultyId", "dbo.Faculties", "FacultyId", cascadeDelete: true);
            AddForeignKey("dbo.AppUsers", "DepartmentId", "dbo.Departments", "DepartmentId", cascadeDelete: true);
        }
    }
}

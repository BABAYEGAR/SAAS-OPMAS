namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate23 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Departments", "FacultyId", "dbo.Faculties");
            DropIndex("dbo.Departments", new[] { "FacultyId" });
            AlterColumn("dbo.Departments", "FacultyId", c => c.Long());
            AlterColumn("dbo.Roles", "ManageEmploymentTypes", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Departments", "FacultyId");
            AddForeignKey("dbo.Departments", "FacultyId", "dbo.Faculties", "FacultyId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "FacultyId", "dbo.Faculties");
            DropIndex("dbo.Departments", new[] { "FacultyId" });
            AlterColumn("dbo.Roles", "ManageEmploymentTypes", c => c.String());
            AlterColumn("dbo.Departments", "FacultyId", c => c.Long(nullable: false));
            CreateIndex("dbo.Departments", "FacultyId");
            AddForeignKey("dbo.Departments", "FacultyId", "dbo.Faculties", "FacultyId", cascadeDelete: true);
        }
    }
}

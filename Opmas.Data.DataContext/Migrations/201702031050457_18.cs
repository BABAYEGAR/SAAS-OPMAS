namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _18 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Selected", c => c.Boolean(nullable: false));
            AddColumn("dbo.EmployeeTrainings", "TrainingCategoryId", c => c.Long(nullable: false));
            CreateIndex("dbo.EmployeeTrainings", "TrainingCategoryId");
            AddForeignKey("dbo.EmployeeTrainings", "TrainingCategoryId", "dbo.TrainingCategories", "TrainingCategoryId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeTrainings", "TrainingCategoryId", "dbo.TrainingCategories");
            DropIndex("dbo.EmployeeTrainings", new[] { "TrainingCategoryId" });
            DropColumn("dbo.EmployeeTrainings", "TrainingCategoryId");
            DropColumn("dbo.Employees", "Selected");
        }
    }
}

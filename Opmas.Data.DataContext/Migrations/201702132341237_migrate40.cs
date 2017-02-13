namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate40 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmploymentCategories", "Description", c => c.String());
            AddColumn("dbo.EmploymentPositions", "Description", c => c.String());
            AddColumn("dbo.EmploymentTypes", "Description", c => c.String());
            AddColumn("dbo.Roles", "Description", c => c.String());
            AddColumn("dbo.TrainingCategories", "Description", c => c.String());
            AddColumn("dbo.LeaveTypes", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LeaveTypes", "Description");
            DropColumn("dbo.TrainingCategories", "Description");
            DropColumn("dbo.Roles", "Description");
            DropColumn("dbo.EmploymentTypes", "Description");
            DropColumn("dbo.EmploymentPositions", "Description");
            DropColumn("dbo.EmploymentCategories", "Description");
        }
    }
}

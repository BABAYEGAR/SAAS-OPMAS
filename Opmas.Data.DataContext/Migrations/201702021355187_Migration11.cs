namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeTrainings", "Title", c => c.String());
            AddColumn("dbo.EmployeeTrainings", "Location", c => c.String());
            AddColumn("dbo.EmployeeTrainings", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmployeeTrainings", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmployeeTrainings", "StartTime", c => c.String());
            AddColumn("dbo.EmployeeTrainings", "EndTime", c => c.String());
            AddColumn("dbo.EmployeeTrainings", "CoordinatorFullname", c => c.String());
            AddColumn("dbo.EmployeeTrainings", "CoordinatorCompany", c => c.String());
            DropColumn("dbo.EmployeeTrainings", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmployeeTrainings", "Name", c => c.String());
            DropColumn("dbo.EmployeeTrainings", "CoordinatorCompany");
            DropColumn("dbo.EmployeeTrainings", "CoordinatorFullname");
            DropColumn("dbo.EmployeeTrainings", "EndTime");
            DropColumn("dbo.EmployeeTrainings", "StartTime");
            DropColumn("dbo.EmployeeTrainings", "EndDate");
            DropColumn("dbo.EmployeeTrainings", "StartDate");
            DropColumn("dbo.EmployeeTrainings", "Location");
            DropColumn("dbo.EmployeeTrainings", "Title");
        }
    }
}

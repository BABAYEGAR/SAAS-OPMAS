namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate26 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeMedicalDatas", "EmploymentCategoryId", c => c.Long(nullable: false));
            DropColumn("dbo.EmployeeWorkDatas", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmployeeWorkDatas", "Category", c => c.String());
            DropColumn("dbo.EmployeeMedicalDatas", "EmploymentCategoryId");
        }
    }
}

namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate41 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.EmployeeMedicalDatas", "EmploymentCategory");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmployeeMedicalDatas", "EmploymentCategory", c => c.String(nullable: false));
        }
    }
}

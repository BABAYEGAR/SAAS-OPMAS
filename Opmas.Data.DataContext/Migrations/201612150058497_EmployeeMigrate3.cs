namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeMigrate3 : DbMigration
    {
        public override void Up()
        {

            AddColumn("dbo.Employees", "InstitutionId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
        }
    }
}

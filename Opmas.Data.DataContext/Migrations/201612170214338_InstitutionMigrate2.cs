namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InstitutionMigrate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeWorkDatas", "Category", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
        }
    }
}

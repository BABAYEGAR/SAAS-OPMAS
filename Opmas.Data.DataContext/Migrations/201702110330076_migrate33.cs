namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate33 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmploymentPositions", "Income", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmploymentPositions", "Income");
        }
    }
}

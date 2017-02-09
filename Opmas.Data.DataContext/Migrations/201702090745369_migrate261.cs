namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate261 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeTrainingMappings", "CompletionStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmployeeTrainingMappings", "CompletionStatus");
        }
    }
}

namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate46 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmploymentPositions", "SeniorMember", c => c.Boolean(nullable: false));
            AddColumn("dbo.LeaveTypes", "DurationIn", c => c.String());
            AddColumn("dbo.LeaveTypes", "Duration", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LeaveTypes", "Duration");
            DropColumn("dbo.LeaveTypes", "DurationIn");
            DropColumn("dbo.EmploymentPositions", "SeniorMember");
        }
    }
}

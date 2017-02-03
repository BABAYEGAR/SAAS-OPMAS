namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Roles", "ManageTraining", c => c.Boolean(nullable: false));
            AddColumn("dbo.Roles", "ManageTrainingTypes", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Roles", "ManageTrainingTypes");
            DropColumn("dbo.Roles", "ManageTraining");
        }
    }
}

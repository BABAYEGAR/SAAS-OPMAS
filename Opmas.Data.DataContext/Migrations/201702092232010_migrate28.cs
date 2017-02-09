namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate28 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Roles", "ManageEmploymentPositions");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Roles", "ManageEmploymentPositions", c => c.Boolean(nullable: false));
        }
    }
}

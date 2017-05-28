namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate68 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Roles", "ManageQualification", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Roles", "ManageQualification");
        }
    }
}

namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate66 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Departments", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Faculties", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Units", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Units", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Units", "Description", c => c.String());
            AlterColumn("dbo.Units", "Name", c => c.String());
            AlterColumn("dbo.Faculties", "Name", c => c.String());
            AlterColumn("dbo.Departments", "Name", c => c.String());
        }
    }
}

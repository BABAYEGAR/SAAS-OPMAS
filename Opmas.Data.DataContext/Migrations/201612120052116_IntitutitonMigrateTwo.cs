namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntitutitonMigrateTwo : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Institutions", "InstitutionCategory");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Institutions", "InstitutionCategory", c => c.String());
        }
    }
}

namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InstitutionMigrate3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Institutions", "AccessCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Institutions", "AccessCode");
        }
    }
}

namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate69 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InstitutionQualifications", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InstitutionQualifications", "Name", c => c.String());
        }
    }
}

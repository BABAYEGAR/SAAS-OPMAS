namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Institutions", "RegistrationNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Institutions", "RegistrationNumber");
        }
    }
}

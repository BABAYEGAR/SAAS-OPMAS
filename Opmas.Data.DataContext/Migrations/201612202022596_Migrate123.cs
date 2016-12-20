namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrate123 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeePersonalDatas", "Title", c => c.String(nullable: true));
        }
        
        public override void Down()
        {
        }
    }
}

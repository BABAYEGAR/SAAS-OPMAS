namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EmployeeBankDatas", "AccountNumber", c => c.String(nullable: false, maxLength: 11));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmployeeBankDatas", "AccountNumber", c => c.String(nullable: false));
        }
    }
}

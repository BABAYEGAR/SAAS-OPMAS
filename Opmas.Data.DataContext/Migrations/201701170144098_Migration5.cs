namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeBankDatas", "AccountFirstName", c => c.String(nullable: false));
            AddColumn("dbo.EmployeeBankDatas", "AccountMiddleName", c => c.String());
            AddColumn("dbo.EmployeeBankDatas", "AccountLastName", c => c.String(nullable: false));
            DropColumn("dbo.EmployeeBankDatas", "AccountName");
            DropColumn("dbo.EmployeeFamilyDatas", "NextOfKin");
            DropColumn("dbo.EmployeeFamilyDatas", "FakeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmployeeFamilyDatas", "FakeId", c => c.Long(nullable: false));
            AddColumn("dbo.EmployeeFamilyDatas", "NextOfKin", c => c.Boolean(nullable: false));
            AddColumn("dbo.EmployeeBankDatas", "AccountName", c => c.String(nullable: false));
            DropColumn("dbo.EmployeeBankDatas", "AccountLastName");
            DropColumn("dbo.EmployeeBankDatas", "AccountMiddleName");
            DropColumn("dbo.EmployeeBankDatas", "AccountFirstName");
        }
    }
}

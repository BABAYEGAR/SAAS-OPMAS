namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigrate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EmployeePersonalDatas", "Firstname", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePersonalDatas", "Lastname", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePersonalDatas", "PlaceOfBirth", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePersonalDatas", "PrimaryAddress", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePersonalDatas", "Gender", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePersonalDatas", "PostalCode", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePersonalDatas", "HomePhone", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePersonalDatas", "WorkPhone", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePersonalDatas", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePersonalDatas", "MaritalStatus", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmployeePersonalDatas", "MaritalStatus", c => c.String());
            AlterColumn("dbo.EmployeePersonalDatas", "Email", c => c.String());
            AlterColumn("dbo.EmployeePersonalDatas", "WorkPhone", c => c.String());
            AlterColumn("dbo.EmployeePersonalDatas", "HomePhone", c => c.String());
            AlterColumn("dbo.EmployeePersonalDatas", "PostalCode", c => c.String());
            AlterColumn("dbo.EmployeePersonalDatas", "Gender", c => c.String());
            AlterColumn("dbo.EmployeePersonalDatas", "PrimaryAddress", c => c.String());
            AlterColumn("dbo.EmployeePersonalDatas", "PlaceOfBirth", c => c.String());
            AlterColumn("dbo.EmployeePersonalDatas", "Lastname", c => c.String());
            AlterColumn("dbo.EmployeePersonalDatas", "Firstname", c => c.String());
        }
    }
}

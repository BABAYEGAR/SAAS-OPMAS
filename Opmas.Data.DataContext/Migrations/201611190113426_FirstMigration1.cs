namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EmployeeBankDatas", "BankName", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeeBankDatas", "AccountName", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeeBankDatas", "AccountNumber", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeeBankDatas", "AccountType", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeeMedicalDatas", "Genotype", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeeMedicalDatas", "BloodGroup", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePastWorkExperiences", "EmployerName", c => c.String());
            AlterColumn("dbo.EmployeePastWorkExperiences", "EmployerLocation", c => c.String());
            AlterColumn("dbo.EmployeePastWorkExperiences", "EmployerContact", c => c.String());
            AlterColumn("dbo.EmployeePastWorkExperiences", "ReasonForLeaving", c => c.String());
            AlterColumn("dbo.EmployeePastWorkExperiences", "PositionHeld", c => c.String());
            AlterColumn("dbo.EmployeePersonalDatas", "Firstname", c => c.String());
            AlterColumn("dbo.EmployeePersonalDatas", "Lastname", c => c.String());
            AlterColumn("dbo.EmployeePersonalDatas", "PlaceOfBirth", c => c.String());
            AlterColumn("dbo.EmployeePersonalDatas", "PrimaryAddress", c => c.String());
            AlterColumn("dbo.EmployeePersonalDatas", "SecondaryAddress", c => c.String());
            AlterColumn("dbo.EmployeePersonalDatas", "Gender", c => c.String());
            AlterColumn("dbo.EmployeePersonalDatas", "PostalCode", c => c.String());
            AlterColumn("dbo.EmployeePersonalDatas", "HomePhone", c => c.String());
            AlterColumn("dbo.EmployeePersonalDatas", "WorkPhone", c => c.String());
            AlterColumn("dbo.EmployeePersonalDatas", "Email", c => c.String());
            AlterColumn("dbo.EmployeePersonalDatas", "MaritalStatus", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmployeePersonalDatas", "MaritalStatus", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePersonalDatas", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePersonalDatas", "WorkPhone", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePersonalDatas", "HomePhone", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePersonalDatas", "PostalCode", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePersonalDatas", "Gender", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePersonalDatas", "SecondaryAddress", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePersonalDatas", "PrimaryAddress", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePersonalDatas", "PlaceOfBirth", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePersonalDatas", "Lastname", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePersonalDatas", "Firstname", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePastWorkExperiences", "PositionHeld", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePastWorkExperiences", "ReasonForLeaving", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePastWorkExperiences", "EmployerContact", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePastWorkExperiences", "EmployerLocation", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePastWorkExperiences", "EmployerName", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeeMedicalDatas", "BloodGroup", c => c.String());
            AlterColumn("dbo.EmployeeMedicalDatas", "Genotype", c => c.String());
            AlterColumn("dbo.EmployeeBankDatas", "AccountType", c => c.String());
            AlterColumn("dbo.EmployeeBankDatas", "AccountNumber", c => c.String());
            AlterColumn("dbo.EmployeeBankDatas", "AccountName", c => c.String());
            AlterColumn("dbo.EmployeeBankDatas", "BankName", c => c.String());
        }
    }
}

namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate31 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EmployeeEducationalQualifications", "InstitutionName", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeeEducationalQualifications", "Location", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeeEducationalQualifications", "DegreeAttained", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeeEducationalQualifications", "ClassOfDegree", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePastWorkExperiences", "EmployerName", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePastWorkExperiences", "EmployerLocation", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePastWorkExperiences", "EmployerContact", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePastWorkExperiences", "ReasonForLeaving", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeePastWorkExperiences", "PositionHeld", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmployeePastWorkExperiences", "PositionHeld", c => c.String());
            AlterColumn("dbo.EmployeePastWorkExperiences", "ReasonForLeaving", c => c.String());
            AlterColumn("dbo.EmployeePastWorkExperiences", "EmployerContact", c => c.String());
            AlterColumn("dbo.EmployeePastWorkExperiences", "EmployerLocation", c => c.String());
            AlterColumn("dbo.EmployeePastWorkExperiences", "EmployerName", c => c.String());
            AlterColumn("dbo.EmployeeEducationalQualifications", "ClassOfDegree", c => c.String());
            AlterColumn("dbo.EmployeeEducationalQualifications", "DegreeAttained", c => c.String());
            AlterColumn("dbo.EmployeeEducationalQualifications", "Location", c => c.String());
            AlterColumn("dbo.EmployeeEducationalQualifications", "InstitutionName", c => c.String());
        }
    }
}

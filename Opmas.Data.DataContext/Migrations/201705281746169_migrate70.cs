namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate70 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeEducationalQualifications", "InstitutionQualificationId", c => c.Long());
            CreateIndex("dbo.EmployeeEducationalQualifications", "InstitutionQualificationId");
            AddForeignKey("dbo.EmployeeEducationalQualifications", "InstitutionQualificationId", "dbo.InstitutionQualifications", "InstitutionQualificationId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeEducationalQualifications", "InstitutionQualificationId", "dbo.InstitutionQualifications");
            DropIndex("dbo.EmployeeEducationalQualifications", new[] { "InstitutionQualificationId" });
            DropColumn("dbo.EmployeeEducationalQualifications", "InstitutionQualificationId");
        }
    }
}

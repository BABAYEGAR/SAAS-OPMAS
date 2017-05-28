namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate71 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EmployeeEducationalQualifications", "DegreeAttained", c => c.String());
            AlterColumn("dbo.EmployeeEducationalQualifications", "ClassOfDegree", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmployeeEducationalQualifications", "ClassOfDegree", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeeEducationalQualifications", "DegreeAttained", c => c.String(nullable: false));
        }
    }
}

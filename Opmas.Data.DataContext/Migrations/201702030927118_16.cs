namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeTrainings", "InstitutionId", c => c.Long(nullable: false));
            CreateIndex("dbo.EmployeeTrainings", "InstitutionId");
            AddForeignKey("dbo.EmployeeTrainings", "InstitutionId", "dbo.Institutions", "InstitutionId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeTrainings", "InstitutionId", "dbo.Institutions");
            DropIndex("dbo.EmployeeTrainings", new[] { "InstitutionId" });
            DropColumn("dbo.EmployeeTrainings", "InstitutionId");
        }
    }
}

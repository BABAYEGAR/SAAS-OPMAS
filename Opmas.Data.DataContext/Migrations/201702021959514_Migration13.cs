namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeTrainingMappings", "InstitutionId", c => c.Long(nullable: false));
            CreateIndex("dbo.EmployeeTrainingMappings", "InstitutionId");
            AddForeignKey("dbo.EmployeeTrainingMappings", "InstitutionId", "dbo.Institutions", "InstitutionId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeTrainingMappings", "InstitutionId", "dbo.Institutions");
            DropIndex("dbo.EmployeeTrainingMappings", new[] { "InstitutionId" });
            DropColumn("dbo.EmployeeTrainingMappings", "InstitutionId");
        }
    }
}

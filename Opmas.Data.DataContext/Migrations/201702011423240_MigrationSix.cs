namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationSix : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.EmployeeWorkDatas", new[] { "EmploymentTypeId" });
            AlterColumn("dbo.EmployeeWorkDatas", "EmploymentTypeId", c => c.Long());
            CreateIndex("dbo.EmployeeWorkDatas", "EmploymentTypeId");
            AddForeignKey("dbo.EmployeeWorkDatas", "EmploymentTypeId", "dbo.EmploymentTypes", "EmploymentTypeId");
        }
        
        public override void Down()
        {
        }
    }
}

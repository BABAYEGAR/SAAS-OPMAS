namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationFive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeWorkDatas", "EmploymentTypeId", c => c.Long(nullable: false));
            //CreateIndex("dbo.EmployeeWorkDatas", "EmploymentTypeId");
            //AddForeignKey("dbo.EmployeeWorkDatas", "EmploymentTypeId", "dbo.EmploymentTypes", "EmploymentTypeId", cascadeDelete: false);
        }
        
        public override void Down()
        {
        }
    }
}

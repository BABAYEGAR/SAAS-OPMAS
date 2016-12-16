namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InstitutionMigratess : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Institutions", "PackagePeriodId", c => c.Long(nullable: false));
            CreateIndex("dbo.Institutions", "PackagePeriodId");
            AddForeignKey("dbo.Institutions", "PackagePeriodId", "dbo.PackagePeriods", "PackagePeriodId", cascadeDelete: true);
        }
        
        public override void Down()
        {
        }
    }
}

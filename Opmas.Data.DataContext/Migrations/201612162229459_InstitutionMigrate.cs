namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InstitutionMigrate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PackagePeriods",
                c => new
                    {
                        PackagePeriodId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.PackagePeriodId);
            
            AddColumn("dbo.Institutions", "PackagePeriodId", c => c.Long(nullable: false));
            CreateIndex("dbo.Institutions", "PackagePeriodId");
        }
        
        public override void Down()
        {
        }
    }
}

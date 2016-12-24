namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigrate10 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Institutions", "PackagePeriodId", "dbo.PackagePeriods");
            DropIndex("dbo.Institutions", new[] { "PackagePeriodId" });
            AddColumn("dbo.Packages", "Type", c => c.String());
            AddColumn("dbo.Packages", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Packages", "EndDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Institutions", "PackagePeriodId");
            DropTable("dbo.PackagePeriods");
        }
        
        public override void Down()
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
            DropColumn("dbo.Packages", "EndDate");
            DropColumn("dbo.Packages", "StartDate");
            DropColumn("dbo.Packages", "Type");
            CreateIndex("dbo.Institutions", "PackagePeriodId");
            AddForeignKey("dbo.Institutions", "PackagePeriodId", "dbo.PackagePeriods", "PackagePeriodId", cascadeDelete: true);
        }
    }
}

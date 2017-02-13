namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate38 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LeaveTypes",
                c => new
                    {
                        LeaveTypeId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        InstitutionId = c.Long(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.LeaveTypeId)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: true)
                .Index(t => t.InstitutionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LeaveTypes", "InstitutionId", "dbo.Institutions");
            DropIndex("dbo.LeaveTypes", new[] { "InstitutionId" });
            DropTable("dbo.LeaveTypes");
        }
    }
}

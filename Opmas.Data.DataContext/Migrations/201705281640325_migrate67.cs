namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate67 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InstitutionQualifications",
                c => new
                    {
                        InstitutionQualificationId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        InstitutionId = c.Long(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.InstitutionQualificationId)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: true)
                .Index(t => t.InstitutionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InstitutionQualifications", "InstitutionId", "dbo.Institutions");
            DropIndex("dbo.InstitutionQualifications", new[] { "InstitutionId" });
            DropTable("dbo.InstitutionQualifications");
        }
    }
}

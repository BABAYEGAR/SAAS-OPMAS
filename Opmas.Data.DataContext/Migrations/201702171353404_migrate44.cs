namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate44 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InstitutionStructures", "CreatedBy", c => c.Long(nullable: false));
            AddColumn("dbo.InstitutionStructures", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.InstitutionStructures", "DateLastModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.InstitutionStructures", "LastModifiedBy", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InstitutionStructures", "LastModifiedBy");
            DropColumn("dbo.InstitutionStructures", "DateLastModified");
            DropColumn("dbo.InstitutionStructures", "DateCreated");
            DropColumn("dbo.InstitutionStructures", "CreatedBy");
        }
    }
}

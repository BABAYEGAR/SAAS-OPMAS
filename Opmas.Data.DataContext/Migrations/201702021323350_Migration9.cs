namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmploymentTypes", "CreatedBy", c => c.Long(nullable: false));
            AddColumn("dbo.EmploymentTypes", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmploymentTypes", "DateLastModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmploymentTypes", "LastModifiedBy", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmploymentTypes", "LastModifiedBy");
            DropColumn("dbo.EmploymentTypes", "DateLastModified");
            DropColumn("dbo.EmploymentTypes", "DateCreated");
            DropColumn("dbo.EmploymentTypes", "CreatedBy");
        }
    }
}

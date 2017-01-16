namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMig1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeEducationalQualifications", "FileUpload", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmployeeEducationalQualifications", "FileUpload");
        }
    }
}

namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigrate : DbMigration
    {
        public override void Up()
        {

            CreateTable(
                "dbo.AppUsers",
                c => new
                {
                    AppUserId = c.Long(nullable: false, identity: true),
                    Firstname = c.String(nullable: false, maxLength: 100),
                    Middlename = c.String(maxLength: 100),
                    Lastname = c.String(nullable: false, maxLength: 100),
                    Email = c.String(nullable: false, maxLength: 100),
                    Mobile = c.String(nullable: false, maxLength: 100),
                    Password = c.String(),
                    Role = c.String(),
                    DateCreated = c.DateTime(nullable: false),
                    DateLastModified = c.DateTime(nullable: false),
                    CreatedById = c.Long(nullable: false),
                    LastModifiedById = c.Long(nullable: false),
                    AppUserImage = c.String(),
                    FingerPrint = c.String(),
                    RememberMe = c.Boolean(nullable: false),
                    EmployeeId = c.Long(nullable: false),
                })
                .PrimaryKey(t => t.AppUserId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
        }
        
        public override void Down()
        {
        }
    }
}

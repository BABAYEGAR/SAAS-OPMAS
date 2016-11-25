namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seventhigrate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Banks",
                c => new
                    {
                        BankId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.BankId);
            
            AddForeignKey("dbo.EmployeeBankDatas", "BankId", "dbo.Banks", "BankId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmployeeBankDatas", "BankName", c => c.String(nullable: false));
            DropForeignKey("dbo.EmployeeBankDatas", "BankId", "dbo.Banks");
            DropIndex("dbo.EmployeeBankDatas", new[] { "BankId" });
            DropColumn("dbo.EmployeeBankDatas", "BankId");
            DropTable("dbo.Banks");
        }
    }
}

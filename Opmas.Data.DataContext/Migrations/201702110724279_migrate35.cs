namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate35 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentDeductionRequests", "InstitutionId", c => c.Long(nullable: false));
            CreateIndex("dbo.PaymentDeductionRequests", "InstitutionId");
            AddForeignKey("dbo.PaymentDeductionRequests", "InstitutionId", "dbo.Institutions", "InstitutionId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentDeductionRequests", "InstitutionId", "dbo.Institutions");
            DropIndex("dbo.PaymentDeductionRequests", new[] { "InstitutionId" });
            DropColumn("dbo.PaymentDeductionRequests", "InstitutionId");
        }
    }
}

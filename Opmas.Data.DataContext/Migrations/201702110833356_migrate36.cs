namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate36 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentDeductionRequests", "Amount", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentDeductionRequests", "Amount");
        }
    }
}

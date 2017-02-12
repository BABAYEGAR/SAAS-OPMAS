namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate37 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentAllowances", "Rate", c => c.Long(nullable: false));
            DropColumn("dbo.PaymentAllowances", "Amount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PaymentAllowances", "Amount", c => c.Long(nullable: false));
            DropColumn("dbo.PaymentAllowances", "Rate");
        }
    }
}

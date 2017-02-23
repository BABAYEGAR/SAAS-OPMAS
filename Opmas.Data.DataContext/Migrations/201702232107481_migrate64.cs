namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate64 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PositionChanges", "PreviousPositionId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PositionChanges", "PreviousPositionId");
        }
    }
}

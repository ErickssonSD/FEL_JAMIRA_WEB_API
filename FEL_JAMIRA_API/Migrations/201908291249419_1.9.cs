namespace FEL_JAMIRA_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _19 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.COMPRA_CREDITOS", "DataTransacao", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.COMPRA_CREDITOS", "DataTransacao");
        }
    }
}

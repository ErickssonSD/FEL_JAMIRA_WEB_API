namespace FEL_JAMIRA_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcriacao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PESSOA", "DataCriacao", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PESSOA", "DataCriacao");
        }
    }
}

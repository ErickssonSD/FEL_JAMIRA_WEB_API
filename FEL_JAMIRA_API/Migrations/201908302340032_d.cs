namespace FEL_JAMIRA_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CLIENTE", "TemCarro", c => c.Boolean(nullable: false));
            AddColumn("dbo.ESTACIONAMENTO", "TemEstacionamento", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ESTACIONAMENTO", "TemEstacionamento");
            DropColumn("dbo.CLIENTE", "TemCarro");
        }
    }
}

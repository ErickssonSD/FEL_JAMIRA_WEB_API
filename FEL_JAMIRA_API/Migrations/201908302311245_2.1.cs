namespace FEL_JAMIRA_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CLIENTE", "IdCarro", "dbo.CARROS");
            DropIndex("dbo.CLIENTE", new[] { "IdCarro" });
            AddColumn("dbo.CARROS", "IdCliente", c => c.Int(nullable: false));
            CreateIndex("dbo.CARROS", "IdCliente");
            AddForeignKey("dbo.CARROS", "IdCliente", "dbo.CLIENTE", "Id");
            DropColumn("dbo.CLIENTE", "IdCarro");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CLIENTE", "IdCarro", c => c.Int());
            DropForeignKey("dbo.CARROS", "IdCliente", "dbo.CLIENTE");
            DropIndex("dbo.CARROS", new[] { "IdCliente" });
            DropColumn("dbo.CARROS", "IdCliente");
            CreateIndex("dbo.CLIENTE", "IdCarro");
            AddForeignKey("dbo.CLIENTE", "IdCarro", "dbo.CARROS", "Id");
        }
    }
}

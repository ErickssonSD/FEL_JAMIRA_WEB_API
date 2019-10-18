namespace FEL_JAMIRA_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _17 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CLIENTE", "IdCarro", "dbo.CARROS");
            DropIndex("dbo.CLIENTE", new[] { "IdCarro" });
            AlterColumn("dbo.CLIENTE", "IdCarro", c => c.Int());
            CreateIndex("dbo.CLIENTE", "IdCarro");
            AddForeignKey("dbo.CLIENTE", "IdCarro", "dbo.CARROS", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CLIENTE", "IdCarro", "dbo.CARROS");
            DropIndex("dbo.CLIENTE", new[] { "IdCarro" });
            AlterColumn("dbo.CLIENTE", "IdCarro", c => c.Int(nullable: false));
            CreateIndex("dbo.CLIENTE", "IdCarro");
            AddForeignKey("dbo.CLIENTE", "IdCarro", "dbo.CARROS", "Id", cascadeDelete: true);
        }
    }
}

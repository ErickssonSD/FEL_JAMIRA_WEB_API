namespace FEL_JAMIRA_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gg4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SOLICITACAO", "IdEstacionamento", "dbo.ESTACIONAMENTO");
            DropPrimaryKey("dbo.ESTACIONAMENTO");
            AddColumn("dbo.ESTACIONAMENTO", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ESTACIONAMENTO", "Id");
            AddForeignKey("dbo.SOLICITACAO", "IdEstacionamento", "dbo.ESTACIONAMENTO", "Id");
            DropColumn("dbo.ESTACIONAMENTO", "IdEstacionamento");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ESTACIONAMENTO", "IdEstacionamento", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.SOLICITACAO", "IdEstacionamento", "dbo.ESTACIONAMENTO");
            DropPrimaryKey("dbo.ESTACIONAMENTO");
            DropColumn("dbo.ESTACIONAMENTO", "Id");
            AddPrimaryKey("dbo.ESTACIONAMENTO", "IdEstacionamento");
            AddForeignKey("dbo.SOLICITACAO", "IdEstacionamento", "dbo.ESTACIONAMENTO", "IdEstacionamento");
        }
    }
}

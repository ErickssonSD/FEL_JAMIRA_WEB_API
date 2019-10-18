namespace FEL_JAMIRA_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _13 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ESTACIONAMENTO", "IdEnderecoEstabelecimento", "dbo.ENDERECO");
            DropIndex("dbo.ESTACIONAMENTO", new[] { "IdEnderecoEstabelecimento" });
            AlterColumn("dbo.ESTACIONAMENTO", "IdEnderecoEstabelecimento", c => c.Int());
            CreateIndex("dbo.ESTACIONAMENTO", "IdEnderecoEstabelecimento");
            AddForeignKey("dbo.ESTACIONAMENTO", "IdEnderecoEstabelecimento", "dbo.ENDERECO", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ESTACIONAMENTO", "IdEnderecoEstabelecimento", "dbo.ENDERECO");
            DropIndex("dbo.ESTACIONAMENTO", new[] { "IdEnderecoEstabelecimento" });
            AlterColumn("dbo.ESTACIONAMENTO", "IdEnderecoEstabelecimento", c => c.Int(nullable: false));
            CreateIndex("dbo.ESTACIONAMENTO", "IdEnderecoEstabelecimento");
            AddForeignKey("dbo.ESTACIONAMENTO", "IdEnderecoEstabelecimento", "dbo.ENDERECO", "Id", cascadeDelete: true);
        }
    }
}

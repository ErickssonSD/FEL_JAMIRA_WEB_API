namespace FEL_JAMIRA_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _40 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.ESTACIONAMENTO", "IdEnderecoPessoa");
            AddForeignKey("dbo.ESTACIONAMENTO", "IdEnderecoPessoa", "dbo.ENDERECO", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ESTACIONAMENTO", "IdEnderecoPessoa", "dbo.ENDERECO");
            DropIndex("dbo.ESTACIONAMENTO", new[] { "IdEnderecoPessoa" });
        }
    }
}

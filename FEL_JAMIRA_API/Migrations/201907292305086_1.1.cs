namespace FEL_JAMIRA_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ESTACIONAMENTO", name: "IdEnderecoPessoa", newName: "IdEnderecoEstabelecimento");
            RenameIndex(table: "dbo.ESTACIONAMENTO", name: "IX_IdEnderecoPessoa", newName: "IX_IdEnderecoEstabelecimento");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ESTACIONAMENTO", name: "IX_IdEnderecoEstabelecimento", newName: "IX_IdEnderecoPessoa");
            RenameColumn(table: "dbo.ESTACIONAMENTO", name: "IdEnderecoEstabelecimento", newName: "IdEnderecoPessoa");
        }
    }
}

namespace FEL_JAMIRA_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _30 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ESTACIONAMENTO", "IdEstacionamento", c => c.Int(nullable: false));
            AddColumn("dbo.ESTACIONAMENTO", "IdEnderecoPessoa", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ESTACIONAMENTO", "IdEnderecoPessoa");
            DropColumn("dbo.ESTACIONAMENTO", "IdEstacionamento");
        }
    }
}

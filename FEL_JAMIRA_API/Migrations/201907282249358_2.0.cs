namespace FEL_JAMIRA_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.USUARIO", "AuxSenha", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.USUARIO", "AuxSenha");
        }
    }
}

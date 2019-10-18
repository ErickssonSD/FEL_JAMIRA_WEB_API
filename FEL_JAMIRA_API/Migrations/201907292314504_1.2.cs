namespace FEL_JAMIRA_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PESSOA", "Nascimento", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PESSOA", "Nascimento", c => c.DateTime(nullable: false));
        }
    }
}

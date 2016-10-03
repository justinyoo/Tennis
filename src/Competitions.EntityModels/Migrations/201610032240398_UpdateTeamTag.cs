namespace Competitions.EntityModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTeamTag : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Team", "Tag", c => c.String(maxLength: 16));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Team", "Tag", c => c.String(nullable: false, maxLength: 16));
        }
    }
}

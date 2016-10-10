namespace Competitions.EntityModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateClubByAddingClubHousePhone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Club", "ClubHousePhone", c => c.String(maxLength: 16));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Club", "ClubHousePhone");
        }
    }
}

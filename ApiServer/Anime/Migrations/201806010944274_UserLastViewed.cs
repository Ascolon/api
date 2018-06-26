namespace Anime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserLastViewed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "LasViewed", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "LasViewed");
        }
    }
}

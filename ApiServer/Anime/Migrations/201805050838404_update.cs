namespace Anime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Films", "Imdb", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Films", "Imdb");
        }
    }
}

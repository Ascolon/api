namespace Anime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addradio2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Radios",
                c => new
                    {
                        RadioId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Url = c.String(),
                        Poster = c.String(),
                    })
                .PrimaryKey(t => t.RadioId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Radios");
        }
    }
}

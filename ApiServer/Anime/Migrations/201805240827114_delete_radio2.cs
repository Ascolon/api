namespace Anime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delete_radio2 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Radios");
        }
        
        public override void Down()
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
    }
}

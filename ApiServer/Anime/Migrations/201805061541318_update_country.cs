namespace Anime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_country : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountriesId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CountriesId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Countries");
        }
    }
}

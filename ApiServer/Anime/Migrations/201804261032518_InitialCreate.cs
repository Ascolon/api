namespace Anime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Time = c.String(),
                        Author = c.String(),
                        AuthorAvatar = c.String(),
                        Film = c.Int(nullable: false),
                        Film_FilmId = c.Int(),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Films", t => t.Film_FilmId)
                .Index(t => t.Film_FilmId);
            
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        FavoriteId = c.Int(nullable: false, identity: true),
                        User = c.Int(nullable: false),
                        Film_FilmId = c.Int(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.FavoriteId)
                .ForeignKey("dbo.Films", t => t.Film_FilmId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.Film_FilmId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Films",
                c => new
                    {
                        FilmId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Year = c.Int(nullable: false),
                        Genre = c.String(),
                        Description = c.String(),
                        Poster = c.String(),
                        Video = c.String(),
                        Pagebg = c.String(),
                    })
                .PrimaryKey(t => t.FilmId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.GenreId);
            
            CreateTable(
                "dbo.Stuffs",
                c => new
                    {
                        StuffId = c.Int(nullable: false, identity: true),
                        DefaulUserAvatar = c.String(),
                    })
                .PrimaryKey(t => t.StuffId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        Avatar = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Favorites", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Favorites", "Film_FilmId", "dbo.Films");
            DropForeignKey("dbo.Comments", "Film_FilmId", "dbo.Films");
            DropIndex("dbo.Favorites", new[] { "User_UserId" });
            DropIndex("dbo.Favorites", new[] { "Film_FilmId" });
            DropIndex("dbo.Comments", new[] { "Film_FilmId" });
            DropTable("dbo.Users");
            DropTable("dbo.Stuffs");
            DropTable("dbo.Genres");
            DropTable("dbo.Films");
            DropTable("dbo.Favorites");
            DropTable("dbo.Comments");
        }
    }
}

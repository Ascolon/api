using Anime.Models;
using System.Data.Entity;

namespace Anime.Context
{
    public class AskorbinkaContext : DbContext
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Countries> Country { get; set; }
        public DbSet<Stuff> Stuffs { get; set; }
    }
}
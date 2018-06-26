namespace Anime.Models
{
    public class Favorite
    {
        public int FavoriteId { get; set; }
        public int User { get; set; }
        public Film Film { get; set; }
    }
}
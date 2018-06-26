using System.Collections.Generic;

namespace Anime.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public string LasViewed { get; set; }

        public ICollection<Favorite> Favorites { get; set; }

        public User()
        {
            Favorites = new List<Favorite>();
        }
    }
}
using System.Collections.Generic;

namespace Anime.Models
{
    public class Film
    {
        public int FilmId { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Country { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string Poster { get; set; }
        public string Video { get; set; }
        public string Pagebg { get; set; }
        public string Imdb { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public Film()
        {
            Comments = new List<Comment>();
        }
    }
}
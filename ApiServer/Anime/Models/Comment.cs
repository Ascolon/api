using System.Web.Mvc;

namespace Anime.Models
{
    [ValidateInput(false)]
    public class Comment
    {
        public int CommentId { get; set; }
        [AllowHtml]
        public string Text { get; set; }
        public string Time { get; set; }
        public string Author { get; set; }
        public string AuthorAvatar { get; set; }
        public int Film { get; set; }
        public bool IsEditable { get; set; } = false;
        public string EditingTime { get; set; }
    }
}
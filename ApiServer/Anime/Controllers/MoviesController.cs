using Askorbinka.AttributeCross;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System;
using Anime.Context;
using Anime.Models; 

namespace Askorbinka.Controllers
{
    [CrossOriginAccess]
    public class MoviesController : Controller
    {
        AskorbinkaContext context = new AskorbinkaContext();

        public JsonResult GetAllGenre()
        {
            var genres = context
                .Films
                .Select(g => g.Genre)
                .Distinct()
                .ToList();
            return Json(genres, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllCountry()
        {
            var country = context.Films.Select(f => f.Country).Distinct();
            return Json(country, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMoviesByCountry(string c)
        {
            var films = context.Films.Where(f => f.Country == c);
            return Json(films, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMoviesByGenre(string g)
        {
            var Films = context.Films.Where(f => f.Genre == g).OrderByDescending(f => f.FilmId).Take(60).ToList(); 
            return Json(Films, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMovieById(int f_id, int u_id)
        {
            bool IsFavorite = false;
            var Film = context.Films.FirstOrDefault(f => f.FilmId == f_id);
            if (u_id > 0)
            {
                IsFavorite = IsFavorited(u_id, f_id);
            }
            Film.Comments = context
                .Comments
                .Where(c => c.Film == f_id)
                .AsEnumerable()
                .OrderByDescending(c => c.CommentId)
                .Take(20)
                .ToList();
            return Json(new
            {
                Film.FilmId,
                Film.Name,
                Film.Year,
                Film.Genre,
                Film.Country,
                Film.Video,
                Film.Poster,
                Film.Description,
                Film.Comments,
                Film.Pagebg,
                IsFavorite,
                InFavorites = InFavoritesByUsers(Film.FilmId),
                CommentCount = AllComments(Film.FilmId)

            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLastAdded()
        {
            var Films = context
                .Films
                .AsEnumerable()
                .OrderByDescending(g => g.FilmId)
                .Take(60);
            return Json(Films, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFilmsByName(string n)
        {
            var films = context
                .Films
                .Where(f => f.Name.Contains(n))
                .ToList();
            return Json(films, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UploadContentByGenre(int s, string g)
        {
            var Films = context
                .Films
                .Where(v => v.Genre == g)
                .OrderByDescending(v => v.FilmId)
                .Skip(s)
                .Take(50)
                .ToList();
            return Json(Films, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UploadContent(int s)
        {
            var Films = context
                .Films
                .OrderByDescending(v => v.FilmId)
                .Skip(s)
                .Take(50)
                .ToList();
            return Json(Films, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public JsonResult AddComment(int f, string t, int u)
        {
            

            var film = context.Films.FirstOrDefault(g => g.FilmId == f);
            var user = context.Users.AsEnumerable().FirstOrDefault(g => g.UserId == u);

            if (user != null && film != null)
            {
                string if_added = "<br/>";
                var cmt = new Comment()
                {
                    Time = DateTime.UtcNow.ToString(),
                    Text = t,
                    Film = film.FilmId,
                    Author = user.Login,
                    AuthorAvatar = user.Avatar
                };

                var last_cmt = context
                    .Comments
                    .AsEnumerable()
                    .LastOrDefault(c => c.Film == film.FilmId);
                if (last_cmt != null && last_cmt.Author == user.Login)
                {
                    last_cmt.Text = last_cmt.Text + if_added + t;
                    context.SaveChanges();
                    return Json(new { comment = last_cmt, updated = true }, JsonRequestBehavior.AllowGet);
                }

                film.Comments.Add(cmt);
                context.SaveChanges();


                return Json(new { comment = cmt, updated = false }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { error = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UploadComments(int s, int f_id)
        {
            var data = context.Comments.Where(c => c.Film == f_id).OrderByDescending(c => c.CommentId).Skip(s).Take(20).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public JsonResult EditComment(int c_id, string text)
        {
            var cmt = context.Comments.FirstOrDefault(c => c.CommentId == c_id);
            if(cmt != null)
            {
                cmt.Text = text;
                cmt.IsEditable = true;
                cmt.EditingTime = DateTime.UtcNow.ToString();
                context.SaveChanges();
                return Json(cmt, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddToFavorite(int f_id, int u_id)
        {
            var user = context.Users.Include(u => u.Favorites).FirstOrDefault(u => u.UserId == u_id);
            var film = context.Films.FirstOrDefault(f => f.FilmId == f_id);
            
            if (user == null || film == null)
            {
                return Json(new
                {
                    result = "error",
                    show = false,
                    InFavorites = InFavoritesByUsers(film.FilmId)
                }, JsonRequestBehavior.AllowGet);
            }
            if (user.Favorites.Count(c => c.Film == film) > 0)
            {
                context.Favorites.Remove(user.Favorites.FirstOrDefault(f => f.Film == film));
                context.SaveChanges();
                return Json(new
                {
                    result = "delete",
                    show = false,
                    InFavorites = InFavoritesByUsers(film.FilmId)
                }, JsonRequestBehavior.AllowGet);                
            }
            user.Favorites.Add(new Favorite()
            {
                Film = film,
                User = u_id
            });
            context.SaveChanges();
            return Json(new
            {
                result = "add",
                show = true,
                InFavorites = InFavoritesByUsers(film.FilmId)
            }, JsonRequestBehavior.AllowGet);
        }




        private bool IsFavorited(int u_id, int f_id)
        {

            var likedMovies = context.Favorites
                .Where(g => g.User == u_id)
                .Include(g => g.Film)
                .Select(g => g.Film)
                .ToList();

            var f = likedMovies.FirstOrDefault(g => g.FilmId == f_id);

            return likedMovies.Contains(f);

        }
        private int InFavoritesByUsers(int f_id)
        {
            var length = context.Favorites.Where(f => f.Film.FilmId == f_id).Count();
            return length;
        }
        private int AllComments(int f_id)
        {
            var all = context.Comments.Where(c => c.Film == f_id).Count();
            return all; 
        }
    }
}
using Anime.Context;
using Anime.Models;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Anime.Controllers
{
    public class ManageController : Controller
    {
        AskorbinkaContext context = new AskorbinkaContext();
        public ActionResult Index()
        {
            ViewBag.Genre = context.Genres.Select(g => g.Name).Distinct();
            ViewBag.Country = context.Country.Select(g => g.Name).Distinct();
            return View();
        }

        public ActionResult EditMovie(int id)
        {
            ViewBag.Film = context.Films.FirstOrDefault(g => g.FilmId == id);
            ViewBag.Genre = context.Genres.Select(g => g.Name).Distinct().ToList();
            ViewBag.Country = context.Country.Select(g => g.Name).Distinct();
            return View();
        }

        public object DeleteMovie(int id)
        {
            var pth = "h:/root/home/ascolon228-001/www/joycasino/files/";
            var f = context.Films.FirstOrDefault(g => g.FilmId == id);
            if (f == null)
                return false;

            var cmt = context.Comments.Where(c => c.Film == f.FilmId).ToList();
            if (cmt.Count > 0)
            {
                context.Comments.RemoveRange(cmt);
                context.SaveChanges();
            } 

            var fvr = context.Favorites.Where(fr => fr.Film.FilmId == id);
            if (fvr.Count() > 0)
            {
                context.Favorites.RemoveRange(fvr);
                context.SaveChanges();
            }
                

            context.Films.Remove(f);
            context.SaveChanges();
            
            var pst = Server.MapPath("~/files/image/" + f.Poster.Substring(f.Poster.LastIndexOf("/") + 1));
            var vdo = Server.MapPath("~/files/video/" + f.Video.Substring(f.Video.LastIndexOf("/") + 1));
            var pgb = Server.MapPath("~/files/pagesbg/" + f.Pagebg.Substring(f.Pagebg.LastIndexOf("/") + 1));
            if (System.IO.File.Exists(pst))
                new FileInfo(pst).Delete();
            if (System.IO.File.Exists(vdo))
                new FileInfo(vdo).Delete();
            if (System.IO.File.Exists(pgb))
                new FileInfo(pgb).Delete();

            return true;
        }


        public ActionResult AddMovie(
            string Name, 
            int Year,
            string Genre, 
            string Country,
            string Imdb,
            string Description,
            HttpPostedFileBase Poster,
            HttpPostedFileBase Video,
            HttpPostedFileBase Pagebg)
        {;

            //
            var PosterPath = Server.MapPath("~/Files/Image/" + Poster.FileName);
            Poster.SaveAs(PosterPath);
            var VideoPath = Server.MapPath("~/Files/Video/" + Video.FileName);
            Video.SaveAs(VideoPath);
            var PageBgPath = Server.MapPath("~/Files/PagesBg/" + Pagebg.FileName);
            Pagebg.SaveAs(PageBgPath);
            //

            var newFilm = new Film()
            {
                Name = Name,
                Description = Description,
                Genre = Genre,
                Year = Year,
                Country = Country,
                Imdb = Imdb
            };
            newFilm.Poster = $"{ProjectAttribute.ProjectUrl}/Files/Image/{Poster.FileName}";
            newFilm.Video = $"{ProjectAttribute.ProjectUrl}/Files/Video/{Video.FileName}";
            newFilm.Pagebg = $"{ProjectAttribute.ProjectUrl}/Files/PagesBg/{Pagebg.FileName}";
            context.Films.Add(newFilm);
            context.SaveChanges();

            return View();
        }


        public ActionResult SaveChangeMovie(
            int FilmId,
            string Name,
            int Year,
            string Genre,
            string Country,
            string Imdb,
            string Description,
            HttpPostedFileBase Poster,
            HttpPostedFileBase Video,
            HttpPostedFileBase Pagebg)
        {

            var newFilm = context.Films.FirstOrDefault(f => f.FilmId == FilmId);
            if (newFilm != null)
            {
                if (Poster != null)
                {
                    var PosterPath = Server.MapPath("~/Files/Image/" + Poster.FileName);
                    Poster.SaveAs(PosterPath);
                    newFilm.Poster = $"{ProjectAttribute.ProjectUrl}/Files/Image/{Poster.FileName}";
                }
                if (Video != null)
                {
                    var VideoPath = Server.MapPath("~/Files/Video/" + Video.FileName);
                    Video.SaveAs(VideoPath);
                    newFilm.Video = $"{ProjectAttribute.ProjectUrl}/Files/Video/{Video.FileName}";
                }
                if (Pagebg != null)
                {
                    var PageBgPath = Server.MapPath("~/Files/PagesBg/" + Pagebg.FileName);
                    Pagebg.SaveAs(PageBgPath);
                    newFilm.Pagebg = $"{ProjectAttribute.ProjectUrl}/Files/PagesBg/{Pagebg.FileName}";
                }
                newFilm.Name = Name;
                newFilm.Description = Description;
                newFilm.Year = Year;
                newFilm.Genre = Genre;
                newFilm.Country = Country;
                newFilm.Imdb = Imdb;
                context.SaveChanges();
            }
            

            context.SaveChanges();

            return View("AddMovie");
        }
    }
}
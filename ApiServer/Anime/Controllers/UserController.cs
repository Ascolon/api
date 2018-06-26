using Anime.Context;
using Anime.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Askorbinka.Controllers
{
    public class UserController : Controller
    {
        AskorbinkaContext context = new AskorbinkaContext();

        class NoUser
        {
            public bool NotFound { get; set; } = true;
        }
        class UserBusy
        {
            public bool Busy { get; set; } = true;
        }

        public JsonResult Autorization(string l, string p)
        {
            var user = context.Users.FirstOrDefault(g => g.Login == l && g.Password == p);
            if (user == null)
                return Json(new NoUser(), JsonRequestBehavior.AllowGet);
            
            return Json(new
            {
                Name = user.Login,
                Id = user.UserId,
                Avatar = user.Avatar,
                LastViewed = user.LasViewed
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Registration(string l, string p)
        {
            if (context.Users.Count(g => g.Login == l) > 0)
                return Json(new UserBusy(), JsonRequestBehavior.AllowGet);
            var def_avatar = SetAvatar(l);
            var user = new User()
            {
                Login = l,
                Password = p,
                Avatar = def_avatar,
            };
            context.Users.Add(user);
            context.SaveChanges();
            return Json(new
            {
                Name = user.Login,
                Id = user.UserId,
                Avatar = user.Avatar,
                LastViewed = user.LasViewed
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Favorites(int id)
        {

            var likedMovies = context.Favorites
                .Where(g => g.User == id)
                .Include(g => g.Film)
                .Select(g => g.Film)
                .ToList();
            return Json(likedMovies, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ChangePassword(int u_id, string new_pass)
        {
            var c_user = context.Users.FirstOrDefault(g => g.UserId == u_id);
            if (c_user != null)
            {
                c_user.Password = new_pass;
                context.SaveChanges();
                return Json(new { result = new_pass, success = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ThisNameIsBusy(string login)
        {
            if (context.Users.Count(g => g.Login == login) > 0)
                return Json(true, JsonRequestBehavior.AllowGet);
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LastViewed(int id)
        {
            var user = context.Users.FirstOrDefault(u => u.UserId == id);
            return Json(user.LasViewed, JsonRequestBehavior.AllowGet);
        }




        private string SetAvatar(string name)
        {
            var literal = name.ToUpper().First();
            return $"{ProjectAttribute.ProjectUrl}Files/UserData/Avatars/{literal}.png";
        }
    }
}
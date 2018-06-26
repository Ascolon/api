using Anime.Context;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Anime.Controllers
{
    public class HomeController : Controller
    {
        int GetYEAR()
        {
            System.Random rbd = new System.Random();

            return rbd.Next(2000, 2018);
        }
        double GetIMDB()
        {
            System.Random rbd = new System.Random();

            return rbd.Next(5, 8) + rbd.NextDouble();
        }
        AskorbinkaContext c = new AskorbinkaContext();
        string path = "http://ascolon228-001-site1.ftempurl.com/Files/Image/";

        public ActionResult Index()
        {
            //Cr();
            SetVideo();
            ViewBag
                .Movies = c
                .Films
                .Take(50)
                .ToList();
            return View();
        }

        void Cr()
        {
            //var files = Directory.GetFiles("c://data/Icons");
            //var names = System.IO.File.ReadAllLines("c://data/names.txt");
            //var t = new List<FileInfo2>();

            //for (int i = 0; i < files.Length; i++)
            //{
            //    t.Add(new FileInfo2()
            //    {
            //        Name = new FileInfo(files[i]).Name
            //    });
            //}
            //var t1 = t.OrderBy(g => g.Name).ToList();

            //for (int i = 0; i < t1.Count; i++)
            //{
            //c.Films.Add(new Models.Film()
            //{
            //    Poster = path + t[i].Name,
            //    Imdb = GetIMDB().ToString(),
            //    Genre = names[i].Split(':')[1],
            //    Name = names[i].Split(':')[0],
            //    Year = GetYEAR(),
            //    Pagebg = "http://ascolon228-001-site1.ftempurl.com/Files/Pagesbg/loli.jpg",
            //    Video = "http://ascolon228-001-site1.ftempurl.com/files/video/hood.mp4",
            //    Description = "Легендарная история об отваге и любви — эффектнее, чем когда-либо! Закаленный в боях крестоносец и его командир бросают вызов продажным приспешникам английской короны…"
            //});
            //}
            var files = Directory.GetFiles("c://data/new");
            var names = System.IO.File.ReadAllLines("c://data/names.txt");

            for (int i = 0; i < names.Length; i++)
            {
                var data = names[i] + $":{i}.jpg";
                c.Films.Add(new Models.Film()
                {
                    Poster = path + data.Split(':')[2],
                    Imdb = GetIMDB().ToString().Substring(0, 4),
                    Genre = names[i].Split(':')[1],
                    Name = names[i].Split(':')[0],
                    Year = GetYEAR(),
                    Pagebg = "http://ascolon228-001-site1.ftempurl.com/Files/Pagesbg/loli.jpg",
                    Video = "http://ascolon228-001-site1.ftempurl.com/files/video/hood.mp4",
                    Description = "Легендарная история об отваге и любви — эффектнее, чем когда-либо! Закаленный в боях крестоносец и его командир бросают вызов продажным приспешникам английской короны…"
                });
            }
            c.SaveChanges();


        }
        class FileInfo2
        {
            public string Name { get; set; }
        }

        void SetVideo()
        {
            var f = c.Films.ToList();

            for (int i = 0; i < f.Count; i++)
            {
                f[i].Video = @"http://ascolon228-001-site1.ftempurl.com/Files/Video/web.mp4";
            }
            c.SaveChanges();
        }


    }
}
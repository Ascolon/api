using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Anime.Controllers
{
    public class KonturController : Controller
    {
        class CityArray
        {
            public string Id { get; set; }
            public string City { get; set; }
        }

        public JsonResult GetAllCity(string pattern)
        {
            var city = JsonConvert
                .DeserializeObject<List<CityArray>>(System.IO.File.ReadAllText(Server.MapPath("~/Files/kladr.json")));
            var res = city.Select(c => c.City)
                .Where(c => c.ToLower().Contains(pattern.ToLower()));
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anime.Context
{
    static public class ProjectAttribute
    {
        static public string ProjectUrl { get; } = "http://ascolon228-001-site1.ftempurl.com/";

        static public string Style { get; } = @"background-color: #353F59;font-size:14px;height:25px;margin:10px;display:flex;align-items:center;justify-content:center;border-radius: 4px";
    }

    static class TotalMilliseconds
    {
        private static readonly DateTime Jan1St1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static long Milliseconds { get { return (long)((DateTime.UtcNow - Jan1St1970).TotalMilliseconds); } }
    }
}
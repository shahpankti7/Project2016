using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project2016.Models
{
    public class MusicViewModel
    {
        public int artist_id { get; set; }

        public int album_id { get; set; }

        public int track_id { get; set; }

        public DateTimeOffset played { get; set; }

        public int? user_id { get; set; }

        public string artist_name { get; set; }

        public string album_name { get; set; }

        public string track_name { get; set; }
        
        public decimal? track_time { get; set; }        
    }
}
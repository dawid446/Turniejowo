using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace programowanie_wsi.Models
{
    public class Statistics
    {
        public int id_file { get; set; }
        public int id_team { get; set; }
        public String team_name { get; set; }
        public int points { get; set; }
        public int match { get; set; }
        public int win { get; set; }
        public int draw { get; set; }
        public int lose { get; set; }
        public int score_goal { get; set; }
        public int lose_goal { get; set; }
    }
}
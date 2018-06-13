using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace programowanie_wsi.Models
{
    public class TournamentTeamViewModel
    {
        
        public List<Team> Team { get; set; }
        public Tournament Tournament { get; set; }

        public int TeamID { get; set; }
        public string team_name { get; set; }

        public int[] teamList { get; set; }
      
    }
}
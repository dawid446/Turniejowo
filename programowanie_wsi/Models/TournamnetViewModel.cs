using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace programowanie_wsi.Models
{
    public class TournamnetViewModel
    {
        
        public List<TournamentTeam> TournamentTeam { get; set; }

        public int TournamentID { get; set; }

        public string tournament_name { get; set; }
        public Player Player { get; set; }
        public int[] tournamnetList { get; set; }
    }
}
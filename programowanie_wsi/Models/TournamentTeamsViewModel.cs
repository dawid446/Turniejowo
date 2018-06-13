using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace programowanie_wsi.Models
{
    public class TournamentTeamsViewModel
    {
        public IQueryable<Tournament> TournamentList { get; set; }
        public Team Team { get; set; }
 
    }
}
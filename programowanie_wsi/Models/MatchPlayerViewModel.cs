using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace programowanie_wsi.Models
{
    public class MatchPlayerViewModel
    {

        public Player Player { get; set; }
        public List<Player> PlayerList { get; set; }
        public List<Player> PlayerList1 { get; set; }

      
        public PlayerMatch playerMatch { get; set; }

        public List<PlayerMatch> listPlayerMatch { get; set; }
        public List<PlayerMatch> listPlayerMatch1 { get; set; }

        public IQueryable<PlayerMatch> goalList { get; set; }
        public IQueryable<PlayerMatch> goalList1 { get; set; }

        public int[] playerSelect { get; set; }
        public int[] playerSelect1 { get; set; }

        public Match Match { get; set; }
    }
}
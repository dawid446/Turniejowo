using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace programowanie_wsi.Models
{
    public class PlayerTeamViewModel
    {
       public IQueryable<Player> PlayerList { get; set; }
       public Player Player { get; set; }
       public Team Team { get; set; }
       public Tournament Tournament { get; set; }
    }
}
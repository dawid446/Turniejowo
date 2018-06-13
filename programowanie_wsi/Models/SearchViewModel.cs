using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace programowanie_wsi.Models
{
    public class SearchViewModel
    {
        public List<Tournament> Tournament { get; set; }
        public List<Player> Player { get; set; }
        public List<Team> Team { get; set; }
       
    }

}
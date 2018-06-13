using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace programowanie_wsi.Models
{
    public class Google_map
    {
        [Key]
        public int LocationID { get; set; }

       
        public string lat { get; set; }
        public string lng { get; set; }


        public virtual ICollection<Match> Match { get; set; }


    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace programowanie_wsi.Models
{
    public class PlayerMatch
    {
        [Key]
        public int PlayerMatchID { get; set; }

        [Range(0, 10, ErrorMessage = "Przedzial od 0 do 10")]
        [Display(Name = "Strzelone gole")]
        public int? goal { get; set; }
        
        [Display(Name = "Rezerwa")]
        public bool reserve { get; set; }
        public int MatchID { get; set; }

        [ForeignKey("MatchID")]
        public virtual Match Match { get; set; }

        public int PlayerID { get; set; }
        [ForeignKey("PlayerID")]
        public virtual Player Player { get; set; }

       
    }
}
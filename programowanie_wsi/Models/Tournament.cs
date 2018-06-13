using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace programowanie_wsi.Models
{
    public class Tournament 
    {
        [Key]
        public int TournamentID { get; set; }

        [Display(Name = "Nazwa turnieju")]
        [Required(ErrorMessage = " Prosze podać nazwę drużyny ")]
        public string name { get; set; }

        [Display(Name = "Data utworzenia turnieju")]
        public DateTime date { get; set; }

        public virtual ICollection<TournamentTeam> TournamentTeam { get; set; }
        public virtual ICollection<Match> Match { get; set; }
        public virtual ICollection<PlayerTeam> PlayerTeam { get; set; }
    }
   
}
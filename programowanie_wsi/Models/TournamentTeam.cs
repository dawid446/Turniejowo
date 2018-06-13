using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace programowanie_wsi.Models
{
    public class TournamentTeam
    {
        [Key]
        public int TournamentTeamID { get; set; }

        public int TournamentID { get; set; }
        [ForeignKey("TournamentID")]
        public virtual Tournament Tournament { get; set; }

     
        public int TeamID { get; set; }
        [ForeignKey("TeamID")]
        public virtual Team Team{ get; set; }
    }
}
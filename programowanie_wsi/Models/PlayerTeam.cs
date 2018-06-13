using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace programowanie_wsi.Models
{
    public class PlayerTeam
    {
        [Key]
        public int PlayerTeamID { get; set; }

        public bool delete { get; set; }

        public int PlayerID { get; set; }
        [ForeignKey("PlayerID")]
        public virtual Player Player { get; set; }

        public int TeamID { get; set; }
        [ForeignKey("TeamID")]
        public virtual Team Team { get; set; }

        public int? TournamentID { get; set; }
        [ForeignKey("TournamentID")]
        public virtual Tournament Tournament { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace programowanie_wsi.Models
{
    public class Team
    {
        [Key]
        public int TeamID { get; set; }

        [Required(ErrorMessage = " Prosze podać nazwę drużyny ")]
        [Display(Name = "Nazwa drużyny")]
        public string team_name { get; set; }

        public bool delete { get; set; }

        public string UserId { get; set; }


        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public int FileID { get; set; }

       public virtual File File { get; set; }

        public virtual ICollection<TournamentTeam> TournamentTeam { get; set; }
        public virtual ICollection<PlayerTeam> PlayerTeam{ get; set; }

        public virtual ICollection<Match> Match { get; set; }
        public virtual ICollection<Match> Match1 { get; set; }

       


    }

    
}
   
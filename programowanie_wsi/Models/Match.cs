using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace programowanie_wsi.Models
{
    public class Match
    {
        [Key]
        public int MatchID { get; set; }

        [Range(0, 20, ErrorMessage = "Wynik nie może byc ujemny")]
        [Display(Name = "Wynik 1 druzyny")]
        public int? score { get; set; }

        [Range(0, 20, ErrorMessage = "Wynik nie może byc ujemny")]
        [Display(Name = "Wynik 2 druzyny")]
        public int? score1 { get; set; }

        [Display(Name = "Kolejka")]
        public int queue { get; set; }

        [Display(Name = "Data meczu")]
        public DateTime? date { get; set; }

        public bool isPlayed { get; set; }
        public bool isBreak { get; set; }

        [Display(Name = "Drużyna 1")]
        public int TeamID { get; set; }
        public virtual Team Team { get; set; }

        [Display(Name = "Drużyna 2")]
        public int TeamID1 {get ; set;}
        public virtual Team Team1 { get; set; }

       
        public int TournamentID { get; set; }
        public virtual Tournament Tournament { get; set; }

        public int? LocationID { get; set; }
        public virtual Google_map Google_Map { get; set; }


    }
}
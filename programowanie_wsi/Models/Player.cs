using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace programowanie_wsi.Models
{
    public class Player
    {
        [Key]
        public int PlayerID { get; set; }

        [Required]
        [Display(Name = "Imie")]
        public string name { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        public string surname { get; set; }

        [Range(1, 99, ErrorMessage = "Numer zawodnika musi być z przedziału od 1 do 99")]
        [Display(Name = "Numer zawodnika")]
        public int number { get; set; }
    
        [Range(100, 210, ErrorMessage = "Wzrost musi mieścić się w przedziale 100 a 210 cm")]
        [Display(Name = "Wzrost")]
        public int height { get; set; }

        [Range(20, 150, ErrorMessage = "Waga musi być wartością mięzdzy 20kg a 150 kg")]
        [Display(Name = "Waga")]
        public int weight { get; set; }

        public int? PositionID { get; set; }
        [ForeignKey("PositionID")]
        public virtual Position Position { get; set; }

        public virtual ICollection<PlayerMatch> PlayerMatch { get; set; }
        public virtual ICollection<PlayerTeam> PlayerTeam { get; set; }

       
        
    }
}
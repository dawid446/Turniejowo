using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace programowanie_wsi.Models
{
    public class Position
    {
        [Key]
        public int PostionID { get; set; }

        [Display(Name = "Pozycja")]
        [Required]
        public string position { get; set; }

        public virtual ICollection<Player> Player { get; set; }
    }

}
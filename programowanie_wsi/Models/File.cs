﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebGrease.Activities;


namespace programowanie_wsi.Models
{
    public class File
    {
        public int FileID { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        public byte[] Content { get; set; }

        public virtual ICollection<Team> Team { get; set; }
    }
}
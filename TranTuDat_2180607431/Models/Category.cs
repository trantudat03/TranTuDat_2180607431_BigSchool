using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TranTuDat_2180607431.Models
{
    public class Category
    {
        public byte Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}
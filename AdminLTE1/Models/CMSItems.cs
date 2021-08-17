using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE1.Models
{
    public class CMSItems
    {
        [Key]
        public int Id { get; set; }

        
        public string PageName { get; set; }

        [Required(ErrorMessage = "Please Fill Pageurl.")]
        public string PageUrl { get; set; }

        [Required(ErrorMessage = "Please Fill Description.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please Fill BannerImage.")]
        public string BannerImage { get; set; }
    }
}

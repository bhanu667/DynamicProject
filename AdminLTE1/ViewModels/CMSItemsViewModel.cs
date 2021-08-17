using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE1.ViewModels
{
    public class CMSItemsViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Remote("IsProductNameExist", "Home", AdditionalFields = "Id",
                ErrorMessage = "Page name already exists")]
        public string PageName { get; set; }
        [Required]
        [StringLength(50)]
        [Remote("IsPageUrlExist", "Home", AdditionalFields = "Id",
                ErrorMessage = "Page Url already exists")]
        public string PageUrl { get; set; }
        [Required(ErrorMessage = "Please Fill Description.")]
        public string Description { get; set; }
        //[Required(ErrorMessage = "Please Fill BannerImage.")]
        public string BannerImage { get; set; }
        [Required(ErrorMessage = "Please Fill BannerImage.")]
        public IFormFile BannerImageFile { get; set; }

    }
}

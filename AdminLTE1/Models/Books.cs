using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AdminLTE1.Models
{
    public class Books
    {
        [Key]
        public int BookId { get; set; }
        [Required(ErrorMessage = "Please Fill Description.")]
        //[Remote("IsBookNameExist", "Home", AdditionalFields = "BookId",
        //        ErrorMessage = "Book name already exists")]
        public string BookName { get; set; }
        [Required(ErrorMessage = "Please Fill Author.")]

        public string Author { get; set; }
        [Required(ErrorMessage = "Please Fill Description.")]

        public string Description { get; set; }
        public int? ClassId { get; set; }
        [Required(ErrorMessage = "Please select status.")]
        public bool IsActive { get; set; }
        public string BookImage { get; set; }
        [Required(ErrorMessage = "Please Fill BannerImage.")]
        [NotMapped]
        public IFormFile BookImageFile { get; set; }
    }
}

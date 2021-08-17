using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE1.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [NotMapped]
        //[Display(Name = "Profile Picture")]
        public IFormFile ProfilePictureFile { get; set; }

        [Display(Name = "Profile Picture")]
        public string ProfilePicture { get; set; }

       
        [Display(Name = "Country")]
        public int CId { get; set; }

        public int StateId { get; set; }

        public int CityId { get; set; }

       // [Required]
        [Display(Name = "Address1")]
        public string Address1 { get; set; }

       // [Required]
        [Display(Name = "Address2")]
        public string Address2 { get; set; }

        // public string Password { get; set; }


    }
}

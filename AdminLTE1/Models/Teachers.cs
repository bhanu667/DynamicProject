using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE1.Models
{
    public class Teachers
    {
        [Key]
        public int TeacherId { get; set; }
        public int? ClassId { get; set; }
        [Required(ErrorMessage = "Please Fill Name.")]
        public string TeacherName { get; set; }
        [Required(ErrorMessage = "Please Fill Subject.")]
        public string Subject { get; set; }
        public string TeacherImage { get; set; }
        [Required(ErrorMessage = "Please Fill BannerImage.")]
        [NotMapped]
        public IFormFile TeacherImageFile { get; set; }
    }
}

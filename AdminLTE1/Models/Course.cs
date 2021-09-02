using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE1.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public int? ClassId { get; set; }
        [Required(ErrorMessage = "Please Fill Name.")]
        public string CourseName { get; set; }
    }
}

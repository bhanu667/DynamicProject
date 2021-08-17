using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE1.Models
{
    public class State
    {
        [Key]
        public int StateId { get; set; }
        public string SName { get; set; }
        public int CId { get; set; }
    }
}

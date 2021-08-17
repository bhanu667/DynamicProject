using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE1.Models
{
    public class OrderAPI
    {
        public string Id { get; set; }
        public string Intent { get; set; }
        public string State { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

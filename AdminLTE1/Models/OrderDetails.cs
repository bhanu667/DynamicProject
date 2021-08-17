using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE1.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string Status { get; set; }
        public DateTime CreateTime { get; set; }
    }  

}

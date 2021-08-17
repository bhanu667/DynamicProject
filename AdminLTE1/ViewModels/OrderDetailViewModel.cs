using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE1.ViewModels
{
    public class OrderDetailViewModel
    {
        public int OrderDetailId { get; set; }
        public int OrderForId { get; set; }
        public string Status { get; set; }
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Price { get; set; }
    }
}

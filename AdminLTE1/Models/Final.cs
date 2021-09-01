using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE1.Models
{
    public class Final
    {
        public int id { get; set; }
        public string title { get; set; }
        public int? pId { get; set; }
        public bool Checked { get; set; }
        public List<Child> subs { get; set; }
    }
    public class Child
    {
        public int id { get; set; }
        public string title { get; set; }
        public bool Checked { get; set; }
    }
}

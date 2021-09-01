using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE1.Models
{
    public partial class MenuItems
    {
        public MenuItems()
        {
            MenuPermissions = new HashSet<MenuPermissions>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int ParentId { get; set; }
        public int MenuLevel { get; set; }
        public int MenuGrpId { get; set; }

        public virtual ICollection<MenuPermissions> MenuPermissions { get; set; }
    }
}

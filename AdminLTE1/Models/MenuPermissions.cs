using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE1.Models
{
    public partial class MenuPermissions
    {
        public Guid PermissionId { get; set; }
        public int MenuId { get; set; }
        public Guid RoleId { get; set; }

        public virtual MenuItems Menu { get; set; }
    }
}

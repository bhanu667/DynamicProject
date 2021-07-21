using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE1.Entities
{
    public class MenuPermission
    {
        [Key]
        public Guid PermissionId { get; set; }
        public int MenuId { get; set; }
        public Guid RoleId { get; set; }
    }
}

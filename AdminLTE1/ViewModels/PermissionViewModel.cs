using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE1.ViewModels
{
    public class PermissionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int ParentId { get; set; }
        public int MenuLevel { get; set; }
        public bool HasAccess { get; set; }
        public int MenuGrpId { get; set; }
    }


    public class ParentChildModel
    {
        public PermissionViewModel ParentNode { get; set; }

        public IList<PermissionViewModel> ChildNodes { get; set; }

    }

    public class ListModel
    {
        public List<ParentChildModel> Nodes { get; set; }
    }
}

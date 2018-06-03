using System;
using System.Collections.Generic;

namespace Logistika.Service.Common.Entities.Menu
{
    public class GroupUser
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public IList<string> GroupUsers { get; set; }
        public IList<MenuItem> MenuItems { get; set; }
        public IList<int> OperationId { get; set; }
    }
}

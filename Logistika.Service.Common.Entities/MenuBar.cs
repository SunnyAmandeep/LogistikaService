using System;
using System.Collections.Generic;

namespace Logistika.Service.Common.Entities
{
    public class MenuBar
    {
        public int MenuID { get; set; }
        public int? ParentID { get; set; }
        public bool IsMenuItemGroup { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tooltip { get; set; }
        public int Sequence { get; set; }
        public string URL { get; set; }
        public string Icon { get; set; }
    }
}
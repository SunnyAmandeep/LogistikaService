
using Logistika.Service.Common.Entities.Lookup;
using System;
using System.Collections.Generic;
namespace Logistika.Service.Common.Entities
{
    public class KfisUser : Person
    {

        public int KfisUserId { get; set; }
        public string UserName { get; set; }
        public string Designation { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public int PrimaryClient { get; set; }
        public string ClientList { get; set; }
        public string PermissionList { get; set; }
        public string JobTitle { get; set; }
        public DateTime LastLogin { get; set; }

        public string LeftImagePath { get; set; }
        public string RightImagePath { get; set; }
        public string AddtionalPermissionList { get; set; }
    }


    public class LogistikaUser : Person
    {

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Designation { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public string JobTitle { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }
        public string BranchId { get; set; }
        public string Branch { get; set; }
        public IList<DropdownData> OtherInfo { get; set; }

    }
    public class LogistikaUserModal
    {
        public LogistikaUser User { get; set; }
        public IList<Document> Documents { get; set; }
        public IList<LogistikaUser> Users { get; set; }
    }
}

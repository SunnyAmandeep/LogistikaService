using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistika.Service.Common.Entities
{
    public class EmployeePlanner
    {
        public int      CompanyAddressID { get; set; }
        public DateTime WeekDate { get; set; }
        public string   CompanyAddress { get; set; }
        public int      TotalDrivers { get; set; }
        public DateTime PlanningStartDate { get; set; }
        public DateTime PlanningEndDate { get; set; }
    }

    public class EmployeePlannerModal
    {
        public EmployeePlanner employeePlanner { get; set; }
    }
}

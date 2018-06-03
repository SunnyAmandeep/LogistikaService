using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistika.Service.Common.Entities
{
    public class Metrics
    {
        public DriverMetrics DriverMetrics { get; set; }
        public RevenueExpenseMetrics RevenueExpenseMetrics { get; set; }
        public IList<RevenueExpenseMetricsDate> RevenueExpenseMetricsDate { get; set; }
        public RevPaySubUserMetrics RevPaySubUserMetrics { get; set; }
        public OperationalExpenseMetrics OperationalExpenseMetrics { get; set; }
        public SolutionExpenseMetrics SolutionExpenseMetrics { get; set; }
    }

    public class DriverMetrics
    {
        public int Drivers { get; set; }
        public int WavesCompletedOnTime { get; set; }
        public int DelayedWaves { get; set; }
        public int OfficeStaff { get; set; }
    }

    public class RevenueExpenseMetrics
    {
        public int TotalOrderRevenue { get; set; }
        public int TotalDriverExpenditure { get; set; }
        public int TotalShippingExpenditure { get; set; }
        public int OtherExpenditure { get; set; }
        public int CashInFlow { get; set; }
        public int CashOutFlow { get; set; }
    }

    public class RevenueExpenseMetricsDate
    {
        public DateTime Date { get; set; }
        public int TotalOrderRevenue { get; set; }
        public int TotalDriverExpenditure { get; set; }
        public int TotalShippingExpenditure { get; set; }
        public int OtherExpenditure { get; set; }
        public int CashInFlow { get; set; }
        public int CashOutFlow { get; set; }
    }

    public class RevPaySubUserMetrics
    {
        public int CashInFlow { get; set; }
        public int CashOutFlow { get; set; }
        public int Credit { get; set; }
        public int Cash { get; set; }
        public int OnlinePayments { get; set; }
        public int NewSubscription { get; set; }
        public int UpgradeSubscription { get; set; }
        public int CancelledSubscription { get; set; }
        public int NewUsers { get; set; }
        public int InactiveUsers { get; set; }
    }

    public class OperationalExpenseMetrics
    {
        public int TaxExpense { get; set; }
        public int SalaryExpense { get; set; }
        public int AdministrationExpense { get; set; }
        public int OtherExpense { get; set; }
    }

    public class SolutionExpenseMetrics
    {
        public int TaxExpense { get; set; }
        public int SalaryExpense { get; set; }
        public int AdministrationExpense { get; set; }
        public int OtherExpense { get; set; }
    }
}

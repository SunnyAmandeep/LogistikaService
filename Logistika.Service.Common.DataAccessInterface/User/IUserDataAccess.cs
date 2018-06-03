
using Logistika.Service.Common.Entities;
using Logistika.Service.Common.Entities.Menu;
using System.Collections.Generic;
using System;

namespace Logistika.Service.Common.DataAccessInterface.User
{
    public interface IUserDataAccess
    {
        //string LogFileImport(string FileName, string Status, string ValidRecords, string InValidRecords, string ErrorFileName, string ImportFileHistory_PK);

        IList<string> GetUserRoles(string UserId);
        
        //string UserFileImport(string FileName, string CallType);
        //string FleetFileImport(string FileName, string CallType);
        IList<Country> GetCountryInfo(string Country);

        ForgotUserPassword ForgotUserNamePassword(string UserName = null, string PrimaryPhone = null);

        string checkAvailablity(string Type, string UserName = null, string Company = null, string TaxIdentification = null);

        Metrics GetMetricsList(DateTime StartDt, DateTime EndDt, int? CompanyAddressID = 0);

        IList<Inquiry> GetInquiryList(int? UserCompanyID = 0);
        bool SaveInquiryInfo(Inquiry Inquiry);

        bool SaveExpenseLog(ExpenseLog ExpenseLog);
        IList<ExpenseLog> GetExpenseLogList();

        IList<SubscriptionPlans> GetSubscriptionPlans();

        bool SaveCompanySubscriptionInfo(CompanySubscription CompanySubscription);
        IList<CompanySubscription> GetCompanySubscriptionList(int? UserCompanyID = 0);

        bool UpdateEmployeePlanner(EmployeePlanner employeePlanner);
        IList<EmployeePlanner> GetEmployeePlannerList(int? CompanyID = 0, int? AddressId = 0);

        IList<Customer> GetCustomerList(string UserName);
        bool SaveCustomer(Customer customer);

        IList<CompanyAddress> GetCompanyList(int? CompanyAddressID = 0);
        bool SaveCompanyAddressInfo(CompanyAddress companyAddress);

        LogistikaUserModal GetsUsers(int? Id = 0, int? AddressId = 0);
        string SaveFrameworkUser(FrameworkUser User);

        IList<FrameworkUser> getUserList(string UserName);
        bool UpdateUser(string UserName, bool IsActive);

        IList<ItemList> getItemList(string Type);
        IList<CompanyAddress> getCompanyAddressList(int? CompanyID = 0, int? FrameworkAddressID = 0);
        IList<UserStats> getUserStats(int CompanyID, string UserType, string StartDt, string EndDt);
        IList<MenuBar> getMenuItem(string UserName, string Password);
        IList<GeoLocation> getGeoLocation(int CompanyID);
        IList<StatusList> getStatusList();
        IList<CompanyAddress> GetHandOffAddress(int? CompanyID = 0);

        string AddUpdateCompany(string CompanyName, string TaxIdentification, int IsActive, string AuthCode);

        IList<Fleet> getFleetMaster(string Make, string Model);
        IList<WaveFleet> getFleet4Wave();

        FleetModal GetsFleet(string Id = "", int? AddressId = 0);
        bool SaveFleet(Fleet Fleet, string DocumentXml);
        
        //string InsUpdFleetinfo(Fleet myfleet);
        //IList<EmployeePlanner> GetEmployeePlannerList(int CompanyID, int CompanyAddressID, string LastModifiedBy, char Operation, DateTime? Date = null, int? TotalDrivers = null);


        IList<MenuItem> GetUserMenu(string UserId, string Type = "NESTED");
        //  IList<Entities.Menu.MenuItem> GetAllUserMenu(string Type = "NESTED");
        IList<GroupUser> GetUserGroup(int? GroupId);
        bool CheckRestrictedServiceAuthorization(string UserName, string Url, string OprationCode, string ScreenCode = "MVC_KFISPORTAL");
        KfisUser GetKfisUser(string UserName);


        string SaveUser(KfisUser User);
        bool SaveUser(LogistikaUser User, string DocumentXml);
        LogistikaUser SaveUser(string Id);
        //string LoginUserAuthorization { get; set; }

        void AuditLogin(string UserName);
        string UpdateFrameworkAddress(Address address);
    }
}


using Logistika.Service.Common.Entities;
using Logistika.Service.Common.Entities.Menu;
using System.Collections.Generic;
using System;

namespace Logistika.Service.Common.BusinessComponentInterface.User
{
    public interface IUserBusinessComponent
    {

        //string LogFileImport(string FileName, string Status, string ValidRecords, string InValidRecords, string ErrorFileName, string ImportFileHistory_PK);

        string UserFileImport(string FileName, string CallType);
        string FleetFileImport(string FileName, string CallType);

        IList<Country> GetCountryInfo(string Country);

        ForgotUserPassword ForgotUserNamePassword(string UserName = null, string PrimaryPhone = null);

        string checkAvailablity(string Type, string UserName = null, string Company = null, string TaxIdentification = null);

        Metrics GetMetricsList(DateTime StartDt, DateTime EndDt, int? CompanyAddressID = 0);

        IList<SubscriptionPlans> GetSubscriptionPlans();

        IList<Inquiry> GetInquiryList(int? UserCompanyID = 0);
        bool SaveInquiryInfo(Inquiry Inquiry);

        bool SaveExpenseLog(ExpenseLog ExpenseLog);
        IList<ExpenseLog> GetExpenseLogList();

        bool SaveCompanySubscriptionInfo(CompanySubscription CompanySubscription);
        IList<CompanySubscription> GetCompanySubscriptionList(int? UserCompanyID = 0);

        bool UpdateEmployeePlanner(EmployeePlanner employeePlanner);
        IList<EmployeePlanner> GetEmployeePlannerList(int? Id = 0, int? AddressId = 0);

        bool SaveUser(LogistikaUser User, IList<Document> DocumentXml);
        LogistikaUserModal GetsUsers(int? Id = 0, int? AddressId = 0);

        IList<Customer> GetCustomerList(string UserName);
        bool SaveCustomer(Customer customer);

        IList<CompanyAddress> GetCompanyList(int? CompanyAddressID = 0);
        bool SaveCompanyAddressInfo(CompanyAddress companyAddress);

        //IList<CompanyAddress> getCompanyAddressListSingle(int CompanyID);
        FrameworkUser AuthenticateUser(string UserName, string Password);
        IList<MenuBar> getMenuItem(string UserName, string Password);
        //IList<OrderDriverInfo> getOrderDriverInfo(string StartDt, string EndDt);
        IList<StatusList> getStatusList();
        IList<UserStats> getUserStats(int CompanyID, string UserType, string StartDt, string EndDt);
        IList<CompanyAddress> getCompanyAddressList(int? CompanyID = 0, int? FrameworkAddressID = 0);
        IList<FrameworkUser> getUserList(string UserName);
        bool UpdateUser(string UserName, bool IsActive);
        IList<ItemList> getItemList(string Type);
        IList<GeoLocation> getGeoLocation(int CompanyID);
        IList<CompanyAddress> GetHandOffAddress(int? CompanyID = 0);

     
        AuthUser AuthorizeUser(string UserName, string Password);

        string AddUpdateCompany(string CompanyName, string TaxIdentification, int IsActive, string AuthCode);

        string SaveFrameworkUser(FrameworkUser User);

        IList<Fleet> getFleetMaster(string Make, string Model);
        IList<WaveFleet> getFleet4Wave();

        bool SaveFleet(Fleet Fleet, IList<Document> DocumentXml, string callType);
        FleetModal GetFleet(string  Id = "", int? AddressId = 0);
        //string InsUpdFleetinfo(Fleet myfleet);

        //IList<EmployeePlanner> GetEmployeePlannerList(int CompanyID, int CompanyAddressID, string LastModifiedBy, char Operation);
        //IList<EmployeePlanner> GetEmployeePlannerList(int CompanyID, int CompanyAddressID, string LastModifiedBy, char Operation, DateTime? Date = null, int? TotalDrivers = null);

        IList<string> GetUserRoles(string UserId);
        //IList<MenuItem> GetUserMenu(string UserId, string Type = "NESTED");
        //IList<Entities.Menu.MenuItem> GetAllUserMenu(string Type = "NESTED");
        //IList<GroupUser> GetUserGroup(int? GroupId);
        //bool CheckRestrictedServiceAuthorization(string UserName, string Url, string OprationCode, string ScreenCode = "MVC_KFISPORTAL");
        //KfisUser GetKfisUser(string UserName);
        //KfisUser GetAdUser(string UserName);
        //string SaveUser(KfisUser User);
        string UpdateFrameworkAddress(Address myAddress);
    }
}

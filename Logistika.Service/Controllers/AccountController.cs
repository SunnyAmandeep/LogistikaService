using Logistika.Service.Common.BusinessComponentInterface.User;
using Logistika.Service.Common.Entities;
using Logistika.Service.Common.Entities.Menu;
using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Logistika.Service.Common.Helper;

namespace Logistika.Service.Controllers
{
    //[Authorize]
    // [RoutePrefix("api/Account")]

    public class AccountController : ApiController
    {

        IUserBusinessComponent _businessInstance = null;
        IOrderBusinessComponent _orderBusinessInstance = null;

        public AccountController(IUserBusinessComponent Instance, IOrderBusinessComponent OrderBusinessInstance)
        {
            _businessInstance = Instance;
            _orderBusinessInstance = OrderBusinessInstance;
        }

        [HttpGet]
        [Route("api/Account/CountryInfo")]
        public IList<Country> GetCountryInfo(string Country = null)
        {
            return _businessInstance.GetCountryInfo(Country);
        }
        
        [HttpGet]
        [Route("api/Account/User/Forgot")]
        public ForgotUserPassword ForgotUserNamePassword(string UserName = null, string PrimaryPhone = null)
        {
            return _businessInstance.ForgotUserNamePassword(UserName,PrimaryPhone);
        }


        [HttpGet]
        [Route("api/Account/UserCompany/Availablity")]
        public string checkAvailablity(string Type, string UserName = null, string Company = null, string TaxIdentification = null)
        {
            return _businessInstance.checkAvailablity(Type, UserName, Company,TaxIdentification);
        }


        [HttpGet]
        [Route("api/Account/Company/Metrics")]
        public Metrics GetMetrics(DateTime StartDt, DateTime EndDt, int? CompanyAddressID = 0)
        {
            return _businessInstance.GetMetricsList(StartDt,EndDt,CompanyAddressID);
        }


        [HttpPost]
        [Route("api/Company/Expense")]
        public bool SaveExpense(ExpenseLog ExpenseLog)
        {
            return _businessInstance.SaveExpenseLog(ExpenseLog);
        }

        [HttpGet]
        [Route("api/Company/Expense")]
        public IList<ExpenseLog> GetExpenseLogList()
        {
            return _businessInstance.GetExpenseLogList();
        }

        [HttpGet]
        [Route("api/Company/Users")]
        public IList<FrameworkUser> GetCompanyUser()
        {
            return (IList<FrameworkUser>)null;
            //return _businessInstance.GetCompanyUser(UserName);
        }

        [HttpGet]
        [Route("api/Account/getUserList")]
        public IList<FrameworkUser> getUserList(string UserName)
        {
            return _businessInstance.getUserList(UserName);
        }

        [HttpGet]
        [Route("api/Account/ItemList")]
        public IList<ItemList> getItemList(string Type)
        {
            return _businessInstance.getItemList(Type);
        }

        [HttpPost]
        [Route("api/Account/AuthorizeUser")]
        public AuthUser AuthorizeUser([FromBody] JObject Data)
        {
            if (Data == null)
            {
                return (AuthUser)null;
            }
            Dictionary<string, string> data = Data.ToObject<Dictionary<string, string>>();
            //[FromBody]string UserName, [FromBody]string Password
            //dynamic data = Data;
            return _businessInstance.AuthorizeUser(data["UserName"], data["Password"]);
        }

        [HttpGet]
        [Route("api/Account/Company/Address")]
        public IList<CompanyAddress> getCompanyAddressList(int? FrameworkAddressID = 0)
        {
            return _businessInstance.getCompanyAddressList(Convert.ToInt32(ClaimHelper.CompanyId), FrameworkAddressID.HasValue ? FrameworkAddressID.Value : 0);
        }

        [HttpGet]
        [Route("api/Account/FleetMaster")]
        public IList<Fleet> GetFleetMaster(string Make, string Model = "")
        {
            return _businessInstance.getFleetMaster(Make, Model);
        }

        [HttpGet]
        [Route("api/Account/Company/HandOffAddress")]
        public IList<CompanyAddress> getHandOffAddress()
        {
            return _businessInstance.GetHandOffAddress(Convert.ToInt32(ClaimHelper.CompanyId));
            //return _businessInstance.getCompanyAddressList( 2, FrameworkAddressID.HasValue ? FrameworkAddressID.Value : 0);
        }

        [HttpGet]
        [Route("api/Account/Company")]
        public string AddUpdateCompany(string CompanyName, string TaxIdentification, int IsActive, string AuthCode)
        {
            return _businessInstance.AddUpdateCompany(CompanyName, TaxIdentification, IsActive, AuthCode);
        }

        [HttpGet]
        [Route("api/Account/EmployeePlanner")]
        public IList<EmployeePlanner> GetEmployeePlanner(int? CompanyAddressID = 0)
        {
            return _businessInstance.GetEmployeePlannerList(Convert.ToInt32(ClaimHelper.CompanyId), CompanyAddressID);
        }

        [HttpPost]
        [Route("api/Account/EmployeePlanner")]
        public bool UpdateEmployeePlanner (EmployeePlannerModal EmployeePlanner)
        {
            return _businessInstance.UpdateEmployeePlanner(EmployeePlanner.employeePlanner);
        }

        [HttpGet]
        [Route("api/Account/Customer")]
        public IList<Customer> GetCustomer(string UserName = "")
        {
            return _businessInstance.GetCustomerList(UserName);
        }

        [HttpPost]
        [Route("api/Account/Customer")]
        public bool SaveCustomer(Customer customer)
        {
            return _businessInstance.SaveCustomer(customer);
        }

        [HttpGet]
        [Route("api/Account/Company/CompanyAddress")]
        public IList<CompanyAddress> GetCompanyAddress(int? CompanyAddressID = 0)
        {
            return _businessInstance.GetCompanyList(CompanyAddressID);
        }

        [HttpPost]
        [Route("api/Account/Company/CompanyAddress")]
        public bool SaveCompanyAddressInfo(CompanyAddress companyAddress)
        {
            return _businessInstance.SaveCompanyAddressInfo(companyAddress);
        }

        [HttpGet]
        [Route("api/Account/Company/User")]
        public LogistikaUserModal GetUsers(int? Id = 0, int? AddressId = 0)
        {
            return _businessInstance.GetsUsers(Id, AddressId);
        }

        [HttpPost]
        [Route("api/Account/Company/User")]
        public string InsertUpdateUser(LogistikaUserModal Modal)
        {
            try
            {
                if (_businessInstance.SaveUser(Modal.User, Modal.Documents))

                    return "Data Saved Successfully.";
            }
            catch (Exception ex)
            {
                   return ex.Message;
            }

            return "Oops";
        }

        [HttpGet]
        [Route("api/Account/Company/Inquiry")]
        public IList<Inquiry> GetInquiry(int? UserCompanyID = 0)
        {
            return _businessInstance.GetInquiryList(UserCompanyID);
        }

        [HttpPost]
        [Route("api/Account/Company/Inquiry")]
        public bool SaveInquiry(Inquiry Inquiry)
        {
            return _businessInstance.SaveInquiryInfo(Inquiry);
        }

        [HttpGet]
        [Route("api/Account/Company/CompanySubscription")]
        public IList <CompanySubscription> GetCompanySubscription(int? UserCompanyID = 0)
        {
            return _businessInstance.GetCompanySubscriptionList(UserCompanyID);
        }

        [HttpGet]
        [Route("api/Account/Company/SubscriptionPlans")]
        public IList<SubscriptionPlans> GetSubscriptionPlans()
        {
            return _businessInstance.GetSubscriptionPlans();
        }

        [HttpPost]
        [Route("api/Account/Company/CompanySubscription")]
        public bool SaveCompanySubscription(CompanySubscription CompanySubscription)
        {
            return _businessInstance.SaveCompanySubscriptionInfo(CompanySubscription);
        }

        [HttpGet]
        [Route("api/Account/UserStats")]
        public IList<UserStats> getUserStats(string UserType, string StartDt, string EndDt)
        {
            return _businessInstance.getUserStats(Convert.ToInt32(ClaimHelper.CompanyId), UserType, StartDt, EndDt);
        }

        [HttpGet]
        [Route("api/Account/StatusList")]
        public IList<StatusList> getStatusList()
        {
            return _businessInstance.getStatusList();
        }

        [HttpGet]
        [Route("api/Account/Menu")]
        public IList<MenuBar> getMenuItem(string userName, string Password)
        {
            return _businessInstance.getMenuItem(userName, Password);
        }

        [HttpPost]
        [Route("api/Account/FrameworkUser1")]
        public string SaveFrameworkUser1(FrameworkUser User)
        {
            return "Test";//_businessInstance.SaveFrameworkUser(UserName, Password, FirstName, MiddleName, LastName, PrimaryEmail, PrimaryPhone, PrimaryFax, FrameworkApplicationUserType_FK, Company_FK, MyAddress);
        }

        [HttpPost]
        [Route("api/Account/Address")]
        public string UpdateAddress(Address MyAddress)
        {
            return _businessInstance.UpdateFrameworkAddress(MyAddress);
            //return "Success";
        }

        [HttpPost]
        [Route("api/Account/SaveFrameworkUser")]
        public string SaveFrameworkUser(FrameworkUser User)
        {
            return _businessInstance.SaveFrameworkUser(User);
		}

        [HttpPost]
        [Route("api/User/Update")]
        public bool UpdateUser(string UserName, bool IsActive)
        {
            return _businessInstance.UpdateUser(UserName, IsActive);
        }

        //FleetModal GetsFleet(string Id = "", int? AddressId = 0);
        [HttpGet]
        [Route("api/Company/Fleet")]
        public FleetModal GetFleet(string Id = "", int? AddressId = 0)
        {
          return _businessInstance.GetFleet(Id,AddressId);
        }

        [HttpGet]
        [Route("api/Company/WaveFleet")]
        public IList<WaveFleet> GetFleet4Wave()
        {
            return _businessInstance.getFleet4Wave();
        }

        [HttpGet]
        [Route("api/Account/Company/FileImport")]
        public IList<FileImport> FileImport()
        {
            return _orderBusinessInstance.GetFileImportList();
        }

        [HttpPost]
        [Route("api/Account/Company/FileImport")]
        public  string FileImport(string FileName, string CallType)
        {
            if (CallType == "User")
            {
                return _businessInstance.UserFileImport(FileName, CallType);
            }

            if (CallType == "Fleet")
            {
                return _businessInstance.FleetFileImport(FileName, CallType);
            }

            if (CallType == "Order")
            {
                return _orderBusinessInstance.OrderFileImport(FileName, CallType);
            }

            return "Incorrect Call Type!";
        }

        [HttpPost]
        [Route("api/Company/Fleet")]
        public string SaveFleet(FleetModal Modal)
        {
            if (Modal.CallType == null)
            {
                Modal.CallType = "Portal";
            }

            try
            {
                if (_businessInstance.SaveFleet(Modal.Fleet, Modal.Documents,Modal.CallType))

                    return "Data Saved Successfully.";
            }
            catch (Exception ex)
            {
                   return ex.Message;
            }

            return "Oops";
        }

    }
}

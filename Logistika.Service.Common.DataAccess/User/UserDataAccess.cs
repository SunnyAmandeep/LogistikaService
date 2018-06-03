using Logistika.Service.Common.DataAccessInterface.User;
using Logistika.Service.Common.EFDataContext;
using Logistika.Service.Common.Entities;
using Logistika.Service.Common.Entities.Menu;
using Logistika.Service.Common.Extension;
using Logistika.Service.Common.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
namespace Logistika.Service.Common.DataAccess.User
{
    public class UserDataAccess : BaseDataAccess, IUserDataAccess
    {
        //public string LogFileImport(string FileName, string Status, string ValidRecords, string InValidRecords, string ErrorFileName, string ImportFileHistory_PK)
        //{
        //    var message = GetOutputParameter("OutputMessage", ParameterDirection.InputOutput);
        //    var ImportFileHistoryID = GetOutputParameter("ImportFileHistoryID", ParameterDirection.InputOutput);
        //    var ds = GetDataSetResult("proc_ins_upd_Import_File",
        //            new SqlParameter("CompanyID", ClaimHelper.CompanyId),
        //            new SqlParameter("FileName", FileName),
        //            new SqlParameter("Status", Status),
        //            new SqlParameter("ValidRecords", (string.IsNullOrEmpty(ValidRecords) ? DBNull.Value : (object)ValidRecords)),
        //            new SqlParameter("InValidRecords", (string.IsNullOrEmpty(InValidRecords) ? DBNull.Value : (object)InValidRecords)),
        //            new SqlParameter("ErrorFileName", (string.IsNullOrEmpty(ErrorFileName) ? DBNull.Value : (object)ErrorFileName)),
        //            new SqlParameter("ImportFileHistory_PK", (string.IsNullOrEmpty(ImportFileHistory_PK) ? DBNull.Value : (object)ImportFileHistory_PK)),
        //            new SqlParameter("LastModifiedBy", ClaimHelper.UserName),
        //            new SqlParameter("Operation", (string.IsNullOrEmpty(ImportFileHistory_PK) ? "I" : "U")),
        //            message,
        //            ImportFileHistoryID);
        //    if (string.IsNullOrEmpty(ImportFileHistoryID.Value.ToString()))
        //    {
        //        return message.Value.ToString();
        //    }
        //    return ImportFileHistoryID.Value.ToString();
        //}

        public IList<Country> GetCountryInfo(string Country)
        {
            return GetList<Country>("[dbo].[proc_get_CountryInfomation]",
                    new SqlParameter("Country", string.IsNullOrEmpty(Country) ? DBNull.Value :(object)Country));
        }

        public IList<string> GetUserRoles(string UserId)
        {
            return new string[] { "Role1", "Role12", "Role13" };
            IList<string> roles = null;
            Func<DbDataReader, IList<String>> func = (reader) =>
            {
                return (from IDataRecord x in reader select Convert.ToString(x[""])).ToList<string>();
            };

            roles = GetList1<string>(func, null, "[dbo].[]",
             new SqlParameter("UserId", UserId)
             , OutputParameter
            );
            return roles;

        }

        public ForgotUserPassword ForgotUserNamePassword(string UserName = null, string PrimaryPhone = null)
        {
            ForgotUserPassword FrameworkUser = new ForgotUserPassword();

            var ds = GetDataSetResult("dbo.proc_get_Forgot_UserName_Password",
                   new SqlParameter("UserName", UserName),
                   new SqlParameter("PrimaryPhone", PrimaryPhone));

            if (PrimaryPhone != null && PrimaryPhone.Length == 10)
            {
                if (ds.IsDataSetNotNullAndTableHasRows())
                {
                    FrameworkUser = (from row in ds.Tables[0].AsEnumerable()
                                     select new Entities.ForgotUserPassword
                                     {
                                         UserName = Convert.ToString(row["UserName"].CheckDBNull()),
                                         Password = Encryption.EncryptionManager.BasicDecrypt(Convert.ToString(row["Password"].CheckDBNull())),
                                         PrimaryPhone = Convert.ToString(row["PrimaryPhone"].CheckDBNull()),
                                         PrimaryEmail = Convert.ToString(row["PrimaryEmail"].CheckDBNull()),
                                         SecurityQuestion = Convert.ToString(row["Description"].CheckDBNull()),
                                         SecurityAnswer = Encryption.EncryptionManager.Encrypt(Convert.ToString(row["Answer"].CheckDBNull()), Convert.ToString(row["UserName"].CheckDBNull())),
                                     }).FirstOrDefault();

                    return FrameworkUser;
                }
            }
            return null;
        }

        // Check UserName and Company Availablity proc_Check_Availablity_UserName_Company
        public string checkAvailablity(string Type, string UserName, string Company, string TaxIdentification)
        {
            var message = GetOutputParameter("Msg", ParameterDirection.InputOutput);

            var ds = GetDataSetResult("dbo.proc_Check_Availablity_UserName_Company",
                   new SqlParameter("Type", Type),
                   new SqlParameter("UserName", UserName == "" ? DBNull.Value : (object)UserName),
                   new SqlParameter("Company", Company == "" ? DBNull.Value : (object)Company),
                   new SqlParameter("TaxIdentification", TaxIdentification == "" ? DBNull.Value : (object)TaxIdentification),
                   message);
            return message.Value.ToString();
        }
        
        // Framework User get_Method
        public IList<FrameworkUser> getUserList(string UserName)
        {
            return GetList<FrameworkUser>("[dbo].[proc_FrameworkUserList]",
                   new SqlParameter("UserName", !string.IsNullOrEmpty(UserName) ? (object)UserName : ClaimHelper.UserName)
                );
        }

        public bool UpdateUser(string UserName, bool IsActive)
        {
            return Exec("[dbo].[proc_AI_FrameworkUserList]",
                   new SqlParameter("UserName", UserName),
                   new SqlParameter("Operation", IsActive),
                   new SqlParameter("LastModifiedBy", ClaimHelper.UserName)
                );
        }
        //  get_Item List
        public IList<ItemList> getItemList(string Type)
        {
            return GetList<ItemList>("[dbo].[proc_get_ItemList]",
                   new SqlParameter("Type", Type)
                );
        }

        // Company Address List get_Method
        public IList<Address> getFrameAddress(int FramewordAddressID)
        {
            var ds = GetDataSetResult("[dbo].[proc_FrameworkAddressList]",
                   new SqlParameter("FrameworkAddressID", FramewordAddressID)
                );

            return BindAddress(ds);
        }

        public IList<Address> BindAddress(DataSet Data, int index = 0)
        {
            if (!Data.IsDataSetNotNullAndTableHasRows(index))
            {
                return (IList<Address>)null;
            }
            return Data.Tables[index].DataTableToCollectionType<Address>();
        }

        // Company Handoff Address List get_Method
        public IList<CompanyAddress> GetHandOffAddress(int? CompanyID = 0)
        {
            IList<CompanyAddress> HandOffAddressList = null;
            var ds = GetDataSetResult("dbo.proc_get_HandOffAddress",
                   new SqlParameter("CompanyID", ClaimHelper.CompanyId)
                );

            HandOffAddressList = (from row in ds.Tables[0].AsEnumerable()
                           select new Entities.CompanyAddress
                           {
                               LandMark = Convert.ToString(row["LandMark"].CheckDBNull()),
                               AddressLine1 = Convert.ToString(row["AddressLine1"].CheckDBNull()),
                               CompanyAddressID = Convert.ToInt32(row["CompanyAddressID"].CheckDBNull()),
                               Locality = Convert.ToString(row["Locality"].CheckDBNull()),
                               Suite = Convert.ToString(row["Suite"].CheckDBNull()),
                               City = Convert.ToString(row["City"].CheckDBNull()),
                               District = Convert.ToString(row["District"].CheckDBNull()),
                               StateCode = Convert.ToString(row["StateCode"].CheckDBNull()),
                               PostalCode = Convert.ToString(row["PostalCode"].CheckDBNull()),
                               CountryCode = Convert.ToString(row["CountryCode"].CheckDBNull())
                               
                           }).ToList();



            return HandOffAddressList;
        }

        // Company Address List get_Method
        public IList<CompanyAddress> getCompanyAddressList(int? CompanyID = 0,int? FrameworkAddressID = 0)
        {
            IList<CompanyAddress> AddressList = null;
            var ds = GetDataSetResult("[dbo].[proc_Get_Company_Address]",
                   new SqlParameter("CompanyID", ClaimHelper.CompanyId),
                   new SqlParameter("FrameworkAddressID", FrameworkAddressID),
                   new SqlParameter("UserName", ClaimHelper.UserName)
                );

            AddressList = (from row in ds.Tables[0].AsEnumerable()
                           select new Entities.CompanyAddress
                           {
                                CompanyAddressID = Convert.ToInt32(row["CompanyAddress_PK"].CheckDBNull()),
                                CompanyID = Convert.ToInt32(row["CompanyID"].CheckDBNull()),
                                CompanyName = Convert.ToString(row["CompanyName"].CheckDBNull()),
                                IsCompanyActive = Convert.ToBoolean(row["IsCompanyActive"].CheckDBNull()),
                                FrameworkAddressID = Convert.ToInt32(row["FrameworkAddressID"].CheckDBNull()),
                                LandMark = Convert.ToString(row["LandMark"].CheckDBNull()),
                                AddressLine1 = Convert.ToString(row["AddressLine1"].CheckDBNull()),
                                Locality = Convert.ToString(row["Locality"].CheckDBNull()),
                                Suite = Convert.ToString(row["Suite"].CheckDBNull()),
                                City = Convert.ToString(row["City"].CheckDBNull()),
                                District = Convert.ToString(row["District"].CheckDBNull()),
                                StateCode = Convert.ToString(row["StateCode"].CheckDBNull()),
                                PostalCode = Convert.ToString(row["PostalCode"].CheckDBNull()),
                                CountryCode = Convert.ToString(row["CountryCode"].CheckDBNull()),
                                IsCompanyAddressActive = Convert.ToBoolean(row["IsCompanyAddressActive"].CheckDBNull())

                           }).ToList();



            return AddressList;
        }

        // Fleet Master List get_Method
        public IList<Fleet> getFleetMaster(string Make, string Model)
        {
            IList<Fleet> FleetList = null;
            var ds = GetDataSetResult("[dbo].[proc_get_FleetMaster]",
                   new SqlParameter("Make", Make),
                   new SqlParameter("Model", Model)
                );

            FleetList = (from row in ds.Tables[0].AsEnumerable()
                         select new Entities.Fleet
                         {
                             FleetMasterID = Convert.ToInt32(row["FleetMaster_PK"].CheckDBNull()),
                             Make = Convert.ToString(row["Make"].CheckDBNull()),
                             Model = Convert.ToString(row["Model"].CheckDBNull()),
                             OverallLength = Convert.ToString(row["OverallLength"].CheckDBNull()),
                             OverallWidth = Convert.ToString(row["OverallWidth"].CheckDBNull()),
                             OverallHeight = Convert.ToString(row["OverallHeight"].CheckDBNull()),
                             Payload = Convert.ToString(row["Capacity"].CheckDBNull()),
                         }).ToList();
            return FleetList;
        }

        //Employee Planner
		public bool UpdateEmployeePlanner(EmployeePlanner EmpPlanner)
		{
            return Exec("[dbo].[proc_ins_upd_EmployeePlannerList]",
                    new SqlParameter("CompanyID", ClaimHelper.CompanyId),
                    new SqlParameter("CompanyAddressID", EmpPlanner.CompanyAddressID),
                    new SqlParameter("TotalDrivers", EmpPlanner.TotalDrivers),
                    new SqlParameter("Date", EmpPlanner.WeekDate.ToShortDateString()),
                    new SqlParameter("LastModifiedBy", ClaimHelper.UserName),
                    new SqlParameter("Operation", "U"));
		}
        //public IList<EmployeePlannerModal> GetEmployeePlannerList(int? CompanyID = 0, int? AddressId = 0)

        public IList<EmployeePlanner> GetEmployeePlannerList(int? CompanyID, int? CompanyAddressID = 0)
        {
            IList<EmployeePlanner> EmployeePlannerList = null;
            var ds = GetDataSetResult("[dbo].[proc_ins_upd_EmployeePlannerList]",
                   new SqlParameter("CompanyID", ClaimHelper.CompanyId),
                   new SqlParameter("CompanyAddressID", (!CompanyAddressID.HasValue || CompanyAddressID.Value == 0) ? DBNull.Value : (object)CompanyAddressID),
                   new SqlParameter("Operation", "G")

                );
            EmployeePlannerList = (from row in ds.Tables[0].AsEnumerable()
                                   select new Entities.EmployeePlanner
                                   {
                                       WeekDate = Convert.ToDateTime(row["WeekDate"].CheckDBNull()),
                                       TotalDrivers = Convert.ToInt32(row["TotalDrivers"].CheckDBNull()),
                                       PlanningStartDate = Convert.ToDateTime(row["PlanningStartDate"].CheckDBNull()),
                                       PlanningEndDate = Convert.ToDateTime(row["PlanningEndDate"].CheckDBNull()),
                                       CompanyAddressID = Convert.ToInt32(row["CompanyAddress_FK"].CheckDBNull()),
                                       CompanyAddress = "".NotNullThenAppend(" - ", Convert.ToString(row["LandMark"].CheckDBNull()))
                                       + "".NotNullThenAppend(", ", Convert.ToString(row["LandMark"].CheckDBNull())
                                       , Convert.ToString(row["AddressLine1"].CheckDBNull())
                                       , Convert.ToString(row["City"].CheckDBNull())
                                       , Convert.ToString(row["StateCode"].CheckDBNull())
                                       , Convert.ToString(row["PostalCode"].CheckDBNull()))
                                   }).ToList();
            return EmployeePlannerList;
        }

        // Company Add or Update
        public string AddUpdateCompany(string CompanyName, string TaxIdentification, int IsActive, string AuthCode)
        {
            // IList<Company> Company = null;
            var ds = GetDataSetResult("[dbo].[proc_ins_upd_Company]",
                   new SqlParameter("CompanyName", CompanyName),
                   new SqlParameter("TaxIdentification", TaxIdentification),
                   new SqlParameter("IsActive", IsActive),
                   new SqlParameter("AuthCode", AuthCode)
                );
            return CompanyName;
        }
       
        // Login User Authorization
        public void AuditLogin(string UserName)
        {
            Exec("[dbo].[proc_ins_AuditLogin]",
                   new SqlParameter("UserName", UserName)
                );
        }

        public Metrics GetMetricsList(DateTime StartDt, DateTime EndDt, int? CompanyAddressID = 0)
        {
            Metrics Metrics = null;
            var ds = GetDataSetResult("dbo.proc_get_CompanyStatistics",
                   new SqlParameter("CompanyID", ClaimHelper.CompanyId),
                   new SqlParameter("CompanyAddressID", CompanyAddressID == 0 ? DBNull.Value : (object)CompanyAddressID),
                   new SqlParameter("StartDt", StartDt),
                   new SqlParameter("EndDt", EndDt)
                );
            Metrics = new Metrics
            {
                DriverMetrics = ds.Tables[0].AsEnumerable().Select(row =>
                            new DriverMetrics()
                            {
                                Drivers = Convert.ToInt32(row["Drivers"].CheckDBNull()),
                                DelayedWaves = Convert.ToInt32(row["DelayedWaves"].CheckDBNull()),
                                WavesCompletedOnTime = Convert.ToInt32(row["WavesCompletedOnTime"].CheckDBNull()),
                                OfficeStaff = Convert.ToInt32(row["OfficeStaff"].CheckDBNull()),
                            }
                        ).FirstOrDefault(),

                RevenueExpenseMetrics = ds.Tables[1].AsEnumerable().Select(row =>
                            new RevenueExpenseMetrics()
                            {
                                TotalOrderRevenue = Convert.ToInt32(row["TotalOrderRevenue"].CheckDBNull()),
                                TotalDriverExpenditure = Convert.ToInt32(row["DriverExpenditure"].CheckDBNull()),
                                TotalShippingExpenditure = Convert.ToInt32(row["ShippingExpenditure"].CheckDBNull()),
                                OtherExpenditure = Convert.ToInt32(row["OtherExpenditure"].CheckDBNull()),
                                CashInFlow = Convert.ToInt32(row["CashInFlow"].CheckDBNull()),
                                CashOutFlow = Convert.ToInt32(row["CashOutFlow"].CheckDBNull()),
                            }
                        ).FirstOrDefault(),

                RevenueExpenseMetricsDate = ds.Tables[2].AsEnumerable().Select(row =>
                            new RevenueExpenseMetricsDate()
                            {
                                Date = Convert.ToDateTime(row["Date"].CheckDBNull()),
                                TotalOrderRevenue = Convert.ToInt32(row["TotalOrderRevenue"].CheckDBNull()),
                                TotalDriverExpenditure = Convert.ToInt32(row["DriverExpenditure"].CheckDBNull()),
                                TotalShippingExpenditure = Convert.ToInt32(row["ShippingExpenditure"].CheckDBNull()),
                                OtherExpenditure = Convert.ToInt32(row["OtherExpenditure"].CheckDBNull()),
                                CashInFlow = Convert.ToInt32(row["CashInFlow"].CheckDBNull()),
                                CashOutFlow = Convert.ToInt32(row["CashOutFlow"].CheckDBNull()),
                            }
                        ).ToList(),

                RevPaySubUserMetrics = ds.Tables[3].AsEnumerable().Select(row =>
                            new RevPaySubUserMetrics()
                            {
                                CashInFlow = Convert.ToInt32(row["CashInFlow"].CheckDBNull()),
                                CashOutFlow = Convert.ToInt32(row["CashOutFlow"].CheckDBNull()),
                                Credit = Convert.ToInt32(row["Credit"].CheckDBNull()),
                                Cash = Convert.ToInt32(row["Cash"].CheckDBNull()),
                                OnlinePayments = Convert.ToInt32(row["OnlinePayments"].CheckDBNull()),
                                NewSubscription = Convert.ToInt32(row["NewSubscription"].CheckDBNull()),
                                UpgradeSubscription = Convert.ToInt32(row["UpgradeSubscription"].CheckDBNull()),
                                CancelledSubscription = Convert.ToInt32(row["CancelledSubscription"].CheckDBNull()),
                                NewUsers = Convert.ToInt32(row["NewUsers"].CheckDBNull()),
                                InactiveUsers = Convert.ToInt32(row["InactiveUsers"].CheckDBNull()),
                            }
                        ).FirstOrDefault(),

                OperationalExpenseMetrics = ds.Tables[4].AsEnumerable().Select(row =>
                            new OperationalExpenseMetrics()
                            {
                                SalaryExpense = Convert.ToInt32(row["SalaryExpense"].CheckDBNull()),
                                AdministrationExpense = Convert.ToInt32(row["AdministrationExpense"].CheckDBNull()),
                                OtherExpense = Convert.ToInt32(row["MiscExpense"].CheckDBNull()),
                                TaxExpense = Convert.ToInt32(row["TaxExpense"].CheckDBNull()),
                            }
                        ).FirstOrDefault(),

                SolutionExpenseMetrics = ds.Tables[5].AsEnumerable().Select(row =>
                            new SolutionExpenseMetrics()
                            {
                                SalaryExpense = Convert.ToInt32(row["SalaryExpense"].CheckDBNull()),
                                AdministrationExpense = Convert.ToInt32(row["AdministrationExpense"].CheckDBNull()),
                                OtherExpense = Convert.ToInt32(row["MiscExpense"].CheckDBNull()),
                                TaxExpense = Convert.ToInt32(row["TaxExpense"].CheckDBNull()),
                            }
                        ).FirstOrDefault()
            };

            return Metrics;
        }

        public IList<SubscriptionPlans> GetSubscriptionPlans()
        {

            IList<SubscriptionPlans> SubscriptionPlanList = null;
            var ds = GetDataSetResult("dbo.proc_get_Subscriptionplans",
                   new SqlParameter("CompanyID", ClaimHelper.CompanyId));

            SubscriptionPlanList = (from row in ds.Tables[0].AsEnumerable()
                                       select new Entities.SubscriptionPlans
                                       {
                                           SubscriptionServiceID = Convert.ToInt32(row["SubscriptionServiceID"].CheckDBNull()),
                                           SubscriptionName = Convert.ToString(row["SubscriptionName"].CheckDBNull()),
                                           SubscriptionCode = Convert.ToString(row["SubscriptionCode"].CheckDBNull()),
                                           SubscriptionPrice = Convert.ToString(row["SubscriptionPrice"].CheckDBNull()),
                                           Frequency = Convert.ToString(row["Frequency"].CheckDBNull()),
                                           FleetLimit = Convert.ToInt32(row["FleetLimit"].CheckDBNull()),
                                           DriverLimit = Convert.ToInt32(row["DriverLimit"].CheckDBNull()),
                                           OrderLimit = Convert.ToInt32(row["OrderLimit"].CheckDBNull()),
                                           FeatureCode = Convert.ToString(row["FeatureCode"].CheckDBNull()),
                                           Description = Convert.ToString(row["Description"].CheckDBNull()),
                                           Value = Convert.ToString(row["Value"].CheckDBNull()),
                                           ApplicationCode = Convert.ToString(row["ApplicationCode"].CheckDBNull()),
                                           Sequence = Convert.ToInt32(row["Sequence"].CheckDBNull()),
                                           IsActive = Convert.ToBoolean(row["IsActive"].CheckDBNull())
                                       }).ToList();
            return SubscriptionPlanList;
        }

        public IList<Inquiry> GetInquiryList(int? UserCompanyID = 0)
        {
            IList<Inquiry> InquiryList = null;
            var ds = GetDataSetResult("dbo.proc_ins_upd_InquiryLog",
                   new SqlParameter("CompanyID", ClaimHelper.CompanyId),
                   new SqlParameter("Operation", "G")
                );
                InquiryList = (from row in ds.Tables[0].AsEnumerable()
                        select new Entities.Inquiry
                        {
                            InquiryID = Convert.ToInt32(row["InquiryID"].CheckDBNull()),
                            InquirySource = Convert.ToString(row["InquirySource"].CheckDBNull()),
                            ContactName = Convert.ToString(row["ContactName"].CheckDBNull()),
                            Phone = Convert.ToString(row["Phone"].CheckDBNull()),
                            Email = Convert.ToString(row["Email"].CheckDBNull()),
                            PreferredCallback = Convert.ToString(row["PreferredCallback"].CheckDBNull()),
                            Subject = Convert.ToString(row["Subject"].CheckDBNull()),
                            Message = Convert.ToString(row["Message"].CheckDBNull()),
                            SLA = Convert.ToString(row["SLA"].CheckDBNull()),
                            Resolution = Convert.ToString(row["Resolution"].CheckDBNull()),
                            ResponseDt = Convert.ToDateTime(row["ResponseDt"].CheckDBNull()),
                            Status = Convert.ToString(row["Status"].CheckDBNull()),
                            LastModifiedBy = Convert.ToString(row["LastModifiedBy"].CheckDBNull()),
                            LastModifiedDt = Convert.ToDateTime(row["LastModifiedDt"].CheckDBNull()),
                        }).ToList();
                return InquiryList;
        }

        public bool SaveInquiryInfo(Inquiry Inquiry)
        {
                return Exec("dbo.proc_ins_upd_InquiryLog",
                    new SqlParameter("CompanyID", ClaimHelper.CompanyId),
                    new SqlParameter("InquiryID", Inquiry.InquiryID == 0 ? DBNull.Value : (object)Inquiry.InquiryID),
                    new SqlParameter("InquirySource", Inquiry.InquirySource),
                    new SqlParameter("ContactName", Inquiry.ContactName),
                    new SqlParameter("Phone", string.IsNullOrEmpty(Inquiry.Phone) ? DBNull.Value : (object)Inquiry.Phone),
                    new SqlParameter("Email", Inquiry.Email),
                    new SqlParameter("PreferredCallback", string.IsNullOrEmpty(Inquiry.PreferredCallback) ? DBNull.Value : (object)Inquiry.PreferredCallback),
                    new SqlParameter("Subject", Inquiry.Subject),
                    new SqlParameter("Message", Inquiry.Message),
                    new SqlParameter("SLA", string.IsNullOrEmpty(Inquiry.SLA) ? DBNull.Value : (object)Inquiry.SLA),
                    new SqlParameter("Resolution", string.IsNullOrEmpty(Inquiry.Resolution) ? DBNull.Value : (object)Inquiry.Resolution),
                    new SqlParameter("ResponseDt", Inquiry.ResponseDt == null ? DBNull.Value : (object)Inquiry.ResponseDt),
                    new SqlParameter("Status", Inquiry.Status == null ? DBNull.Value : (object)Inquiry.Status),
                    new SqlParameter("LastModifiedBy", ClaimHelper.UserName),
                    new SqlParameter("Operation", Inquiry.InquiryID > 0 ? "U":"I"));
        }

        public IList<CompanySubscription> GetCompanySubscriptionList(int? UserCompanyID = 0)
        {
            IList<CompanySubscription> CompanySubscriptionList = null;
            var ds = GetDataSetResult("dbo.proc_ins_upd_CompanyInformationList",
                   new SqlParameter("CompanyID", ClaimHelper.CompanyId),
                   new SqlParameter("UserCompanyID", (UserCompanyID.HasValue && UserCompanyID.Value > 0) ? (object)UserCompanyID : DBNull.Value),//UserName == "" ? (object)UserName : DBNull.Value),
                   new SqlParameter("Operation", "G")
                );
            if (UserCompanyID.HasValue && UserCompanyID.Value > 0)
            {
                CompanySubscriptionList = (from row in ds.Tables[0].AsEnumerable()
                                           select new Entities.CompanySubscription
                                           {
                                               Company = new Company
                                               {
                                                   CompanyName = Convert.ToString(row["CompanyName"].CheckDBNull()),
                                                   TaxIdentification = Convert.ToString(row["TaxIdentification"].CheckDBNull()),
                                                   IsActive = Convert.ToBoolean(row["IsActive"].CheckDBNull())
                                               },
                                               SubscriptionServiceID = Convert.ToInt32(row["SubscriptionServiceID"].CheckDBNull()),
                                               SubscriptionName = Convert.ToString(row["SubscriptionName"].CheckDBNull()),
                                               SubscriptionCode = Convert.ToString(row["SubscriptionCode"].CheckDBNull()),
                                               SubscriptionPrice = Convert.ToString(row["SubscriptionPrice"].CheckDBNull()),
                                               Frequency = Convert.ToString(row["Frequency"].CheckDBNull()),
                                               FleetLimit = Convert.ToInt32(row["FleetLimit"].CheckDBNull()),
                                               DriverLimit = Convert.ToInt32(row["DriverLimit"].CheckDBNull()),
                                               OrderLimit = Convert.ToInt32(row["OrderLimit"].CheckDBNull()),
                                               //FeatureCode = Convert.ToString(row["FeatureCode"].CheckDBNull()),
                                               //Description = Convert.ToString(row["Description"].CheckDBNull()),
                                               ////Value = Convert.ToString(row["Value"].CheckDBNull()),
                                               //ApplicationCode = Convert.ToString(row["ApplicationCode"].CheckDBNull()),
                                               //Sequence = Convert.ToInt32(row["Sequence"].CheckDBNull()),
                                               //SubscriptionIsActive = Convert.ToBoolean(row["SubscriptionIsActive"].CheckDBNull()),
                                               CompanyAddress = new CompanyAddress
                                               {
                                                   AddressLine1 = Convert.ToString(row["AddressLine1"].CheckDBNull()),
                                                   LandMark = Convert.ToString(row["LandMark"].CheckDBNull()),
                                                   Locality = Convert.ToString(row["Locality"].CheckDBNull()),
                                                   Suite = Convert.ToString(row["Suite"].CheckDBNull()),
                                                   City = Convert.ToString(row["City"].CheckDBNull()),
                                                   District = Convert.ToString(row["District"].CheckDBNull()),
                                                   StateCode = Convert.ToString(row["StateCode"].CheckDBNull()),
                                                   PostalCode = Convert.ToString(row["PostalCode"].CheckDBNull()),
                                                   CountryCode = Convert.ToString(row["CountryCode"].CheckDBNull()),
                                               }
                                           }).ToList();
                return CompanySubscriptionList;
            }
            else
            {
                CompanySubscriptionList = (from row in ds.Tables[0].AsEnumerable()
                                           select new Entities.CompanySubscription
                                           {
                                               Company = new Company
                                               {
                                                   CompanyName = Convert.ToString(row["CompanyName"].CheckDBNull()),
                                                   CompanyID = Convert.ToInt32(row["CompanyID"].CheckDBNull()),
                                                   TaxIdentification = Convert.ToString(row["TaxIdentification"].CheckDBNull()),
                                                   MemberSince = Convert.ToString(row["MemberSince"].CheckDBNull()),
                                                   SubscriptionName = Convert.ToString(row["SubscriptionName"].CheckDBNull()),
                                                   Revenue = Convert.ToString(row["Revenue"].CheckDBNull()),
                                                   IsActive = Convert.ToBoolean(row["IsActive"].CheckDBNull())
                                               },
                                           }).ToList();
                return CompanySubscriptionList;
            }
        }

        public bool SaveCompanySubscriptionInfo(CompanySubscription CompanySubscription)
        {
            //return true;
            return Exec("dbo.proc_ins_upd_CompanyInformationList",
                new SqlParameter("CompanyID", ClaimHelper.CompanyId),
                new SqlParameter("UserCompanyID", CompanySubscription.UserCompanyID == 0 ? DBNull.Value : (object)CompanySubscription.UserCompanyID),

                new SqlParameter("SubscriptionCode", CompanySubscription.SubscriptionCode == null ? DBNull.Value : (object)CompanySubscription.SubscriptionCode),
                new SqlParameter("DriverLimit", CompanySubscription.DriverLimit == 0 ? DBNull.Value : (object)CompanySubscription.DriverLimit),
                new SqlParameter("FleetLimit", CompanySubscription.FleetLimit == 0 ? DBNull.Value : (object)CompanySubscription.FleetLimit),
                new SqlParameter("OrderLimit", CompanySubscription.OrderLimit == 0 ? DBNull.Value : (object)CompanySubscription.OrderLimit),

                new SqlParameter("AddressLine1", CompanySubscription.CompanyAddress == null ? DBNull.Value : (object)CompanySubscription.CompanyAddress.AddressLine1),
                new SqlParameter("SuiteNo", CompanySubscription.CompanyAddress == null ? DBNull.Value : (object)CompanySubscription.CompanyAddress.Suite),
                new SqlParameter("LandMark", CompanySubscription.CompanyAddress == null ? DBNull.Value : (object)CompanySubscription.CompanyAddress.LandMark),
                new SqlParameter("Locality", CompanySubscription.CompanyAddress == null ? DBNull.Value : (object)CompanySubscription.CompanyAddress.Locality),
                new SqlParameter("District", CompanySubscription.CompanyAddress == null ? DBNull.Value : (object)CompanySubscription.CompanyAddress.District),
                new SqlParameter("City", CompanySubscription.CompanyAddress == null ? DBNull.Value : (object)CompanySubscription.CompanyAddress.City),
                new SqlParameter("State", CompanySubscription.CompanyAddress == null ? DBNull.Value : (object)CompanySubscription.CompanyAddress.StateCode),
                new SqlParameter("PinCode", CompanySubscription.CompanyAddress == null ? DBNull.Value : (object)CompanySubscription.CompanyAddress.PostalCode),
                new SqlParameter("CountryCode", CompanySubscription.CompanyAddress == null ? DBNull.Value : (object)CompanySubscription.CompanyAddress.CountryCode),
                
                new SqlParameter("CompanyIsActive", CompanySubscription.Company == null ? DBNull.Value : (object)CompanySubscription.Company.IsActive),
                
                new SqlParameter("LastModifiedBy", ClaimHelper.UserName),
                new SqlParameter("Operation", CompanySubscription.UserCompanyID > 0 ? "U" : "U"));
        }

        public IList<ExpenseLog> GetExpenseLogList()
        {
            IList<ExpenseLog> ExpenseLogList = null;
            var ds = GetDataSetResult("dbo.proc_ins_upd_ExpenseLog",
                   new SqlParameter("CompanyID", ClaimHelper.CompanyId),
                   //new SqlParameter("CompanyAddressID", CompanyAddressID),
                   new SqlParameter("LastModifiedBy", ClaimHelper.UserName),
                   new SqlParameter("Operation", "G")
                );
            {
                ExpenseLogList = (from row in ds.Tables[0].AsEnumerable()
                                  select new Entities.ExpenseLog
                                  {
                                      CompanyAddressID = Convert.ToInt32(row["CompanyAddressID"].CheckDBNull()),
                                      TransactionDt = Convert.ToDateTime(row["TransactionDt"].CheckDBNull()),
                                      TransactionType = Convert.ToString(row["TransactionType"].CheckDBNull()),
                                      Purpose = Convert.ToString(row["Purpose"].CheckDBNull()),
                                      Name = Convert.ToString(row["Name"].CheckDBNull()),
                                      Amount = Convert.ToDecimal(row["Amount"].CheckDBNull()),
                                      IsApproved = Convert.ToBoolean(row["IsApproved"].CheckDBNull()),
                                      ApprovedBy = Convert.ToString(row["ApprovedBy"].CheckDBNull()),
                                      ApprovalDate = Convert.ToDateTime(row["ApprovalDate"].CheckDBNull()),
                                      Comment = Convert.ToString(row["Comment"].CheckDBNull())
                                  }).ToList();
                return ExpenseLogList;
            }
        }

        public bool SaveExpenseLog(ExpenseLog ExpenseLog)
        {
            //return true;
            return Exec("dbo.proc_ins_upd_ExpenseLog",
                new SqlParameter("CompanyID", ClaimHelper.CompanyId),
                new SqlParameter("CompanyAddressID", ExpenseLog.CompanyAddressID),
                new SqlParameter("TransactionDt", ExpenseLog.TransactionDt),
                new SqlParameter("TransactionType",ExpenseLog.TransactionType),
                new SqlParameter("Purpose", ExpenseLog.Purpose),
                new SqlParameter("Name", ExpenseLog.Name),
                new SqlParameter("Amount", ExpenseLog.Amount),
                new SqlParameter("IsApproved", ExpenseLog.IsApproved),
                new SqlParameter("ApprovedBy", ExpenseLog.ApprovedBy),
                new SqlParameter("ApprovalDate", ExpenseLog.ApprovalDate),
                new SqlParameter("Comment", ExpenseLog.Comment),
                new SqlParameter("LastModifiedBy", ClaimHelper.UserName),
                new SqlParameter("Operation", "I"));
        }

        public IList<CompanyAddress> GetCompanyList(int? CompanyAddressID = 0)
        {
            IList<CompanyAddress> CompanyList = null;
            var ds = GetDataSetResult("dbo.proc_ins_upd_CompanyBranchAddress",
                   new SqlParameter("CompanyID", ClaimHelper.CompanyId),
                   new SqlParameter("CompanyAddressID", (CompanyAddressID.HasValue && CompanyAddressID.Value > 0) ? (object)CompanyAddressID : DBNull.Value),//UserName == "" ? (object)UserName : DBNull.Value),
                   new SqlParameter("LastModifiedBy", ClaimHelper.UserName),
                   new SqlParameter("Operation", "G")
                );
            //if (CompanyAddressID.HasValue && CompanyAddressID.Value > 0)
            {
                CompanyList = (from row in ds.Tables[0].AsEnumerable()
                               select new Entities.CompanyAddress
                               {
                                    CompanyAddressID = Convert.ToInt32(row["CompanyAddressID"].CheckDBNull()),
                                    FrameworkAddressID = Convert.ToInt32(row["FrameworkAddressID"].CheckDBNull()),
                                    AddressLine1 = Convert.ToString(row["AddressLine1"].CheckDBNull()),
                                    LandMark = Convert.ToString(row["LandMark"].CheckDBNull()),
                                    Locality = Convert.ToString(row["Locality"].CheckDBNull()),
                                    Suite = Convert.ToString(row["Suite"].CheckDBNull()),
                                    City = Convert.ToString(row["City"].CheckDBNull()),
                                    District = Convert.ToString(row["District"].CheckDBNull()),
                                    StateCode = Convert.ToString(row["StateCode"].CheckDBNull()),
                                    PostalCode = Convert.ToString(row["PostalCode"].CheckDBNull()),
                                    CountryCode = Convert.ToString(row["CountryCode"].CheckDBNull()),
                                    AddressType = Convert.ToString(row["AddressType"].CheckDBNull()),
                                    PhoneNumber = Convert.ToString(row["PhoneNumber"].CheckDBNull()),
                                    IsCompanyActive = Convert.ToBoolean(row["IsCompanyActive"].CheckDBNull()),
                               }).ToList();
                return CompanyList;
            }
            //return CompanyList; 
        }

        public bool SaveCompanyAddressInfo (CompanyAddress companyAddress)
        {
            return Exec("dbo.proc_ins_upd_CompanyBranchAddress",
                new SqlParameter("CompanyID",ClaimHelper.CompanyId),
                new SqlParameter("CompanyAddressID", companyAddress.CompanyAddressID),
                new SqlParameter("Phone", companyAddress.PhoneNumber),
                new SqlParameter("AddressLine1", companyAddress.AddressLine1),
                new SqlParameter("SuiteNo", companyAddress.Suite),
                new SqlParameter("LandMark", companyAddress.LandMark),
                new SqlParameter("Locality", companyAddress.Locality),
                new SqlParameter("District", companyAddress.District),
                new SqlParameter("City", companyAddress.City),
                new SqlParameter("State", companyAddress.StateCode),
                new SqlParameter("PinCode", companyAddress.PostalCode),
                new SqlParameter("CountryCode", companyAddress.CountryCode),
                new SqlParameter("AddressType", companyAddress.AddressType),
                new SqlParameter("Latitude", companyAddress.Latitude),
                new SqlParameter("Longitude", companyAddress.Longitude),
                new SqlParameter("IsActive", companyAddress.IsCompanyActive),
                new SqlParameter("LastModifiedBy", ClaimHelper.UserName),
                new SqlParameter("Operation", "U"));
        }

        public IList<Customer> GetCustomerList(string UserName)
        {
            IList<Customer> CustomerList = null;
            //IList<Address> CustomerAddress = null;
            //int FrameworkAddressID = 0;
            var ds = GetDataSetResult("dbo.proc_ins_upd_CustomerInformationList",
                   new SqlParameter("CompanyID", ClaimHelper.CompanyId),
                   new SqlParameter("UserName", !string.IsNullOrEmpty(UserName) ? (object)UserName : DBNull.Value),//UserName == "" ? (object)UserName : DBNull.Value),
                   new SqlParameter("Operation", "G")
                );

            if (UserName.IsEmptyNullOrDefault())
            {
                CustomerList = (from row in ds.Tables[0].AsEnumerable()
                                select new Entities.Customer
                                {
                                    ApplicationCode = Convert.ToString(row["ApplicationCode"].CheckDBNull()),
                                    FrameworkApplicationUserID = Convert.ToInt32(row["FrameworkApplicationUserID"].CheckDBNull()),
                                    UserName = Convert.ToString(row["UserName"].CheckDBNull()),
                                    Password = Encryption.EncryptionManager.BasicDecrypt(Convert.ToString(row["Password"].CheckDBNull())),//Convert.ToString(row["Password"].CheckDBNull()),
                                    FirstName = Convert.ToString(row["FirstName"].CheckDBNull()),
                                    MiddleName = Convert.ToString(row["MiddleName"].CheckDBNull()),
                                    LastName = Convert.ToString(row["LastName"].CheckDBNull()),
                                    PrimaryEmail = Convert.ToString(row["PrimaryEmail"].CheckDBNull()),
                                    PrimaryPhone = Convert.ToString(row["PrimaryPhone"].CheckDBNull()),
                                    PrimaryFax = Convert.ToString(row["PrimaryFax"].CheckDBNull()),
                                    LastLoginDt = Convert.ToString(row["LastLoginDt"].CheckDBNull()),
                                    SecurityQuestion = Convert.ToString(row["SecurityQuestion"].CheckDBNull()),
                                    SecurityAnswer = Convert.ToString(row["SecurityAnswer"].CheckDBNull()),
                                    LastModifiedBy = Convert.ToString(row["LastModifiedBy"].CheckDBNull()),
                                    LastModifiedDt = Convert.ToString(row["LastModifiedDt"].CheckDBNull()),
                                    FrameworkAddressID = Convert.ToString(row["FrameworkAddressID"].CheckDBNull()),
                                    AddressType = Convert.ToString(row["AddressType"].CheckDBNull()),
                                    IsActive = Convert.ToBoolean(row["IsActive"].CheckDBNull()),
                                    CompanyID = Convert.ToInt32(row["CompanyID"].CheckDBNull()),
                                    CompanyName = Convert.ToString(row["CompanyName"].CheckDBNull()),
                                    TaxIdentification = Convert.ToString(row["TaxIdentification"].CheckDBNull()),
                                    IsCompanyActive = Convert.ToBoolean(row["IsCompanyActive"].CheckDBNull()),
                                    Sales = Convert.ToString(row["Sales"].CheckDBNull()),
                                    OrderCount = Convert.ToString(row["OrderCount"].CheckDBNull()),
                                    MemberSince = Convert.ToDateTime(row["MemberSince"].CheckDBNull()),
                                }).ToList();
                return CustomerList;
            }
            else 
            {
                CustomerList = (from row in ds.Tables[0].AsEnumerable()
                                select new Entities.Customer
                                {
                                    ApplicationCode = Convert.ToString(row["ApplicationCode"].CheckDBNull()),
                                    FrameworkApplicationUserID = Convert.ToInt32(row["FrameworkApplicationUserID"].CheckDBNull()),
                                    UserName = Convert.ToString(row["UserName"].CheckDBNull()),
                                    Password = Encryption.EncryptionManager.BasicDecrypt(Convert.ToString(row["Password"].CheckDBNull())),
                                    //Convert.ToString(row["Password"].CheckDBNull()),
                                    FirstName = Convert.ToString(row["FirstName"].CheckDBNull()),
                                    MiddleName = Convert.ToString(row["MiddleName"].CheckDBNull()),
                                    LastName = Convert.ToString(row["LastName"].CheckDBNull()),
                                    PrimaryEmail = Convert.ToString(row["PrimaryEmail"].CheckDBNull()),
                                    PrimaryPhone = Convert.ToString(row["PrimaryPhone"].CheckDBNull()),
                                    PrimaryFax = Convert.ToString(row["PrimaryFax"].CheckDBNull()),
                                    LastLoginDt = Convert.ToString(row["LastLoginDt"].CheckDBNull()),
                                    SecurityQuestion = Convert.ToString(row["SecurityQuestion"].CheckDBNull()),
                                    SecurityAnswer = Convert.ToString(row["SecurityAnswer"].CheckDBNull()),
                                    LastModifiedBy = Convert.ToString(row["LastModifiedBy"].CheckDBNull()),
                                    LastModifiedDt = Convert.ToString(row["LastModifiedDt"].CheckDBNull()),
                                    //FrameworkAddressID = Convert.ToString(row["FrameworkAddressID"].CheckDBNull()),
                                    CustomerAddress = new Address()
                                    {
                                        AddressLine1 = Convert.ToString(row["AddressLine1"].CheckDBNull()),
                                        Locality = Convert.ToString(row["Locality"].CheckDBNull()),
                                        Suite = Convert.ToString(row["Suite"].CheckDBNull()),
                                        LandMark = Convert.ToString(row["LandMark"].CheckDBNull()),
                                        District = Convert.ToString(row["District"].CheckDBNull()),
                                        City = Convert.ToString(row["City"].CheckDBNull()),
                                        PostalCode = Convert.ToString(row["PostalCode"].CheckDBNull()),
                                        StateCode = Convert.ToString(row["StateCode"].CheckDBNull()),
                                        CountryCode = Convert.ToString(row["CountryCode"].CheckDBNull()),
                                        FrameworkAddressID = Convert.ToInt32(row["FrameworkAddressID"].CheckDBNull()),
                                    },
                                    AddressType = Convert.ToString(row["AddressType"].CheckDBNull()),
                                    IsActive = Convert.ToBoolean(row["IsActive"].CheckDBNull()),
                                    CompanyID = Convert.ToInt32(row["CompanyID"].CheckDBNull()),
                                    CompanyName = Convert.ToString(row["CompanyName"].CheckDBNull()),
                                    TaxIdentification = Convert.ToString(row["TaxIdentification"].CheckDBNull()),
                                    IsCompanyActive = Convert.ToBoolean(row["IsCompanyActive"].CheckDBNull()),
                                    Sales = Convert.ToString(row["Sales"].CheckDBNull()),
                                    OrderCount = Convert.ToString(row["OrderCount"].CheckDBNull()),
                                    MemberSince = Convert.ToDateTime(row["MemberSince"].CheckDBNull()),
                                }).ToList();
                return CustomerList;
            }
        }

        public bool SaveCustomer(Customer customer)
        {

            return Exec("proc_ins_upd_CustomerInformationList",
                 new SqlParameter("CompanyID", ClaimHelper.CompanyId),
                 new SqlParameter("UserName", customer.UserName),
                 new SqlParameter("Password", Encryption.EncryptionManager.BasicEncrypt(customer.Password)),
                 //customer.Password),
                 new SqlParameter("FirstName", customer.FirstName),
                 new SqlParameter("LastName", customer.LastName),
                 new SqlParameter("Phone", customer.PrimaryPhone),
                 new SqlParameter("SecurityQuestion1", customer.SecurityQuestion),
                 new SqlParameter("SecurityAnswer1", customer.SecurityAnswer),
                 new SqlParameter("AddressLine1", customer.CustomerAddress.AddressLine1),
                 new SqlParameter("SuiteNo", customer.CustomerAddress.Suite),
                 new SqlParameter("LandMark", customer.CustomerAddress.LandMark),
                 new SqlParameter("City", customer.CustomerAddress.City),
                 new SqlParameter("State", customer.CustomerAddress.StateCode),
                 new SqlParameter("PinCode", customer.CustomerAddress.PostalCode),
                 new SqlParameter("CountryCode", customer.CustomerAddress.CountryCode),
                 new SqlParameter("IsActive", customer.IsActive),
                 new SqlParameter("LastModifiedBy", ClaimHelper.UserName),
                 new SqlParameter("Operation", "U"));
        }

        public LogistikaUserModal GetsUsers(int? Id = 0, int? AddressId = 0)
        {
            LogistikaUserModal user = null;
            var ds = GetDataSetResult("proc_ins_upd_EmployeeInformationList",
                 new SqlParameter("CompanyID", ClaimHelper.CompanyId),
                 new SqlParameter("CompanyAddressID", (AddressId.HasValue && AddressId.Value > 0) ? (object)AddressId.Value : DBNull.Value),
                 new SqlParameter("UserID", (Id.HasValue && Id.Value > 0) ? (object)Id.Value : DBNull.Value),
                 new SqlParameter("Operation", "G")
                );
            if (Id.HasValue && Id.Value > 0)
            {
                if (ds.IsDataSetNotNullAndTableHasRows())
                {
                    user = new LogistikaUserModal();
                    user.User = ds.Tables[0].AsEnumerable().Select(row =>
                        new LogistikaUser()
                        {
                            //   Title StartDate EndDate Branch IsActive
                            UserId = Convert.ToInt32(row["ApplicationUserID"].CheckDBNull()),
                            UserName = Convert.ToString(row["UserName"].CheckDBNull()),
                            LastName = Convert.ToString(row["LastName"].CheckDBNull()),
                            Password = Encryption.EncryptionManager.BasicDecrypt(Convert.ToString(row["Password"].CheckDBNull())),
                            //Convert.ToString(row["Password"].CheckDBNull()),
                            FirstName = Convert.ToString(row["FirstName"].CheckDBNull()),
                            MiddleName = Convert.ToString(row["MiddleName"].CheckDBNull()),
                            DateOfBirth = Convert.ToDateTime(row["DateofBirth"].CheckDBNull()),
                            StartDate = Convert.ToDateTime(row["StartDate"].CheckDBNull()),
                            EndDate = Convert.ToDateTime(row["EndDate"].CheckDBNull()),
                            BranchId = Convert.ToString(row["CompanyAddressID"].CheckDBNull()),
                            Designation = Convert.ToString(row["EmployeeRole"].CheckDBNull()),
                            JobTitle = Convert.ToString(row["EmployeeTitle"].CheckDBNull()),
                            Photo = Convert.ToString(row["ProfileImage"].CheckDBNull()),
                            Contact = new Contact()
                            {
                                Email = Convert.ToString(row["PrimaryEmail"].CheckDBNull()),
                                Phone = Convert.ToString(row["Phone"].CheckDBNull()),
                                Mobile = Convert.ToString(row["MobileNo"].CheckDBNull())
                            },
                            Addresses = new List<Address>(){ new Address()
                            {
                                AddressLine1 = Convert.ToString(row["AddressLine1"].CheckDBNull()),
                                StateCode = Convert.ToString(row["State"].CheckDBNull()),
                                CountryCode = Convert.ToString(row["CountryCode"].CheckDBNull()),
                                LandMark = Convert.ToString(row["LandMark"].CheckDBNull()),
                                City = Convert.ToString(row["City"].CheckDBNull()),
                                PostalCode = Convert.ToString(row["PinCode"].CheckDBNull()),
                                Suite = Convert.ToString(row["SuiteNo"].CheckDBNull()),
                            }},
                            OtherInfo = row.FromRowToListObject("LicenseNumber", "LicenseClass", "Experience", "MonthlyWage"),
                            Gender = Convert.ToString(row["Gender"].CheckDBNull()),
                            ModifiedBy = Convert.ToString(row["LastModifiedBy"].CheckDBNull()),
                            Active = Convert.ToBoolean(row["IsActive"].CheckDBNull())


                        }
                        ).FirstOrDefault();

                    user.Documents = ds.Tables[1].AsEnumerable().Select(row =>
                        new Document()
                        {
                            //   Title StartDate EndDate Branch IsActive
                            DocumentUrl = Convert.ToString(row["DocumentURL"].CheckDBNull()),
                            DocumentName = Convert.ToString(row["FileName"].CheckDBNull()),
                            DocumentType = Convert.ToString(row["DocumentType"].CheckDBNull()),
                            ExpirationDate = Convert.ToDateTime(row["ExpirationDate"].CheckDBNull()),
                            DocumentNumber = Convert.ToString(row["DocumentNumber"].CheckDBNull()),
                            IssuedState = Convert.ToString(row["IssuedState"].CheckDBNull()),
                            IsActive = Convert.ToBoolean(row["IsActive"].CheckDBNull()),
                            IssuedDate = Convert.ToDateTime(row["IssuedDate"].CheckDBNull()),
                            CreatedDt = Convert.ToString(row["LastModifiedBy"].CheckDBNull())
                        }
                        ).ToList();
                }
            }
            else
            {
                if (ds.IsDataSetNotNullAndTableHasRows())
                {

                    user = new LogistikaUserModal();
                    user.Users = ds.Tables[0].AsEnumerable().Select(row =>
                        new LogistikaUser()
                        {
                            //   Title StartDate EndDate Branch IsActive
                            UserId = Convert.ToInt32(row["ApplicationUserID"].CheckDBNull()),
                            UserName = Convert.ToString(row["UserName"].CheckDBNull()),
                            LastName = Convert.ToString(row["LastName"].CheckDBNull()),
                            FirstName = Convert.ToString(row["FirstName"].CheckDBNull()),
                            DateOfBirth = Convert.ToDateTime(row["DOB"].CheckDBNull()),
                            JobTitle = Convert.ToString(row["Title"].CheckDBNull()),
                            StartDate = Convert.ToDateTime(row["StartDate"].CheckDBNull()),
                            EndDate = Convert.ToDateTime(row["EndDate"].CheckDBNull()),
                            Branch = Convert.ToString(row["Branch"].CheckDBNull()),
                            //BranchId = Convert.ToInt32(row["Branch"].CheckDBNull()),
                            Active = Convert.ToBoolean(row["IsActive"].CheckDBNull())
                        }
                        ).ToList();
                }

            }
            return user;
        }

        public bool SaveUser(LogistikaUser User, string DocumentXml)
        {
            var OutputMessage = GetOutputParameter("RtnMsg", ParameterDirection.InputOutput);
            var success = Exec("proc_ins_upd_EmployeeInformationList",
                     new SqlParameter("CompanyID", ClaimHelper.CompanyId),//ClaimHelper.CompanyId), //ClaimHelper.CompanyId),
                     new SqlParameter("CompanyAddressID", User.BranchId),
                     new SqlParameter("UserID",User.UserId),
                     new SqlParameter("EmployeeRole", User.Designation),
                     new SqlParameter("ProfileImage", User.Photo),//Work on photo
                     new SqlParameter("UserName", User.Contact.Email),
                     new SqlParameter("Password", Encryption.EncryptionManager.BasicEncrypt(User.Password)),
                     new SqlParameter("FirstName", User.FirstName),
                     new SqlParameter("MiddleName", User.MiddleName),
                     new SqlParameter("LastName", User.LastName),
                     new SqlParameter("PrimaryEmail", User.Contact.Email),
                     new SqlParameter("Phone", User.Contact.Phone),
                     new SqlParameter("MobileNo", User.Contact.Mobile),
                     new SqlParameter("DateofBirth", User.DateOfBirth),
                     new SqlParameter("LicenseNumber", User.OtherInfo.TryGet("LicenseNumber")),
                     new SqlParameter("LicenseClass", User.OtherInfo.TryGet("LicenseClass")),
                     new SqlParameter("Experience", User.OtherInfo.TryGet("Experience")),
                     new SqlParameter("MonthlyWage", User.OtherInfo.TryGet("MonthlyWage")),
                     new SqlParameter("EmployeeTitle", User.JobTitle),
                     new SqlParameter("Gender", User.Gender),
                     new SqlParameter("StartDt", User.StartDate),
                     new SqlParameter("EndDt", User.EndDate),
                     new SqlParameter("AddressLine1", User.Addresses[0].AddressLine1),
                     new SqlParameter("SuiteNo", User.Addresses[0].Suite),
                     new SqlParameter("LandMark", User.Addresses[0].LandMark),
                     new SqlParameter("City", User.Addresses[0].City),
                     new SqlParameter("State", User.Addresses[0].StateCode),
                     new SqlParameter("PinCode", User.Addresses[0].PostalCode),
                     new SqlParameter("CountryCode", User.Addresses[0].CountryCode),
                     new SqlParameter("IsActive", User.Active),
                     new SqlParameter("DocumentXML", DocumentXml),
                     new SqlParameter("LastModifiedBy", ClaimHelper.UserName),
                     new SqlParameter("Operation", User.UserId > 0 ? "U" : "I"),
                     OutputMessage);

            if (string.IsNullOrEmpty(OutputMessage.Value.ToString()))
            {
                return true;
            }
            return false;
        }

        // Framework User success update
        public string SaveFrameworkUser(FrameworkUser User)
        {

            var addressId = GetOutputParameter("FrameworkAddressID", ParameterDirection.InputOutput);

            if (User.MyAddressList != null)
            {
                DataSet ds = GetDataSetResult("dbo.proc_ins_FrameworkAddress",
                            new SqlParameter("LandMark", User.MyAddressList.FirstOrDefault().LandMark),
                            new SqlParameter("AddressLine1", User.MyAddressList.FirstOrDefault().AddressLine1),
                            new SqlParameter("Locality", User.MyAddressList.FirstOrDefault().Locality),
                            new SqlParameter("Suite", User.MyAddressList.FirstOrDefault().Suite),
                            new SqlParameter("City", User.MyAddressList.FirstOrDefault().City),
                            new SqlParameter("District", User.MyAddressList.FirstOrDefault().District),
                            new SqlParameter("StateCode", User.MyAddressList.FirstOrDefault().StateCode),
                            new SqlParameter("PostalCode", User.MyAddressList.FirstOrDefault().PostalCode),
                            new SqlParameter("CountryCode", User.MyAddressList.FirstOrDefault().CountryCode),
                            addressId);
            }

            var success = Exec("proc_ins_upd_FrameworkUserList",
                          new SqlParameter("UserName", User.UserName),
                          new SqlParameter("Password", Encryption.EncryptionManager.BasicEncrypt(User.Password)), //User.Password),
                          new SqlParameter("FirstName", User.FirstName),
                          new SqlParameter("MiddleName", User.MiddleName),
                          new SqlParameter("LastName", User.LastName),
                          new SqlParameter("PrimaryEmail", User.PrimaryEmail),
                          new SqlParameter("PrimaryPhone", User.PrimaryPhone),
                          new SqlParameter("PrimaryFax", User.PrimaryFax),
                          new SqlParameter("SessionID", User.SessionID == null ? DBNull.Value : (object)User.SessionID),
                          new SqlParameter("FrameworkApplicationUserType_FK", User.FrameworkApplicationUserType_FK == 0 ? DBNull.Value : (object)User.FrameworkApplicationUserType_FK),
                          new SqlParameter("SecurityQuestion1", User.SecurityQuestion),
                          new SqlParameter("SecurityAnswer1", User.SecurityAnswer),
                          new SqlParameter("CompanyName", User.Company == null ? DBNull.Value : (object)User.Company.CompanyName),
                          new SqlParameter("TaxIdentification", User.Company == null ? DBNull.Value : (object)User.Company.TaxIdentification),
                          new SqlParameter("SubscriptionCode", User.Company == null ? DBNull.Value : (object)User.Company.SubscriptionCode),
                          new SqlParameter("SubscriptionAmount", User.Company == null ? DBNull.Value : (object)User.Company.SubscriptionAmount),
                          new SqlParameter("SubscriptionFrequency", User.Company == null ? DBNull.Value : (object)User.Company.SubscriptionFrequency),
                          new SqlParameter("Company_FK", ClaimHelper.CompanyId),
                          new SqlParameter("FrameworkAddressID", addressId.Value),
                          new SqlParameter("LastModifiedBy", User.UserName));
            return User.UserName; //"Successfully Updated"; //Convert.ToString(retMsg.Value);FrameworkApplicationUserType_FK
        }

        // User Stats get_Method
        public IList<UserStats> getUserStats(int CompanyID, string UserType, string StartDt, string EndDt)
        {
            IList<UserStats> UserList = null;
            var ds = GetDataSetResult("[dbo].[proc_get_UserStats]",
                   new SqlParameter("CompanyID", CompanyID),
                   new SqlParameter("UserType", UserType),
                   new SqlParameter("StartDt", StartDt),
                   new SqlParameter("EndDt", EndDt)
                );

            UserList = (from row in ds.Tables[0].AsEnumerable()
                        select new Entities.UserStats
                        {
                            UserType = Convert.ToString(row["UserType"].CheckDBNull()),
                            DriversOnDuty = Convert.ToInt32(row["DriversOnDuty"].CheckDBNull()),
                            TotalWaves = Convert.ToInt32(row["TotalWaves"].CheckDBNull()),
                            CompletedWaves = Convert.ToInt32(row["CompletedWaves"].CheckDBNull()),
                        }).ToList();
            return UserList;
        }

        // All Statuses for Order Type  get_Method
        public IList<StatusList> getStatusList()
        {
            IList<StatusList> StatusList = null;
            var ds = GetDataSetResult("[dbo].[proc_get_StatusList]");

            StatusList = (from row in ds.Tables[0].AsEnumerable()
                          select new Entities.StatusList
                          {
                              Status_PK = Convert.ToInt32(row["Status_PK"].CheckDBNull()),
                              Name = Convert.ToString(row["Name"].CheckDBNull()),
                              Code = Convert.ToString(row["Code"].CheckDBNull()),
                              ReportStatus = Convert.ToString(row["ReportStatus"].CheckDBNull()),
                          }).ToList();
            return StatusList;
        }

        // Geo Location get_Method
        public IList<GeoLocation> getGeoLocation(int CompanyID)
        {
            IList<GeoLocation> GeoLocationList = null;
            var ds = GetDataSetResult("[dbo].[proc_get_GeoFenceByCompany]",
                   new SqlParameter("CompanyID", ClaimHelper.CompanyId)
                );

            GeoLocationList = (from row in ds.Tables[0].AsEnumerable()
                               select new Entities.GeoLocation
                               {
                                   GeoFence = Convert.ToString(row["GeoFence"].CheckDBNull()),
                               }).ToList();
            return GeoLocationList;
        }

        public IList<LogistikaOrderLineItem> BindLineItemDetail(DataSet Data, int index = 0)
        {

            if (!Data.IsDataSetNotNullAndTableHasRows(index))
            {

                return (IList<LogistikaOrderLineItem>)null;
            }
            return Data.Tables[index].DataTableToCollectionType<LogistikaOrderLineItem>();
        }

        public IList<Document> BindDocument(DataSet Data, int index = 0)
        {
            if (!Data.IsDataSetNotNullAndTableHasRows(index))
            {
                return (IList<Document>)null;
            }
            return Data.Tables[index].DataTableToCollectionType<Document>();
        }

        // Menu get_Method
        public IList<MenuBar> getMenuItem(string UserName, string Password)
        {
            return GetList<MenuBar>("[dbo].[proc_get_MenuItem]",
                   new SqlParameter("UserName", UserName),
                   new SqlParameter("Password", Encryption.EncryptionManager.BasicEncrypt(Password)) //User.Password),)
                );
        }

        public UserDataAccess()
            : base()
        {

        }

        public bool CheckRestrictedServiceAuthorization(string UserName, string Url, string OprationCode, string ScreenCode = "MVC_KFISPORTAL")
        {
            var ds = GetDataSetResult("[dbo].[proc_Check_Restricted_Service_Authorization]",
                  new SqlParameter("UserName", UserName)
                 , new SqlParameter("OprationCode", OprationCode)
                 , new SqlParameter("ScreenCode", ScreenCode)
                 , new SqlParameter("URL", Url)
                 , OutputParameter
                );

            if (ds.IsDataSetNotNullAndTableHasRows())
            {
                return Convert.ToString(ds.Tables[0].Rows[0][0]) == "1";
            }
            return false;
        }
        public IList<Entities.Menu.MenuItem> GetUserMenu(string UserId, string Type = "NESTED")
        {
            IList<Entities.Menu.MenuItem> lst = null;
            var ds = GetDataSetResult("[dbo].[proc_get_KFIS_All_Menu_With_Operation]",
             new SqlParameter("ScreenCode", "MVC_KfisPortal"),
             new SqlParameter("UserName", ((UserId != string.Empty && UserId.Length > 0) ? (object)UserId : DBNull.Value)),
             OutputParameter
            );

            if (ds.IsDataSetNotNullAndTableHasRows())
            {
                if (Type == "NESTED")
                {
                    lst = (from row in ds.Tables[0].AsEnumerable()
                           where string.IsNullOrEmpty(Convert.ToString(row["ParentID"].CheckDBNull()))
                           && string.IsNullOrEmpty(Convert.ToString(row["URL"].CheckDBNull()))
                           orderby Convert.ToInt32(row["Menu_PK"].CheckDBNull()), Convert.ToInt32(row["Sequence"].CheckDBNull())
                           select GetMenuItem(row)).ToList();
                    if (lst != null && lst.Count > 0)
                    {
                        foreach (MenuItem item in lst)
                        {
                            GetSubMenu(ds, item);
                        }
                    }
                }
                if (Type == "LONG")
                {
                    lst = (from row in ds.Tables[0].AsEnumerable()
                           orderby Convert.ToInt32(row["Menu_PK"].CheckDBNull()), Convert.ToInt32(row["Sequence"].CheckDBNull())
                           select GetMenuItem(row)).ToList();
                }
                else { }

            }

            return lst;
        }

        public IList<Entities.Menu.MenuItem> GetAllMenu(string Type = "NESTED")
        {
            return GetUserMenu(string.Empty, Type);
        }

        public IList<GroupUser> GetUserGroup(int? GroupId)
        {
            IList<Entities.Menu.GroupUser> lst = null;

            var ds = GetDataSetResult("[dbo].[proc_get_KFIS_Group_Details]",
                new SqlParameter("GroupId", (GroupId.HasValue ? (Object)GroupId.Value : DBNull.Value))
            , OutputParameter
            );

            if (ds.IsDataSetNotNullAndTableHasRows())
            {
                lst = (from row in ds.Tables[0].AsEnumerable()
                       select new Entities.Menu.GroupUser
                  {
                      Name = Convert.ToString(row["GroupName"].CheckDBNull()),
                      Description = Convert.ToString(row["Description"].CheckDBNull()),
                      CreatedDate = Convert.ToDateTime(row["CreatedDt"].CheckDBNull()),
                      CreatedBy = Convert.ToString(row["CreatedBy"].CheckDBNull()),
                      GroupId = Convert.ToInt32(row["FrameworkKFISUserGroup_PK"].CheckDBNull()),
                      IsActive = Convert.ToBoolean(row["IsActive"].CheckDBNull()),
                      GroupUsers = Convert.ToString(row["Users"].CheckDBNull()).StringToList<string>(),
                      OperationId = Convert.ToString(row["OperationId"].CheckDBNull()).StringToList<int>()

                  }).ToList();
            }
            return lst;
        }

        public KfisUser GetKfisUser(string UserName)
        {
            KfisUser user = null;

            var ds = GetDataSetResult("[dbo].[proc_get_UserDetails]",
                new SqlParameter("UserName", UserName)
            );

            if (ds.IsDataSetNotNullAndTableHasRows())
            {
                user = (from row in ds.Tables[0].AsEnumerable()
                        select new Entities.KfisUser
                   {
                       FirstName = Convert.ToString(row["FirstName"].CheckDBNull()),
                       LastName = Convert.ToString(row["LastName"].CheckDBNull()),
                       MiddleName = Convert.ToString(row["MiddleName"].CheckDBNull()),
                       ModifiedBy = Convert.ToString(row["ModifiedBy"].CheckDBNull()),
                       ModifiedDt = Convert.ToDateTime(row["ModifiedDt"].CheckDBNull()),
                       PersonId = Convert.ToString(row["UserId"].CheckDBNull()),
                       PrimaryClient = Convert.ToInt32(row["ClientId"].CheckDBNull()),
                       Suffix = Convert.ToString(row["Suffix"].CheckDBNull()),
                       Title = Convert.ToString(row["Title"].CheckDBNull()),
                       UserName = Convert.ToString(row["UserName"].CheckDBNull()),
                       Designation = Convert.ToString(row["ProfessionalDesignation"].CheckDBNull()),
                       ClientList = Convert.ToString(row["ClientList"].CheckDBNull()),
                       CreatedBy = Convert.ToString(row["CreatedBy"].CheckDBNull()),
                       CreatedDt = Convert.ToDateTime(row["CreatedDt"].CheckDBNull()),
                       Active = Convert.ToBoolean(row["Active"].CheckDBNull()),
                       // FullName = ,
                       JobTitle = Convert.ToString(row["JobTitle"].CheckDBNull()),
                       KfisUserId = Convert.ToInt32(row["UserId"].CheckDBNull()),
                       LastLogin = Convert.ToDateTime(row["LastLogin"].CheckDBNull()),
                       LeftImagePath = Convert.ToString(row["LeftImagePath"].CheckDBNull()),
                       RightImagePath = Convert.ToString(row["RightImagePath"].CheckDBNull()),
                       AddtionalPermissionList = Convert.ToString(row["AddtionalPermissionList"].CheckDBNull()),
                       Contact = new Contact
                       {
                           Phone = Convert.ToString(row["PhonePrimary"].CheckDBNull()),
                           SecondaryPhone = Convert.ToString(row["PhoneSecondary"].CheckDBNull()),
                           FaxNo = Convert.ToString(row["Fax"].CheckDBNull()),
                           Email = Convert.ToString(row["EmailAddressPrimary"].CheckDBNull()),
                           SecondaryEmail = Convert.ToString(row["EmailAddressSecondary"].CheckDBNull()),
                       }

                   }).FirstOrDefault();
            }
            return user;
        }

        public string SaveUser(KfisUser User)
        {
            var retMsg = GetOutputParameter("RetMsg", ParameterDirection.InputOutput);
            retMsg.Value = "MVC_KfisPortal";
            var success = Exec("proc_ins_upd_KFIS_User",
                     new SqlParameter("KFISUser_PK", User.KfisUserId)
                    , new SqlParameter("UserName", User.UserName)
                    , new SqlParameter("Title", User.Title)
                    , new SqlParameter("FirstName", User.FirstName)
                    , new SqlParameter("MiddleName", User.MiddleName)
                    , new SqlParameter("LastName", User.LastName)
                    , new SqlParameter("Suffix", User.Suffix)
                    , new SqlParameter("Designation", User.Designation)
                    , new SqlParameter("Password", User.Password)
                    , new SqlParameter("Active", User.Active)
                    , new SqlParameter("PrimaryClient_PK", User.PrimaryClient)
                    , new SqlParameter("JobTitle", User.JobTitle)
                    , new SqlParameter("PhonePrimary", User.Contact.PhoneNumber)
                    , new SqlParameter("PhoneSecondary", User.Contact.SecondaryPhone)
                    , new SqlParameter("Fax", User.Contact.FaxNo)
                    , new SqlParameter("EmailPrimary", User.Contact.Email)
                    , new SqlParameter("EmailSecondary", User.Contact.SecondaryEmail)
                    , new SqlParameter("CreatedBy", User.CreatedBy)
                    , new SqlParameter("ModifiedBy", User.CreatedBy)
                    , new SqlParameter("PermissionList", User.PermissionList)
                    , new SqlParameter("ClientList", (string.IsNullOrEmpty(User.ClientList) ? "-1" : User.ClientList))
                    , new SqlParameter("PasswordText", String.Empty)
                    , retMsg
                    );

            return Convert.ToString(retMsg.Value);
        }

        public string UpdateFrameworkAddress(Address address)
        {

            var success = Exec("[proc_upd_FrameworkAddress]",
                     new SqlParameter("FrameworkAddressID", address.FrameworkAddressID)
                    ,new SqlParameter("AddressLine1", address.AddressLine1)
                    ,new SqlParameter("Locality", address.Locality)
                    ,new SqlParameter("LastModifiedBy", ClaimHelper.UserName)
                    );

            return "Success";
        }

        #region Private Methods

        private Entities.Menu.MenuItem GetMenuItem(DataRow row)
        {
            return new Entities.Menu.MenuItem
            {
                MenuText = Convert.ToString(row["MenuText"].CheckDBNull()),
                Sequence = Convert.ToInt32(row["Sequence"].CheckDBNull()),
                ScreenName = Convert.ToString(row["ScreenName"].CheckDBNull()),
                MenuId = Convert.ToInt32(row["Menu_PK"].CheckDBNull()),
                MenuURL = Convert.ToString(row["URL"].CheckDBNull()),
                ToolTip = Convert.ToString(row["ToolTip"].CheckDBNull()),
                ParentId = Convert.ToInt32(row["ParentID"].CheckDBNull()),
                //Operations = Convert.ToString(row["Operation"].CheckDBNull()).StringToList<Operation>(),
                OperationsUrl = GetOperationUrl(Convert.ToString(row["OperationWithId"].CheckDBNull()))

            };
        }

        private void GetSubMenu(DataSet DataSource, MenuItem Item)
        {
            var lst = (from row in DataSource.Tables[0].AsEnumerable()
                       where Item.MenuId == Convert.ToInt32(row["ParentID"].CheckDBNull())
                       orderby Convert.ToInt32(row["Sequence"].CheckDBNull())
                       select GetMenuItem(row)).ToList<MenuItem>();
            if (lst != null && lst.Count > 0)
            {
                Item.SubMenu = lst;

                foreach (MenuItem item in lst)
                {
                    GetSubMenu(DataSource, item);
                }
            }

        }

        private IList<OperationUrl> GetOperationUrl(string StringToFormat)
        {
            IList<OperationUrl> lstOperationUrl = null;
            if (!string.IsNullOrEmpty(StringToFormat))
            {
                lstOperationUrl = new List<OperationUrl>();
                var lstOfOperation = StringToFormat.StringToList<string>(",");
                if (lstOfOperation != null && lstOfOperation.Count > 0)
                {

                    foreach (string str in lstOfOperation)
                    {
                        var ou = new OperationUrl();

                        int index = str.IndexOf('(');
                        if (index > 0)
                        {
                            var operation = str.Substring(0, index);
                            var id = str.Substring(index + 1, str.Length - index - 2);
                            lstOperationUrl.Add(new OperationUrl
                            {
                                Code = operation,
                                OperationUrlId = Convert.ToInt32(id)
                            });
                        }

                    }
                }
            }
            return lstOperationUrl;
        }
        #endregion

        public LogistikaUser SaveUser(string Id)
        {
            throw new NotImplementedException();
        }

        public FleetModal GetsFleet(string Id = "", int? AddressId = 0)
        {
            FleetModal user = null;
            var ds = GetDataSetResult("proc_ins_upd_FleetInformation",
                 new SqlParameter("CompanyID", ClaimHelper.CompanyId),
                 new SqlParameter("CompanyAddressID", (AddressId.HasValue && AddressId.Value > 0) ? (object)AddressId.Value : DBNull.Value),
                 new SqlParameter("FleetID", string.IsNullOrEmpty(Id) ? DBNull.Value : (object)Id),
                 new SqlParameter("Operation", "G")
                );

            IList<Fleet> flts = null;
            if (ds.IsDataSetNotNullAndTableHasRows())
            {
                user = new FleetModal();
                flts = ds.Tables[0].AsEnumerable().Select(row =>
                    new Fleet()
                    {
                        Make = Convert.ToString(row["Make"].CheckDBNull()),
                        CompanyAddressID = Convert.ToInt32(row["CompanyAddressID"].CheckDBNull()),
                        FleetID = Convert.ToString(row["FleetID"].CheckDBNull()),
                        OwnerShipType = Convert.ToString(row["OwnerShipType"].CheckDBNull()),
                        Model = Convert.ToString(row["Model"].CheckDBNull()),
                        Colour = Convert.ToString(row["Colour"].CheckDBNull()),
                        ModelYear = Convert.ToInt32(row["ModelYear"].CheckDBNull()),
                        StartingMileage = Convert.ToInt32(row["StartingMileage"].CheckDBNull()),
                        Payload = Convert.ToString(row["Capacity"].CheckDBNull()),
                        OverallLength = Convert.ToString(row["Dimension"].CheckDBNull()).SplitAndTakeIndex("X", 0),
                        OverallWidth = Convert.ToString(row["Dimension"].CheckDBNull()).SplitAndTakeIndex("X", 1),
                        OverallHeight = Convert.ToString(row["Dimension"].CheckDBNull()).SplitAndTakeIndex("X", 2),
                        IsActive = Convert.ToBoolean(row["IsActive"].CheckDBNull()),
                        LastModifiedBy = Convert.ToString(row["LastModifiedBy"].CheckDBNull()),
                        LastModifiedDt = Convert.ToString(row["LastModifiedDt"].CheckDBNull())
                    }
                    ).ToList();

                if (!string.IsNullOrEmpty(Id))
                {
                    user.Fleet = flts.FirstOrDefault();
                }
                else
                { user.Fleets = flts; }

                if (ds.IsDataSetNotNullAndTableHasRows(1))
                {
                    user.Documents = ds.Tables[1].AsEnumerable().Select(row =>
                        new Document()
                        {
                            //   Title StartDate EndDate Branch IsActive
                            DocumentUrl = Convert.ToString(row["DocumentURL"].CheckDBNull()),
                            DocumentName = Convert.ToString(row["FileName"].CheckDBNull()),
                            DocumentType = Convert.ToString(row["DocumentType"].CheckDBNull()),
                            ExpirationDate = Convert.ToDateTime(row["ExpirationDate"].CheckDBNull()),
                            DocumentNumber = Convert.ToString(row["DocumentNumber"].CheckDBNull()),
                            IssuedState = Convert.ToString(row["IssuedState"].CheckDBNull()),
                            IsActive = Convert.ToBoolean(row["IsActive"].CheckDBNull()),
                            IssuedDate = Convert.ToDateTime(row["IssuedDate"].CheckDBNull()),
                            CreatedDt = Convert.ToString(row["LastModifiedBy"].CheckDBNull())
                        }
                        ).ToList();
                }
            }
            return user;
        }

        public IList<WaveFleet> getFleet4Wave()
        {
            IList<WaveFleet> WaveFleetList = null;

            var ds = GetDataSetResult("proc_get_FleetInformation",
                     new SqlParameter("CompanyID", ClaimHelper.CompanyId)
                    );

            WaveFleetList = (from row in ds.Tables[0].AsEnumerable()
                           select new Entities.WaveFleet
                           {
                               Make =  Convert.ToString(row["Make"].CheckDBNull()),
                               Model = Convert.ToString(row["Model"].CheckDBNull()),
                               ModelYear = Convert.ToString(row["ModelYear"].CheckDBNull()),
                               FleetID = Convert.ToString(row["FleetID"].CheckDBNull()),
                               DocumentNumber = Convert.ToString(row["DocumentNumber"].CheckDBNull()),
                               DocumentType = Convert.ToString(row["DocumentType"].CheckDBNull()),
                               Colour = Convert.ToString(row["Colour"].CheckDBNull()),
                               CompanyAddressID = Convert.ToString(row["CompanyAddressID"].CheckDBNull()),
                               AddressLine1 = Convert.ToString(row["AddressLine1"].CheckDBNull()),
                               LandMark = Convert.ToString(row["LandMark"].CheckDBNull()),
                               City = Convert.ToString(row["City"].CheckDBNull()),
                               StateCode = Convert.ToString(row["StateCode"].CheckDBNull()),
                           }).ToList();
            return WaveFleetList;
        }
        public bool SaveFleet(Fleet Fleet, string DocumentXml)
        {
            var OutputMessage = GetOutputParameter("OutputMessage", ParameterDirection.InputOutput);
            var success = Exec("proc_ins_upd_FleetInformation",
                    new SqlParameter("CompanyID",ClaimHelper.CompanyId),
                    new SqlParameter("CompanyAddressID", Fleet.CompanyAddressID),
                    new SqlParameter("Make", Fleet.Make),
                    new SqlParameter("Model", Fleet.Model),
                    new SqlParameter("OverallLength", Fleet.OverallLength),
                    new SqlParameter("OverallWidth", Fleet.OverallWidth),
                    new SqlParameter("OverallHeight", Fleet.OverallHeight),
                    new SqlParameter("Payload", Fleet.Payload),
                    new SqlParameter("Colour", Fleet.Colour),
                    new SqlParameter("ModelYear", Fleet.ModelYear),
                    new SqlParameter("OwnerShipType", Fleet.OwnerShipType),
                    new SqlParameter("StartingMileage", Fleet.StartingMileage),
                    new SqlParameter("FleetID", Fleet.FleetID),
                    new SqlParameter("IsActive", Fleet.IsActive),
                    new SqlParameter("DocumentXML", DocumentXml),
                    new SqlParameter("LastModifiedBy", ClaimHelper.UserName),
                    new SqlParameter("Operation", !string.IsNullOrEmpty(Fleet.FleetGuid) ? "U" : "I"),
                    OutputMessage);

            if (string.IsNullOrEmpty(OutputMessage.Value.ToString()))
            {
                return true;
            }
            return false;
        }


        //public IList<EmployeePlannerModal> GetEmployeePlannerList(int? CompanyID = 0, int? AddressId = 0)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

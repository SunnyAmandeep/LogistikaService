
using Logistika.Service.Common.BusinessComponentInterface.User;
using Logistika.Service.Common.Entities;
using Logistika.Service.Common.Extension;
using Logistika.Service.Common.File;
using System;
using System.Collections.Generic;
using Logistika.Service.Common.Helper;
using System.Linq;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Data;
using Logistika.Service.Common.Entities.Lookup;
using Logistika.Service.Common.DataAccess.Logger;

namespace Logistika.Service.Common.DataAccessInterface.User
{
    public class UserBusinessComponent : IUserBusinessComponent
    {
        IUserDataAccess _userDataAccess = null;
        string fileName = null;
        string accessfileName = null;
        string ImportFileHistoryID = null;

        public UserBusinessComponent(IUserDataAccess Instance)
        {
            _userDataAccess = Instance;
        }

        public IList<Country> GetCountryInfo(string Country)
        {
            return _userDataAccess.GetCountryInfo(Country);
        }

        //public string LogFileImport(string FileName, string Status, string ValidRecords, string InValidRecords, string ErrorFileName, string ImportFileHistory_PK)
        //{
        //    return _userDataAccess.LogFileImport(FileName, Status, ValidRecords, InValidRecords, ErrorFileName, ImportFileHistory_PK);
        //}

        public string UserFileImport(string FileName, string CallType)
        {
            int ValidRecords = 0;
            int InValidRecords = 0;

            try
            {
                accessfileName = FileName + SiteConfigurationManager.GetAppSettingKey("FileImportExtension");
                fileName = ClaimHelper.CompanyId + "_" + FileName;
                FileName = fileName + SiteConfigurationManager.GetAppSettingKey("FileImportExtension");

                string logImport = null;
                bool UserResult = false;
                string rootFolderPath = SiteConfigurationManager.GetAppSettingKey("TemplatePath") + "driverFile\\";
                string destinationPath = rootFolderPath + "\\Archive\\";
                string fullPathToExcel = rootFolderPath + accessfileName;
                string fullpathToArchive = destinationPath + accessfileName;

                LoggerDataAccess loggerDataAccess = new LoggerDataAccess();

                logImport = loggerDataAccess.LogFileImport(FileName, "IMP_FL_INITIATED", ValidRecords.ToString(), InValidRecords.ToString(), null, null);
                ImportFileHistoryID = logImport;

                if (logImport == "File is already processed.")
                {
                    return "File is already processed.";
                }

                if (string.IsNullOrEmpty(logImport))
                {
                    throw new Exception("Invalid response from Log File Import.");
                }

                if (new FileInfo(fullpathToArchive).Exists == false)
                {
                    string connString = string.Format("Provider =Microsoft.ACE.OLEDB.12.0;Data Source=" + fullPathToExcel + ";Extended Properties='Excel 12.0;HDR=yes'", fullPathToExcel);
                    DataTable dt = FileManager.GetDataTable("SELECT * FROM [Sheet1$]", connString);

                    Thread.CurrentThread.CurrentCulture =
                    CultureInfo.CreateSpecificCulture("en-GB");
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");

                    var date = DateTime.Now;

                    IList<Country> PhoneCode = null;

                    if (CallType == "User")
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            try
                            {
                                LogistikaUser User = new LogistikaUser()
                                {
                                    BranchId = dt.Rows[i]["BranchId"].ToString(),
                                    FirstName = dt.Rows[i]["FirstName"].ToString(),
                                    MiddleName = dt.Rows[i]["MiddleName"].ToString(),
                                    LastName = dt.Rows[i]["LastName"].ToString(),
                                    DateOfBirth = Convert.ToDateTime(dt.Rows[i]["DateOfBirth"].ToString()),
                                    Designation = dt.Rows[i]["EmployeeRole"].ToString(),
                                    JobTitle = dt.Rows[i]["EmployeeTitle"].ToString(),
                                    Contact = new Contact()
                                    {
                                        Email = dt.Rows[i]["PrimaryEmail"].ToString(),
                                        Phone = dt.Rows[i]["Phone"].ToString(),
                                        Mobile = dt.Rows[i]["MobileNo"].ToString(),
                                    },
                                    Password = dt.Rows[i]["Password"].ToString(),
                                    Gender = dt.Rows[i]["Gender"].ToString(),
                                    OtherInfo = new List<DropdownData>
                                    {
                                        new DropdownData()
                                        {
                                            Text = "LicenseNumber",
                                            Value = dt.Rows[i]["LicenseNumber"].ToString(),
                                        },
                                        new DropdownData()
                                        {
                                            Text = "LicenceClass",
                                            Value = dt.Rows[i]["LicenceClass"].ToString(),
                                        },
                                        new DropdownData()
                                        {
                                            Text = "Experience",
                                            Value = dt.Rows[i]["Experience"].ToString(),
                                        },
                                        new DropdownData()
                                        {
                                            Text = "MonthlyWage",
                                            Value = dt.Rows[i]["MonthlyWage"].ToString(),
                                        }
                                    },
                                    StartDate = Convert.ToDateTime(dt.Rows[i]["StartDate"].ToString()),
                                    EndDate = Convert.ToDateTime(dt.Rows[i]["EndDate"].ToString()),
                                    Active = Convert.ToBoolean(dt.Rows[i]["IsActive"].ToString()),
                                    Addresses = new List<Address>
                                    {
                                        new Address()
                                        {
                                            AddressLine1 = dt.Rows[i]["AddressLine1"].ToString(),
                                            Suite = dt.Rows[i]["SuiteNo"].ToString(),
                                            Locality = dt.Rows[i]["Locality"].ToString(),
                                            City = dt.Rows[i]["City"].ToString(),
                                            StateCode = dt.Rows[i]["State"].ToString(),
                                            PostalCode = dt.Rows[i]["PinCode"].ToString(),
                                            CountryCode = dt.Rows[i]["CountryCode"].ToString(),
                                            LandMark = dt.Rows[i]["LandMark"].ToString()
                                        }
                                    }
                                };

                                PhoneCode = _userDataAccess.GetCountryInfo(dt.Rows[i]["CountryCode"].ToString());

                                if (User.Contact.Mobile.StartsWith("0"))
                                {
                                    User.Contact.Mobile = User.Contact.Mobile.TrimStart('0');
                                    User.Contact.Mobile = PhoneCode[0].PhoneCode + User.Contact.Mobile;
                                }
                                else if (!User.Contact.Mobile.StartsWith("0"))
                                {
                                    User.Contact.Mobile = PhoneCode[0].PhoneCode + User.Contact.Mobile;
                                }
                                if (User.Contact.Phone.StartsWith("0"))
                                {
                                    User.Contact.Phone = User.Contact.Phone.TrimStart('0');
                                    User.Contact.Phone = PhoneCode[0].PhoneCode + User.Contact.Phone;
                                }
                                else if (!User.Contact.Phone.StartsWith("0"))
                                {
                                    User.Contact.Phone = PhoneCode[0].PhoneCode + User.Contact.Phone;
                                }

                                UserResult = SaveUser(User, null);
                                //var service = new UserBusinessComponent(new UserDataAccess());
                                if (UserResult)
                                {
                                    ValidRecords = ValidRecords + 1;
                                }
                                if (!UserResult)
                                {
                                    InValidRecords = InValidRecords + 1;
                                }
                            }
                            catch (Exception ex)
                            {
                                logImport = loggerDataAccess.LogFileImport(FileName, "IMP_FL_FLD", ValidRecords.ToString(), InValidRecords.ToString(), ex.Message, ImportFileHistoryID);
                                return ex.Message;
                            }
                        }
                    }
                    //Console.ReadLine();

                    string[] fileList = Directory.GetFiles(rootFolderPath, FileName);

                    if (Directory.Exists(rootFolderPath))
                    {
                        foreach (string file in fileList)
                        {
                            FileInfo mFile = new FileInfo(file);
                            if (new FileInfo(destinationPath + "\\" + mFile.Name).Exists == false)
                                mFile.MoveTo(destinationPath + "\\" + mFile.Name);
                        }
                    }
                }
                else if (new FileInfo(fullpathToArchive).Exists == true)
                {
                    return "File is already processed";
                }
                if (UserResult)
                {
                    logImport = loggerDataAccess.LogFileImport(FileName, "IMP_FL_SCCS", ValidRecords.ToString(), InValidRecords.ToString(), null, ImportFileHistoryID);
                    return FileName + ":- File imported successully.";
                }
                string eFileName;
                string errorFileName;

                LoggerDataAccess eloggerDataAccess = new LoggerDataAccess();

                eFileName = "UserError_" + fileName + "_" + DateTime.Now.ToString("MMddyyyyHH");
                errorFileName = "DriverFile\\error\\" + eFileName + ".error.txt";
                string filePath = SiteConfigurationManager.GetAppSettingKey("TemplatePath") + errorFileName;

                var v = FileManager.FileErrorLog(filePath, "Incorrect File Layout/User already exists", "Error while processing file");

                logImport = loggerDataAccess.LogFileImport(FileName, "IMP_FL_FLD", ValidRecords.ToString(), InValidRecords.ToString(), errorFileName, ImportFileHistoryID);
                return FileName + ":- File import Failed.";
            }
            catch (Exception ex)
            {
                string eFileName;
                string errorFileName;
                
                eFileName = "UserError_" + fileName + "_" + DateTime.Now.ToString("MMddyyyyHH");
                errorFileName = "DriverFile\\error\\" + eFileName + ".error.txt";
                string filePath = SiteConfigurationManager.GetAppSettingKey("TemplatePath") + errorFileName;

                LoggerDataAccess eloggerDataAccess = new LoggerDataAccess();

                if (!string.IsNullOrEmpty(fileName))
                {
                    var v = FileManager.FileErrorLog(filePath, ex.Message, "Error with File");
                    eloggerDataAccess.LogFileImport(FileName, "IMP_FL_FLD", ValidRecords.ToString(), InValidRecords.ToString(), errorFileName, ImportFileHistoryID);
                }
                return FileName + ":- File import Failed.";
            }
        }

        public string FleetFileImport(string FileName, string CallType)
        {
            int ValidRecords = 0;
            int InValidRecords = 0;

            try
            {
                accessfileName = FileName + SiteConfigurationManager.GetAppSettingKey("FileImportExtension");
                fileName = ClaimHelper.CompanyId + "_" + FileName;
                FileName = fileName + SiteConfigurationManager.GetAppSettingKey("FileImportExtension");

                string logImport = null;
                bool FleetResult = false;
                string rootFolderPath = SiteConfigurationManager.GetAppSettingKey("TemplatePath") + "vehicleFile\\";
                string destinationPath = rootFolderPath + "\\Archive\\";
                string fullPathToExcel = rootFolderPath + accessfileName;
                string fullpathToArchive = destinationPath + accessfileName;

                LoggerDataAccess loggerDataAccess = new LoggerDataAccess();

                logImport = loggerDataAccess.LogFileImport(FileName, "IMP_FL_INITIATED", ValidRecords.ToString(), InValidRecords.ToString(), null, null);
                ImportFileHistoryID = logImport;

                if (new FileInfo(fullpathToArchive).Exists == false)
                {
                    string connString = string.Format("Provider =Microsoft.ACE.OLEDB.12.0;Data Source=" + fullPathToExcel + ";Extended Properties='Excel 12.0;HDR=yes'", fullPathToExcel);
                    DataTable dt = FileManager.GetDataTable("SELECT * FROM [Sheet1$]", connString);

                    Thread.CurrentThread.CurrentCulture =
                    CultureInfo.CreateSpecificCulture("en-GB");
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");

                    var date = DateTime.Now;

                    if (CallType.ToLower() == "fleet")
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Fleet Fleet = new Fleet()
                            {
                                CompanyAddressID = Convert.ToInt32(dt.Rows[i]["BranchID"].ToString()),
                                FleetID = dt.Rows[i]["FleetID"].ToString(),
                                Make = dt.Rows[i]["Make"].ToString(),
                                Model = dt.Rows[i]["Model"].ToString(),
                                ModelYear = Convert.ToInt32(dt.Rows[i]["ModelYear"].ToString()),
                                Colour = dt.Rows[i]["Colour"].ToString(),
                                StartingMileage = Convert.ToInt32(dt.Rows[i]["StartingMileage"].ToString()),
                                Payload = dt.Rows[i]["Payload"].ToString(),
                                OwnerShipType = dt.Rows[i]["OwnerShipType"].ToString(),
                                OverallLength = dt.Rows[i]["OverallLength"].ToString(),
                                OverallWidth = dt.Rows[i]["OverallWidth"].ToString(),
                                OverallHeight = dt.Rows[i]["OverallHeight"].ToString(),
                                IsActive = Convert.ToBoolean(dt.Rows[i]["IsActive"].ToString())
                            };
                            FleetResult = SaveFleet(Fleet, null,"");
                            if (FleetResult)
                            {
                                ValidRecords = ValidRecords + 1;
                            }
                            if (!FleetResult)
                            {
                                InValidRecords = InValidRecords + 1;
                            }
                        }

                    }
                    //Console.ReadLine();

                    string[] fileList = Directory.GetFiles(rootFolderPath, FileName);

                    if (Directory.Exists(rootFolderPath))
                    {
                        foreach (string file in fileList)
                        {
                            FileInfo mFile = new FileInfo(file);
                            if (new FileInfo(destinationPath + "\\" + mFile.Name).Exists == false)
                                mFile.MoveTo(destinationPath + "\\" + mFile.Name);
                        }
                    }
                }
                else if (new FileInfo(fullpathToArchive).Exists == true)
                {
                    return "File is already processed";
                }
                if (FleetResult)
                {
                    logImport = loggerDataAccess.LogFileImport(FileName, "IMP_FL_SCCS", ValidRecords.ToString(), InValidRecords.ToString(), null, ImportFileHistoryID);
                    return FileName + ":- File imported successully.";
                }
                string eFileName;
                string errorFileName;

                LoggerDataAccess eloggerDataAccess = new LoggerDataAccess();

                eFileName = "VehicleError_" + fileName + "_" + DateTime.Now.ToString("MMddyyyyHH");
                errorFileName = "VehicleFile\\error\\" + eFileName + ".error.txt";
                string filePath = SiteConfigurationManager.GetAppSettingKey("TemplatePath") + errorFileName;

                var v = FileManager.FileErrorLog(filePath, "Incorrect File Layout/FleetID already exists", "Error while processing file");

                logImport = loggerDataAccess.LogFileImport(FileName, "IMP_FL_FLD", ValidRecords.ToString(), InValidRecords.ToString(), errorFileName, ImportFileHistoryID);
                return FileName + ":- File import Failed.";
            }
            catch(Exception ex)
            {
                string eFileName;
                string errorFileName;

                LoggerDataAccess eloggerDataAccess = new LoggerDataAccess();

                eFileName = "VehicleError_" + fileName + "_" + DateTime.Now.ToString("MMddyyyyHH");
                errorFileName = "VehileFile\\error\\" + eFileName + ".error.txt";
                string filePath = SiteConfigurationManager.GetAppSettingKey("TemplatePath") + errorFileName;

                if (!string.IsNullOrEmpty(fileName))
                {
                    var v = FileManager.FileErrorLog(filePath, ex.Message, "Error with File");
                    eloggerDataAccess.LogFileImport(FileName, "IMP_FL_FLD", ValidRecords.ToString(),InValidRecords.ToString(), errorFileName, ImportFileHistoryID);
                }
                return FileName + ":- File import Failed.";
            }
        }

        public ForgotUserPassword ForgotUserNamePassword(string UserName = null, string PrimaryPhone = null)
        {
            return _userDataAccess.ForgotUserNamePassword(UserName, PrimaryPhone);
        }

        public string checkAvailablity(string Type, string UserName = null, string Company = null, string TaxIdentification = null)
        {
            return _userDataAccess.checkAvailablity(Type, UserName, Company, TaxIdentification);
        }

        public Metrics GetMetricsList(DateTime StartDt, DateTime EndDt, int? CompanyAddressID = 0)
        {
            return _userDataAccess.GetMetricsList(StartDt, EndDt, CompanyAddressID);
        }

        public IList<string> GetUserRoles(string UserId)
        {
            return _userDataAccess.GetUserRoles(UserId);
        }

        public IList<Inquiry> GetInquiryList(int? UserCompanyID = 0)
        {
            return _userDataAccess.GetInquiryList(UserCompanyID);
        }

        public bool SaveInquiryInfo(Inquiry Inquiry)
        {
            return _userDataAccess.SaveInquiryInfo(Inquiry);
        }

        public IList<ExpenseLog> GetExpenseLogList()
        {
            return _userDataAccess.GetExpenseLogList();
        }

        public bool SaveExpenseLog(ExpenseLog ExpenseLog)
        {
            return _userDataAccess.SaveExpenseLog(ExpenseLog);
        }

        public FrameworkUser AuthenticateUser(string UserName, string Password)
        {
            //AuthUser AuthorizeUser(string UserName, string Password);
            //AuthUser FrameworkUser = new AuthUser();
            //FrameworkUser = AuthorizeUser(UserName, Password);
            
            //return FrameworkUser;
            if (!string.IsNullOrEmpty(Password))
            {
                var userList = _userDataAccess.getUserList(UserName);
                if (userList != null && userList.Count > 0 && !string.IsNullOrEmpty(Password) && userList[0].Password.Equals(Encryption.EncryptionManager.BasicEncrypt(Password)))
                //userList[0].Password.Equals(Encryption.EncryptionManager.DecryptString(Password)))
                {
                    return userList[0];
                }
            }
            return null;
        }

        public IList<FrameworkUser> getUserList(string UserName)
        {
            if (string.IsNullOrEmpty(UserName) && ClaimHelper.CompanyId == "2")
            {
                throw new Exception("Invalid Oparation");
            }
            return _userDataAccess.getUserList(UserName);
        }

        public bool UpdateUser(string UserName, bool IsActive)
        {
            if (ClaimHelper.CompanyId != "2")
            {
                throw new Exception("Invalid Oparation");
            }
            if (string.IsNullOrEmpty(UserName))
            {
                throw new Exception("UserName Missing");
            }
            return _userDataAccess.UpdateUser(UserName, IsActive);
        }

        public IList<ItemList> getItemList(string Type)
        {
            return _userDataAccess.getItemList(Type);
        }

        public string AddUpdateCompany(string CompanyName, string TaxIdentification, int IsActive, string AuthCode)
        {
            return _userDataAccess.AddUpdateCompany(CompanyName, TaxIdentification, IsActive, AuthCode);
        }

        public string UpdateFrameworkAddress(Address myAddress)
        {
            return _userDataAccess.UpdateFrameworkAddress(myAddress);
        }

        public IList<CompanyAddress> GetHandOffAddress(int? CompanyID = 0)
        {
            return _userDataAccess.GetHandOffAddress(CompanyID);
        }

        public IList<CompanyAddress> getCompanyAddressList(int? CompanyID = 0, int? FrameworkAddressID = 0)
        {
            return _userDataAccess.getCompanyAddressList(CompanyID, FrameworkAddressID);
        }

        public IList<EmployeePlanner> GetEmployeePlannerList(int? CompanyID = 0, int? AddressId = 0)
        {
            return _userDataAccess.GetEmployeePlannerList(CompanyID, AddressId);
        }

        public bool UpdateEmployeePlanner(EmployeePlanner employeePlanner)
        {
            return _userDataAccess.UpdateEmployeePlanner(employeePlanner);
        }

        public IList<Customer> GetCustomerList(string UserName)
        {
            return _userDataAccess.GetCustomerList(UserName);
        }

        public bool SaveCustomer(Customer customer)
        {
            return _userDataAccess.SaveCustomer(customer);
        }

        public IList<CompanyAddress> GetCompanyList(int? CompanyAddressID = 0)
        {
            return _userDataAccess.GetCompanyList(CompanyAddressID);
        }

        public bool SaveCompanyAddressInfo(CompanyAddress companyAddress)
        {
            return _userDataAccess.SaveCompanyAddressInfo(companyAddress);
        }

        public bool SaveCompanySubscriptionInfo(CompanySubscription CompanySubscription)
        {
            return _userDataAccess.SaveCompanySubscriptionInfo(CompanySubscription);
        }

        public IList<CompanySubscription> GetCompanySubscriptionList(int? UserCompanyID = 0)
        {
            return _userDataAccess.GetCompanySubscriptionList(UserCompanyID);
        }

        public IList<SubscriptionPlans> GetSubscriptionPlans()
        {
            return _userDataAccess.GetSubscriptionPlans();
        }

        public IList<Fleet> getFleetMaster(string Make, string Model)
        {
            return _userDataAccess.getFleetMaster(Make, Model);
        }
		
        public IList<GeoLocation> getGeoLocation(int CompanyID)
        {
            return _userDataAccess.getGeoLocation(CompanyID);
        }
        
        public IList<UserStats> getUserStats(int CompanyID, string UserType, string StartDt, string EndDt)
        {
            return _userDataAccess.getUserStats(CompanyID, UserType, StartDt, EndDt);
        }

        public IList<StatusList> getStatusList()
        {
            return _userDataAccess.getStatusList();
        }

        public IList<MenuBar> getMenuItem(string UserName, string Password)
        {
            return _userDataAccess.getMenuItem(UserName, Password);
        }

        public AuthUser AuthorizeUser(string UserName, string Password)
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                throw new Exception("UserName or Password cannot be null and empty");
            }
            IList<FrameworkUser> UserList = null;
            UserList = _userDataAccess.getUserList(UserName);



            if (UserList != null && UserList.Count > 0 && UserList[0].Password.ToString() == Encryption.EncryptionManager.BasicEncrypt(Password))
            {
                _userDataAccess.AuditLogin(UserName);
                var menuList = _userDataAccess.getMenuItem(UserName, Password);
                Subscription cSubscription = new Subscription();
                return new AuthUser
                {
                    Email = UserList[0].PrimaryEmail,
                    FullName = UserList[0].FirstName + " " + UserList[0].LastName,
                    LastLoginDt = UserList[0].LastLoginDt,
                    Password = Encryption.EncryptionManager.BasicDecrypt(UserList[0].Password),
                    UserName = UserList[0].UserName,
                    CompanyID = UserList[0].CompanyID,
                    CompanyName = UserList[0].CompanyName,
                    PhoneNumber = UserList[0].PrimaryPhone,
                    UserType = UserList[0].UserType,
                    Subscription = new Subscription()
                    {
                        MemberSince = UserList[0].MemberSince,
                        ValidTill = UserList[0].ValidTill,
                        PlanDetail = UserList[0].PlanDetail
                    },
                    Menu = menuList
                };
                
            }
            return null;
        }
        
        public string SaveFrameworkUser(FrameworkUser User)
        {
            return _userDataAccess.SaveFrameworkUser(User);
        }

        public IList<Entities.Menu.MenuItem> GetUserMenu(string UserId, string Type = "NESTED")
        {
            return _userDataAccess.GetUserMenu(UserId, Type);
        }

        public IList<Entities.Menu.MenuItem> GetAllUserMenu(string Type = "NESTED")
        {
            return _userDataAccess.GetUserMenu(string.Empty, Type);
        }

        public IList<Entities.Menu.GroupUser> GetUserGroup(int? GroupId)
        {
            return _userDataAccess.GetUserGroup(GroupId);
        }

        public bool CheckRestrictedServiceAuthorization(string UserName, string Url, string OprationCode, string ScreenCode = "MVC_KFISPORTAL") { return _userDataAccess.CheckRestrictedServiceAuthorization(UserName, Url, OprationCode, ScreenCode); }
        
        public LogistikaUserModal GetsUsers(int? Id = 0, int? AddressId = 0)
        {
            return _userDataAccess.GetsUsers(Id, AddressId);
        }

        public bool SaveUser(LogistikaUser User, IList<Document> Documents)
        {
            string exceptionMessages = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(User.BranchId))
                {
                    exceptionMessages = "BranchId is missing";
                }

                if (string.IsNullOrEmpty(User.FirstName))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "First Name is missing" : exceptionMessages + ", " + "First Name is missing";
                }

                if (string.IsNullOrEmpty(User.LastName))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "Last Name is missing" : exceptionMessages + ", " + "Last Name is missing";
                }

                if (User.DateOfBirth == null)
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "Date Of Birth is missing" : exceptionMessages + ", " + "Date Of Birth is missing";
                }

                if (string.IsNullOrEmpty(User.Designation))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "Employee Role is missing" : exceptionMessages + ", " + "Employee Role is missing";
                }

                if (string.IsNullOrEmpty(User.JobTitle))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "Employee Title is missing" : exceptionMessages + ", " + "Employee Title is missing";
                }

                if (string.IsNullOrEmpty(User.Contact.Email))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "Primary Email is missing" : exceptionMessages + ", " + "Primary Email is missing";
                }

                if (string.IsNullOrEmpty(User.Password))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "Password is missing" : exceptionMessages + ", " + "Password is missing";
                }

                if (string.IsNullOrEmpty(User.Gender))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "Gender is missing" : exceptionMessages + ", " + "Gender is missing";
                }

                if (string.IsNullOrEmpty(User.Addresses.FirstOrDefault().AddressLine1))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "AddressLine1 is missing" : exceptionMessages + ", " + "AddressLine1 is missing";
                }

                if (string.IsNullOrEmpty(User.Addresses.FirstOrDefault().Suite))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "House Number/Flat is missing" : exceptionMessages + ", " + "House Number/Flat is missing";
                }

                if (string.IsNullOrEmpty(User.Addresses.FirstOrDefault().City))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "City is missing" : exceptionMessages + ", " + "City is missing";
                }

                if (string.IsNullOrEmpty(User.Addresses.FirstOrDefault().StateCode))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "StateCode is missing" : exceptionMessages + ", " + "StateCode is missing";
                }

                if (string.IsNullOrEmpty(User.Addresses.FirstOrDefault().PostalCode))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "PostalCode is missing" : exceptionMessages + ", " + "PostalCode is missing";
                }

                if (string.IsNullOrEmpty(User.Contact.Phone))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "Phone is missing" : exceptionMessages + ", " + "Phone is missing";
                }

                if (string.IsNullOrEmpty(User.Contact.Mobile))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "Mobile phone is missing" : exceptionMessages + ", " + "Mobile phone is missing";
                }

                if ((string.IsNullOrEmpty(User.OtherInfo.TryGet("LicenseNumber")) || string.IsNullOrEmpty(User.OtherInfo.TryGet("LicenseClass"))) && (User.Designation == "USR_DRV" || User.Designation == "Driver"))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "License Number and License Class is required" : exceptionMessages + ", " + "License Number and License Class is required";
                }


                if ((string.IsNullOrEmpty(User.OtherInfo.TryGet("Experience")) || string.IsNullOrEmpty(User.OtherInfo.TryGet("MonthlyWage"))))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "Experience and MonthlyWage is required" : exceptionMessages + ", " + "Experience and MonthlyWage is required";
                }

                if (User.StartDate == null)
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "StartDate is required" : exceptionMessages + ", " + "StartDate is required";
                }
                
                if (!string.IsNullOrEmpty(exceptionMessages))
                {
                    throw new Exception(exceptionMessages);
                }

                string documentXml = string.Empty;
                if (Documents != null && Documents.Count > 0)
                {
                    var r = Documents.Where(x => x.DocumentType.ToLower() == "photo").FirstOrDefault();
                    if (r != null)
                    {
                        User.Photo = r.DocumentUrl;
                        Documents.Remove(r);
                    }
                    documentXml = Documents.ConvertToXmlWithoutNamespaces<Document>();
                }
                return _userDataAccess.SaveUser(User, documentXml);
            }
            catch (Exception ex)
            {
                string eFileName;
                string errorFileName;

                eFileName = "UserError_" + fileName + "_" + DateTime.Now.ToString("MMddyyyyHH");
                errorFileName = "DriverFile\\error\\" + eFileName + ".error.txt";
                string filePath = SiteConfigurationManager.GetAppSettingKey("TemplatePath") + errorFileName;

                LoggerDataAccess eloggerDataAccess = new LoggerDataAccess();

                if (!string.IsNullOrEmpty(fileName))
                {
                    fileName = fileName + SiteConfigurationManager.GetAppSettingKey("FileImportExtension");
                    if (string.IsNullOrEmpty(User.Contact.Email))
                    {
                        User.Contact.Email = "Empty";
                    }
                    var v = FileManager.FileErrorLog(filePath, ex.Message, User.Contact.Email);
                    eloggerDataAccess.LogFileImport(fileName, "IMP_FL_FLD", null, null, errorFileName, ImportFileHistoryID);
                    
                }
                return false;

            }
        }

        public FleetModal GetFleet(string Id = "", int? AddressId = 0)
        {
            return _userDataAccess.GetsFleet(Id, AddressId);
        }

        public IList<WaveFleet> getFleet4Wave()
        {
            return _userDataAccess.getFleet4Wave();
        }

        public bool SaveFleet(Fleet Fleet, IList<Document> Documents,string CallType)
        {
            string documentXml = string.Empty;
            string exceptionMessages = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(Fleet.FleetID))
                {
                    exceptionMessages = "FleetID is missing";
                }

                if (string.IsNullOrEmpty(Fleet.CompanyAddressID.ToString()))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "Branch ID is missing" : exceptionMessages + ", " + "Branch ID is missing";
                }

                if (string.IsNullOrEmpty(Fleet.Make))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "Make is missing" : exceptionMessages + ", " + "Make is missing";
                }
                if (string.IsNullOrEmpty(Fleet.Model))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "Model is missing" : exceptionMessages + ", " + "Model is missing";
                }
                if (string.IsNullOrEmpty(Fleet.ModelYear.ToString()))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "ModelYear is missing" : exceptionMessages + ", " + "ModelYear is missing";
                }
                if (string.IsNullOrEmpty(Fleet.Colour))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "Colour is missing" : exceptionMessages + ", " + "Colour is missing";
                }
                if (string.IsNullOrEmpty(Fleet.StartingMileage.ToString()))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "StartingMileage is missing" : exceptionMessages + ", " + "StartingMileage is missing";
                }
                if (string.IsNullOrEmpty(Fleet.Payload))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "Payload is missing" : exceptionMessages + ", " + "Payload is missing";
                }
                if (string.IsNullOrEmpty(Fleet.OwnerShipType))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "OwnerShipType is missing" : exceptionMessages + ", " + "OwnerShipType is missing";
                }
                if (string.IsNullOrEmpty(Fleet.OverallLength))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "OverallLength is missing" : exceptionMessages + ", " + "OverallLength is missing";
                }
                if (string.IsNullOrEmpty(Fleet.OverallWidth))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "OverallWidth is missing" : exceptionMessages + ", " + "OverallWidth is missing";
                }
                if (string.IsNullOrEmpty(Fleet.OverallHeight))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "OverallHeight is missing" : exceptionMessages + ", " + "OverallHeight is missing";
                }

                if (!string.IsNullOrEmpty(exceptionMessages))
                {
                    throw new Exception(exceptionMessages);
                }

                if (CallType == "Portal")
                {
                    if (Documents != null && Documents.Count > 0)
                    {
                        documentXml = Documents.ConvertToXmlWithoutNamespaces<Document>();
                    }
                    return _userDataAccess.SaveFleet(Fleet, documentXml);
                }
                if (CallType != "Portal")
                {
                    if (Documents != null && Documents.Count > 0)
                    {
                        documentXml = Documents.ConvertToXmlWithoutNamespaces<Document>();
                    }
                    return _userDataAccess.SaveFleet(Fleet, documentXml);
                }
                return false;
            }
            catch (Exception ex)
            {
                string eFileName;
                string errorFileName;

                eFileName = "VehicleError_" + fileName + "_" + DateTime.Now.ToString("MMddyyyyHH");
                errorFileName = "VehileFile\\error\\" + eFileName + ".error.txt";
                string filePath = SiteConfigurationManager.GetAppSettingKey("TemplatePath") + errorFileName;

                LoggerDataAccess eloggerDataAccess = new LoggerDataAccess();

                if (!string.IsNullOrEmpty(fileName))
                {
                    fileName = fileName + SiteConfigurationManager.GetAppSettingKey("FileImportExtension");
                    if (string.IsNullOrEmpty(Fleet.FleetID))
                    {
                        Fleet.FleetID = "Empty";
                    }
                    
                    var v = FileManager.FileErrorLog(filePath, ex.Message, Fleet.FleetID);

                    eloggerDataAccess.LogFileImport(fileName, "IMP_FL_FLD", null, null,errorFileName, ImportFileHistoryID);
                    
                }
                return false;
            }
        }
    }

}

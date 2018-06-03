using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using Logistika.Service.Common.Entities;
using System.Data.OleDb;
using System.Globalization;
using System.Threading;
using Logistika.Service.Common.DataAccessInterface.Config;
using Logistika.Service.Common.Entities.Lookup;
using Logistika.Service.Common.DataAccessInterface.User;
using Logistika.Service.Common.DataAccess.User;
using Logistika.Service.Common;
using System.Net;
using System.Xml.Linq;

namespace Test
{
    public class FileImport
    {
        static void Main(string[] args)
        {

            string pwd1 = null;
            string pwd2 = null;

            //pwd1 = Logistika.Service.Common.Encryption.EncryptionManager.BasicEncrypt(User.Password);
            //pwd2 = Logistika.Service.Common.Encryption.EncryptionManager.Encrypt(User.Password, User.Contact.Email);

            ////--aHVycnlyMTI =

            pwd1 = Logistika.Service.Common.Encryption.EncryptionManager.BasicDecrypt("aHVycnlyMTI=");

            pwd2 = Logistika.Service.Common.Encryption.EncryptionManager.BasicDecrypt("aHVycnkxMg==");
            Console.ReadKey();
            return;
            string FileName = null;
            string rootFolderPath = @"C:\API\Logistika\LogistikaService\Test\Files\ToFiles\";
             
            string destinationPath = @"C:\API\Logistika\LogistikaService\Test\Files\Archive\";

            FileName = "USERS_05222018_1.xlsx";
            string fullPathToExcel = rootFolderPath + FileName; //ie C:\Temp\YourExcel.xls
            string fullpathToArchive = destinationPath + FileName; //ie C:\Temp\YourExcel.xls


            if (new FileInfo(fullpathToArchive).Exists == false)
            {
                string connString = string.Format("Provider =Microsoft.ACE.OLEDB.12.0;Data Source=" + fullPathToExcel + ";Extended Properties='Excel 12.0;HDR=yes'", fullPathToExcel);
                DataTable dt = FileManager.GetDataTable("SELECT * FROM [Sheet1$]", connString);

                Thread.CurrentThread.CurrentCulture =
                CultureInfo.CreateSpecificCulture("en-GB");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");

                var date = DateTime.Now;
                var type = "user";
                IList<Country> PhoneCode = null;

                if (type == "Order")
                {
                    if (dt != null)
                    {
                        var distinctOrders = dt.AsEnumerable().Select(c => Convert.ToString(c["VendorOrderID"])).Distinct().ToList<string>();
                        foreach (var order in distinctOrders)
                        {
                            var orders = dt.AsEnumerable().Where(x => Convert.ToString(x["VendorOrderID"]) == order).ToList();
                            if (orders != null && orders.Count() > 0)
                            {
                                string pickupLatitude = null;
                                string pickupLongitude = null;
                                string dropoffLatitude = null;
                                string dropoffLongitude = null;
                                var first = orders[0];
                                //first["PICKUPHOUSE_FLATNO"].ToString() + first["PickupAddress"].ToString() + ", " + first["PICKUPCITY"].ToString() + ", " + first["PICKUPSTATE"].ToString() + " " + first["PICKUPPINCODE"].ToString() + ", " + first["PICKUPCOUNTRY"].ToString(),
                                //AIzaSyB9PgorTUksSwiRCNNTkfEEu1gaHIGG0Rw

                                string Pickupaddress = first["PickupAddress"].ToString() + ", " + first["PICKUPAREA_LOCALITY"].ToString() + ", " + first["PICKUPCITY"].ToString() + ", " + first["PICKUPSTATE"].ToString() + " " + first["PICKUPPINCODE"].ToString() + ", " + first["PICKUPCOUNTRY"].ToString();

                                string PickuprequestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false?key=AIzaSyB9PgorTUksSwiRCNNTkfEEu1gaHIGG0Rw", Uri.EscapeDataString(Pickupaddress));

                                WebRequest request = WebRequest.Create(PickuprequestUri);
                                WebResponse response = request.GetResponse();
                                System.Xml.Linq.XDocument xdoc = XDocument.Load(response.GetResponseStream());

                                XElement result = xdoc.Element("GeocodeResponse").Element("result");
                                if (result != null)
                                {
                                    XElement locationElement = result.Element("geometry").Element("location");
                                    XElement pickuplat = locationElement.Element("lat");
                                    XElement pickuplng = locationElement.Element("lng");

                                    pickupLatitude = pickuplat.Value;
                                    pickupLongitude = pickuplng.Value;
                                }


                                string DropOffaddress = first["DROPOFFADDRESS"].ToString() + ", " + first["DROPOFFAREA_LOCALITY"].ToString() + ", " + first["DROPOFFCITY"].ToString() + ", " + first["DROPOFFSTATE"].ToString() + " " + first["DROPOFFPINCODE"].ToString() + ", " + first["DROPOFFCOUNTRY"].ToString();

                                string DropoffrequestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(DropOffaddress));

                                WebRequest requestD = WebRequest.Create(DropoffrequestUri);
                                WebResponse responseD = requestD.GetResponse();
                                System.Xml.Linq.XDocument xdocD = XDocument.Load(responseD.GetResponseStream());

                                XElement resultD = xdocD.Element("GeocodeResponse").Element("result");
                                if (resultD != null)
                                {
                                    XElement locationElementD = resultD.Element("geometry").Element("location");
                                    XElement dropOfflat = locationElementD.Element("lat");
                                    XElement dropOfflng = locationElementD.Element("lng");

                                    dropoffLatitude = dropOfflat.Value;
                                    dropoffLongitude = dropOfflng.Value;
                                }



                                LogistikaOrderHeader tOrder = new LogistikaOrderHeader
                                {
                                    //ClientOrderSource = "FileImport_" + first["ServiceCode"].ToString(),
                                    CallType = "Portal",
                                    ClientOrderSource = "FileImport_" + Convert.ToString(System.DateTime.Now),
                                    ServiceCode = first["ServiceCode"].ToString(),
                                    OrderType = first["OrderType"].ToString(),
                                    FreightType = first["FreightType"].ToString(),
                                    OrderByName = first["OrderByName"].ToString(),
                                    OrderByPhoneNumber = first["OrderByPhoneNumber"].ToString(),
                                    OrderByEmail = first["OrderByEmail"].ToString(),
                                    PickUpDate = Convert.ToDateTime(first["PickUpDate"].ToString()),
                                    VendorOrderID = first["VendorOrderID"].ToString(),
                                    Payment = new Payment
                                    {
                                        Amount = Convert.ToDouble(first["CHARGEDAMOUNT"].ToString()),
                                        PaymentMethod = first["PAYMENTMODE"].ToString()
                                    },
                                    PickupAddress = new List<Address>
                                {
                                    new Address()
                                    {
                                        AddressLine1 = first["PickupAddress"].ToString(),
                                        Suite = first["PICKUPHOUSE_FLATNO"].ToString(),
                                        Locality = first["PICKUPAREA_LOCALITY"].ToString(),
                                        City = first["PICKUPCITY"].ToString(),
                                        StateCode = first["PICKUPSTATE"].ToString(),
                                        PostalCode = first["PICKUPPINCODE"].ToString(),
                                        CountryCode = first["PICKUPCOUNTRY"].ToString(),
                                        LandMark = first["PICKUPLANDMARK"].ToString(),
                                        Instruction = first["PICKUPINSTRUCTION"].ToString(),
                                        Latitude = pickupLatitude,
                                        Longitude = pickupLongitude
                                    }
                                },
                                    DropoffAddress = new List<Address>
                                {
                                    new Address()
                                    {
                                        AddressLine1 = first["DROPOFFADDRESS"].ToString(),
                                        Suite = first["DROPOFFHOUSE_FLATNO"].ToString(),
                                        Locality = first["DROPOFFAREA_LOCALITY"].ToString(),
                                        City = first["DROPOFFCITY"].ToString(),
                                        StateCode = first["DROPOFFSTATE"].ToString(),
                                        PostalCode = first["DROPOFFPINCODE"].ToString(),
                                        CountryCode = first["DROPOFFCOUNTRY"].ToString(),
                                        LandMark = first["DROPOFFLANDMARK"].ToString(),
                                        Instruction = first["DROPOFFINSTRUCTION"].ToString(),
                                        DropOffDate = Convert.ToDateTime(first["DropOffDate"].ToString()),
                                        Name = first["DROPOFFNAME"].ToString(),
                                        PhoneNumber = first["DROPOFFPHONE"].ToString(),
                                        Latitude = dropoffLatitude,
                                        Longitude = dropoffLongitude
                                    }
                                }
                                };
                                tOrder.LineItem = orders.Select(s => new LogistikaOrderLineItem
                                {
                                    Item = s["ITEM"].ToString(),
                                    //ItemType_FK = Convert.ToInt32(s["ItemType_FK"].ToString()),
                                    ShipmentType = s["ShipmentType"].ToString(),
                                    GoodsType = s["GOODSTYPE"].ToString(),
                                    UOM = s["UOM"].ToString(),
                                    Quantity = Convert.ToInt32(s["NOOFITEMS"].ToString()),
                                    Weight = Convert.ToDouble(s["WEIGHT"].ToString()),
                                    Length = Convert.ToDouble(s["Length"].ToString()),
                                    Width = Convert.ToDouble(s["Width"].ToString()),
                                    Height = Convert.ToDouble(s["Height"].ToString()),
                                    IsPackagingRequiredFlag = Convert.ToBoolean(s["ISPACKAGINGREQUIRED?"].ToString()),
                                    IsPermitVerifiedFlag = Convert.ToBoolean(s["ISPERMITVERIFIED?"].ToString())
                                }).ToList();


                                var service = new OrderBusinessComponent(new Logistika.Service.Common.DataAccess.Config.OrderDataAccess());
                                service.createOrder(tOrder);
                            }
                        }
                    }
                }
                if (type == "user")
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
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
                                //Locality = dt.Rows[i]["Locality"].ToString(),
                                City = dt.Rows[i]["City"].ToString(),
                                StateCode = dt.Rows[i]["State"].ToString(),
                                PostalCode = dt.Rows[i]["PinCode"].ToString(),
                                CountryCode = dt.Rows[i]["CountryCode"].ToString(),
                                LandMark = dt.Rows[i]["LandMark"].ToString()
                            }
                        }
                        };
                        var Pservice = new UserBusinessComponent(new UserDataAccess());
                        PhoneCode = Pservice.GetCountryInfo(dt.Rows[i]["CountryCode"].ToString());
                        //PhoneCode = _userDataAccess.GetCountryInfo(dt.Rows[i]["CountryCode"].ToString());

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

                        var service = new UserBusinessComponent(new UserDataAccess());
                        service.SaveUser(User, null);
                    }
                }
                if (type == "fleet")
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Fleet Fleet = new Fleet()
                        {
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
                        var service = new UserBusinessComponent(new UserDataAccess());
                        service.SaveFleet(Fleet, null, "File");
                    }
                }
                //Console.ReadLine();

                string[] fileList = Directory.GetFiles(rootFolderPath, "*.xlsx");

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
        }

        public static class FileManager
        {
            public static DataTable GetDataTable(string sql, string connectionString)
            {
                DataTable dt = new DataTable();

                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        using (OleDbDataReader rdr = cmd.ExecuteReader())
                        {
                            dt.Load(rdr);
                            return dt;
                        }
                    }
                }
            }
        }
    }


}


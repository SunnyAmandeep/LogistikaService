
using Logistika.Service.Common.BusinessComponentInterface.User;
using Logistika.Service.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Xml.Linq;
using Logistika.Service.Common.Helper;
using System.Globalization;
using System.Threading;
using System.Linq;
using Logistika.Service.Common.File;
using Logistika.Service.Common.DataAccess.Logger;

namespace Logistika.Service.Common.DataAccessInterface.Config
{
    public class OrderBusinessComponent : IOrderBusinessComponent
    {
        IOrderDataAccess _orderDataAccess = null;
        //IUserBusinessComponent _userDataAccess = null;

        string fileName = null;
        string accessfileName = null;
        string ImportFileHistoryID = null;

        public string orderImport(LogistikaOrderHeader order)
        {

            //logic
            return createOrder(order);
            
        }

        public IList<FileImport> GetFileImportList()
        {
            return _orderDataAccess.GetFileImportList();
        }

        public string OrderFileImport(string FileName, string CallType)
        {
            int ValidRecords = 0;
            int InValidRecords = 0;
            try
            {
                accessfileName = FileName + SiteConfigurationManager.GetAppSettingKey("FileImportExtension");
                fileName = ClaimHelper.CompanyId + "_" + FileName;
                FileName = fileName + SiteConfigurationManager.GetAppSettingKey("FileImportExtension");

                string logImport = null;
                string rootFolderPath = SiteConfigurationManager.GetAppSettingKey("TemplatePath") + "orderFile\\";
                string destinationPath = rootFolderPath + "\\Archive\\";
                string fullPathToExcel = rootFolderPath + accessfileName;
                string fullpathToArchive = destinationPath + accessfileName;
                string OrderResult = null;

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

                    if (CallType == "Order")
                    {
                        if (dt != null)
                        {
                            var distinctOrders = dt.AsEnumerable().Select(c => Convert.ToString(c["VendorOrderID"])).Distinct().ToList<string>();

                            //ValidRecords = distinctOrders.Count();

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
                                        ClientOrderSource = "FileImport_" + FileName,//Convert.ToString(System.DateTime.Now),
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

                                    OrderResult = createOrder(tOrder);
                                    if (string.IsNullOrEmpty(OrderResult) || OrderResult.Contains("File import Failed"))
                                    {
                                        InValidRecords = InValidRecords + 1;
                                    }
                                    if (OrderResult.StartsWith("H"))
                                    {
                                        ValidRecords = ValidRecords + 1;
                                    }
                                }
                            }
                        }
                    }

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
                    return "File was already processed";
                }
                if (!string.IsNullOrEmpty(OrderResult))
                {
                    logImport = loggerDataAccess.LogFileImport(FileName, "IMP_FL_SCCS", ValidRecords.ToString(), InValidRecords.ToString(), null, ImportFileHistoryID);
                    return FileName + ":- File imported successully.";
                }
                logImport = loggerDataAccess.LogFileImport(FileName, "IMP_FL_FLD", ValidRecords.ToString(), InValidRecords.ToString(), "IncorrectFileLayout", ImportFileHistoryID);
                return FileName + ":- File import Failed.";
            }
            catch (Exception ex)
            {
                string eFileName;
                string errorFileName;
                LoggerDataAccess eloggerDataAccess = new LoggerDataAccess();

                eFileName = "OrderError_" + fileName + "_" + DateTime.Now.ToString("MMddyyyyHH");
                errorFileName = "OrderFile\\error\\" + eFileName + ".error.txt";
                string filePath = SiteConfigurationManager.GetAppSettingKey("TemplatePath") + errorFileName;

                if (!string.IsNullOrEmpty(fileName))
                {
                    //fileName = fileName + SiteConfigurationManager.GetAppSettingKey("FileImportExtension");
                    var v = FileManager.FileErrorLog(filePath, ex.Message, "Error with File");
                    eloggerDataAccess.LogFileImport(FileName, "IMP_FL_FLD", ValidRecords.ToString(), InValidRecords.ToString(), errorFileName, ImportFileHistoryID);
                }
                return FileName + ":- File import Failed.";
            }
        }
              
        public DeliveryRep OrderTrackingInfo(string orderID)
        {
            return _orderDataAccess.OrderTrackingInfo(orderID);
        }

        public string UpdateOrderStatus(OrderUpdate Request)
        {
            return _orderDataAccess.UpdateOrderStatus(Request);
        }

        public OrderBusinessComponent(IOrderDataAccess Instance)
        {
            _orderDataAccess = Instance;
        }

        public IList<OrderStats> getOrderStats(int CompanyID, string StartDt, string EndDt)
        {
            return _orderDataAccess.getOrderStats(CompanyID, StartDt, EndDt);
        }

        public IList<OrderDriverInfo> getOrderDriverInfo(DateTime? StartDt = null, DateTime? EndDt = null, string OrderStatusCode = null, string VendorOrderID = null, string UserName = null, string CompanyAddressID = null)
        {
            if (string.IsNullOrEmpty(VendorOrderID)
                && (
                        ((string.IsNullOrEmpty(OrderStatusCode) || !StartDt.HasValue || !EndDt.HasValue)
                    || 
                        (StartDt.HasValue && StartDt.Value == DateTime.MinValue)
                    ||
                        (string.IsNullOrEmpty(CompanyAddressID))
                    ||
                        (EndDt.HasValue && EndDt.Value == DateTime.MinValue))
                    && 
                        (string.IsNullOrEmpty(UserName))     
                   ))
            {
                throw new Exception("Invalid Parameter");

            }
            return _orderDataAccess.getOrderDriverInfo(StartDt.HasValue ? StartDt.Value.ToShortDateString() : string.Empty, EndDt.HasValue ? EndDt.Value.ToShortDateString() : string.Empty, OrderStatusCode, VendorOrderID, UserName,CompanyAddressID);
        }

        public IList<DeliveryRep> getDeliveryRepknownLoc(int CompanyID)
        {
            return _orderDataAccess.getDeliveryRepknownLoc(CompanyID);
        }

        public IList<LogistikaOrderHeader> getOrderDetail(string LogistikaOrderID)
        {
            return _orderDataAccess.getOrderDetail(LogistikaOrderID);
        }

        public string createOrder(LogistikaOrderHeader orderHeader)
        {
            // orderHeader.
            string exceptionMessages = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(orderHeader.PickupAddress[0].AddressLine1) || string.IsNullOrEmpty(orderHeader.DropoffAddress[0].AddressLine1))
                {
                    exceptionMessages = "AddressLine1 is missing";
                    //throw new Exception("AddressLine1 is missing");
                }
                if (string.IsNullOrEmpty(orderHeader.PickupAddress[0].City) || string.IsNullOrEmpty(orderHeader.DropoffAddress[0].City))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "City is missing" : exceptionMessages + ", " + "City is missing";
                    //throw new Exception("City is missing");
                }
                if (string.IsNullOrEmpty(orderHeader.PickupAddress[0].StateCode) || string.IsNullOrEmpty(orderHeader.DropoffAddress[0].StateCode))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "StateCode is missing" : exceptionMessages + ", " + "StateCode is missing";
                    //throw new Exception("StateCode is missing");
                }
                if (string.IsNullOrEmpty(orderHeader.DropoffAddress[0].PhoneNumber))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "DropOff PhoneNumber is missing" : exceptionMessages + ", " + "DropOff PhoneNumber is missing";
                    //throw new Exception("CountryCode is missing");
                }

                if (string.IsNullOrEmpty(orderHeader.PickupAddress[0].CountryCode) || string.IsNullOrEmpty(orderHeader.DropoffAddress[0].CountryCode))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "CountryCode is missing" : exceptionMessages + ", " + "CountryCode is missing";
                    //throw new Exception("CountryCode is missing");
                }

                if (string.IsNullOrEmpty(orderHeader.PickUpDate.ToString()) || string.IsNullOrEmpty(orderHeader.DropoffAddress[0].DropOffDate.ToString()))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "Pickup or DropOff Date is missing" : exceptionMessages + ", " + "Pickup or DropOff Date is missing";
                    //throw new Exception("CountryCode is missing");
                }

                if (string.IsNullOrEmpty(orderHeader.FreightType))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "FreightType is missing" : exceptionMessages + ", " + "FreightType is missing";
                    //throw new Exception("FreightType is missing");
                }

                if (string.IsNullOrEmpty(orderHeader.OrderType))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "OrderType is missing" : exceptionMessages + ", " + "OrderType is missing";
                    //throw new Exception("OrderType is missing");
                }

                if (string.IsNullOrEmpty(orderHeader.OrderByName))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "OrderByName is missing" : exceptionMessages + ", " + "OrderByName is missing";
                    //throw new Exception("OrderType is missing");
                }

                if (string.IsNullOrEmpty(orderHeader.OrderByPhoneNumber))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "OrderByPhoneNumber is missing" : exceptionMessages + ", " + "OrderByPhoneNumber is missing" ;
                }

                if (string.IsNullOrEmpty(orderHeader.OrderByEmail))
                {
                    exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "OrderByEmail is missing" : exceptionMessages + ", " + "OrderByEmail is missing";
                    //throw new Exception("OrderType is missing");
                }

                foreach (var v in orderHeader.LineItem)
                {
                    if (string.IsNullOrEmpty(v.Quantity.ToString()))
                    {
                        exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "Quantity is missing" : exceptionMessages + ", " + "Quantity is missing";
                    }
                    if (string.IsNullOrEmpty(v.Weight.ToString()))
                    {
                        exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "Weight is missing" : exceptionMessages + ", " + "Weight is missing";
                    }
                    if (string.IsNullOrEmpty(v.Item.ToString()))
                    {
                        exceptionMessages = string.IsNullOrEmpty(exceptionMessages) ? "Item is missing" : exceptionMessages + ", " + "Item is missing";
                    }
                }
                
                if (!string.IsNullOrEmpty(exceptionMessages))
                {
                    throw new Exception(exceptionMessages);
                }

                return _orderDataAccess.createOrder(orderHeader);
            }
            catch (Exception ex)
            {
                string eFileName;
                string errorFileName;

                eFileName = "OrderError_" + fileName + "_" + DateTime.Now.ToString("MMddyyyyHH");
                errorFileName = "OrderFile\\error\\" + eFileName + ".error.txt";
                string filePath = SiteConfigurationManager.GetAppSettingKey("TemplatePath") + errorFileName;

                LoggerDataAccess eloggerDataAccess = new LoggerDataAccess();

                if (!string.IsNullOrEmpty(fileName))
                {
                    if (string.IsNullOrEmpty(orderHeader.VendorOrderID))
                    {
                        orderHeader.VendorOrderID = "Empty";
                    }
                    fileName = fileName + SiteConfigurationManager.GetAppSettingKey("FileImportExtension");
                    var v = FileManager.FileErrorLog(filePath, ex.Message, orderHeader.VendorOrderID);
                    eloggerDataAccess.LogFileImport(fileName, "IMP_FL_FLD", null, null, errorFileName, ImportFileHistoryID);
                }
                return fileName + ":- File import Failed.";
            }
        }

        public string SubmitOrderQuote(OrderQuote orderQuote)
        {
            // orderHeader.
            return _orderDataAccess.SubmitOrderQuote(orderQuote);
        }

        public IList<OrderStatsArea> getOrderStatsArea(int CompanyID, string StartDt, string EndDt)
        {
            return _orderDataAccess.getOrderStatsArea(CompanyID, StartDt, EndDt);
        }

        public WaveModal GetWavePlanner(int? WaveID = 0, int? CompanyAddressID = 0)
        {
            return _orderDataAccess.GetWavePlanner(WaveID, CompanyAddressID);
        }

        public bool UpdateInsertWavePlanner(IList<WavedOrders> WaveOrder)
        {
            return _orderDataAccess.UpdateInsertWavePlanner(WaveOrder);
        }

    }

}

using Logistika.Service.Common.DataAccessInterface.Config;
using Logistika.Service.Common.EFDataContext;
using Logistika.Service.Common.Entities;
using Logistika.Service.Common.Entities.Lookup;
using Logistika.Service.Common.Extension;
using Logistika.Service.Common.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Logistika.Service.Common.DataAccess.Config
{
    public class OrderDataAccess : BaseDataAccess, IOrderDataAccess
    {
        public OrderDataAccess()
            : base()
        {

        }

        public IList<FileImport> GetFileImportList()
        {
            IList<FileImport> FileLst = null;
            var ds = GetDataSetResult("dbo.proc_ins_upd_Import_File",
                       new SqlParameter("CompanyID", ClaimHelper.CompanyId),
                       new SqlParameter("Operation", "G")
                    );

            FileLst = (from row in ds.Tables[0].AsEnumerable()
                                        select new Entities.FileImport
                                        {
                                            FileName = Convert.ToString(row["FileName"].CheckDBNull()),
                                            ValidRecords = Convert.ToInt32(row["ValidRecords"].CheckDBNull()),
                                            InValidRecords = Convert.ToInt32(row["InValidRecords"].CheckDBNull()),
                                            Status = Convert.ToString(row["Status"].CheckDBNull()),
                                            StatusDt = Convert.ToDateTime(row["StatusDt"].CheckDBNull()),
                                            LastModifiedBy = Convert.ToString(row["LastModifiedBy"].CheckDBNull()),
                                            ErrorFileName = Convert.ToString(row["ErrorFileName"].CheckDBNull()),
                                        }).ToList();
           return FileLst;
        }

        public string UpdateOrderStatus(OrderUpdate Request)
        {
            var message = GetOutputParameter("OutputMessage", ParameterDirection.InputOutput);

            var ds = GetDataSetResult("dbo.proc_ins_upd_Order_Status",
                   new SqlParameter("RequestType", Request.RequestType),
                   new SqlParameter("OrderID", Request.OrderID),
                   new SqlParameter("PickupDate", !string.IsNullOrEmpty(Request.PickupDate) ? (object)Request.PickupDate : DBNull.Value),
                   new SqlParameter("StatusDt", !string.IsNullOrEmpty(Request.StatusDt) ? (object)Request.StatusDt : DBNull.Value),
                   new SqlParameter("LastModifiedBy", ClaimHelper.UserName == "" ? DBNull.Value : (object)ClaimHelper.UserName),
                   message);
            return message.Value.ToString();
        }

        public DeliveryRep OrderTrackingInfo(string orderID)
        {
            string orderid = Encryption.EncryptionManager.Decrypt(orderID, "$Talluri$");
            string pwd = Encryption.EncryptionManager.Encrypt("test", "truck@truck.com");

            DeliveryRep orderInfo = null;
            var ds = GetDataSetResult("[dbo].[proc_get_Order_TrackingInfo]",
                   new SqlParameter("OrderID", orderid)
                );

            if (ds.IsDataSetNotNullAndTableHasRows())
            {
                orderInfo = (from row in ds.Tables[0].AsEnumerable()
                             select new Entities.DeliveryRep
                             {
                                 FirstName = Convert.ToString(row["FirstName"].CheckDBNull()),
                                 LastName = Convert.ToString(row["LastName"].CheckDBNull()),
                                 PhoneNumber = Convert.ToString(row["PhoneNumber"].CheckDBNull()),
                                 ProfilePic = Convert.ToString(row["ProfilePic"].CheckDBNull()),
                                 //ProfilePic = SiteConfigurationManager.GetAppSettingKey("WebsiteProfilePicSite") + Convert.ToString(row["ProfilePic"].CheckDBNull()),
                                 Longitutue = Convert.ToDouble(row["Longitude"].CheckDBNull()),
                                 Latitude = Convert.ToDouble(row["Latitude"].CheckDBNull()),
                                 Company_PK = Convert.ToInt32(row["Company_PK"].CheckDBNull()),
                                 LastknownDt = Convert.ToDateTime(row["LastknownDt"].CheckDBNull()),
                                 FrameApplicationUserID = Convert.ToInt32(row["FrameApplicationUserID"].CheckDBNull()),
                             }).FirstOrDefault();
            }
            return orderInfo;
        }

        public string createOrder(LogistikaOrderHeader orderHeader)
        {
            int PickupAddressID = 0;
            int ParentOrderHeader_PK = 0;
            string ParentLogistikaOrderId = "";
            //int DropOffAddressID = 0;
            var LogistikaOrderID = GetOutputParameter("LogistikaOrderID", ParameterDirection.InputOutput);
            var OrderHeader_PK = GetOutputParameter("OrderHeader_PK", ParameterDirection.InputOutput);

            Dictionary<Address, int> frameworkAddressID = new Dictionary<Address, int>();
            //Insert Pickup Address
            if (orderHeader.PickupAddress != null)
            {
                var addId = GetOutputParameter("FrameworkAddressID");
                var ds = Exec("dbo.proc_ins_FrameworkAddress",
                                new SqlParameter("LandMark", orderHeader.PickupAddress[0].LandMark),
                                new SqlParameter("AddressLine1", orderHeader.PickupAddress[0].AddressLine1),
                                new SqlParameter("Locality", orderHeader.PickupAddress[0].Locality),
                                new SqlParameter("Suite", orderHeader.PickupAddress[0].Suite),
                                new SqlParameter("City", orderHeader.PickupAddress[0].City),
                                new SqlParameter("District", orderHeader.PickupAddress[0].District),
                                new SqlParameter("StateCode", orderHeader.PickupAddress[0].StateCode),
                                new SqlParameter("PostalCode", orderHeader.PickupAddress[0].PostalCode),
                                new SqlParameter("CountryCode", orderHeader.PickupAddress[0].CountryCode),
                                new SqlParameter("Latitude", orderHeader.PickupAddress[0].Latitude),
                                new SqlParameter("Longitude", orderHeader.PickupAddress[0].Longitude),
                                addId);
                PickupAddressID = Convert.ToInt32(addId.Value);
            }
            //add drp address
            if (orderHeader.DropoffAddress != null && orderHeader.DropoffAddress.Count() > 0)
            {
                foreach (Address v in orderHeader.DropoffAddress)
                {
                    var addId = GetOutputParameter("FrameworkAddressID");
                    var ds = Exec("dbo.proc_ins_FrameworkAddress",
                                new SqlParameter("LandMark", v.LandMark),
                                new SqlParameter("AddressLine1", v.AddressLine1),
                                new SqlParameter("Locality", v.Locality),
                                new SqlParameter("Suite", v.Suite),
                                new SqlParameter("City", v.City),
                                new SqlParameter("District", v.District),
                                new SqlParameter("StateCode", v.StateCode),
                                new SqlParameter("PostalCode", v.PostalCode),
                                new SqlParameter("CountryCode", v.CountryCode),
                                new SqlParameter("Latitude", v.Latitude),
                                new SqlParameter("Longitude", v.Longitude),
                                addId);
                    //Get Framework Id and map
                    frameworkAddressID.Add(v, Convert.ToInt32(addId.Value));
                }
            }

            if (frameworkAddressID.Count() == 1)
            {
                OrderHeader_PK = GetOutputParameter("OrderHeader_PK", ParameterDirection.InputOutput);
                LogistikaOrderID = GetOutputParameter("LogistikaOrderID", ParameterDirection.InputOutput);
                var ParentsuccessHeader = Exec("proc_ins_Order_Header",
                             new SqlParameter("Company_FK", 2),//ClaimHelper.CompanyId),//2),//ClaimHelper.CompanyId),
                             new SqlParameter("ClientOrderSource", orderHeader.ClientOrderSource),
                             new SqlParameter("VendorOrderID", orderHeader.VendorOrderID),
                             new SqlParameter("DropOffDate", frameworkAddressID.FirstOrDefault().Key.DropOffDate),
                             new SqlParameter("DropOffFrameworkAddress_FK", frameworkAddressID.FirstOrDefault().Value),
                             new SqlParameter("FreightType", orderHeader.FreightType),
                             new SqlParameter("LastModifiedBy", orderHeader.LastModifiedBy == "" ? DBNull.Value : (object)ClaimHelper.UserName),
                             new SqlParameter("OrderByName", orderHeader.OrderByName),
                             new SqlParameter("OrderByEmail", orderHeader.OrderByEmail),
                             new SqlParameter("OrderByPhoneNumber", orderHeader.OrderByPhoneNumber),
                             new SqlParameter("PrimaryEmailAddress", orderHeader.PrimaryEmailAddress),
                             new SqlParameter("OrderType", orderHeader.OrderType),
                             new SqlParameter("ParentOrderHeader_PK", ParentOrderHeader_PK == 0 ? DBNull.Value : (object)ParentOrderHeader_PK),
                             new SqlParameter("PickUpDate", orderHeader.PickUpDate),
                             new SqlParameter("PickupFrameworkAddress_FK", PickupAddressID),
                             new SqlParameter("ServiceCode", orderHeader.ServiceCode),
                             new SqlParameter("ShipToName", frameworkAddressID.FirstOrDefault().Key.Name),
                             new SqlParameter("ShipToEmail", frameworkAddressID.FirstOrDefault().Key.Email),
                             new SqlParameter("ShipToPhoneNumber", frameworkAddressID.FirstOrDefault().Key.PhoneNumber),
                             new SqlParameter("PickupInstruction", orderHeader.PickupAddress[0].Instruction),
                             new SqlParameter("CompanyAddress_FK", 4),// frameworkAddressID.FirstOrDefault().Key.CompanyAddress_FK),
                             new SqlParameter("IsInsuredFlag", orderHeader.IsInsuredFlag),
                             new SqlParameter("IsCancellableFlag", orderHeader.IsCancellableFlag),
                             new SqlParameter("IsSalesTaxPaid", orderHeader.IsSalesTaxPaidFlag),
                             new SqlParameter("InsuredAmount", orderHeader.InsuredAmount),
                             LogistikaOrderID,
                             OrderHeader_PK);
                // ClaimHelper.CompanyId

                if (ParentOrderHeader_PK == 0)
                {
                    ParentOrderHeader_PK = Convert.ToInt32(OrderHeader_PK.Value);
                    ParentLogistikaOrderId = Convert.ToString(LogistikaOrderID.Value);
                }

                var Allproducts = from b in orderHeader.LineItem select b;
                foreach (LogistikaOrderLineItem product in Allproducts)
                {
                    var successOrderLineItem = Exec("proc_ins_Order_Line_Item",
                                                    new SqlParameter("OrderHeader_FK", OrderHeader_PK.Value),
                                                    new SqlParameter("ItemType_FK", product.ItemType_FK == 0 ? DBNull.Value : (object)product.ItemType_FK),
                                                    new SqlParameter("Item", product.Item),
                                                    new SqlParameter("Quantity", product.Quantity),
                                                    new SqlParameter("Source", product.Source),
                                                    new SqlParameter("Image", product.Image),
                                                    new SqlParameter("Length", product.Length),
                                                    new SqlParameter("Width", product.Width),
                                                    new SqlParameter("Height", product.Height),
                                                    new SqlParameter("Weight", product.Weight),
                                                    new SqlParameter("FreightType", product.FreightType),
                                                    new SqlParameter("ShipmentType", product.ShipmentType),
                                                    new SqlParameter("GoodsType", product.GoodsType),
                                                    new SqlParameter("UOM", product.UOM),
                                                    new SqlParameter("ContainerTrackingCount", product.ContainerTrackingCount),
                                                    new SqlParameter("IsPermitVerifiedFlag", product.IsPermitVerifiedFlag),
                                                    new SqlParameter("IsTrackableFlag", product.IsTrackableFlag),
                                                    new SqlParameter("IsPackagingRequiredFlag", product.IsPackagingRequiredFlag),
                                                    new SqlParameter("LastModifiedBy", orderHeader.LastModifiedBy == "" ? DBNull.Value : (object)ClaimHelper.UserName));
                }

                // Only create Payments for Consumer Orders
                if ((orderHeader.OrderType == ("Consumer")) || ((orderHeader.OrderType == ("Corporate") && orderHeader.CompanyID != 2)))
                {
                    var PaymentSuccess = Exec("proc_ins_upd_Payment",
                            new SqlParameter("OrderHeader_FK", OrderHeader_PK.Value),
                            new SqlParameter("PaymentMethod", orderHeader.Payment.PaymentMethod),
                            new SqlParameter("Amount", orderHeader.Payment.Amount),
                            new SqlParameter("TaxAmount", orderHeader.Payment.TaxAmount == 0 ? DBNull.Value : (object)orderHeader.Payment.TaxAmount),
                            new SqlParameter("DiscountAmount", orderHeader.Payment.DiscountAmount == 0 ? DBNull.Value : (object)orderHeader.Payment.DiscountAmount),
                            new SqlParameter("LastModifiedBy", orderHeader.LastModifiedBy == "" ? DBNull.Value : (object)ClaimHelper.UserName));
                }
            }
            else if (frameworkAddressID.Count() > 1)
            {
                OrderHeader_PK = GetOutputParameter("OrderHeader_PK", ParameterDirection.InputOutput);
                LogistikaOrderID = GetOutputParameter("LogistikaOrderID", ParameterDirection.InputOutput);
                var ParentsuccessHeader = Exec("proc_ins_Order_Header",
                             new SqlParameter("Company_FK", ClaimHelper.CompanyId),//2),//ClaimHelper.CompanyId),
                             new SqlParameter("ClientOrderSource", orderHeader.ClientOrderSource),
                             new SqlParameter("VendorOrderID", orderHeader.VendorOrderID),
                             new SqlParameter("DropOffDate", DBNull.Value),
                             new SqlParameter("DropOffFrameworkAddress_FK", DBNull.Value),
                             new SqlParameter("FreightType", orderHeader.FreightType),
                             new SqlParameter("LastModifiedBy", orderHeader.LastModifiedBy == "" ? DBNull.Value : (object)ClaimHelper.UserName),
                             new SqlParameter("OrderByName", orderHeader.OrderByName),
                             new SqlParameter("OrderByEmail", orderHeader.OrderByEmail),
                             new SqlParameter("OrderByPhoneNumber", orderHeader.OrderByPhoneNumber),
                             new SqlParameter("PrimaryEmailAddress", orderHeader.PrimaryEmailAddress),
                             new SqlParameter("OrderType", orderHeader.OrderType),
                             new SqlParameter("ParentOrderHeader_PK", ParentOrderHeader_PK == 0 ? DBNull.Value : (object)ParentOrderHeader_PK),
                             new SqlParameter("PickUpDate", orderHeader.PickUpDate),
                             new SqlParameter("PickupFrameworkAddress_FK", PickupAddressID),
                             new SqlParameter("ServiceCode", orderHeader.ServiceCode),
                             new SqlParameter("ShipToName", DBNull.Value),
                             new SqlParameter("ShipToEmail", DBNull.Value),
                             new SqlParameter("ShipToPhoneNumber", DBNull.Value),
                             new SqlParameter("PickupInstruction", orderHeader.PickupAddress[0].Instruction),
                             new SqlParameter("CompanyAddress_FK", DBNull.Value),
                             new SqlParameter("IsInsuredFlag", orderHeader.IsInsuredFlag),
                             new SqlParameter("IsCancellableFlag", orderHeader.IsCancellableFlag),
                             new SqlParameter("IsSalesTaxPaid", orderHeader.IsSalesTaxPaidFlag),
                             new SqlParameter("InsuredAmount", orderHeader.InsuredAmount),
                             LogistikaOrderID,
                             OrderHeader_PK);
                // ClaimHelper.CompanyId

                if (ParentOrderHeader_PK == 0)
                {
                    ParentOrderHeader_PK = Convert.ToInt32(OrderHeader_PK.Value);
                    ParentLogistikaOrderId = Convert.ToString(LogistikaOrderID.Value);
                }

                var Allproducts = from b in orderHeader.LineItem select b;
                foreach (LogistikaOrderLineItem product in Allproducts)
                {
                    var successOrderLineItem = Exec("proc_ins_Order_Line_Item",
                                    new SqlParameter("OrderHeader_FK", OrderHeader_PK.Value),
                                    new SqlParameter("ItemType_FK", product.ItemType_FK),
                                    new SqlParameter("Item", product.Item),
                                    new SqlParameter("Quantity", product.Quantity),
                                    new SqlParameter("Source", product.Source),
                                    new SqlParameter("Image", product.Image),
                                    new SqlParameter("Length", product.Length),
                                    new SqlParameter("Width", product.Width),
                                    new SqlParameter("Height", product.Height),
                                    new SqlParameter("Weight", product.Weight),
                                    new SqlParameter("FreightType", product.FreightType),
                                    new SqlParameter("ShipmentType", product.ShipmentType),
                                    new SqlParameter("GoodsType", product.GoodsType),
                                    new SqlParameter("UOM", product.UOM),
                                    new SqlParameter("ContainerTrackingCount", product.ContainerTrackingCount),
                                    new SqlParameter("IsPermitVerifiedFlag", product.IsPermitVerifiedFlag),
                                    new SqlParameter("IsTrackableFlag", product.IsTrackableFlag),
                                    new SqlParameter("IsPackagingRequiredFlag", product.IsPackagingRequiredFlag),
                                    new SqlParameter("LastModifiedBy", orderHeader.LastModifiedBy == "" ? DBNull.Value : (object)ClaimHelper.UserName)
                          );
                }

                // Only create Payments for Consumer Orders
                if ((orderHeader.OrderType == ("Consumer")) || ((orderHeader.OrderType == ("Corporate") && orderHeader.CompanyID != 2)))
                {
                    var PaymentSuccess = Exec("proc_ins_upd_Payment",
                            new SqlParameter("OrderHeader_FK", OrderHeader_PK.Value),
                            new SqlParameter("PaymentMethod", orderHeader.Payment.PaymentMethod),
                            new SqlParameter("Amount", orderHeader.Payment.Amount),
                            new SqlParameter("TaxAmount", orderHeader.Payment.TaxAmount == 0 ? DBNull.Value : (object)orderHeader.Payment.TaxAmount),
                            new SqlParameter("DiscountAmount", orderHeader.Payment.DiscountAmount == 0 ? DBNull.Value : (object)orderHeader.Payment.DiscountAmount),
                            new SqlParameter("LastModifiedBy", orderHeader.LastModifiedBy == "" ? DBNull.Value : (object)ClaimHelper.UserName));
                }

                if (frameworkAddressID.Count() >= 1)
                {
                    foreach (var addressKey in frameworkAddressID.Keys)
                    {

                        var products = from b in orderHeader.LineItem where b.DropoffAddressID == addressKey.FrameworkAddressID select b;
                        if (products != null && products.Count() > 0)
                        {
                            OrderHeader_PK = GetOutputParameter("OrderHeader_PK", ParameterDirection.InputOutput);
                            LogistikaOrderID = GetOutputParameter("LogistikaOrderID", ParameterDirection.InputOutput);
                            var successHeader = Exec("proc_ins_Order_Header",
                                      new SqlParameter("Company_FK", ClaimHelper.CompanyId),
                                      new SqlParameter("ClientOrderSource", orderHeader.ClientOrderSource),
                                      new SqlParameter("VendorOrderID", orderHeader.VendorOrderID),
                                      new SqlParameter("DropOffDate", addressKey.DropOffDate),
                                      new SqlParameter("DropOffFrameworkAddress_FK", frameworkAddressID[addressKey]),
                                      new SqlParameter("FreightType", orderHeader.FreightType),
                                      new SqlParameter("LastModifiedBy", orderHeader.LastModifiedBy == "" ? DBNull.Value : (object)ClaimHelper.UserName),
                                      new SqlParameter("OrderByName", orderHeader.OrderByName),
                                      new SqlParameter("OrderByEmail", orderHeader.OrderByEmail),
                                      new SqlParameter("OrderByPhoneNumber", orderHeader.OrderByPhoneNumber),
                                      new SqlParameter("PrimaryEmailAddress", orderHeader.PrimaryEmailAddress),
                                      new SqlParameter("OrderType", orderHeader.OrderType),
                                      new SqlParameter("ParentOrderHeader_PK", ParentOrderHeader_PK),
                                      new SqlParameter("PickUpDate", DBNull.Value),
                                      new SqlParameter("PickupFrameworkAddress_FK", DBNull.Value),
                                      new SqlParameter("ServiceCode", orderHeader.ServiceCode),
                                      new SqlParameter("ShipToName", addressKey.Name),
                                      new SqlParameter("ShipToEmail", addressKey.Email),
                                      new SqlParameter("ShipToPhoneNumber", addressKey.PhoneNumber),
                                      new SqlParameter("DropOffInstruction", addressKey.Instruction),
                                      new SqlParameter("CompanyAddress_FK", addressKey.CompanyAddress_FK == 0 ? DBNull.Value : (object)addressKey.CompanyAddress_FK),
                                      new SqlParameter("IsInsuredFlag", orderHeader.IsInsuredFlag),
                                      new SqlParameter("IsCancellableFlag", orderHeader.IsCancellableFlag),
                                      new SqlParameter("IsSalesTaxPaid", orderHeader.IsSalesTaxPaidFlag),
                                      new SqlParameter("InsuredAmount", orderHeader.InsuredAmount),
                                      LogistikaOrderID,
                                      OrderHeader_PK
                            );


                            foreach (LogistikaOrderLineItem product in products)
                            {
                                var successOrderLineItem = Exec("proc_ins_Order_Line_Item",
                                                new SqlParameter("OrderHeader_FK", OrderHeader_PK.Value),
                                                new SqlParameter("ItemType_FK", product.ItemType_FK),
                                                new SqlParameter("Item", product.Item),
                                                new SqlParameter("Quantity", product.Quantity),
                                                new SqlParameter("Source", product.Source),
                                                new SqlParameter("Image", product.Image),
                                                new SqlParameter("Length", product.Length),
                                                new SqlParameter("Width", product.Width),
                                                new SqlParameter("Height", product.Height),
                                                new SqlParameter("Weight", product.Weight),
                                                new SqlParameter("FreightType", product.FreightType),
                                                new SqlParameter("ShipmentType", product.ShipmentType),
                                                new SqlParameter("GoodsType", product.GoodsType),
                                                new SqlParameter("UOM", product.UOM),
                                                new SqlParameter("ContainerTrackingCount", product.ContainerTrackingCount),
                                                new SqlParameter("IsPermitVerifiedFlag", product.IsPermitVerifiedFlag),
                                                new SqlParameter("IsTrackableFlag", product.IsTrackableFlag),
                                                new SqlParameter("IsPackagingRequiredFlag", product.IsPackagingRequiredFlag),
                                                new SqlParameter("LastModifiedBy", orderHeader.LastModifiedBy == "" ? DBNull.Value : (object)ClaimHelper.UserName)
                                      );
                            }
                        }
                    }
                }

            }
            return ParentLogistikaOrderId;
        }

        public string SubmitOrderQuote(OrderQuote orderQuote)
        {
            Exec("proc_ins_Order_Header_Status_History_Quote",
                new SqlParameter("CompanyID", ClaimHelper.CompanyId),
                new SqlParameter("OrderID", orderQuote.OrderID),
                new SqlParameter("QuoteExpedited", orderQuote.QuoteExpedited == 0 ? DBNull.Value : (object)orderQuote.QuoteExpedited),
                new SqlParameter("QuoteExpress", orderQuote.QuoteExpress == 0 ? DBNull.Value : (object)orderQuote.QuoteExpress),
                new SqlParameter("QuoteEconomy", orderQuote.QuoteEconomy == 0 ? DBNull.Value : (object)orderQuote.QuoteEconomy),
                new SqlParameter("OptedService", orderQuote.OptedService == null ? DBNull.Value : (object)orderQuote.OptedService),
                new SqlParameter("PaymentMethod", orderQuote.Payment == null ? DBNull.Value : (object)orderQuote.Payment.PaymentMethod),
                new SqlParameter("Amount", orderQuote.Payment == null ? DBNull.Value : (object)orderQuote.Payment.Amount),
                new SqlParameter("TaxAmount", orderQuote.Payment == null ? DBNull.Value : (object)orderQuote.Payment.TaxAmount),
                new SqlParameter("DiscountAmount", orderQuote.Payment == null ? DBNull.Value : (object)orderQuote.Payment.DiscountAmount),
                new SqlParameter("LastModifiedBy", ClaimHelper.UserName));

            return "Success";
        }

        //Wave Planner
        public bool UpdateInsertWavePlanner(IList<WavedOrders> WaveOrder)
        {

            foreach (var order in WaveOrder)
            {
                var success = Exec("dbo.proc_ins_upd_WaveOrder",
                        new SqlParameter("WaveID", order.Wave_PK),
                        new SqlParameter("OrderHeaderID", order.OrderHeader_FK),
                        new SqlParameter("FleetID", order.FleetID),
                        new SqlParameter("Sequence", order.Sequence),
                        new SqlParameter("OperationType", order.OperationType),
                        new SqlParameter("IsActive", order.IsActive),
                        new SqlParameter("WaveOrderID", order.WaveOrderID == 0 ? DBNull.Value : (object)order.WaveOrderID),
                        new SqlParameter("LastModifiedBy", ClaimHelper.UserName));
                if (!success)
                {
                    return false;
                }
            }
            return true;
        }

        public WaveModal GetWavePlanner(int? WaveID = 0, int? CompanyAddressID = 0)
        {
            WaveModal WavePlanner = new WaveModal();

            var ds = GetDataSetResult("dbo.proc_ins_upd_Wave",
                   new SqlParameter("CompanyID", ClaimHelper.CompanyId),
                   new SqlParameter("WaveID", (!WaveID.HasValue || WaveID.Value == 0) ? DBNull.Value : (object)WaveID),
                   new SqlParameter("CompanyAddressID", !CompanyAddressID.HasValue || CompanyAddressID.Value == 0 ? ClaimHelper.UserName : (object)CompanyAddressID),
                   new SqlParameter("Operation", "G")

                );
            if (ds.IsDataSetNotNullAndTableHasRows())
            {

                WavePlanner.WavePlannerList = ds.Tables[0].AsEnumerable().Select(row =>
                    new WavePlanner()
                    {
                        Wave_PK = Convert.ToInt32(row["Wave_PK"].CheckDBNull()),
                        EmployeeID = Convert.ToString(row["EmployeeID"].CheckDBNull()),
                        WaveNumber = Convert.ToString(row["WaveNumber"].CheckDBNull()),
                        WaveDate = Convert.ToDateTime(row["WaveDate"].CheckDBNull()),
                        WaveStartTime = Convert.ToString(row["WaveStartTime"].CheckDBNull()),
                        WaveEndTime = Convert.ToString(row["WaveEndTime"].CheckDBNull()),
                        WaveStatus = Convert.ToString(row["WaveStatus"].CheckDBNull()),
                        FirstName = Convert.ToString(row["FirstName"].CheckDBNull()),
                        LastName = Convert.ToString(row["LastName"].CheckDBNull()),
                        FleetID = Convert.ToString(row["FleetID"].CheckDBNull()),
                        LastModifiedBy = Convert.ToString(row["LastModifiedBy"].CheckDBNull()),
                        LastModifiedDt = Convert.ToDateTime(row["LastModifiedDt"].CheckDBNull()),
                    }
                    ).ToList();
            }
            if (ds.IsDataSetNotNullAndTableHasRows(1))
            {

                WavePlanner.AvailableOrders = ds.Tables[1].AsEnumerable().Select(row =>
                    new WaveAvailableOrders()
                    {
                        OrderHeader_PK = Convert.ToInt32(row["OrderHeader_PK"].CheckDBNull()),
                        LogistikaOrderID = Convert.ToString(row["LogistikaOrderID"].CheckDBNull()),
                        PickUpDate = Convert.ToDateTime(row["PickUpDate"].CheckDBNull()),
                        DropOffDate = Convert.ToDateTime(row["DropOffDate"].CheckDBNull()),
                        OrderType = Convert.ToString(row["OrderType"].CheckDBNull()),
                        OperationType = Convert.ToString(row["OperationType"].CheckDBNull()),
                        OrderStatus = Convert.ToString(row["OrderStatus"].CheckDBNull()),
                        OrderAddress = new Address()
                        {
                            AddressLine1 = Convert.ToString(row["AddressLine1"].CheckDBNull()),
                            Locality = Convert.ToString(row["Locality"].CheckDBNull()),
                            City = Convert.ToString(row["Locality"].CheckDBNull()),
                            StateCode = Convert.ToString(row["Locality"].CheckDBNull()),
                            PostalCode = Convert.ToString(row["Locality"].CheckDBNull()),
                            
                        },
                    }
                    ).ToList();
            }
            if (ds.IsDataSetNotNullAndTableHasRows(2))
            {

                WavePlanner.WavedOrders = ds.Tables[2].AsEnumerable().Select(row =>
                    new WavedOrders()
                    {
                        Wave_PK = Convert.ToInt32(row["Wave_PK"].CheckDBNull()),
                        OrderHeader_FK = Convert.ToInt32(row["OrderHeader_FK"].CheckDBNull()),
                        LogistikaOrderID = Convert.ToString(row["LogistikaOrderID"].CheckDBNull()),
                        FleetID = Convert.ToString(row["FleetID"].CheckDBNull()),
                        OperationType = Convert.ToString(row["OperationType"].CheckDBNull()),
                        Sequence = Convert.ToString(row["Sequence"].CheckDBNull()),
                        IsActive = Convert.ToBoolean(row["IsActive"].CheckDBNull()),
                    
                    }
                    ).ToList();
            }
            return WavePlanner;
        }

        // Order Stats get_Method
        public IList<OrderStats> getOrderStats(int CompanyID, string StartDt, string EndDt)
        {
            IList<OrderStats> StatList = null;
            var ds = GetDataSetResult("[dbo].[proc_get_OrderStats]",
                   new SqlParameter("CompanyID", CompanyID),
                   new SqlParameter("StartDt", StartDt),
                   new SqlParameter("EndDt", EndDt)
                );

            StatList = (from row in ds.Tables[0].AsEnumerable()
                        select new Entities.OrderStats
                        {
                            ReportStatus = Convert.ToString(row["ReportStatus"].CheckDBNull()),
                            OrderCount = Convert.ToInt32(row["OrderCount"].CheckDBNull()),
                            DeliveryCategory = Convert.ToString(row["DeliveryCategory"].CheckDBNull()),
                            PickupSLAMet = Convert.ToInt32(row["PickupSLAMet"].CheckDBNull()),
                            DropSLAMet = Convert.ToInt32(row["DropSLAMet"].CheckDBNull()),
                        }).ToList();
            return StatList;
        }

        // Order Stats by Area get_Method
        public IList<OrderStatsArea> getOrderStatsArea(int CompanyID, string StartDt, string EndDt)
        {
            IList<OrderStatsArea> StatAreaList = null;
            var ds = GetDataSetResult("[dbo].[proc_get_OrderStatsByArea]",
                   new SqlParameter("CompanyID", CompanyID),
                   new SqlParameter("StartDt", StartDt),
                   new SqlParameter("EndDt", EndDt)
                );

            StatAreaList = (from row in ds.Tables[0].AsEnumerable()
                            select new Entities.OrderStatsArea
                            {
                                CountryCode = Convert.ToString(row["CountryCode"].CheckDBNull()),
                                StateCode = Convert.ToString(row["StateCode"].CheckDBNull()),
                                City = Convert.ToString(row["City"].CheckDBNull()),
                                Area = Convert.ToString(row["Area"].CheckDBNull()),
                                TotalOrders = Convert.ToInt32(row["TotalOrders"].CheckDBNull()),
                            }).ToList();
            return StatAreaList;
        }

        public  IList<LogistikaOrderHeader> getOrderDetail(string LogistikaOrderID)
        {
            //  IList<OrderDetail> detail = null;
            IList<LogistikaOrderHeader> orders = null;

            var ds = GetDataSetResult("[dbo].[proc_get_OrderDetail]",
                   new SqlParameter("LogistikaOrderID", LogistikaOrderID)
                );

            if (ds.IsDataSetNotNullAndTableHasRows())
            {

                orders = (from row in ds.Tables[0].AsEnumerable()
                          select new Entities.LogistikaOrderHeader
                          {
                              //Fill other fields
                              //Change BAL,Iterface and controler
                              //test, see if new fields are populating
                              CompanyID             = Convert.ToInt32(row["CompanyID"].CheckDBNull()),
                              ClientOrderSource     = Convert.ToString(row["ClientOrderSource"].CheckDBNull()),
                              VendorOrderID         = Convert.ToString(row["VendorOrderID"].CheckDBNull()),
                              OrderHeaderID         = Convert.ToString(row["OrderHeaderID"].CheckDBNull()),
                              WaveID                = Convert.ToInt32(row["WaveID"].CheckDBNull()),
                              LogistkaOrderID       = Convert.ToString(row["OrderID"].CheckDBNull()),
                              ParentLogistikaOrderID = Convert.ToString(row["ParentLogistikaOrderID"].CheckDBNull()),
                              OrderDate             = Convert.ToDateTime(row["OrderDate"].CheckDBNull()),
                              Status                = Convert.ToString(row["Status"].CheckDBNull()),
                              TrackingNumber        = Convert.ToString(row["TrackingNumber"].CheckDBNull()),
                              TrackingUrl           = Convert.ToString(row["TrackingUrl"].CheckDBNull()),
                              OrderType             = Convert.ToString(row["OrderType"].CheckDBNull()),
                              OrderByName           = Convert.ToString(row["OrderByName"].CheckDBNull()),
                              OrderByEmail          = Convert.ToString(row["OrderByEmail"].CheckDBNull()),
                              OrderByPhoneNumber    = Convert.ToString(row["OrderByPhoneNumber"].CheckDBNull()),
                              ItemType              = Convert.ToString(row["ItemType"].CheckDBNull()),
                              FreightType           = Convert.ToString(row["FreightType"].CheckDBNull()),
                              DriverName            = Convert.ToString(row["DriverName"].CheckDBNull()),
                              VehicleNumber         = Convert.ToString(row["VehicleNumber"].CheckDBNull()),
                              PickUpDate            = Convert.ToDateTime(row["PickUpDate"].CheckDBNull()),
                              //DropOffDate           = Convert.ToDateTime(row["DropOffDate"].CheckDBNull()),
                              CustomerName          = Convert.ToString(row["CustomerName"].CheckDBNull()),
                              CustomerPhone         = Convert.ToString(row["CustomerPhone"].CheckDBNull()),
                              CustomerEmail         = Convert.ToString(row["CustomerEmail"].CheckDBNull()),
                              PrimaryEmailAddress   = Convert.ToString(row["PrimaryEmailAddress"].CheckDBNull()),
                              InvoiceType           = Convert.ToString(row["InvoiceType"].CheckDBNull()),
                              ServiceCode           = Convert.ToString(row["ServiceCode"].CheckDBNull()),
                              IsCancellableFlag     = Convert.ToBoolean(row["IsCancellableFlag"].CheckDBNull()),
                              IsInsuredFlag         = Convert.ToBoolean(row["IsInsuredFlag"].CheckDBNull()),
                              IsSalesTaxPaidFlag    = Convert.ToBoolean(row["IsSalesTaxPaidFlag"].CheckDBNull()),
                              InsuredAmount         = Convert.ToDouble(row["InsuredAmount"].CheckDBNull()),
                              ReceiverSignatureUrl = Convert.ToString(row["ReceiverSignatureUrl"].CheckDBNull()),
                              SenderSignatureUrl    = Convert.ToString(row["SenderSignatureUrl"].CheckDBNull()),
                              QuoteExpedited        = Convert.ToDouble(row["QuoteExpedited"].CheckDBNull()),
                              QuoteExpress          = Convert.ToDouble(row["QuoteExpress"].CheckDBNull()),
                              QuoteEconomy          = Convert.ToDouble(row["QuoteEconomy"].CheckDBNull()),
                              Payment = new Payment
                              {
                                  Amount = Convert.ToDouble(row["Price"].CheckDBNull()),
                                  PaymentMethod = Convert.ToString(row["PaymentMode"].CheckDBNull()),
                              },
                          }).ToList();

                foreach (var order in orders) { 
                    order.LineItem = ds.Tables[1].Select("OrderHeader_FK=" + order.OrderHeaderID).DataTableToCollectionType<LogistikaOrderLineItem>();
                    order.Documents = ds.Tables[2].Select("OrderHeader_PK=" + order.OrderHeaderID).DataTableToCollectionType<Document>();
                    order.PickupAddress = BindAddress(ds, 3);
                    order.DropoffAddress = ds.Tables[4].Select("OrderHeader_PK=" + order.OrderHeaderID).DataTableToCollectionType<Address>();
                    order.OrderHeaderID = string.Empty;
                } 
            }
            return orders;
        }

        public IList<Address> BindAddress(DataSet Data, int index = 0)
        {
            if (!Data.IsDataSetNotNullAndTableHasRows(index))
            {
                return (IList<Address>)null;
            }
            return Data.Tables[index].DataTableToCollectionType<Address>();
        }
        // Order and Driver information for dashboard get_Method
        public IList<OrderDriverInfo> getOrderDriverInfo(string StartDt, string EndDt, string OrderStatusCode, string VendorOrderID, string UserName, string CompanyAddressID)
        {
            IList<OrderDriverInfo> OrderDriverInfoList = null;

            var ds = GetDataSetResult("[dbo].[proc_get_OrderDriverInfo]",
                   new SqlParameter("StartDt",StartDt),
                   new SqlParameter("EndDt",EndDt),
                   new SqlParameter("OrderStatusCode", OrderStatusCode),
                   new SqlParameter("CompanyAddressID", CompanyAddressID.IsEmptyNullOrDefault() ? null : CompanyAddressID),
                   new SqlParameter("VendorOrderID", VendorOrderID.IsEmptyNullOrDefault() ? null : VendorOrderID),
                   new SqlParameter("UserName", UserName.IsEmptyNullOrDefault() ? null : UserName),
                 //new SqlParameter("LastModifiedBy", ClaimHelper.UserName == "" ? DBNull.Value : (object)ClaimHelper.UserName),
                   new SqlParameter("CompanyID", ClaimHelper.CompanyId)
                );

            OrderDriverInfoList = (from row in ds.Tables[0].AsEnumerable()
                                   select new Entities.OrderDriverInfo
                                   {
                                       VendorOrderID = Convert.ToString(row["VendorOrderID"].CheckDBNull()),
                                       WaveID = Convert.ToString(row["WaveID"].CheckDBNull()),
                                       OrderDate = Convert.ToDateTime(row["OrderDate"].CheckDBNull()),
                                       CompanyID = Convert.ToInt32(row["CompanyID"].CheckDBNull()),
                                       LogistikaOrderID = Convert.ToString(row["LogistikaOrderID"].CheckDBNull()),
                                       TrackOrder = Encryption.EncryptionManager.Encrypt(Convert.ToString(row["LogistikaOrderID"].CheckDBNull()), "$Talluri$"),
                                       FrameworkApplicationUserID = Convert.ToInt32(row["FrameworkApplicationUserID"].CheckDBNull()),
                                       ParentLogistikaOrderID = Convert.ToString(row["ParentLogistikaOrderID"].CheckDBNull()),
                                       PickupAddress = Convert.ToString(row["PickupAddress"].CheckDBNull()),
                                       DropOffAddress = Convert.ToString(row["DropOffAddress"].CheckDBNull()),
                                       PickUpSignature = Convert.ToString(row["PickUpSignature"].CheckDBNull()),
                                       DropOffSignature = Convert.ToString(row["DropOffSignature"].CheckDBNull()),
                                       PickUpDate = Convert.ToString(row["PickUpDate"].CheckDBNull()),
                                       DropOffDate = Convert.ToString(row["DropOffDate"].CheckDBNull()),
                                       ItemType = Convert.ToString(row["ItemType"].CheckDBNull()),
                                       AssignedDriver = Convert.ToString(row["AssignedDriver"].CheckDBNull()),
                                       AssignedVehicle = Convert.ToString(row["AssignedVehicle"].CheckDBNull()),
                                       OrderStatusCode = Convert.ToString(row["OrderStatusCode"].CheckDBNull()),
                                       OrderStatus = Convert.ToString(row["OrderStatus"].CheckDBNull()),
                                       DriverStatus = Convert.ToString(row["DriverStatus"].CheckDBNull()),
                                       BatteryStatus = Convert.ToDecimal(row["BatteryStatus"].CheckDBNull()),
                                   }).ToList();
            return OrderDriverInfoList;
        }

        // Delivery Reps Location get_Method
        public IList<DeliveryRep> getDeliveryRepknownLoc(int CompanyID)
        {
            IList<DeliveryRep> DeliveryRepList = null;
            var ds = GetDataSetResult("[dbo].[proc_get_AllDeliveryRepsLoc]",
                   new SqlParameter("CompanyID", ClaimHelper.CompanyId)
                );

            DeliveryRepList = (from row in ds.Tables[0].AsEnumerable()
                               select new Entities.DeliveryRep
                               {
                                   FirstName = Convert.ToString(row["FirstName"].CheckDBNull()),
                                   LastName = Convert.ToString(row["LastName"].CheckDBNull()),
                                   ProfilePic = SiteConfigurationManager.GetAppSettingKey("WebsiteProfilePicSite") + Convert.ToString(row["ProfilePic"].CheckDBNull()),
                                   Longitutue = Convert.ToDouble(row["Longitutue"].CheckDBNull()),
                                   Latitude = Convert.ToDouble(row["Latitude"].CheckDBNull()),
                                   Company_PK = Convert.ToInt32(row["Company_PK"].CheckDBNull()),
                                   LastknownDt = Convert.ToDateTime(row["LastknownDt"].CheckDBNull()),
                                   FrameApplicationUserID = Convert.ToInt32(row["FrameApplicationUserID"].CheckDBNull()),
                                   Speed = Convert.ToString(row["Speed"].CheckDBNull()),
                               }).ToList();
            return DeliveryRepList;
        }
    }
}


using Logistika.Service.Common.Entities;
using Logistika.Service.Common.Entities.Order;
using System;
using System.Collections.Generic;

namespace Logistika.Service.Common.BusinessComponentInterface.User
{
    public interface IOrderBusinessComponent
    {
        IList<FileImport> GetFileImportList();

        string UpdateOrderStatus(OrderUpdate Request);
        DeliveryRep OrderTrackingInfo(string orderID);

        IList<OrderStats> getOrderStats(int CompanyID, string StartDt, string EndDt);
        IList<OrderStatsArea> getOrderStatsArea(int CompanyID, string StartDt, string EndDt);
        IList<OrderDriverInfo> getOrderDriverInfo(DateTime? StartDt = null, DateTime? EndDt = null, string OrderStatusCode = null, string VendorOrderID = null, string UserName = null, string CompanyAddress = null);

        IList<DeliveryRep> getDeliveryRepknownLoc(int CompanyID);

        string createOrder(LogistikaOrderHeader orderHeader);
        string SubmitOrderQuote(OrderQuote orderQuote);

        string OrderFileImport(string FileName, string CallType);

        IList<LogistikaOrderHeader> getOrderDetail(string LogistikaOrderID);

        WaveModal GetWavePlanner(int? WaveID = 0, int? CompanyAddressID = 0);
        bool UpdateInsertWavePlanner(IList<WavedOrders> WaveOrder);
    }
}

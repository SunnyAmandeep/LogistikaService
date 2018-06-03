
using Logistika.Service.Common.Entities.Order;
using Logistika.Service.Common.Entities.Lookup;
using System;
using System.Collections.Generic;
using Logistika.Service.Common.Entities;

namespace Logistika.Service.Common.DataAccessInterface.Config
{
    public interface IOrderDataAccess
    {
        IList<FileImport> GetFileImportList();
        string UpdateOrderStatus(OrderUpdate Request);
        DeliveryRep OrderTrackingInfo(string orderID);

        IList<OrderStats> getOrderStats(int CompanyID, string StartDt, string EndDt);
        IList<OrderStatsArea> getOrderStatsArea(int CompanyID, string StartDt, string EndDt);
        IList<OrderDriverInfo> getOrderDriverInfo(string StartDt, string EndDt, string OrderStatusCode, string VendorOrderID, string UserName, string CompanyAddressID);

        IList<DeliveryRep> getDeliveryRepknownLoc(int CompanyID);

        string createOrder(LogistikaOrderHeader orderHeader);
        string SubmitOrderQuote(OrderQuote orderQoute);

        IList<LogistikaOrderHeader> getOrderDetail(string LogistikaOrderID);

        WaveModal GetWavePlanner(int? WaveID = 0, int? CompanyAddressiD = 0);
        bool UpdateInsertWavePlanner(IList<WavedOrders> WaveOrder);

    }
}

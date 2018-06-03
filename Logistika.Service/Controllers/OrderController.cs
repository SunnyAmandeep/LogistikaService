using Logistika.Service.Common.BusinessComponentInterface.User;
using Logistika.Service.Common.Entities;
using Logistika.Service.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Http;
using System.Web.Http.Results;

namespace Logistika.Service.Controllers
{
    //[Authorize]

    public class OrderController : BaseController
    {
        IOrderBusinessComponent _businessInstance = null;
        public OrderController(IOrderBusinessComponent Instance)
        {
            _businessInstance = Instance;
        }

        [HttpPost]
        [Route("api/Order/OrderStatus")]
        public string UpdateOrderStatus(OrderUpdate Request)
        {
            return _businessInstance.UpdateOrderStatus(Request);
        }

        [HttpGet]
        [Route("api/Order/OrderStats")]
        public IList<OrderStats> getOrderStats(string StartDt, string EndDt)
        {
            return _businessInstance.getOrderStats(Convert.ToInt32(ClaimHelper.CompanyId), StartDt, EndDt);
        }

        [HttpGet]
        [Route("api/Order/OrderStatsArea")]
        public IList<OrderStatsArea> getOrderStatsArea(string StartDt, string EndDt)
        {
            return _businessInstance.getOrderStatsArea(Convert.ToInt32(ClaimHelper.CompanyId), StartDt, EndDt);
        }


        [HttpGet]
        [Route("api/Order/DeliveryRepLoc")]
        public IList<DeliveryRep> getDeliveryRepknownLoc()
        {
            return _businessInstance.getDeliveryRepknownLoc(Convert.ToInt32(ClaimHelper.CompanyId));
        }

        [HttpPost]
        [Route("api/Order/LogistikaOrder")]
        public string createOrder(LogistikaOrderHeader orderHeader)
        {
            
            if (orderHeader != null)
            {
                orderHeader.LastModifiedBy = this.GetRefUser();
                if (orderHeader.CallType == null)
                {
                    orderHeader.CallType = "Portal";
                }
                return _businessInstance.createOrder(orderHeader);
            }
            return "Invalid Object";
        }

        [HttpPost]
        [Route("api/Order/OrderQuote")]
        public string OrderQuote(OrderQuote orderQuote)
        {
            return _businessInstance.SubmitOrderQuote(orderQuote);

        }

        [HttpGet]
        [Route("api/Order/OrderDriver")]
        //IList<OrderDriverInfo>
        public IHttpActionResult getOrderDriverInfo(DateTime? StartDt = null, DateTime? EndDt = null, string OrderStatusCode = null, string OrderID = null, string UserName = null, string CompanyAddressID = null)
        {
            //NotFound()
            if (string.IsNullOrEmpty(OrderID) && string.IsNullOrEmpty(OrderStatusCode) && string.IsNullOrEmpty(UserName) && string.IsNullOrEmpty(CompanyAddressID))
            {
                return NotFound();
            }
            return Ok(_businessInstance.getOrderDriverInfo(StartDt, EndDt, OrderStatusCode, OrderID,UserName,CompanyAddressID));
        }

        [HttpGet]
        [Route("api/Order/Detail")]
        public IList<LogistikaOrderHeader> OrderDetail(string OrderId)
        {
            return _businessInstance.getOrderDetail(OrderId);

        }

        [HttpGet]
        [Route("api/Order/Wave")]
        public WaveModal GetWavePlanner(int? WaveID = 0, int? CompanyAddressID = 0)
        {
            return _businessInstance.GetWavePlanner(WaveID, CompanyAddressID);
        }

        [HttpPost]
        [Route("api/Order/Wave")]
        public bool UpdateInsertWavePlanner(IList<WavedOrders> Orders)
        {
            return _businessInstance.UpdateInsertWavePlanner(Orders);
        }

        [HttpGet]
        [Route("api/Order/TrackOrder")]
        public DeliveryRep OrderTrackingInfo(string orderID)
        {
            return _businessInstance.OrderTrackingInfo(orderID);
        }
      
        //[HttpGet]
        //public String TestWebService()
        //{
        //    return "testWebService";
        //}

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistika.Service.Common.Entities
{
    public class Payment
    {
        public int       OrderHeader_FK {get;set;}
        public string    PaymentMethod {get;set;}
        public double?   Amount {get;set;}
        public double?   TaxAmount {get;set;}
        public double?   DiscountAmount {get;set;}
        public string    CardHolderName {get;set;}
        public string    CardNumber {get;set;}
        public DateTime? ExpirationDate {get;set;}
        public string    AuthorizationCode {get;set;}
        public int?      SecurityCode {get;set;}
        public string    LastModifiedBy {get;set;}
        public DateTime? LastModifiedDt {get;set;}
    }
}

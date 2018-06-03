
namespace Logistika.Service.Common.Entities
{
    
    public abstract class BaseObject
    {
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDt { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDt { get; set; }
    }
}

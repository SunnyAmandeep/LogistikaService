
namespace Logistika.Service.Common.Entities.CorsConfig
{
    public class CorsConfig
    {
        public long Id { get; set; }
        public string Resources { get; set; }
        public string Origins { get; set; }
        public string Methods { get; set; }
        public bool   AllowCookies { get; set; }
        public string RequestHeaders { get; set; }
        public string ResponseHeaders { get; set; }
    }
}

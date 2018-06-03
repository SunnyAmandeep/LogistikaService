using System.Collections.Generic;

namespace Logistika.Service.Common.Entities.Menu
{
    //public enum Operation
    //{
    //    RUN_REPORT  =	1,
    //    RUN_INIT    =	2,
    //    CREATE	    =	3,
    //    UPDATE	    =	4,
    //    DELETE	    =	5,
    //    READ	    =	6,
    //    EXECUTE	    =	8	
    //}
    public class Operation
    {
        public string  Code { get; set; }
        public int Id { get; set; }
    }
    public class OperationUrl
    {
        public string Code { get; set; }
        public int OperationUrlId { get; set; }
    }
    public class MenuItem
    {

        public string MenuText { get; set; }
        public int MenuId { get; set; }
        public string ToolTip { get; set; }
        public string MenuURL { get; set; }
        public int Sequence { get; set; } 
        public string ScreenName { get; set; }
        public string MenuFullURL { get; set; }
        public int? ParentId { get; set; }
        public IList<OperationUrl> OperationsUrl { get; set; }
       // public IList<Operation> Operations { get; set; }
        public IList<MenuItem> SubMenu { get; set; }
        //{
        //    get
        //    {
        //        return
        //            string.Format(@"{0}://{1}{2}{3}{4}",
        //            HttpContext.Current.Request.Url.Scheme,
        //            HttpContext.Current.Request.Url.Host,
        //            ((String.IsNullOrEmpty(HttpContext.Current.Request.Url.Port.ToString()) || string.Equals(HttpContext.Current.Request.Url.Port.ToString(), "80"))
        //            ? string.Empty :
        //            ":" + HttpContext.Current.Request.Url.Port.ToString()),
        //            (HttpContext.Current.Request.ApplicationPath.Equals("/") ? string.Empty : HttpContext.Current.Request.ApplicationPath.ToString()) + "/",
        //            MenuURL);
        //    }
        //}
       // public string AngluarMenuURL { get; set; }
        //{
        //    get
        //    {
        //        return
        //            string.Format(@"{0}://{1}{2}{3}{4}",
        //            HttpContext.Current.Request.Url.Scheme,
        //            HttpContext.Current.Request.Url.Host,
        //            ((String.IsNullOrEmpty(HttpContext.Current.Request.Url.Port.ToString()) || string.Equals(HttpContext.Current.Request.Url.Port.ToString(), "80"))
        //            ? string.Empty :
        //            ":" + HttpContext.Current.Request.Url.Port.ToString()),
        //            (HttpContext.Current.Request.ApplicationPath.Equals("/") ? string.Empty : HttpContext.Current.Request.ApplicationPath.ToString()) + "/",
        //            MenuURL);
        //    }
        }

}

using System.Text;

namespace Logistika.Service.Common.Entities.ErrorLog
{
    public class AppError //: Exception
    {
        public string Message { get; set; }
        public string URL { get; set; }
        public string Error { get; set; }
        public string AddtionaInfo { get; set; }
        public string Source { get; set; }
        public string User { get; set; }
        public AppError()
        {
        }
        //public AppError(string message)
        //    : base(message)
        //{
        //}

        //public AppError(string message, Exception inner)
        //    : base(message, inner)
        //{
        //}
        public string FormatException()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Source:{0}", Source);
            sb.AppendLine("");
            sb.AppendFormat("User:{0}", User);
            sb.AppendLine("");
            sb.AppendFormat("URL:{0}", URL);
            sb.AppendLine("");
            sb.AppendFormat("Error:{0}", Error);
            sb.AppendLine("");
            sb.AppendFormat("Message:{0}", Message);
            sb.AppendLine("");
            sb.AppendFormat("AddtionaInfo:{0}", AddtionaInfo);
            sb.AppendLine("");
            //Exception ex = this.InnerException;
            //while (ex != null){
            //    sb.AppendFormat("Source:{0}", ex.Source);
            //    sb.AppendLine("");
            //    sb.Append( ex.Message);
            //    sb.AppendLine("");
            //    sb.Append(ex.StackTrace);
            //    sb.AppendLine("");
            //    ex = ex.InnerException;
            //}

            return sb.ToString();
        }
    }
}

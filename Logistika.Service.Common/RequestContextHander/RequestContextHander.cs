using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logistika.Service.Common.RequestContextHander
{
    public static class KnipperRequestContext
    {
        private const string _error = "ERROR";
        private const string _message = "MESSAGE";
        private const string _contextObject = "OBJECT";
       // private Dictionary<string, Dictionary<Type, Object>> _currentContext;

        public static bool IsError { get { return !(HttpContext.Current==null || HttpContext.Current.Items[_error] == null || ((IList<string>)HttpContext.Current.Items[_error]).Count == 0); } }
        public static  void AddError(string Error)
        {
            try
            {
            if (string.IsNullOrEmpty(Error))
                return;
            IList<string> errors = null;
            if (HttpContext.Current.Items[_error] == null)
                errors = new List<string>();
            else
                errors = (IList<string>)HttpContext.Current.Items[_error];
            errors.Insert(0,Error);
            
                HttpContext.Current.Items.Add(_error, errors);
            }
            catch {  }
        }

        public static  IList<string> GetError()
        {
            if (HttpContext.Current.Items[_error] == null)
                return (IList<string>)null;
            return (IList<string>)HttpContext.Current.Items[_error];
        }

        public static  void AddMessage(string Message)
        {
            if (string.IsNullOrEmpty(Message))
                return;
            IList<string> message = null;
            if (HttpContext.Current.Items[_message] == null)
                message = new List<string>();
            else
                message = (IList<string>)HttpContext.Current.Items[_message];
            message.Insert(0,Message);
            HttpContext.Current.Items.Add(_message, message);

             try
            {
                HttpContext.Current.Items.Add(_message, message);
            }
             catch {  }
        }

        public static  IList<string> GetMessage()
        {
            if (HttpContext.Current.Items[_message] == null)
                return (IList<string>)null;
            return (IList<string>)HttpContext.Current.Items[_message];
        }

        public static  void AddObject(string Key,Object Data)
        {
            if (Data == null || string.IsNullOrEmpty(Key))
                return;
             try
            {
                 HttpContext.Current.Items.Add(Key, Data);
            }
            catch { }
           // HttpContext.Current.Items.Add(Key, Data);
        }

        public static  void ClearAllError() { HttpContext.Current.Items.Remove(_error); }

        public static  void ClearAllMessage() { HttpContext.Current.Items.Remove(_message); }

        public static  void RemoveErrorAt(int Index=0) {
            if (Index < 0 || HttpContext.Current.Items[_error] == null)
                return;
          
            var    errors = (IList<string>)HttpContext.Current.Items[_error];
            if (Index < errors.Count()) 
            { 
                errors.RemoveAt(Index);
                if (errors.Count == 0)
                {
                    HttpContext.Current.Items[_error] = null;
                }
                else
                {
                    HttpContext.Current.Items.Add(_error, errors);
                }
            }
        }

        public static  void RemoveMessageAt(int Index)
        {
            if (Index < 0 || HttpContext.Current.Items[_message] == null)
                return;
             var   message = (IList<string>)HttpContext.Current.Items[_message];
             if (Index < message.Count())
             {
                 message.RemoveAt(Index);

                 if (message.Count == 0)
                 {
                     HttpContext.Current.Items[_error] = null;
                 }
                 else
                 {
                     HttpContext.Current.Items.Add(_message, message);
                 }
             }

        }
    }
}

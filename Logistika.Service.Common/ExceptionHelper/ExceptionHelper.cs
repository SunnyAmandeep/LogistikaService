using Logistika.Service.Common.EFDataContext;
using Logistika.Service.Common.RequestContextHander;
using System;
using System.Data.Entity.Validation;

namespace Logistika.Service.Common.ExceptionHelper
{
    public class ExceptionHelper
    {
        public static Action<Exception> Log { get; set; }
        public static T SafeExecute<T, K>(K param, Func<K, T> method) 
        {
           
            T t = default(T);
            try
            {
                t = method(param);
            }
            catch (DbEntityValidationException e)
            {
                var newException = new FormattedDbEntityValidationException(e);
                KnipperRequestContext.AddError(newException.Message);
            }
            catch (Exception ex)
            {
                KnipperRequestContext.AddError(ex.Message);
                Log(ex);
            }
            return t;
        }
      
        public static T SafeExecute<T>(int a, int b, Func<int, int, T> method) 
        {
           
            T t = default(T);
            try
            {
                t = method(a, b);
            }
            catch (DbEntityValidationException e)
            {
                var newException = new FormattedDbEntityValidationException(e);
                KnipperRequestContext.AddError(newException.Message);
            }
            catch (Exception ex)
            {
                KnipperRequestContext.AddError(ex.Message);
                Log(ex);
            }
            return t;
        }


        public static T SafeExecute<T>(string a, bool b, string c, Func<string, bool,string, T> method) 
        {
           
            T t = default(T);
            try
            {
                t = method(a, b,c);
            }
            catch (DbEntityValidationException e)
            {
                var newException = new FormattedDbEntityValidationException(e);
                KnipperRequestContext.AddError(newException.Message);
            }
            catch (Exception ex)
            {
                KnipperRequestContext.AddError(ex.Message); 
            }
            return t;
        }
        
        public static T SafeExecuteWeb<T>(int a, int b, Func<int, int, T> method) 
        {
           
            T t = default(T);
            try
            {
                t = method(a, b);
            }
            catch (DbEntityValidationException e)
            {
                var newException = new FormattedDbEntityValidationException(e);
                KnipperRequestContext.AddError(newException.Message);
            }
            catch (Exception ex)
            {
                KnipperRequestContext.AddError(ex.Message);
                Log(ex);
            }
            return t;
        }

        public static T SafeExecute<T>(Func<T> method) 
        {
           
            T t = default(T);
            try
            {
                t = method();
            }
            catch (DbEntityValidationException e)
            {      
                var newException = new FormattedDbEntityValidationException(e);
                KnipperRequestContext.AddError(newException.Message);
            }
            catch (Exception ex)
            {
                KnipperRequestContext.AddError(ex.Message);
                Log(ex);
            }
            return t;
        }


        public static void SafeVoidExecute(Action method )
        {
             
            try
            { 
                method();
            }
            catch (DbEntityValidationException e)
            {
                var newException = new FormattedDbEntityValidationException(e);
                KnipperRequestContext.AddError(newException.Message);
            }
            catch (Exception ex)
            {
                KnipperRequestContext.AddError(ex.Message);
                Log(ex);
            } 
        }
         
        
    }
}

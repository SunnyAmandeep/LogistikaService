using Logistika.Service.Common.Entities.Lookup;
using Logistika.Service.Common.Extension;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Logistika.Service.Common.EFDataContext
{
    public abstract class BaseDataAccess : IDisposable
    {

        DbContext _context { get; set; }


        public BaseDataAccess()
        {
            _context = new KFISBaseContext();
        }

        public BaseDataAccess(string ConnectionStringName)
        {
            _context = new KFISBaseContext(ConnectionStringName);
        }
        public BaseDataAccess(DbContext Context)
        {
            _context = Context;
        }

        ~BaseDataAccess()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        // NOTE: Leave out the finalizer altogether if this class doesn't 
        // own unmanaged resources itself, but leave the other methods
        // exactly as they are. 
        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }

        protected IList<T> GetList<T>(string ProcedureName, params SqlParameter[] parameter) where T : new()
        {
            IList<T> data = null;

            try
            {
                if (_context.Database.Connection.State != ConnectionState.Open) { _context.Database.Connection.Open(); }
                var command = _context.Database.Connection.CreateCommand();
                command.CommandText = ProcedureName;
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter output = null;
                string outputMessage = string.Empty;
                foreach (SqlParameter p in parameter)
                {
                    command.Parameters.Add(p);
                    if (string.Compare(p.ParameterName, "OutputMessage", true) == 0)
                    {
                        output = p;

                    }
                }

                using (var reader = command.ExecuteReader())
                {
                    if (output != null)
                    {
                        output.CheckOutputErrorMessage();
                    }
                    data = reader.MapToList<T>();
                }

            }
            finally { if (_context != null && _context.Database.Connection.State == ConnectionState.Open) { _context.Database.Connection.Close(); } }

            return data;
        }



        public SqlParameter OutputParameter
        {
            get
            {
                SqlParameter outputmessageParameter = new SqlParameter("outputMessage", SqlDbType.VarChar, 8000);
                outputmessageParameter.Direction = ParameterDirection.Output;
                return outputmessageParameter;
            }
        }
        public SqlParameter GetOutputParameter(string Name, ParameterDirection Direction = ParameterDirection.Output)
        {
            SqlParameter outputmessageParameter = new SqlParameter(Name, SqlDbType.VarChar, 8000);
            outputmessageParameter.Direction = Direction;
            return outputmessageParameter;
        }

        protected IList<T> GetList<T>(Dictionary<string, string> Mapping, string ProcedureName, params SqlParameter[] parameter) where T : new()
        {
            IList<T> data = null;

            try
            {
                if (_context.Database.Connection.State != ConnectionState.Open) { _context.Database.Connection.Open(); }
                var command = _context.Database.Connection.CreateCommand();
                command.CommandText = ProcedureName;
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter output = null;
                string outputMessage = string.Empty;
                foreach (SqlParameter p in parameter)
                {
                    command.Parameters.Add(p);
                    if (string.Compare(p.ParameterName, "OutputMessage", true) == 0)
                    {
                        output = p;

                    }
                }

                using (var reader = command.ExecuteReader())
                {
                    if (output != null)
                    {
                        output.CheckOutputErrorMessage();
                    }

                    data = reader.MapToList<T>(mapping: Mapping);
                }

            }
            finally { if (_context != null && _context.Database.Connection.State == ConnectionState.Open) { _context.Database.Connection.Close(); } }

            return data;
        }

        protected IList<T> GetList<T>(IList<DropdownData> Mapping, string ProcedureName, params SqlParameter[] parameter) where T : new()
        {
            IList<T> data = null;

            try
            {
                if (_context.Database.Connection.State != ConnectionState.Open) { _context.Database.Connection.Open(); }
                var command = _context.Database.Connection.CreateCommand();
                command.CommandText = ProcedureName;
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter output = null;
                string outputMessage = string.Empty;
                foreach (SqlParameter p in parameter)
                {
                    command.Parameters.Add(p);
                    if (string.Compare(p.ParameterName, "OutputMessage", true) == 0)
                    {
                        output = p;

                    }
                }

                using (var reader = command.ExecuteReader())
                {
                    if (output != null)
                    {
                        output.CheckOutputErrorMessage();
                    }

                    data = reader.MapToList<T>(mapping: Mapping);
                }

            }
            finally { if (_context != null && _context.Database.Connection.State == ConnectionState.Open) { _context.Database.Connection.Close(); } }

            return data;
        }

        protected IList<T> GetList1<T>(Func<DbDataReader, IList<T>> Method
            , Dictionary<string, string> Mapping, string ProcedureName, params SqlParameter[] parameter) //where T : new()
        {
            IList<T> data = null;

            try
            {
                if (_context.Database.Connection.State != ConnectionState.Open) { _context.Database.Connection.Open(); }
                var command = _context.Database.Connection.CreateCommand();
                command.CommandText = ProcedureName;
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter output = null;
                string outputMessage = string.Empty;
                foreach (SqlParameter p in parameter)
                {
                    command.Parameters.Add(p);
                    if (string.Compare(p.ParameterName, "OutputMessage", true) == 0)
                    {
                        output = p;

                    }
                }

                using (var reader = command.ExecuteReader())
                {
                    if (output != null)
                    {
                        output.CheckOutputErrorMessage();
                    }

                    data = Method(reader);
                }

            }
            finally { if (_context != null && _context.Database.Connection.State == ConnectionState.Open) { _context.Database.Connection.Close(); } }

            return data;
        }


        protected DbDataReader GetDbDataReader(string ProcedureName, params SqlParameter[] parameter)
        {
            DbDataReader data = null;

            try
            {
                if (_context.Database.Connection.State != ConnectionState.Open) { _context.Database.Connection.Open(); }
                var command = _context.Database.Connection.CreateCommand();
                command.CommandText = ProcedureName;
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter output = null;
                string outputMessage = string.Empty;
                foreach (SqlParameter p in parameter)
                {
                    command.Parameters.Add(p);
                    if (string.Compare(p.ParameterName, "OutputMessage", true) == 0)
                    {
                        output = p;

                    }
                }

                using (var reader = command.ExecuteReader())
                {
                    if (output != null)
                    {
                        output.CheckOutputErrorMessage();
                    }
                    data = reader;
                }

            }
            finally { if (_context != null && _context.Database.Connection.State == ConnectionState.Open) { _context.Database.Connection.Close(); } }

            return data;
        }

        protected void BindParameter(SqlCommand Command, ref SqlParameter Output, params SqlParameter[] Parameters)
        {

            foreach (SqlParameter p in Parameters)
            {
                Command.Parameters.Add(p);
                if (string.Compare(p.ParameterName, "OutputMessage", true) == 0)
                {
                    Output = p;
                }
            }
        }

        protected bool Exec(string ProcedureName, params SqlParameter[] parameter)
        {
            //IList<SqlParameter> listOutput = null;
            try
            {

                if (_context.Database.Connection.State != ConnectionState.Open) { _context.Database.Connection.Open(); }
                var command = _context.Database.Connection.CreateCommand();
                command.CommandText = ProcedureName;
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter output = null;
                string outputMessage = string.Empty;
                foreach (SqlParameter p in parameter)
                {
                    command.Parameters.Add(p);
                    if (string.Compare(p.ParameterName, "OutputMessage", true) == 0)
                    {
                        output = p;

                    }
                    //else if(p.Direction == ParameterDirection.Output) {
                    //    if (listOutput == null) {
                    //        listOutput = new List<SqlParameter>();
                    //    }
                    //    listOutput.Add(p);
                    //}
                }

                var count = command.ExecuteNonQuery();
                if (output != null)
                {
                    output.CheckOutputErrorMessage();
                }

                //if (listOutput != null && listOutput.Count() > 0) { 

                //}
                return true;

            }
            catch (Exception ex) { throw ex; }
            finally { if (_context != null && _context.Database.Connection.State == ConnectionState.Open) { _context.Database.Connection.Close(); } }

            return false;

        }

        protected DataSet GetDataSetResult(string ProcedureName, params SqlParameter[] Parameter)
        {

            DataSet retVal = new DataSet();
            SqlConnection sqlConn = (SqlConnection)_context.Database.Connection;
            SqlCommand command = new SqlCommand(ProcedureName, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter(command);

            SqlParameter output = null;
            using (command)
            {
                BindParameter(command, ref output, Parameter);
                command.CommandType = CommandType.StoredProcedure;
                da.Fill(retVal);
                if (output != null)
                {
                    output.CheckOutputErrorMessage();
                }

            }
            return retVal;
        }
    }
}

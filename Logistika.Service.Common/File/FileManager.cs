using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.OleDb;
using Logistika.Service.Logger.DataAccessInterface;
using Logistika.Service.Common.Entities;

namespace Logistika.Service.Common.File
{
    public static class FileManager
    {
        public static DataTable FileToDataTable(string Path)
        {

            if (string.IsNullOrEmpty(Path))
            { throw new Exception("Path cannot be null or Empty."); }
            if (!System.IO.File.Exists(Path))
            {
                throw new Exception("Invalid File Path.");
            }

            DataTable dt = new DataTable();
            int row = 0;
            using (StreamReader sr = new StreamReader(Path))
            {

                while (sr.Peek() > -1)
                {
                    string[] rowData = sr.ReadLine().Split(new char[] { '|' });
                    if (row == 0)
                    {
                        row++;
                        foreach (var columnName in rowData)
                        {
                            dt.Columns.Add(new DataColumn(columnName, typeof(string)));
                        }
                    }
                    else
                    {
                        dt.Rows.Add(rowData);
                    }

                }

            }
            return dt;
        }

        public static FileExDetails FileErrorLog(string fileName, string Message, string id)
        {

            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                writer.WriteLine("Error for record: " + id + ". " + "Message :" + Message + "." + Environment.NewLine +
                                    "Date :" + DateTime.Now.ToString());
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }

            return new FileExDetails {
                        ErrorFileName = fileName,
                        FileName = fileName,
                        Status = ""};


        }

        public static DataTable GetDataTable(string sql, string connectionString)
        {
            DataTable dt = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    using (OleDbDataReader rdr = cmd.ExecuteReader())
                    {
                        dt.Load(rdr);
                        return dt;
                    }
                }
            }
        }
    }
}


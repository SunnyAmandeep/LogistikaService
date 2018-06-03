using Logistika.Service.Common.Entities.Lookup;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Logistika.Service.Common.Extension
{
    public static class DataExtension
    {
        public static string NotNullThenAppend(this string data, string Separator, params string[] Parameters)
        {
            data = !string.IsNullOrEmpty(data) ? data : "";
            var v = "";
            if (Parameters != null && Parameters.Count() > 0)
            {
                var objs = Parameters.Where(x => !string.IsNullOrEmpty(x));
                if (objs != null && objs.Count() > 0)
                {
                    v = string.Join(Separator, objs);
                }
            }
            return data + v;

        }
        public static string SplitAndTakeIndex(this string data, string Separator = ",", int Index = 0)
        {
            if (!string.IsNullOrEmpty(data) && data.Length > 0 && !string.IsNullOrEmpty(Separator) && Separator.Length > 0 && Index >= 0)
            {
                var v = data.Split(Separator.ToCharArray());
                if (v != null && v.Length > 0 && v.Length > Index)
                {
                    try { return v[Index].Trim(); }
                    catch (Exception ex) { }
                }
            }
            return string.Empty;
        }

        public static string ConvertToXmlWithoutNamespaces<T>(this IList<T> Lstdto) where T : class
        {
            string result = string.Empty;
            var emptyNamepsaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(Lstdto.GetType());
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, Lstdto, emptyNamepsaces);
                return stream.ToString();
            }
            return result;
        }
        public static string TryGet(this IList<DropdownData> Dic, string Key)
        {
            try
            {
                if (Dic != null && Dic.Count() > 0)
                {
                    var v = Dic.Where(x => x.Text == Key).FirstOrDefault();
                    if (v != null)
                    {
                        return v.Value;
                    }
                }
            }
            catch { }
            return string.Empty;

        }
        public static K TryGet<T, K>(this Dictionary<T, K> Dic, T Key)
        {
            K k = default(K);
            try
            {
                Dic.TryGetValue(Key, out k);
            }
            catch { }
            return k;

        }
        public static List<dynamic> ToDynamic(this DataTable dt)
        {
            var dynamicDt = new List<dynamic>();
            foreach (DataRow row in dt.Rows)
            {
                dynamic dyn = new ExpandoObject();
                dynamicDt.Add(dyn);
                foreach (DataColumn column in dt.Columns)
                {
                    var dic = (IDictionary<string, object>)dyn;
                    dic[column.ColumnName] = row[column];
                }
            }
            return dynamicDt;
        }
        public static IList<T> DataTableToCollectionType<T>(this DataTable dt) where T : class
        {
            IList<T> t = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                var properties = typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
                t = new List<T>();
                foreach (DataRow dr in dt.Rows)
                {
                    var temp = (T)Activator.CreateInstance(typeof(T), null);
                    foreach (var p in properties)
                    {
                        try
                        {
                            temp.GetType().GetProperty(p.Name).SetValue(temp, dr[p.Name], null);
                        }
                        catch { }
                    }
                    t.Add(temp);
                }
            }

            return t;
        }

        public static IList<T> DataTableToCollectionType<T>(this DataRow[] dt) where T : class
        {
            IList<T> t = null;
            if (dt != null && dt.Count() > 0)
            {
                var properties = typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
                t = new List<T>();
                foreach (DataRow dr in dt)
                {
                    var temp = (T)Activator.CreateInstance(typeof(T), null);
                    foreach (var p in properties)
                    {
                        try
                        {
                            temp.GetType().GetProperty(p.Name).SetValue(temp, dr[p.Name], null);
                        }
                        catch { }
                    }
                    t.Add(temp);
                }
            }

            return t;
        }
        public static DataTable ListToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                if (prop.Name != "CreatedBy")
                {
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    if (prop.Name != "CreatedBy")
                    {
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    }
                table.Rows.Add(row);
            }
            return table;
        }

        public static T CheckDBNull<T>(this T dr)
        {
            if (dr != null && dr.GetType() == typeof(DBNull))
            {
                return default(T);
            }
            return dr;
        }

        public static string GetDateShortStringFromDr<T>(this T dr)
        {
            if (dr != null && dr.GetType() == typeof(DBNull))
            {
                var dts = Convert.ToString(dr);
                if (!string.IsNullOrEmpty(dts) && dts.Length > 0)
                {
                    var dt = Convert.ToDateTime(dr);
                    if (dt != null && dt != DateTime.MinValue)
                    {
                        return dt.ToShortDateString();
                    }
                }

            }
            return string.Empty;
        }

        public static object SetDbDefaultWhenNull(this string Value, params string[] DefaultValues)
        {

            if (!string.IsNullOrEmpty(Value) && Value.Trim().Length > 0)
            {
                if (DefaultValues != null && DefaultValues.Length > 0 && !DefaultValues.Contains(Value))
                    return Value.Trim();
            }
            return DBNull.Value;
        }


        public static bool IsEmptyNullOrDefault(this string Value, params string[] DefaultValues)
        {

            if (!string.IsNullOrEmpty(Value) && Value.Trim().Length > 0)
            {
                if (DefaultValues != null && DefaultValues.Length > 0 && DefaultValues.Contains(Value))
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        public static string CleanInput(this string strIn)
        {
            // Replace invalid characters with empty strings. 
            try
            {
                return Regex.Replace(strIn, @"[^0-9a-zA-Z]+", "", RegexOptions.None);
            }
            // If we timeout when replacing invalid characters,  
            // we should return Empty. 
            catch (Exception ex)
            {
                return String.Empty;
            }
        }

        public static string RemoveAnyLeadingZeros(this string strIn)
        {
            // Replace invalid characters with empty strings. 
            try
            {
                return strIn.TrimStart(new Char[] { '0' });
            }
            // If we timeout when replacing invalid characters,  
            // we should return Empty. 
            catch (Exception ex)
            {
                return strIn;
            }
        }

        public static string RemoveAandB(this string strIn)
        {
            // Replace invalid characters with empty strings. 
            try
            {
                var str = strIn.ToLower();
                if (str.Contains("a") && str.Contains("b"))
                {
                    return str.Replace("a", "").Replace("b", "");
                }
            }
            // If we timeout when replacing invalid characters,  
            // we should return Empty. 
            catch (Exception ex)
            {

            }
            return strIn;
        }

        public static bool IsTrueOrOne(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return str.ToLower() == "true" || str == "1";
            }
            return false;
        }
        public static string RemoveExtendedCharacter(this string strIn)
        {
            return Encoding.ASCII.GetString(
                 Encoding.Convert(
                     Encoding.UTF8,
                     Encoding.GetEncoding(
                         Encoding.ASCII.EncodingName,
                         new EncoderReplacementFallback(string.Empty),
                         new DecoderExceptionFallback()
                         ),
                     Encoding.UTF8.GetBytes(strIn)
                 )
             );
        }

        public static void CheckOutputErrorMessage(this SqlParameter outputmessageParameter)
        {

            if (outputmessageParameter.Direction == ParameterDirection.Output && outputmessageParameter.Value != null)
            {
                var str = Convert.ToString(outputmessageParameter.Value);
                if (!string.IsNullOrEmpty(str))
                {
                    throw new Exception(str);
                }
            }
        }
        public static string GetOutputValue(this SqlParameter outputmessageParameter)
        {

            if (outputmessageParameter.Direction == ParameterDirection.Output && outputmessageParameter.Value != null)
            {
                return Convert.ToString(outputmessageParameter.Value);

            }
            return string.Empty;
        }

        public static bool IsDataSetNotNullAndTableHasRows(this DataSet dataset, int TableIndex = 0)
        {
            return dataset != null && dataset.Tables.Count > TableIndex && dataset.Tables[TableIndex].Rows.Count > 0;
        }
        public static IList<DropdownData> FromRowToListObject(this DataRow row, params string[] Columns)
        {
            var data = new List<DropdownData>();
            try
            {
                if (Columns != null && Columns.Count() > 0 && row != null)
                {
                    foreach (var c in Columns)
                    {
                        data.Add(new DropdownData { Text = c, Value = Convert.ToString(row[c].CheckDBNull()) });
                    }

                }
            }
            catch { }
            return data;
        }

        public static K TransformData<T, K>(this T Data, Func<T, K> method)
        {
            K data = default(K);
            try { data = method(Data); }
            catch { }
            return data;
        }



        public static string ListToString(this IList<string> Src, string Separator = "\n")
        {

            if (Src != null && Src.Count > 0)
            {
                return string.Join(Separator, Src.Select(x => Regex.Replace(x, @"\t|\n|\r", "")));
            }
            return string.Empty;
        }



        public static IList<T> StringToList<T>(this string Src, string Separator = ",")
        {
            IList<T> dst = default(IList<T>);
            try
            {
                if (!string.IsNullOrEmpty(Src) && Src.Trim().Length > 0)
                {

                    var arrayOfString = Src.Split(Separator[0]);
                    T t = default(T);
                    var typeOfT = typeof(T);
                    dst = new List<T>();
                    foreach (var str in arrayOfString)
                    {
                        if (typeOfT.IsEnum)
                        {
                            t = (T)Enum.Parse(typeOfT, str);
                        }
                        else if (typeOfT == typeof(string)) { t = (T)((Object)str); }
                        else { t = (T)ChangeType(str, t.GetType()); }

                        dst.Add(t);
                    }
                }
            }
            catch { }
            return dst;
        }

        //public static IEnumerable<T> MapToList<T>(this IDataReader reader) where T : new()
        //{
        //    var properties = typeof(T).GetProperties();

        //    var modelProperties = new List<string>();
        //    var columnList = (reader.GetSchemaTable().Select()).Select(r => r.ItemArray[0].ToString());
        //    while (reader.Read())
        //    {
        //        var element = Activator.CreateInstance<T>();
        //        Dictionary<string, string> dbMappings = DBColumn(element);
        //        string columnName;
        //        foreach (var f in properties)
        //        {

        //            if (!columnList.Contains(f.Name) && !dbMappings.ContainsKey(f.Name))
        //                continue;
        //            columnName = dbMappings.ContainsKey(f.Name) ? dbMappings[f.Name] : f.Name;
        //            var o = (object)reader[columnName];

        //            if (o.GetType() != typeof(DBNull)) f.SetValue(element, ChangeType(o, f.PropertyType), null);
        //        }
        //        yield return element;
        //    }

        //}

        public static object ChangeType(object value, Type conversion)
        {
            var t = conversion;

            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }

                t = Nullable.GetUnderlyingType(t); ;
            }

            return Convert.ChangeType(value, t);
        }

        //public static Dictionary<string, string> DBColumn<T>(T item) where T : new()
        //{

        //    Dictionary<string, string> dbMappings = new Dictionary<string, string>();
        //    var type = item.GetType();
        //    var properties = type.GetProperties();
        //    foreach (var property in properties)
        //    {
        //        var attributes = property.GetCustomAttributes(false);
        //        var columnMapping = attributes
        //    .FirstOrDefault(a => a.GetType() == typeof(DbColumnAttribute));
        //        if (columnMapping != null)
        //        {
        //            dbMappings.Add(property.Name, ((DbColumnAttribute)columnMapping).Name);
        //        }
        //    }
        //    return dbMappings;
        //}

        public static IList<T> MapToList<T>(this DbDataReader dr) where T : new()
        {
            if (dr != null && dr.HasRows)
            {
                var entity = typeof(T);
                var entities = new List<T>();
                var propDict = new Dictionary<string, PropertyInfo>();
                var props = entity.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                propDict = props.ToDictionary(p => p.Name.ToUpper(), p => p);

                while (dr.Read())
                {
                    T newObject = new T();
                    for (int index = 0; index < dr.FieldCount; index++)
                    {
                        if (propDict.ContainsKey(dr.GetName(index).ToUpper()))
                        {
                            var info = propDict[dr.GetName(index).ToUpper()];
                            if ((info != null) && info.CanWrite)
                            {
                                var val = dr[index];
                                try
                                {
                                    info.SetValue(newObject, (val == DBNull.Value) ? null : ChangeType(val, info.PropertyType), null);
                                }
                                catch { }
                            }
                        }
                    }
                    entities.Add(newObject);
                }
                return entities;
            }
            return null;
        }

        public static IList<T> MapToList<T>(this DbDataReader dr, Dictionary<string, string> mapping) where T : new()
        {
            if (dr != null && dr.HasRows)
            {
                var entity = typeof(T);
                var entities = new List<T>();
                var propDict = new Dictionary<string, PropertyInfo>();
                var props = entity.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                propDict = props.ToDictionary(p => p.Name.ToUpper(), p => p);

                while (dr.Read())
                {
                    T newObject = new T();
                    foreach (var col in mapping)
                    {
                        if (propDict.ContainsKey(col.Value.ToUpper()))
                        {
                            var info = propDict[col.Value.ToUpper()];
                            if ((info != null) && info.CanWrite)
                            {
                                var val = dr[col.Key];
                                try
                                {
                                    info.SetValue(newObject, (val == DBNull.Value) ? null : ChangeType(val, info.PropertyType), null);
                                }
                                catch { }
                            }
                        }
                    }
                    entities.Add(newObject);
                }
                return entities;
            }
            return null;
        }

        public static IList<T> MapToList<T>(this DbDataReader dr, IList<DropdownData> mapping) where T : new()
        {
            if (dr != null && dr.HasRows)
            {
                var entity = typeof(T);
                var entities = new List<T>();
                var propDict = new Dictionary<string, PropertyInfo>();
                var props = entity.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                propDict = props.ToDictionary(p => p.Name.ToUpper(), p => p);

                while (dr.Read())
                {
                    T newObject = new T();
                    foreach (var col in mapping)
                    {
                        if (propDict.ContainsKey(col.Text.ToUpper()))
                        {
                            var info = propDict[col.Text.ToUpper()];
                            if ((info != null) && info.CanWrite)
                            {
                                var val = dr[col.Value];
                                try
                                {
                                    info.SetValue(newObject, (val == DBNull.Value) ? null : ChangeType(val, info.PropertyType), null);
                                }
                                catch { }
                            }
                        }
                    }
                    entities.Add(newObject);
                }
                return entities;
            }
            return null;
        }

        public static string ListToXML<T>(this IEnumerable<T> Src, string Root, string Node)
        {
            var sb = new StringBuilder();
            if (Src != null)
            {
                var nodeStart = "<" + Node + ">";
                var nodeEnd = "</" + Node + ">";
                sb.Append("<" + Root + ">");
                sb.Append(nodeStart + string.Join(nodeEnd + nodeStart, Src) + nodeEnd);
                sb.Append("</" + Root + ">");
            }
            return sb.ToString();
        }

        public static string AsString(this XmlDocument xmlDoc)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (XmlTextWriter tx = new XmlTextWriter(sw))
                {
                    xmlDoc.WriteTo(tx);
                    string strXmlText = sw.ToString();
                    return strXmlText;
                }
            }
        }
        public static string JObjectToXmlString(this JObject Json)
        {
            if (Json == null)
                return string.Empty;
            var json = Json.ToString(Newtonsoft.Json.Formatting.None);
            using (var stream = new MemoryStream(Encoding.ASCII.GetBytes(json)))
            {
                var quotas = new XmlDictionaryReaderQuotas();
                var x = JsonReaderWriterFactory.CreateJsonReader(stream, quotas);
                var d = XElement.Load(x);
                d.Descendants()
                 .Attributes("type")
                 .Remove();

                return d.ToString();
            }
        }

    }
}

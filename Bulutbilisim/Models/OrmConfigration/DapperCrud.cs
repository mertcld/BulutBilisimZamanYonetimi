using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Bulutbilisim.Models.Helper;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Bulutbilisim.Models.OrmConfigration
{
    public static class DapperCrud
    {
        public static T Get<T>(int? AnaSirketId, long id) where T : new()
        {
            string TableAttributeName = GetTableAttribute.TableName<T>();
            if (TableAttributeName == "User")
            {
                TableAttributeName = "[User]";
            }

            using (var connection = DapperService._dbConnection)
            {
                if (AnaSirketId == null)
                {
                    string sql = "SELECT * FROM " + TableAttributeName + " where id=@id";
                    return connection.QuerySingleOrDefault<T>(sql, new { id = id });
                }
                else
                {
                    string sql = "SELECT * FROM " + TableAttributeName + " where id=@id and AnaSirketId=@AnaSirketId";
                    return connection.QuerySingleOrDefault<T>(sql, new { id = id, AnaSirketId = AnaSirketId });
                }

            }

        }
        public static ResponseModel Crud<T>(T item, EnumCrudModel crud, bool idInsert = false, WhereCondition crudCondition = null, UpdateSetCondition updateSetCondition = null) where T : class
        {
            ResponseModel result = new ResponseModel();
            try
            {
                result = ResponseModel.Hatali();
                if (item != null)
                {
                    if (EnumCrudModel.Delete == crud)
                    {
                        DapperService._dbConnection.Delete<T>(item);
                        result = ResponseModel.Basarili();
                    }
                    if (EnumCrudModel.Insert == crud)
                    {
                        string insertQuery = Insert<T>(item, idInsert);
                        var a = DapperService._dbConnection.ExecuteScalar(insertQuery);
                        long id = a.ConvertToLong();
                        result = ResponseModel.Basarili(id);
                    }
                    if (EnumCrudModel.Update == crud)
                    {

                        string UpdateQuery = Update<T>(item, crudCondition, updateSetCondition);
                        DapperService._dbConnection.Execute(UpdateQuery);
                        result = ResponseModel.Basarili();
                    }

                }
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return result;
            }

        }
        public static ResponseModel Crudsupport<T>(T item, EnumCrudModel crud, bool idInsert = false) where T : class
        {
            ResponseModel result = new ResponseModel();
            try
            {

                result.Code = 101;
                result.Message = "İşlem Sırasında Bir Hata Oluştu!";
                if (item != null)
                {


                    if (EnumCrudModel.Delete == crud)
                    {
                        DapperService._dbremoteConnection.Delete<T>(item);
                        result.Code = 100;
                        result.Message = "İşlem Başarılı";
                    }
                    if (EnumCrudModel.Insert == crud)
                    {
                        string insertQuery = Insert<T>(item, idInsert);
                        var a = DapperService._dbremoteConnection.ExecuteScalar(insertQuery);
                        result.id = a.ConvertToLong();
                        result.Code = 100;
                        result.Message = "İşlem Başarılı";
                    }
                    if (EnumCrudModel.Update == crud)
                    {

                        string UpdateQuery = Update<T>(item);
                        DapperService._dbremoteConnection.Execute(UpdateQuery);
                        result.Code = 100;
                        result.Message = "İşlem Başarılı";
                    }

                }
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return result;
            }

        }
        public static string Insert<T>(T sourceTable, bool idInsert = false) where T : class
        {
            StringBuilder Columns = new StringBuilder();
            StringBuilder Values = new StringBuilder();
            string TableAttributeName = GetTableAttribute.TableName<T>();
            if (TableAttributeName == "User")
            {
                TableAttributeName = "[User]";
            }
            bool _firstInsert = true;
            PropertyInfo[] props = sourceTable.GetType().GetProperties().Where(x => x.GetCustomAttributes(typeof(ComputedAttribute), false).Length == 0).ToArray();

            object propVal;
            string id = string.Empty;
            foreach (PropertyInfo p in props)
            {

                propVal = p.GetValue(sourceTable, null);
                if (propVal != null)
                {
                    if (p.Name.ToLower() == "id")
                    {
                        id = propVal.ToString();
                        if (idInsert != true)
                            continue;
                    }
                    if (propVal is DateTime)
                    {

                        if (Convert.ToDateTime(propVal) == DateTime.MinValue || Convert.ToDateTime(propVal) == (DateTime)SqlDateTime.Null)
                        {
                            continue;
                        }

                    }

                    if (propVal is bool)
                    {
                        if (Convert.ToBoolean(propVal))
                        {
                            propVal = "1";
                        }
                        else
                        {
                            propVal = "0";
                        }
                    }

                    if (_firstInsert)
                    {
                        Columns.Append(p.Name);
                        Values.Append("'" + propVal.ToString() + "' ");
                        _firstInsert = false;
                    }
                    else
                    {

                        if (propVal is DateTime)
                        {
                            Columns.Append(", " + p.Name);
                            if (propVal is DateTime)
                            {

                                Values.Append(",'" + ((DateTime)propVal).ToString("yyyy-MM-dd HH:mm:ss") + "' ");

                            }
                            else
                            {
                                Values.Append(",'" + propVal.ToString() + "' ");
                            }

                        }
                        else if (propVal is float)
                        {
                            Columns.Append(", " + p.Name);
                            Values.Append("," + propVal.ToString().Replace(",", "."));
                        }
                        else if (propVal is decimal)
                        {
                            Columns.Append(", " + p.Name);
                            Values.Append("," + propVal.ToString().Replace(",", "."));
                        }
                        else
                        {
                            //N' (türk lirası güncelleme işleminde sembolü ? yaptığından dolayı ekleme işlemi yapılmıştır.)
                            Columns.Append(", " + p.Name);
                            Values.Append(",N'" + propVal.ToString().Replace("'", "''") + "' ");
                        }
                    }
                }
            }

            string Query = string.Format("INSERT INTO " + TableAttributeName + "({0}) OUTPUT INSERTED.id VALUES ({1}) ", Columns, Values);

            return Query;
        }


        public static string Update<T>(T sourceTable, WhereCondition crudCondition = null, UpdateSetCondition updateSetCondition = null) where T : class
        {
            StringBuilder sb = new StringBuilder();
            string TableAttributeName = GetTableAttribute.TableName<T>();
            if (TableAttributeName == "User")
            {
                TableAttributeName = "[User]";
            }
            sb.Append("Update " + TableAttributeName + " Set ");

            string id = string.Empty;
            if (updateSetCondition != null)
            {
                bool _firstInsert = true;
                PropertyInfo[] props = updateSetCondition.GetType().GetProperties();

                object propVal;

                foreach (PropertyInfo p in props)
                {

                    propVal = p.GetValue(updateSetCondition, null);
                    if (propVal != null)//&& propVal.ToString() != "0"
                    {
                        if (p.Name.ToLower() == "id")
                        {
                            id = propVal.ToString();
                            continue;
                        }
                        if (propVal is DateTime)
                        {

                            if (Convert.ToDateTime(propVal) == DateTime.MinValue || Convert.ToDateTime(propVal) == (DateTime)SqlDateTime.Null)
                            {
                                continue;
                            }

                        }
                        if (propVal is bool)
                        {
                            if (Convert.ToBoolean(propVal))
                            {
                                propVal = "1";
                            }
                            else
                            {
                                propVal = "0";
                            }
                        }

                        if (_firstInsert)
                        {
                            sb.Append(p.Name + "='" + propVal.ToString() + "' ");
                            _firstInsert = false;
                        }
                        else
                        {

                            if (propVal is float)
                            {
                                sb.Append(", " + p.Name + "=N'" + propVal.ToString().Replace(",", ".") + "'");
                            }
                            else if (propVal is decimal)
                            {
                                sb.Append(", " + p.Name + "=N'" + propVal.ToString().Replace(",", ".") + "'");

                            }
                            else
                            {
                                //N' (türk lirası güncelleme işleminde sembolü ? yaptığından dolayı ekleme işlemi yapılmıştır.)
                                sb.Append(", " + p.Name + "=N'" + propVal.ToString().Replace("'", "''") + "'");
                            }
                        }
                    }
                }

            }
            else
            {
                bool _firstInsert = true;
                PropertyInfo[] props = sourceTable.GetType().GetProperties();
                object propVal;
                foreach (PropertyInfo p in props)
                {

                    propVal = p.GetValue(sourceTable, null);
                    if (propVal != null)//&& propVal.ToString() != "0"
                    {
                        if (p.Name.ToLower() == "id")
                        {
                            id = propVal.ToString();
                            continue;
                        }
                        if (propVal is DateTime)
                        {

                            if (Convert.ToDateTime(propVal) == DateTime.MinValue || Convert.ToDateTime(propVal) == (DateTime)SqlDateTime.Null)
                            {
                                continue;
                            }

                        }
                        if (propVal is bool)
                        {
                            if (Convert.ToBoolean(propVal))
                            {
                                propVal = "1";
                            }
                            else
                            {
                                propVal = "0";
                            }
                        }

                        if (_firstInsert)
                        {
                            sb.Append(p.Name + "='" + propVal.ToString() + "' ");
                            _firstInsert = false;
                        }
                        else
                        {

                             if (propVal is float)
                            {
                                sb.Append(", " + p.Name + "=N'" + propVal.ToString().Replace(",", ".") + "'");
                            }
                            else if (propVal is decimal)
                            {
                                sb.Append(", " + p.Name + "=N'" + propVal.ToString().Replace(",", ".") + "'");

                            }
                            else
                            {
                                //N' (türk lirası güncelleme işleminde sembolü ? yaptığından dolayı ekleme işlemi yapılmıştır.)
                                sb.Append(", " + p.Name + "=N'" + propVal.ToString().Replace("'", "''") + "'");
                            }
                        }
                    }
                }
            }

            if (crudCondition != null)//Where eklemek için
            {
                StringBuilder sb1 = new StringBuilder();
                PropertyInfo[] options = crudCondition.GetType().GetProperties();

                sb1.Append(" Where 1=1 ");

                object propValue;
                foreach (PropertyInfo pi in options)
                {
                    propValue = pi.GetValue(crudCondition, null);
                    if (propValue != null)//&& propVal.ToString() != "0"
                    {
                        sb1.Append(" and " + pi.Name + "='" + propValue.ToString() + "' ");
                    }
                }

                sb.Append(sb1.ToString());
            }
            else
            {
                sb.Append(" Where id='" + id + "'");
            }


            return sb.ToString();
        }
        public static long ConvertToLong(this object value)
        {
            if (value == null)
                return 0;

            long oVal;

            if (long.TryParse(value.ToString(), out oVal))
            {
                return oVal;
            }
            else
            {
                return 0;
            }

        }
    }
}

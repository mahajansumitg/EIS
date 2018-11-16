using EIS.model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EIS
{
    static class Connection
    {
        private static string USER_ID = "sumit";
        private static string PSWD = "Mahajan123@";
        private static string DATA_SOURCE = "sumit-mahajan.database.windows.net";
        private static string CATALOG = "Employee_Information_System";

        private static SqlConnection connection = new SqlConnection(getConnectionString());



        public static void close()
        {
            connection.Close();
        }
        public static List<T> getData<T>(string query) 
        {
            connection.Open();
            SqlDataReader dataReader = new SqlCommand(query, connection).ExecuteReader();
            return processResponse<T>(dataReader);
        }

        public static Boolean setData<T>(T obj)
        {
            connection.Open();
            SqlDataReader dataReader = new SqlCommand(getInsertQuery(obj), connection).ExecuteReader();
            return dataReader.Read();
        }

        public static Boolean updateData<T>(T obj, String key)
        {
            connection.Open();
            SqlDataReader dataReader = new SqlCommand(getUpdateQuery(obj, key), connection).ExecuteReader();
            return dataReader.Read();
        }

        public static Boolean deleteData<T>(T obj, String key)
        {
            connection.Open();
            SqlDataReader dataReader = new SqlCommand(getDeleteQuery(obj, key), connection).ExecuteReader();
            return dataReader.Read();
        }


        private static List<T> processResponse<T>(SqlDataReader dataReader)
        {
            Type type = typeof(T);

            List<T> dataList = new List<T>();
            while (dataReader.Read())
            {
                T obj = (T) Activator.CreateInstance(type);
                foreach (FieldInfo info in type.GetFields())
                {
                    string data = dataReader.GetValue(dataReader.GetOrdinal(info.Name)).ToString();
                    string datatype = info.FieldType.Name.ToString();

                    switch (datatype)
                    {
                        case "String":
                            info.SetValue(obj, data);
                            break;
                        case "Int32":
                            info.SetValue(obj, Int32.Parse(data));
                            break;
                        case "DateTime":
                            info.SetValue(obj, DateTime.Parse(data));
                            break;
                        default:
                            throw new Exception("Unhanded Data Type Found: " +  datatype);
                    }
                }

                dataList.Add(obj);
            }

            connection.Close();
            return dataList;
        }


        private static string getInsertQuery<T>(T obj)
        {
            Type type = typeof(T);

            StringBuilder builder = new StringBuilder();
            builder.Append("Insert Into ");
            builder.Append(type.Name);

            builder.Append(" (");
            Array.ForEach( type.GetFields(),fieldInfo => builder.Append(fieldInfo.Name + ","));
            builder.Remove(builder.Length - 1, 1);
            builder.Append(") ");

            builder.Append(" Values(");
            foreach (FieldInfo info in type.GetFields())
            {
                if (info.FieldType.Equals(typeof(string)))
                    builder.Append("'" + info.GetValue(obj) + "',");
                else
                    builder.Append(info.GetValue(obj) + ",");
            }
            builder.Remove(builder.Length-1, 1);
            builder.Append(");");

            return builder.ToString();
        }

        private static string getUpdateQuery<T>(T obj, string key)
        {
            Type type = typeof(T);
            string keyValue = "";

            StringBuilder builder = new StringBuilder();
            builder.Append("Update ");
            builder.Append(type.Name);
            builder.Append(" Set ");
      
            foreach (FieldInfo info in type.GetFields())
            {
                string datatype = info.FieldType.Name.ToString();
                builder.Append(info.Name);
                builder.Append(" = ");
                switch (datatype)
                {
                    case "String":
                    case "DateTime":  
                        builder.Append("'" + info.GetValue(obj) + "',");
                        if (info.Name.Equals(key))  keyValue = "'" + info.GetValue(obj) + "'";
                        break;
                    case "Int32":
                        builder.Append(info.GetValue(obj) + ",");
                        if (info.Name.Equals(key))  keyValue = info.GetValue(obj).ToString();
                        break;
                    default:
                        throw new Exception("Unhanded Data Type Found: " + datatype);
                }
            }
            builder.Remove(builder.Length - 1, 1);

            builder.Append(" Where ");
            builder.Append(key);
            builder.Append(" = ");
            builder.Append(keyValue);
            builder.Append(";");

            return builder.ToString();
        }

        private static string getDeleteQuery<T>(T obj, string key)
        {
            Type type = typeof(T);
            string keyValue = "";

            StringBuilder builder = new StringBuilder();
            builder.Append("Delete From");
            builder.Append(type.Name);
            builder.Append(" Set ");

            foreach (FieldInfo info in type.GetFields())
            {
                string datatype = info.FieldType.Name.ToString();
                builder.Append(info.Name);
                builder.Append(" = ");
                switch (datatype)
                {
                    case "String":
                    case "DateTime":
                        builder.Append("'" + info.GetValue(obj) + "',");
                        if (info.Name.Equals(key)) keyValue = "'" + info.GetValue(obj) + "'";
                        break;
                    case "Int32":
                        builder.Append(info.GetValue(obj) + ",");
                        if (info.Name.Equals(key)) keyValue = info.GetValue(obj).ToString();
                        break;
                    default:
                        throw new Exception("Unhanded Data Type Found: " + datatype);
                }
            }
            builder.Remove(builder.Length - 1, 1);

            builder.Append(" Where ");
            builder.Append(key);
            builder.Append(" = ");
            builder.Append(keyValue);
            builder.Append(";");

            return builder.ToString();
        }

        private static string getConnectionString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Data Source=");
            builder.Append(DATA_SOURCE);
            builder.Append(";Initial Catalog=");
            builder.Append(CATALOG);
            builder.Append(";User ID=");
            builder.Append(USER_ID);
            builder.Append(";Password=");
            builder.Append(PSWD);

            return builder.ToString(); 
        }
    }
}

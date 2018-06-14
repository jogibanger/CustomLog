using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomLogs
{


    public class CreateLogs
    {
        string TableName = string.Empty;
        string objConnection = string.Empty;
        
        /// <summary>
        /// Please enter the Table Name.
        /// Please Enter the ConnectionString value.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="ConnectionString"></param>
        public CreateLogs(string tableName, string ConnectionString)
        {
            LogWriter obj = new LogWriter("Have a Nice Day");
            TableName = tableName;
            objConnection = ConnectionString;
        }
        public CreateLogs()
        {
            throw new NotImplementedException("Table Name Must be Requireds");
        }
        public void CreateDatabaseLogs(object obj)
        {
            string IsTableCreated = string.Empty;
            Dictionary<string, string> ObjDictionary = new Dictionary<string, string>();
            var commandStr = "select name from sysobjects where name = '" + TableName + "'";
            try
            {

                using (SqlConnection connection = new SqlConnection(objConnection))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(commandStr))
                    {
                        command.Connection = connection;
                        IsTableCreated = Convert.ToString(command.ExecuteScalar());
                    }

                }

                string ClassName = obj.GetType().BaseType.Name;
                Type myClassType = obj.GetType();
                PropertyInfo[] properties = myClassType.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    ObjDictionary.Add(property.Name, Convert.ToString(property.GetValue(obj, null)));

                    //Console.WriteLine("Name: " + property.Name + ", Value: " + property.GetValue(obj, null));
                }
                bool IsNewTableCreated = false;
                if (string.IsNullOrEmpty(IsTableCreated))
                {
                    StringBuilder ObjCreateTable = new StringBuilder();
                    ObjCreateTable.Append("CREATE TABLE " + TableName + " (");
                    ObjCreateTable.Append("" + TableName + "_ID int IDENTITY(1,1) NOT NULL, ");

                    foreach (var item in ObjDictionary)
                    {
                        ObjCreateTable.Append("[" + item.Key + "] varchar(MAX) NULL, ");

                    }

                    //",First_Name char(50),Last_Name char(50),Address char(50),City char(50),Country char(25),Birth_Date datetime)");
                    ObjCreateTable.Append("[CreateDateTime] [datetime] NULL )");
                    ObjCreateTable.Append("ALTER TABLE [dbo]." + TableName + " ADD  CONSTRAINT [" + TableName + " _CreateDateTime]  DEFAULT (getdate()) FOR [CreateDateTime]");
                    string CreateTable = ObjCreateTable.ToString();

                    using (SqlConnection connection = new SqlConnection(objConnection))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(CreateTable))
                        {
                            command.Connection = connection;
                            int IsTableCreateds = command.ExecuteNonQuery();
                            IsNewTableCreated = true;
                        }

                    }
                }
                if (IsNewTableCreated == true || !string.IsNullOrEmpty(IsTableCreated))
                {
                    StringBuilder ObjCreateTable = new StringBuilder();
                    string ColumnName = string.Empty;
                    int counter1 = 0;
                    foreach (var item in ObjDictionary)
                    {
                        counter1 += 1;
                        if (counter1 == ObjDictionary.Count)
                        {
                            ColumnName += "[" + item.Key + "] ";
                        }
                        else
                        {
                            ColumnName += "[" + item.Key + "] ,";

                        }

                    }

                    ObjCreateTable.Append("INSERT INTO [dbo]. " + TableName + "(" + ColumnName + " ) VALUES (");
                    int counter = 0;
                    foreach (var item in ObjDictionary)
                    {
                        counter += 1;
                        if (counter == ObjDictionary.Count)
                        {
                            ObjCreateTable.Append("'" + item.Value + "' )");
                        }
                        else
                        {
                            ObjCreateTable.Append("'" + item.Value + "',");
                        }

                    }
                    //ObjCreateTable.Append("'"+DateTime.Now.ToShortDateString()+"' )");

                    using (SqlConnection connection = new SqlConnection(objConnection))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(ObjCreateTable.ToString()))
                        {
                            command.Connection = connection;
                            int IsTableCreateds = command.ExecuteNonQuery();

                        }

                    }



                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
       
    }
}

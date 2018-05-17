using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCustomLogs
{
    public class NameOfValue
    {
        public string Name { get; set; }
        public string Values { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //string ConnectionString = "Data Source=SV01;User ID=sa;Password=mpx123;Database=dummy";
            //CustomLogs.CreateLogs aa = new CustomLogs.CreateLogs("CustomLogs",ConnectionString);
            //NameOfValue obj=new NameOfValue();
            //obj.Name = "Have a nice day";
            //obj.Values = "1";
            //aa.CreateDatabaseLogs(obj);
            CustomLogs.LogWriter log = new CustomLogs.LogWriter("asf");

            

        }
    }
}

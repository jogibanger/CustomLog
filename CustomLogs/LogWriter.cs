using System;
using System.IO;
using System.Reflection;

namespace CustomLogs
{
    public class LogWriter
    {
        private string m_exePath = string.Empty;
        private string FolderName = string.Empty;
        private string FileName = string.Empty;
        public LogWriter(string logMessage)
        {
            LogWrite(logMessage);
        }
        /// <summary>
        /// Log message and Folder Name
        /// In this method we can write the log with own folder Name
        /// </summary>
        /// <param name="logMessage"> </param>
        /// <param name="folderName"></param>
        public LogWriter(string logMessage, string folderName)
        {
            this.FolderName = folderName;          
            LogWrite(logMessage);
        }

        public LogWriter(string logMessage,string folderName,string fileName )
        {
            this.FolderName = folderName;
            this.FileName = fileName;
            LogWrite(logMessage);
        }
        /// <summary>
        /// You can write here..... Custom Log
        /// </summary>
        /// <param name="logMessage"></param>
        public void LogWrite(string logMessage)
        {

            if (!string.IsNullOrEmpty(this.FolderName))
            {
                m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                m_exePath += "\\" + this.FolderName;
                if(!Directory.Exists(m_exePath))
                {
                  DirectoryInfo dir=  Directory.CreateDirectory(m_exePath);
                   
                }

            }
            else
            {
                m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
            try
            {
                if (string.IsNullOrEmpty(FileName))
                    FileName = DateTime.Now.Day+"_"+DateTime.Now.Month+"_"+DateTime.Now.Year+"_"+ "log.txt";
                else
                    FileName = DateTime.Now.Day+"_"+DateTime.Now.Month+"_"+DateTime.Now.Year+"_" + FileName + ".txt";
                using (StreamWriter w = File.AppendText(m_exePath + "\\"+FileName))
                {
                    Log(logMessage, w);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
            }
        }
    }
}

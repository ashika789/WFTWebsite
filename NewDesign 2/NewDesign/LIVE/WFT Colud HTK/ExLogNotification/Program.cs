using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Net;
using WFTCloud.DataAccess;


namespace ExLogNotification
{
    class Program
    {
        static SqlConnection sqlConnection = new SqlConnection();
        static string LogFileData = string.Empty;
        static cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();

        static void Main(string[] args)
        {
            try
            {
                LogExecutionComment("---------------------------------------------------------", false);
                LogExecutionComment("Automated ExLog Notification Exe Started...", true);
                LogExecutionComment(string.Empty, true);

                DataTable dtExLogRecords = new DataTable();

                sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                sqlConnection.Open();
                DateTime ToDate= DateTime.Now;
                double TimeLimit = Convert.ToDouble(ConfigurationManager.AppSettings["TimeLimit"]);
                DateTime FromDate = DateTime.Now.AddHours(TimeLimit);
                SqlCommand command = new SqlCommand("pr_GetLatestExceptionLogForNotification", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@FromTime", SqlDbType.DateTime).Value = FromDate;
                command.Parameters.Add("@ToTime", SqlDbType.DateTime).Value = ToDate ;
                SqlDataReader sqlData = command.ExecuteReader();
                dtExLogRecords.Load(sqlData);
                sqlData.Close();

                LogExecutionComment("Automated ExLog...", true);
                string Message = "<table cellpadding='3' cellspacing='0' width='100%' align='center' border='1' style='margin-top:-10px;'>"+
                    "<tr><td><b>Exception ID</b></td><td><b>Page Name</b></td><td><b>Ex Message</b></td><td><b>Ex StackTrace</b></td><td><b>Date&Time</b></td><td>Method Name</td><td>User Name</td></tr>";
                foreach (DataRow dtRow in dtExLogRecords.Rows)
                {
                    Message += ("<tr><td>" + dtRow["ExceptionID"].ToString() + "</td><td>"
                               + dtRow["ClassName"].ToString() + "</td><td>"
                               + dtRow["ExMessage"].ToString() + "</td><td>"
                               + dtRow["ExStackTrace"].ToString()) + "</td><td>"
                               + dtRow["ExDateTime"].ToString() + "</td><td>"
                               + dtRow["ExceptionMethodName"].ToString()+ "</td><td>"
                               + dtRow["UserName"].ToString() + "</td></tr>";
                }
                Message += "</table>";
                if (dtExLogRecords.Rows.Count > 0)
                {
                    string EmailContent = File.ReadAllText("./EmailTemplates/ExLogMail.html");
                    EmailContent = EmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                    EmailContent = EmailContent.Replace("++Content++", Message);
                    SendEXlogNotificationEmail(EmailContent, "Ex Log Details from " + FromDate.ToString("dd-MMM-yyyy hh:mm tt") + " to " + ToDate.ToString("dd-MMM-yyyy hh:mm tt"), false);
                }

                LogExecutionComment("---------------------------------------------------------", false);
                LogExecutionComment("Automated ExLog Notification Exe Ended...", true);
                LogExecutionComment(string.Empty, true);

                LogTransactionDatatoFile(LogFileData);
            }
            catch (Exception ex)
            {

                LogException("ExLogNotificationExe", "Main", ex.Message, ex.StackTrace, DateTime.Now);
                LogExecutionComment("Error: " + ex.Message, true);
                LogTransactionDatatoFile(LogFileData);
            }
        }


        private static void LogExecutionComment(string Description, bool LogDate)
        {
            string tmpDescription = string.Empty;

            if (LogDate)
                tmpDescription = DateTime.Now.ToString() + " : " + Description;
            else
                tmpDescription = Description;

            Console.WriteLine(tmpDescription);

            LogFileData += tmpDescription + Environment.NewLine;
        }

        private static void LogTransactionDatatoFile(string LogDetails)
        {
            string fileName = String.Format("{0:yyyy-MM-dd}", DateTime.Now) + ".Log";
            string Folder = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";

            bool isExists = System.IO.Directory.Exists(Folder);

            if (!isExists)
                System.IO.Directory.CreateDirectory(Folder);

            string FullFilePath = Path.Combine(Folder, fileName);

            TextWriter txtWr = new StreamWriter(FullFilePath, true);
            txtWr.Write(LogDetails);
            txtWr.Close();

        }

        public static void SendEXlogNotificationEmail(string messageBody, string subject, bool sendInBCC)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
                string FromMail = Convert.ToString(ConfigurationManager.AppSettings["SMTPUsernameAdminNotification"]);
                mail.From = new MailAddress(FromMail);
                string ToMail = objDBContext.WftSettings.FirstOrDefault(s => s.SettingKey == "MAINTENANCE_MAIL").SettingValue;
                if (!sendInBCC)
                    mail.To.Add(ToMail);
                else
                {
                    mail.Bcc.Add(ToMail);
                }
                mail.Subject = subject;
                mail.Body = messageBody;
                mail.IsBodyHtml = true;
                SmtpServer.Port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
                SmtpServer.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTPUsernameAdminNotification"], ConfigurationManager.AppSettings["SMTPPasswordAdminNotification"]);
                SmtpServer.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSSL"]);

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                LogException("ExLogNotificationExe", "SendEXlogNotificationEmail", ex.Message, ex.StackTrace, DateTime.Now);
            }
        }
        public static void LogException(string pageNameOrClassName, string MethodName, string ExMessage, string ExStackTrace, DateTime ExcepDateTime)
        {
            //Log exception into database.

            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                sqlConnection.Open();

                SqlCommand command = new SqlCommand("InsertExceptionLog", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ExceptionClassName", SqlDbType.VarChar).Value = pageNameOrClassName;
                command.Parameters.Add("@ExceptionMessage", SqlDbType.VarChar).Value = ExMessage;
                command.Parameters.Add("@ExceptionStackTrace", SqlDbType.VarChar).Value = ExStackTrace;
                command.Parameters.Add("@ExceptionDateTime", SqlDbType.VarChar).Value = ExcepDateTime;
                command.Parameters.Add("@ExceptionMethodName", SqlDbType.VarChar).Value = MethodName;
                command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = "Automated ExLog Notification Exe";
                command.ExecuteNonQuery();

                sqlConnection.Close();
            }
            catch (SqlException)
            {
                //Ignore exception

            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }
    }
}

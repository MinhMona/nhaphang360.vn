using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.IO;
using System.Web.Caching;
using System.Text;
using System.Web.Security;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Net;
using System.Xml;
using System.Security.Cryptography;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Web.Script.Serialization;
using WebUI.Business;
using MB.Extensions;
using NHST.Controllers;
using NHST.Models;
using Supremes;
using System.Drawing;
using System.Drawing.Text;
using WebPush;
using System.Data.OleDb;

namespace NHST.Bussiness
{
    public class PJUtils
    {
        public static DataTable ReadDataExcel(string FilePath, string Extension, string isHDR)
        {
            DataTable dt = new DataTable();

            string excelConString = "";

            //Get connection string using extension 
            switch (Extension)
            {
                //If uploaded file is Excel 1997-2007.
                case ".xls":
                    excelConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";
                    break;
                //If uploaded file is Excel 2007 and above
                case ".xlsx":
                    excelConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";
                    break;
            }

            excelConString = string.Format(excelConString, FilePath);

            try
            {
                using (OleDbConnection excelOledbConnection = new OleDbConnection(excelConString))
                {
                    using (OleDbCommand excelDbCommand = new OleDbCommand())
                    {
                        using (OleDbDataAdapter excelDataAdapter = new OleDbDataAdapter())
                        {
                            excelDbCommand.Connection = excelOledbConnection;

                            excelOledbConnection.Open();
                            //Get schema from excel sheet
                            DataTable excelSchema = GetSchemaFromExcel(excelOledbConnection);
                            //Get sheet name
                            string sheetName = excelSchema.Rows[0]["TABLE_NAME"].ToString();
                            excelOledbConnection.Close();

                            //Read Data from First Sheet.
                            excelOledbConnection.Open();
                            excelDbCommand.CommandText = "SELECT * From [" + sheetName + "]";
                            excelDataAdapter.SelectCommand = excelDbCommand;
                            //Fill datatable from adapter
                            excelDataAdapter.Fill(dt);
                            excelOledbConnection.Close();
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static DataTable GetSchemaFromExcel(OleDbConnection excelOledbConnection)
        {
            return excelOledbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        }

        public static string Encrypt(string key, string data)
        {
            data = data.Trim();
            byte[] keydata = Encoding.ASCII.GetBytes(key);
            string md5String = BitConverter.ToString(new
            MD5CryptoServiceProvider().ComputeHash(keydata)).Replace("-", "").ToLower();
            byte[] tripleDesKey = Encoding.ASCII.GetBytes(md5String.Substring(0, 24));
            TripleDES tripdes = TripleDESCryptoServiceProvider.Create();
            tripdes.Mode = CipherMode.ECB;
            tripdes.Key = tripleDesKey;
            tripdes.GenerateIV();
            MemoryStream ms = new MemoryStream();
            CryptoStream encStream = new CryptoStream(ms, tripdes.CreateEncryptor(),
            CryptoStreamMode.Write);
            encStream.Write(Encoding.ASCII.GetBytes(data), 0,
            Encoding.ASCII.GetByteCount(data));
            encStream.FlushFinalBlock();
            byte[] cryptoByte = ms.ToArray();
            ms.Close();
            encStream.Close();
            return Convert.ToBase64String(cryptoByte, 0, cryptoByte.GetLength(0)).Trim();
        }

        public static string Decrypt(string key, string data)
        {
            byte[] keydata = System.Text.Encoding.ASCII.GetBytes(key);
            string md5String = BitConverter.ToString(new
            MD5CryptoServiceProvider().ComputeHash(keydata)).Replace("-", "").ToLower();
            byte[] tripleDesKey = Encoding.ASCII.GetBytes(md5String.Substring(0, 24));
            TripleDES tripdes = TripleDESCryptoServiceProvider.Create();
            tripdes.Mode = CipherMode.ECB;
            tripdes.Key = tripleDesKey;
            byte[] cryptByte = Convert.FromBase64String(data);
            MemoryStream ms = new MemoryStream(cryptByte, 0, cryptByte.Length);
            ICryptoTransform cryptoTransform = tripdes.CreateDecryptor();
            CryptoStream decStream = new CryptoStream(ms, cryptoTransform,
            CryptoStreamMode.Read);
            StreamReader read = new StreamReader(decStream);
            return (read.ReadToEnd());
        }

        public static bool SendMail(string strFrom, string pass, string strTo, string strSubject, string strMsg, string cc)
        {
            try
            {
                // Create the mail message
                MailMessage objMailMsg = new MailMessage(strFrom, strTo);

                objMailMsg.BodyEncoding = Encoding.UTF8;
                objMailMsg.Subject = strSubject;
                objMailMsg.CC.Add(cc);
                objMailMsg.IsBodyHtml = true;
                objMailMsg.Body = strMsg;
                SmtpClient objSMTPClient = new SmtpClient();

                objSMTPClient.Host = "202.43.110.136";
                objSMTPClient.Port = 25;
                objSMTPClient.EnableSsl = false;
                objSMTPClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                objSMTPClient.Credentials = new NetworkCredential(strFrom, pass);
                objSMTPClient.Timeout = 20000;
                objSMTPClient.Send(objMailMsg);
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool SendMailGmail(string strFrom, string pass, string strTo, string strSubject, string strMsg, string cc)
        {
            try
            {
                string fromAddress = strFrom;
                string mailPassword = pass;       // Mail id password from where mail will be sent.
                string messageBody = strMsg;

                // Create smtp connection.
                SmtpClient client = new SmtpClient();
                client.Port = 587;//outgoing port for the mail.
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(fromAddress, mailPassword);

                // Fill the mail form.
                var send_mail = new MailMessage();
                send_mail.IsBodyHtml = true;
                //address from where mail will be sent.
                send_mail.From = new MailAddress(strFrom);
                //address to which mail will be sent.           
                send_mail.To.Add(new MailAddress(strTo));
                //subject of the mail.
                send_mail.Subject = strSubject;
                send_mail.Body = messageBody;
                client.Send(send_mail);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool SendMailGmail_new(string strTo, string strSubject, string strMsg, string cc)
        {
            try
            {
                string fromAddress = "hotronhaphang360@gmail.com";
                string mailPassword = "zgqounxnapimzdqe";       // Mail id password from where mail will be sent.
                string messageBody = strMsg;

                // Create smtp connection.
                SmtpClient client = new SmtpClient();
                client.Port = 587;//outgoing port for the mail.
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(fromAddress, mailPassword);

                // Fill the mail form.
                var send_mail = new MailMessage();
                send_mail.IsBodyHtml = true;
                //address from where mail will be sent.
                send_mail.From = new MailAddress(fromAddress);
                //address to which mail will be sent.           
                send_mail.To.Add(new MailAddress(strTo));
                //subject of the mail.
                send_mail.Subject = strSubject;
                send_mail.Body = messageBody;
                client.Send(send_mail);
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static void ExportToExcel(DataTable dt)
        {

            if (dt.Rows.Count > 0)
            {

                string filename = "ProjectReport_" + DateTime.Now.Date + ".xls";

                string excelHeader = "Project Report";

                System.IO.StringWriter tw = new System.IO.StringWriter();

                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

                DataGrid dgGrid = new DataGrid();

                dgGrid.DataSource = dt;

                dgGrid.DataBind();

                // Report Header
                hw.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
                hw.WriteLine("<b><u><font size='3'> " + excelHeader + " </font></u></b>");

                //Get the HTML for the control.

                dgGrid.RenderControl(hw);

                //Write the HTML back to the browser.

                //Response.ContentType = “application/vnd.ms-excel”;

                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";

                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");

                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                //this.EnableViewState = false;

                HttpContext.Current.Response.Write(tw.ToString());

                HttpContext.Current.Response.End();
            }
        }
        public static string ReturnStatusPayHelp(int status)
        {
            if (status == 2)
            {
                return "<span class='bg-black'>Đã hủy</span>";
            }
            else if (status == 0)
            {
                return "<span class='bg-red'>Chưa thanh toán</span>";
            }
            else if (status == 1)
            {
                return "<span class='bg-blue'>Đã thanh toán</span>";
            }
            else if (status == 3)
            {
                return "<span class='bg-green'>Đã hoàn thành</span>";
            }
            else
            {
                return "<span class='bg-yellow'>Đã xác nhận</span>";
            }
        }
        public static string ReturnIsNotComplete(bool check)
        {
            if (check == true)
            {
                return "<input type=\"checkbox\" disabled checked>";
            }
            else
                return "<input type=\"checkbox\" disabled>";
        }
        public static void ShowMsg(string txt, bool? isRefresh, System.Web.UI.Page page)
        {
            //isRefresh = isRefresh == null;
            var content = txt;
            var _type = string.Empty;
            switch (txt.Trim().ToLower())
            {
                case "100":
                    content = "Tên hoặc mã đã được sử dụng";
                    _type = "i";
                    isRefresh = false;
                    break;
                case "101":
                    content = "Không tìm thấy đối tượng";
                    _type = "i";
                    isRefresh = false;
                    break;
                case "102":
                    content = "Thực hiện thành công !";
                    _type = "";
                    isRefresh = true;
                    break;
                case "103":
                    content = "Thực hiện thất bại !";
                    _type = "e";
                    isRefresh = false;
                    break;
            }
            ShowMessageBoxSwAlert(content, _type, isRefresh, page);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="type">e: Error,i: warning, default: succes</param>
        /// <param name="isRefresh"></param>
        /// <param name="page"></param>
        /// 
        public static void ShowMessageBoxSwAlert(string txt, string type, bool? isRefresh, System.Web.UI.Page page)
        {
            txt = new JavaScriptSerializer().Serialize(txt);
            string p;
            switch (type)
            {

                case "e":
                    p = "error";
                    break;
                case "i":
                    p = "warning";
                    break;

                default:
                    p = "success";
                    break;
            }
            JavaScript.AfterPageLoad(page).ExecuteCustomScript("swal({ title: 'Thông báo',text:' " + txt + "',type: '" + p + "'}" + (Convert.ToBoolean(isRefresh.ToString()) ? ", function () { window.location.replace(window.location.href); });" : ");"));
        }
        public static void ShowMessageBoxSwAlertBackToLink(string txt, string type, bool? isRefresh, string BackLink, System.Web.UI.Page page)
        {
            txt = new JavaScriptSerializer().Serialize(txt);
            string p;
            switch (type)
            {
                case "e":
                    p = "error";
                    break;
                case "i":
                    p = "warning";
                    break;

                default:
                    p = "success";
                    break;
            }
            JavaScript.AfterPageLoad(page).ExecuteCustomScript("swal({ title: 'Thông báo',text:' " + txt + "',type: '" + p + "'}" + (Convert.ToBoolean(isRefresh.ToString()) ? ", function () { window.location.replace('" + BackLink + "'); });" : ");"));
        }
        public static string GetIcon(object o)
        {
            if (o == null)
                return "/NO-IMAGE.jpg";
            if (!string.IsNullOrEmpty(o.ToString()))
                return o.ToString();
            return "/NO-IMAGE.jpg";
        }
        public static string SubString(string title, int length)
        {
            if (string.IsNullOrEmpty(title))
                return "";

            if (!title.Contains(" "))
            {
                if (title.Length > length)
                    title = title.Substring(0, length - 1) + "...";
            }
            else if (title.Length >= length)
            {
                int i = length - 1;
                while (title.Substring(i--, 1) != " " && i > 0) ;
                if (i == 0)
                    return title.Substring(0, length - 4) + " ...";
                else
                    return title.Substring(0, i + 1) + " ...";
            }

            return title;
        }
        public static string RandomString(int numberrandom)
        {
            //var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var chars = "0123456789";
            var stringChars = new char[numberrandom];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }
        public static string RandomStringWithText(int numberrandom)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            //var chars = "0123456789";
            var stringChars = new char[numberrandom];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }

        public static string requestOutStockStatus(int status)
        {
            if (status == 1)
                return "<span class=\"statusre bg-red\">Chưa xuất</span>";
            else
                return "<span class=\"statusre bg-blue\">Đã xuất</span>";
        }

        public static bool ConvertStringToBool(string i)
        {
            if (!string.IsNullOrEmpty(i))
            {
                i = i.ToLower();
                if (i == "1" || i == "true")
                    return true;
                else return false;
            }
            else return false;

        }
        public static string StatusToRequest(object i)
        {
            if (i != null)
            {
                if (i.ToString() == "1")
                {
                    return "<span class=\"badge yellow darken-2 white-text border-radius-2\">Chưa kích hoạt</span>";
                }
                else if (i.ToString() == "2")
                {
                    return "<span class=\"badge green darken-2 white-text border-radius-2\">Đã kích hoạt</span>";
                }
                else
                {
                    return "<span class=\"badge red darken-2 white-text border-radius-2\">Đang bị khóa</span>";
                }

            }
            else return "<span class='badge red darken-2 white-text border-radius-2'>Đang bị khóa</span>";
        }
        public static string IntToRequestAdmin(int i)
        {
            if (i == 0)
                return "<span class=\"bg-red\">Chờ đặt cọc</span>";
            else if (i == 1)
                return "<span class=\"bg-black\">Hủy đơn hàng</span>";
            else if (i == 2)
                return "<span class=\"bg-bronze\">Khách đã đặt cọc</span>";
            else if (i == 3)
                return "<span class=\"bg-green\">Chờ duyệt đơn</span>";
            else if (i == 4)
                return "<span class=\"bg-green\">Shop phát hàng</span>";
            else if (i == 5)
                return "<span class=\"bg-green\">Đã mua hàng</span>";
            else if (i == 6)
                return "<span class=\"bg-green\">Đã về kho TQ</span>";
            else if (i == 7)
                return "<span class=\"bg-orange\">Đã về kho VN</span>";
            else if (i == 8)
                return "<span class=\"bg-yellow\">Chờ thanh toán</span>";
            else if (i == 9)
                return "<span class=\"bg-blue\">Khách đã thanh toán</span>";
            else if (i == 10)
                return "<span class=\"bg-blue\">Đã hoàn thành</span>";
            else
                return "";

        }
        public static string IntToRequestAdminReturnBG(int i)
        {
            if (i == 0)
                return "bg-red";
            else if (i == 1)
                return "bg-black";
            else if (i == 2)
                return "bg-bronze";
            else if (i == 3)
                return "bg-green";
            else if (i == 4)
                return "bg-green";
            else if (i == 5)
                return "bg-green";
            else if (i == 6)
                return "bg-green";
            else if (i == 7)
                return "bg-orange";
            else if (i == 8)
                return "bg-yellow";
            else if (i == 9)
                return "bg-blue";
            else if (i == 10)
                return "bg-blue";
            else
                return "";

        }
        public static string IntToRequestClient(int i)
        {
            if (i == 0)
                return "<span class=\"bg-red\">Chờ đặt cọc</span>";
            else if (i == 1)
                return "<span class=\"bg-black\">Hủy đơn hàng</span>";
            else if (i == 2)
                return "<span class=\"bg-bronze\">Đã đặt cọc</span>";
            else if (i == 3)
                return "<span class=\"bg-green\">Chờ duyệt đơn</span>";
            else if (i == 4)
                return "<span class=\"bg-green\">Shop phát hàng</span>";
            else if (i == 5)
                return "<span class=\"bg-green\">Đã mua hàng</span>";
            else if (i == 6)
                return "<span class=\"bg-green\">Đang về Việt Nam</span>";
            else if (i == 7)
                return "<span class=\"bg-green\">Đã nhận hàng tại kho đích</span>";
            else if (i == 8)
                return "<span class=\"bg-yellow\">Chờ thanh toán</span>";
            else if (i == 9)
                return "<span class=\"bg-blue\">Khách đã thanh toán</span>";
            else if (i == 10)
                return "<span class=\"bg-blue\">Đã hoàn thành</span>";
            else
                return "";
        }
        public static string generateTransportationStatus(int i)
        {
            if (i == 0)
                return "<span class=\"bg-black\">Đã hủy</span>";
            else if (i == 1)
                return "<span class=\"bg-red\">Chờ duyệt</span>";
            else if (i == 2)
                return "<span class=\"bg-bronze\">Đã duyệt</span>";
            else if (i == 3)
                return "<span class=\"bg-green\">Đang xử lý</span>";
            else if (i == 4)
                return "<span class=\"bg-green\">Đang vận chuyển về kho đích</span>";
            else if (i == 5)
                return "<span class=\"bg-green\">Đã về kho VN</span>";
            else if (i == 6)
                return "<span class=\"bg-green\">Đã thanh toán</span>";
            else
                return "<span class=\"bg-green\">Đã hoàn thành</span>";
        }
        public static string BoolToRequest(object i)
        {
            if (i != null)
            {
                return ConvertStringToBool(i.ToString()) == true ? "<span class='red'>Đang yêu cầu</span>" : "<span class='blue'>Không</span>";
            }
            else return "<span class='blue'>Không</span>";
        }
        public static string ShowStatusPayHistory(int status)
        {
            if (status == 2)
                return "<span class=\"bg-bronze\">Đặt cọc</span>";
            else if (status == 3)
                return "<span class=\"bg-yellow\">Đặt cọc</span>";
            else if (status == 12)
                return "<span class=\"bg-red\">Sản phẩm hết hàng hoặc giảm giá trả lại cọc</span>";
            else
                return "<span class=\"bg-blue\">Thanh toán</span>";
        }
        public static int CheckRoleShowRosePrice()
        {
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            if (ac != null)
            {
                if (ac.RoleID == 6 || ac.RoleID == 4 || ac.RoleID == 5 || ac.RoleID == 8)
                    return 1;
                else
                    return 0;
            }
            else
                return 0;
        }
        public static string BoolToStatus(string i)
        {
            return ConvertStringToBool(i) == true ? "<span class='show-stat-s'>Hiện</span>" : "<span class='show-stat-w'>Ẩn</span>";

        }
        public static string GetTradeType(int TradeType)
        {
            if (TradeType == 1)
            {
                return "Đặt cọc";
            }
            else if (TradeType == 2)
            {
                return "Nhận lại tiền đặt cọc";
            }
            else if (TradeType == 3)
            {
                return "Thanh toán hóa đơn";
            }
            else if (TradeType == 4)
            {
                return "Admin chuyển tiền";
            }
            else if (TradeType == 5)
            {
                return "Rút tiền";
            }
            else if (TradeType == 6)
            {
                return "Hủy lệnh rút tiền";
            }
            else if (TradeType == 7)
            {
                return "Hoàn tiền khiếu nại";
            }
            else if (TradeType == 8)
            {
                return "Thanh toán vận chuyển hộ";
            }
            else if (TradeType == 9)
            {
                return "Thanh toán hộ";
            }
            else
            {
                return "...";
            }


        }
        public static string BoolToStatusShow(string i)
        {
            if (!string.IsNullOrEmpty(i))
                return ConvertStringToBool(i) == true ? "<span class='show-stat-w'>Ẩn</span>" : "<span class='show-stat-s'>Hiện</span>";
            else
                return "<span class='show-stat-s'>Hiện</span>";

        }
        public static string ReturnStatusWithdraw(int status)
        {
            if (status == 1)
            {
                return "<span class='bg-red'>Đang chờ duyệt</span>";
            }
            else if (status == 2)
            {
                return "<span class='bg-blue'>Đã duyệt</span>";
            }
            else
            {
                return "<span class='bg-black'>Hủy lệnh</span>";
            }
        }
        public static string ReturnPosition(int BenefitSide)
        {
            if (BenefitSide == 1)
            {
                return "Trái";
            }
            else
            {
                return "Phải";
            }
        }
        public static string ReturnPlace(int Place)
        {
            if (Place == 1)
            {
                return "Hà Nội";
            }
            else
            {
                return "Việt Trì";
            }
        }
        public static string ReturnHidden(bool IsHidden)
        {
            if (IsHidden == true)
            {
                return "<span class=\"red\">Ẩn</span>";
            }
            else
            {
                return "<span class=\"blue\">Hiện</span>";
            }
        }
        public static string ReturnRoleName(string name)
        {
            if (name == "Store")
            {
                return "<span class='yellow'>Cửa hàng</span>";
            }
            else if (name == "Customer")
            {
                return "<span class=''>Người dùng</span>";
            }
            return name;
        }
        public static string ReturnSymbol(int Type)
        {
            if (Type == 1)
            {
                return "-";
            }
            else
                return "+";
        }
        public static string ReturnStatusComplainRequest(int status)
        {
            if (status == 1)
            {
                return "<span class='red'>Chưa duyệt</span>";
            }
            else if (status == 2)
            {
                return "<span class='yellow'>Đang xử lý</span>";
            }
            else if (status == 3)
            {
                return "<span class='blue'>Đã xử lý</span>";
            }
            else
            {
                return "<span class='orange'>Đã hủy</span>";
            }

        }
        public static string ReturnStatusRequest(string status)
        {
            if (status == "1")
            {
                return "<span class='red'>Đang chờ</span>";
            }
            else if (status == "2")
            {
                return "<span class='blue'>Đã xử lý</span>";
            }
            else
            {
                return "<span class='orange'>Đã hủy</span>";
            }

        }

        public static string GetFirstProductIMG(string MainorderID)
        {
            var orders = OrderController.GetByMainOrderID(MainorderID.ToInt(0));
            string IMG = "";
            if (orders.Count > 0)
            {
                IMG = orders[0].image_origin;
            }
            return IMG;
        }
        public static List<countries> loadprefix()
        {
            string file = HttpContext.Current.Server.MapPath("~/Models/phonecode.json");
            //deserialize JSON from file  
            string Json = System.IO.File.ReadAllText(file);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            var personlist = ser.Deserialize<List<countries>>(Json);
            List<countries> cs = new List<countries>();
            foreach (var item in personlist)
            {
                countries c = new countries();
                c.name = item.name;
                c.dial_code = item.dial_code;
                c.code = item.code;
                cs.Add(c);
            }
            return cs;
        }
        public class countries
        {
            public string name { get; set; }
            public string dial_code { get; set; }
            public string code { get; set; }
        }
        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
        public static string GetRandomStringByDateTime()
        {
            DateTime dt = DateTime.Now;
            string returnD = dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString()
                           + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString() + dt.Millisecond.ToString();
            return returnD;
        }
        public static string ShowFirstNameByUID(int ID)
        {
            var ui = AccountInfoController.GetByUserID(ID);
            if (ui != null)
            {
                return ui.FirstName;
            }
            else
                return "";
        }
        public static string ShowLastNameByUID(int ID)
        {
            var ui = AccountInfoController.GetByUserID(ID);
            if (ui != null)
            {
                return ui.LastName;
            }
            else
                return "";
        }
        public static string IntToStringStatusPackage(int status)
        {
            if (status == 0)
                return "<span class=\"bg-bronze\">Mới tạo</span>";
            else if (status == 1)
                return "<span class=\"bg-green\">Đang chuyển về VN</span>";
            else if (status == 2)
                return "<span class=\"bg-blue\">Đã nhận hàng tại kho đích</span>";
            else
                return "<span class=\"bg-red\">Đã hủy</span>";
        }
        public static string IntToStringStatusSmallPackage(int status)
        {
            if (status == 0)
                return "<span class=\"bg-black\">Đã hủy</span>";
            else if (status == 1)
                return "<span class=\"bg-red\">Mới đặt - chưa về kho TQ</span>";
            else if (status == 2)
                return "<span class=\"bg-orange\">Đã về kho TQ</span>";
            else if (status == 3)
                return "<span class=\"bg-green\">Đã về kho VN</span>";
            else
                return "<span class=\"bg-blue\">Đã giao cho khách</span>";
        }
        public static string IntToStringStatusSmallPackageWithBG(int status)
        {
            if (status == 0)
                return "<span class=\"bg-black\">Đã hủy</span>";
            else if (status == 1)
                return "<span class=\"bg-red\">Mới đặt - chưa về kho TQ</span>";
            else if (status == 2)
                return "<span class=\"bg-yellow\">Đã về kho TQ</span>";
            else if (status == 3)
                return "<span class=\"bg-green\">Đã về kho VN</span>";
            else
                return "<span class=\"bg-blue\">Đã giao cho khách</span>";
        }
        public static string ShowPhoneByUID(int ID)
        {
            var ui = AccountInfoController.GetByUserID(ID);
            if (ui != null)
            {
                return ui.MobilePhonePrefix + ui.MobilePhone;
            }
            else
                return "";
        }
        public static string RemoveHTMLTags(string content)
        {
            var cleaned = string.Empty;
            try
            {
                StringBuilder textOnly = new StringBuilder();
                using (var reader = XmlNodeReader.Create(new System.IO.StringReader("<xml>" + content + "</xml>")))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Text)
                            textOnly.Append(reader.ReadContentAsString());
                    }
                }
                cleaned = textOnly.ToString();
            }
            catch
            {
                //A tag is probably not closed. fallback to regex string clean.
                string textOnly = string.Empty;
                Regex tagRemove = new Regex(@"<[^>]*(>|$)");
                Regex compressSpaces = new Regex(@"[\s\r\n]+");
                textOnly = tagRemove.Replace(content, string.Empty);
                textOnly = compressSpaces.Replace(textOnly, " ");
                cleaned = textOnly;
            }

            return cleaned;
        }
        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
            "đ",
            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
            "í","ì","ỉ","ĩ","ị",
            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
            "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
            "d",
            "e","e","e","e","e","e","e","e","e","e","e",
            "i","i","i","i","i",
            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
            "u","u","u","u","u","u","u","u","u","u","u",
            "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }
        public static bool CheckUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
            "đ",
            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
            "í","ì","ỉ","ĩ","ị",
            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
            "ý","ỳ","ỷ","ỹ","ỵ",};
            bool check = false;
            for (int i = 0; i < arr1.Length; i++)
            {
                if (text.Contains(arr1[i]))
                    check = true;
            }
            return check;
        }

        public static string TranslateText(string input, string languagePair)
        {
            string url = String.Format("https://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", input, languagePair);
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36";
            request.Method = "GET";
            var content = String.Empty;
            HttpStatusCode statusCode;
            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                var contentType = response.ContentType;
                Encoding encoding = null;
                if (contentType != null)
                {
                    var match = Regex.Match(contentType, @"(?<=charset\=).*");
                    if (match.Success)
                        encoding = Encoding.GetEncoding(match.ToString());
                }
                encoding = encoding ?? Encoding.UTF8;
                statusCode = ((HttpWebResponse)response).StatusCode;
                using (var reader = new StreamReader(stream, encoding))
                    content = reader.ReadToEnd();
            }

            var doc = Dcsoup.Parse(content);
            var scoreDiv = doc.Select("html").Select("span[class=tlid-translation translation]").Html;
            return scoreDiv.ToString();
        }
        public static string TranslateTextNew(string input, string sLang, string tLang)
        {
            //string url = String.Format("https://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", input, languagePair);
            string url = String.Format("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}", sLang, tLang, input);
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36";
            request.Method = "GET";
            var content = String.Empty;
            HttpStatusCode statusCode;
            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                var contentType = response.ContentType;
                Encoding encoding = null;
                if (contentType != null)
                {
                    var match = Regex.Match(contentType, @"(?<=charset\=).*");
                    if (match.Success)
                        encoding = Encoding.GetEncoding(match.ToString());
                }
                encoding = encoding ?? Encoding.UTF8;
                statusCode = ((HttpWebResponse)response).StatusCode;
                using (var reader = new StreamReader(stream, encoding))
                    content = reader.ReadToEnd();
            }
            return content;
            //var doc = Dcsoup.Parse(content);
            //var scoreDiv = doc.Select("html").Select("span[class=tlid-translation translation]").Html;
            //return scoreDiv.ToString();
        }
        public static string ReturnPlaceVCH(int Place)
        {
            if (Place == 1)
            {
                return "Hà Nội";
            }
            else
                return "Việt Trì";
        }
        public static string ReturnTypeFastSlow(int type)
        {
            if (type == 1)
            {
                return "<span class='red'>Đi bay</span>";
            }
            else
                return "<span class='blue'>Đi tàu</span>";
        }
        public static string ReturnStatusBigpackage(int status)
        {
            if (status == 1)
            {
                return "Quảng Châu";
            }
            else if (status == 2)
            {
                return "Hà Nội";
            }
            else
            {
                return "Việt Trì";
            }

        }
        public static string ReturnStatusPayment(int status)
        {
            if (status == 0)
            {
                return "<span class='bg-red'>Chưa thanh toán</span>";
            }
            else
            {
                return "<span class='bg-blue'>Đã thanh toán</span>";
            }

        }
        public static string ReturnStatusReceivePackage(int status)
        {
            if (status == 0)
            {
                return "<span class='bg-red'>Chưa nhận hàng</span>";
            }
            else
            {
                return "<span class='bg-blue'>Đã nhận hàng</span>";
            }

        }
        public static string GetTradeTypeCYN(int TradeType)
        {
            if (TradeType == 1)
            {
                return "Thanh toán tiền mua hộ";
            }
            else
            {
                return "Nhận lại tiền mua hộ";
            }
        }
        public static Bitmap CreateBarcode1(string data)
        {
            Bitmap barCode = new Bitmap(1, 1);
            Font threeOfNine = new Font("Free 3 of 9", 60, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Graphics graphics = Graphics.FromImage(barCode);
            SizeF dataSize = graphics.MeasureString(data, threeOfNine);
            barCode = new Bitmap(barCode, dataSize.ToSize());
            graphics = Graphics.FromImage(barCode);
            graphics.Clear(Color.White);
            graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
            graphics.DrawString(data, threeOfNine, new SolidBrush(Color.Black), 0, 0);
            graphics.Flush();
            threeOfNine.Dispose();
            graphics.Dispose();
            return barCode;
        }
        public static string ShowIMG(string i)
        {
            return "<img src=\"" + i + "\">";
        }
        public static string BoolToStatusShowNoti(string i)
        {
            return ConvertStringToBool(i) == false ? "<span class='show-stat-w'>Không</span>" : "<span class='show-stat-s'>Có</span>";

        }


        //New
        public static string IntToRequestAdminNew(int i)
        {
            if (i == 0)
                return "<span class=\"badge red darken-2 white-text border-radius-2\">Chờ đặt cọc</span>";
            else if (i == 1)
                return "<span class=\"badge black darken-2 white-text border-radius-2\">Hủy đơn hàng</span>";
            else if (i == 2)
                return "<span class=\"badge orange darken-2 white-text border-radius-2\">Khách đã đặt cọc</span>";
            else if (i == 3)
                return "<span class=\"badge green darken-2 white-text border-radius-2\">Chờ duyệt đơn</span>";
            else if (i == 4)
                return "<span class=\"badge green darken-2 white-text border-radius-2\">Shop phát hàng</span>";
            else if (i == 5)
                return "<span class=\"badge green darken-2 white-text border-radius-2\">Đã mua hàng</span>";
            else if (i == 6)
                return "<span class=\"badge green darken-2 white-text border-radius-2\">Đã về kho TQ</span>";
            else if (i == 7)
                return "<span class=\"badge orange darken-2 white-text border-radius-2\">Đã về kho VN</span>";
            else if (i == 8)
                return "<span class=\"badge yellow darken-2 white-text border-radius-2\">Chờ thanh toán</span>";
            else if (i == 9)
                return "<span class=\"badge blue darken-2 white-text border-radius-2\">Khách đã thanh toán</span>";
            else if (i == 10)
                return "<span class=\"badge blue darken-2 white-text border-radius-2\">Đã hoàn thành</span>";
            else
                return "";

        }

        public static string ReturnStatusPayHelpNew(int status)
        {
            if (status == 2)
            {
                return "<span class='badge black darken-2 white-text border-radius-2'>Đã hủy</span>";
            }
            else if (status == 0)
            {
                return "<span class='badge red darken-2 white-text border-radius-2'>Chưa thanh toán</span>";
            }
            else if (status == 1)
            {
                return "<span class='badge blue darken-2 white-text border-radius-2'>Đã thanh toán</span>";
            }
            else if (status == 3)
            {
                return "<span class='badge green darken-2 white-text border-radius-2'>Đã hoàn thành</span>";
            }
            else
            {
                return "<span class='badge yellow darken-2 white-text border-radius-2'>Đã xác nhận</span>";
            }
        }

        public static string generateTransportationStatusNew(int i)
        {
            if (i == 0)
                return "<span class=\"badge black darken-2 white-text border-radius-2\">Đã hủy</span>";
            else if (i == 1)
                return "<span class=\"badge red darken-2 white-text border-radius-2\">Chờ duyệt</span>";
            else if (i == 2)
                return "<span class=\"badge red darken-2 white-text border-radius-2\">Đã duyệt</span>";
            else if (i == 3)
                return "<span class=\"badge green darken-2 white-text border-radius-2\">Đang xử lý</span>";
            else if (i == 4)
                return "<span class=\"badge green darken-2 white-text border-radius-2\">Đang vận chuyển về kho đích</span>";
            else if (i == 5)
                return "<span class=\"badge green darken-2 white-text border-radius-2\">Đã về kho VN</span>";
            else if (i == 6)
                return "<span class=\"badge blue darken-2 white-text border-radius-2\">Đã thanh toán</span>";
            else
                return "<span class=\"badge blue darken-2 white-text border-radius-2\">Đã hoàn thành</span>";
        }

        public static string IntToStringStatusSmallPackageNew(int status)
        {
            if (status == 0)
                return "<span class=\"badge black darken-2 white-text border-radius-2\">Đã hủy</span>";
            else if (status == 1)
                return "<span class=\"badge red darken-2 white-text border-radius-2\">Mới đặt - chưa về kho TQ</span>";
            else if (status == 2)
                return "<span class=\"badge orange darken-2 white-text border-radius-2\">Đã về kho TQ</span>";
            else if (status == 3)
                return "<span class=\"badge green darken-2 white-text border-radius-2\">Đã về kho VN</span>";
            else if (status == 4)
                return "<span class=\"badge blue darken-2 white-text border-radius-2\">Đã thanh toán</span>";
            else
                return "<span class=\"badge blue darken-2 white-text border-radius-2\">Đã hoàn thành</span>";
        }

        public static string IntToStringStatusSmallPackageTextNew(int status)
        {
            if (status == 0)
                return "<span class=\"black-text\">Đã hủy</span>";
            else if (status == 1)
                return "<span class=\"red-text\">Mới đặt - chưa về kho TQ</span>";
            else if (status == 2)
                return "<span class=\"orange-text\">Đã về kho TQ</span>";
            else if (status == 3)
                return "<span class=\"blue-text\">Đã về kho VN</span>";
            else
                return "<span class=\"blue-text\">Đã giao cho khách</span>";
        }

        public static string ReturnStatusComplainRequestNew(int status)
        {
            if (status == 1)
            {
                return "<span class='badge red darken-2 white-text border-radius-2'>Chưa duyệt</span>";
            }
            else if (status == 2)
            {
                return "<span class='badge yellow darken-2 white-text border-radius-2'>Đang xử lý</span>";
            }
            else if (status == 3)
            {
                return "<span class='badge blue darken-2 white-text border-radius-2'>Đã xử lý</span>";
            }
            else
            {
                return "<span class='badge orange darken-2 white-text border-radius-2'>Đã hủy</span>";
            }
        }

        public static string ReturnStatusWithdrawNew(int status)
        {
            if (status == 1)
            {
                return "<span class='badge red darken-2 white-text border-radius-2'>Đang chờ duyệt</span>";
            }
            else if (status == 2)
            {
                return "<span class='badge blue darken-2 white-text border-radius-2'>Đã duyệt</span>";
            }
            else
            {
                return "<span class='badge black darken-2 white-text border-radius-2'>Hủy lệnh</span>";
            }
        }

        public static string IntToStringStatusSmallPackageWithBGNew(int status)
        {
            if (status == 0)
                return "<span class=\"badge black darken-2 white-text border-radius-2\">Đã hủy</span>";
            else if (status == 1)
                return "<span class=\"badge red darken-2 white-text border-radius-2\">Mới đặt - chưa về kho TQ</span>";
            else if (status == 2)
                return "<span class=\"badge yellow darken-2 white-text border-radius-2\">Hàng về kho TQ</span>";
            else if (status == 3)
                return "<span class=\"badge green darken-2 white-text border-radius-2\">Hàng về kho VN</span>";
            else if (status == 5)
                return "<span class=\"badge teal darken-2 white-text border-radius-2\">Shop phát hàng</span>";
            else
                return "<span class=\"badge blue darken-2 white-text border-radius-2\">Đã giao cho khách</span>";
        }

        public static string generateTransportationStatusNew2(int i)
        {
            if (i == 0)
                return "<span class=\"badge black darken-2 white-text left no-margin\">Đã hủy</span>";
            else if (i == 1)
                return "<span class=\"badge red darken-2 white-text left no-margin\">Chờ duyệt</span>";
            else if (i == 2)
                return "<span class=\"badge bronze darken-2 white-text left no-margin\">Đã duyệt</span>";
            else if (i == 3)
                return "<span class=\"badge green darken-2 white-textleft no-margin\">Đang xử lý</span>";
            else if (i == 4)
                return "<span class=\"badge green darken-2 white-text left no-margin\">Đang vận chuyển về kho đích</span>";
            else if (i == 5)
                return "<span class=\"badge green darken-2 white-text left no-margin\">Đã về kho VN</span>";
            else if (i == 6)
                return "<span class=\"badge green darken-2 white-text left no-margin\">Đã thanh toán</span>";
            else
                return "<span class=\"badge green darken-2 white-text left no-margin\">Đã hoàn thành</span>";
        }


        public static string AddWalletStatus(int status)
        {
            if (status == 1)
            {
                return "<span class='badge red darken-2 white-text border-radius-2'>Đang chờ duyệt</span>";
            }
            else if (status == 2)
            {
                return "<span class='badge blue darken-2 white-text border-radius-2'>Đã duyệt</span>";
            }
            else
            {
                return "<span class='badge black darken-2 white-text border-radius-2'>Hủy lệnh</span>";
            }
        }

        public static string IntToRequestClientNew(int i)
        {
            if (i == 0)
                return "<span class=\"badge red darken-2 left m-0\">Chờ đặt cọc</span>";
            else if (i == 1)
                return "<span class=\"badge black darken-2 left m-0\">Hủy đơn hàng</span>";
            else if (i == 2)
                return "<span class=\"badge bronze darken-2 left m-0\">Đã đặt cọc</span>";
            else if (i == 3)
                return "<span class=\"badge green darken-2 left m-0\">Chờ duyệt đơn</span>";
            else if (i == 4)
                return "<span class=\"badge green darken-2 left m-0\">Shop phát hàng</span>";
            else if (i == 5)
                return "<span class=\"badge green darken-2 left m-0\">Đã mua hàng</span>";
            else if (i == 6)
                return "<span class=\"badge green darken-2 left m-0\">Đang về Việt Nam</span>";
            else if (i == 7)
                return "<span class=\"badge green darken-2 left m-0\">Đã nhận hàng tại kho đích</span>";
            else if (i == 8)
                return "<span class=\"badge yellow darken-2 left m-0\">Chờ thanh toán</span>";
            else if (i == 9)
                return "<span class=\"badge blue darken-2 left m-0\">Khách đã thanh toán</span>";
            else if (i == 10)
                return "<span class=\"badge blue darken-2 left m-0\">Đã hoàn thành</span>";
            else
                return "";
        }

        public static string ShowStatusPayHistoryUserNew(int status)
        {
            if (status == 2)
                return "<span class=\"badge bronze darken-2 white-text border-radius-2\">Đặt cọc</span>";
            else if (status == 3)
                return "<span class=\"badge yellow darken-2 white-text border-radius-2\">Đặt cọc</span>";
            else if (status == 12)
                return "<span class=\"badge red darken-2 white-text border-radius-2\">Sản phẩm hết hàng hoặc giảm giá trả lại cọc</span>";
            else
                return "<span class=\"badge blue darken-2 white-text border-radius-2\">Thanh toán</span>";
        }

        public static string StatusToRequestNew(object i)
        {
            if (i != null)
            {
                if (i.ToString() == "1")
                {
                    return "<span class='badge yellow darken-2 white-text border-radius-2'>Chưa kích hoạt</span>";
                }
                else if (i.ToString() == "2")
                {
                    return "<span class='badge green darken-2 white-text border-radius-2'>Đã kích hoạt</span>";
                }
                else
                {
                    return "<span class='badge red darken-2 white-text border-radius-2'>Đang bị khóa</span>";
                }

            }
            else return "<span class='badge red darken-2 white-text border-radius-2'>Đang bị khóa</span>";
        }

        public static string GeneralTransportationOrderNewStatus(int status)
        {
            if (status == 0)
                return "<span class=\"badge black darken-2 white-text border-radius-2\">Đã hủy</span>";
            else if (status == 1)
                return "<span class=\"badge red darken-2 white-text border-radius-2\">Đơn hàng mới</span>";
            else if (status == 2)
                return "<span class=\"badge yellow darken-2 white-text border-radius-2\">Đã duyệt</span>";
            else if (status == 3)
                return "<span class=\"badge orange darken-2 white-text border-radius-2\">Đã về kho TQ</span>";
            else if (status == 4)
                return "<span class=\"badge green darken-2 white-text border-radius-2\">Đã về kho VN</span>";
            else if (status == 5)
                return "<span class=\"badge blue darken-2 white-text border-radius-2\">Đã yêu cầu</span>";
            else
                return "<span class=\"badge blue darken-2 white-text border-radius-2\">Đã giao khách hàng</span>";
        }


        public static string GeneralTransportationOrderNewStatusApp(int status)
        {
            if (status == 0)
                return "<span class=\"bg-black\">Đã hủy</span>";
            else if (status == 1)
                return "<span class=\"bg-red\">Đơn hàng mới</span>";
            else if (status == 2)
                return "<span class=\"bg-orange\">Đã duyệt</span>";
            else if (status == 3)
                return "<span class=\"bg-green\">Đã về kho TQ</span>";
            else if (status == 4)
                return "<span class=\"bg-green\">Đã về kho VN</span>";
            else if (status == 5)
                return "<span class=\"bg-blue\">Đã yêu cầu</span>";
            else
                return "<span class=\"bg-blue\">Đã giao cho khách</span>";
        }


        public static string ShowIMG40x40(string i)
        {
            return "<img style=\"height:40px;width:40px\" src=\"" + i + "\">";
        }

        public static string ReturnStatusTT(int status)
        {
            if (status == 1)
                return "<span class=\"badge red darken-2 white-text border-radius-2\">Chưa thanh toán</span>";
            else if (status == 2)
                return "<span class=\"badge blue darken-2 white-text border-radius-2\">Đã thanh toán</span>";
            else
                return "<span class=\"badge blue darken-2 white-text border-radius-2\">Đã thanh toán</span>";
        }

        public static string ReturnStatusXK(int status)
        {
            if (status == 1)
                return "<span class=\"badge red darken-2 white-text border-radius-2\">Chưa xuất kho</span>";
            else if (status == 2)
                return "<span class=\"badge blue darken-2 white-text border-radius-2\">Đã xuất kho</span>";
            else
                return "<span class=\"badge blue darken-2 white-text border-radius-2\">Đã xuất kho</span>";
        }


        //barcode 128

        private const int CQuietWidth = 10;

        private static readonly int[,] CPatterns =
                                                   {
                                                     { 2, 1, 2, 2, 2, 2, 0, 0 }, // 0
                                                     { 2, 2, 2, 1, 2, 2, 0, 0 }, // 1
                                                     { 2, 2, 2, 2, 2, 1, 0, 0 }, // 2
                                                     { 1, 2, 1, 2, 2, 3, 0, 0 }, // 3
                                                     { 1, 2, 1, 3, 2, 2, 0, 0 }, // 4
                                                     { 1, 3, 1, 2, 2, 2, 0, 0 }, // 5
                                                     { 1, 2, 2, 2, 1, 3, 0, 0 }, // 6
                                                     { 1, 2, 2, 3, 1, 2, 0, 0 }, // 7
                                                     { 1, 3, 2, 2, 1, 2, 0, 0 }, // 8
                                                     { 2, 2, 1, 2, 1, 3, 0, 0 }, // 9
                                                     { 2, 2, 1, 3, 1, 2, 0, 0 }, // 10
                                                     { 2, 3, 1, 2, 1, 2, 0, 0 }, // 11
                                                     { 1, 1, 2, 2, 3, 2, 0, 0 }, // 12
                                                     { 1, 2, 2, 1, 3, 2, 0, 0 }, // 13
                                                     { 1, 2, 2, 2, 3, 1, 0, 0 }, // 14
                                                     { 1, 1, 3, 2, 2, 2, 0, 0 }, // 15
                                                     { 1, 2, 3, 1, 2, 2, 0, 0 }, // 16
                                                     { 1, 2, 3, 2, 2, 1, 0, 0 }, // 17
                                                     { 2, 2, 3, 2, 1, 1, 0, 0 }, // 18
                                                     { 2, 2, 1, 1, 3, 2, 0, 0 }, // 19
                                                     { 2, 2, 1, 2, 3, 1, 0, 0 }, // 20
                                                     { 2, 1, 3, 2, 1, 2, 0, 0 }, // 21
                                                     { 2, 2, 3, 1, 1, 2, 0, 0 }, // 22
                                                     { 3, 1, 2, 1, 3, 1, 0, 0 }, // 23
                                                     { 3, 1, 1, 2, 2, 2, 0, 0 }, // 24
                                                     { 3, 2, 1, 1, 2, 2, 0, 0 }, // 25
                                                     { 3, 2, 1, 2, 2, 1, 0, 0 }, // 26
                                                     { 3, 1, 2, 2, 1, 2, 0, 0 }, // 27
                                                     { 3, 2, 2, 1, 1, 2, 0, 0 }, // 28
                                                     { 3, 2, 2, 2, 1, 1, 0, 0 }, // 29
                                                     { 2, 1, 2, 1, 2, 3, 0, 0 }, // 30
                                                     { 2, 1, 2, 3, 2, 1, 0, 0 }, // 31
                                                     { 2, 3, 2, 1, 2, 1, 0, 0 }, // 32
                                                     { 1, 1, 1, 3, 2, 3, 0, 0 }, // 33
                                                     { 1, 3, 1, 1, 2, 3, 0, 0 }, // 34
                                                     { 1, 3, 1, 3, 2, 1, 0, 0 }, // 35
                                                     { 1, 1, 2, 3, 1, 3, 0, 0 }, // 36
                                                     { 1, 3, 2, 1, 1, 3, 0, 0 }, // 37
                                                     { 1, 3, 2, 3, 1, 1, 0, 0 }, // 38
                                                     { 2, 1, 1, 3, 1, 3, 0, 0 }, // 39
                                                     { 2, 3, 1, 1, 1, 3, 0, 0 }, // 40
                                                     { 2, 3, 1, 3, 1, 1, 0, 0 }, // 41
                                                     { 1, 1, 2, 1, 3, 3, 0, 0 }, // 42
                                                     { 1, 1, 2, 3, 3, 1, 0, 0 }, // 43
                                                     { 1, 3, 2, 1, 3, 1, 0, 0 }, // 44
                                                     { 1, 1, 3, 1, 2, 3, 0, 0 }, // 45
                                                     { 1, 1, 3, 3, 2, 1, 0, 0 }, // 46
                                                     { 1, 3, 3, 1, 2, 1, 0, 0 }, // 47
                                                     { 3, 1, 3, 1, 2, 1, 0, 0 }, // 48
                                                     { 2, 1, 1, 3, 3, 1, 0, 0 }, // 49
                                                     { 2, 3, 1, 1, 3, 1, 0, 0 }, // 50
                                                     { 2, 1, 3, 1, 1, 3, 0, 0 }, // 51
                                                     { 2, 1, 3, 3, 1, 1, 0, 0 }, // 52
                                                     { 2, 1, 3, 1, 3, 1, 0, 0 }, // 53
                                                     { 3, 1, 1, 1, 2, 3, 0, 0 }, // 54
                                                     { 3, 1, 1, 3, 2, 1, 0, 0 }, // 55
                                                     { 3, 3, 1, 1, 2, 1, 0, 0 }, // 56
                                                     { 3, 1, 2, 1, 1, 3, 0, 0 }, // 57
                                                     { 3, 1, 2, 3, 1, 1, 0, 0 }, // 58
                                                     { 3, 3, 2, 1, 1, 1, 0, 0 }, // 59
                                                     { 3, 1, 4, 1, 1, 1, 0, 0 }, // 60
                                                     { 2, 2, 1, 4, 1, 1, 0, 0 }, // 61
                                                     { 4, 3, 1, 1, 1, 1, 0, 0 }, // 62
                                                     { 1, 1, 1, 2, 2, 4, 0, 0 }, // 63
                                                     { 1, 1, 1, 4, 2, 2, 0, 0 }, // 64
                                                     { 1, 2, 1, 1, 2, 4, 0, 0 }, // 65
                                                     { 1, 2, 1, 4, 2, 1, 0, 0 }, // 66
                                                     { 1, 4, 1, 1, 2, 2, 0, 0 }, // 67
                                                     { 1, 4, 1, 2, 2, 1, 0, 0 }, // 68
                                                     { 1, 1, 2, 2, 1, 4, 0, 0 }, // 69
                                                     { 1, 1, 2, 4, 1, 2, 0, 0 }, // 70
                                                     { 1, 2, 2, 1, 1, 4, 0, 0 }, // 71
                                                     { 1, 2, 2, 4, 1, 1, 0, 0 }, // 72
                                                     { 1, 4, 2, 1, 1, 2, 0, 0 }, // 73
                                                     { 1, 4, 2, 2, 1, 1, 0, 0 }, // 74
                                                     { 2, 4, 1, 2, 1, 1, 0, 0 }, // 75
                                                     { 2, 2, 1, 1, 1, 4, 0, 0 }, // 76
                                                     { 4, 1, 3, 1, 1, 1, 0, 0 }, // 77
                                                     { 2, 4, 1, 1, 1, 2, 0, 0 }, // 78
                                                     { 1, 3, 4, 1, 1, 1, 0, 0 }, // 79
                                                     { 1, 1, 1, 2, 4, 2, 0, 0 }, // 80
                                                     { 1, 2, 1, 1, 4, 2, 0, 0 }, // 81
                                                     { 1, 2, 1, 2, 4, 1, 0, 0 }, // 82
                                                     { 1, 1, 4, 2, 1, 2, 0, 0 }, // 83
                                                     { 1, 2, 4, 1, 1, 2, 0, 0 }, // 84
                                                     { 1, 2, 4, 2, 1, 1, 0, 0 }, // 85
                                                     { 4, 1, 1, 2, 1, 2, 0, 0 }, // 86
                                                     { 4, 2, 1, 1, 1, 2, 0, 0 }, // 87
                                                     { 4, 2, 1, 2, 1, 1, 0, 0 }, // 88
                                                     { 2, 1, 2, 1, 4, 1, 0, 0 }, // 89
                                                     { 2, 1, 4, 1, 2, 1, 0, 0 }, // 90
                                                     { 4, 1, 2, 1, 2, 1, 0, 0 }, // 91
                                                     { 1, 1, 1, 1, 4, 3, 0, 0 }, // 92
                                                     { 1, 1, 1, 3, 4, 1, 0, 0 }, // 93
                                                     { 1, 3, 1, 1, 4, 1, 0, 0 }, // 94
                                                     { 1, 1, 4, 1, 1, 3, 0, 0 }, // 95
                                                     { 1, 1, 4, 3, 1, 1, 0, 0 }, // 96
                                                     { 4, 1, 1, 1, 1, 3, 0, 0 }, // 97
                                                     { 4, 1, 1, 3, 1, 1, 0, 0 }, // 98
                                                     { 1, 1, 3, 1, 4, 1, 0, 0 }, // 99
                                                     { 1, 1, 4, 1, 3, 1, 0, 0 }, // 100
                                                     { 3, 1, 1, 1, 4, 1, 0, 0 }, // 101
                                                     { 4, 1, 1, 1, 3, 1, 0, 0 }, // 102
                                                     { 2, 1, 1, 4, 1, 2, 0, 0 }, // 103
                                                     { 2, 1, 1, 2, 1, 4, 0, 0 }, // 104
                                                     { 2, 1, 1, 2, 3, 2, 0, 0 }, // 105
                                                     { 2, 3, 3, 1, 1, 1, 2, 0 } // 106
                                                   };

        public static System.Drawing.Image MakeBarcodeImage(string inputData, int barWeight, bool addQuietZone)
        {
            // get the Code128 codes to represent the message
            var content = new Code128Content(inputData);
            var codes = content.Codes;

            var width = (((codes.Length - 3) * 11) + 35) * barWeight;
            var height = Convert.ToInt32(Math.Ceiling(Convert.ToSingle(width) * .15F));

            if (addQuietZone)
            {
                width += 2 * CQuietWidth * barWeight; // on both sides
            }

            // get surface to draw on
            System.Drawing.Image myImage = new Bitmap(width, height);
            using (var gr = Graphics.FromImage(myImage))
            {
                // set to white so we don't have to fill the spaces with white
                gr.FillRectangle(Brushes.White, 0, 0, width, height);

                // skip quiet zone
                var cursor = addQuietZone ? CQuietWidth * barWeight : 0;

                for (var codeIdx = 0; codeIdx < codes.Length; codeIdx++)
                {
                    var code = codes[codeIdx];

                    // take the bars two at a time: a black and a white
                    for (var bar = 0; bar < 8; bar += 2)
                    {
                        var barWidth = CPatterns[code, bar] * barWeight;
                        var spcWidth = CPatterns[code, bar + 1] * barWeight;

                        // if width is zero, don't try to draw it
                        if (barWidth > 0)
                        {
                            gr.FillRectangle(Brushes.Black, cursor, 0, barWidth, height);
                        }

                        // note that we never need to draw the space, since we 
                        // initialized the graphics to all white

                        // advance cursor beyond this pair
                        cursor += barWidth + spcWidth;
                    }
                }
            }

            return myImage;
        }


        public static string IntToStringStatusSmallPackage45(int status)
        {
            if (status == 0)
                return "<span class=\"white-text badge black darken-2\">Đã hủy</span>";
            else if (status == 1)
                return "<span class=\"white-text badge red darken-2\">Mới đặt - chưa về kho TQ</span>";
            else if (status == 2)
                return "<span class=\"white-text badge teal darken-2\">Đã về kho TQ</span>";
            else if (status == 3)
                return "<span class=\"white-text badge teal darken-2\">Đã về kho đích</span>";
            else
                return "<span class=\"white-text badge blue darken-2\">Đã giao cho khách</span>";
        }

        public static string IntToStringStatusSmallPackageWithBG45(int status)
        {
            if (status == 0)
                return "<span class=\"badge border-radius-2 darken-2 black white-text\">Đã hủy</span>";
            else if (status == 1)
                return "<span class=\"badge border-radius-2 darken-2 red white-text\">Mới đặt - chưa về kho TQ</span>";
            else if (status == 2)
                return "<span class=\"badge border-radius-2 darken-2 yellow white-text\">Đã về kho TQ</span>";
            else if (status == 3)
                return "<span class=\"badge border-radius-2 darken-2 green white-text\">Đã về kho VN</span>";
            else if (status == 5)
                return "<span class=\"badge border-radius-2 darken-2 orange white-text\">Shop phát hàng</span>";
            else
                return "<span class=\"badge border-radius-2 darken-2 blue white-text\">Đã giao cho khách</span>";
        }

        public static string ReturnStatusAddNewWallet(int status)
        {
            if (status == 1)
            {
                return "<span class='badge orange darken-2 white-text border-radius-2'>Chờ duyệt</span>";
            }
            else if (status == 2)
            {
                return "<span class='badge green darken-2 white-text border-radius-2'>Đã duyệt</span>";
            }
            else
            {
                return "<span class='badge red darken-2 white-text border-radius-2'>Đã hủy</span>";
            }
        }

        public static string ShowStatusPayHistoryNew(int status)
        {
            if (status == 2)
                return "<span class=\"badge orange darken-2 white-text border-radius-2\">Đặt cọc</span>";
            else if (status == 3)
                return "<span class=\"badge orange darken-2 white-text border-radius-2\">Đặt cọc</span>";
            else if (status == 12)
                return "<span class=\"badge red darken-2 white-text border-radius-2\">Sản phẩm hết hàng hoặc giảm giá trả lại cọc</span>";
            else
                return "<span class=\"badge blue darken-2 white-text border-radius-2\">Thanh toán</span>";
        }

        public static string ReturnBank(int ID)
        {
            var bk = BankController.GetByID(ID);
            if (bk != null)
            {
                return bk.BankName + " CN " + bk.Branch + " - " + bk.AccountHolder + " - " + bk.BankNumber;
            }
            else if (ID == 100)
            {
                return "Trực tiếp tại văn phòng";
            }            
            else
            {
                return "Trực tiếp tại văn phòng";
            }
        }

        public static string vapidPublicKey = "BLV67mH2vJ089lrdChQhSzSwJgWXvpKBdwgZ-AzuDpmlKGlZPtCbH_AD28gDnd7u42srBlEQLmbRYf46thgGIzI";
        public static string vapidPrivateKey = "RTQLMmzY3Ey72ELoJpW-_gDJbC-v_sG7d8r9JKalM0c";
        public static void PushNotiDesktop(int UID, string title, string link)// đẩy thông báo xuống desktop trình duyệt
        {
            var ltoken = DeviceBrowserTable.getbyuid(UID);
            if (ltoken != null)
            {
                jsonNoti n = new jsonNoti();
                n.title = "ptite1688.com";
                n.message = title;
                n.icon = "https://ptite1688.com/Uploads/NewsIMG/2-10-2020-21420-PM.png";
                n.link = link;
                var notiPush = new JavaScriptSerializer().Serialize(n);
                foreach (var item in ltoken)
                {
                    try
                    {
                        var pushSubscription = new PushSubscription(item.PushEndpoint, item.PushP256DH, item.PushAuth);
                        var vapidDetails = new VapidDetails("mailto:Monamedia@gmail.com", vapidPublicKey, vapidPrivateKey);
                        var webPushClient = new WebPushClient();
                        webPushClient.SendNotification(pushSubscription, notiPush, vapidDetails);
                    }
                    catch
                    {
                        DeviceBrowserTable.updatehide(item.ID, "auto");
                    }
                }
            }
        }

        public class jsonNoti
        {
            public string title { get; set; }
            public string message { get; set; }
            public string icon { get; set; }
            public string link { get; set; }
        }

        public static string ConnectApi(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    var encoding = ASCIIEncoding.ASCII;

                    using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
                    {
                        string responseText = reader.ReadToEnd();
                        return responseText;
                    }
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace NHST.manager
{
    public partial class import_smallpackage : System.Web.UI.Page
    {
        string currFilePath = string.Empty;
        string currFileExtension = string.Empty;  //File Extension
        protected DataTable _FileTempPlan
        {
            get { return (DataTable)this.Session["FileTempDb992"]; }
            set
            {
                this.Session["FileTempDb992"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                else
                {
                    _FileTempPlan = null;
                }
                LoadDDL();
            }
        }



        public void LoadDDL()
        {
            var bs = BigPackageController.GetAllWithStatus(1);
            ddlBigpack.Items.Clear();
            ddlBigpack.Items.Insert(0, new ListItem("Chọn bao lớn", "0"));
            if (bs.Count > 0)
            {
                foreach (var b in bs)
                {
                    ListItem listitem = new ListItem(b.PackageCode, b.ID.ToString());
                    ddlBigpack.Items.Add(listitem);
                }
            }
            ddlBigpack.DataBind();
        }


        private void UploadToTemp()
        {

            HttpPostedFile file = this.FileUpload1.PostedFile;
            string fileName = file.FileName;
            string tempPath = System.IO.Path.GetTempPath();
            fileName = Path.GetFileName(fileName);
            this.currFileExtension = Path.GetExtension(fileName);
            this.currFilePath = tempPath + fileName;
            file.SaveAs(this.currFilePath);
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    string username_current = Session["userLoginSystem"].ToString();
                    DateTime currentDate = DateTime.Now;
                    var obj_user = AccountController.GetByUsername(username_current);

                    UploadToTemp();
                    if (this.currFileExtension == ".xlsx" || this.currFileExtension == ".xls")
                    {
                        _FileTempPlan = PJUtils.ReadDataExcel(currFilePath, Path.GetExtension(FileUpload1.PostedFile.FileName), "Yes");// WebUtil.ReadDataFromExcel(currFilePath);
                        File.Delete((Path.GetTempPath() + FileUpload1.FileName));

                        var rs = string.Empty;
                        var t = 0;
                        if (_FileTempPlan != null)
                        {
                            double currency = 0;
                            int id = hdfID.Value.ToInt(0);
                            var confi = ConfigurationController.GetByTop1();
                            if (confi != null)
                            {
                                currency = Convert.ToDouble(confi.Currency);
                            }
                            string checkMVD = "";
                            bool checktb = true;
                            int MainOrderID = 0;
                            int baolon = Convert.ToInt32(ddlBigpack.SelectedValue);
                            foreach (DataRow drRow in _FileTempPlan.Rows)
                            {

                                try
                                {

                                    checkMVD = drRow["MaVanDon"].ToString();

                                    DateTime dt = DateTime.Parse(drRow["NgayVeKhoTQ"].ToString());


                                    var check = SmallPackageController.GetByOrderTransactionCode(drRow["MaVanDon"].ToString());
                                    if (check != null)
                                    {
                                        var a = TransportationOrderNewController.GetByBarCode_Thang(drRow["MaVanDon"].ToString());
                                        var package = SmallPackageController.GetByID(Convert.ToInt32(check.ID));
                                        if (package != null)
                                        {
                                            var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
                                            if (mainorder != null)
                                            {
                                                string orderstatus = "";
                                                int currentOrderStatus = Convert.ToInt32(mainorder.Status);
                                                switch (currentOrderStatus)
                                                {
                                                    case 0:
                                                        orderstatus = "Chờ đặt cọc";
                                                        break;
                                                    case 1:
                                                        orderstatus = "Hủy đơn hàng";
                                                        break;
                                                    case 2:
                                                        orderstatus = "Đã đặt cọc";
                                                        break;
                                                    case 5:
                                                        orderstatus = "Đã mua hàng";
                                                        break;
                                                    case 6:
                                                        orderstatus = "Đang về kho đích";
                                                        break;
                                                    case 7:
                                                        orderstatus = "Đã nhận hàng tại kho đích";
                                                        break;
                                                    case 8:
                                                        orderstatus = "Chờ thanh toán";
                                                        break;
                                                    case 9:
                                                        orderstatus = "Khách đã thanh toán";
                                                        break;
                                                    case 10:
                                                        orderstatus = "Đã hoàn thành";
                                                        break;
                                                    default:
                                                        break;
                                                }
                                                MainOrderController.UpdateMVD_Thang(Convert.ToInt32(mainorder.ID), 6, dt, obj_user.ID);
                                                SmallPackageController.Update_Thang(check.ID,
                                                       baolon, drRow["MaVanDon"].ToString(), "", 0, 0, 0, 2, "", DateTime.Now, username_current, DateTime.Now);
                                                if (mainorder.Status != 6)
                                                {
                                                    HistoryOrderChangeController.Insert(mainorder.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                                    " đã import đổi trạng thái của đơn hàng ID là: " + mainorder.ID + ", từ: " + orderstatus + ", sang: Đang về kho đích", 0, currentDate);
                                                }
                                            }
                                        }

                                        if (a != null)
                                        {
                                            if (!string.IsNullOrEmpty(a.ToString()))
                                            {
                                                TransportationOrderNewController.Update_Thang(a.ID, 3, DateTime.Now, username_current);
                                                SmallPackageController.Update_Thang(check.ID,
                                                       baolon, drRow["MaVanDon"].ToString(), "", 0, 0, 0, 2, "", DateTime.Now, username_current, DateTime.Now);

                                            }
                                        }

                                    }
                                    else if (check == null)
                                    {
                                        string packageID = SmallPackageController.InsertWithMainOrderIDAndIsTemp_DateTQ(MainOrderID,
                                                baolon, drRow["MaVanDon"].ToString(), "", 0, 0, 0, 2, true, 0, DateTime.UtcNow.AddHours(7), username_current, DateTime.Now);
                                    }
                                    else
                                    {
                                        checkMVD += drRow["MaVanDon"].ToString() + " - ";
                                        checktb = false;
                                    }
                                }
                                catch (Exception a)
                                {
                                }
                                t++;
                            }
                            if (checktb)
                            {
                                PJUtils.ShowMessageBoxSwAlert("Import danh sách mã vận đơn thành công.", "s", true, Page);
                            }
                            else
                            {
                                PJUtils.ShowMessageBoxSwAlert("Mã vận đơn " + checkMVD + " trùng.", "e", true, Page);
                            }
                        }
                        else
                        {
                            PJUtils.ShowMessageBoxSwAlert("Không có dữ liệu.", "s", true, Page);
                        }
                    }
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình import.", "e", true, Page);
                }
            }
            catch (Exception ex)
            {

            }

        }

        //protected void btnExport_Click(object sender, EventArgs e)
        //{
        //    StringBuilder StrExport = new StringBuilder();
        //    StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
        //    StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
        //    StrExport.Append("<DIV  style='font-size:12px;'>");
        //    StrExport.Append("<table border=\"1\">");
        //    StrExport.Append("  <tr>");
        //    StrExport.Append("      <th><strong>MaVanDon</strong></th>");
        //    StrExport.Append("  </tr>");
        //    StrExport.Append("  <tr>");
        //    StrExport.Append("      <td>MVD12312312</td>");
        //    StrExport.Append("  </tr>");
        //    StrExport.Append("</table>");
        //    StrExport.Append("</div></body></html>");
        //    string strFile = "danh-sach-ma-van-don.xls";
        //    string strcontentType = "application/vnd.ms-excel";
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    Response.BufferOutput = true;
        //    Response.ContentType = strcontentType;
        //    Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
        //    Response.Write(StrExport.ToString());
        //    Response.Flush();
        //    //Response.Close();
        //    Response.End();
        //}

        protected void btnExport_Click(object sender, EventArgs e)
        {
            StringBuilder StrExport = new StringBuilder();
            StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
            StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
            StrExport.Append("<DIV  style='font-size:12px;'>");
            StrExport.Append("<table border=\"1\">");
            StrExport.Append("  <tr>");
            StrExport.Append("      <th><strong>MaVanDon</strong></th>");
            StrExport.Append("      <th><strong>NgayVeKhoTQ</strong></th>");
            StrExport.Append("  </tr>");
            StrExport.Append("  <tr>");
            StrExport.Append("      <td>MVD12312312</td>");
            StrExport.Append("      <td>11/05/2020</td>");
            StrExport.Append("  </tr>");
            StrExport.Append("</table>");
            StrExport.Append("</div></body></html>");
            string strFile = "danh-sach-ma-van-don.xls";
            string strcontentType = "application/vnd.ms-excel";
            Response.ClearContent();
            Response.ClearHeaders();
            Response.BufferOutput = true;
            Response.ContentType = strcontentType;
            Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
            Response.Write(StrExport.ToString());
            Response.Flush();
            //Response.Close();
            Response.End();
        }



        [WebMethod]
        public static string AddBigPackage(string PackageCode, string Weight, string Volume)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                var user = AccountController.GetByUsername(username);
                if (user != null)
                {
                    var check = BigPackageController.GetByPackageCode(PackageCode);
                    if (check != null)
                    {
                        return "existCode";
                    }
                    else
                    {
                        double volume = 0;
                        double weight = 0;
                        if (Convert.ToDouble(Volume) > 0)
                            volume = Convert.ToDouble(Volume);
                        if (Convert.ToDouble(Weight) > 0)
                            weight = Convert.ToDouble(Weight);

                        string kq = BigPackageController.Insert(PackageCode, weight, volume, 1,
                            DateTime.UtcNow.AddHours(7), username);

                        if (kq.ToInt(0) > 0)
                            return kq;
                        else
                            return null;
                    }
                }
                else
                    return null;
            }
            else
                return null;
        }

        [WebMethod]
        public static string GetBigPackage()
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                var bs = BigPackageController.GetAllWithStatus(1);
                if (bs.Count > 0)
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    return serializer.Serialize(bs);
                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}
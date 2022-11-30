using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using System.Data;
using System.IO;
using System.Text;

namespace NHST.manager
{
    public partial class import_smallpackage_vn : System.Web.UI.Page
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
            }
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
                    int UIDCreate = 0;
                    var obj_user = AccountController.GetByUsername(username_current);
                    if (obj_user != null)
                    {
                        UIDCreate = obj_user.ID;
                    }
                    UploadToTemp();
                    if (this.currFileExtension == ".xlsx" || this.currFileExtension == ".xls")
                    {
                        _FileTempPlan = PJUtils.ReadDataExcel(currFilePath, Path.GetExtension(FileUpload1.PostedFile.FileName), "Yes");// WebUtil.ReadDataFromExcel(currFilePath);
                        File.Delete((Path.GetTempPath() + FileUpload1.FileName));

                        var rs = string.Empty;
                        var t = 0;
                        if (_FileTempPlan != null)
                        {
                            double currencyMuaHo = 0;
                            var confimua = ConfigurationController.GetByTop1();
                            if (confimua != null)
                            {
                                currencyMuaHo = Convert.ToDouble(confimua.Currency);
                            }
                            double currencyKyGui = 0;
                            var confiky = ConfigurationController.GetByTop1();
                            if (confiky != null)
                            {
                                currencyKyGui = Convert.ToDouble(confiky.AgentCurrency);
                            }
                            foreach (DataRow drRow in _FileTempPlan.Rows)
                            {
                                try
                                {
                                    double weight = 0.5;
                                    if (drRow["CanNang"] != DBNull.Value)
                                    {
                                        weight = Convert.ToDouble(drRow["CanNang"]);
                                        if (weight < 0.5)
                                            weight = 0.5;
                                    }
                                    if (drRow["MaKhachHang"] != DBNull.Value)
                                    {
                                        if (Convert.ToInt32(drRow["LoaiHang"]) == 1 || Convert.ToInt32(drRow["LoaiHang"]) == 3)
                                        {
                                            var mo = MainOrderController.GetAllByID(Convert.ToInt32(drRow["MaDonHang"].ToString()));
                                            if (mo != null)
                                            {
                                                MainOrderController.UpdateWeightStatusVN(Convert.ToInt32(drRow["MaDonHang"].ToString()), Convert.ToString(weight), Convert.ToString(weight), 7, currentDate);                                                
                                                SmallPackageController.UpdateMainOrderImport(Convert.ToString(drRow["MaVanDon"]), weight, 3, currentDate, username_current);                                                
                                            }
                                        }
                                        else if (Convert.ToInt32(drRow["LoaiHang"]) == 5)
                                        {
                                            var trans = TransportationOrderNewController.GetByID(Convert.ToInt32(drRow["MaDonHang"].ToString()));
                                            if (trans != null)
                                            {
                                                TransportationOrderNewController.UpdateTransImport(Convert.ToInt32(drRow["MaDonHang"].ToString()), Convert.ToString(weight), 4, username_current, currentDate, currentDate);
                                                SmallPackageController.UpdateTransImport(Convert.ToInt32(drRow["MaDonHang"].ToString()), weight, 3, currentDate, username_current);
                                            }   
                                        }
                                    }
                                    else
                                    {
                                        var checkmvd = SmallPackageController.GetByOrderTransactionCode(drRow["MaVanDon"].ToString());
                                        if (checkmvd != null)
                                        {
                                            SmallPackageController.UpdateUndefineImport(drRow["MaVanDon"].ToString(), 0, 0, "", 0, weight, 0, 3, true, 0, currentDate, username_current);
                                        }
                                        else
                                        {
                                            SmallPackageController.InsertWithMainOrderIDAndIsTemp(0, 0, drRow["MaVanDon"].ToString(), "", 0, weight, 0, 3, true, 0, currentDate, username_current);
                                        }                                        
                                    }
                                }
                                catch (Exception a)
                                {
                                    //rs = rs + drRow["Title"].ToString() + " : lỗi :" + a.Message + "<br/>";
                                }
                                t++;
                            }
                            PJUtils.ShowMessageBoxSwAlert("Import danh sách mã vận đơn thành công.", "s", true, Page);
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

        protected void btnExport_Click(object sender, EventArgs e)
        {
            StringBuilder StrExport = new StringBuilder();
            StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
            StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
            StrExport.Append("<DIV  style='font-size:12px;'>");
            StrExport.Append("<table border=\"1\">");
            StrExport.Append("  <tr>");
            StrExport.Append("      <th><strong>MaKhachHang</strong></th>");
            StrExport.Append("      <th><strong>MaDonHang</strong></th>");
            StrExport.Append("      <th><strong>LoaiHang</strong></th>");
            StrExport.Append("      <th><strong>MaVanDon</strong></th>");
            StrExport.Append("      <th><strong>CanNang</strong></th>");
            StrExport.Append("      <th><strong>KhoTQ</strong></th>");
            StrExport.Append("      <th><strong>KhoVN</strong></th>");
            StrExport.Append("      <th><strong>PhuongThucVC</strong></th>");
            StrExport.Append("  </tr>");
            StrExport.Append("  <tr>");
            StrExport.Append("      <td>456789</td>");
            StrExport.Append("      <td>99</td>");
            StrExport.Append("      <td>1</td>");
            StrExport.Append("      <td>MVD-456789</td>");
            StrExport.Append("      <td>0.5</td>");
            StrExport.Append("      <td>HaKhau</td>");
            StrExport.Append("      <td>HaNoi</td>");
            StrExport.Append("      <td>Thuong</td>");
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
            Response.End();
        }
    }
}
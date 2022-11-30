using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZLADIPJ.Business;
using Telerik.Web.UI;
using MB.Extensions;
using System.Text;

namespace NHST.manager
{
    public partial class yeu_cau_giao : System.Web.UI.Page
    {
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
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    if (ac != null)
                        if (ac.RoleID == 1)
                            Response.Redirect("/trang-chu");
                    if (ac.RoleID == 0)
                        btnExcel.Visible = true;
                }
            }
        }
        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            if (ac != null)
            {
                //var la = MainOrderController.GetByUserInView(ac.RoleID.ToString().ToInt(), ac.ID, tSearchName.Text.Trim(), ddlType.SelectedValue.ToString().ToInt(1));
                var la1 = MainOrderController.GetByUserInViewFilterYCG(ac.RoleID.ToString().ToInt(), ac.ID, tSearchName.Text.Trim(), ddlType.SelectedValue.ToString().ToInt(1));
                var la = la1.Where(o => o.Status == 9).ToList();
                if (la != null)
                {
                    List<Danhsachorder> ds = new List<Danhsachorder>();
                    if (la.Count > 0)
                    {
                        int i = 1;
                        foreach (var item in la)
                        {
                            //string image = "";
                            //var pros = OrderController.GetByMainOrderID(item.ID);
                            //if (pros.Count > 0)
                            //{
                            //    image = pros[0].image_origin;
                            //}
                            Danhsachorder d = new Danhsachorder();
                            d.ID = item.ID;
                            d.STT = i;
                            d.ProductImage = item.anhsanpham;
                            d.TotalPriceVND = item.TotalPriceVND;
                            d.Deposit = item.Deposit;
                            d.CreatedDate = item.CreatedDate.ToString();
                            d.username = item.Uname;
                            d.kinhdoanh = item.saler;
                            d.dathang = item.dathang;
                            d.statusstring = PJUtils.IntToRequestAdmin(Convert.ToInt32(item.Status));
                            ds.Add(d);


                            //d.ProductImage = image;
                            //d.ShopID = item.ShopID;
                            //d.ShopName = item.ShopName;
                            //d.UID = item.UID.ToString().ToInt();
                            //d.username = AccountController.GetByID(Convert.ToInt32(item.UID)).Username;
                            //int dathangid = item.DathangID.ToString().ToInt(0);
                            //int salerid = item.SalerID.ToString().ToInt(0);
                            //int khotqid = item.KhoTQID.ToString().ToInt(0);
                            //int khovnid = item.KhoVNID.ToString().ToInt(0);
                            //string dathang = "";
                            //string kinhdoanh = "";
                            //string khotq = "";
                            //string khovn = "";

                            //var dh = AccountController.GetByID(dathangid);
                            //if (dh != null)
                            //    dathang = dh.Username;

                            //var kd = AccountController.GetByID(salerid);
                            //if (kd != null)
                            //    kinhdoanh = kd.Username;

                            //var ktq = AccountController.GetByID(khotqid);
                            //if (ktq != null)
                            //    khotq = ktq.Username;

                            //var kvn = AccountController.GetByID(khovnid);
                            //if (kvn != null)
                            //    khovn = kvn.Username;


                            //d.khotq = item.khotq;
                            //d.khovn = item.khoVN;

                            i++;
                        }
                        gr.DataSource = ds;
                    }
                }
            }
        }

        protected void r_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var g = e.Item as GridDataItem;
            if (g == null) return;

        }

        protected void gr_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {

        }
        #endregion

        #region button event

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gr.Rebind();
        }
        #endregion
        public class Danhsachorder
        {
            //public tbl_MainOder morder { get; set; }
            public int ID { get; set; }
            public int STT { get; set; }
            public string ProductImage { get; set; }
            public string ShopID { get; set; }
            public string ShopName { get; set; }
            public string TotalPriceVND { get; set; }
            public string Deposit { get; set; }
            public int UID { get; set; }
            public string CreatedDate { get; set; }
            public string statusstring { get; set; }
            public string username { get; set; }
            public string dathang { get; set; }
            public string kinhdoanh { get; set; }
            public string khotq { get; set; }
            public string khovn { get; set; }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            var la = MainOrderController.GetAll();
            StringBuilder StrExport = new StringBuilder();
            StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
            StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
            StrExport.Append("<DIV  style='font-size:12px;'>");
            StrExport.Append("<table border=\"1\">");
            StrExport.Append("  <tr>");
            StrExport.Append("      <th><strong>OrderID</strong></th>");
            StrExport.Append("      <th><strong>Người đặt</strong></th>");
            StrExport.Append("      <th><strong>Sản phẩm</strong></th>");
            StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
            StrExport.Append("      <th><strong>Trạng thái</strong></th>");
            StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
            StrExport.Append("  </tr>");
            foreach (var item in la)
            {
                string htmlproduct = "";
                string username = "";
                var ui = AccountController.GetByID(item.UID.ToString().ToInt(1));
                if (ui != null)
                {
                    username = ui.Username;
                }
                var products = OrderController.GetByMainOrderID(item.ID);
                foreach (var p in products)
                {
                    string image_src = p.image_origin;
                    if (!image_src.Contains("http:") && !image_src.Contains("https:"))
                        htmlproduct += "https:" + p.image_origin + " <br/> " + p.title_origin + "<br/><br/>";
                    else
                        htmlproduct += "" + p.image_origin + " <br/> " + p.title_origin + "<br/><br/>";
                }
                StrExport.Append("  <tr>");
                StrExport.Append("      <td>" + item.ID + "</td>");
                StrExport.Append("      <td>" + username + "</td>");
                StrExport.Append("      <td>" + htmlproduct + "</td>");
                StrExport.Append("      <td>" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceVND)) + "</td>");
                StrExport.Append("      <td>" + PJUtils.IntToRequestAdmin(Convert.ToInt32(item.Status)) + "</td>");
                StrExport.Append("      <td>" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</td>");
                StrExport.Append("  </tr>");
            }
            StrExport.Append("</table>");
            StrExport.Append("</div></body></html>");
            string strFile = "ExcelReportOrderList.xls";
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
    }
}
using NHST.Models;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Net;
using Supremes;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NHST.Bussiness;
using MB.Extensions;
using Telerik.Web.UI;
using System.Web.Script.Serialization;

namespace NHST.manager
{
    public partial class Package_Detail : System.Web.UI.Page
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
                    {
                        //if (ac.RoleID != 0 && ac.RoleID != 4 && ac.RoleID != 5 && ac.RoleID != 8)
                        //    Response.Redirect("/trang-chu");
                        //else
                        //    LoadData();                        
                        LoadData();
                    }
                }
            }
        }
        public void LoadData()
        {
            txtMVD.Text = "";
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);

            string search = "";
            if (!string.IsNullOrEmpty(Request.QueryString["s"]))
            {
                search = Request.QueryString["s"].ToString().Trim();
                tSearchName.Text = search;
            }
            int page = 0;
            Int32 Page = GetIntFromQueryString("Page");
            if (Page > 0)
            {
                page = Page - 1;
            }
            if (ac != null)
            {
                int roleID = ac.RoleID.ToString().ToInt();
                int i = Request.QueryString["ID"].ToInt(0);
                if (i > 0)
                {
                    ViewState["ID"] = i;
                    var p = BigPackageController.GetByID(i);
                    if (p != null)
                    {
                        int status = p.Status.ToString().ToInt();
                        if (roleID == 0)
                        {
                            //txtPackageCode.Enabled = true;
                            pWeight.Enabled = true;
                            pVolume.Enabled = true;
                        }
                        var bp = BigPackageController.GetAll("");
                        if (bp.Count > 0)
                        {
                            ddlPrefix1.Items.Clear();
                            ddlPrefix1.Items.Insert(0, "Chọn bao hàng");
                            foreach (var item in bp)
                            {
                                ListItem listitem = new ListItem(item.PackageCode, item.ID.ToString());
                                ddlPrefix1.Items.Add(listitem);
                            }
                            ddlPrefix1.DataBind();
                        }

                        txtPackageCode.Text = p.PackageCode;
                        pWeight.Value = p.Weight;
                        pVolume.Value = p.Volume;
                        ddlStatus.SelectedValue = p.Status.ToString();
                        ltrPackageName.Text = "Bao hàng " + p.PackageCode;
                        if (status < 1)
                        {
                            ltrCreateSmallpackage.Text = "<a href=\"/manager/Add-smallpackage.aspx?ID=" + p.ID + "\"  class=\"btn primary-btn\">Tạo mã vận đơn</a>";
                        }
                        if (roleID != 0 && roleID != 4 && roleID != 5)
                        {
                            btncreateuser.Enabled = false;
                        }
                        var total = SmallPackageController.GetTotalBuyBigPackage(i, search);
                        var la = SmallPackageController.GetBuyBigPackageBySQL_DK(i, search, page, 20);
                        pagingall(la, total);

                    }
                    else
                    {
                        Response.Redirect("/trang-chu");
                    }
                }
                else
                {
                    Response.Redirect("/trang-chu");
                }
            }
        }
        #region Pagging
        public void pagingall(List<SmallPackageController.ShowBigPackage> acs, int total)
        {
            int PageSize = 20;
            if (total > 0)
            {
                int TotalItems = total;
                if (TotalItems % PageSize == 0)
                    PageCount = TotalItems / PageSize;
                else
                    PageCount = TotalItems / PageSize + 1;

                Int32 Page = GetIntFromQueryString("Page");

                if (Page == -1) Page = 1;
                int FromRow = (Page - 1) * PageSize;
                int ToRow = Page * PageSize - 1;
                if (ToRow >= TotalItems)
                    ToRow = TotalItems - 1;
                StringBuilder hcm = new StringBuilder();
                int stt = 0;
                for (int i = 0; i < acs.Count; i++)
                {
                    stt++;
                    var item = acs[i];
                    hcm.Append("<tr>");
                    hcm.Append("<td>" + stt + "</td>");
                    hcm.Append("<td>" + item.OrderTransactionCode + "</td>");
                    hcm.Append("<td>" + item.ProductType + "</td>");
                    hcm.Append("<td>" + item.FeeShip + "</td>");
                    hcm.Append("<td>" + item.Weight + "</td>");
                    hcm.Append("<td>" + item.Volume + "</td>");
                    hcm.Append("<td>" + item.StatusString + "</td>");
                    hcm.Append("<td>" + item.CreatedDateString + "</td>");
                    hcm.Append("<td>");
                    hcm.Append("<div class=\"action-table\">");
                    hcm.Append("<a ID=" + item.ID + " onclick=\"CallBtn(" + item.ID + ")\" href = \"mavandon-chitiet.php\" class=\"tooltipped edit-mode\" data-position=\"top\" data-tooltip=\"Cập nhật\"><i class=\"material-icons\">edit</i></a>");
                    hcm.Append("</div>");
                    hcm.Append("</td>");
                    hcm.Append("</tr>");
                }
                ltr.Text = hcm.ToString();
            }
        }
        [WebMethod]
        public static string loadinfo(string ID)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var p = SmallPackageController.GetByID(Convert.ToInt32(ID));
            if (p != null)
            {

                tbl_SmallPackage l = new tbl_SmallPackage();
                l.ID = p.ID;
                l.MainOrderID = p.MainOrderID;
                l.OrderTransactionCode = p.OrderTransactionCode;
                l.ProductType = p.ProductType;
                l.FeeShip = p.FeeShip;
                l.Weight = p.Weight;
                l.Volume = p.Volume;
                l.IMGPackage = p.IMGPackage;
                l.Description = p.Description;
                l.Status = p.Status;
                l.BigPackageID = p.BigPackageID;
                return serializer.Serialize(l);
            }
            return serializer.Serialize(null);
        }
        public static Int32 GetIntFromQueryString(String key)
        {
            Int32 returnValue = -1;
            String queryStringValue = HttpContext.Current.Request.QueryString[key];
            try
            {
                if (queryStringValue == null)
                    return returnValue;
                if (queryStringValue.IndexOf("#") > 0)
                    queryStringValue = queryStringValue.Substring(0, queryStringValue.IndexOf("#"));
                returnValue = Convert.ToInt32(queryStringValue);
            }
            catch
            { }
            return returnValue;
        }
        private int PageCount;
        protected void DisplayHtmlStringPaging1()
        {
            Int32 CurrentPage = Convert.ToInt32(Request.QueryString["Page"]);
            if (CurrentPage == -1) CurrentPage = 1;
            string[] strText = new string[4] { "Trang đầu", "Trang cuối", "Trang sau", "Trang trước" };
            if (PageCount > 1)
                Response.Write(GetHtmlPagingAdvanced(6, CurrentPage, PageCount, Context.Request.RawUrl, strText));
        }
        private static string GetPageUrl(int currentPage, string pageUrl)
        {
            pageUrl = Regex.Replace(pageUrl, "(\\?|\\&)*" + "Page=" + currentPage, "");
            if (pageUrl.IndexOf("?") > 0)
            {
                pageUrl += "&Page={0}";
            }
            else
            {
                pageUrl += "?Page={0}";
            }
            return pageUrl;
        }
        public static string GetHtmlPagingAdvanced(int pagesToOutput, int currentPage, int pageCount, string currentPageUrl, string[] strText)
        {
            //Nếu Số trang hiển thị là số lẻ thì tăng thêm 1 thành chẵn
            if (pagesToOutput % 2 != 0)
            {
                pagesToOutput++;
            }

            //Một nửa số trang để đầu ra, đây là số lượng hai bên.
            int pagesToOutputHalfed = pagesToOutput / 2;

            //Url của trang
            string pageUrl = GetPageUrl(currentPage, currentPageUrl);


            //Trang đầu tiên
            int startPageNumbersFrom = currentPage - pagesToOutputHalfed; ;

            //Trang cuối cùng
            int stopPageNumbersAt = currentPage + pagesToOutputHalfed; ;

            StringBuilder output = new StringBuilder();

            //Nối chuỗi phân trang
            //output.Append("<div class=\"paging\">");
            //output.Append("<ul class=\"paging_hand\">");

            //Link First(Trang đầu) và Previous(Trang trước)
            if (currentPage > 1)
            {
                //output.Append("<li class=\"UnselectedPrev \" ><a title=\"" + strText[0] + "\" href=\"" + string.Format(pageUrl, 1) + "\">|<</a></li>");
                //output.Append("<li class=\"UnselectedPrev\" ><a title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\"><i class=\"fa fa-angle-left\"></i></a></li>");
                output.Append("<a class=\"prev-page pagi-button\" title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\">Prev</a>");
                //output.Append("<span class=\"Unselect_prev\"><a href=\"" + string.Format(pageUrl, currentPage - 1) + "\"></a></span>");
            }

            /******************Xác định startPageNumbersFrom & stopPageNumbersAt**********************/
            if (startPageNumbersFrom < 1)
            {
                startPageNumbersFrom = 1;

                //As page numbers are starting at one, output an even number of pages.  
                stopPageNumbersAt = pagesToOutput;
            }

            if (stopPageNumbersAt > pageCount)
            {
                stopPageNumbersAt = pageCount;
            }

            if ((stopPageNumbersAt - startPageNumbersFrom) < pagesToOutput)
            {
                startPageNumbersFrom = stopPageNumbersAt - pagesToOutput;
                if (startPageNumbersFrom < 1)
                {
                    startPageNumbersFrom = 1;
                }
            }
            /******************End: Xác định startPageNumbersFrom & stopPageNumbersAt**********************/

            //Các dấu ... chỉ những trang phía trước  
            if (startPageNumbersFrom > 1)
            {
                output.Append("<a href=\"" + string.Format(GetPageUrl(currentPage - 1, pageUrl), startPageNumbersFrom - 1) + "\">&hellip;</a>");
            }

            //Duyệt vòng for hiển thị các trang
            for (int i = startPageNumbersFrom; i <= stopPageNumbersAt; i++)
            {
                if (currentPage == i)
                {
                    output.Append("<a class=\"pagi-button current-active\">" + i.ToString() + "</a>");
                }
                else
                {
                    output.Append("<a class=\"pagi-button\" href=\"" + string.Format(pageUrl, i) + "\">" + i.ToString() + "</a>");
                }
            }

            //Các dấu ... chỉ những trang tiếp theo  
            if (stopPageNumbersAt < pageCount)
            {
                output.Append("<a href=\"" + string.Format(pageUrl, stopPageNumbersAt + 1) + "\">&hellip;</a>");
            }

            //Link Next(Trang tiếp) và Last(Trang cuối)
            if (currentPage != pageCount)
            {
                //output.Append("<span class=\"Unselect_next\"><a href=\"" + string.Format(pageUrl, currentPage + 1) + "\"></a></span>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\"><i class=\"fa fa-angle-right\"></i></a></li>");
                output.Append("<a class=\"next-page pagi-button\" title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\">Next</a>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[3] + "\" href=\"" + string.Format(pageUrl, pageCount) + "\">>|</a></li>");
            }
            //output.Append("</ul>");
            //output.Append("</div>");
            return output.ToString();
        }
        #endregion
        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            int ID = ViewState["ID"].ToString().ToInt(0);
            string username_current = Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            //string BackLink = "/manager/Add-Package.aspx";
            if (ID > 0)
            {
                var p = BigPackageController.GetByID(ID);
                if (p != null)
                {
                    string current_code = p.PackageCode;
                    double current_weight = Convert.ToDouble(p.Weight);
                    double current_volume = Convert.ToDouble(p.Volume);
                    int current_status = p.Status.ToString().ToInt(1);
                    string new_code = txtPackageCode.Text.Trim();
                    double new_weight = Convert.ToDouble(pWeight.Value);
                    double new_volume = Convert.ToDouble(pVolume.Value);
                    int new_status = ddlStatus.SelectedValue.ToString().ToInt(1);
                    string mavando = txtMavandon.Text.Trim();
                    if (!string.IsNullOrEmpty(mavando))
                    {
                        string[] listmavan = mavando.Split(';');
                        for (int i = 0; i < listmavan.Length - 1; i++)
                        {
                            string ma = listmavan[i];
                            var code = SmallPackageController.CheckCodeExist(ma);
                            if (code.Count > 0)
                            {
                                foreach (var c in code)
                                {
                                    if (c.Status > 1)
                                        SmallPackageController.UpdateBigPackageID(c.ID, p.ID);
                                }

                            }
                        }
                    }
                    //var cs = RadComboBox1.CheckedItems;
                    //if (cs.Count > 0)
                    //{
                    //    foreach (var item in cs)
                    //    {
                    //        SmallPackageController.UpdateBigPackageID(item.Value.ToInt(), p.ID);
                    //    }
                    //}

                    //Update bao hàng 
                    BigPackageController.Update(ID, new_code, new_weight, new_volume, new_status, currentDate, username_current);
                    if (new_status == 2)
                    {
                        var smlpac = SmallPackageController.GetBuyBigPackageID(ID, "");
                        if (smlpac.Count > 0)
                        {
                            foreach (var item in smlpac)
                            {
                                if (item.Status == 2)
                                    SmallPackageController.UpdateStatus(Convert.ToInt32(item.ID), 3, currentDate, username_current);
                            }
                        }
                    }
                    //Kiểm tra update History
                    if (current_code != new_code)
                    {
                        BigPackageHistoryController.Insert(ID, "PackageCode", current_code, new_code, 1, currentDate, username_current);
                    }
                    if (current_weight != new_weight)
                    {
                        BigPackageHistoryController.Insert(ID, "Weight", current_weight.ToString(), new_weight.ToString(), 1, currentDate, username_current);
                    }
                    if (current_volume != new_volume)
                    {
                        BigPackageHistoryController.Insert(ID, "Volume", current_volume.ToString(), new_volume.ToString(), 1, currentDate, username_current);
                    }
                    if (current_status != new_status)
                    {
                        BigPackageHistoryController.Insert(ID, "Status", current_status.ToString(), new_status.ToString(), 1, currentDate, username_current);
                    }
                    //PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật bao hàng thành công.", "s", true, BackLink, Page);
                    PJUtils.ShowMessageBoxSwAlert("Cập nhật bao hàng thành công.", "s", true, Page);
                }
            }
        }

        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            int i = Request.QueryString["ID"].ToInt(0);
            var la = SmallPackageController.GetBuyBigPackageID(i, tSearchName.Text);
            if (la != null)
            {
                if (la.Count > 0)
                {
                    List<smpacka> sps = new List<smpacka>();
                    int stt = 1;
                    foreach (var item in la)
                    {
                        smpacka sp = new smpacka();
                        sp.ID = item.ID;
                        sp.STT = stt;
                        var big = BigPackageController.GetByID(Convert.ToInt32(item.BigPackageID));
                        if (big != null)
                        {
                            sp.PackageCode = big.PackageCode;
                        }
                        sp.OrderTransactionCode = item.OrderTransactionCode;
                        sp.ProductType = item.ProductType;
                        sp.FeeShip = item.FeeShip.ToString();
                        sp.Weight = item.Weight.ToString();
                        sp.Volume = item.Volume.ToString();
                        sp.Status = Convert.ToInt32(item.Status);
                        sp.Statusname = PJUtils.IntToStringStatusSmallPackage(Convert.ToInt32(item.Status));
                        sp.CreatedDate = string.Format("{0:dd/MM/yyyy}", item.CreatedDate);
                        sps.Add(sp);
                        stt++;
                    }
                    //gr.DataSource = sps;
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
        protected void Update_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string username_current = Session["userLoginSystem"].ToString();
            DateTime currendDate = DateTime.Now;
            int id = hdfIDMVD.Value.ToInt();
            var s = SmallPackageController.GetByID(id);
            if (s != null)
            {
                string dbIMG = s.ListIMG;
                string[] listk = { };
                if (!string.IsNullOrEmpty(s.ListIMG))
                {
                    listk = dbIMG.Split('|');
                }

                string value = hdfListIMG.Value;
                string link = "";
                if (!string.IsNullOrEmpty(value))
                {
                    string[] listIMG = value.Split('|');
                    for (int i = 0; i < listIMG.Length - 1; i++)
                    {
                        string imageData = listIMG[i];
                        bool ch = listk.Any(x => x == imageData);
                        if (ch == true)
                        {
                            link += imageData + "|";
                        }
                        else
                        {
                            string path = System.Web.HttpContext.Current.Server.MapPath("~/Uploads/smallpackageIMG/");
                            string date = DateTime.Now.ToString("dd-MM-yyyy");
                            string time = DateTime.Now.ToString("hh:mm tt");
                            Page page = (Page)HttpContext.Current.Handler;
                            //  TextBox txtCampaign = (TextBox)page.FindControl("txtCampaign");
                            string k = i.ToString();
                            string fileNameWitPath = path + k + "-" + DateTime.Now.ToString().Replace("/", "-").Replace(" ", "-").Replace(":", "") + ".png";
                            string linkIMG = "/Uploads/smallpackageIMG/" + k + "-" + DateTime.Now.ToString().Replace("/", "-").Replace(" ", "-").Replace(":", "") + ".png";
                            link += linkIMG + "|";
                            //   string fileNameWitPath = path + s + ".png";
                            byte[] data;
                            string convert;
                            using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
                            {
                                using (BinaryWriter bw = new BinaryWriter(fs))
                                {
                                    if (imageData.Contains("data:image/png"))
                                    {
                                        convert = imageData.Replace("data:image/png;base64,", String.Empty);
                                        data = Convert.FromBase64String(convert);
                                        bw.Write(data);
                                    }
                                    else if (imageData.Contains("data:image/jpeg"))
                                    {
                                        convert = imageData.Replace("data:image/jpeg;base64,", String.Empty);
                                        data = Convert.FromBase64String(convert);
                                        bw.Write(data);
                                    }
                                    else if (imageData.Contains("data:image/gif"))
                                    {
                                        convert = imageData.Replace("data:image/gif;base64,", String.Empty);
                                        data = Convert.FromBase64String(convert);
                                        bw.Write(data);
                                    }
                                }
                            }
                        }
                    }
                }


                string current_ordertransactioncode = s.OrderTransactionCode;
                string current_producttype = s.ProductType;

                double current_ship = 0;
                if (s.FeeShip.ToString().ToFloat(0) > 0)
                    current_ship = Convert.ToDouble(s.FeeShip);

                double current_weight = 0;
                if (s.Weight.ToString().ToFloat(0) > 0)
                    current_weight = Convert.ToDouble(s.Weight);

                double current_volume = 0;
                if (s.Volume.ToString().ToFloat(0) > 0)
                    current_volume = Convert.ToDouble(s.Volume);

                int current_status = s.Status.ToString().ToInt();
                int current_BigpackageID = s.BigPackageID.ToString().ToInt(0);

                string new_ordertransactionCode = txtMVD.Text.Trim();

                string new_producttype = txtLoaiHang.Text.Trim();

                double new_ship = 0;
                if (txtFeeShip.Text.ToString().ToFloat(0) > 0)
                    new_ship = Convert.ToDouble(txtFeeShip.Text);

                double new_weight = 0;
                if (txtWeight.Text.ToString().ToFloat(0) > 0)
                    new_weight = Convert.ToDouble(txtWeight.Text);

                double new_volume = 0;
                if (txtVolume.Text.ToString().ToFloat(0) > 0)
                    new_volume = Convert.ToDouble(txtVolume.Text);

                int new_status = ddlStatusUpdate.SelectedValue.ToString().ToInt(1);
                int new_BigpackageID = ddlPrefix1.SelectedValue.ToString().ToInt(0);
                string new_description = txtDescription.Text.Trim();

                string kq = SmallPackageController.Update(id, new_BigpackageID, new_ordertransactionCode, new_producttype, new_ship,
                   new_weight, new_volume, new_status, new_description, DateTime.Now, username_current);

                string kt = SmallPackageController.UpdateIMG(id, link, DateTime.Now, username_current);

                var allsmall = SmallPackageController.GetBuyBigPackageID(new_BigpackageID, "");
                if (allsmall.Count > 0)
                {
                    double totalweight = 0;
                    foreach (var item in allsmall)
                    {
                        totalweight += Convert.ToDouble(item.Weight);
                    }
                    BigPackageController.UpdateWeight(new_BigpackageID, totalweight);
                }

                if (current_ordertransactioncode != new_ordertransactionCode)
                {
                    BigPackageHistoryController.Insert(id, "OrderTransactionCode", current_ordertransactioncode, new_ordertransactionCode, 2, currendDate, username_current);
                }
                if (current_producttype != new_producttype)
                {
                    BigPackageHistoryController.Insert(id, "ProductType", current_producttype, new_producttype, 2, currendDate, username_current);
                }
                if (current_ship != new_ship)
                {
                    BigPackageHistoryController.Insert(id, "FeeShip", current_ship.ToString(), new_ship.ToString(), 2, currendDate, username_current);
                }
                if (current_weight != new_weight)
                {
                    BigPackageHistoryController.Insert(id, "Weight", current_weight.ToString(), new_weight.ToString(), 2, currendDate, username_current);
                }
                if (current_volume != new_volume)
                {
                    BigPackageHistoryController.Insert(id, "Volume", current_volume.ToString(), new_volume.ToString(), 2, currendDate, username_current);
                }
                if (current_status != new_status)
                {
                    BigPackageHistoryController.Insert(id, "Status", current_status.ToString(), new_status.ToString(), 2, currendDate, username_current);
                }
                if (current_BigpackageID != new_BigpackageID)
                {
                    BigPackageHistoryController.Insert(id, "BigpackageID", current_BigpackageID.ToString(), new_BigpackageID.ToString(), 2, currendDate, username_current);
                }

                if (kq.ToInt(0) > 0)
                {
                    PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công.", "s", true, Page);
                }
                else
                    PJUtils.ShowMessageBoxSwAlert("Cập nhật thất bại.", "e", true, Page);
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string i = ViewState["ID"].ToString();
            string searchname = tSearchName.Text.Trim();
            if (string.IsNullOrEmpty(searchname) == false)
            {
                Response.Redirect("Package-Detail.aspx?ID=" + i + "&s=" + searchname);
            }
            else
            {

                Response.Redirect("Package-Detail.aspx?ID=" + i);
            }
        }
        #endregion       
        public class smpacka
        {
            public int ID { get; set; }
            public int STT { get; set; }
            public int BigPackageID { get; set; }
            public string PackageCode { get; set; }
            public string OrderTransactionCode { get; set; }
            public string ProductType { get; set; }
            public string FeeShip { get; set; }
            public string Weight { get; set; }
            public string Volume { get; set; }
            public int Status { get; set; }
            public string Statusname { get; set; }
            public string CreatedDate { get; set; }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(Session["Page"].ToString()))
            {
                Response.Redirect("Warehouse-Management?Page=" + Session["Page"].ToString());
            }
            else
            {
                Response.Redirect("Warehouse-Management");
            }
        }
    }
}
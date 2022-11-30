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
using System.Data;
using System.Text;
using MB.Extensions;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Text.RegularExpressions;

namespace NHST.manager
{
    public partial class kien_that_lac : System.Web.UI.Page
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
                    LoadData();
                    LoadFrom();
                    //if (ac.RoleID != 0 && ac.RoleID != 4 && ac.RoleID != 5 && ac.RoleID != 8)
                    //{

                    //}
                    //else
                    //{
                    //    //ltraddminre.Text = "<a type=\"button\" class=\"btn btn-success m-b-sm\" href=\"/Admin/Add-smallpackage.aspx\">Thêm mã vận đơn</a>";
                    //}
                }
            }
        }

        private void LoadFrom()
        {
            var bp = BigPackageController.GetAll("");
            if (bp.Count > 0)
            {
                ddlPrefix.Items.Clear();
                ddlPrefix.Items.Insert(0, "Chọn bao hàng");
                foreach (var item in bp)
                {
                    ListItem listitem = new ListItem(item.PackageCode, item.ID.ToString());
                    ddlPrefix.Items.Add(listitem);
                }
                ddlPrefix.DataBind();
            }
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            if (ac != null)
            {
                int roleID = ac.RoleID.ToString().ToInt();
                if (roleID == 0)
                {
                    txtEditOrderTransaction.Enabled = true;
                    txtEditProductType.Enabled = true;
                    txtEditFeeShip.Enabled = true;
                    txtEditWeight.Enabled = true;
                    txtEditVolume.Enabled = true;
                }
                else
                {
                    txtEditOrderTransaction.Enabled = false;
                    txtEditProductType.Enabled = false;
                    txtEditFeeShip.Enabled = false;
                    txtEditWeight.Enabled = false;
                    txtEditVolume.Enabled = false;
                }
            }
        }

        private void LoadData()
        {
            string search = "";
            if (!string.IsNullOrEmpty(Request.QueryString["s"]))
            {
                search = Request.QueryString["s"].ToString().Trim();
                search_name.Text = search;
            }
            int page = 0;
            Int32 Page = GetIntFromQueryString("Page");
            if (Page > 0)
            {
                page = Page - 1;
            }
            var total = SmallPackageController.GetTotalLostBySQL(search);
            var la = SmallPackageController.GetAllLostBySQL(search, 20, page);
            pagingall(la, total);
        }
        #region button event
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchname = search_name.Text.Trim();
            if (!string.IsNullOrEmpty(searchname))
            {
                Response.Redirect("kien-that-lac?s=" + searchname);
            }
            else
            {
                Response.Redirect("kien-that-lac");
            }
        }
        #endregion
        #region Pagging
        public void pagingall(List<tbl_SmallPackage> acs, int total)
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
                for (int i = 0; i < acs.Count; i++)
                {
                    var item = acs[i];
                    var baoHang = BigPackageController.GetByID(item.BigPackageID.Value);
                    string mabaohang = "";
                    if (baoHang != null)
                        mabaohang = baoHang.PackageCode;
                    hcm.Append("<tr>");
                    hcm.Append("<td>" + item.ID + "</td>");
                    hcm.Append("<td>" + mabaohang + "</td>");
                    hcm.Append("<td>" + item.OrderTransactionCode + "</td>");
                    hcm.Append("<td>" + item.MainOrderID + "</td>");
                    hcm.Append("<td>" + item.ProductType + "</td>");
                    hcm.Append("<td>" + item.FeeShip + "</td>");
                    hcm.Append("<td>" + item.Weight + "</td>");
                    hcm.Append("<td>" + item.Volume + "</td>");
                    hcm.Append("<td>" + PJUtils.IntToStringStatusSmallPackageWithBG45(item.Status.Value) + "</td>");
                    hcm.Append("<td>" + item.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm") + "</td>");
                    hcm.Append("<td>");
                    hcm.Append("<div class=\"action-table\">");
                    hcm.Append("<a href=\"#\" class=\"edit-mode\" id=\"EditFunction-" + item.ID + "\" onclick=\"EditFunction(" + item.ID + ")\" data-position=\"top\"><i class=\"material-icons\">edit</i><span>Cập nhật</span></a>");
                    hcm.Append("</div>");
                    hcm.Append("</td>");
                    hcm.Append("</tr>");
                }
                ltr.Text = hcm.ToString();
            }
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
                if (pageUrl.IndexOf("Page=") > 0)
                {
                    int a = pageUrl.IndexOf("Page=");
                    int b = pageUrl.Length;
                    pageUrl.Remove(a, b - a);
                }
                else
                {
                    pageUrl += "&Page={0}";
                }

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
        [WebMethod]
        public static string loadinfo(string ID)
        {

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            var p = SmallPackageController.GetByID(ID.ToInt(0));
            if (p != null)
            {

                tbl_SmallPackage l = new tbl_SmallPackage();
                l.ID = p.ID;
                l.OrderTransactionCode = p.OrderTransactionCode;
                l.BigPackageID = p.BigPackageID;
                l.MainOrderID = p.MainOrderID;
                l.ProductType = p.ProductType;
                l.FeeShip = p.FeeShip;
                l.Weight = p.Weight;
                l.CreatedDate = p.CreatedDate;
                l.Volume = p.Volume;
                l.Description = p.Description;
                l.Status = p.Status;
                l.ListIMG = p.ListIMG;
                return serializer.Serialize(l);
            }
            return serializer.Serialize(null);
        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string username_current = Session["userLoginSystem"].ToString();
            DateTime currendDate = DateTime.Now;
            int id = hdfID.Value.ToInt(0);
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
                //string link = "";
                //string FolderURL = "/Uploads/smallpackageIMG/";
                //if (EditIMG.HasFiles)
                //{
                //    foreach (HttpPostedFile f in EditIMG.PostedFiles)
                //    {
                //        var date = DateTime.Now;
                //        string x = date.Day.ToString() + date.Month.ToString() + date.Year.ToString() + date.Hour.ToString() + date.Minute.ToString() + date.Millisecond.ToString();
                //        var o = FolderURL + Path.GetFileNameWithoutExtension(f.FileName) + "_" + x + Path.GetExtension(f.FileName);
                //        if (f.FileName.ToLower().Contains(".jpg") || f.FileName.ToLower().Contains(".png") || f.FileName.ToLower().Contains(".jpeg"))
                //        {
                //            if (f.ContentType == "image/png" || f.ContentType == "image/jpeg" || f.ContentType == "image/jpg")
                //            {                              
                //                try
                //                {
                //                    f.SaveAs(Server.MapPath(o));
                //                    link += o +"|";
                //                }
                //                catch
                //                {

                //                }
                //            }
                //        }
                //    }
                //}
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

                string new_ordertransactionCode = txtEditOrderTransaction.Text.Trim();
                string new_producttype = txtEditProductType.Text.Trim();

                double new_ship = 0;
                if (txtEditFeeShip.Text.ToFloat(0) > 0)
                    new_ship = Convert.ToDouble(txtEditFeeShip.Text);

                double new_weight = 0;
                if (txtEditWeight.Text.ToFloat(0) > 0)
                    new_weight = Convert.ToDouble(txtEditWeight.Text);

                double new_volume = 0;
                if (txtEditVolume.Text.ToString().ToFloat(0) > 0)
                    new_volume = Convert.ToDouble(txtEditVolume.Text);

                int new_status = ddlStatus.SelectedValue.ToString().ToInt(1);
                int new_BigpackageID = ddlPrefix.SelectedValue.ToString().ToInt(0);
                string new_description = txtEditNote.Text.Trim();
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
    }
}
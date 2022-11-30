using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST.manager
{
    public partial class add_transportation_new : System.Web.UI.Page
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
                    if (ac.RoleID != 4 && ac.RoleID != 0 && ac.RoleID != 2 && ac.RoleID != 5)
                        Response.Redirect("/trang-chu");
                }
                LoadDDL();
            }
        }


        public void LoadDDL()
        {
            //Danh sách bao
            //var bs = BigPackageController.GetAllWithStatus(1);
            //ddlBigpackage.Items.Clear();
            //ddlBigpackage.Items.Insert(0, new ListItem("Chọn bao lớn", "0"));
            //if (bs.Count > 0)
            //{
            //    foreach (var b in bs)
            //    {
            //        ListItem listitem = new ListItem(b.PackageCode, b.ID.ToString());
            //        ddlBigpackage.Items.Add(listitem);
            //    }
            //}
            //ddlBigpackage.DataBind();

            //Danh sách kho TQ

            var warehouseTQ = WarehouseFromController.GetAllWithIsHidden(false);
            ddlWareHouseFrom.Items.Clear();
            ddlWareHouseFrom.Items.Insert(0, new ListItem("Chọn kho TQ", "0"));
            if (warehouseTQ.Count > 0)
            {
                foreach (var b in warehouseTQ)
                {
                    ListItem listitem = new ListItem(b.WareHouseName, b.ID.ToString());
                    ddlWareHouseFrom.Items.Add(listitem);
                }
            }
            ddlWareHouseFrom.DataBind();


            var wht = WarehouseController.GetAllWithIsHidden(false);
            ddlWareHouseTo.Items.Clear();
            ddlWareHouseTo.Items.Insert(0, new ListItem("Chọn kho VN", "0"));
            if (wht.Count > 0)
            {
                foreach (var b in wht)
                {
                    ListItem listitem = new ListItem(b.WareHouseName, b.ID.ToString());
                    ddlWareHouseTo.Items.Add(listitem);
                }
            }
            ddlWareHouseTo.DataBind();

            var shippingType = ShippingTypeToWareHouseController.GetAllWithIsHidden(false);
            ddlShippingType.Items.Clear();
            ddlShippingType.Items.Insert(0, new ListItem("Chọn phương thức VC", "0"));
            if (shippingType.Count > 0)
            {
                foreach (var b in shippingType)
                {
                    ListItem listitem = new ListItem(b.ShippingTypeName, b.ID.ToString());
                    ddlShippingType.Items.Add(listitem);
                }
            }
            ddlShippingType.DataBind();
        }

        [WebMethod]
        public static string CheckCode(string barcode)
        {
            DateTime currentDate = DateTime.Now;
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                var bc = SmallPackageController.GetByOrderTransactionCode(barcode.Trim());
                if (bc != null)
                {
                    return "none";
                }
                else
                {
                    small s = new small();
                    s.Barcode = barcode;
                    var listb = BigPackageController.GetAllWithStatus(1);
                    if (listb.Count > 0)
                    {
                        s.ListBig = listb;
                    }
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    return serializer.Serialize(s);
                }
            }
            else
                return "none";
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
                            DateTime.Now, username);

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

        public class small
        {
            public string Barcode { get; set; }
            public List<tbl_BigPackage> ListBig { get; set; }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            double currency = 0;
            var config = ConfigurationController.GetByTop1();
            if (config != null)
            {
                currency = Convert.ToDouble(config.Currency);
            }

            DateTime currentDate = DateTime.Now;
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            if (obj_user != null)
            {
                var userkh = AccountController.GetByUsername(txtUsername.Text.Trim());
                if (userkh != null)
                {
                    string listPackage = hdfValue.Value;
                    if (!string.IsNullOrEmpty(listPackage))
                    {

                        string[] list = listPackage.Split('*');
                        if (list.Length - 1 > 0)
                        {

                            for (int i = 0; i < list.Length - 1; i++)
                            {
                                string items = list[i];
                                string[] item = items.Split('|');
                                string code = item[0].Trim();
                                double weight = Math.Round(Convert.ToDouble(item[1].ToString()), 1);

                                double d = Math.Round(Convert.ToDouble(item[2].ToString()), 2);
                                double r = Math.Round(Convert.ToDouble(item[3].ToString()), 2);
                                double c = Math.Round(Convert.ToDouble(item[4].ToString()), 2);
                                int bigpackageID = Convert.ToInt32(item[5]);
                                string note = item[6];

                                string tID = TransportationOrderNewController.Insert(userkh.ID, userkh.Username, weight.ToString(), "0", "0", "0", "0", "0", "0",
                                "0", "0", "0", 0, code, 3, note, "", "0", "0", currentDate, username, ddlWareHouseFrom.SelectedValue.ToInt(1), ddlWareHouseTo.SelectedValue.ToInt(1), ddlShippingType.SelectedValue.ToInt(1));
                                int packageID = 0;
                                var smallpackage = SmallPackageController.GetByOrderTransactionCode(code);
                                if (smallpackage == null)
                                {
                                    string kq = SmallPackageController.InsertWithTransportationID(tID.ToInt(0), bigpackageID, code, "",
                                    0, weight, 0, 1, currentDate, username);
                                    packageID = kq.ToInt();
                                    SmallPackageController.UpdateNote(kq.ToInt(0), note);
                                    TransportationOrderNewController.UpdateSmallPackageID(tID.ToInt(0), packageID);
                                }
                            }
                        }

                        PJUtils.ShowMessageBoxSwAlert("Tạo đơn hàng thành công", "s", true, Page);
                    }
                    else
                    {
                        PJUtils.ShowMessageBoxSwAlert("User khách hàng không tồn tại trong hệ thống, vui lòng thử lại sau.", "e", false, Page);
                    }
                }
            }
        }
    }
}
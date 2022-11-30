using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class tao_ma_van_don_ky_gui : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] != null)
                {
                    LoadDDL();
                    loaddata();
                }
                else
                {
                    Response.Redirect("/dang-nhap");
                }
            }
        }

        public void loaddata()
        {
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            if (obj_user != null)
            {
                int id = obj_user.ID;
                ViewState["UID"] = id;
                lblUsername.Text = username;
            }
        }

        public void LoadDDL()
        {
            var warehousefrom = WarehouseFromController.GetAllWithIsHidden(false);
            if (warehousefrom.Count > 0)
            {
                ddlWarehouseFrom.DataSource = warehousefrom;
                ddlWarehouseFrom.DataBind();
            }
            var warehouse = WarehouseController.GetAllWithIsHidden(false);
            if (warehouse.Count > 0)
            {
                ddlReceivePlace.DataSource = warehouse;
                ddlReceivePlace.DataBind();
            }

            var shippingtype = ShippingTypeToWareHouseController.GetAllWithIsHidden(false);
            if (shippingtype.Count > 0)
            {
                ddlShippingType.DataSource = shippingtype;
                ddlShippingType.DataBind();
            }
        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            if (obj_user != null)
            {
                int UID = obj_user.ID;
                string listPackage = hdfProductList.Value;
                if (!string.IsNullOrEmpty(listPackage))
                {
                    string[] list = listPackage.Split('|');
                    if (list.Length - 1 > 0)
                    {
                        for (int i = 0; i < list.Length - 1; i++)
                        {
                            string items = list[i];
                            string[] item = items.Split(']');
                            string code = item[0];
                            string note = item[1];

                            string tID = TransportationOrderNewController.Insert(UID, username, "0", "0", "0", "0", "0", "0", "0",
                                 "0", "0", "0", 0, code, 1, note, "", "0", "0", currentDate, username, Convert.ToInt32(ddlWarehouseFrom.SelectedValue), Convert.ToInt32(ddlReceivePlace.SelectedValue), Convert.ToInt32(ddlShippingType.SelectedValue));
                            int packageID = 0;
                            var smallpackage = SmallPackageController.GetByOrderTransactionCode(code);
                            if (smallpackage == null)
                            {
                                string kq = SmallPackageController.InsertWithTransportationID(tID.ToInt(0), 0, code, "",
                                0, 0, 0, 1, currentDate, username);
                                packageID = kq.ToInt();
                                TransportationOrderNewController.UpdateSmallPackageID(tID.ToInt(0), packageID);
                            }
                        }
                    }
                    PJUtils.ShowMessageBoxSwAlert("Tạo đơn hàng thành công", "s", true, Page);
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Vui lòng nhập ít nhất 1 mã kiện.", "e", true, Page);
                }
            }
        }

        [WebMethod]
        public static string checkbefore(string listStr)
        {
            string returns = "";
            if (!string.IsNullOrEmpty(listStr))
            {
                double totalWeight = 0;
                string[] list = listStr.Split('|');
                bool checkConflitCode = false;
                if (list.Length - 1 > 0)
                {
                    for (int i = 0; i < list.Length - 1; i++)
                    {
                        string items = list[i];
                        string[] item = items.Split(']');
                        string code = item[0].ToString().Trim();
                        var getsmallcheck = SmallPackageController.GetByOrderCode(code);
                        if (getsmallcheck.Count > 0)
                        {
                            checkConflitCode = true;
                            returns += code + "; ";
                        }
                    }
                }
                if (checkConflitCode == true)
                {
                    return returns;
                }
                else
                {
                    return "ok";
                }
            }
            return "ok";
        }
    }
}
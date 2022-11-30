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
    public partial class tao_ma_van_don_ky_gui_app : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDDL();
                LoadData();
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

        public void LoadData()
        {
            string Key = Request.QueryString["Key"];
            int UID = Request.QueryString["UID"].ToInt(0);
            if (UID > 0)
            {
                var tk = DeviceTokenController.GetByToken(UID, Key);
                if (tk != null)
                {
                    var ac = AccountController.GetByID(UID);
                    if (ac != null)
                    {
                        ViewState["UID"] = UID;
                        ViewState["Key"] = Key;
                        pnMobile.Visible = true;
                        lbUsername.Text = ac.Username;
                    }
                }
                else
                {
                    pnShowNoti.Visible = true;
                }
            }
            else
            {
                pnShowNoti.Visible = true;
            }
        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            double currency = 0;
            var config = ConfigurationController.GetByTop1();
            if (config != null)
            {
                currency = Convert.ToDouble(config.Currency);
            }
            DateTime currentDate = DateTime.Now;
            int UID = ViewState["UID"].ToString().ToInt(0);
            var obj_user = AccountController.GetByID(UID);
            if (obj_user != null)
            {
                string username = obj_user.Username;
                string listPackage = hdfProductList.Value;
                if (!string.IsNullOrEmpty(listPackage))
                {
                    double totalWeight = 0;
                    string[] list = listPackage.Split('|');
                    if (list.Length - 1 > 0)
                    {
                        for (int i = 0; i < list.Length - 1; i++)
                        {
                            string items = list[i];
                            string[] item = items.Split(']');
                            string code = item[0].ToString();
                            string note = item[1].ToString();

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
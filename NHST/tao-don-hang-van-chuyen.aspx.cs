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
    public partial class tao_don_hang_van_chuyen1 : System.Web.UI.Page
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

        protected void btncreateuser_Click(object sender, EventArgs e)
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
                string listPackage = hdfProductList.Value;
                if (!string.IsNullOrEmpty(listPackage))
                {
                    double totalWeight = 0;
                    double totalCodTQCYN = 0;
                    double totalCodTQVND = 0;
                    double totalPriceVND = 0;
                    string[] list = listPackage.Split('|');
                    if (list.Length - 1 > 0)
                    {
                        for (int i = 0; i < list.Length - 1; i++)
                        {
                            string items = list[i];
                            string[] item = items.Split(']');
                            string code = item[0].ToString().Trim();
                            string packageType = item[1].ToString();
                            string quantity = item[2];


                            double weight = Math.Round(Convert.ToDouble(item[3].ToString()), 1);
                            double codeTQ = 0;
                            if (item[7].ToFloat(0) > 0)
                                codeTQ = Math.Round(Convert.ToDouble(item[7]), 2);

                            totalCodTQCYN += codeTQ;
                            totalWeight += weight;
                        }
                    }
                    totalCodTQVND = totalCodTQCYN * currency;
                    totalPriceVND += Math.Round(totalCodTQVND, 0);
                    string kq = TransportationOrderController.InsertNew(obj_user.ID, username,
                        ddlWarehouseFrom.SelectedValue.ToInt(1),
                        ddlReceivePlace.SelectedValue.ToInt(1), ddlShippingType.SelectedValue.ToInt(1),
                        1, totalWeight, currency, 0, 0, 0, totalCodTQCYN, totalCodTQVND, totalPriceVND,
                        txtNote.Text, currentDate, username);

                    //string kq = TransportationOrderController.Insert(obj_user.ID, username,
                    //    ddlWarehouseFrom.SelectedValue.ToInt(1),
                    //    ddlReceivePlace.SelectedValue.ToInt(1), ddlShippingType.SelectedValue.ToInt(1),
                    //    1, totalWeight, currency, 0, txtNote.Text, currentDate, username);
                    if (kq.ToInt(0) > 0)
                    {
                        if (list.Length - 1 > 0)
                        {
                            for (int i = 0; i < list.Length - 1; i++)
                            {
                                string items = list[i];
                                string[] item = items.Split(']');
                                string code = item[0].ToString();
                                string packageType = item[1].ToString();
                                string quantity = item[2];

                                double weight = Math.Round(Convert.ToDouble(item[3].ToString()), 1);
                                bool isCheckProduct = false;
                                if (item[4].ToInt(0) == 1)
                                {
                                    isCheckProduct = true;
                                }

                                bool isPackage = false;
                                if (item[5].ToInt(0) == 1)
                                {
                                    isPackage = true;
                                }

                                bool isInsurrance = false;
                                if (item[6].ToInt(0) == 1)
                                {
                                    isInsurrance = true;
                                }

                                double codeTQ = 0;
                                if (item[7].ToFloat(0) > 0)
                                    codeTQ = Math.Round(Convert.ToDouble(item[7]), 2);

                                codeTQ = Math.Round(codeTQ, 2);
                                double codeTQVND = codeTQ * currency;
                                codeTQVND = Math.Round(codeTQVND, 0);

                                string note = item[8].ToString();
                                TransportationOrderDetailController.InsertNew(kq.ToInt(0), code, weight,
                                    packageType, isCheckProduct, isPackage, isInsurrance,
                                    codeTQ.ToString(), codeTQVND.ToString(), note, quantity.ToString(),
                                    currentDate, username);
                                //TransportationOrderDetailController.Insert(kq.ToInt(0), orderCode, weight, currentDate, username);
                            }
                        }

                        var setNoti = SendNotiEmailController.GetByID(13);
                        if (setNoti.IsSentNotiAdmin == true)
                        {
                            var admins = AccountController.GetAllByRoleID(0);
                            if (admins.Count > 0)
                            {
                                foreach (var admin in admins)
                                {
                                    NotificationsController.Inser(admin.ID,
                                                                       admin.Username, kq.ToInt(),
                                                                        "Có đơn hàng vận chuyển hộ mới ID là: " + kq + ".",
                                                                       10, currentDate, username, false);
                                    string strPathAndQuery = Request.Url.PathAndQuery;
                                    string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                    string datalink = "" + strUrl + "manager/transportation-list/";
                                    PJUtils.PushNotiDesktop(admin.ID, "Có đơn hàng vận chuyển hộ mới ID là: " + kq, datalink);
                                }
                            }
                        }

                        if (setNoti.IsSentEmailAdmin == true)
                        {
                            var admins = AccountController.GetAllByRoleID(0);
                            if (admins.Count > 0)
                            {
                                foreach (var admin in admins)
                                {
                                    try
                                    {
                                        PJUtils.SendMailGmail_new( admin.Email,
                                            "Thông báo tại NHẬP HÀNG 360.", "Có đơn hàng vận chuyển hộ mới ID là: " + kq + ".", "");
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                    PJUtils.ShowMessageBoxSwAlert("Tạo đơn hàng thành công", "s", true, Page);
                }
                else
                {
                    //string kq = TransportationOrderController.Insert(obj_user.ID, username,
                    //    ddlWarehouseFrom.SelectedValue.ToInt(1),
                    //   ddlReceivePlace.SelectedValue.ToInt(1), ddlShippingType.SelectedValue.ToInt(1), 1, 0,
                    //   currency, 0, txtNote.Text, currentDate, username);
                    //PJUtils.ShowMessageBoxSwAlert("Tạo đơn hàng thành công", "s", true, Page);
                    PJUtils.ShowMessageBoxSwAlert("Không tồn tại kiện trong đơn, vui lòng thêm kiện hàng.", "e", true, Page);
                }
            }
        }
    }
}
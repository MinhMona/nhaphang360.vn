using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST.manager
{
    public partial class chi_tiet_vch_admin : System.Web.UI.Page
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
                    if (ac.RoleID != 0)
                        Response.Redirect("/trang-chu");
                }
                LoadShippingType();
                LoadData();

            }
        }
        public void LoadShippingType()
        {
            ddlShippingType.Items.Clear();
            ddlShippingType.Items.Insert(0, "Chưa chọn hình thức vận chuyển");
            var s = ShippingTypeVNController.GetAllWithIsHidden("", false);
            if (s.Count > 0)
            {
                foreach (var item in s)
                {
                    ListItem listitem = new ListItem(item.ShippingTypeVNName.ToString(), item.ID.ToString());
                    ddlShippingType.Items.Add(listitem);
                }
            }
            ddlShippingType.DataBind();
        }

        public void LoadData()
        {
            var id = Request.QueryString["ID"].ToInt(0);
            if (id > 0)
            {
                ViewState["ID"] = id;
                var config = ConfigurationController.GetByTop1();
                if (config != null)
                {
                    hdfCurrency.Value = config.AgentCurrency;
                }
                var user = AccountController.GetAll_RoleID_All("");
                if (user.Count > 0)
                {
                    ddlUsername.DataSource = user;
                    ddlUsername.DataBind();
                }
                var t = TransportationOrderNewController.GetByID(id);
                if (t != null)
                {
                    //ddlUsername.Text = t.Username;

                    int index = 0;
                    var a = AccountController.GetByUsername(t.Username);
                    if (a != null)
                    {
                        if (user.Count > 0)
                        {
                            index = user.FindIndex(n => n.ID == a.ID);
                        }
                    }

                    ddlUsername.SelectedIndex = index;
                    ddlUsername.DataBind();

                    txtBarcode.Text = t.BarCode;
                    double weight = 0;
                    int spID = 0;
                    if (t.SmallPackageID != null)
                        spID = Convert.ToInt32(t.SmallPackageID);
                    var package = SmallPackageController.GetByID(spID);
                    if (package != null)
                    {
                        if (package.Weight != null)
                            if (package.Weight.ToString().ToFloat(0) > 0)
                                weight = Convert.ToDouble(package.Weight);
                    }
                    rWeight.Value = weight;
                    double addFeeCYN = 0;
                    double addFeeVND = 0;
                    if (!string.IsNullOrEmpty(t.AdditionFeeCYN))
                    {
                        if (t.AdditionFeeCYN.ToFloat(0) > 0)
                            addFeeCYN = Convert.ToDouble(t.AdditionFeeCYN);
                    }
                    if (!string.IsNullOrEmpty(t.AdditionFeeVND))
                    {
                        if (t.AdditionFeeVND.ToFloat(0) > 0)
                            addFeeVND = Convert.ToDouble(t.AdditionFeeVND);
                    }
                    rAdditionFeeCYN.Value = addFeeCYN;
                    rAdditionFeeVND.Value = addFeeVND;

                    double sensorCYN = 0;
                    double sensorVND = 0;
                    if (!string.IsNullOrEmpty(t.SensorFeeCYN))
                    {
                        if (t.SensorFeeCYN.ToFloat(0) > 0)
                            sensorCYN = Convert.ToDouble(t.SensorFeeCYN);
                    }
                    if (!string.IsNullOrEmpty(t.SensorFeeeVND))
                    {
                        if (t.SensorFeeeVND.ToFloat(0) > 0)
                            sensorVND = Convert.ToDouble(t.SensorFeeeVND);
                    }

                    rSensorFeeCYN.Value = sensorCYN;
                    rSensorFeeeVND.Value = sensorVND;
                    //txtTotalPriceVND.Text = string.Format("{0:N0} VND", Math.Round(Convert.ToDouble(t.TotalPriceVND), 0));
                    txtSummary.Text = t.Note;
                    txtStaffNote.Text = t.StaffNote;
                    ddlStatus.SelectedValue = t.Status.ToString();
                    ddlShippingType.SelectedValue = t.ShippingTypeVN.ToString();
                    txtExportRequestNote.Text = t.ExportRequestNote;
                    string dateexre = "";
                    string dateout = "";
                    if (t.DateExportRequest != null)
                        dateexre = string.Format("{0:dd/MM/yyyy}", t.DateExportRequest);
                    if (t.DateExport != null)
                        dateout = string.Format("{0:dd/MM/yyyy}", t.DateExport);
                    txtDateExportRequest.Text = dateexre;
                    txtDateExport.Text = dateout;
                    txtCancelReason.Text = t.CancelReason;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string username_current = Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            int ID = ViewState["ID"].ToString().ToInt(0);
            double currency = 0;
            var config = ConfigurationController.GetByTop1();
            if (config != null)
            {
                currency = Convert.ToDouble(config.AgentCurrency);
            }

            var a = AccountController.GetByID(Convert.ToInt32(ddlUsername.Text));
            var t = TransportationOrderNewController.GetByID(ID);
            if (t != null)
            {
                int packageID = Convert.ToInt32(t.SmallPackageID);
                int statusOld = Convert.ToInt32(t.Status);
                int statusNew = ddlStatus.SelectedValue.ToInt();
                TransportationOrderNewController.Update(ID, a.ID, a.Username, rWeight.Value.ToString(), currency.ToString(),
                    rAdditionFeeCYN.Value.ToString(), rAdditionFeeVND.Value.ToString(), "", "", "", "", rSensorFeeCYN.Value.ToString(),
                    rSensorFeeeVND.Value.ToString(), packageID, txtBarcode.Text, statusNew, txtSummary.Text,
                    txtStaffNote.Text, "", "", currentDate, username_current);
                SmallPackageController.UpdateUIDandUserName(Convert.ToInt32(t.SmallPackageID),a.ID,a.Username);
                PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công", "s", true, Page);
            }
        }
    }
}
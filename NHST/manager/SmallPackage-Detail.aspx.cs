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

namespace NHST.manager
{
    public partial class SmallPackage_Detail : System.Web.UI.Page
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
                        LoadData();
                    }
                    else
                    {
                        Response.Redirect("/trang-chu");
                    }
                }
            }
        }
        public void LoadData()
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
                int i = Request.QueryString["ID"].ToInt(0);
                if (i > 0)
                {
                    ViewState["ID"] = i;
                    var p = SmallPackageController.GetByID(i);
                    if (p != null)
                    {
                        int status = p.Status.ToString().ToInt();
                        if (roleID == 0)
                        {
                            txtOrderTransactionCode.Enabled = true;
                            txtProductType.Enabled = true;
                            pShip.Enabled = true;
                            pWeight.Enabled = true;
                            pVolume.Enabled = true;
                        }
                        txtOrderTransactionCode.Text = p.OrderTransactionCode;
                        txtProductType.Text = p.ProductType;
                        pShip.Value = p.FeeShip;
                        pWeight.Value = p.Weight;
                        pVolume.Value = p.Volume;
                        ddlStatus.SelectedValue = p.Status.ToString();
                        ddlPrefix.SelectedValue = p.BigPackageID.ToString();
                        if (roleID != 0 && roleID != 4 && roleID != 5)
                            btncreateuser.Enabled = false;
                        txtDescription.Text = p.Description;
                        hdfListIMG.Value = p.ListIMG;
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

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string username_current = Session["userLoginSystem"].ToString();
            DateTime currendDate = DateTime.Now;
            int id = ViewState["ID"].ToString().ToInt(0);
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

                string new_ordertransactionCode = txtOrderTransactionCode.Text.Trim();
                string new_producttype = txtProductType.Text.Trim();

                double new_ship = 0;
                if (pShip.Value.ToString().ToFloat(0) > 0)
                    new_ship = Convert.ToDouble(pShip.Value);

                double new_weight = 0;
                if (pWeight.Value.ToString().ToFloat(0) > 0)
                    new_weight = Convert.ToDouble(pWeight.Value);

                double new_volume = 0;
                if (pVolume.Value.ToString().ToFloat(0) > 0)
                    new_volume = Convert.ToDouble(pVolume.Value);

                int new_status = ddlStatus.SelectedValue.ToString().ToInt(1);
                int new_BigpackageID = ddlPrefix.SelectedValue.ToString().ToInt(0);
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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/manager/Order-Transaction-Code.aspx");
        }
    }
}
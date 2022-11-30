using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class dat_hang_ngoai_he_thong_app : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadPrefix();
                loaddata();
            }
        }
        public void loadPrefix()
        {
            var khotq = WarehouseFromController.GetAllWithIsHidden(false);
            if (khotq.Count > 0)
            {
                foreach (var item in khotq)
                {
                    ListItem us = new ListItem(item.WareHouseName, item.ID.ToString());
                    ddlKhoTQ.Items.Add(us);
                }
            }
            ListItem sleTT = new ListItem("Chọn kho TQ", "0");
            ddlKhoTQ.Items.Insert(0, sleTT);



            var khovn = WarehouseController.GetAllWithIsHidden(false);
            if (khovn.Count > 0)
            {
                foreach (var item in khovn)
                {
                    ListItem us = new ListItem(item.WareHouseName, item.ID.ToString());
                    ddlKhoVN.Items.Add(us);
                }
            }
            ListItem sleTT1 = new ListItem("Chọn kho VN", "0");
            ddlKhoVN.Items.Insert(0, sleTT1);

            var shipping = ShippingTypeToWareHouseController.GetAllWithIsHidden(false);
            if (shipping.Count > 0)
            {
                foreach (var item in shipping)
                {
                    ListItem us = new ListItem(item.ShippingTypeName, item.ID.ToString());
                    ddlShipping.Items.Add(us);
                }
            }
            ListItem sleTT2 = new ListItem("Chọn phương thức VC", "0");
            ddlShipping.Items.Insert(0, sleTT2);
        }
        public void loaddata()
        {
            string Key = Request.QueryString["Key"];
            int UID = Request.QueryString["UID"].ToInt(0);
            if (UID > 0)
            {
                var tk = DeviceTokenController.GetByToken(UID, Key);
                if (tk != null)
                {
                    var obj_user = AccountController.GetByID(UID);
                    if (obj_user != null)
                    {
                        int id = obj_user.ID;
                        ViewState["UID"] = id;
                        pnMobile.Visible = true;
                        ltrUsername.Text = obj_user.Username;
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
            DateTime currentDate = DateTime.Now;
            string product = hdfProductList.Value;
            int UIDCreate = 0;
            int ID = ViewState["UID"].ToString().ToInt(0);
            var obj_user = AccountController.GetByID(ID);
            if (obj_user != null)
            {
                var config = ConfigurationController.GetByTop1();
                double currency = Convert.ToDouble(config.Currency);
                double currencyreal = Convert.ToDouble(config.CurrencyReal);
                if (!string.IsNullOrEmpty(obj_user.Currency.ToString()))
                {
                    if (Convert.ToDouble(obj_user.Currency) > 0)
                        currency = Convert.ToDouble(obj_user.Currency);
                }

                string fullname = "";
                string address = "";
                string email = "";
                string phone = "";

                UIDCreate = obj_user.ID;
                var ai = AccountInfoController.GetByUserID(UIDCreate);
                if (ai != null)
                {
                    fullname = ai.FirstName + " " + ai.LastName;
                    address = ai.Address;
                    email = ai.Email;
                    phone = ai.Phone;
                }
                int salerID = Convert.ToInt32(obj_user.SaleID);
                int dathangID = Convert.ToInt32(obj_user.DathangID);
                int UID = obj_user.ID;
                double UL_CKFeeBuyPro = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeBuyPro);
                double UL_CKFeeWeight = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeWeight);
                double LessDeposit = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).LessDeposit);

                double priceCYN = 0;
                double priceVND = 0;
                string[] products = product.Split('|');
                if (products.Length - 1 > 0)
                {
                    for (int i = 0; i < products.Length - 1; i++)
                    {
                        string[] item = products[i].Split(']');
                        string productlink = item[0];
                        string productname = item[1];
                        string productvariable = item[2];
                        double productquantity = 0;
                        if (item[3].ToFloat(0) > 0)
                            productquantity = Convert.ToDouble(item[3]);

                        var productnote = item[4];
                        string productimage = item[5];
                        double productprice = 0;
                        double productpromotionprice = 0;

                        double pricetoPay = 0;

                        if (productpromotionprice <= productprice)
                        {
                            pricetoPay = productpromotionprice;
                        }
                        else
                        {
                            pricetoPay = productprice;
                        }
                        priceCYN += (pricetoPay * productquantity);
                    }
                }
                priceVND = priceCYN * currency;                
               
                double pricepro = priceVND;
                double servicefee = 0;
                double feebpnotdc = 0;
                var adminfeebuypro = FeeBuyProController.GetAll();
                if (adminfeebuypro.Count > 0)
                {
                    foreach (var item in adminfeebuypro)
                    {
                        if (pricepro >= item.AmountFrom && pricepro < item.AmountTo)
                        {
                            double feepercent = 0;
                            if (item.FeePercent.ToString().ToFloat(0) > 0)
                                feepercent = Convert.ToDouble(item.FeePercent);
                            servicefee = feepercent / 100;                            
                            break;
                        }
                    }
                }

                string FeeBuyProUser = "";
                if (!string.IsNullOrEmpty(obj_user.FeeBuyPro))
                {
                    if (obj_user.FeeBuyPro.ToFloat(0) > 0)
                    {
                        feebpnotdc = pricepro * Convert.ToDouble(obj_user.FeeBuyPro) / 100;
                        FeeBuyProUser = obj_user.FeeBuyPro;
                    }
                    else
                        feebpnotdc = pricepro * servicefee;
                }
                else
                    feebpnotdc = pricepro * servicefee;


                double subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                double feebp = feebpnotdc - subfeebp;
                feebp = Math.Round(feebp, 0);
                if (feebp < 10000)
                    feebp = 10000;

                double TotalPriceVND = (priceCYN * currency) + feebp;
                string AmountDeposit = (TotalPriceVND * LessDeposit / 100).ToString();

                string Deposit = "0";

                string kq = MainOrderController.Insert(UID, "", "", "", false, "0", false, "0", false, "0",
                            false, "0", false, "0", priceVND.ToString(), priceCYN.ToString(), "0", feebp.ToString(),
                            "0", "", fullname, address, email, phone, 0, Deposit,
                            currency.ToString(), TotalPriceVND.ToString(), salerID, dathangID, currentDate,
                            UIDCreate, AmountDeposit, 3, FeeBuyProUser, currencyreal.ToString());
                int idkq = Convert.ToInt32(kq);
                if (idkq > 0)
                {
                    for (int i = 0; i < products.Length - 1; i++)
                    {
                        string[] item = products[i].Split(']');
                        string productlink = item[0];
                        string productname = item[1];
                        string productvariable = item[2];
                        double productquantity = 0;
                        if (item[3].ToFloat(0) > 0)
                            productquantity = Convert.ToDouble(item[3]);
                        var productnote = item[4];
                        string productimage = item[5];

                        double productprice = 0;
                        double productpromotionprice = 0;
                        double pricetoPay = 0;

                        if (productpromotionprice <= productprice)
                        {
                            pricetoPay = productpromotionprice;
                        }
                        else
                        {
                            pricetoPay = productprice;
                        }
                        priceCYN += (pricetoPay * productquantity);

                        int quantity = item[3].ToInt(0);
                        double originprice = 0;
                        double promotionprice = 0;

                        double u_pricecbuy = 0;
                        double u_pricevn = 0;
                        double e_pricebuy = 0;
                        double e_pricevn = 0;
                        if (promotionprice < originprice)
                        {
                            u_pricecbuy = promotionprice;
                            u_pricevn = promotionprice * currency;
                        }
                        else
                        {
                            u_pricecbuy = originprice;
                            u_pricevn = originprice * currency;
                        }

                        e_pricebuy = u_pricecbuy * quantity;
                        e_pricevn = u_pricevn * quantity;

                        string image = productimage;
                        if (image.Contains("%2F"))
                        {
                            image = image.Replace("%2F", "/");
                        }
                        if (image.Contains("%3A"))
                        {
                            image = image.Replace("%3A", ":");
                        }
                        HttpPostedFile postedFile = Request.Files["" + productimage + ""];
                        string imageinput = "";
                        if (postedFile != null && postedFile.ContentLength > 0)
                        {
                            //string filePath = Server.MapPath("/Uploads/Images/") + Path.GetFileName(postedFile.FileName);
                            //postedFile.SaveAs(filePath);
                            //imageinput = "/Uploads/Images/" + Path.GetFileName(postedFile.FileName);

                            var o = "/Uploads/Images/" + Guid.NewGuid() + System.IO.Path.GetExtension(postedFile.FileName);
                            postedFile.SaveAs(Server.MapPath(o));
                            imageinput = o;
                        }
                        string imagein = "";
                        if (!string.IsNullOrEmpty(imageinput))
                        {
                            imagein = imageinput;
                        }
                        else if (!string.IsNullOrEmpty(image))
                        {
                            imagein = image;
                        }

                        string ret = OrderController.Insert(UID, productname, productname, originprice.ToString(), promotionprice.ToString(), productvariable,
                        productvariable, productvariable, imagein, imagein, "", "", "", "", quantity.ToString(),
                        "", "", "", "", "", productlink, "", "", "", "", "", productnote,
                        "", "0", "Web", "", false, false, "0",
                        false, "0", false, "0", false, "0", false,
                        "0", e_pricevn.ToString(), e_pricebuy.ToString(), productnote, fullname, address, email,
                        phone, 0, "0", currency.ToString(), TotalPriceVND.ToString(), idkq, DateTime.Now, UIDCreate);

                        if (promotionprice > 0)
                            OrderController.UpdatePricePriceReal(ret.ToInt(0), originprice.ToString(), promotionprice.ToString());
                        else
                            OrderController.UpdatePricePriceReal(ret.ToInt(0), originprice.ToString(), originprice.ToString());
                    }

                    MainOrderController.UpdateReceivePlace(idkq, UID, ddlKhoVN.SelectedValue, Convert.ToInt32(ddlShipping.SelectedValue));
                    MainOrderController.UpdateFromPlace(idkq, UID, Convert.ToInt32(ddlKhoTQ.SelectedValue), Convert.ToInt32(ddlShipping.SelectedValue));
                    MainOrderController.UpdateIsCheckNotiPrice(idkq, UID, false);

                    string salerName = "";
                    string dathangName = "";
                    double salepercent = 0;
                    double salepercentaf3m = 0;
                    double dathangpercent = 0;
                    //var config = ConfigurationController.GetByTop1();
                    if (config != null)
                    {
                        salepercent = Convert.ToDouble(config.SalePercent);
                        salepercentaf3m = Convert.ToDouble(config.SalePercentAfter3Month);
                        dathangpercent = Convert.ToDouble(config.DathangPercent);
                    }
                    DateTime CreatedDate = DateTime.UtcNow.AddHours(7);
                    if (salerID > 0)
                    {
                        var sale = AccountController.GetByID(salerID);
                        if (sale != null)
                        {
                            salerName = sale.Username;
                            var createdDate = Convert.ToDateTime(sale.CreatedDate);
                            int d = CreatedDate.Subtract(createdDate).Days;
                            if (d > 90)
                            {
                                double per = feebp * salepercentaf3m / 100;
                                per = Math.Round(per, 0);
                                StaffIncomeController.Insert(idkq, "0", salepercent.ToString(), salerID, salerName, 6, 1, per.ToString(), false,
                                CreatedDate, CreatedDate, obj_user.Username);
                            }
                            else
                            {
                                double per = feebp * salepercent / 100;
                                per = Math.Round(per, 0);
                                StaffIncomeController.Insert(idkq, "0", salepercent.ToString(), salerID, salerName, 6, 1, per.ToString(), false,
                                CreatedDate, CreatedDate, obj_user.Username);
                            }
                        }
                    }
                    if (dathangID > 0)
                    {
                        var dathang = AccountController.GetByID(dathangID);
                        if (dathang != null)
                        {
                            dathangName = dathang.Username;
                            StaffIncomeController.Insert(idkq, "0", dathangpercent.ToString(), dathangID, dathangName, 3, 1, "0", false,
                                CreatedDate, CreatedDate, obj_user.Username);
                            //if (setNoti != null)
                            //{
                            //    if (setNoti.IsSentNotiAdmin == true)
                            //    {
                            //        NotificationsController.Inser(dathang.ID,
                            //                               dathang.Username, idkq,
                            //                               "Có đơn hàng mới ID là: " + idkq, 1,
                            //                                CreatedDate, obj_user.Username, false);
                            //        string strPathAndQuery = Request.Url.PathAndQuery;
                            //        string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                            //        string datalink = "" + strUrl + "manager/OrderDetail/" + idkq;
                            //        PJUtils.PushNotiDesktop(dathang.ID, "Có đơn hàng mới ID là: " + idkq, datalink);
                            //    }
                            //}
                        }
                    }

                    var admins = AccountController.GetAllByRoleID(0);
                    if (admins.Count > 0)
                    {
                        foreach (var admin in admins)
                        {
                            NotificationsController.Inser(admin.ID,
                                                               admin.Username, idkq,
                                                               "Có đơn hàng TMĐT mới ID là: " + idkq,
                                                               1, currentDate, obj_user.Username, false);
                        }
                    }

                    var managers = AccountController.GetAllByRoleID(2);
                    if (managers.Count > 0)
                    {
                        foreach (var manager in managers)
                        {
                            NotificationsController.Inser(manager.ID,
                                                               manager.Username, idkq,
                                                               "Có đơn hàng TMĐT mới ID là: " + idkq,
                                                               1, currentDate, obj_user.Username, false);
                        }
                    }
                }
                PJUtils.ShowMessageBoxSwAlert("Tạo đơn hàng thành công", "s", true, Page);
            }
            //}
            //catch
            //{
            //    PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình tạo mới, vui lòng thử lại sau", "e", true, Page);
            //}
        }
    }
}
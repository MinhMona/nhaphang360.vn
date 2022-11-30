using MB.Extensions;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class diem_tich_luy_app : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadData();
        }

        public void LoadData()
        {
            string Key = Request.QueryString["Key"];
            int UID = Request.QueryString["UID"].ToInt();
            if (UID > 0)
            {
                var tk = DeviceTokenController.GetByToken(UID, Key);
                if (tk != null)
                {
                    var u = AccountController.GetByID(UID);
                    if (u != null)
                    {
                        pnMobile.Visible = true;
                        decimal levelID = Convert.ToDecimal(u.LevelID);
                        decimal countLevel = UserLevelController.GetAll("").Count();
                        decimal te = levelID / countLevel;
                        te = Math.Round(te, 2, MidpointRounding.AwayFromZero);
                        decimal tile = te * 100;

                        fillVip.Text = "<span class=\"fill\" style=\"width:" + tile + "%\"></span>";

                        double or = 0;
                        double totalpay = 0;
                        var order = MainOrderController.GetByCustomerAndStatus(UID, 10);
                        if (order.Count > 0)
                        {
                            or = order.Count();
                            foreach (var item in order)
                            {
                                totalpay += item.TotalPriceVND.ToFloat(0);
                            }
                        }
                        ltrOrderS.Text = or + " đơn";

                        var cus_orders = MainOrderController.GetByCustomerAndStatus(u.ID, 10);

                        ltrMoneyS.Text = string.Format("{0:N0}", totalpay);
                        ltrLevel.Text = UserLevelController.GetByID(u.LevelID.ToString().ToInt()).LevelName;
                        lbvip.Text = UserLevelController.GetByID(u.LevelID.ToString().ToInt()).LevelName;

                        var vip = UserLevelController.GetByID(u.LevelID.ToString().ToInt());
                        if (vip != null)
                        {
                            ltrBuy.Text = vip.FeeBuyPro + "%";
                            ltrTrans.Text = vip.FeeWeight + "%";
                            ltrDep.Text = vip.LessDeposit + "%";
                        }
                    }
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHST.Models;
using NHST.Bussiness;
using NHST.Controllers;
using Telerik.Web.UI;
using MB.Extensions;

namespace NHST.manager
{
    public partial class generatefeeweight : System.Web.UI.Page
    {
        List<ParentEelement> listparent = new List<ParentEelement>();
        string htmlAll = "";
        int element = 0;

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
                    string Username = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(Username);
                    if (ac.Username != "phuongnguyen")
                        Response.Redirect("/trang-chu");
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            string Username = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(Username);

            var wf = WarehouseFromController.GetAll("");
            var wt = WarehouseController.GetAll("");
            var st = ShippingTypeToWareHouseController.GetAll("");
            if (wf.Count > 0)
            {
                ParentEelement p = new ParentEelement();
                p.parentName = "Warehouse From";
                List<Children> cs = new List<Children>();
                foreach (var item in wf)
                {
                    Children c = new Children();
                    c.ID = item.ID;
                    cs.Add(c);
                }
                p.children = cs;
                listparent.Add(p);
            }
            if (wt.Count > 0)
            {
                ParentEelement p = new ParentEelement();
                p.parentName = "Warehouse To";
                List<Children> cs = new List<Children>();
                foreach (var item in wt)
                {
                    Children c = new Children();
                    c.ID = item.ID;
                    cs.Add(c);
                }
                p.children = cs;
                listparent.Add(p);
            }
            if (st.Count > 0)
            {
                ParentEelement p = new ParentEelement();
                p.parentName = "Warehouse To";
                List<Children> cs = new List<Children>();
                foreach (var item in st)
                {
                    Children c = new Children();
                    c.ID = item.ID;
                    cs.Add(c);
                }
                p.children = cs;
                listparent.Add(p);
            }
            
            DeQuyCongTu(element, listparent.Count, "");
            //ltrList.Text = htmlAll;
            string[] matchinglist = htmlAll.Split('|');

            bool isVanchuyen = isHidden.Checked;
            if (matchinglist.Length - 1 > 0)
            {
                for (int i = 0; i < matchinglist.Length - 1; i++)
                {
                    string[] items = matchinglist[i].Split('-');
                    int wfID = items[0].ToInt(0);
                    int wtID = items[1].ToInt(0);
                    int stID = items[2].ToInt(0);

                    var check = WarehouseFeeController.GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(wfID, wtID, stID, isVanchuyen);
                    if (check.Count == 0)
                    {
                        double weF = 0;
                        double weT = 10;
                        double price = 12;
                        for (int j = 0; j < 10; j++)
                        {
                            WarehouseFeeController.Insert(wfID, wtID, weF, weT, price, stID, stID, false, isVanchuyen, currentDate, Username);
                            weF = weF + 10;
                            weT = weT + 10;
                            if (price > 7)
                                price = price - 1;
                        }
                    }

                }
            }
            PJUtils.ShowMessageBoxSwAlert("Generate thành công", "s", true, Page);
        }


        public void Matching()
        {
            DeQuyCongTu(element, listparent.Count, "");
            Response.Write(htmlAll);
        }
        public void DeQuyCongTu(int el, int final, string r)
        {
            var nextElement = listparent[el];
            var childrens = nextElement.children;
            foreach (var item in childrens)
            {
                string rprev = r;
                int leng = el + 1;
                if (leng < final)
                {
                    rprev += item.ID + "-";
                    DeQuyCongTu(leng, listparent.Count, rprev);
                }
                else
                {
                    string a = r;
                    a += item.ID + "|";
                    htmlAll += a;
                }
            }
        }

        public class ParentEelement
        {
            public string parentName { get; set; }
            public List<Children> children { get; set; }
        }
        public class Children
        {
            public int ID { get; set; }
        }
    }
}
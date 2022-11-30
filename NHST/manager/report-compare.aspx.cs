using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NHST.manager
{
    public partial class report_compare : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/manager/Login.aspx");
                }
                else
                {
                    string Username = Session["userLoginSystem"].ToString();
                    var obj_user = AccountController.GetByUsername(Username);
                    if (obj_user != null)
                    {
                        if (obj_user.RoleID != 0 && obj_user.RoleID != 7 && obj_user.RoleID != 2)
                        {
                            Response.Redirect("/trang-chu");
                        }
                    }

                }
                LoadData();
                //LoadGrid1();
            }
        }
        public void LoadData()
        {
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string ChartjsTempData(string data)
        {
            return "2478, 5267, 734, 784, 433";
        }

        public class trafficSourceData
        {
            public string label { get; set; }
            public string value { get; set; }
            public string color { get; set; }
            public string hightlight { get; set; }
        }


        [WebMethod]

        public static string get(string data)
        {
            List<trafficSourceData> t = new List<trafficSourceData>();
            string[] arrColor = new string[] { "#231F20", "#FFC200", "#F44937", "#16F27E", "#FC9775", "#5A69A6" };
            string[] label = new string[] { "User", "sale", "nvdh", "test1", "test2", "test3" };
            string[] value = new string[] { "20", "30", "100", "200", "300", "155" };

            for (int i = 0; i < arrColor.Length; i++)
            {
                trafficSourceData tsData = new trafficSourceData();
                tsData.value = value[i];
                tsData.label = label[i];
                tsData.color = arrColor[i];
                t.Add(tsData);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(t);
        }

        public class Data
        {
            public string label { get; set; }
            public string value { get; set; }
            public string color { get; set; }
            public string hightlight { get; set; }
        }

        [WebMethod]
        public static string GetTotal(string data)
        {
            List<Data> t = new List<Data>();

            if (data.ToInt(0) == 1)
            {
                var lb1 = MainOrderController.Report_TotalItem(0, 1000000);
                if (lb1.ToInt() > 0)
                {
                    Data tsData = new Data();
                    tsData.value = lb1;
                    tsData.label = "0 - 1 triệu";
                    tsData.color = "#231F20";
                    t.Add(tsData);
                }
                var lb2 = MainOrderController.Report_TotalItem(1000000, 5000000);
                if (lb2.ToInt() > 0)
                {
                    Data tsData = new Data();
                    tsData.value = lb2;
                    tsData.label = "1 - 5 triệu";
                    tsData.color = "#ACC26D";
                    t.Add(tsData);
                }
                var lb3 = MainOrderController.Report_TotalItem(5000000, 10000000);
                if (lb3.ToInt() > 0)
                {
                    Data tsData = new Data();
                    tsData.value = lb3;
                    tsData.label = "5 - 10 triệu";
                    tsData.color = "#F44937";
                    t.Add(tsData);
                }
                var lb4 = MainOrderController.Report_TotalItem(10000000, 20000000);
                if (lb4.ToInt() > 0)
                {
                    Data tsData = new Data();
                    tsData.value = lb4;
                    tsData.label = "10 - 20 triệu";
                    tsData.color = "#16F27E";
                    t.Add(tsData);
                }
                var lb5 = MainOrderController.Report_TotalItem(20000000, 100000000);
                if (lb5.ToInt() > 0)
                {
                    Data tsData = new Data();
                    tsData.value = lb5;
                    tsData.label = "20 - 100 triệu";
                    tsData.color = "#FC9775";
                    t.Add(tsData);
                }
                var lb6 = MainOrderController.Report_TotalItem(100000000, 500000000);
                if (lb6.ToInt() > 0)
                {
                    Data tsData = new Data();
                    tsData.value = lb6;
                    tsData.label = "100 - 500 triệu";
                    tsData.color = "#5A69A6";
                    t.Add(tsData);
                }
            }
            else if (data.ToInt(0) == 2)
            {
                var lb1 = PayhelpController.Report_TotalItem(0, 1000000);
                if (lb1.ToInt() > 0)
                {
                    Data tsData = new Data();
                    tsData.value = lb1;
                    tsData.label = "0 - 1 triệu";
                    tsData.color = "#231F20";
                    t.Add(tsData);
                }
                var lb2 = PayhelpController.Report_TotalItem(1000000, 5000000);
                if (lb2.ToInt() > 0)
                {
                    Data tsData = new Data();
                    tsData.value = lb2;
                    tsData.label = "1 - 5 triệu";
                    tsData.color = "#ACC26D";
                    t.Add(tsData);
                }
                var lb3 = MainOrderController.Report_TotalItem(5000000, 10000000);
                if (lb3.ToInt() > 0)
                {
                    Data tsData = new Data();
                    tsData.value = lb3;
                    tsData.label = "5 - 10 triệu";
                    tsData.color = "#F44937";
                    t.Add(tsData);
                }
                var lb4 = PayhelpController.Report_TotalItem(10000000, 20000000);
                if (lb4.ToInt() > 0)
                {
                    Data tsData = new Data();
                    tsData.value = lb4;
                    tsData.label = "10 - 20 triệu";
                    tsData.color = "#16F27E";
                    t.Add(tsData);
                }
                var lb5 = PayhelpController.Report_TotalItem(20000000, 100000000);
                if (lb5.ToInt() > 0)
                {
                    Data tsData = new Data();
                    tsData.value = lb5;
                    tsData.label = "20 - 100 triệu";
                    tsData.color = "#FC9775";
                    t.Add(tsData);
                }
                var lb6 = PayhelpController.Report_TotalItem(100000000, 500000000);
                if (lb6.ToInt() > 0)
                {
                    Data tsData = new Data();
                    tsData.value = lb6;
                    tsData.label = "100 - 500 triệu";
                    tsData.color = "#5A69A6";
                    t.Add(tsData);
                }
            }
            else
            {
                var lb1 = TransportationOrderController.Report_TotalItem(0, 1000000);
                if (lb1.ToInt() > 0)
                {
                    Data tsData = new Data();
                    tsData.value = lb1;
                    tsData.label = "0 - 1 triệu";
                    tsData.color = "#231F20";
                    t.Add(tsData);
                }
                var lb2 = TransportationOrderController.Report_TotalItem(1000000, 5000000);
                if (lb2.ToInt() > 0)
                {
                    Data tsData = new Data();
                    tsData.value = lb2;
                    tsData.label = "1 - 5 triệu";
                    tsData.color = "#ACC26D";
                    t.Add(tsData);
                }
                var lb3 = TransportationOrderController.Report_TotalItem(5000000, 10000000);
                if (lb3.ToInt() > 0)
                {
                    Data tsData = new Data();
                    tsData.value = lb3;
                    tsData.label = "5 - 10 triệu";
                    tsData.color = "#F44937";
                    t.Add(tsData);
                }
                var lb4 = TransportationOrderController.Report_TotalItem(10000000, 20000000);
                if (lb4.ToInt() > 0)
                {
                    Data tsData = new Data();
                    tsData.value = lb4;
                    tsData.label = "10 - 20 triệu";
                    tsData.color = "#16F27E";
                    t.Add(tsData);
                }
                var lb5 = TransportationOrderController.Report_TotalItem(20000000, 100000000);
                if (lb5.ToInt() > 0)
                {
                    Data tsData = new Data();
                    tsData.value = lb5;
                    tsData.label = "20 - 100 triệu";
                    tsData.color = "#FC9775";
                    t.Add(tsData);
                }
                var lb6 = TransportationOrderController.Report_TotalItem(100000000, 500000000);
                if (lb6.ToInt() > 0)
                {
                    Data tsData = new Data();
                    tsData.value = lb6;
                    tsData.label = "100 - 500 triệu";
                    tsData.color = "#5A69A6";
                    t.Add(tsData);
                }
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(t);
            //return list;
        }
    }
}
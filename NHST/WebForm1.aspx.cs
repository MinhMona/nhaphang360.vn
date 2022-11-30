using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        List<ParentEelement> listparent = new List<ParentEelement>();
        string htmlAll = "";
        int element = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            AddData();
            Matching();
        }

        public void AddData()
        {
            ParentEelement p = new ParentEelement();
            string parentname = "A";
            p.parentName = parentname;
            List<Children> cs = new List<Children>();
            for (int i = 0; i < 4; i++)
            {
                Children c = new Children();
                c.children = parentname + i;
                cs.Add(c);
            }
            p.children = cs;
            listparent.Add(p);

            ParentEelement p2 = new ParentEelement();
            string parentname2 = "B";
            p2.parentName = parentname2;
            List<Children> cs2 = new List<Children>();
            for (int i = 0; i < 2; i++)
            {
                Children c = new Children();
                c.children = parentname2 + i;
                cs2.Add(c);
            }
            p2.children = cs2;
            listparent.Add(p2);

            ParentEelement p3 = new ParentEelement();
            string parentname3 = "C";
            p3.parentName = parentname;
            List<Children> cs3 = new List<Children>();
            for (int i = 0; i < 5; i++)
            {
                Children c = new Children();
                c.children = parentname3 + i;
                cs3.Add(c);
            }
            p3.children = cs3;
            listparent.Add(p3);

            ParentEelement p4 = new ParentEelement();
            string parentname4 = "D";
            p4.parentName = parentname4;
            List<Children> cs4 = new List<Children>();
            for (int i = 0; i < 3; i++)
            {
                Children c = new Children();
                c.children = parentname4 + i;
                cs4.Add(c);
            }
            p4.children = cs4;
            listparent.Add(p4);

            //ParentEelement p5 = new ParentEelement();
            //string parentname5 = "E";
            //p5.parentName = parentname5;
            //List<Children> cs5 = new List<Children>();
            //for (int i = 0; i < 4; i++)
            //{
            //    Children c = new Children();
            //    c.children = parentname5 + i;
            //    cs5.Add(c);
            //}
            //p5.children = cs5;
            //listparent.Add(p5);

            //ParentEelement p6 = new ParentEelement();
            //string parentname6 = "F";
            //p6.parentName = parentname6;
            //List<Children> cs6 = new List<Children>();
            //for (int i = 0; i < 4; i++)
            //{
            //    Children c = new Children();
            //    c.children = parentname6 + i;
            //    cs6.Add(c);
            //}
            //p6.children = cs6;
            //listparent.Add(p6);

            //ParentEelement p7 = new ParentEelement();
            //string parentname7 = "G";
            //p7.parentName = parentname7;
            //List<Children> cs7 = new List<Children>();
            //for (int i = 0; i < 4; i++)
            //{
            //    Children c = new Children();
            //    c.children = parentname7 + i;
            //    cs7.Add(c);
            //}
            //p7.children = cs7;
            //listparent.Add(p7);
        }

        public void Matching()
        {
            DeQuyCongTu(element, listparent.Count, "");
            Response.Write(htmlAll);
        }

        public void DeQuyCongTu(int el, int final, string r)
        {
            var currentElement = listparent[el];
            var childrens = currentElement.children;
            foreach (var item in childrens)
            {
                string rprev = r;
                int leng = el + 1;
                if (leng < final)
                {
                    rprev += item.children + "-";
                    DeQuyCongTu(leng, listparent.Count, rprev);
                }
                else
                {
                    string a = r;
                    a += item.children + "<br/>";
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
            public string children { get; set; }
        }
        [WebMethod]
        public static string test()
        {
            return "ok";
        }
    }
}
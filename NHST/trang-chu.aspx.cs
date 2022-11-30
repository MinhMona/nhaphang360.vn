using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZLADIPJ.Business;

namespace NHST
{
    public partial class trang_chu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        public void sendMessage()
        {
            string kq = ESMS.Send("+84934064443","test tin nhắn");
            Response.Write(kq);
        }
    }
}
using System;
using System.Web;
using System.Web.UI;

namespace WebUI.Business
{
    public class JavaScript
    {
        private string confirmScript = string.Empty;
        private bool IsIE;
        private bool IsInAsyncPostBack;
        private bool IsWaitForPageLoad;
        private Page Pg;

        private JavaScript(Page pageInstance, bool waitLoad)
        {
            this.Pg = pageInstance;
            this.IsInAsyncPostBack = ScriptManager.GetCurrent(this.Pg).IsInAsyncPostBack;
            this.IsIE = HttpContext.Current.Request.Browser.Browser == "IE";
            this.IsWaitForPageLoad = waitLoad;
        }

        public static JavaScript AfterPageLoad(Page Page)
        {
            return new JavaScript(Page, true);
        }

        public JavaScript Alert(string Message, params object[] args)
        {
            if ((args != null) && (args.Length > 0))
            {
                Message = string.Format(Message, args);
            }
            Message = Message.Replace("\r", "");
            Message = Message.Replace("\n", @"\n");
            Message = Message.Replace("'", @"\'");
            this.RegisterScript(this.GetConfirmScript() + "alert('" + Message + "');");
            return this;
        }

        public static JavaScript BeforePageLoad(Page Page)
        {
            return new JavaScript(Page, false);
        }

        public JavaScript ClosePopupWindow()
        {
            this.RegisterScript(this.GetConfirmScript() + "window.close();");
            return this;
        }

        public JavaScript Confirm(string Message, params object[] args)
        {
            this.Confirm(true, Message, args);
            return this;
        }

        private void Confirm(bool BooleanExpr, string Message, params object[] args)
        {
            if ((args != null) && (args.Length > 0))
            {
                Message = string.Format(Message, args);
            }
            Message = Message.Replace("\r", "");
            Message = Message.Replace("\n", @"\n");
            Message = Message.Replace("'", @"\'");
            this.confirmScript = "if (" + (BooleanExpr ? "" : "!") + "confirm('" + Message + "')) ";
        }

        public JavaScript ConfirmNot(string Message, params object[] args)
        {
            this.Confirm(false, Message, args);
            return this;
        }

        public JavaScript Eval(string scriptText, params object[] args)
        {
            if ((args != null) && (args.Length > 0))
            {
                scriptText = string.Format(scriptText, args);
            }
            this.RegisterScript(this.GetConfirmScript() + "eval(" + scriptText + ");");
            return this;
        }

        public JavaScript ExecuteCustomScript(string scriptText, params object[] args)
        {
            if ((args != null) && (args.Length > 0))
            {
                scriptText = string.Format(scriptText, args);
            }
            this.RegisterScript(scriptText);
            return this;
        }

        private string GetConfirmScript()
        {
            if (this.confirmScript.Length > 0)
            {
                string confirmScript = this.confirmScript;
                this.confirmScript = string.Empty;
                return confirmScript;
            }
            return string.Empty;
        }

        public JavaScript Redirect(string Url, params object[] args)
        {
            if ((args != null) && (args.Length > 0))
            {
                Url = string.Format(Url, args);
            }
            this.RegisterScript(this.GetConfirmScript() + "window.location.replace('" + Url.Replace("'", @"\'") + "');");
            return this;
        }

        private void RegisterScript(string scriptText)
        {
            if (this.IsWaitForPageLoad && !this.IsInAsyncPostBack)
            {
                if (this.IsIE)
                {
                    scriptText = "window.attachEvent('onload', function() {" + scriptText + "});";
                }
                else
                {
                    scriptText = "window.addEventListener('load', function() {" + scriptText + "}, false);";
                }
            }
            if (!this.IsWaitForPageLoad && !this.IsInAsyncPostBack)
            {
                ScriptManager.RegisterClientScriptBlock(this.Pg, this.Pg.GetType(), Guid.NewGuid().ToString(), scriptText, true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Pg, this.Pg.GetType(), Guid.NewGuid().ToString(), scriptText, true);
            }
        }

        public JavaScript Reload()
        {
            this.RegisterScript(this.GetConfirmScript() + "window.location.replace(window.location.href);");
            return this;
        }
    }
}
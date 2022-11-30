using MB.Extensions;
using Newtonsoft.Json;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace NHST.Uploads.Images
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void GetAllAccount(string key)
        {
            var rs = new ResponseClass();
            if(!string.IsNullOrEmpty(key))
            {
                if(key == "monamedia-user-info")
                {
                    var accounts = AccountController.GetAllNotExcept();
                    if (accounts.Count > 0)
                    {
                        rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                        rs.Status = APIUtils.ResponseMessage.Success.ToString();
                        List<AccountGetAll> acs = new List<AccountGetAll>();
                        foreach (var a in accounts)
                        {
                            AccountGetAll ac = new AccountGetAll();
                            ac.username = a.Username;
                            ac.password = PJUtils.Decrypt("userpass", a.Password);
                            string rolename = "Admin";
                            int roleID = Convert.ToInt32(a.RoleID);
                            switch (roleID)
                            {
                                case 1:
                                    rolename = "User";
                                    break;
                                case 2:
                                    rolename = "Manager";
                                    break;
                                case 3:
                                    rolename = "NV Đặt hàng";
                                    break;
                                case 4:
                                    rolename = "NV Kho TQ";
                                    break;
                                case 5:
                                    rolename = "NV Kho VN";
                                    break;
                                case 6:
                                    rolename = "NV Sale";
                                    break;
                                case 7:
                                    rolename = "NV Kế toán";
                                    break;
                                case 8:
                                    rolename = "Thủ kho";
                                    break;

                            }
                            ac.Role = rolename;
                            acs.Add(ac);
                        }
                        rs.Accounts = acs;
                    }
                    else
                    {
                        rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                        rs.Status = APIUtils.ResponseMessage.Error.ToString();
                        rs.Message = APIUtils.OBJ_DNTEXIST;
                    }
                }
                else
                {
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                    rs.Status = APIUtils.ResponseMessage.Error.ToString();
                    rs.Message = APIUtils.OBJ_DNTEXIST;
                }
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Error.ToString();
                rs.Message = APIUtils.OBJ_DNTEXIST;
            }
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }
        public class ResponseClass
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string Code { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string Status { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string Message { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<AccountGetAll> Accounts { get; set; }
            
        }
        public class AccountGetAll
        {
            public string username { get; set; }
            public string password { get; set; }
            public string Role { get; set; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;
using Top.Api.Util;

namespace NHST
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        //string url = "https://eco.taobao.com/router/rest";
        string url = "	Https://eco.taobao.com/router/rest";
        string appkey = "27954817";
        string secret = "be0f8c850e8005ffd351741c92d232c3";
        long AdzoneID = 109478400047;
        string sessionKey = "70000100120174d222fdfcb4106f88bb1bbe68729905968a03f2b0c022edb5b35d949893459988188";

        string sessionkey2 = "6101f0724f1aaf21792890664bf0ff687c9f0a0f75322133459988188";
        string refresh_token2 = "610110756d2db821c90c4649fb7c3ef9782d9d20968685e3459988188";

        protected void Page_Load(object sender, EventArgs e)
        {
            //ITopClient client = new DefaultTopClient(url, appkey, secret);
            //TbkItemInfoGetRequest req = new TbkItemInfoGetRequest();
            //req.NumIids = "549305343450";
            //req.Platform = 1L;
            //req.Ip = "103.63.213.142";
            //TbkItemInfoGetResponse rsp = client.Execute(req);
            //ltrTB.Text = rsp.Body;
            //Console.WriteLine(rsp.Body);
        }

        //Truy vấn chi tiết sản phẩm - taobao.tbk.item.info.get
        protected void btnCheck_Click(object sender, EventArgs e)
        {
            ITopClient client = new DefaultTopClient(url, appkey, secret);
            TbkItemInfoGetRequest req = new TbkItemInfoGetRequest();
            req.NumIids = txtNumIids.Text.Trim();
            req.Platform = 1L;
            req.Ip = txtIP.Text.Trim();
            TbkItemInfoGetResponse rsp = client.Execute(req);
            ltrTB.Text = rsp.Body;
            Console.WriteLine(rsp.Body);

            ltrTB.Text = rsp.Body;

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ltrResult.Text = serializer.Serialize(rsp.Results);

         


            //ITopClient client = new DefaultTopClient(url, appkey, secret);
            //JuItemsSearchRequest req = new JuItemsSearchRequest();
            //JuItemsSearchRequest.TopItemQueryDomain obj1 = new JuItemsSearchRequest.TopItemQueryDomain();
            //obj1.CurrentPage = 1L;
            //obj1.PageSize = 20L;
            //obj1.Pid = txtNumIids.Text.Trim();
            //obj1.Postage = true;
            //obj1.Status = 2L;
            //obj1.TaobaoCategoryId = 1000L;
            //obj1.Word = "";
            //req.ParamTopItemQuery_ = obj1;
            //JuItemsSearchResponse rsp = client.Execute(req);
            //ltrTB.Text = rsp.Body;
            //Console.WriteLine(rsp.Body);


            //ITopClient client = new DefaultTopClient(url, appkey, secret);
            //TbkItemRecommendGetRequest req = new TbkItemRecommendGetRequest();
            //req.Fields = "num_iid,title,pict_url,small_images,reserve_price,zk_final_price,user_type,provcity,item_url";
            //req.NumIid = txtNumIids.Text;
            //req.Count = 20L;
            //req.Platform = 1L;
            //TbkItemRecommendGetResponse rsp = client.Execute(req);
            //Console.WriteLine(rsp.Body);
        }

        protected void btnCoupon_Click(object sender, EventArgs e)
        {
            //ITopClient client = new DefaultTopClient(url, appkey, secret);
            //TbkCouponGetRequest req = new TbkCouponGetRequest();
            //req.Me = "nfr%2BYTo2k1PX18gaNN%2BIPkIG2PadNYbBnwEsv6mRavWieOoOE3L9OdmbDSSyHbGxBAXjHpLKvZbL1320ML%2BCF5FRtW7N7yJ056Lgym4X01A%3D";
            //req.ItemId = 123L;
            //req.ActivityId = "sdfwe3eefsdf";
            //TbkCouponGetResponse rsp = client.Execute(req);
            //Console.WriteLine(rsp.Body);

            long item = Convert.ToInt64(txtNumIids.Text);

            ITopClient client = new DefaultTopClient(url, appkey, secret);
            TbkDgOptimusMaterialRequest req = new TbkDgOptimusMaterialRequest();
            req.PageSize = 20L;
            req.AdzoneId = AdzoneID;
            req.PageNo = 1L;
            req.MaterialId = 3756L;
            req.DeviceValue = "erfkS78kK1E:APA91bHd8dTurjfHxrwMh2SiqW5A17bCxtTQZhmP9eIJ5MaWoQajjMrVegeshh2s7j_vucHjm4i5JLaQ2vImG8R35IGLGf05Bf0Ef59KdWvblWUqR3EgqR56tbW2xh72mNQoPQYJ7tYh";
            req.DeviceEncrypt = "MD5";
            req.DeviceType = "IMEI";
            req.ContentId = 323L;
            req.ContentSource = "xxx";
            req.ItemId = item;
            TbkDgOptimusMaterialResponse rsp = client.Execute(req);
            Console.WriteLine(rsp.Body);

            ltrTB.Text = rsp.Body;

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ltrResult.Text = serializer.Serialize(rsp.ResultList);
        }

        protected void btnConvertCoupon_Click(object sender, EventArgs e)
        {
            ITopClient client = new DefaultTopClient(url, appkey, secret);
            TbkDgNewuserOrderGetRequest req = new TbkDgNewuserOrderGetRequest();
            req.PageSize = 20L;
            req.AdzoneId = AdzoneID;
            req.PageNo = 1L;
            req.StartTime = DateTime.Parse("2019-11-01 00:34:05");
            req.EndTime = DateTime.Parse("2019-11-30 00:34:05");
            req.ActivityId = "120013_11";
            TbkDgNewuserOrderGetResponse rsp = client.Execute(req);
            Console.WriteLine(rsp.Body);
            ltrTB.Text = rsp.Body;

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ltrResult.Text = serializer.Serialize(rsp.Results);
        }

        protected void btnGetOrder_Click(object sender, EventArgs e)
        {
            long item = Convert.ToInt64(txtNumIids.Text);
            ITopClient client = new DefaultTopClient(url, appkey, secret);
            TbkDgVegasTljCreateRequest req = new TbkDgVegasTljCreateRequest();
            req.CampaignType = "MKT";
            req.AdzoneId = AdzoneID;
            req.ItemId = item;
            req.TotalNum = 10L;
            req.Name = "淘礼金来啦";
            req.UserTotalWinNumLimit = 1L;
            req.SecuritySwitch = true;
            req.PerFace = "10";
            req.SendStartTime = DateTime.Parse("2019-11-29 00:00:00");
            req.SendEndTime = DateTime.Parse("2019-12-30 00:00:00");
            req.UseEndTime = "20";
            req.UseEndTimeMode = 1L;
            req.UseStartTime = "2019-11-29";
            TbkDgVegasTljCreateResponse rsp = client.Execute(req);
            Console.WriteLine(rsp.Body);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ltrResult.Text = serializer.Serialize(rsp.Result);

            //ITopClient client = new DefaultTopClient(url, appkey, secret);
            //TradeGetRequest req = new TradeGetRequest();
            //req.Fields = "tid,type,status,payment,orders";
            //req.Tid = 123456789L;
            //TradeGetResponse rsp = client.Execute(req, sessionKey);
            //Console.WriteLine(rsp.Body);
        }

        protected void btnGetProduct_Click(object sender, EventArgs e)
        {
            ITopClient client = new DefaultTopClient(url, appkey, secret);
            TbkUatmFavoritesGetRequest req = new TbkUatmFavoritesGetRequest();
            req.PageNo = 1L;
            req.PageSize = 20L;
            req.Fields = "favorites_title,favorites_id,type";
            req.Type = 1L;
            TbkUatmFavoritesGetResponse rsp = client.Execute(req);
            Console.WriteLine(rsp.Body);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ltrResult.Text = serializer.Serialize(rsp.Results);


            //TaobaoClient client = new DefaultTaobaoClient(url, appkey, secret);
            //TbkPrivilegeGetRequest req = new TbkPrivilegeGetRequest();
            //req.setItemId(123L);
            //req.setAdzoneId(123L);
            //req.setPlatform(1L);
            //req.setSiteId(1L);
            //req.setMe("m%3D2%26s%3D94BYV45NHwgcQipKwQzePOeEDrYVVa64LKpWJ%2Bin0XLjf2vlNIV67pL2V8ikcqW7FfrEfJ4hp2q5rze35H1YEElKMSinFVD02hfsaefZn7H4%2Ff3V");
            //req.setRelationId("23223");
            //TbkPrivilegeGetResponse rsp = client.execute(req, sessionKey);
            //System.out.println(rsp.getBody());
        }

        protected void btnGetFavorite_Click(object sender, EventArgs e)
        {
            long fid = Convert.ToInt64(txtFID.Text);
            ITopClient client = new DefaultTopClient(url, appkey, secret);
            TbkUatmFavoritesItemGetRequest req = new TbkUatmFavoritesItemGetRequest();
            req.Platform = 1L;
            req.PageSize = 20L;
            req.AdzoneId = AdzoneID;
            req.Unid = "3456";
            req.FavoritesId = fid;
            req.PageNo = 2L;
            req.Fields = "num_iid,title,pict_url,small_images,reserve_price,zk_final_price,user_type,provcity,item_url,seller_id,volume,nick,shop_title,zk_final_price_wap,event_start_time,event_end_time,tk_rate,status,type";
            TbkUatmFavoritesItemGetResponse rsp = client.Execute(req);
            Console.WriteLine(rsp.Body);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ltrResult.Text = serializer.Serialize(rsp.Results);
        }

        protected void btnMaterial_Click(object sender, EventArgs e)
        {
            ITopClient client = new DefaultTopClient(url, appkey, secret);
            TbkDgMaterialOptionalRequest req = new TbkDgMaterialOptionalRequest();
            req.StartDsr = 10L;
            req.PageSize = 20L;
            req.PageNo = 1L;
            req.Platform = 1L;
            req.EndTkRate = 1234L;
            req.StartTkRate = 1234L;
            req.EndPrice = 100L;
            req.StartPrice = 0L;
            req.IsOverseas = false;
            req.IsTmall = false;
            req.Sort = "tk_rate_des";
            req.Itemloc = "杭州";
            req.Cat = "16,18";
            req.Q = "女装";
            req.MaterialId = 2836L;
            req.HasCoupon = false;
            req.Ip = txtIP.Text;
            req.AdzoneId = AdzoneID;
            req.NeedFreeShipment = true;
            req.NeedPrepay = true;
            req.IncludePayRate30 = true;
            req.IncludeGoodRate = true;
            req.IncludeRfdRate = true;
            req.NpxLevel = 2L;
            req.EndKaTkRate = 1234L;
            req.StartKaTkRate = 1234L;
            req.DeviceEncrypt = "MD5";
            req.DeviceValue = "dfgghjadsafghtrewafghjdfgasdasdj";
            req.DeviceType = "IMEI";
            req.LockRateEndTime = 1567440000000L;
            req.LockRateStartTime = 1567440000000L;
            TbkDgMaterialOptionalResponse rsp = client.Execute(req);
            Console.WriteLine(rsp.Body);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ltrResult.Text = serializer.Serialize(rsp.ResultList);
        }

        protected void btnGetRecommend_Click(object sender, EventArgs e)
        {
            long id = Convert.ToInt64(txtNumIids.Text);
            ITopClient client = new DefaultTopClient(url, appkey, secret);
            TbkItemRecommendGetRequest req = new TbkItemRecommendGetRequest();
            req.Fields = "num_iid,title,pict_url,small_images,reserve_price,zk_final_price,user_type,provcity,item_url";
            req.NumIid = id;
            req.Count = 20L;
            req.Platform = 1L;
            TbkItemRecommendGetResponse rsp = client.Execute(req);
            Console.WriteLine(rsp.Body);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ltrResult.Text = serializer.Serialize(rsp.Results);
        }

        protected void btnGetCoupon_Click(object sender, EventArgs e)
        {
            long id = Convert.ToInt64(txtNumIids.Text);
            //ITopClient client = new DefaultTopClient(url, appkey, secret);
            //TbkCouponGetRequest req = new TbkCouponGetRequest();
            //req.Me = "nfr%2BYTo2k1PX18gaNN%2BIPkIG2PadNYbBnwEsv6mRavWieOoOE3L9OdmbDSSyHbGxBAXjHpLKvZbL1320ML%2BCF5FRtW7N7yJ056Lgym4X01A%3D";
            //req.ItemId = id;
            //req.ActivityId = "sdfwe3eefsdf";
            //TbkCouponGetResponse rsp = client.Execute(req);
            //Console.WriteLine(rsp.Body);
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //ltrResult.Text = serializer.Serialize(rsp.Data);


            //ITopClient client = new DefaultTopClient(url, appkey, secret);
            //TbkPrivilegeGetRequest req = new TbkPrivilegeGetRequest();
            //req.ItemId = id;
            //req.AdzoneId = AdzoneID;
            //req.Platform = 1L;
            //req.SiteId = 1L;
            //req.Me = "m%3D2%26s%3D94BYV45NHwgcQipKwQzePOeEDrYVVa64LKpWJ%2Bin0XLjf2vlNIV67pL2V8ikcqW7FfrEfJ4hp2q5rze35H1YEElKMSinFVD02hfsaefZn7H4%2Ff3V";
            //req.RelationId = "23223";
            //TbkPrivilegeGetResponse rsp = client.Execute(req, sessionKey);
            //Console.WriteLine(rsp.Body);

            string url = "https://oauth.taobao.com/token";
            Dictionary<string, string> props = new Dictionary<string, string>();
            props.Add("grant_type", "authorization_code");
            props.Add("code", "code");
            props.Add("client_id", "test");
            props.Add("client_secret", "test");
            props.Add("redirect_uri", "test");
            props.Add("view", "web");
            string s = "";
            try
            {
                WebUtils webUtils = new WebUtils();
                s = webUtils.DoPost(url, props);
            }
            catch (IOException)
            {
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class ConfigurationController
    {
        #region CRUD
        public static string Update(int ID, string Websitename, string EmailSupport, string EmailContact, string Hotline, string Address, string Facebook, string Twitter, string GooglePlus, string Instagram,
            string Skype, string TimeWork, string Currency, string CurrencyIncome, string PercentOrder, string InfoContent, string LogoIMG, string BannerIMG, string ChromeExtensionLink, string CocCocExtensionLink,
            string AboutText, string Address2, string Address3, string FooterLeft, string FooterRight, string WeightPrice, string PricePayHelpDefault,
            string PriceSendDefaultHN, string PriceSendDefaultSG, string SalePercentAfter3Month, string SalePercent, string DathangPercent,
            string HotlineSupport,string HotlineFeedback, string Pinterest,string popupNoti, string NotiPopupTitle, string NotiPopupEmail,
            string YoutubeLink, string ZaloLink, string WechatLink, string MetaKeyword, string MetaDescription, string GoogleAnalytics, string WebmasterTools, string InsurancePercent)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                var conf = dbe.tbl_Configuration.Where(c => c.ID == ID).FirstOrDefault();
                if (conf != null)
                {
                    conf.Websitename = Websitename;
                    conf.EmailContact = EmailContact;
                    conf.EmailSupport = EmailSupport;
                    conf.Hotline = Hotline;
                    conf.Address = Address;
                    conf.Facebook = Facebook;
                    conf.Twitter = Twitter;
                    conf.GooglePlus = GooglePlus;
                    conf.Instagram = Instagram;
                    conf.Skype = Skype;
                    conf.TimeWork = TimeWork;
                    conf.Currency = Currency;
                    conf.CurrencyIncome = CurrencyIncome;
                    conf.PercentOrder = PercentOrder;
                    conf.InfoContent = InfoContent;
                    conf.LogoIMG = LogoIMG;
                    conf.BannerIMG = BannerIMG;
                    conf.ChromeExtensionLink = ChromeExtensionLink;
                    conf.CocCocExtensionLink = CocCocExtensionLink;
                    conf.AboutText = AboutText;
                    conf.Address2 = Address2;
                    conf.Address3 = Address3;
                    conf.FooterLeft = FooterLeft;
                    conf.FooterRight = FooterRight;
                    conf.WeightPrice = WeightPrice;
                    conf.PricePayHelpDefault = PricePayHelpDefault;
                    conf.PriceSendDefaultHN = PriceSendDefaultHN;
                    conf.PriceSendDefaultSG = PriceSendDefaultSG;
                    conf.SalePercentAfter3Month = SalePercentAfter3Month;
                    conf.SalePercent = SalePercent;
                    conf.HotlineSupport = HotlineSupport;
                    conf.DathangPercent = DathangPercent;
                    conf.HotlineFeedback = HotlineFeedback;
                    conf.Pinterest = Pinterest;
                    conf.NotiPopup = popupNoti;
                    conf.NotiPopupTitle = NotiPopupTitle;
                    conf.NotiPopupEmail = NotiPopupEmail;
                    conf.YoutubeLink = YoutubeLink;
                    conf.ZaloLink = ZaloLink;
                    conf.WechatLink = WechatLink;
                    conf.MetaKeyword = MetaKeyword;
                    conf.MetaDescription = MetaDescription;
                    conf.GoogleAnalytics = GoogleAnalytics;
                    conf.WebmasterTools = WebmasterTools;

                    conf.InsurancePercent = InsurancePercent;

                    dbe.SaveChanges();
                    return "ok";
                }
                else return null;
            }
        }
        #endregion
        #region Select
        public static tbl_Configuration GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var conf = dbe.tbl_Configuration.Where(c => c.ID == ID).FirstOrDefault();
                if (conf != null)
                {
                    return conf; 
                }
                else
                    return null;
            } 
        } 
        public static tbl_Configuration GetByTop1()
        {
            using (var dbe = new NHSTEntities())
            {
                var conf = dbe.tbl_Configuration.Where(c => c.ID == 1).FirstOrDefault();
                if (conf != null)
                { 
                    return conf;
                }
                else
                    return null;
            }
        }
        #endregion

        public static string UpdateVer2(int ID, string Websitename, string EmailSupport, string EmailContact, string Hotline, string Address, string Facebook, string Twitter, string GooglePlus, string Instagram,
          string Skype, string TimeWork, string Currency, string PricePayHelpDefault, string AboutText, string Address2, string FooterLeft, string SalePercentAfter3Month, string SalePercent, string DathangPercent,
          string HotlineSupport, string HotlineFeedback, string Pinterest, string popupNoti, string NotiPopupTitle, string NotiPopupEmail, string YoutubeLink, string ZaloLink, string WechatLink, string MetaKeyword,
          string MetaDescription, string GoogleAnalytics, string WebmasterTools, int NumberLinkOfOrder, string MetaTitle, string OGTitle, string OGDescription, string OGImage, string OGFBTitle, string OGFBDescription,
          string OGFBImage, string OGTwitterTitle, string OGTwitterDescription, string OGTwitterImage, string InsurancePercent, string HeaderScript, string FooterScript, string CompanyShortName, string CompanyLongName,
          string CompanyName, string Logo, string TaxCode, string AgentCurrency, string PriceCheckOutWareDefault, string zalo2, string zalo3, string zalo4, string zalo5, string CurrencyReal)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                var conf = dbe.tbl_Configuration.Where(c => c.ID == ID).FirstOrDefault();
                if (conf != null)
                {
                    conf.CompanyShortName = CompanyShortName;
                    conf.CompanyName = CompanyName;
                    conf.CompanyLongName = CompanyLongName;
                    if (!string.IsNullOrEmpty(Logo))
                        conf.LogoIMG = Logo;

                    conf.TaxCode = TaxCode;
                    conf.Websitename = Websitename;
                    conf.EmailContact = EmailContact;
                    conf.EmailSupport = EmailSupport;
                    conf.Hotline = Hotline;
                    conf.Address = Address;
                    conf.Facebook = Facebook;
                    conf.Twitter = Twitter;
                    conf.GooglePlus = GooglePlus;
                    conf.Instagram = Instagram;
                    conf.Skype = Skype;
                    conf.TimeWork = TimeWork;
                    conf.Currency = Currency;
                    conf.AgentCurrency = AgentCurrency;
                    conf.PriceCheckOutWareDefault = PriceCheckOutWareDefault;                    
                    conf.AboutText = AboutText;
                    conf.Address2 = Address2;
                    conf.FooterLeft = FooterLeft;                   
                    conf.PricePayHelpDefault = PricePayHelpDefault;                  
                    conf.SalePercentAfter3Month = SalePercentAfter3Month;
                    conf.SalePercent = SalePercent;
                    conf.HotlineSupport = HotlineSupport;
                    conf.DathangPercent = DathangPercent;
                    conf.HotlineFeedback = HotlineFeedback;
                    conf.Pinterest = Pinterest;
                    conf.NotiPopup = popupNoti;
                    conf.NotiPopupTitle = NotiPopupTitle;
                    conf.NotiPopupEmail = NotiPopupEmail;
                    conf.YoutubeLink = YoutubeLink;
                    conf.ZaloLink = ZaloLink;
                    conf.WechatLink = WechatLink;
                    conf.MetaKeyword = MetaKeyword;
                    conf.MetaDescription = MetaDescription;
                    conf.GoogleAnalytics = GoogleAnalytics;
                    conf.WebmasterTools = WebmasterTools;
                    conf.NumberLinkOfOrder = NumberLinkOfOrder;
                    conf.MetaTitle = MetaTitle;
                    conf.OGTitle = OGTitle;
                    conf.OGDescription = OGDescription;

                    if (!string.IsNullOrEmpty(OGImage))
                        conf.OGImage = OGImage;
                    
                    conf.OGFBTitle = OGFBTitle;
                    conf.OGFBDescription = OGFBDescription;

                    if (!string.IsNullOrEmpty(OGFBImage))
                        conf.OGFBImage = OGFBImage;                    

                    conf.OGTwitterTitle = OGTwitterTitle;
                    conf.OGTwitterDescription = OGTwitterDescription;

                    if (!string.IsNullOrEmpty(OGTwitterImage))
                        conf.OGTwitterImage = OGTwitterImage;
                    
                    conf.InsurancePercent = InsurancePercent;
                    conf.HeaderScriptCode = HeaderScript;
                    conf.FooterRight = FooterScript;

                    conf.ZaloLink2 = zalo2;
                    conf.ZaloLink3 = zalo3;
                    conf.ZaloLink4 = zalo4;
                    conf.ZaloLink5 = zalo5;
                    conf.CurrencyReal = CurrencyReal;

                    dbe.SaveChanges();
                    return "ok";
                }
                else return null;
            }
        } 
    }
}
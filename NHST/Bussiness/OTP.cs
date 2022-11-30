using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHST.Bussiness
{
    public class OTPUtils
    {
        public static string ResetAndCreateOTP(int UID, string Prefix, string UserPhone, int type)
        {
            List<tbl_OTP> otps = OTPController.SelectByUserPhonePrefixType(UID, UserPhone, Prefix, type);
            if (otps.Count > 0)
            {
                DateTime current = DateTime.Now;
                foreach (var item in otps)
                {
                    OTPController.UpdateStatusByID(item.ID, false);
                }
                string otp = PJUtils.RandomString(6);
                string kq = OTPController.Insert(UID, otp, DateTime.Now, Prefix, UserPhone, type, true);
                if (Convert.ToInt32(kq) > 0)
                {
                    //Send cái OTP
                    return otp;
                    //HttpContext.Current.Response.Redirect("/OTP.aspx");
                }
            }
            else
            {
                string otp = PJUtils.RandomString(6);
                string kq = OTPController.Insert(UID, otp, DateTime.Now, Prefix, UserPhone, type, true);
                if (Convert.ToInt32(kq) > 0)
                {
                    //Send cái OTP

                    //Điều hướng sang trang điền OTP
                    return otp;
                    //HttpContext.Current.Response.Redirect("/OTP.aspx");
                }
            }
            return null;
        }
    }
}
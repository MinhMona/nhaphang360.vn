using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class OTPController
    {        
        #region CRUD
        public static string Insert(int UID, string Value, DateTime CreatedDate, string Prefix, string UserPhone, int Type, bool Status)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_OTP o = new tbl_OTP();
                o.UID = UID;
                o.Value = Value;
                o.CreatedDate = CreatedDate;
                o.Prefix = Prefix;
                o.UserPhone = UserPhone;
                o.Type = Type;
                o.Status = Status;
                dbe.tbl_OTP.Add(o);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                string kq = dbe.SaveChanges().ToString();
                return kq;
            }
        }
        public static string UpdateStatusByID(int ID, bool Status)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_OTP o = dbe.tbl_OTP.Where(ot => ot.ID == ID).FirstOrDefault();
                if (o != null)
                {
                    o.Status = Status;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "done";
                }
                return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_OTP> SelectByUserPhonePrefixType(int UID,string UserPhone, string Prefix, int Type)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_OTP> otps = new List<tbl_OTP>();
                otps = dbe.tbl_OTP.Where(ot => ot.UserPhone == UserPhone && ot.Prefix == Prefix && ot.Type == Type).ToList();
                if (otps.Count > 0)
                {
                    return otps;
                }
                else
                    return otps;
            }
        }
        public static tbl_OTP SelectByUserPhonePrefixTypeValue(int UID, string UserPhone, string Prefix, string Value, int Type)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_OTP otp = dbe.tbl_OTP.Where(ot => ot.UserPhone == UserPhone && ot.Prefix == Prefix && ot.Type == Type && ot.Value == Value).FirstOrDefault();
                if (otp != null)
                {
                    return otp;
                }
                else
                    return null;
            }
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class AccountInfoController
    {
        #region CRUD
        public static string Insert(int UID, string FirstName, string LastName, string MobilePhonePrefix, string MobilePhone, string Email, string Phone, string Address,
            string Latitude, string Longitude, DateTime Birthday, int Gender, DateTime CreatedDate, string CreatedBy, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_AccountInfo ui = new tbl_AccountInfo();
                ui.UID = UID;
                ui.FirstName = FirstName;
                ui.LastName = LastName;
                ui.MobilePhonePrefix = MobilePhonePrefix;
                ui.MobilePhone = MobilePhone;
                ui.Email = Email;
                ui.Phone = Phone;
                ui.Address = Address;
                ui.BirthDay = Birthday;
                ui.Gender = Gender;
                ui.Latitude = Latitude;
                ui.Longitude = Longitude;
                ui.CreatedDate = CreatedDate;
                ui.CreatedBy = CreatedBy;
                ui.ModifiedBy = ModifiedBy;
                ui.ModifiedDate = ModifiedDate;
                dbe.tbl_AccountInfo.Add(ui);
                int kq = dbe.SaveChanges();
                return kq.ToString();
            }
        }
        public static string Update(int UID, string FirstName, string LastName, string Email, string Phone,
            string Address, DateTime Birthday, int Gender, string Latitude, string Longitude, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_AccountInfo ui = dbe.tbl_AccountInfo.Where(a => a.UID == UID).FirstOrDefault();
                if (ui != null)
                {
                    ui.FirstName = FirstName;
                    ui.LastName = LastName;
                    ui.Email = Email;
                    ui.Phone = Phone;
                    ui.MobilePhone = Phone;
                    ui.Address = Address;
                    ui.BirthDay = Birthday;
                    ui.Gender = Gender;
                    ui.Latitude = Latitude;
                    ui.Longitude = Longitude;
                    ui.ModifiedBy = ModifiedBy;
                    ui.ModifiedDate = ModifiedDate;
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        public static string UpdateIMGUser(int UID, string IMGUser)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_AccountInfo ui = dbe.tbl_AccountInfo.Where(a => a.UID == UID).FirstOrDefault();
                if (ui != null)
                {
                    ui.IMGUser = IMGUser;
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static tbl_AccountInfo GetByUserID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_AccountInfo ai = dbe.tbl_AccountInfo.Where(a => a.UID == UID).FirstOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static tbl_AccountInfo GetByEmailFP(string Email)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_AccountInfo acc = dbe.tbl_AccountInfo.Where(a => a.Email == Email).FirstOrDefault();
                if (acc != null)
                    return acc;
                else
                    return null;
            }
        }
        public static tbl_AccountInfo GetByPhone(string phone)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_AccountInfo acc = dbe.tbl_AccountInfo.Where(a => a.Phone == phone).FirstOrDefault();
                if (acc != null)
                    return acc;
                else
                    return null;
            }
        }

        #endregion
    }
}
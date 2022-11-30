using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;

namespace NHST.Controllers
{
    public class SendNotiEmailController
    {
        public static string Update(int ID, bool IsSendNotiAdmin, bool IsSendNotiUser, bool IsSendEmailAdmin, bool IsSendEmailUser)
        {
            using (var db = new NHSTEntities())
            {
                var sn = db.tbl_SendNotiEmail.Where(x => x.ID == ID).FirstOrDefault();
                if(sn != null)
                {
                    sn.IsSentNotiAdmin = IsSendNotiAdmin;
                    sn.IsSentNotiUser = IsSendNotiUser;
                    sn.IsSentEmailAdmin = IsSendEmailAdmin;
                    sn.IsSendEmailUser = IsSendEmailUser;
                    string kq = db.SaveChanges().ToString();
                    return kq;
                }
                return null;
            }
        }

        public static List<tbl_SendNotiEmail> GetAll()
        {
            using (var db = new NHSTEntities())
            {
                var sn = db.tbl_SendNotiEmail.ToList();
                return sn;
            }
        }

        public static tbl_SendNotiEmail GetByID(int ID)
        {
            using (var db = new NHSTEntities())
            {
                var sn = db.tbl_SendNotiEmail.Where(x => x.ID == ID).FirstOrDefault();
                if (sn != null)
                    return sn;
                return null;
            }
        }
    }
}
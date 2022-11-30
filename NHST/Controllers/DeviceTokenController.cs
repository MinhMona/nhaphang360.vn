using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHST.Controllers
{
    public class DeviceTokenController
    {
        public static tbl_DeviceToken insert(int UID, int Type, string TypeName, string Device, string CreatedBy, string token)
        {
            using (var db = new NHSTEntities())
            {
                tbl_DeviceToken dt = new tbl_DeviceToken();
                dt.UID = UID;
                dt.Type = Type;
                dt.TypeName = TypeName;
                dt.Device = Device;
                dt.UserToken = token;
                dt.isHide = true;
                dt.CreatedBy = CreatedBy;
                dt.CreatedDate = DateTime.Now;
                db.tbl_DeviceToken.Add(dt);
                db.SaveChanges();
                return dt;
            }
        }

        public static int update(int ID)
        {
            using (var db = new NHSTEntities())
            {
                var dt = db.tbl_DeviceToken.Where(x => x.ID == ID).FirstOrDefault();
                if (dt != null)
                {
                    dt.isHide = false;
                    int i = db.SaveChanges();
                    return i;
                }
                return 0;
            }
        }

        public static tbl_DeviceToken GetByUIDandDevice(int UID, string Device)
        {
            using (var db = new NHSTEntities())
            {
                var dt = db.tbl_DeviceToken.Where(x => x.UID == UID && x.Device == Device && x.isHide == true).FirstOrDefault();
                if (dt != null)
                    return dt;
                return null;
            }
        }

        public static List<tbl_DeviceToken> GetByIsHidden()
        {
            using (var db = new NHSTEntities())
            {
                var dt = db.tbl_DeviceToken.Where(x => x.isHide == true).ToList();
                if (dt.Count > 0)
                    return dt;
                return null;
            }
        }

        public static List<tbl_DeviceToken> GetAllDevice(int UID, string Device)
        {
            using (var db = new NHSTEntities())
            {
                var dt = db.tbl_DeviceToken.Where(x => x.isHide == true && x.UID == UID && x.Device == Device).ToList();
                if (dt.Count > 0)
                    return dt;
                return null;
            }
        }

        public static List<tbl_DeviceToken> GetAllByDevice(string Device)
        {
            using (var db = new NHSTEntities())
            {
                var dt = db.tbl_DeviceToken.Where(x => x.Device == Device).ToList();
                if (dt.Count > 0)
                    return dt;
                return null;
            }
        }


        public static tbl_DeviceToken GetByID(int UID)
        {
            using (var db = new NHSTEntities())
            {
                var dt = db.tbl_DeviceToken.Where(x => x.isHide == true && x.UID == UID).LastOrDefault();
                if (dt != null)
                    return dt;
                return null;
            }
        }

        public static tbl_DeviceToken GetByUID(int UID)
        {
            using (var db = new NHSTEntities())
            {
                var dt = db.tbl_DeviceToken.Where(x => x.isHide == true && x.UID == UID).FirstOrDefault();
                if (dt != null)
                    return dt;
                return null;
            }
        }

        public static List<tbl_DeviceToken> GetAllByUID(int UID)
        {
            using (var db = new NHSTEntities())
            {
                var dt = db.tbl_DeviceToken.Where(x => x.isHide == true && x.UID == UID).ToList();
                if (dt.Count > 0)
                    return dt;
                return null;
            }
        }

        public static List<tbl_DeviceToken> GetAllDevice()
        {
            using (var db = new NHSTEntities())
            {
                var dt = db.tbl_DeviceToken.Where(x => x.isHide == true).ToList();
                if (dt.Count > 0)
                    return dt;
                return null;
            }
        }

        public static tbl_DeviceToken GetByToken(int UID, string Token)
        {
            using (var db = new NHSTEntities())
            {
                var dt = db.tbl_DeviceToken.Where(x => x.isHide == true && x.UID == UID && x.UserToken == Token).FirstOrDefault();
                if (dt != null)
                    return dt;
                return null;
            }
        }

            public static List<tbl_DeviceToken> GetAllByUID(int UID, string Token)
            {
                using (var db = new NHSTEntities())
                {
                    var dt = db.tbl_DeviceToken.Where(x => x.isHide == true && x.UID == UID && x.UserToken != Token).ToList();
                    if (dt.Count > 0)
                        return dt;
                    return null;
                }
            }

        public static List<tbl_DeviceToken> GetAllByUIDandIsHide(int UID)
        {
            using (var db = new NHSTEntities())
            {
                var dt = db.tbl_DeviceToken.Where(x => x.isHide == true && x.UID == UID).ToList();
                if (dt.Count > 0)
                    return dt;
                return null;
            }
        }

    }
}
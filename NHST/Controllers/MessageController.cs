using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
namespace NHST.Controllers
{
    public class MessageController
    {
     
        #region CRUD
        #endregion
        #region Select
        public static tbl_Message GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Message m = dbe.tbl_Message.Where(me => me.ID == ID).FirstOrDefault();
                if (m != null)
                    return m;
                else return null;
            }
        }
        public static tbl_Message GetByType(int Type)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Message m = dbe.tbl_Message.Where(me => me.Type == Type).FirstOrDefault();
                if (m != null)
                    return m;
                else return null;
            }
        }
        #endregion
    }
}
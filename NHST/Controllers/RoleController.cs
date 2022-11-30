using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Bussiness;
using NHST.Models;

namespace NHST.Controllers
{
    public class RoleController
    {
        #region CRUD
        #endregion
        #region Select
        public static tbl_Role GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var r = dbe.tbl_Role.Where(ro => ro.ID == ID).FirstOrDefault();
                if (r != null)
                    return r;
                else return null;
            }
        }
        #endregion
    }
}
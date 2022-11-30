using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class InWarehousePriceController
    {           
        #region Select
        public static List<tbl_InWareHousePrice> GetAll()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_InWareHousePrice> pages = new List<tbl_InWareHousePrice>();
                pages = dbe.tbl_InWareHousePrice.ToList();
                return pages;
            }
        }        
        public static tbl_InWareHousePrice GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_InWareHousePrice page = dbe.tbl_InWareHousePrice.Where(p => p.ID == ID).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        #endregion
    }
}
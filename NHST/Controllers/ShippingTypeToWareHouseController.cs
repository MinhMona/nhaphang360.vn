using MB.Extensions;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebUI.Business;

namespace NHST.Controllers
{
    public class ShippingTypeToWareHouseController
    {
        #region CRUD
        public static string Insert(string ShippingTypeName, string ShippintTypeDescription, bool IsHidden, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_ShippingTypeToWareHouse c = new tbl_ShippingTypeToWareHouse();
                c.ShippingTypeName = ShippingTypeName;
                c.ShippintTypeDescription = ShippintTypeDescription;
                c.IsHidden = IsHidden;
                c.CreatedDate = CreatedDate;
                c.CreatedBy = CreatedBy;
                dbe.tbl_ShippingTypeToWareHouse.Add(c);
                dbe.SaveChanges();
                string kq = c.ID.ToString();
                return kq;
            }
        }
        public static string Update(int ID, string ShippingTypeName, string ShippintTypeDescription, bool IsHidden, 
            DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_ShippingTypeToWareHouse.Where(p => p.ID == ID).FirstOrDefault();
                if (c != null)
                {
                    c.ShippingTypeName = ShippingTypeName;
                    c.ShippintTypeDescription = ShippintTypeDescription;
                    c.IsHidden = IsHidden;
                    c.ModifiedDate = ModifiedDate;
                    c.ModifiedBy = ModifiedBy;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select        
        public static List<tbl_ShippingTypeToWareHouse> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_ShippingTypeToWareHouse> cs = new List<tbl_ShippingTypeToWareHouse>();
                //cs = dbe.tbl_ShippingTypeToWareHouse.Where(c => c.ShippingTypeName.Contains(s)).OrderByDescending(c => c.ID).ToList();
                cs = dbe.tbl_ShippingTypeToWareHouse.Where(c => c.ShippingTypeName.Contains(s)).ToList();
                return cs;
            }
        }
        public static List<tbl_ShippingTypeToWareHouse> GetAllWithIsHidden(bool IsHidden)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_ShippingTypeToWareHouse> cs = new List<tbl_ShippingTypeToWareHouse>();
                //cs = dbe.tbl_ShippingTypeToWareHouse.Where(c => c.IsHidden == IsHidden).OrderByDescending(c => c.ID).ToList();
                cs = dbe.tbl_ShippingTypeToWareHouse.Where(c => c.IsHidden == IsHidden).ToList();
                return cs;
            }
        }
        public static tbl_ShippingTypeToWareHouse GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_ShippingTypeToWareHouse.Where(p => p.ID == ID).FirstOrDefault();
                if (c != null)
                {
                    return c;
                }
                else
                    return null;
            }
        }
        #endregion

        #region New
        public static int GetTotalBySQL(string s)
        {
            var sql = @"select Total=Count(*) ";
            sql += "from tbl_ShippingTypeToWareHouse ";
            sql += "Where ShippingTypeName like N'%" + s + "%' ";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int a = 0;
            while (reader.Read())
            {
                if (reader["Total"] != DBNull.Value)
                    a = reader["Total"].ToString().ToInt(0);
            }
            reader.Close();
            return a;
        }
        public static List<tbl_ShippingTypeToWareHouse> GetAllBySQL(string s, int pageIndex, int pageSize)
        {
            var sql = @"select * ";
            sql += "from tbl_ShippingTypeToWareHouse ";
            sql += "Where ShippingTypeName like N'%" + s + "%' ";
            sql += "order by id DESC OFFSET " + pageIndex + "*" + pageSize + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<tbl_ShippingTypeToWareHouse> a = new List<tbl_ShippingTypeToWareHouse>();
            while (reader.Read())
            {
                var entity = new tbl_ShippingTypeToWareHouse();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["ShippingTypeName"] != DBNull.Value)
                    entity.ShippingTypeName = reader["ShippingTypeName"].ToString();

                if (reader["ShippintTypeDescription"] != DBNull.Value)
                    entity.ShippintTypeDescription = reader["ShippintTypeDescription"].ToString();

                if (reader["IsHidden"] != DBNull.Value)
                    entity.IsHidden = Convert.ToBoolean(reader["IsHidden"]);

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                a.Add(entity);
            }
            reader.Close();
            return a;
        }
        #endregion
    }
}
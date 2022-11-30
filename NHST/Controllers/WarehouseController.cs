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
    public class WarehouseController
    {
        #region CRUD
        public static string Insert(string WareHouseName, double AdditionFee, string Address, string Email, string Phone,
            string Latitude, string Longitude, bool IsHidden, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Warehouse c = new tbl_Warehouse();
                c.WareHouseName = WareHouseName;
                c.AdditionFee = AdditionFee;
                c.Address = Address;
                c.Email = Email;
                c.Phone= Phone;
                c.Latitude = Latitude;
                c.Longitude = Longitude;
                c.IsHidden = IsHidden;
                c.CreatedDate = CreatedDate;
                c.CreatedBy = CreatedBy;
                dbe.tbl_Warehouse.Add(c);
                dbe.SaveChanges();
                string kq = c.ID.ToString();
                return kq;
            }
        }
        public static string Update(int ID, string WareHouseName, double AdditionFee, string Address, string Email, string Phone,
            string Latitude, string Longitude, bool IsHidden, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_Warehouse.Where(p => p.ID == ID).FirstOrDefault();
                if (c != null)
                {
                    c.WareHouseName = WareHouseName;
                    c.AdditionFee = AdditionFee;
                    c.Address = Address;
                    c.Email = Email;
                    c.Phone = Phone;
                    c.Latitude = Latitude;
                    c.Longitude = Longitude;
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
        public static List<tbl_Warehouse> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Warehouse> cs = new List<tbl_Warehouse>();
                //cs = dbe.tbl_Warehouse.Where(c => c.WareHouseName.Contains(s)).OrderByDescending(c => c.ID).ToList();
                cs = dbe.tbl_Warehouse.Where(c => c.WareHouseName.Contains(s)).ToList();
                return cs;
            }
        }
        public static List<tbl_Warehouse> GetAllWithIsHidden(bool IsHidden)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Warehouse> cs = new List<tbl_Warehouse>();
                //cs = dbe.tbl_Warehouse.Where(c => c.IsHidden == IsHidden).OrderByDescending(c => c.ID).ToList();
                cs = dbe.tbl_Warehouse.Where(c => c.IsHidden == IsHidden).ToList();
                return cs;
            }
        }
        public static tbl_Warehouse GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_Warehouse.Where(p => p.ID == ID).FirstOrDefault();
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
        public static List<tbl_Warehouse> GetAllBySQL(string s, int pageIndex, int pageSize)
        {
            var sql = @"select * ";
            sql += "from tbl_Warehouse ";
            sql += "Where WareHouseName like N'%" + s + "%' ";
            sql += "order by id DESC OFFSET " + pageIndex + "*" + pageSize + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<tbl_Warehouse> a = new List<tbl_Warehouse>();
            while (reader.Read())
            {
                var entity = new tbl_Warehouse();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["WareHouseName"] != DBNull.Value)
                    entity.WareHouseName = reader["WareHouseName"].ToString();

                if (reader["IsHidden"] != DBNull.Value)
                    entity.IsHidden = Convert.ToBoolean(reader["IsHidden"]);

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                a.Add(entity);
            }
            reader.Close();
            return a;
        }
        public static int GetTotalBySQL(string s)
        {
            var sql = @"select Total=Count(*) ";
            sql += "from tbl_Warehouse ";
            sql += "Where WareHouseName like N'%" + s + "%' ";
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
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using MB.Extensions;
using WebUI.Business;
using System.Data;
using System.Globalization;

namespace NHST.Controllers
{
    public class ComplainController
    {
        #region CRUD
        public static string Insert(int UID, int OrderID, string Amount, string IMG, string ComplainText, int Status, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Complain c = new tbl_Complain();
                c.UID = UID;
                c.OrderID = OrderID;
                c.Amount = Amount;
                c.IMG = IMG;
                c.ComplainText = ComplainText;
                c.Status = Status;
                c.CreatedDate = CreatedDate;
                c.CreatedBy = CreatedBy;
                dbe.tbl_Complain.Add(c);
                dbe.SaveChanges();
                string kq = c.ID.ToString();
                return kq;
            }
        }
        public static string Update(int ID, string Amount, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_Complain.Where(p => p.ID == ID).FirstOrDefault();
                if (c != null)
                {
                    c.Amount = Amount;
                    c.Status = Status;
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
        public static List<tbl_Complain> GetByUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Complain> cs = new List<tbl_Complain>();
                cs = dbe.tbl_Complain.Where(c => c.UID == UID).OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_Complain> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Complain> cs = new List<tbl_Complain>();
                cs = dbe.tbl_Complain.Where(c => c.CreatedBy.Contains(s)).OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_Complain> GetAllByOrderShopCodeAndUID(int UID, int OrderID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Complain> cs = new List<tbl_Complain>();
                cs = dbe.tbl_Complain.Where(c => c.UID == UID && c.OrderID == OrderID).OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static tbl_Complain GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_Complain.Where(p => p.ID == ID).FirstOrDefault();
                if (c != null)
                {
                    return c;
                }
                else
                    return null;
            }
        }
        #endregion

        public static List<tbl_Complain> GetByUID_SQL(int UID)
        {
            var list = new List<tbl_Complain>();
            var sql = @"select * from tbl_Complain ";
            sql += " where UID = " + UID + "";
            sql += " Order By ID desc";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);

            int i = 1;
            while (reader.Read())
            {
                var entity = new tbl_Complain();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);
                if (reader["OrderID"] != DBNull.Value)
                    entity.OrderID = reader["OrderID"].ToString().ToInt(0);

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                if (reader["Amount"] != DBNull.Value)
                    entity.Amount = reader["Amount"].ToString();

                if (reader["ComplainText"] != DBNull.Value)
                    entity.ComplainText = reader["ComplainText"].ToString();

                if (reader["ProductID"] != DBNull.Value)
                    entity.ProductID = reader["ProductID"].ToString().ToInt(0);

                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt(0);
                if (reader["OrderShopCode"] != DBNull.Value)
                    entity.OrderShopCode = reader["OrderShopCode"].ToString();

                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString();

                if (reader["OrderCode"] != DBNull.Value)
                    entity.OrderCode = reader["OrderCode"].ToString();
                if (reader["IMG"] != DBNull.Value)
                    entity.IMG = reader["IMG"].ToString();

                i++;
                list.Add(entity);
            }
            reader.Close();
            return list;

        }

        public static int GetTotal(string s, string fd, string td, int Status)
        {
            var sql = @"select Total=Count(*) ";
            sql += "from tbl_Complain ";
            sql += "Where CreatedBy LIKE N'%" + s + "%' ";
            if (Status > -1)
            {
                sql += " And Status=" + Status + " ";
            }
            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += "AND CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += "AND CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
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
        public static List<tbl_Complain> GetAllBySQL(string s, int pageIndex, int pageSize, string fd, string td, int Status)
        {
            var sql = @"select * ";
            sql += "from tbl_Complain ";
            sql += "Where CreatedBy LIKE N'%" + s + "%' ";
            if (Status > -1)
            {
                sql += " And Status=" + Status + " ";
            }
            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += "AND CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += "AND CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            sql += "order by id DESC OFFSET " + pageIndex + "*" + pageSize + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<tbl_Complain> a = new List<tbl_Complain>();
            while (reader.Read())
            {
                var entity = new tbl_Complain();

                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["OrderID"] != DBNull.Value)
                    entity.OrderID = reader["OrderID"].ToString().ToInt(0);

                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);

                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString();

                if (reader["ComplainText"] != DBNull.Value)
                    entity.ComplainText = reader["ComplainText"].ToString();

                if (reader["Status"] != DBNull.Value)
                    entity.Status = Convert.ToInt32(reader["Status"].ToString());

                if (reader["Amount"] != DBNull.Value)
                {
                    entity.Amount = Convert.ToDouble(reader["Amount"].ToString()).ToString("0,0", CultureInfo.InvariantCulture) + " VND";
                }
                //if (reader["Status"] != DBNull.Value)
                //{
                //    entity.Status = reader["Status"].ToString().ToInt(0);
                //    //if (reader["Status"].ToString().ToInt(0) == 1)
                //    //    entity.StatusName = "<span class=\"badge orange darken-2 white-text border-radius-2\">Đang chờ</span>";
                //    //if (reader["Status"].ToString().ToInt(0) == 2)
                //    //    entity.StatusName = "<span class=\"badge green darken-2 white-text border-radius-2\">Duyệt</span>";
                //    //if (reader["Status"].ToString().ToInt(0) == 3)
                //    //    entity.StatusName = "<span class=\"badge red darken-2 white-text border-radius-2\">Hủy</span>";
                //}

                if (reader["CreatedDate"] != DBNull.Value)
                {
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                    //entity.CreatedDateString = Convert.ToDateTime(reader["CreatedDate"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                }
                a.Add(entity);
            }
            reader.Close();
            return a;
        }
    }
}
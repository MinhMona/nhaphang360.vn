using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using MB.Extensions;
using WebUI.Business;
using System.Globalization;
using System.Data;

namespace NHST.Controllers
{
    public class OutStockSessionController
    {
        #region CRUD
        public static string Insert(int UID, string Username, string FullName, string Phone, int Status,
            DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities()) //now wrapping the context in a using to ensure it is disposed
            {
                tbl_OutStockSession user = new tbl_OutStockSession();
                user.UID = UID;
                user.Username = Username;
                user.FullName = FullName;
                user.Phone = Phone;
                user.Status = Status;
                user.CreatedDate = CreatedDate;
                user.CreatedBy = CreatedBy;
                dbe.tbl_OutStockSession.Add(user);
                int kq = dbe.SaveChanges();
                string k = user.ID.ToString();
                return k;
            }

        }
        public static string update(int ID, string FullName, string Phone, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_OutStockSession.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.FullName = FullName;
                    a.Phone = Phone;
                    a.Status = Status;
                    a.ModifiedBy = ModifiedBy;
                    a.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
      
        public static string update_mainorderID(int ID, int MainOrderID, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_OutStockSession.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.MainOrderID = MainOrderID;
                    a.ModifiedBy = ModifiedBy;
                    a.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }

        public static string Remove(int MainOrderID)
        {
            using (var db = new NHSTEntities())
            {
                var p = db.tbl_OutStockSession.Where(x => x.MainOrderID == MainOrderID).SingleOrDefault();
                if (p != null)
                {
                    db.tbl_OutStockSession.Remove(p);
                    db.SaveChanges();
                    return "ok";
                }
                return null;
            }
        }
        public static string updateInfo(int ID, string FullName, string Phone)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_OutStockSession.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.FullName = FullName;
                    a.Phone = Phone;                    
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string updateTotalPay(int ID, double TotalPay)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_OutStockSession.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.TotalPay = TotalPay;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string updateStatus(int ID, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_OutStockSession.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.Status = Status;
                    a.ModifiedBy = ModifiedBy;
                    a.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_OutStockSession> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_OutStockSession> las = new List<tbl_OutStockSession>();
                las = dbe.tbl_OutStockSession.Where(a => a.Username.Contains(s)).OrderByDescending(a => a.CreatedDate).ToList();
                return las;
            }
        }
        public static List<tbl_OutStockSession> GetAllByUsername(string username)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_OutStockSession> las = new List<tbl_OutStockSession>();
                las = dbe.tbl_OutStockSession.Where(a => a.Username == username).OrderByDescending(a => a.CreatedDate).ToList();
                return las;
            }
        }
        public static tbl_OutStockSession GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_OutStockSession acc = dbe.tbl_OutStockSession.Where(a => a.ID == ID).FirstOrDefault();
                if (acc != null)
                    return acc;
                else
                    return null;
            }
        }
        #endregion
        #region New
        public static List<OutStockNew> GetBySQL_Xuka(string UserName, int pageIndex, int pageSize, string fd, string td, int status)
        {
            var sql = @"select ID,UID,Username,Status,TotalPay,CreatedDate ";
            sql += "from tbl_OutStockSession ";

            sql += "where Username LIKE '%" + UserName + "%' ";
            if (status != -1)
            {
                sql += "AND Status=" + status + " ";
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
            List<OutStockNew> a = new List<OutStockNew>();
            while (reader.Read())
            {
                var entity = new OutStockNew();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);

                if (reader["Username"] != DBNull.Value)
                    entity.Username = reader["Username"].ToString();

                if (reader["TotalPay"] != DBNull.Value)
                {
                    entity.TotalPay = Convert.ToDouble(reader["TotalPay"].ToString());
                    entity.TotalPayString = Convert.ToDouble(reader["TotalPay"].ToString()).ToString("0,0", CultureInfo.InvariantCulture) + " VNĐ";
                }

                if (reader["Status"] != DBNull.Value)
                {
                    entity.Status = reader["Status"].ToString().ToInt(0);
                    if (reader["Status"].ToString().ToInt(0) == 2)
                        entity.StatusName = "<span class=\"badge green darken-2 white-text border-radius-2\">Đã xử lý</span>";
                    if (reader["Status"].ToString().ToInt(0) == 1 || reader["Status"].ToString().ToInt(0) == 0)
                        entity.StatusName = "<span class=\"badge red darken-2 white-text border-radius-2\">Chưa xử lý</span>";
                }

                if (reader["CreatedDate"] != DBNull.Value)
                {
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                    entity.CreatedDateString = Convert.ToDateTime(reader["CreatedDate"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                }
                a.Add(entity);

            }
            reader.Close();
            return a;
        }
        public static int GetTotalBySQL_Xuka(string UserName, string fd, string td, int status)
        {
            var sql = @"select Total=Count(*)";
            sql += "from tbl_OutStockSession ";

            sql += "where Username LIKE '%" + UserName + "%' ";
            if (status != -1)
            {
                sql += "AND Status=" + status + " ";
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
            int a = 0;
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);

            while (reader.Read())
            {
                if (reader["Total"] != DBNull.Value)
                    a = reader["Total"].ToString().ToInt(0);
            }
            return a;

        }        
        public static int GetTotalBySQLNew(string UserName, string UID, string MainOrderID, string fd, string td, int status)
        {
            var sql = @"select Total=Count(*)";         
            sql += "from tbl_OutStockSession as st ";
            sql += "left outer join(select * from tbl_MainOder) as mo ON st.MainOrderID = mo.ID ";
            sql += "where st.Username LIKE '%" + UserName + "%' and st.UID LIKE N'%" + UID + "%' and st.MainOrderID LIKE N'%" + MainOrderID + "%' and mo.Status = 10 and st.Status = 2 ";
            if (status != -1)
            {
                sql += "AND st.Status=" + status + " ";
            }
            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += "AND st.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += "AND st.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            int a = 0;
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);

            while (reader.Read())
            {
                if (reader["Total"] != DBNull.Value)
                    a = reader["Total"].ToString().ToInt(0);
            }
            return a;
        }
        public static List<OutStockNew> GetBySQL_DKNew(string UserName, string UID, string MainOrderID, int pageIndex, int pageSize, string fd, string td, int status)
        {
            var sql = @"select st.ID,st.UID,st.Username,st.Status,mo.TotalPriceVND as TotalPay,st.CreatedDate,st.MainOrderID ";
            sql += "from tbl_OutStockSession as st ";
            sql += "left outer join(select * from tbl_MainOder) as mo ON st.MainOrderID = mo.ID ";
            sql += "where st.Username LIKE '%" + UserName + "%' and st.UID LIKE N'%" + UID + "%' and st.MainOrderID LIKE N'%" + MainOrderID + "%' and mo.Status = 10 and st.Status = 2 ";
            if (status != -1)
            {
                sql += "AND st.Status=" + status + " ";
            }
            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += "AND st.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += "AND st.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            sql += "order by id DESC OFFSET " + pageIndex + "*" + pageSize + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<OutStockNew> a = new List<OutStockNew>();
            while (reader.Read())
            {
                var entity = new OutStockNew();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);

                if (reader["MainOrderID"] != DBNull.Value)
                    entity.MainOrderID = reader["MainOrderID"].ToString().ToInt(0);

                if (reader["Username"] != DBNull.Value)
                    entity.Username = reader["Username"].ToString();

                if (reader["TotalPay"] != DBNull.Value)
                {
                    entity.TotalPay = Convert.ToDouble(reader["TotalPay"].ToString());
                    entity.TotalPayString = Convert.ToDouble(reader["TotalPay"].ToString()).ToString("0,0", CultureInfo.InvariantCulture) + " VNĐ";
                }

                if (reader["Status"] != DBNull.Value)
                {
                    entity.Status = reader["Status"].ToString().ToInt(0);
                    if (reader["Status"].ToString().ToInt(0) == 2)
                        entity.StatusName = "<span class=\"badge green darken-2 white-text border-radius-2\">Đã xử lý</span>";
                    if (reader["Status"].ToString().ToInt(0) == 1 || reader["Status"].ToString().ToInt(0) == 0)
                        entity.StatusName = "<span class=\"badge red darken-2 white-text border-radius-2\">Chưa xử lý</span>";
                }

                if (reader["CreatedDate"] != DBNull.Value)
                {
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                    entity.CreatedDateString = Convert.ToDateTime(reader["CreatedDate"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                }
                a.Add(entity);

            }
            reader.Close();
            return a;
        }        
        public partial class OutStockNew
        {
            public int ID { get; set; }
            public int UID { get; set; }
            public string Username { get; set; }
            public double TotalPay { get; set; }
            public string TotalPayString { get; set; }
            public int Status { get; set; }
            public string StatusName { get; set; }
            public DateTime CreatedDate { get; set; }
            public string CreatedDateString { get; set; }
            public int MainOrderID { get; set; }
        }
        #endregion
    }
}
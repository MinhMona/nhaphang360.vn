using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using MB.Extensions;
using WebUI.Business;
using System.Data;

namespace NHST.Controllers
{
    public class StaffIncomeController
    {
        #region CRUD
        public static string Insert(int MainOrderID, string OrderTotalPrice, string PercentReceive, int UID, string Username, int RoleID, int Status,
            string TotalPriceReceive, bool IsHidden, DateTime OrderCreatedDate, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_StaffIncome c = new tbl_StaffIncome();
                c.MainOrderID = MainOrderID;
                c.OrderTotalPrice = OrderTotalPrice;
                c.PercentReceive = PercentReceive;
                c.UID = UID;
                c.Username = Username;
                c.RoleID = RoleID;
                c.Status = Status;
                c.TotalPriceReceive = TotalPriceReceive;
                c.OrderCreatedDate = OrderCreatedDate;
                c.IsHidden = IsHidden;
                c.CreatedDate = CreatedDate;
                c.CreatedBy = CreatedBy;
                dbe.tbl_StaffIncome.Add(c);
                dbe.SaveChanges();
                string kq = c.ID.ToString();
                return kq;
            }
        }
        public static string Delete(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_StaffIncome.Where(p => p.ID == ID).FirstOrDefault();
                if (c != null)
                {
                    dbe.tbl_StaffIncome.Remove(c);
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string Update(int ID, string OrderTotalPrice, string PercentReceive, int Status,
            string TotalPriceReceive, bool IsHidden, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_StaffIncome.Where(p => p.ID == ID).FirstOrDefault();
                if (c != null)
                {
                    c.OrderTotalPrice = OrderTotalPrice;
                    c.PercentReceive = PercentReceive;
                    c.Status = Status;
                    c.TotalPriceReceive = TotalPriceReceive;
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
        public static string UpdateStatus(int ID, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_StaffIncome.Where(p => p.ID == ID).FirstOrDefault();
                if (c != null)
                {
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
        public static List<tbl_StaffIncome> GetByUIDWithHidden(int UID, bool IsHidden)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.UID == UID && c.IsHidden == IsHidden).OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_StaffIncome> GetByUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.UID == UID).OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_StaffIncome> GetByStatus(int Status)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.Status == Status).OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_StaffIncome> GetByStatusFromDateToDate(int Status, DateTime fromdate, DateTime todate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.Status == Status && c.CreatedDate >= fromdate && c.CreatedDate < todate)
                    .OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }

        public static List<tbl_StaffIncome> GetByUIDFromDateToDate(int UID, DateTime fromdate, DateTime todate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.UID == UID && c.CreatedDate >= fromdate && c.CreatedDate < todate)
                    .OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_StaffIncome> GetByUIDFromDateToDateIsHidden(int UID, DateTime fromdate, DateTime todate, bool IsHidden)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.UID == UID && c.CreatedDate >= fromdate && c.CreatedDate < todate && c.IsHidden == IsHidden)
                    .OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_StaffIncome> GetByUIDStatusFromDateToDate(int UID, int Status, DateTime fromdate, DateTime todate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.UID == UID && c.Status == Status && c.CreatedDate >= fromdate && c.CreatedDate < todate)
                    .OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_StaffIncome> GetByFromDateToDate(DateTime fromdate, DateTime todate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.CreatedDate >= fromdate && c.CreatedDate < todate)
                    .OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_StaffIncome> GetByUIDStatusFromDate(int UID, int Status, DateTime fromdate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.UID == UID && c.Status == Status && c.CreatedDate >= fromdate)
                    .OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_StaffIncome> GetByUIDStatusToDate(int UID, int Status, DateTime todate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.UID == UID && c.Status == Status && c.CreatedDate < todate)
                    .OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }

        public static List<tbl_StaffIncome> GetByUIDFromDate(int UID, DateTime fromdate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.UID == UID && c.CreatedDate >= fromdate)
                    .OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_StaffIncome> GetByUIDToDate(int UID, DateTime todate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.UID == UID && c.CreatedDate < todate)
                    .OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_StaffIncome> GetByStatusToDate(int Status, DateTime todate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.Status == Status && c.CreatedDate < todate)
                    .OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }

        public static List<tbl_StaffIncome> GetByFromDate(DateTime fromdate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.CreatedDate >= fromdate)
                    .OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_StaffIncome> GetByToDate(DateTime todate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.CreatedDate < todate)
                    .OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_StaffIncome> GetByUIDStatus(int UID, int Status)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.UID == UID && c.Status == Status)
                    .OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_StaffIncome> GetByUIDStatusFromDateToDateIsHidden(int UID, int Status, DateTime fromdate, DateTime todate, bool IsHidden)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.UID == UID && c.Status == Status && c.CreatedDate >= fromdate && c.CreatedDate < todate && c.IsHidden == IsHidden)
                    .OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_StaffIncome> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_StaffIncome> cs = new List<tbl_StaffIncome>();
                cs = dbe.tbl_StaffIncome.Where(c => c.CreatedBy.Contains(s)).OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }

        public static tbl_StaffIncome GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_StaffIncome.Where(p => p.ID == ID).FirstOrDefault();
                if (c != null)
                {
                    return c;
                }
                else
                    return null;
            }
        }
        public static tbl_StaffIncome GetByMainOrderID(int MainOrderID)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_StaffIncome.Where(p => p.MainOrderID == MainOrderID).FirstOrDefault();
                if (c != null)
                {
                    return c;
                }
                else
                    return null;
            }
        }
        public static tbl_StaffIncome GetByMainOrderIDUID(int MainOrderID, int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_StaffIncome.Where(p => p.MainOrderID == MainOrderID && p.UID == UID).FirstOrDefault();
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
        public static int GetTotal(int status, string s, string fd, string td)
        {
            var sql = @"SELECT Total=Count(*) ";
            sql += " from tbl_StaffIncome as st ";
            sql += " left outer join(select * from tbl_MainOder) as mo ON st.MainOrderID = mo.ID ";
            sql += " where mo.Status > 8 and CONVERT(float,st.TotalPriceReceive) > 0 and st.RoleID in (3,6) ";
            if (status == 1)
                sql += " AND st.Status=1 ";
            if (status == 2)
                sql += " AND st.Status=2 ";
            if (!string.IsNullOrEmpty(s))
                sql += " AND st.Username Like N'%" + s + "%'";
            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += " AND st.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += " AND st.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
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

        public static int GetTotalWithUID(int UID, int status, string s, string fd, string td)
        {
            var sql = @"SELECT Total=Count(*) ";
            sql += " from tbl_StaffIncome st ";
            sql += " left outer join(select * from tbl_MainOder) as mo ON st.MainOrderID = mo.ID ";
            sql += " where mo.Status > 8 and st.UID=" + UID + " and CONVERT(float,TotalPriceReceive) > 0 ";
            if (status == 1)
                sql += " AND st.Status=1 ";
            if (status == 2)
                sql += " AND st.Status=2 ";
            if (!string.IsNullOrEmpty(s))
                sql += " AND st.Username Like N'%" + s + "%'";
            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += " AND st.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += " AND st.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
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

        public static List<StaffInCome> GetAllByUIDWithSQL(int UID, int status, string s, string fd, string td, int pageIndex, int pageSize)
        {
            var sql = @"SELECT st.ID,st.MainOrderID,st.OrderTotalPrice,st.PercentReceive,st.UID,st.Username,st.RoleID,st.Status,st.TotalPriceReceive,st.CreatedDate ";
            sql += " from tbl_StaffIncome st ";
            sql += " left outer join(select * from tbl_MainOder) as mo ON st.MainOrderID = mo.ID ";
            sql += " where mo.Status > 8 and st.UID=" + UID + " and CONVERT(float,TotalPriceReceive) > 0 ";
            if (status == 1)
                sql += " AND st.Status=1 ";
            if (status == 2)
                sql += " AND st.Status=2 ";
            if (!string.IsNullOrEmpty(s))
                sql += " AND st.Username Like N'%" + s + "%'";
            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += " AND st.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += " AND st.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            sql += " order by st.ID desc OFFSET " + pageIndex + "*" + pageSize + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<StaffInCome> a = new List<StaffInCome>();
            while (reader.Read())
            {
                var entity = new StaffInCome();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["MainOrderID"] != DBNull.Value)
                    entity.MainOrderID = reader["MainOrderID"].ToString().ToInt(0);

                if (reader["OrderTotalPrice"] != DBNull.Value)
                    entity.OrderTotalPrice = Convert.ToDouble(reader["OrderTotalPrice"].ToString());

                if (reader["PercentReceive"] != DBNull.Value)
                    entity.Percent = Convert.ToDouble(reader["PercentReceive"].ToString());

                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);

                if (reader["Username"] != DBNull.Value)
                    entity.UserName = reader["Username"].ToString();

                if (reader["TotalPriceReceive"] != DBNull.Value)
                    entity.TotalPriceReceive = Convert.ToDouble(reader["TotalPriceReceive"].ToString());

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());

                if (reader["Status"] != DBNull.Value)
                {
                    entity.Status = reader["Status"].ToString().ToInt(0);
                    if (reader["Status"].ToString().ToInt(0) == 1)
                    {
                        entity.StatusName = "<span class=\"badge red darken-2 white-text border-radius-2\">Chưa thanh toán</span>";
                    }
                    else
                    {
                        entity.StatusName = "<span class=\"badge green darken-2 white-text border-radius-2\">Đã thanh toán</span>";
                    }
                }              

                if (reader["RoleID"] != DBNull.Value)
                {
                    entity.RoleID = reader["RoleID"].ToString().ToInt(0);
                    if (reader["RoleID"].ToString().ToInt(0) == 3)
                    {
                        entity.RoleName = "NV Đặt Hàng";
                    }
                    else
                    {
                        entity.RoleName = "NV Kinh Doanh";
                    }
                }
                a.Add(entity);
            }
            reader.Close();
            return a;
        }

        public static List<StaffInCome> GetAllBySQLForExcel(int status, string s, string fd, string td)
        {
            var sql = @"SELECT st.ID,st.MainOrderID,st.OrderTotalPrice,st.PercentReceive,st.UID,st.Username,st.RoleID,st.Status,st.TotalPriceReceive,st.CreatedDate ";
            sql += " from tbl_StaffIncome st ";
            sql += " left outer join(select * from tbl_MainOder) as mo ON st.MainOrderID = mo.ID ";
            sql += " where mo.Status > 8 and CONVERT(float,st.TotalPriceReceive) > 0 and st.RoleID in (3,6) ";
            if (status == 1)
                sql += " AND st.Status=1 ";
            if (status == 2)
                sql += " AND st.Status=2 ";
            if (!string.IsNullOrEmpty(s))
                sql += " AND st.Username Like N'%" + s + "%' ";
            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += " AND st.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += " AND st.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            sql += " order by st.ID desc ";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<StaffInCome> a = new List<StaffInCome>();
            while (reader.Read())
            {
                var entity = new StaffInCome();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["MainOrderID"] != DBNull.Value)
                    entity.MainOrderID = reader["MainOrderID"].ToString().ToInt(0);

                if (reader["OrderTotalPrice"] != DBNull.Value)
                    entity.OrderTotalPrice = Convert.ToDouble(reader["OrderTotalPrice"].ToString());

                if (reader["PercentReceive"] != DBNull.Value)
                    entity.Percent = Convert.ToDouble(reader["PercentReceive"].ToString());

                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);

                if (reader["Username"] != DBNull.Value)
                    entity.UserName = reader["Username"].ToString();

                if (reader["TotalPriceReceive"] != DBNull.Value)
                    entity.TotalPriceReceive = Convert.ToDouble(reader["TotalPriceReceive"].ToString());

                if (reader["Status"] != DBNull.Value)
                {
                    entity.Status = reader["Status"].ToString().ToInt(0);
                    if (reader["Status"].ToString().ToInt(0) == 1)
                    {
                        entity.StatusName = "<span class=\"badge red darken-2 white-text border-radius-2\">Chưa thanh toán</span>";
                    }
                    else
                    {
                        entity.StatusName = "<span class=\"badge green darken-2 white-text border-radius-2\">Đã thanh toán</span>";
                    }
                }

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());

                if (reader["RoleID"] != DBNull.Value)
                {
                    entity.RoleID = reader["RoleID"].ToString().ToInt(0);
                    if (reader["RoleID"].ToString().ToInt(0) == 3)
                    {
                        entity.RoleName = "NV Đặt Hàng";
                    }
                    else
                    {
                        entity.RoleName = "NV Kinh Doanh";
                    }
                }

                a.Add(entity);
            }
            reader.Close();
            return a;
        }
        public static List<StaffInCome> GetAllBySQL(int status, string s, string fd, string td, int pageIndex, int pageSize)
        {
            var sql = @"SELECT st.ID,st.MainOrderID,st.OrderTotalPrice,st.PercentReceive,st.UID,st.Username,st.RoleID,st.Status,st.TotalPriceReceive,st.CreatedDate ";
            sql += " from tbl_StaffIncome st ";
            sql += " left outer join(select * from tbl_MainOder) as mo ON st.MainOrderID = mo.ID ";
            sql += " where mo.Status > 8 and CONVERT(float,st.TotalPriceReceive) > 0 and st.RoleID in (3,6) ";
            if (status == 1)
                sql += " AND st.Status=1 ";
            if (status == 2)
                sql += " AND st.Status=2 ";
            if (!string.IsNullOrEmpty(s))
                sql += " AND st.Username Like N'%" + s + "%' ";            
            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += " AND st.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += " AND st.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            sql += " order by st.ID desc OFFSET " + pageIndex + "*" + pageSize + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<StaffInCome> a = new List<StaffInCome>();
            while (reader.Read())
            {
                var entity = new StaffInCome();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["MainOrderID"] != DBNull.Value)
                    entity.MainOrderID = reader["MainOrderID"].ToString().ToInt(0);

                if (reader["OrderTotalPrice"] != DBNull.Value)
                    entity.OrderTotalPrice = Convert.ToDouble(reader["OrderTotalPrice"].ToString());

                if (reader["PercentReceive"] != DBNull.Value)
                    entity.Percent = Convert.ToDouble(reader["PercentReceive"].ToString());

                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);

                if (reader["Username"] != DBNull.Value)
                    entity.UserName = reader["Username"].ToString();

                if (reader["TotalPriceReceive"] != DBNull.Value)
                    entity.TotalPriceReceive = Convert.ToDouble(reader["TotalPriceReceive"].ToString());

                if (reader["Status"] != DBNull.Value)
                {
                    entity.Status = reader["Status"].ToString().ToInt(0);
                    if (reader["Status"].ToString().ToInt(0) == 1)
                    {
                        entity.StatusName = "<span class=\"badge red darken-2 white-text border-radius-2\">Chưa thanh toán</span>";
                    }
                    else
                    {
                        entity.StatusName = "<span class=\"badge green darken-2 white-text border-radius-2\">Đã thanh toán</span>";
                    }
                }

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());

                if (reader["RoleID"] != DBNull.Value)
                {
                    entity.RoleID = reader["RoleID"].ToString().ToInt(0);
                    if (reader["RoleID"].ToString().ToInt(0) == 3)
                    {
                        entity.RoleName = "NV Đặt Hàng";
                    }
                    else
                    {
                        entity.RoleName = "NV Kinh Doanh";
                    }
                }

                a.Add(entity);
            }
            reader.Close();
            return a;
        }

        public partial class StaffInCome
        {
            public int ID { get; set; }
            public int MainOrderID { get; set; }
            public double Percent { get; set; }
            public string UserName { get; set; }
            public double TotalPriceReceive { get; set; }
            public double OrderTotalPrice { get; set; }
            public int UID { get; set; }
            public DateTime CreatedDate { get; set; }
            public int Status { get; set; }
            public string StatusName { get; set; }
            public int RoleID { get; set; }
            public string RoleName { get; set; }

        }
        #endregion

        public static double GetTotalPriceAdmin(int status, string PriceType, string s, string fd, string td)
        {
            var sql = @"select total=SUM(CAST(" + PriceType + " as float)) ";
            sql += "from tbl_StaffIncome st ";
            sql += "left outer join(select * from tbl_MainOder) as mo ON st.MainOrderID = mo.ID ";
            sql += "where mo.Status > 8 AND st.Status = " + status + " ";
            if (!string.IsNullOrEmpty(s))
                sql += "and st.Username Like N'%" + s + "%' ";           
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
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            double total = 0;
            while (reader.Read())
            {
                if (reader["total"] != DBNull.Value)
                    total = Convert.ToDouble(reader["total"].ToString());
            }
            reader.Close();
            return total;
        }
        public static double GetTotalPrice(int UID, int status, string PriceType, string s, string fd, string td)
        {
            var sql = @"select total=SUM(CAST(" + PriceType + " as float)) ";
            sql += "from tbl_StaffIncome st ";
            sql += "left outer join(select * from tbl_MainOder) as mo ON st.MainOrderID = mo.ID ";
            sql += "where st.UID=" + UID + " AND mo.Status > 8 AND st.Status = " + status + " ";
            if (!string.IsNullOrEmpty(s))
                sql += " AND st.Username Like N'%" + s + "%' ";
            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += " AND st.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += " AND st.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            double total = 0;
            while (reader.Read())
            {
                if (reader["total"] != DBNull.Value)
                    total = Convert.ToDouble(reader["total"].ToString());
            }
            reader.Close();
            return total;
        }
        public static List<SQLsumtotal> GetTotal_Sumtotal(int status, string s, string fd, string td)
        {
            var list = new List<SQLsumtotal>();
            var sql = @"SELECT st.Status as status, st.TotalPriceReceive as totalprice from tbl_StaffIncome st ";
            sql += "left outer join(select * from tbl_MainOder) as mo ON st.MainOrderID = mo.ID  ";
            sql += " where mo.Status > 8 and  Convert(float,st.TotalPriceReceive) > 0 and st.RoleID in (3,6)  ";
            if (status == 1)
                sql += " AND st.Status = 1 ";
            if (status == 2)
                sql += " AND st.Status = 2 ";
            if (!string.IsNullOrEmpty(s))
                sql += " AND st.Username Like N'%" + s + "%' ";
            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += " AND st.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += "AND st.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<SQLsumtotal> a = new List<SQLsumtotal>();
            while (reader.Read())
            {
                var entity = new SQLsumtotal();

                if (reader["status"] != DBNull.Value)
                    entity.Status = reader["status"].ToString().ToInt(0);
                if (reader["totalprice"] != DBNull.Value)
                    entity.TotalPriceReceive = reader["totalprice"].ToString();


                a.Add(entity);
            }
            reader.Close();
            return a;
        }

        public class SQLsumtotal
        {
            public int ID { get; set; }
            public int UID { get; set; }
            public int Status { get; set; }
            public string TotalPriceReceive { get; set; }

        }
        public static List<tbl_StaffIncome> GetBy_SQLHelper(int UID, int Status, string fd, string td, int MainOrderID)
        {
            var list = new List<tbl_StaffIncome>();

            var sql = @"select * from tbl_StaffIncome st ";
            sql += " left outer join(select * from tbl_MainOder) as mo ON st.MainOrderID = mo.ID";
            sql += " where mo.Status > 8 and Convert(float, st.TotalPriceReceive) >0 and st.RoleID in (3,6) ";
            if (UID > 0)
                sql += " AND st.UID=" + UID + "";
            if (Status == 1)
                sql += " AND st.Status = 1 ";
            if (Status == 2)
                sql += " AND st.Status = 2 ";
            if (MainOrderID > 0)
                sql += " AND st.MainOrderID=" + MainOrderID + "";

            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += " AND st.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += " AND st.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            sql += " Order By st.ID desc";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                var entity = new tbl_StaffIncome();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["MainOrderID"] != DBNull.Value)
                    entity.MainOrderID = reader["MainOrderID"].ToString().ToInt(0);
                if (reader["OrderTotalPrice"] != DBNull.Value)
                    entity.OrderTotalPrice = reader["OrderTotalPrice"].ToString();
                if (reader["PercentReceive"] != DBNull.Value)
                    entity.PercentReceive = reader["PercentReceive"].ToString();
                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);
                if (reader["Username"] != DBNull.Value)
                    entity.Username = reader["Username"].ToString();
                if (reader["RoleID"] != DBNull.Value)
                    entity.RoleID = reader["RoleID"].ToString().ToInt(0);

                if (reader["Status"] != DBNull.Value)
                {
                    entity.Status = Convert.ToInt32(reader["Status"].ToString());
                }
                if (reader["TotalPriceReceive"] != DBNull.Value)
                    entity.TotalPriceReceive = reader["TotalPriceReceive"].ToString();

                if (reader["OrderCreatedDate"] != DBNull.Value)
                    entity.OrderCreatedDate = Convert.ToDateTime(reader["OrderCreatedDate"]);
                if (reader["IsHidden"] != DBNull.Value)
                    entity.IsHidden = Convert.ToBoolean(reader["IsHidden"]);
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString();
                if (reader["ModifiedDate"] != DBNull.Value)
                    entity.ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"]);

                if (reader["ModifiedBy"] != DBNull.Value)
                    entity.ModifiedBy = reader["ModifiedBy"].ToString();
                list.Add(entity);
            }
            reader.Close();

            return list;
        }
    }
}
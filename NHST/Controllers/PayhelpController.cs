using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using WebUI.Business;
using System.Data;
using MB.Extensions;

namespace NHST.Controllers
{
    public class PayhelpController
    {
        #region CRUD
        public static string Insert(int UID, string Username, string Note, string TotalPrice, string TotalPriceVND, string Currency,
            string CurrencyGiagoc, string TotalPriceVNDGiagoc, int Status, string Phone, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_PayHelp o = new tbl_PayHelp();
                o.UID = UID;
                o.Username = Username;
                o.Note = Note;
                o.TotalPrice = TotalPrice;
                o.TotalPriceVND = TotalPriceVND;
                o.Currency = Currency;
                o.CurrencyGiagoc = CurrencyGiagoc;
                o.TotalPriceVNDGiagoc = TotalPriceVNDGiagoc;
                o.Status = Status;
                o.Phone = Phone;
                o.IsNotComplete = false;
                o.CreatedDate = CreatedDate;
                o.CreatedBy = CreatedBy;
                dbe.tbl_PayHelp.Add(o);
                dbe.SaveChanges();
                int kq = o.ID;
                return kq.ToString();
            }
        }
        public static string Update(int ID, string Note, string TotalPrice, string TotalPriceVND,
            int Status, string Phone, bool IsNotComplete, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var o = dbe.tbl_PayHelp.Where(od => od.ID == ID).FirstOrDefault();
                if (o != null)
                {
                    o.Note = Note;
                    o.TotalPrice = TotalPrice;
                    o.TotalPriceVND = TotalPriceVND;
                    o.Status = Status;
                    o.Phone = Phone;
                    o.IsNotComplete = IsNotComplete;
                    o.ModifiedDate = ModifiedDate;
                    o.ModifiedBy = ModifiedBy;
                    string kq = dbe.SaveChanges().ToString();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        public static string UpdateStatus(int ID, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var o = dbe.tbl_PayHelp.Where(od => od.ID == ID).FirstOrDefault();
                if (o != null)
                {
                    o.Status = Status;
                    o.ModifiedDate = ModifiedDate;
                    o.ModifiedBy = ModifiedBy;
                    string kq = dbe.SaveChanges().ToString();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static tbl_PayHelp GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var o = dbe.tbl_PayHelp.Where(od => od.ID == ID).FirstOrDefault();
                if (o != null)
                {
                    return o;
                }
                else return null;
            }
        }
        public static tbl_PayHelp GetByIDAndUID(int ID, int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                var o = dbe.tbl_PayHelp.Where(od => od.ID == ID && od.UID == UID).FirstOrDefault();
                if (o != null)
                {
                    return o;
                }
                else return null;
            }
        }
        public static List<tbl_PayHelp> GetAll()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_PayHelp> ps = new List<tbl_PayHelp>();
                ps = dbe.tbl_PayHelp.OrderByDescending(p => p.ID).ToList();
                return ps;
            }
        }
        public static List<tbl_PayHelp> GetAllUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_PayHelp> ps = new List<tbl_PayHelp>();
                ps = dbe.tbl_PayHelp.Where(p => p.UID == UID).OrderByDescending(p => p.ID).ToList();
                return ps;
            }
        }
        public static List<tbl_PayHelp> GetAllByFromStatusFromdateToDate(int status, DateTime fromdate, DateTime todate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_PayHelp> os = new List<tbl_PayHelp>();
                os = dbe.tbl_PayHelp.Where(od => od.Status >= status && od.CreatedDate >= fromdate && od.CreatedDate < todate).OrderByDescending(od => od.ID).ToList();
                return os;
            }
        }
        public static List<tbl_PayHelp> GetAllByStatusFromdateToDate(int status, DateTime fromdate, DateTime todate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_PayHelp> os = new List<tbl_PayHelp>();
                os = dbe.tbl_PayHelp.Where(od => od.Status == status && od.CreatedDate >= fromdate && od.CreatedDate < todate).OrderByDescending(od => od.ID).ToList();
                return os;
            }
        }
        #endregion

        #region New

        public static int GetTotalPage(string s, int st, string fd, string td)
        {
            var sql = @"select Total=Count(*) ";
            sql += "from tbl_PayHelp as p ";
            sql += "left outer join (select * from tbl_AccountInfo) as Ai ON Ai.UID = p.UID ";
            sql += "where Username like N'%" + s + "%' ";
            if (st > -1)
                sql += " and status=" + st;
            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += "AND p.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += "AND p.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int a = 0;
            while (reader.Read())
            {
                if (reader["Total"] != DBNull.Value)
                    a = Convert.ToInt32(reader["Total"]);
            }
            reader.Close();
            return a;
        }

        public static List<Danhsachyeucau> GetByUserInSQLHelper_nottextnottypeWithstatus(int page, int maxrows, string s, int st, string fd, string td, int sort)
        {
            var list = new List<Danhsachyeucau>();

            var sql = @"select p.ID, p.Username, p.TotalPrice, p.TotalPriceVND, p.Currency, p.Status, p.CreatedDate, p.IsNotComplete, Ai.Phone, ";
            sql += "            CASE   WHEN p.Status = 0 THEN N'<span class=\"badge red darken-2 white-text border-radius-2\">Chưa thanh toán</span>'  ";
            sql += "WHEN p.Status = 1 THEN N'<span class=\"badge blue darken-2 white-text border-radius-2\">Đã thanh toán</span>' ";
            sql += "WHEN p.Status = 2 THEN N'<span class=\"badge black darken-2 white-text border-radius-2\">Đã hủy</span>' ";
            sql += "WHEN p.Status = 3 THEN N'<span class=\"badge green darken-2 white-text border-radius-2\">Đã hoàn thành</span>' ";
            sql += "ELSE N'<span class=\"badge yellow darken-2 white-text border-radius-2\">Đã xác nhận</span>' END AS statusstring ";
            sql += "from tbl_PayHelp as p ";
            sql += "left outer join (select * from tbl_AccountInfo) as Ai ON Ai.UID = p.UID ";
            sql += "where p.Username like N'%" + s + "%' ";

            if (st > -1)
            {
                sql += " And p.Status=" + st + "";
            }

            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += "AND p.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += "AND p.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }


            //sql += " ORDER BY ID DESC OFFSET (" + page + " * " + maxrows + ") ROWS FETCH NEXT " + maxrows + " ROWS ONLY";

            if (sort == 0)
            {
                sql += " ORDER BY ID DESC OFFSET (" + page + " * " + maxrows + ") ROWS FETCH NEXT " + maxrows + " ROWS ONLY";
            }
            else if (sort == 1)
            {
                sql += " ORDER BY ID ASC OFFSET (" + page + " * " + maxrows + ") ROWS FETCH NEXT " + maxrows + " ROWS ONLY";
            }
            else if (sort == 2)
            {
                sql += " ORDER BY ID DESC OFFSET (" + page + " * " + maxrows + ") ROWS FETCH NEXT " + maxrows + " ROWS ONLY";
            }
            else if (sort == 3)
            {
                sql += " ORDER BY Status ASC OFFSET (" + page + " * " + maxrows + ") ROWS FETCH NEXT " + maxrows + " ROWS ONLY";
            }
            else if (sort == 4)
            {
                sql += " ORDER BY Status DESC OFFSET (" + page + " * " + maxrows + ") ROWS FETCH NEXT " + maxrows + " ROWS ONLY";
            }

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                var entity = new Danhsachyeucau();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["TotalPriceVND"] != DBNull.Value)
                {
                    entity.TotalPriceVND = Convert.ToDouble(reader["TotalPriceVND"]);
                    entity.TotalPriceVND_String = string.Format("{0:N0}", Convert.ToDouble(reader["TotalPriceVND"].ToString())).Replace(",", ".");
                }


                if (reader["TotalPrice"] != DBNull.Value)
                    entity.TotalPriceCYN = Convert.ToDouble(reader["TotalPrice"]);
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = string.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(reader["CreatedDate"].ToString()));

                if (reader["statusstring"] != DBNull.Value)
                {
                    entity.statusstring = reader["statusstring"].ToString();
                }

                if (reader["Username"] != DBNull.Value)
                    entity.Username = reader["Username"].ToString();

                if (reader["Currency"] != DBNull.Value)
                    entity.Currency = Convert.ToDouble(reader["Currency"]);

                if (reader["IsNotComplete"] != DBNull.Value)
                    entity.IsNotComplete = reader["IsNotComplete"].ToString();
                if (reader["Phone"] != DBNull.Value)
                    entity.Phone = reader["Phone"].ToString();

                list.Add(entity);
            }
            reader.Close();

            return list;
        }

        public static string Report_TotalItem(int pricefrom, int priceto)
        {
            var sql = @"select Count(*) as Total from tbl_Account as ac";
            sql += " left outer join(select Sum(CONVERT(numeric(18, 2), TotalPriceVND)) as Total, UID from tbl_PayHelp group by UID) as p ON p.UID = ac.ID ";
            sql += " where ac.RoleID = 1 and p.Total > " + pricefrom + " and p.Total < " + priceto + " ";

            string Total = "0";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                if (reader["Total"] != DBNull.Value)
                    Total = reader["Total"].ToString();
            }

            reader.Close();
            return Total;
        }

        public class Danhsachyeucau
        {
            public int ID { get; set; }
            public string Username { get; set; }
            public string Phone { get; set; }
            public double TotalPriceCYN { get; set; }
            public string TotalPriceCYN_String { get; set; }
            public double TotalPriceVND { get; set; }
            public double Currency { get; set; }
            public object IsNotComplete { get; set; }
            public string TotalPriceVND_String { get; set; }
            public string statusstring { get; set; }
            public string CreatedDate { get; set; }
        }
        #endregion

        public static List<tbl_PayHelp> GetAllByFromStatusFromdateToDateBySQL(int status, string fd, string td, int pageIndex, int pageSize)
        {
            var sql = @"select * ";
            sql += "from dbo.tbl_PayHelp ";
            sql += "Where Status > = " + status + " ";
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
            sql += " ORDER BY CreatedDate DESC, Status desc OFFSET (" + pageIndex + " * " + pageSize + ") ROWS FETCH NEXT " + pageSize + " ROWS ONLY ";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<tbl_PayHelp> list = new List<tbl_PayHelp>();
            while (reader.Read())
            {
                var entity = new tbl_PayHelp();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["Username"] != DBNull.Value)
                    entity.Username = reader["Username"].ToString();
                entity.TotalPriceVND = "0";
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();

                entity.TotalPrice = "0";
                if (reader["TotalPrice"] != DBNull.Value)
                    entity.TotalPrice = reader["TotalPrice"].ToString();

                entity.TotalPriceVND = "0";
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();


                entity.TotalPriceVND = "0";
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();


                entity.TotalPriceVNDGiagoc = "0";
                if (reader["TotalPriceVNDGiagoc"] != DBNull.Value)
                    entity.TotalPriceVNDGiagoc = reader["TotalPriceVNDGiagoc"].ToString();


                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt();

                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt();

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());

                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString();
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static int GetTotalByStatusFromdateToDateBySQL(int status, string fd, string td)
        {
            var sql = @"select Total=Count(*) ";
            sql += "from dbo.tbl_PayHelp ";
            sql += "Where Status > = " + status + " ";
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
        public static double GetTotalPriceByStatusFromdateToDateBySQL(int status, string fd, string td, string col)
        {
            var sql = @"select Total=Count(*) ";
            sql += ", TotalPrice=SUM(CAST(" + col + " as Float)) ";
            sql += "from dbo.tbl_PayHelp ";
            sql += "Where Status > = " + status + " ";
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
            double a = 0;
            while (reader.Read())
            {

                if (reader["TotalPrice"] != DBNull.Value)
                    a = Convert.ToDouble(reader["TotalPrice"].ToString());

            }
            reader.Close();
            return a;
        }
    }
}
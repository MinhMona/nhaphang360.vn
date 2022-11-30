using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using System.Globalization;
using WebUI.Business;
using System.Data;
using MB.Extensions;

namespace NHST.Controllers
{
    public class AdminSendUserWalletController
    {

        #region CRUD
        public static string Insert(int UID, string Username, double Amount, int Status, int BankID, string Content, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_AdminSendUserWallet a = new tbl_AdminSendUserWallet();
                a.UID = UID;
                a.BankID = BankID;
                a.Username = Username;
                a.Amount = Amount;
                a.Status = Status;
                a.TradeContent = Content;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_AdminSendUserWallet.Add(a);
                int kq = dbe.SaveChanges();
                //string k = kq + "|" + user.ID;
                string k = a.ID.ToString();
                return k;
            }
        }

        public static string UpdateCongNo(int ID, bool IsLoan, bool IsPayLoan)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_AdminSendUserWallet a = dbe.tbl_AdminSendUserWallet.Where(ad => ad.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.IsLoan = IsLoan;
                    a.IsPayLoan = IsPayLoan;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }

        public static string UpdateStatus(int ID, int Status, string Content, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_AdminSendUserWallet a = dbe.tbl_AdminSendUserWallet.Where(ad => ad.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.Status = Status;
                    a.TradeContent = Content;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }


        #endregion
        #region Select
        public static tbl_AdminSendUserWallet GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_AdminSendUserWallet a = dbe.tbl_AdminSendUserWallet.Where(ad => ad.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static List<tbl_AdminSendUserWallet> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_AdminSendUserWallet> aus = new List<tbl_AdminSendUserWallet>();
                aus = dbe.tbl_AdminSendUserWallet.Where(a => a.Username.Contains(s)).OrderByDescending(a => a.ID).ToList();
                return aus;
            }
        }
        public static List<tbl_AdminSendUserWallet> GetFromDateToDate(DateTime from, DateTime to)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_AdminSendUserWallet> lo = new List<tbl_AdminSendUserWallet>();

                var alllist = dbe.tbl_AdminSendUserWallet.OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();

                if (!string.IsNullOrEmpty(from.ToString()) && !string.IsNullOrEmpty(to.ToString()))
                {
                    lo = alllist.Where(t => t.CreatedDate >= from && t.CreatedDate <= to).OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();
                }
                else if (!string.IsNullOrEmpty(from.ToString()) && string.IsNullOrEmpty(to.ToString()))
                {
                    lo = alllist.Where(t => t.CreatedDate >= from).OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();
                }
                else if (string.IsNullOrEmpty(from.ToString()) && !string.IsNullOrEmpty(to.ToString()))
                {
                    lo = alllist.Where(t => t.CreatedDate <= to).OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();
                }
                else
                {
                    lo = alllist;
                }
                if (lo.Count > 0)
                    return lo.Where(l => l.Status == 2).ToList();
                else return lo;
            }
        }
        public static List<tbl_AdminSendUserWallet> GetByCreatedBy(string s, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_AdminSendUserWallet> aus = new List<tbl_AdminSendUserWallet>();
                aus = dbe.tbl_AdminSendUserWallet.Where(a => a.Username.Contains(s) && a.CreatedBy == CreatedBy).OrderByDescending(a => a.ID).ToList();
                return aus;
            }
        }
        public static List<tbl_AdminSendUserWallet> GetByUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_AdminSendUserWallet> aus = new List<tbl_AdminSendUserWallet>();
                aus = dbe.tbl_AdminSendUserWallet.Where(a => a.UID == UID).OrderByDescending(a => a.ID).ToList();
                return aus;
            }
        }

        public static tbl_AdminSendUserWallet GetByUID_New(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_AdminSendUserWallet a = dbe.tbl_AdminSendUserWallet.Where(ad => ad.UID == UID).FirstOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        #endregion


        public static int GetTotalList(string UserName, string st, string fd, string td, string ispayloan)
        {
            var sql = @"select Total=COUNT(*) ";
            sql += "from tbl_AdminSendUserWallet ";
            sql += "where Username like N'%" + UserName + "%' ";
            if (!string.IsNullOrEmpty(st))
            {
                if (st != "0")
                {
                    sql += "and Status=" + st + " ";
                }
            }

            if (ispayloan != "-1")
            {
                sql += "and IsLoan=1 and  IsPayLoan=" + ispayloan + " ";
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

        public static double GetTotalAmount(string UserName, string st, string fd, string td, string ispayloan)
        {
            var sql = @"select Sum(Amount) as TotalAmount ";
            sql += "from tbl_AdminSendUserWallet ";
            sql += "where Username like N'%" + UserName + "%' ";
            if (!string.IsNullOrEmpty(st))
            {
                if (st != "0")
                {
                    sql += "and Status=" + st + " ";
                }
            }

            sql += "and mo.IsLoan=1 and  mo.IsPayLoan=" + ispayloan + " ";

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
                if (reader["TotalAmount"] != DBNull.Value)
                    a = Convert.ToDouble(reader["TotalAmount"].ToString());
            }
            reader.Close();
            return a;
        }

        public static List<ListShowNew> GetBySQL_DK(string UserName, string st, int pageIndex, string fd, string td, int pageSize, string ispayloan)
        {
            var sql = @"Select mo.ID, mo.UID, mo.Username, mo.IsLoan, mo.IsPayLoan, mo.Amount, mo.Status, mo.CreatedDate,mo.CreatedBy, mo.BankID ,o.TenNH ,o.ChiNhanh ,o.TenTK ,o.SoTK ";
            sql += "from tbl_AdminSendUserWallet as mo left outer join  ";
            sql += "(SELECT ID, BankName as TenNH, AccountHolder as TenTK, Branch as ChiNhanh, BankNumber as SoTK, ROW_NUMBER() OVER(PARTITION BY ID ORDER BY(SELECT NULL)) AS RowNum FROM tbl_Bank) o ON o.ID = mo.BankID ";
            sql += "where mo.Username LIKE '%" + UserName + "%' ";
            if (!string.IsNullOrEmpty(st))
            {
                if (st != "0")
                {
                    sql += "and mo.Status=" + st + " ";
                }

            }

            if (ispayloan != "-1")
            {
                sql += "and mo.IsLoan=1 and  mo.IsPayLoan=" + ispayloan + " ";
            }

            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += "AND mo.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += "AND mo.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            sql += " order by mo.ID DESC OFFSET " + pageIndex + "*" + pageSize + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<ListShowNew> a = new List<ListShowNew>();
            while (reader.Read())
            {
                var entity = new ListShowNew();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);

                if (reader["Username"] != DBNull.Value)
                    entity.UserName = reader["Username"].ToString();

                if (reader["IsLoan"] != DBNull.Value)
                    entity.IsLoan = Convert.ToBoolean(reader["IsLoan"].ToString());
                else
                    entity.IsLoan = false;

                if (reader["IsPayLoan"] != DBNull.Value)
                    entity.IsPayLoan = Convert.ToBoolean(reader["IsPayLoan"].ToString());
                else
                    entity.IsPayLoan = false;

                if (reader["Amount"] != DBNull.Value)
                {
                    entity.Amount = Convert.ToDouble(reader["Amount"].ToString());
                    entity.AmountString = Convert.ToDouble(reader["Amount"].ToString()).ToString("0,0", CultureInfo.InvariantCulture) + " VND";
                }

                if (reader["Status"] != DBNull.Value)
                {
                    entity.Status = reader["Status"].ToString().ToInt(0);

                    if (reader["Status"].ToString().ToInt(0) == 1)
                        entity.StatusName = "<span class=\"badge orange darken-2 white-text border-radius-2\">Chờ duyệt</span>";
                    if (reader["Status"].ToString().ToInt(0) == 2)
                        entity.StatusName = "<span class=\"badge green darken-2 white-text border-radius-2\">Đã duyệt</span>";
                    if (reader["Status"].ToString().ToInt(0) == 3)
                        entity.StatusName = "<span class=\"badge red darken-2 white-text border-radius-2\">Đã hủy</span>";
                }
                if (reader["BankID"] != DBNull.Value)
                {
                    entity.BankID = reader["BankID"].ToString().ToInt(0);
                }
                if (reader["TenNH"] != DBNull.Value)
                {
                    entity.BankName = reader["TenNH"].ToString();
                }
                if (reader["ChiNhanh"] != DBNull.Value)
                {
                    entity.BankBranch = reader["ChiNhanh"].ToString();
                }
                if (reader["SoTK"] != DBNull.Value)
                {
                    entity.BankNumber = reader["SoTK"].ToString();
                }
                if (reader["TenTK"] != DBNull.Value)
                {
                    entity.BankAccountHolder = reader["TenTK"].ToString();
                }
                if (reader["CreatedDate"] != DBNull.Value)
                {
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                    entity.CreatedDateString = Convert.ToDateTime(reader["CreatedDate"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");

                }
                if (reader["CreatedBy"] != DBNull.Value)
                {
                    entity.CreatedBy = reader["CreatedBy"].ToString();
                }
                a.Add(entity);

            }
            reader.Close();
            return a;
        }
        public static double GetTotalPriceWithBank(string UserName, string st, string bank, string fd, string td)
        {
            var sql = @"select Total=Sum(Amount) ";
            sql += "from tbl_AdminSendUserWallet ";
            sql += "where Username like N'%" + UserName + "%' ";
            if (!string.IsNullOrEmpty(st))
            {
                if (st != "0")
                {
                    sql += "and Status=" + st + " ";
                }
            }
            if (!string.IsNullOrEmpty(bank))
            {
                if (bank != "0")
                {
                    sql += "and BankID=" + bank + " ";
                }
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
            double a = 0;
            while (reader.Read())
            {
                if (reader["Total"] != DBNull.Value)
                    a = Convert.ToDouble(reader["Total"].ToString());
            }
            reader.Close();
            return a;
        }
        public static List<ListShowNew> GetBySQL_DK(string UserName, int pageIndex, string fd, string td, int pageSize)
        {
            var sql = @"select ID, UID, Username, Amount, Status, CreatedDate ";
            sql += "from tbl_AdminSendUserWallet ";

            sql += "where Username LIKE '%" + UserName + "%' ";

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
            List<ListShowNew> a = new List<ListShowNew>();
            while (reader.Read())
            {
                var entity = new ListShowNew();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);

                if (reader["Username"] != DBNull.Value)
                    entity.UserName = reader["Username"].ToString();

                if (reader["Amount"] != DBNull.Value)
                {
                    entity.Amount = Convert.ToDouble(reader["Amount"].ToString());
                    entity.AmountString = Convert.ToDouble(reader["Amount"].ToString()).ToString("0,0", CultureInfo.InvariantCulture) + " VND";
                }

                if (reader["Status"] != DBNull.Value)
                {
                    entity.Status = reader["Status"].ToString().ToInt(0);

                    if (reader["Status"].ToString().ToInt(0) == 1)
                        entity.StatusName = "<span class=\"badge orange darken-2 white-text border-radius-2\">Đang chờ</span>";
                    if (reader["Status"].ToString().ToInt(0) == 2)
                        entity.StatusName = "<span class=\"badge green darken-2 white-text border-radius-2\">Duyệt</span>";
                    if (reader["Status"].ToString().ToInt(0) == 3)
                        entity.StatusName = "<span class=\"badge red darken-2 white-text border-radius-2\">Hủy</span>";
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
        public static List<ListShowNew> GetBySQL_DK_WithBank(string UserName, string st, string bank, int pageIndex, string fd, string td, int pageSize)
        {
            var sql = @"Select mo.ID, mo.UID, mo.Username, mo.Amount, mo.Status, mo.CreatedDate,mo.CreatedBy, mo.BankID ,o.TenNH ,o.ChiNhanh ,o.TenTK ,o.SoTK ";
            sql += "from tbl_AdminSendUserWallet as mo left outer join  ";
            sql += "(SELECT ID, BankName as TenNH, AccountHolder as TenTK, Branch as ChiNhanh, BankNumber as SoTK, ROW_NUMBER() OVER(PARTITION BY ID ORDER BY(SELECT NULL)) AS RowNum FROM tbl_Bank) o ON o.ID = mo.BankID ";
            sql += "where mo.Username LIKE '%" + UserName + "%' ";
            if (!string.IsNullOrEmpty(st))
            {
                if (st != "0")
                {
                    sql += "and mo.Status=" + st + " ";
                }

            }
            if (!string.IsNullOrEmpty(bank))
            {
                if (bank != "0")
                {
                    sql += "and BankID=" + bank + " ";
                }
            }
            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += "AND mo.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += "AND mo.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            sql += "order by mo.id DESC OFFSET " + pageIndex + "*" + pageSize + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<ListShowNew> a = new List<ListShowNew>();
            while (reader.Read())
            {
                var entity = new ListShowNew();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);

                if (reader["Username"] != DBNull.Value)
                    entity.UserName = reader["Username"].ToString();

                if (reader["Amount"] != DBNull.Value)
                {
                    entity.Amount = Convert.ToDouble(reader["Amount"].ToString());
                    entity.AmountString = Convert.ToDouble(reader["Amount"].ToString()).ToString("0,0", CultureInfo.InvariantCulture) + " VND";
                }

                if (reader["Status"] != DBNull.Value)
                {
                    entity.Status = reader["Status"].ToString().ToInt(0);

                    if (reader["Status"].ToString().ToInt(0) == 1)
                        entity.StatusName = "<span class=\"badge orange darken-2 white-text border-radius-2\">Chờ duyệt</span>";
                    if (reader["Status"].ToString().ToInt(0) == 2)
                        entity.StatusName = "<span class=\"badge green darken-2 white-text border-radius-2\">Đã duyệt</span>";
                    if (reader["Status"].ToString().ToInt(0) == 3)
                        entity.StatusName = "<span class=\"badge red darken-2 white-text border-radius-2\">Đã hủy</span>";
                }
                if (reader["BankID"] != DBNull.Value)
                {
                    entity.BankID = reader["BankID"].ToString().ToInt(0);
                }
                if (reader["TenNH"] != DBNull.Value)
                {
                    entity.BankName = reader["TenNH"].ToString();
                }
                if (reader["ChiNhanh"] != DBNull.Value)
                {
                    entity.BankBranch = reader["ChiNhanh"].ToString();
                }
                if (reader["SoTK"] != DBNull.Value)
                {
                    entity.BankNumber = reader["SoTK"].ToString();
                }
                if (reader["TenTK"] != DBNull.Value)
                {
                    entity.BankAccountHolder = reader["TenTK"].ToString();
                }
                if (reader["CreatedDate"] != DBNull.Value)
                {
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                    entity.CreatedDateString = Convert.ToDateTime(reader["CreatedDate"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");

                }
                if (reader["CreatedBy"] != DBNull.Value)
                {
                    entity.CreatedBy = reader["CreatedBy"].ToString();
                }
                a.Add(entity);

            }
            reader.Close();
            return a;
        }

        public static int GetTotalListWithBank(string UserName, string st, string bank, string fd, string td)
        {
            var sql = @"select Total=COUNT(*) ";
            sql += "from tbl_AdminSendUserWallet ";
            sql += "where Username like N'%" + UserName + "%' ";
            if (!string.IsNullOrEmpty(st))
            {
                if (st != "0")
                {
                    sql += "and Status=" + st + " ";
                }
            }
            if (!string.IsNullOrEmpty(bank))
            {
                if (bank != "0")
                {
                    sql += "and BankID=" + bank + " ";
                }
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
        public partial class ListShowNew
        {
            public int ID { get; set; }
            public int UID { get; set; }
            public string UserName { get; set; }
            public double Amount { get; set; }
            public string AmountString { get; set; }
            public string DonVi { get; set; }
            public int Status { get; set; }
            public string StatusName { get; set; }
            public int BankID { get; set; }
            public string BankName { get; set; }
            public string BankBranch { get; set; }
            public string BankAccountHolder { get; set; }
            public string BankNumber { get; set; }
            public DateTime CreatedDate { get; set; }
            public string CreatedDateString { get; set; }
            public string CreatedBy { get; set; }
            public bool IsLoan { get; set; }
            public bool IsPayLoan { get; set; }
        }
    }
}
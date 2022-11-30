using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using System.Data;
using WebUI.Business;
using MB.Extensions;

namespace NHST.Controllers
{
    public class HistoryPayWalletController
    {
        #region CRUD
        public static string Insert(int UID, string UserName, int MainOrderID, double Amount, string HContent, double MoneyLeft, int Type, int TradeType,
            DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_HistoryPayWallet a = new tbl_HistoryPayWallet();
                a.UID = UID;
                a.UserName = UserName;
                a.MainOrderID = MainOrderID;
                a.Amount = Amount;
                a.HContent = HContent;
                a.MoneyLeft = MoneyLeft;
                a.Type = Type;
                a.TradeType = TradeType;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_HistoryPayWallet.Add(a);
                int kq = dbe.SaveChanges();
                //string k = kq + "|" + user.ID;
                string k = a.ID.ToString();
                return k;
            }
        }
        public static string InsertTransportation(int UID, string UserName, int MainOrderID, double Amount, string HContent, double MoneyLeft, int Type, int TradeType,
            DateTime CreatedDate, string CreatedBy, int TransportationOrderID)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_HistoryPayWallet a = new tbl_HistoryPayWallet();
                a.UID = UID;
                a.UserName = UserName;
                a.MainOrderID = MainOrderID;
                a.Amount = Amount;
                a.HContent = HContent;
                a.MoneyLeft = MoneyLeft;
                a.Type = Type;
                a.TradeType = TradeType;
                a.TransportationOrderID = TransportationOrderID;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_HistoryPayWallet.Add(a);
                int kq = dbe.SaveChanges();
                string k = a.ID.ToString();
                return k;
            }
        }
        public static string InsertTransport(int UID, string UserName, double Amount, string HContent, double MoneyLeft, DateTime DateSend, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_HistoryPayWallet a = new tbl_HistoryPayWallet();
                a.UID = UID;
                a.UserName = UserName;
                a.MainOrderID = 0;
                a.Amount = Amount;
                a.HContent = HContent;
                a.MoneyLeft = MoneyLeft;
                a.Type = 1;
                a.TradeType = 8;
                a.DateSend = DateSend;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_HistoryPayWallet.Add(a);
                int kq = dbe.SaveChanges();
                //string k = kq + "|" + user.ID;
                string k = a.ID.ToString();
                return k;
            }
        }
        #endregion
        #region Select
        public static List<tbl_HistoryPayWallet> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_HistoryPayWallet> aus = new List<tbl_HistoryPayWallet>();
                aus = dbe.tbl_HistoryPayWallet.Where(a => a.UserName.Contains(s)).OrderByDescending(a => a.ID).ToList();
                return aus;
            }
        }
        public static List<tbl_HistoryPayWallet> GetByUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_HistoryPayWallet> aus = new List<tbl_HistoryPayWallet>();
                aus = dbe.tbl_HistoryPayWallet.Where(a => a.UID == UID).OrderByDescending(a => a.ID).ToList();
                return aus;
            }
        }
        public static List<tbl_HistoryPayWallet> GetByUIDASC(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_HistoryPayWallet> aus = new List<tbl_HistoryPayWallet>();
                aus = dbe.tbl_HistoryPayWallet.Where(a => a.UID == UID).OrderByDescending(a => a.ID).ToList();
                return aus;
            }
        }
        public static List<tbl_HistoryPayWallet> GetByMainOrderID(int MainOrderID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_HistoryPayWallet> aus = new List<tbl_HistoryPayWallet>();
                aus = dbe.tbl_HistoryPayWallet.Where(a => a.MainOrderID == MainOrderID).OrderByDescending(a => a.ID).ToList();
                return aus;
            }
        }

        public static List<tbl_HistoryPayWallet> GetFromDateTodate(DateTime from, DateTime to)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_HistoryPayWallet> lo = new List<tbl_HistoryPayWallet>();

                var alllist = dbe.tbl_HistoryPayWallet.OrderByDescending(t => t.CreatedDate).ToList();

                if (!string.IsNullOrEmpty(from.ToString()) && !string.IsNullOrEmpty(to.ToString()))
                {
                    lo = alllist.Where(t => t.CreatedDate >= from && t.CreatedDate <= to).OrderByDescending(t => t.CreatedDate).ToList();
                }
                else if (!string.IsNullOrEmpty(from.ToString()) && string.IsNullOrEmpty(to.ToString()))
                {
                    lo = alllist.Where(t => t.CreatedDate >= from).OrderByDescending(t => t.CreatedDate).ToList();
                }
                else if (string.IsNullOrEmpty(from.ToString()) && !string.IsNullOrEmpty(to.ToString()))
                {
                    lo = alllist.Where(t => t.CreatedDate <= to).OrderByDescending(t => t.CreatedDate).ToList();
                }
                else
                {
                    lo = alllist;
                }
                return lo;
            }
        }
        public static List<tbl_HistoryPayWallet> GetByUIDTradeTypeDateSend(int UID, int TradeType, DateTime DateSend)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_HistoryPayWallet> aus = new List<tbl_HistoryPayWallet>();
                aus = dbe.tbl_HistoryPayWallet.Where(a => a.UID == UID && a.TradeType == TradeType && a.DateSend == DateSend).OrderByDescending(a => a.ID).ToList();
                return aus;
            }
        }
        #endregion

        public static List<tbl_HistoryPayWallet> GetByUID_SQL(int UID)
        {
            var list = new List<tbl_HistoryPayWallet>();
            var sql = @"select * from tbl_HistoryPayWallet ";
            sql += " where UID = " + UID + "";
            sql += " Order By ID desc";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);

            int i = 1;
            while (reader.Read())
            {
                var entity = new tbl_HistoryPayWallet();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);
                if (reader["UserName"] != DBNull.Value)
                    entity.UserName = reader["UserName"].ToString();
                if (reader["MainOrderID"] != DBNull.Value)
                    entity.MainOrderID = reader["MainOrderID"].ToString().ToInt(0);
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                if (reader["Amount"] != DBNull.Value)
                    entity.Amount = Convert.ToDouble(reader["Amount"].ToString());
                if (reader["HContent"] != DBNull.Value)
                    entity.HContent = reader["HContent"].ToString();
                if (reader["MoneyLeft"] != DBNull.Value)
                    entity.MoneyLeft = Convert.ToDouble(reader["MoneyLeft"].ToString());
                if (reader["Type"] != DBNull.Value)
                    entity.Type = reader["Type"].ToString().ToInt(0);
                if (reader["TradeType"] != DBNull.Value)
                    entity.TradeType = reader["TradeType"].ToString().ToInt(0);

                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString();

                if (reader["TransportationOrderID"] != DBNull.Value)
                    entity.TransportationOrderID = reader["TransportationOrderID"].ToString().ToInt(0);

                if (reader["DateSend"] != DBNull.Value)
                    entity.DateSend = Convert.ToDateTime(reader["DateSend"].ToString());

                i++;
                list.Add(entity);
            }
            reader.Close();
            return list;

        }

        #region New
        public static int GetTotal_DK(int status, string UID, string fd, string td)
        {
            var sql = @"select Total=Count(*) ";
            sql += "from tbl_HistoryPayWallet ";
            sql += "where UID='" + UID + "' ";

            if (status > -1)
                sql += " AND TradeType = " + status;
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
            reader.Close();
            return a;
        }
        public static List<tbl_HistoryPayWallet> GetBySQL_DK(int status,string UID, string fd, string td, int pageSize, int pageIndex)
        {
            var sql = @"select * ";
            sql += "from tbl_HistoryPayWallet ";
            sql += "where UID='" + UID + "' ";

            if (status > -1)
                sql += " AND TradeType = " + status;
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
            List<tbl_HistoryPayWallet> a = new List<tbl_HistoryPayWallet>();
            while (reader.Read())
            {
                var entity = new tbl_HistoryPayWallet();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);

                if (reader["UserName"] != DBNull.Value)
                    entity.UserName = reader["UserName"].ToString();

                if (reader["MainOrderID"] != DBNull.Value)
                    entity.MainOrderID = reader["MainOrderID"].ToString().ToInt(0);

                if (reader["Amount"] != DBNull.Value)
                    entity.Amount = Convert.ToDouble(reader["Amount"].ToString());

                if (reader["HContent"] != DBNull.Value)
                    entity.HContent = reader["HContent"].ToString();

                if (reader["MoneyLeft"] != DBNull.Value)
                    entity.MoneyLeft = Convert.ToDouble(reader["MoneyLeft"].ToString());

                if (reader["Type"] != DBNull.Value)
                    entity.Type = reader["Type"].ToString().ToInt(1);

                if (reader["TradeType"] != DBNull.Value)
                    entity.TradeType = reader["TradeType"].ToString().ToInt(1);

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                a.Add(entity);
            }
            reader.Close();
            return a;
        }

        public static List<tbl_HistoryPayWallet> GetBySQL_DK_NoPage(int status, string UID, string fd, string td)
        {
            var sql = @"select * ";
            sql += "from tbl_HistoryPayWallet ";
            sql += "where UID='" + UID + "' ";

            if (status > -1)
                sql += " AND TradeType = " + status;
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
            sql += "order by id DESC ";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<tbl_HistoryPayWallet> a = new List<tbl_HistoryPayWallet>();
            while (reader.Read())
            {
                var entity = new tbl_HistoryPayWallet();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);

                if (reader["UserName"] != DBNull.Value)
                    entity.UserName = reader["UserName"].ToString();

                if (reader["MainOrderID"] != DBNull.Value)
                    entity.MainOrderID = reader["MainOrderID"].ToString().ToInt(0);

                if (reader["Amount"] != DBNull.Value)
                    entity.Amount = Convert.ToDouble(reader["Amount"].ToString());

                if (reader["HContent"] != DBNull.Value)
                    entity.HContent = reader["HContent"].ToString();

                if (reader["MoneyLeft"] != DBNull.Value)
                    entity.MoneyLeft = Convert.ToDouble(reader["MoneyLeft"].ToString());

                if (reader["Type"] != DBNull.Value)
                    entity.Type = reader["Type"].ToString().ToInt(1);

                if (reader["TradeType"] != DBNull.Value)
                    entity.TradeType = reader["TradeType"].ToString().ToInt(1);

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                a.Add(entity);
            }
            reader.Close();
            return a;
        }

        public static List<tbl_HistoryPayWallet> GetFromDateTodate_BySQL(string type, string fd, string td, int pageSize, int pageIndex)
        {

            var sql = @"select * ";
            sql += "from tbl_HistoryPayWallet ";
            sql += "where UID Like N'%%' ";
            if (!string.IsNullOrEmpty(type))
            {
                sql += " And TradeType =" + type + " ";
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
            sql += "order by CreatedDate DESC OFFSET " + pageIndex + "*" + pageSize + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<tbl_HistoryPayWallet> a = new List<tbl_HistoryPayWallet>();
            while (reader.Read())
            {
                var entity = new tbl_HistoryPayWallet();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);

                if (reader["UserName"] != DBNull.Value)
                    entity.UserName = reader["UserName"].ToString();

                if (reader["MainOrderID"] != DBNull.Value)
                    entity.MainOrderID = reader["MainOrderID"].ToString().ToInt(0);

                if (reader["Amount"] != DBNull.Value)
                    entity.Amount = Convert.ToDouble(reader["Amount"].ToString());

                if (reader["HContent"] != DBNull.Value)
                    entity.HContent = reader["HContent"].ToString();

                if (reader["MoneyLeft"] != DBNull.Value)
                    entity.MoneyLeft = Convert.ToDouble(reader["MoneyLeft"].ToString());

                if (reader["Type"] != DBNull.Value)
                    entity.Type = reader["Type"].ToString().ToInt(1);

                if (reader["TradeType"] != DBNull.Value)
                    entity.TradeType = reader["TradeType"].ToString().ToInt(1);

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                a.Add(entity);
            }
            reader.Close();
            return a;
        }

        public static int GetTotalAll_BySQL(string type, string fd, string td)
        {
            var sql = @"select Total=Count(*) ";
            sql += "from tbl_HistoryPayWallet ";
            sql += "where UID Like N'%%' ";
            if (!string.IsNullOrEmpty(type))
            {
                sql += " And TradeType =" + type + " ";
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
            reader.Close();
            return a;
        }
        public static double GetTotalAllAmount_BySQL(string type, string fd, string td)
        {
            var sql = @"select Total=Sum(Amount) ";
            sql += "from tbl_HistoryPayWallet ";
            sql += "where UID Like N'%%' ";
            if (!string.IsNullOrEmpty(type))
            {
                sql += " And TradeType =" + type + " ";
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
            double a = 0;
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                if (reader["Total"] != DBNull.Value)
                    a = Convert.ToDouble(reader["Total"].ToString());
            }
            reader.Close();
            return a;
        }


        public static int GetTotalAll_BySQLUser(int Status, string fd, string td, int UID)
        {
            var sql = @"select Total=Count(*) ";
            sql += "from tbl_HistoryPayWallet ";
            sql += "where UID Like N'%%' ";
            if (Status > -1)
            {
                if (Status == 1)
                {
                    sql += " And TradeType =1 ";
                }
                else if (Status == 2)
                {
                    sql += " And TradeType =3 ";
                }
                else if (Status == 3)
                {
                    sql += " And Type =2 ";
                }
                else
                {
                    sql += " And Type =1 ";
                }
            }

            sql += " And UID = " + UID + "";

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
            reader.Close();
            return a;
        }

        public static List<tbl_HistoryPayWallet> GetFromDateTodate_BySQLUser(int Status, string fd, string td, int pageSize, int pageIndex, int UID)
        {

            var sql = @"select * ";
            sql += "from tbl_HistoryPayWallet ";
            sql += "where UID Like N'%%' ";
            if (Status > -1)
            {
                if (Status == 1)
                {
                    sql += " And TradeType =1 ";
                }
                else if (Status == 2)
                {
                    sql += " And TradeType =3 ";
                }
                else if (Status == 3)
                {
                    sql += " And Type =2 ";
                }
                else
                {
                    sql += " And Type =1 ";
                }
            }

            sql += " And UID = " + UID + "";

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
            sql += " order by CreatedDate DESC OFFSET " + pageIndex + "*" + pageSize + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<tbl_HistoryPayWallet> a = new List<tbl_HistoryPayWallet>();
            while (reader.Read())
            {
                var entity = new tbl_HistoryPayWallet();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);

                if (reader["UserName"] != DBNull.Value)
                    entity.UserName = reader["UserName"].ToString();

                if (reader["MainOrderID"] != DBNull.Value)
                    entity.MainOrderID = reader["MainOrderID"].ToString().ToInt(0);

                if (reader["Amount"] != DBNull.Value)
                    entity.Amount = Convert.ToDouble(reader["Amount"].ToString());

                if (reader["HContent"] != DBNull.Value)
                    entity.HContent = reader["HContent"].ToString();

                if (reader["MoneyLeft"] != DBNull.Value)
                    entity.MoneyLeft = Convert.ToDouble(reader["MoneyLeft"].ToString());

                if (reader["Type"] != DBNull.Value)
                    entity.Type = reader["Type"].ToString().ToInt(1);

                if (reader["TradeType"] != DBNull.Value)
                    entity.TradeType = reader["TradeType"].ToString().ToInt(1);

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                a.Add(entity);
            }
            reader.Close();
            return a;
        }

        public static double GetTotalAllAmount_BySQLUser(string type, string fd, string td, int UID)
        {
            var sql = @"select Total=Sum(Amount) ";
            sql += "from tbl_HistoryPayWallet ";
            sql += "where UID Like N'%%' ";
            if (!string.IsNullOrEmpty(type))
            {
                sql += " And TradeType =" + type + " ";
            }

            sql += " And UID = " + UID + "";

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
            double a = 0;
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                if (reader["Total"] != DBNull.Value)
                    a = Convert.ToDouble(reader["Total"].ToString());
            }
            reader.Close();
            return a;
        }

        #endregion
    }
}

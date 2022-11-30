using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using System.Data.Entity.Validation;
using System.Diagnostics;
using WebUI.Business;
using System.Data;
using MB.Extensions;

namespace NHST.Controllers
{
    public class WithdrawController
    {        
        #region CRUD
        public static void Insert1(int UID, string Username, double Amount, int Status, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                try
                {
                    tbl_Withdraw a = new tbl_Withdraw();
                    a.UID = UID;
                    a.Username = Username;
                    a.Amount = Amount;
                    a.Status = Status;
                    a.CreatedDate = CreatedDate;
                    a.CreatedBy = CreatedBy;
                    dbe.tbl_Withdraw.Add(a);
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    int kq = dbe.SaveChanges();
                    string k = a.ID.ToString();
                }
                catch (DbEntityValidationException dbEx)
                {
                    string html = "";
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }
                }
            }
        }
        public static string Insert(int UID, string Username, double Amount, int Status, string Note, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Withdraw a = new tbl_Withdraw();
                a.UID = UID;
                a.Username = Username;
                a.Amount = Amount;
                a.Status = Status;
                if (Status == 2)
                {
                    a.AcceptBy = CreatedBy;
                    a.AcceptDate = CreatedDate;
                }
                a.Type = 2;
                a.Note = Note;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_Withdraw.Add(a);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = a.ID.ToString();
                return k;
            }
        }
        public static string InsertNote(int UID, string Username, double Amount, int Status,string Note, DateTime CreatedDate, string CreatedBy, string BankNumber, string BankAddress, string Beneficiary)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Withdraw a = new tbl_Withdraw();
                a.UID = UID;
                a.Username = Username;
                a.Amount = Amount;
                a.Status = Status;
                a.Note = Note;
                a.Type = 2;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                a.Beneficiary = Beneficiary;
                a.BankAddress = BankAddress;
                a.BankNumber = BankNumber;
                dbe.tbl_Withdraw.Add(a);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = a.ID.ToString();
                return k;
            }
        }
        public static string UpdateStatus(int ID, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Withdraw.Where(f => f.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.Status = Status;
                    a.ModifiedDate = ModifiedDate;
                    a.ModifiedBy = ModifiedBy;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string UpdateStatusAccept(int ID, int Status, DateTime AcceptDate, string AcceptBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Withdraw.Where(f => f.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.Status = Status;
                    a.AcceptBy = AcceptBy;
                    a.AcceptDate = AcceptDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string InsertRechargeCYN(int UID, string Username, double Amount, 
            string Note, int Status, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Withdraw a = new tbl_Withdraw();
                a.UID = UID;
                a.Username = Username;
                a.Amount = Amount;
                a.Note = Note;
                a.Status = Status;
                a.Type = 3;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_Withdraw.Add(a);                
                int kq = dbe.SaveChanges();
                string k = a.ID.ToString();
                return k;
            }
        }
        public static string Delete(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Withdraw.Where(f => f.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    dbe.tbl_Withdraw.Remove(a);
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_Withdraw> GetAllByType(string s, int Type)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Withdraw> a = new List<tbl_Withdraw>();
                a = dbe.tbl_Withdraw.Where(w => w.Username.Contains(s) && w.Type == Type).OrderByDescending(w => w.ID).ToList();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static List<tbl_Withdraw> GetBuyUIDAndType(int UID, int Type)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Withdraw> a = new List<tbl_Withdraw>();
                a = dbe.tbl_Withdraw.Where(w => w.UID == UID && w.Type == Type).OrderByDescending(w => w.ID).ToList();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static tbl_Withdraw GetByUIDAndID(int UID, int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Withdraw.Where(f => f.ID == ID && f.UID == UID).FirstOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static List<tbl_Withdraw> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Withdraw> a = new List<tbl_Withdraw>();
                a = dbe.tbl_Withdraw.Where(w => w.Username.Contains(s)).OrderByDescending(w => w.ID).ToList();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static tbl_Withdraw GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Withdraw.Where(f => f.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static List<tbl_Withdraw> GetBuyUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Withdraw> a = new List<tbl_Withdraw>();
                a = dbe.tbl_Withdraw.Where(w => w.UID == UID && w.Type == 2).OrderByDescending(w => w.ID).ToList();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        #endregion

        #region New
        public static double GetTotalWithDrawSQL(string username, string st)
        {
            var sql = @"select Total=Sum(Amount) ";
            sql += "from tbl_Withdraw ";
            sql += "where  Username LIKE '%" + username + "%' ";
            if (!string.IsNullOrEmpty(st))
            {
                if (st != "0")
                {
                    sql += " and Status=" + st + " ";
                }
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
        public static int GetTotalSQL(string username, string st)
        {
            var sql = @"select Total=Count(*) ";
            sql += "from tbl_Withdraw ";
            sql += "where Type=2 And  Username LIKE '%" + username + "%'";
            if (!string.IsNullOrEmpty(st))
            {
                if (st != "0")
                {
                    sql += " and Status=" + st + " ";
                }
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
        public static List<ListWithdrawNew> GetBySQL_DK(string username, string st, int pageIndex, int pageSize)
        {

            var sql = @"select ID, UID,BankNumber,BankAddress,Beneficiary, Username, Amount, Status, CreatedDate,AcceptBy,AcceptDate ";
            sql += "from tbl_Withdraw ";
            sql += "where Type=2 And  Username LIKE '%" + username + "%' ";
            if (!string.IsNullOrEmpty(st))
            {
                if (st != "0")
                {
                    sql += " and Status=" + st + " ";
                }
            }
            sql += "order by id DESC OFFSET " + pageIndex + "*" + pageSize + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<ListWithdrawNew> a = new List<ListWithdrawNew>();
            while (reader.Read())
            {
                var entity = new ListWithdrawNew();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);

                if (reader["Username"] != DBNull.Value)
                    entity.Username = reader["Username"].ToString();
                if (reader["BankNumber"] != DBNull.Value)
                    entity.BankNumber = reader["BankNumber"].ToString();


                if (reader["BankAddress"] != DBNull.Value)
                    entity.BankAddress = reader["BankAddress"].ToString();

                if (reader["Beneficiary"] != DBNull.Value)
                    entity.Beneficiary = reader["Beneficiary"].ToString();

                if (reader["Amount"] != DBNull.Value)
                {
                    entity.Amount = Convert.ToDouble(reader["Amount"].ToString());
                    entity.AmountString = string.Format("{0:N0}", Convert.ToDouble(reader["Amount"].ToString())) + " VND";
                }

                if (reader["Status"] != DBNull.Value)
                {
                    entity.Status = reader["Status"].ToString().ToInt(0);

                    if (reader["Status"].ToString().ToInt(0) == 1)
                        entity.StatusName = "<span class=\"badge orange darken-2 white-text border-radius-2\">Đang chờ</span>";
                    if (reader["Status"].ToString().ToInt(0) == 2)
                        entity.StatusName = "<span class=\"badge green darken-2 white-text border-radius-2\">Thành công</span>";
                    if (reader["Status"].ToString().ToInt(0) == 3)
                        entity.StatusName = "<span class=\"badge red darken-2 white-text border-radius-2\">Hủy</span>";
                }
                if (reader["CreatedDate"] != DBNull.Value)
                {
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                    entity.CreatedDateString = Convert.ToDateTime(reader["CreatedDate"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                }
                if (reader["AcceptDate"] != DBNull.Value)
                {
                    entity.AcceptDate = Convert.ToDateTime(reader["AcceptDate"].ToString());
                }
                if (reader["AcceptBy"] != DBNull.Value)
                {
                    entity.AcceptBy = reader["AcceptBy"].ToString();

                }
                a.Add(entity);

            }
            reader.Close();
            return a;
        }

        public static List<tbl_Withdraw> GetAllByTypeBySQL(string s, string Type, string st, int pageSize, int pageIndex)
        {
            var sql = @"select * ";
            sql += "from tbl_Withdraw ";
            sql += "Where Username Like N'%" + s + "%' ";
            if (!string.IsNullOrEmpty(Type))
                sql += "And Type=" + Type + " ";
            if (!string.IsNullOrEmpty(st))
                sql += "And Status=" + st + " ";
            sql += "Order by ID desc OFFSET " + pageIndex + "*" + pageSize + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<tbl_Withdraw> list = new List<tbl_Withdraw>();
            while (reader.Read())
            {
                var entity = new tbl_Withdraw();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["Username"] != DBNull.Value)
                    entity.Username = reader["Username"].ToString();

                if (reader["Amount"] != DBNull.Value)
                    entity.Amount = Convert.ToDouble(reader["Amount"].ToString());

                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt(0);

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());

                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString();

                if (reader["ModifiedDate"] != DBNull.Value)
                    entity.ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"].ToString());

                if (reader["ModifiedBy"] != DBNull.Value)
                    entity.ModifiedBy = reader["ModifiedBy"].ToString();

                if (reader["ModifiedBy"] != DBNull.Value)
                    entity.ModifiedBy = reader["ModifiedBy"].ToString();

                if (reader["AcceptBy"] != DBNull.Value)
                    entity.AcceptBy = reader["AcceptBy"].ToString();

                if (reader["AcceptDate"] != DBNull.Value)
                    entity.AcceptDate = Convert.ToDateTime(reader["AcceptDate"].ToString());

                list.Add(entity);
            }
            reader.Close();
            return list;

        }

        public static int GetTotalByTypeBySQL(string s, string Type, string st)
        {
            var sql = @"select Total=Count(*) ";
            sql += "from tbl_Withdraw ";
            sql += "Where Username Like N'%" + s + "%' ";
            if (!string.IsNullOrEmpty(Type))
                sql += "And Type=" + Type + " ";
            if (!string.IsNullOrEmpty(st))
                sql += "And Status=" + st + " ";
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

        public partial class ListWithdrawNew
        {
            public int ID { get; set; }
            public int UID { get; set; }
            public string Username { get; set; }
            public double Amount { get; set; }
            public string AmountString { get; set; }
            public string BankAddress { get; set; }
            public string BankNumber { get; set; }
            public string Beneficiary { get; set; }
            public int Status { get; set; }
            public string StatusName { get; set; }
            public DateTime CreatedDate { get; set; }
            public string CreatedDateString { get; set; }
            public string AcceptBy { get; set; }
            public DateTime AcceptDate { get; set; }
        }
        #endregion
    }
}
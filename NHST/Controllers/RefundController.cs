using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;
using WebUI.Business;
using System.Data;
using MB.Extensions;

namespace NHST.Controllers
{
    public class RefundController
    {
        #region CRUD
        public static string Insert(int UID, string Username, double Amount, string Note, int Status, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Refund a = new tbl_Refund();
                a.UID = UID;
                a.Username = Username;
                a.Amount = Amount;
                a.Status = Status;
                a.Note = Note;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_Refund.Add(a);              
                dbe.SaveChanges();
                string k = a.ID.ToString();
                return k;
            }
        }

        public static string Update(int ID, double Amount, string Note, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Refund.Where(f => f.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.Amount = Amount;
                    a.Status = Status;
                    a.Note = Note;
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
        public static string UpdateStatus(int ID, int Status)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Refund.Where(f => f.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    
                    a.Status = Status;
                   
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string Delete(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Refund.Where(f => f.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    dbe.tbl_Refund.Remove(a);
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_Refund> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Refund> a = new List<tbl_Refund>();
                a = dbe.tbl_Refund.Where(w => w.Username.Contains(s)).OrderByDescending(w => w.ID).ToList();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static tbl_Refund GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Refund.Where(f => f.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static tbl_Refund GetByUIDAndID(int UID, int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Refund.Where(f => f.ID == ID && f.UID == UID).FirstOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static List<tbl_Refund> GetBuyUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Refund> a = new List<tbl_Refund>();
                a = dbe.tbl_Refund.Where(w => w.UID == UID).OrderByDescending(w => w.ID).ToList();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
       
        public static List<tbl_Refund> GetAllByStatus(int status)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Refund> a = new List<tbl_Refund>();
                a = dbe.tbl_Refund.Where(w => w.Status == status).OrderByDescending(w => w.ID).ToList();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        #endregion

        public static List<tbl_Refund> GetAllBySQL(string s, string st, int pageSize, int pageIndex)
        {

            var sql = @"select * ";
            sql += "from tbl_Refund ";
            sql += "where Username like N'%" + s + "%' ";
            if (!string.IsNullOrEmpty(st))
                sql += "And Status=" + st + " ";
            sql += "order by id DESC OFFSET " + pageIndex + "*" + pageSize + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<tbl_Refund> a = new List<tbl_Refund>();
            while (reader.Read())
            {
                var entity = new tbl_Refund();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["Username"] != DBNull.Value)
                    entity.Username = reader["Username"].ToString();

                if (reader["Amount"] != DBNull.Value)
                    entity.Amount = Convert.ToDouble(reader["Amount"].ToString());

                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt(0);

                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString();

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());

                if (reader["ModifiedDate"] != DBNull.Value)
                    entity.ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"].ToString());

                if (reader["ModifiedBy"] != DBNull.Value)
                    entity.ModifiedBy = reader["ModifiedBy"].ToString();
                a.Add(entity);
            }
            reader.Close();
            return a;

        }
        public static int GetTotalBySQL(string s, string st)
        {

            var sql = @"select Total=Count(*) ";
            sql += "from tbl_Refund ";
            sql += "where Username like N'%" + s + "%' ";
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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using MB.Extensions;
using System.Data;
using WebUI.Business;

namespace NHST.Controllers
{
    public class HistoryPayWalletCYNController
    {
        #region CRUD
        public static string Insert(int UID, string UserName,  double Amount, double MoneyLeft, int Type,
            int TradeType, string Note, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_HistoryPayWalletCYN a = new tbl_HistoryPayWalletCYN();
                a.UID = UID;
                a.UserName = UserName;
                a.Amount = Amount;
                a.MoneyLeft = MoneyLeft;
                a.Type = Type;
                a.TradeType = TradeType;
                a.Note = Note;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_HistoryPayWalletCYN.Add(a);
                int kq = dbe.SaveChanges();
                //string k = kq + "|" + user.ID;
                string k = a.ID.ToString();
                return k;
            }
        }        
        #endregion
        #region Select
        public static List<tbl_HistoryPayWalletCYN> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_HistoryPayWalletCYN> aus = new List<tbl_HistoryPayWalletCYN>();
                aus = dbe.tbl_HistoryPayWalletCYN.Where(a => a.UserName.Contains(s)).OrderByDescending(a => a.ID).ToList();
                return aus;
            }
        }
        public static List<tbl_HistoryPayWalletCYN> GetByUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_HistoryPayWalletCYN> aus = new List<tbl_HistoryPayWalletCYN>();
                aus = dbe.tbl_HistoryPayWalletCYN.Where(a => a.UID == UID).OrderByDescending(a => a.ID).ToList();
                return aus;
            }
        }
        public static List<tbl_HistoryPayWalletCYN> GetByUIDTradeTypeDateSend(int UID, int TradeType, DateTime DateSend)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_HistoryPayWalletCYN> aus = new List<tbl_HistoryPayWalletCYN>();
                aus = dbe.tbl_HistoryPayWalletCYN.Where(a => a.UID == UID && a.TradeType == TradeType && a.DateSend == DateSend).OrderByDescending(a => a.ID).ToList();
                return aus;
            }
        }
        #endregion

        public static List<tbl_HistoryPayWalletCYN> GetByUID_SQL(int UID)
        {
            var list = new List<tbl_HistoryPayWalletCYN>();
            var sql = @"select * from tbl_HistoryPayWalletCYN ";
            sql += " where UID = " + UID + "";
            sql += " Order By ID desc";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);

            int i = 1;
            while (reader.Read())
            {
                var entity = new tbl_HistoryPayWalletCYN();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);
                if (reader["UserName"] != DBNull.Value)
                    entity.UserName = reader["UserName"].ToString();

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                if (reader["Amount"] != DBNull.Value)
                    entity.Amount = Convert.ToDouble(reader["Amount"].ToString());
                if (reader["Note"] != DBNull.Value)
                    entity.Note = reader["Note"].ToString();
                if (reader["MoneyLeft"] != DBNull.Value)
                    entity.MoneyLeft = Convert.ToDouble(reader["MoneyLeft"].ToString());
                if (reader["Type"] != DBNull.Value)
                    entity.Type = reader["Type"].ToString().ToInt(0);
                if (reader["TradeType"] != DBNull.Value)
                    entity.TradeType = reader["TradeType"].ToString().ToInt(0);

                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString();

                if (reader["DateSend"] != DBNull.Value)
                    entity.DateSend = Convert.ToDateTime(reader["DateSend"].ToString());

                i++;
                list.Add(entity);
            }
            reader.Close();
            return list;

        }
    }
}
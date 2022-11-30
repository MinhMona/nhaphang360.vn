using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using MB.Extensions;
using NHST.Models;
using WebUI.Business;

namespace NHST.Controllers
{
    public class YCGController
    {
        public static string Insert(int MainOrderID, string FullName, string Phone, string Address, string Note, string Createdby, DateTime CreatedDate)
        {
            using (var db = new NHSTEntities())
            {
                tbl_YCG yc = new tbl_YCG();
                yc.MainOrderID = MainOrderID;
                yc.FullName = FullName;
                yc.Phone = Phone;
                yc.Note = Note;
                yc.Status = 1;
                yc.Address = Address;
                yc.CreatedBy = Createdby;
                yc.CreatedDate = CreatedDate;
                db.tbl_YCG.Add(yc);
                db.SaveChanges();
                return yc.ID.ToString();
            }
        }

        public static string Update(int ID, int Status, string CreatedBy, DateTime CreatedDate)
        {
            using (var db = new NHSTEntities())
            {
                var yc = db.tbl_YCG.Where(x => x.ID == ID).FirstOrDefault();
                if (yc != null)
                {
                    yc.Status = Status;
                    yc.ModifiedBy = CreatedBy;
                    yc.ModifiedDate = CreatedDate;
                    db.SaveChanges();
                    return yc.ID.ToString();
                }
                else
                    return null;
            }
        }

        public static List<tbl_YCG> GetAll(string Phone)
        {
            using (var db = new NHSTEntities())
            {
                var yc = db.tbl_YCG.Where(x => x.Phone.Contains(Phone)).ToList();
                return yc;
            }
        }

        public static tbl_YCG GetByID(int ID)
        {
            using (var db = new NHSTEntities())
            {
                var yc = db.tbl_YCG.Where(x => x.ID == ID).FirstOrDefault();
                if (yc != null)
                    return yc;
                else return null;
            }
        }

        public static tbl_YCG GetByMainOrderID(int MainOrderID)
        {
            using (var db = new NHSTEntities())
            {
                var yc = db.tbl_YCG.Where(x => x.MainOrderID == MainOrderID).FirstOrDefault();
                if (yc != null)
                    return yc;
                else
                    return null;
            }
        }

        public static int GetTotal(string s)
        {
            var sql = @"select Total=Count(*) ";
            sql += "from tbl_YCG ";
            sql += "Where Phone LIKE N'%" + s + "%' ";
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
        public static List<tbl_YCG> GetAllBySQL(string s, int pageIndex, int pageSize)
        {
            var sql = @"select * ";
            sql += "from tbl_YCG ";
            sql += "Where Phone LIKE N'%" + s + "%' ";
            sql += "order by id DESC OFFSET " + pageIndex + "*" + pageSize + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<tbl_YCG> a = new List<tbl_YCG>();
            while (reader.Read())
            {
                var entity = new tbl_YCG();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);

                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString();

                if (reader["MainOrderID"] != DBNull.Value)
                    entity.MainOrderID = reader["MainOrderID"].ToString().ToInt(0);

                if (reader["Status"] != DBNull.Value)
                    entity.Status = Convert.ToInt32(reader["Status"].ToString());

                if (reader["FullName"] != DBNull.Value)
                    entity.FullName = reader["FullName"].ToString();

                if (reader["Phone"] != DBNull.Value)
                    entity.Phone = reader["Phone"].ToString();

                if (reader["Address"] != DBNull.Value)
                    entity.Address = reader["Address"].ToString();

                if (reader["Note"] != DBNull.Value)
                    entity.Note = reader["Note"].ToString();

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());

                a.Add(entity);
            }
            reader.Close();
            return a;
        }

    }
}
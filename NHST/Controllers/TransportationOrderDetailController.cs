using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class TransportationOrderDetailController
    {
        #region CRUD
        public static string Insert(int TransportationOrderID, string TransportationOrderCode, double Weight,
            DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_TransportaionOrderDetail p = new tbl_TransportaionOrderDetail();
                p.TransportationOrderID = TransportationOrderID;
                p.TransportationOrderCode = TransportationOrderCode;
                p.Weight = Weight;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_TransportaionOrderDetail.Add(p);
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string InsertNew(int TransportationOrderID, string TransportationOrderCode, double Weight,
            string ProductType, bool IsCheckProduct, bool IsPackaged, bool IsInsurrance, string CODTQCYN,
            string CODTQVND, string UserNote, string ProductQuantity,
            DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_TransportaionOrderDetail p = new tbl_TransportaionOrderDetail();
                p.TransportationOrderID = TransportationOrderID;
                p.TransportationOrderCode = TransportationOrderCode;
                p.Weight = Weight;
                p.ProductType = ProductType;
                p.IsCheckProduct = IsCheckProduct;
                p.IsPackaged = IsPackaged;
                p.IsInsurrance = IsInsurrance;
                p.CODTQCYN = CODTQCYN;
                p.CODTQVND = CODTQVND;
                p.UserNote = UserNote;
                p.ProductQuantity = ProductQuantity;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_TransportaionOrderDetail.Add(p);
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }
        #endregion
        #region Select
        public static List<tbl_TransportaionOrderDetail> GetByTransportationOrderID(int TransportationOrderID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_TransportaionOrderDetail> pages = new List<tbl_TransportaionOrderDetail>();
                pages = dbe.tbl_TransportaionOrderDetail.Where(p => p.TransportationOrderID == TransportationOrderID).OrderByDescending(a => a.CreatedDate).ToList();
                return pages;
            }
        }
        public static List<tbl_TransportaionOrderDetail> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_TransportaionOrderDetail> pages = new List<tbl_TransportaionOrderDetail>();
                pages = dbe.tbl_TransportaionOrderDetail.OrderByDescending(a => a.CreatedDate).ToList();
                return pages;
            }
        }

        public static tbl_TransportaionOrderDetail GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_TransportaionOrderDetail page = dbe.tbl_TransportaionOrderDetail.Where(p => p.ID == ID).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        #endregion
    }
}
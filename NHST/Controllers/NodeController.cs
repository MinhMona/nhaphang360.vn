using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class NodeController
    {
        #region CRUD
        public static string Insert(string NodeName, string NodeAliasPath, int TypeID, string TypeName, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Node p = new tbl_Node();
                p.NodeName = NodeName;
                p.NodeAliasPath = NodeAliasPath;
                p.TypeID = TypeID;
                p.TypeName = TypeName;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_Node.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                //string k = kq + "|" + user.ID;
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, string NodeName, string NodeAliasPath, int TypeID, string TypeName, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_Node.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.NodeName = NodeName;
                    p.NodeAliasPath = NodeAliasPath;
                    p.TypeID = TypeID;
                    p.TypeName = TypeName;
                    p.NodeAliasPath = NodeAliasPath;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        #endregion
        #region Select
        public static List<tbl_Node> GetByNodeAliasPathAndNotContainsID(string NodeAliasPath, int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Node> pages = new List<tbl_Node>();
                pages = dbe.tbl_Node.Where(p => p.NodeAliasPath == NodeAliasPath && p.ID != ID).OrderByDescending(a => a.CreatedDate).ToList();
                return pages;
            }
        }
        public static List<tbl_Node> GetByNodeAliasPath(string NodeAliasPath)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Node> pages = new List<tbl_Node>();
                pages = dbe.tbl_Node.Where(p => p.NodeAliasPath == NodeAliasPath).OrderByDescending(a => a.CreatedDate).ToList();
                return pages;
            }
        }
        public static List<tbl_Node> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Node> pages = new List<tbl_Node>();
                pages = dbe.tbl_Node.Where(p => p.NodeName.Contains(s)).OrderByDescending(a => a.CreatedDate).ToList();
                return pages;
            }
        }
        public static tbl_Node GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Node page = dbe.tbl_Node.Where(p => p.ID == ID).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        #endregion
    }
}
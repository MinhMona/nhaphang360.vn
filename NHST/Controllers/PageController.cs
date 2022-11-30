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
    public class PageController
    {
        #region CRUD
        public static string Insert(string Title, string Summary, string IMG, string PageContent, bool IsHidden, int PageTypeID,
            int NodeID, string NodeAliasPath, string ogurl, string ogtitle, string ogdescription, string ogimage, string metatitle,
            string metadescription, string metakeyword, DateTime CreatedDate, string CreatedBy, string ogFBTitle, string ogFBDescription, 
            string ogFBIMG, string ogTWtitle, string ogTWDescription, string ogTWIMG, bool SideBar)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Page p = new tbl_Page();
                p.Title = Title;
                p.Summary = Summary;
                p.IMG = IMG;
                p.PageContent = PageContent;
                p.PageTypeID = PageTypeID;
                p.IsHidden = IsHidden;
                p.NodeID = NodeID;
                p.NodeAliasPath = NodeAliasPath;
                p.ogurl = ogurl;
                p.ogtitle = ogtitle;                
                p.ogdescription = ogdescription;
                p.ogimage = ogimage;
                p.metatitle = metatitle;
                p.metadescription = metadescription;
                p.metakeyword = metakeyword;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                p.OGFacebookTitle = ogFBTitle;
                p.OGFacebookDescription = ogFBDescription;
                p.OGFacebookIMG = ogFBIMG;
                p.OGTwitterTitle = ogTWtitle;
                p.OGTwitterDescription = ogTWDescription;
                p.OGTwitterIMG = ogTWIMG;
                p.SideBar = SideBar;
                dbe.tbl_Page.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                //string k = kq + "|" + user.ID;
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, string Title, string Summary, string IMG, string PageContent, bool IsHidden, int PageTypeID,
             int NodeID, string NodeAliasPath, string ogurl, string ogtitle, string ogdescription, string ogimage, string metatitle,
            string metadescription, string metakeyword, DateTime ModifiedDate, string ModifiedBy, string ogFBTitle, string ogFBDescription,
            string ogFBIMG, string ogTWtitle, string ogTWDescription, string ogTWIMG, bool SideBar)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_Page.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.Title = Title;
                    p.Summary = Summary;
                    if (!string.IsNullOrEmpty(IMG))
                    {
                        p.IMG = IMG;
                    }
            
                    p.PageContent = PageContent;
                    p.PageTypeID = PageTypeID;
                    p.IsHidden = IsHidden;
                    p.NodeID = NodeID;
                    p.NodeAliasPath = NodeAliasPath;
                    p.ogurl = ogurl;
                    p.ogtitle = ogtitle;
                    p.ogdescription = ogdescription;
                    p.ogimage = ogimage;
                    p.metatitle = metatitle;
                    p.metadescription = metadescription;
                    p.metakeyword = metakeyword;

                    p.OGFacebookTitle = ogFBTitle;
                    p.OGFacebookDescription = ogFBDescription;
                    if (!string.IsNullOrEmpty(ogFBIMG))
                    {
                        p.OGFacebookIMG = ogFBIMG;
                    }
               
                    p.OGTwitterTitle = ogTWtitle;
                    p.OGTwitterDescription = ogTWDescription;
                    if (!string.IsNullOrEmpty(ogTWIMG))
                    {
                        p.OGTwitterIMG = ogTWIMG;
                    }
                
                    p.SideBar = SideBar;

     
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
        public static List<tbl_Page> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Page> pages = new List<tbl_Page>();
                pages = dbe.tbl_Page.Where(p => p.Title.Contains(s)).OrderByDescending(a => a.CreatedDate).ToList();
                if (pages.Count > 0)
                {
                    return pages;
                }
                else return null;
            }
        }
        public static List<tbl_Page> GetByPagetTypeID(int PageTypeID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Page> pages = new List<tbl_Page>();
                pages = dbe.tbl_Page.Where(p => p.PageTypeID == PageTypeID && p.IsHidden == false).OrderByDescending(p => p.ID).ToList();
                return pages;
            }
        }
        public static List<tbl_Page> GetTopByPagetTypeID(int TopN, int PageTypeID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Page> pages = new List<tbl_Page>();
                pages = dbe.tbl_Page.Where(p => p.PageTypeID == PageTypeID && p.IsHidden == false).Take(TopN).ToList();
                if (pages.Count > 0)
                {
                    return pages;
                }
                else return null;
            }
        }
        public static tbl_Page GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Page page = dbe.tbl_Page.Where(p => p.ID == ID).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        public static tbl_Page GetByNodeAliasPath(string NodeAliasPath)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Page page = dbe.tbl_Page.Where(p => p.NodeAliasPath == NodeAliasPath).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        #endregion

        #region New
        public static int GetTotal(string s)
        {
            var sql = @"select Total=Count(*) from tbl_Page ";
            sql += "Where Title LIKE N'%" + s + "%' ";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int a = 0;
            while (reader.Read())
            {
                if (reader["Total"] != DBNull.Value)
                    a = reader["Total"].ToString().ToInt(0);
            }
            return a;
        }
        public static List<PageNew> GetAllBySQL(string s, int pageIndex, int pageSize)
        {
            var sql = @"select a.ID, a.Title, IsHidden, a.CreatedDate, b.PageTypeName from tbl_Page as a left join tbl_PageType as b on a.PageTypeID=b.ID ";
            sql += "Where Title LIKE N'%" + s + "%' ";
            sql += "order by id DESC OFFSET " + pageIndex + "*" + pageSize + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<PageNew> a = new List<PageNew>();
            while (reader.Read())
            {

                var entity = new PageNew();
                if (reader["ID"] != DBNull.Value)
                    entity.NewPage.ID = reader["ID"].ToString().ToInt(0);
                if (reader["Title"] != DBNull.Value)
                    entity.NewPage.Title = reader["Title"].ToString();
                if (reader["IsHidden"] != DBNull.Value)
                    entity.NewPage.IsHidden = Convert.ToBoolean(reader["IsHidden"]);
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.NewPage.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                if (reader["PageTypeName"] != DBNull.Value)
                    entity.PageTypeName = reader["PageTypeName"].ToString();
                a.Add(entity);
            }
            reader.Close();
            return a;
        }
        public partial class PageNew
        {
            public tbl_Page NewPage { get; set; }
            public string PageTypeName { get; set; }
            public PageNew()
            {
                NewPage = new tbl_Page();
            }
        }
        #endregion
    }
}
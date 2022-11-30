using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHST.Controllers
{
    public class TokenForgotPassController
    {
        public static tbl_TokenForgotPass Insert(int UID, string Token, string Createby)
        {
            using (var db = new NHSTEntities())
            {
                tbl_TokenForgotPass tk = new tbl_TokenForgotPass();
                tk.UID = UID;
                tk.Token = Token;
                tk.CreatedBy = Createby;
                tk.CreatedDate = DateTime.Now;
                tk.IsHidden = false;
                db.tbl_TokenForgotPass.Add(tk);
                db.SaveChanges();
                return tk;
            }
        }

        public static string Update(int ID)
        {
            using (var db = new NHSTEntities())
            {
                var tk = db.tbl_TokenForgotPass.Where(x => x.ID == ID).FirstOrDefault();
                if (tk != null)
                {
                    tk.IsHidden = true;
                    tk.ModifiedDate = DateTime.Now;
                    int i = db.SaveChanges();
                    return i.ToString();
                }
                else
                    return null;
            }
        }

        public static tbl_TokenForgotPass GetByToken(string Token)
        {
            using (var db = new NHSTEntities())
            {
                var tk = db.tbl_TokenForgotPass.Where(x => x.Token == Token && x.IsHidden == false).FirstOrDefault();
                if (tk != null)
                    return tk;
                else
                    return null;
            }
        }

        public static tbl_TokenForgotPass GetToken(string Token)
        {
            using (var db = new NHSTEntities())
            {
                var tk = db.tbl_TokenForgotPass.Where(x => x.Token == Token).FirstOrDefault();
                if (tk != null)
                    return tk;
                else
                    return null;
            }
        }
    }
}
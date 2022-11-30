using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHST
{
    public class TokenSession
    {
        internal static string CreateAndStoreSessionToken(string userName)
        {
            Guid sessionToken = System.Guid.NewGuid();
            var acc = AccountController.GetByUsername(userName);
            if (acc != null)
            {
                while (sessionToken.ToString() == acc.LoginStatus)
                {
                    sessionToken = System.Guid.NewGuid();
                }
                AccountController.UpdateLoginStatus(acc.ID, sessionToken.ToString());
                return sessionToken.ToString();
            }
            else
                return null;
        }
    }
}
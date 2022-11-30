<%@ WebHandler Language="C#" Class="HandlerCS" %>

using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using NHST.Bussiness;

public class HandlerCS : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {

        if (context.Request.Files.Count > 0)
        {
            string FolderURL = "/Uploads/ChatIMG/";
            string a = "[";
            for (int i = 0; i < context.Request.Files.Count; i++)
            {
                HttpPostedFile f = context.Request.Files[i];

                string fileContentType = f.ContentType; // getting ContentType

                byte[] tempFileBytes = new byte[f.ContentLength];

                var data = f.InputStream.Read(tempFileBytes, 0, Convert.ToInt32(f.ContentLength));

                string fileName = f.FileName; // getting File Name
                string fileExtension = Path.GetExtension(fileName).ToLower();

                var result = FileUploadCheck.isValidFile(tempFileBytes, fileExtension, fileContentType); // Validate Header
                if (result)
                {

                    var date = DateTime.Now;
                    string x = date.Day.ToString() + date.Month.ToString() + date.Year.ToString() + date.Hour.ToString() + date.Minute.ToString() + date.Millisecond.ToString();
                    var o = FolderURL + Path.GetFileNameWithoutExtension(f.FileName) + "_" + x + Path.GetExtension(f.FileName);
                    if (f.FileName.ToLower().Contains(".jpg") || f.FileName.ToLower().Contains(".png") || f.FileName.ToLower().Contains(".jpeg") || f.FileName.ToLower().Contains(".doc") || f.FileName.ToLower().Contains(".xls"))
                    {
                        if (f.ContentType == "image/png" || f.ContentType == "image/jpeg" || f.ContentType == "image/jpg" || f.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || f.ContentType == "application/msword" || f.ContentType == "application/vnd.ms-excel" || f.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                        {

                            try
                            {
                                //f.SaveAs(context.Server.MapPath(o));
                                string IMG =   FileUploadCheck.ConvertToBase64(tempFileBytes);
                                string json = new JavaScriptSerializer().Serialize(
                                               new
                                               {
                                                   //name = o,
                                                   name = IMG,
                                                   realname = f.FileName
                                               });
                                a += json + ",";
                            }
                            catch
                            {

                            }
                        }
                    }
                }

                //Send File details in a JSON Response.



            }
            a = a.Remove(a.Length - 1, 1);
            a += "]";

            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.ContentType = "text/json";
            context.Response.Write(a);
            context.Response.End();
        }
    }



    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
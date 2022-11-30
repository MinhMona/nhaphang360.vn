using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHST.Bussiness
{
    public class APIUtils
    {
        //public static string WEB_LINK = "http://bnails.monamedia.net";
        public static string WEB_LINK = "localhost:62291";
        public static string DATA_REQUIRED = "Data is required";
        public static string DATA_BROKEN = "Data could not been edited at moment";
        public static string OBJ_DNTEXIST = "Object does not exist";
        public enum ResponseMessage
        {
            Success, Fail, Error
        }

        public static string O_DUPLICATE = "100";// " trùng khớp";
        public static string O_NotFound = "101";//"Không tìm thấy đối tượng";
        public static string O_Success = "102";//"Thực hiện thành công";
        public static string O_Fail = "103"; //"Thực hiên thất bại";
        public enum ResponseCode
        {
            SUCCESS = 102,//Success
            FAILED = 103,
            NotFound = 101,//The URI requested is invalid or the resource requested, such as a user, does not exists
            DataDupliacation = 100,//Let you know if provided data is already there.
            InternalServerError = 500,//Something is broken,

        }

        public static string GetResponseCode(ResponseCode code)
        {
            var rs = string.Empty;
            switch (code.ToString())
            {
                case "SUCCESS":
                    rs = "102";
                    break;
                case "FAILED":
                    rs = "103";
                    break;
                case "NotFound":
                    rs = "101";
                    break;
                case "DataDupliacation":
                    rs = "100";
                    break;
            }
            return rs;
        }
        public static bool GetBooleanFromString(string value)
        {
            return value.ToLower() == "true" || value.ToLower() == "1" || value == "";
        }
    }
}
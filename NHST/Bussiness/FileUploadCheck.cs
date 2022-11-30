using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace NHST.Bussiness
{
    public class FileUploadCheck
    {
        private enum ImageFileExtension
        {
            none = 0,
            jpg = 1,
            jpeg = 2,
            bmp = 3,
            gif = 4,
            png = 5
        }
        public enum FileType
        {
            Image = '1',
            Video = '2',
            PDF = '3',
            Text = '4',
            DOC = '5',
            DOCX = '6',
            PPT = '7',
        }

        public static bool isValidFile(byte[] bytFile, string flType, String FileContentType)
        {
            bool isvalid = false;
            if (flType == ".jpg" || flType == ".jpeg" || flType == ".png")
            {
                isvalid = isValidImageFile(bytFile, FileContentType);//we are going call this method
            }
            //else if (flType == FileType.Video)
            //{
            //    isvalid = isValidVideoFile(bytFile, FileContentType);
            //}
            //else if (flType == FileType.PDF)
            //{
            //    isvalid = isValidPDFFile(bytFile, FileContentType);
            //}
            return isvalid;
        }


        public static bool isValidImageFile(byte[] bytFile, String FileContentType)
        {
            bool isvalid = false;

            byte[] chkBytejpg = { 255, 216, 255, 224 };
            byte[] chkBytebmp = { 66, 77 };
            byte[] chkBytegif = { 71, 73, 70, 56 };
            byte[] chkBytepng = { 137, 80, 78, 71 };

            ImageFileExtension imgfileExtn = ImageFileExtension.none;

            if (FileContentType.Contains("jpg") | FileContentType.Contains("jpeg"))
            {
                imgfileExtn = ImageFileExtension.jpg;
            }
            else if (FileContentType.Contains("png"))
            {
                imgfileExtn = ImageFileExtension.png;
            }
            else if (FileContentType.Contains("bmp"))
            {
                imgfileExtn = ImageFileExtension.bmp;
            }
            else if (FileContentType.Contains("gif"))
            {
                imgfileExtn = ImageFileExtension.gif;
            }

            if (imgfileExtn == ImageFileExtension.jpg || imgfileExtn == ImageFileExtension.jpeg)
            {
                if (bytFile.Length >= 4)
                {
                    int j = 0;
                    for (Int32 i = 0; i <= 3; i++)
                    {
                        if (bytFile[i] == chkBytejpg[i])
                        {
                            j = j + 1;
                            if (j == 3)
                            {
                                isvalid = true;
                            }
                        }
                    }
                }
            }


            if (imgfileExtn == ImageFileExtension.png)
            {
                if (bytFile.Length >= 4)
                {
                    int j = 0;
                    for (Int32 i = 0; i <= 3; i++)
                    {
                        if (bytFile[i] == chkBytepng[i])
                        {
                            j = j + 1;
                            if (j == 3)
                            {
                                isvalid = true;
                            }
                        }
                    }
                }
            }


            if (imgfileExtn == ImageFileExtension.bmp)
            {
                if (bytFile.Length >= 4)
                {
                    int j = 0;
                    for (Int32 i = 0; i <= 1; i++)
                    {
                        if (bytFile[i] == chkBytebmp[i])
                        {
                            j = j + 1;
                            if (j == 2)
                            {
                                isvalid = true;
                            }
                        }
                    }
                }
            }

            if (imgfileExtn == ImageFileExtension.gif)
            {
                if (bytFile.Length >= 4)
                {
                    int j = 0;
                    for (Int32 i = 0; i <= 1; i++)
                    {
                        if (bytFile[i] == chkBytegif[i])
                        {
                            j = j + 1;
                            if (j == 3)
                            {
                                isvalid = true;
                            }
                        }
                    }
                }
            }

            return isvalid;
        }

        public static string ConvertToBase64(byte[] imageBytes)
        {
            string base64String = Convert.ToBase64String(imageBytes);
            return ConvertBase64ToImage(base64String);
        }

        public static string ConvertBase64ToImage(string imageData)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("~/Uploads/NewsIMG/");

            string fileNameWitPath = path + DateTime.Now.ToString().Replace("/", "-").Replace(" ", "-").Replace(":", "") + ".png";
            string linkIMG = "/Uploads/NewsIMG/" + DateTime.Now.ToString().Replace("/", "-").Replace(" ", "-").Replace(":", "") + ".png";
            byte[] data;
            string convert;
            using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    convert = imageData;
                    data = Convert.FromBase64String(convert);
                    bw.Write(data);
                    return linkIMG;
                }
            }
        }

    }
}
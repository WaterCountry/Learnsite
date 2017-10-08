using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
namespace LearnSite.Common
{
    /// <summary>
    ///Photo 的摘要说明
    /// </summary>
    public class Photo
    {
        static string photopath = "~/StudentPhoto/";
        public Photo()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static string GetStudentPhotoUrl(string snumstr, string sexstr)
        {
            string url = "~/Images/nothing.gif";
            string photogifurl = photopath + snumstr + ".gif";
            string photojpgurl = photopath + snumstr + ".jpg";
            if (File.Exists(HttpContext.Current.Server.MapPath(photogifurl)))
            {
                url = photogifurl;
            }
            else
            {
                if (File.Exists(HttpContext.Current.Server.MapPath(photojpgurl)))
                {
                    url = photojpgurl;
                }
                else
                {
                    if (sexstr == "男")
                    {
                        url = "~/Images/boy.gif";
                    }
                    if (sexstr == "女")
                    {
                        url = "~/Images/girl.gif";
                    }
                }
            }
            return url;

        }
        public static string GetStuPhotoUrl(string snumstr, string sexstr)
        {
            string url = "~/Images/nothing.gif";
            string photogifurl = photopath + snumstr + ".gif";
            string photojpgurl = photopath + snumstr + ".jpg";
            if (File.Exists(HttpContext.Current.Server.MapPath(photogifurl)))
            {
                url = photogifurl;
            }
            else
            {
                if (File.Exists(HttpContext.Current.Server.MapPath(photojpgurl)))
                {
                    url = photojpgurl;
                }
            }
            return url;
        }
        public static string ExistStuPhoto(string snumstr)
        {
            string photopathurl = photopath + snumstr + ".gif";
            string photojpgurl = photopath + snumstr + ".jpg";
            if (File.Exists(HttpContext.Current.Server.MapPath(photopathurl)))
            {
                return "gif";
            }
            else
            {
                if (File.Exists(HttpContext.Current.Server.MapPath(photojpgurl)))
                    return "jpg";
                else
                    return "none";
            }
        }
        /// <summary>
        /// 返回值1为jpg,2为gif,0为无
        /// </summary>
        /// <param name="snumstr"></param>
        /// <returns></returns>
        public static string ExistStuPhotoIntStr(string snumstr)
        {
            string photogifurl = photopath + snumstr + ".gif";
            string photojpgurl = photopath + snumstr + ".jpg";
            if (File.Exists(HttpContext.Current.Server.MapPath(photojpgurl)))
            {
                return "1";
            }
            else
            {
                if (File.Exists(HttpContext.Current.Server.MapPath(photogifurl)))
                    return "2";
                else
                    return "0";
            }
        }
        /// <summary>
        /// 保存资源上传文件
        /// </summary>
        /// <param name="FUsoft"></param>
        /// <returns></returns>
        public static string PhotoUpload(FileUpload FUphoto, string Snum)
        {
            string msg = "0";
            int whdefine = 321;//默认最大宽高
            if (FUphoto.HasFile)
            {
                string ftype = Path.GetExtension(FUphoto.PostedFile.FileName).ToLower();
                if (ftype == ".gif" || ftype == ".jpg")
                {
                    if (FUphoto.PostedFile.ContentLength < 1048576)
                    {
                        Stream streamup = FUphoto.PostedFile.InputStream;
                        try
                        {
                            System.Drawing.Image imagefile = System.Drawing.Image.FromStream(streamup);
                            int width = imagefile.Width;
                            int height = imagefile.Height;
                            string newfilename = photopath + Snum + ftype.ToLower();
                            MakePhotoDir();
                            string gifurl = photopath + Snum + ".gif";
                            string jpgurl = photopath + Snum + ".jpg";
                            File.Delete(HttpContext.Current.Server.MapPath(gifurl));
                            File.Delete(HttpContext.Current.Server.MapPath(jpgurl));
                            System.Threading.Thread.Sleep(200);
                            string newfilepath = HttpContext.Current.Server.MapPath(newfilename);

                            if (width < whdefine && height < whdefine)
                            {
                                imagefile.Save(newfilepath);
                                imagefile.Dispose();
                                msg = "1";
                            }
                            else
                            {
                                int thumbwidth = 320;
                                int thumbheight = height * 320 / width;
                                System.Drawing.Image img = imagefile.GetThumbnailImage(thumbwidth, thumbheight, null, IntPtr.Zero);
                                imagefile.Dispose();
                                img.Save(newfilepath);
                                img.Dispose();
                                msg = "2";
                            }
                            streamup.Close();
                        }
                        catch
                        {
                            msg = "3";
                            streamup.Close();
                            return msg;
                        }
                    }
                    else
                    {
                        msg = "4";
                    }
                }
                else
                {
                    msg = "5";
                }
            }
            return msg;
        }
        /// <summary>
        /// 如果不存在，创建目录
        /// </summary>
        /// <param name="savepath">物理路径</param>
        public static void MakePhotoDir()
        {
            string savepath = HttpContext.Current.Server.MapPath(photopath);
            if (!Directory.Exists(savepath))
            {
                Directory.CreateDirectory(savepath);
            }
        }

        /// <summary>
        /// 获取图片的长度和宽度属性，超大图片按指定比例缩小
        /// </summary>
        public class Facephoto
        {
            private int _width;
            private int _height;
            private bool _exist = false;
            public Facephoto(string photourl)
            {
                string photopath = HttpContext.Current.Server.MapPath(photourl);
                if (File.Exists(photopath))
                {
                    try
                    {
                        System.Drawing.Image img = System.Drawing.Image.FromFile(photopath);
                        _width = img.Width;
                        _height = img.Height;
                        if (_width > 160)
                        {
                            _height = _height * 160 / _width;
                            _width = 160;
                        }
                        img.Dispose();
                        _exist = true;
                    }
                    catch
                    {
                        _exist = false;
                    }
                }
            }
            public int Width
            {
                set { _width = value; }
                get { return _width; }            
            }
            public int Height
            {
                set { _height = value; }
                get { return _height; }

            }
            public bool Exist
            {
                set { _exist = value; }
                get { return _exist; }
            }
        }
    }
}
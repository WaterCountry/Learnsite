using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Drawing;
using System.Text;

namespace LearnSite.Common
{
    /// <summary>
    ///FileDown 的摘要说明
    /// </summary>
    public class FileDown
    {
        public FileDown()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private static string browseSelect(string filenamea)
        {
            int  browserStr = HttpContext.Current.Request.Browser.Browser.Length;
            if (browserStr !=2)
            {
                return HttpContext.Current.Server.UrlDecode(Path.GetFileName(filenamea));
                //如果不是IE则解码
            }
            else
            {
                return System.Web.HttpUtility.UrlEncode(Path.GetFileName(filenamea), System.Text.Encoding.UTF8);//解决文件名乱码
                //return  ToHexString(Path.GetFileName(filenamea));
                //如果是IE则编码
            }            
        }
        private static string browseSelectNew(string filenamea)
        {
            FileNameInfo fni = new FileNameInfo(filenamea);
            string newfilename = Common.WordProcess.GetRandomNum(100).ToString() + "." + fni.Ext;
            return newfilename;
        }
        private static string browseSelectOld(string filenamea)
        {
            return System.Web.HttpUtility.UrlEncode(Path.GetFileName(filenamea), System.Text.Encoding.UTF8);//解决文件名乱码               
        }
        /// <summary>
        /// 参数为虚拟路径
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static string FileNameExtension(string FileName)
        {
            return Path.GetExtension(MapPathFile(FileName));
        }
        /// <summary>
        /// 获取物理地址
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static string MapPathFile(string FileName)
        {
            return HttpContext.Current.Server.MapPath(FileName);
        }
        public static string DownLoadSelectff(string FileName)
        {
            string str = "";
            string downurl = "http://" + Computer.GetServerIp() + "/Plugins/download.aspx?Id=" +LearnSite.Common.EnDeCode.Encrypt(FileName,"ls");
            string openurl = "<script>window.open('" + downurl + "','_blank')</script>";
            HttpContext.Current.Response.Write(openurl);//原窗口保留，另外新增一个新页面
            str = downurl;
            return str;
        }

        public static string DownLoadView(string downfilename)
        {
            string str = "无法预览";
            string ext = LearnSite.Common.WordProcess.getext(downfilename);
            if (string.IsNullOrEmpty(ext))
                ext = "htm";
            str = LearnSite.Common.WordProcess.SelectWriteNew(ext, downfilename, true);

            return str;
        }      
        public static string DownLoadOut(string downfilename)
        {
            string str = "";
            if (downfilename.EndsWith("/"))
            {
                string cururl = HttpContext.Current.Request.Url.ToString();
                int cur = cururl.IndexOf("Plugins") - 1;
                string weburl = cururl.Substring(0, cur) + downfilename.Replace("~", "");// +"index.htm";
                HttpContext.Current.Response.Redirect(weburl);
                str = weburl;
            }
            else
            {
                string ft = WordProcess.getext(downfilename);

                switch (ft)
                {
                    case "rar":
                        DownLoadrar(downfilename);
                        break;
                    case "swf":
                    case "docx":
                    case "pptx":
                    case "xlsx":
                    case "e":
                    case "gif":
                        DownLoadold(downfilename);
                        break;
                    default:
                        DownLoadold(downfilename);
                        //DownLoad(downfilename); 作品重新提交后无法下载，难道有缓存？
                        break;
                }
            }
            return str;
        }
        /// <summary>
        ///使用WriteFile下载文件，参数为文件虚拟路径
        /// </summary>
        /// <param name="FileName"></param>
        public static void DownLoadold(string FileName)
        {
            string destFileName = MapPathFile(FileName);
            if (File.Exists(destFileName))
            {
                FileInfo fi = new FileInfo(destFileName);
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = false;
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + browseSelectOld(destFileName));
                HttpContext.Current.Response.AppendHeader("Content-Length", fi.Length.ToString());
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.WriteFile(destFileName);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }
        /// <summary>
        /// 使用OutputStream.Write分块下载文件，参数为文件虚拟路径（无法下载swf)
        /// </summary>
        /// <param name="FileName"></param>
        public static void DownLoadrar(string FileName)
        {
            string destFileName = MapPathFile(FileName);
            if (File.Exists(destFileName))
            {
                FileInfo fi = new FileInfo(destFileName);
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = false;
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + browseSelectOld(destFileName));
                HttpContext.Current.Response.AppendHeader("Content-Length", fi.Length.ToString());
                HttpContext.Current.Response.ContentType = "application/x-msdownload ; Charset=utf-8";
                HttpContext.Current.Response.WriteFile(destFileName);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }        
        /// <summary>
        /// 使用OutputStream.Write分块下载文件，参数为文件虚拟路径（无法下载swf)
        /// </summary>
        /// <param name="FileName"></param>
        public static void DownLoad(string FileName)
        {
            string filePath = MapPathFile(FileName);
            if (File.Exists(filePath))
            {
                //指定块大小   
                long chunkSize = 102400;
                //建立一个100K的缓冲区   
                byte[] buffer = new byte[chunkSize];
                //已读的字节数   
                long dataToRead = 0;
                FileStream stream = null;
                try
                {
                    //打开文件   
                    stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    dataToRead = stream.Length;

                    //添加Http头   
                    HttpContext.Current.Response.ContentType = "application/octet-stream";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachement;filename=" + browseSelectOld(filePath));
                    HttpContext.Current.Response.AddHeader("Content-Length", dataToRead.ToString());

                    while (dataToRead > 0)
                    {
                        if (HttpContext.Current.Response.IsClientConnected)
                        {
                            int length = stream.Read(buffer, 0, Convert.ToInt32(chunkSize));
                            HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                            HttpContext.Current.Response.Flush();
                            HttpContext.Current.Response.Clear();
                            dataToRead -= length;
                        }
                        else
                        {
                            //防止client失去连接   
                            dataToRead = -1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (stream != null)
                    {
                        stream.Close();
                    }
                    HttpContext.Current.Response.Write("Error:" + ex.Message);
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Close();
                    }
                    HttpContext.Current.Response.Close();
                }
            }
        }        
        /// <summary>
        /// （学案包专用下载输出，下载流一定要关闭，不然重打包出错)  后缀重命名下载后损坏
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="newfilename"></param>
        public static void DownLoadPackage(string FileName, string newfilename)
        {
            string filePath = MapPathFile(FileName);
            if (File.Exists(filePath))
            {
                //指定块大小   
                long chunkSize = 102400;
                //建立一个100K的缓冲区   
                byte[] buffer = new byte[chunkSize];
                //已读的字节数   
                long dataToRead = 0;
                FileStream stream = null;
                try
                {
                    //打开文件   
                    stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    dataToRead = stream.Length;

                    //添加Http头   
                    HttpContext.Current.Response.ContentType = "application/octet-stream";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachement;filename=" + browseSelectOld(newfilename).Replace("+", ""));
                    HttpContext.Current.Response.AddHeader("Content-Length", dataToRead.ToString());

                    while (dataToRead > 0)
                    {
                        if (HttpContext.Current.Response.IsClientConnected)
                        {
                            int length = stream.Read(buffer, 0, Convert.ToInt32(chunkSize));
                            HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                            HttpContext.Current.Response.Flush();
                            HttpContext.Current.Response.Clear();
                            dataToRead -= length;
                        }
                        else
                        {
                            //防止client失去连接   
                            dataToRead = -1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (stream != null)
                    {
                        stream.Close();
                    }
                    HttpContext.Current.Response.Write("Error:" + ex.Message);
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Close();
                    }
                    HttpContext.Current.Response.Close();
                }
            }
        }

        public static void DownPackageFile(string FileName, string newfileName)
        {
            string filePath = MapPathFile(FileName);
            if (File.Exists(filePath))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachement;filename=" + browseSelectOld(newfileName).Replace("+", ""));
                HttpContext.Current.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                HttpContext.Current.Response.AddHeader("Content-Transfer-Encoding", "binary");
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                HttpContext.Current.Response.WriteFile(fileInfo.FullName);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }

        /// <summary>
        /// PSD文件页面上直接输出
        /// </summary>
        /// <param name="filenamep">物理路径</param>
        private static void Psdout(string filenamep)
        {
            Bitmap bmp = psdToBmp.myImg(filenamep);
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            HttpContext.Current.Response.BinaryWrite(ms.GetBuffer());
            HttpContext.Current.Response.End();
            ms.Dispose();
        }

        /// <summary>
        /// PSD文件另存为jpg，并返回jpg格式的相对路径  参数FileName为虚拟路径
        /// </summary>
        /// <param name="filenamep">虚拟路径</param>
        public static string PsdToJpg(string FileName)
        {
            string jpgfilename = FileName.Replace(".psd", ".jpg");
            string jpgpath = MapPathFile(jpgfilename);//jpg的物理路径
            string psdpath = MapPathFile(FileName);//psd的物理路径
            bool jpgexist = File.Exists(jpgpath);
            FileInfo psdfi = new FileInfo(psdpath);
            long ln = psdfi.Length / 1024 / 1024;//转换成Mb
            if (ln < 100)
            {
                if (!jpgexist)//如果不存在jpg则转换
                {
                    
                    Bitmap bmp =psdToBmp.myImg(psdpath);
                    bmp.Save(jpgpath, System.Drawing.Imaging.ImageFormat.Jpeg);//另存为jpg格式
                    bmp.Dispose();
                }
                else
                {
                    FileInfo jpgfi = new FileInfo(jpgpath);
                    if (DateTime.Compare(psdfi.LastWriteTime, jpgfi.LastWriteTime) > 0)//如果psd修改的日期比jpg迟，说明重新提交过了
                    {
                        Bitmap bmp = psdToBmp.myImg(psdpath);
                        bmp.Save(jpgpath, System.Drawing.Imaging.ImageFormat.Jpeg);//再次另存为jpg格式
                        bmp.Dispose();
                    }
                }
                return jpgfilename;//返回jpg格式的相对路径
            }
            else
            {
                return FileName;//大于100Mb的不另存，直接返回psd的相对路径
            }
        }



        /// <summary>
        /// 为字符串中的非英文字符编码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToHexString(string s)
        {
            char[] chars = s.ToCharArray();
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < chars.Length; index++)
            {
                bool needToEncode = NeedToEncode(chars[index]);
                if (needToEncode)
                {
                    string encodedString = ToHexString(chars[index]);
                    builder.Append(encodedString);
                }
                else
                {
                    builder.Append(chars[index]);
                }
            }

            return builder.ToString();
        }

        /// <summary>
        ///指定 一个字符是否应该被编码
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        private static bool NeedToEncode(char chr)
        {
            string reservedChars = "$-_.+!*'(),@=&";

            if (chr > 127)
                return true;
            if (char.IsLetterOrDigit(chr) || reservedChars.IndexOf(chr) >= 0)
                return false;

            return true;
        }

        /// <summary>
        /// 为非英文字符串编码
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        private static string ToHexString(char chr)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] encodedBytes = utf8.GetBytes(chr.ToString());
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < encodedBytes.Length; index++)
            {
                builder.AppendFormat("%{0}", Convert.ToString(encodedBytes[index], 16));
            }
            return builder.ToString();
        }


    }
}
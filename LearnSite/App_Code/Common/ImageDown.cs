using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;

namespace LearnSite.Common
{
/// <summary>
///ImageDown 的摘要说明
/// </summary>
    public class ImageDown
    {
        public ImageDown()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 自动远程上传图片模块
        /// </summary>
        /// <param name="fck">编辑器内容</param>
        /// <param name="coursePath">保存相对路径</param>
        /// <returns></returns>
        public static string UploadRemote(string fck, string coursePath)
        {
            ArrayList InputBoxVlaue = new ArrayList();
            //获得远程图片UrJ地址列表
            InputBoxVlaue = GetImgTag(fck);
            ///循环读取图片数据并上传
            for (int i = 0; i <= InputBoxVlaue.Count - 1; i++)
            {
                ///读取图片数据.上传图片,返回上传图片路径。
                string Newurl = ReadWriteRemoteData(InputBoxVlaue[i].ToString(), coursePath);
                if (Newurl != "")
                {
                    string pattern = InputBoxVlaue[i].ToString();
                    ///替换远程图片路径为新上传的图片路径
                    Regex reg = new Regex(pattern);
                    fck = reg.Replace(fck, Newurl);
                }
            }
            return fck;
        }


        private static string ReadWriteRemoteData(string Url, string coursePath)
        {
            String FilePath = HttpContext.Current.Server.MapPath(coursePath);
            string NewFlieName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
            ///读取对象并上传
            WebClient wc = new WebClient();
            try
            {
                wc.DownloadFile(Url, FilePath + NewFlieName);
                return coursePath.Replace("~", "") + NewFlieName;
            }
            catch
            {
                return "";
            }
        }
        /// <summary>   
        /// 获取图片标志   
        /// </summary>   
        private static ArrayList GetImgTag(string htmlStr)
        {
            ArrayList strAry = new ArrayList();
            Regex regObj = new Regex("<img.+?>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (Match matchItem in regObj.Matches(htmlStr))
            {
                strAry.Add(GetImgUrl(matchItem.Value));
            }
            return strAry;
        }

        /// <summary>   
        /// 获取图片URL地址   
        /// </summary>   
        private static string GetImgUrl(string imgTagStr)
        {
            string str = "";
            Regex regObj = new Regex("http://.+.(?:jpg|gif|bmp|png)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (Match matchItem in regObj.Matches(imgTagStr))
            {
                str = matchItem.Value;
            }
            return str;
        }
    }    
}
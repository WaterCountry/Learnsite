using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
namespace LearnSite.Common
{
    /// <summary>
    ///Template 的摘要说明
    /// </summary>
    public class Template
    {
        public Template()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static string mp3htm(string ext)
        {
            string strhtm = "~/Plugins/mp3/src.htm";
            if (ext == "mp3")
                strhtm = "~/Plugins/mp3/mp3.htm";//如果是mp3，则使用mp3 flash player
            return readhtm(strhtm);
        }

        public static string flashhtm()
        {
            string strhtm = "~/Plugins/flash.htm";
            if (IsFlashLoop())
                strhtm = strhtm.Replace("loop: '0' ", "loop: '1' ");//"~/Plugins/flashloop.htm";
            else
                strhtm = strhtm.Replace("loop: '1' ", "loop: '0' ");//"~/Plugins/flashloop.htm";

            return readhtm(strhtm);
        }
        //flash播放参数自动循环设置
        public static bool IsFlashLoop()
        {
            bool fLoop = false;
            string hid = "";
            if (HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
            {
                hid = HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
            }
            if (HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname] != null)
            {
                hid = HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Rhid"].ToString();
            }
            if (HttpContext.Current.Session["FlashLoop" + hid] != null)
            {
                fLoop = bool.Parse(HttpContext.Current.Session["FlashLoop" + hid].ToString());
            }
            return fLoop;
        }

        public static string officehtm()
        {
            string strhtm = "~/Plugins/office.htm";
            return readhtm(strhtm);
        }

        public static string webofficetwohtm()
        {
            string strhtm = "~/weboffice/webofficetwo.htm";
            return readhtm(strhtm);
        }
        public static string webofficehtm()
        {
            string strhtm = "~/weboffice/weboffice.htm";
            return readhtm(strhtm);
        }
        public static string scratchhtm()
        {
            string strhtm = "~/Plugins/scratch/scratchbrowser.htm";
            return readhtm(strhtm);
        }
        public static string sketchuphtm()
        {
            string strhtm = "~/Plugins/sketchup/sketchup.htm";
            return readhtm(strhtm);
        }
        public static string scratchflashnewhtm()
        {
            string strhtm = "~/Statics/scratch.htm";
            return readhtm(strhtm);
        }
        public static string scratchflashhtm()
        {
            string strhtm = "~/Plugins/scratch/scratch.htm";
            return readhtm(strhtm);
        }
        public static string photohtm(bool toswf)
        {
            string strhtmsrc = "~/Plugins/Photo/src.htm";
            string strhtmswf = "~/Plugins/Photo/photo.htm";
            if (toswf)
                return readhtm(strhtmswf);//转swf显示
            else
                return readhtm(strhtmsrc);//直接显示
        }
        public static string jwplayer()
        {
            string strhtm = "~/Plugins/Photo/photo.htm";
            return readhtm(strhtm);//直接显示
        }
        public static string flvhtm()
        {
            string strhtm = "~/Plugins/Flv/flv.htm";
            return readhtm(strhtm);//直接显示
        }
        public static string freemindhtm()
        {
            string strhtm = "~/Plugins/freemind/freemindbrowser.htm";
            return readhtm(strhtm);
        }
        private static string readhtm(string filepath)
        {
            string str = "找不到预览的插件模板！<br/>"+filepath;
            string fpath = HttpContext.Current.Server.MapPath(filepath);
            if (File.Exists(fpath))
            {
                StreamReader fr = new StreamReader(fpath, System.Text.Encoding.Default);
                str = fr.ReadToEnd();
                fr.Close();
                fr.Dispose();
            }
            return str;
        }
        /// <summary>
        /// 修改txt中的内容
        /// </summary>
        private static void UpdateTextFile(string FileName, string oldStr, string newStr)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(FileName, System.Text.Encoding.Default);//GetEncoding("gb2312 "));
            string s = sr.ReadToEnd();
            sr.Close();
            s = s.Replace(oldStr, newStr);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(FileName, false);
            sw.Write(s);
            sw.Close();
        }
        /// <summary>
        /// 无效
        /// </summary>
        /// <param name="upmodel"></param>
        public static void UpdateRoadJs(string upmodel)
        {
            string jsfile = "~/Js/road.js";
            string jspath = HttpContext.Current.Server.MapPath(jsfile);
            if (File.Exists(jspath))
            {
                string oldStr = "0';";
                string newStr = "1';";
                if (upmodel == "0")
                    UpdateTextFile(jspath, oldStr, newStr);//0换1
                else
                    UpdateTextFile(jspath, newStr, oldStr);//反过来1换0
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using System.IO;

namespace LearnSite.Common
{
    /// <summary>
    ///XmlHelp 的摘要说明
    /// </summary>
    public class XmlHelp
    {
        public XmlHelp()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static bool GetHouseMode()
        {
            string res = GetTypeName("HouseMode");
            if (res.Equals("0"))
                return false;
            else
                return bool.Parse(res);
        }
        public static bool RightSite()
        {
            string oldorg = "www.learnsite.org";
            string host = HttpContext.Current.Request.Url.Host;
            string sethost = GetHostName();
            if (sethost != "0")
            {
                if (sethost.Contains(host))
                    return true;
                if (sethost.Contains(oldorg))
                    return true;
                else
                    return false;
            }
            else
                return true;
        }

        public static string GetHostName()
        {
            return GetTypeName("HostName");
        }

        private static string FieldStr = "value";
        public static int ShareDiskLimit()
        {
            int dd = Int32.Parse(GetTypeName("ShareDiskLimit"));
            if (dd == 0)
                dd = 30;
            return dd;
        }

        //TeacherRepeatLogin
        public static bool GetRepeatLogin()
        {
            string res = GetTypeName("TeacherRepeatLogin");
            if (res.Equals("0"))
                return true;
            else
                return bool.Parse(res);
        }

        public static bool GetLogin()
        {
            string res = GetTypeName("TeacherLogin");
            if (res.Equals("0"))
                return true;
            else
                return bool.Parse(res);
        }
        public static string GetKindeditor()
        {
            return GetTypeName("Kindeditor");
        }
        public static string GetFtpPort()
        {
            return GetTypeName("FtpPort");
        }
        public static string GetUploadMode()
        {
            return GetTypeName("UploadMode");
        }
        public static string GetGroupMax()
        {
            return GetTypeName("GroupMax");
        }
        public static string GetCookiesFix()
        {
            return GetTypeName("CookiesFix");
        }
        public static string GetNameEdit()
        {
            return GetTypeName("NameEdit");
        }
        public static bool GetAutoHostName()
        {
            return bool.Parse(GetTypeName("AutoHostName"));
        }
        public static string GetSignSort()
        {
            return GetTypeName("SignSort");
        }
        public static bool GetSingleLogin()
        {
            return bool.Parse(GetTypeName("SingleLogin"));
        }
        public static string GetSexEdit()
        {
            return GetTypeName("SexEdit");
        }
        public static string GetPhotoEdit()
        {
            return GetTypeName("PhotoEdit");
        }
        public static string GetClassMax()
        {
            return GetTypeName("ClassMax");
        }
        public static string GetClassEdit()
        {
            return GetTypeName("ClassEdit");
        }
        public static string GetStudentCookiesPeriod()
        {
            return GetTypeName("StudentCookiesPeriod");
        }
        public static bool GetWorkIpLimit()
        {
            return bool.Parse(GetTypeName("WorkIpLimit"));
        }
        public static string GetWorkDowntime()
        {
            return GetTypeName("WorkDowntime");
        }
        public static string GetTypeName(string typename)
        {
            return GetValue(typename, FieldStr);
        }
        public static string WeatherLimit()
        {
            return GetTypeName("WeatherLimit");
        }
        public static int LoginMode()
        {
            return Int32.Parse(GetTypeName("LoginMode"));
        }

        public static string SiteTitle()
        {
            return GetTypeName("SiteTitle");
        }
        public static bool WebVote_Limit()
        {
            return bool.Parse(GetTypeName("WebVote_Limit"));
        }
        public static string AllowFileType()
        {
            return GetTypeName("AllowFileType");
        }
        public static string AllowDir()
        {
            return GetTypeName("AllowDir");
        }

        /* =====================设置代码=====================*/
        public static bool SetHouseMode(string HouseModeStr)
        {
            return SetValue("HouseMode", FieldStr, HouseModeStr);
        }
        public static bool SetRepeatLogin(string repeat)
        {
            return SetValue("TeacherRepeatLogin", FieldStr, repeat);
        }
        public static bool SetLogin(string LoginStr)
        {
            return SetValue("TeacherLogin", FieldStr, LoginStr);
        }
        public static bool SetFtpPort(string FtpPortStr)
        {
            return SetValue("FtpPort", FieldStr, FtpPortStr);
        }
        public static bool SetUploadMode(string UploadModeStr)
        {
            return SetValue("UploadMode", FieldStr, UploadModeStr);
        }
        public static bool SetGroupMax(string GroupMaxStr)
        {
            return SetValue("GroupMax", FieldStr, GroupMaxStr);
        }
        public static bool SetCookiesFix(string CookiesFixStr)
        {
            return SetValue("CookiesFix", FieldStr, CookiesFixStr);
        }
        public static bool SetNameEdit(string NameEditStr)
        {
            return SetValue("NameEdit", FieldStr, NameEditStr);
        }
        public static bool SetAutoHostName(string AutoHostName)
        {
            return SetValue("AutoHostName", FieldStr, AutoHostName);
        }
        public static bool SetSignSort(string SignSort)
        {
            return SetValue("SignSort", FieldStr, SignSort);
        }

        public static bool SetSingleLogin(string SingleLogin)
        {
            return SetValue("SingleLogin", FieldStr, SingleLogin);
        }
        public static bool SetSexEdit(string SexEditLimit)
        {
            return SetValue("SexEdit", FieldStr, SexEditLimit);
        }
        public static bool SetPhotoEdit(string PhotoEditLimit)
        {
            return SetValue("PhotoEdit", FieldStr, PhotoEditLimit);
        }

        public static bool SetClassEdit(string ClassEditLimit)
        {
            return SetValue("ClassEdit", FieldStr, ClassEditLimit);
        }

        public static bool SetStudentCookiesPeriod(string cookiestr)
        {
            return SetValue("StudentCookiesPeriod", FieldStr, cookiestr);
        }
        public static bool SetWorkIpLimit(bool workiplimitstr)
        {
            return SetValue("WorkIpLimit", FieldStr, workiplimitstr.ToString());
        }
        public static bool SetWorkDowntime(string days)
        {
            return SetValue("WorkDowntime", FieldStr, days);
        }
        public static bool SetWeatherLimit(string weatherstr)
        {
            return SetValue("WeatherLimit", FieldStr, weatherstr);
        }
        public static bool SetSiteTitle(string sitestr)
        {
            return SetValue("SiteTitle", FieldStr, sitestr);
        }
        public static bool SetWebVote_Limit(bool IsLimit)
        {
            return SetValue("WebVote_Limit", FieldStr, IsLimit.ToString());
        }
        public static bool SetAllowFileType(string allowtype)
        {
            return SetValue("AllowFileType", FieldStr, allowtype);
        }
        public static bool SetAllowDir(string allowdir)
        {
            return SetValue("AllowDir", FieldStr, allowdir);
        }
        public static bool SetLoginMode(string lmode)
        {
            return SetValue("LoginMode", FieldStr, lmode);
        }
        /// <summary>
        /// 获得多少时间后下载
        /// </summary>
        /// <returns></returns>
        public static int GetDowntime()
        {
            int Downtime = 60;
            string Downtimestr = GetValue("Downtime", FieldStr);
            if (LearnSite.Common.WordProcess.IsNum(Downtimestr))
                Downtime = Int32.Parse(Downtimestr);
            return Downtime;
        }
        /// <summary>
        /// 设置多少时间后下载
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool SetDowntime(string strminute)
        {
            return SetValue("Downtime", FieldStr, strminute);
        }
        /// <summary>
        /// 获取下载限制，返回真假
        /// </summary>
        /// <returns></returns>
        public static bool DowncanIs()
        {
            return bool.Parse(GetTypeName("Downcan"));
        }
        /// <summary>
        /// 设置下载限制
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool SetDowncan(bool check)
        {
            return SetValue("Downcan", FieldStr, check.ToString());
        }

        /// <summary>
        /// 读取学期
        /// </summary>
        /// <returns></returns>
        public static string GetTerm()
        {
            return GetTypeName("Term");
        }
        /// <summary>
        /// 获取当前学期，如果无返回0，判断是否数字
        /// </summary>
        /// <returns></returns>
        public static int GetIntTerm()
        {
            int Cterm = 0;
            string strTerm = GetTerm();
            if (LearnSite.Common.WordProcess.IsNum(strTerm))
            {
                Cterm = Int32.Parse(strTerm);
            }
            return Cterm;
        }
        /// <summary>
        /// 设置学期
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public static bool SetTerm(string term)
        {
            return SetValue("Term", FieldStr, term);
        }

        #region  读取写入基本信息

        private static string xmlFile()
        {
            string xmlFileName = HttpContext.Current.Server.MapPath("~/") + @"\website.xml";
            return xmlFileName;
        }
        /// <summary>
        /// 获取xml文件到XmlDocument
        /// </summary>
        /// <returns></returns>
        /// 
        private static XmlDocument siteXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFile());
            return doc;
        }
        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="key">元素</param>
        /// <param name="atb">属性</param>
        /// <returns></returns>
        private static string GetValue(string key, string atb)
        {
            string mystr = "0";
            XmlDocument xmlDoc = siteXml();
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("/LearnSite/website").ChildNodes;//获取/LearnSite/mysite节点的所有子节点
            foreach (XmlNode xn in nodeList)//遍历所有子节点 
            {
                XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型 
                if (xe.GetAttribute("key") == key)//如果genre属性值为“张三” 
                {
                    mystr = xe.GetAttribute(atb);
                    break;
                }
            }

            return mystr.Trim();
        }
        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="key">元素</param>
        /// <param name="atb">属性</param>
        /// <param name="str">值</param>
        /// <returns></returns>
        private static bool SetValue(string key, string atb, string str)
        {
            bool isSet = false;
            XmlDocument xmlDoc = siteXml();
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("/LearnSite/website").ChildNodes;//获取/LearnSite/website节点的所有子节点
            foreach (XmlNode xn in nodeList)//遍历所有子节点 
            {
                XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型 
                if (xe.GetAttribute("key") == key)//如果key属性值为传递参数key的值 
                {
                    xe.SetAttribute(atb, str);//则修改value属性为传递参数value的值
                    isSet = true;
                    try
                    {
                        xmlDoc.Save(xmlFile());
                    }
                    catch
                    {
                        string msg = "网站根目录下website.xml文件为只读，无法修改系统配置！";
                        LearnSite.Common.WordProcess.jsdialog(msg);
                    }
                    break;
                }
            }
            return isSet;
        }


        #endregion
    }
}
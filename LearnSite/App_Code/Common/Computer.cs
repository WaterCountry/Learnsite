using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Runtime.InteropServices;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
namespace LearnSite.Common
{
/// <summary>
/// Computer 的摘要说明
/// </summary>
public class Computer
{
	public Computer()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    /// <summary>
    /// 判断是否同网段
    /// </summary>
    /// <returns></returns>
    public static bool IsSameNet()
    {
        string sevip = GetServerIp();
        string userip = GetGuestIP();
        sevip = Cutipnet(sevip);
        userip = Cutipnet(userip);
        return sevip.Equals(userip);
    }

    private static string Cutipnet(string ip)
    {
        int lastpoint = ip.IndexOf('.');//换IP的第一个字段数字，因为内网可能有多个网段 ip.LastIndexOf('.');
        if (lastpoint > -1)
        {
            ip = ip.Substring(0, lastpoint);
        }
        return ip;
    }
    public static string ServerUrl()
    {
        string strServer = "http://" +HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString();
        string strPort = ":" + Convert.ToString(HttpContext.Current.Request.ServerVariables["SERVER_PORT"]);
        string strRoot = "/";
        if (strPort.Trim() == ":80")
        {
            strPort = "";
        }
        string strUrl = strServer + strPort + strRoot;
        return strUrl;
    }
        /// <summary>
        /// 获取登录到现在相隔过去的时间（以分钟为单位），返回int值
        /// </summary>
        /// <returns></returns>
        public static int TimePassed()
        {
            int passtime = 0;
            if (HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname] != null)
            {
                string LoginTime = HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["LoginTime"].ToString();
                DateTime time1 = DateTime.Parse(LoginTime);
                DateTime time2 = DateTime.Now;
                passtime = Int32.Parse(DatagoneMinute(time1, time2));
            }
            return passtime;
        }
        /// <summary>
        /// 获取登录到现在相隔过去的时间（以分钟为单位），返回int值
        /// </summary>
        /// <returns></returns>
        public static int TimePassed(string LoginTime)
        {
            DateTime time1 = DateTime.Parse(LoginTime);
            DateTime time2 = DateTime.Now;
            return Int32.Parse(DatagoneMinute(time1, time2));
        }

        /// <summary>
        /// 判断该日期是否为今天
        /// </summary>
        /// <param name="oldTime"></param>
        /// <returns></returns>
        public static bool IsToday(string oldTime)
        {
            try
            {
                DateTime today = DateTime.Now;
                DateTime oldday = DateTime.Parse(oldTime);
                TimeSpan ts = today - oldday;
                if (ts.Days > 0)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }
    /// <summary>
    /// 返回Time1-Time2后的天数
    /// </summary>
    /// <param name="Time1"></param>
    /// <param name="Time2"></param>
    /// <returns></returns>
        public static int Daygone(DateTime Time1, DateTime Time2)
        {
            TimeSpan ts = Time1 - Time2;
            double dnum = Math.Round(ts.TotalDays, 0);
            return Convert.ToInt32(dnum);
        }
        /// <summary>
        /// 返回OldTime后的天数
        /// </summary>
        /// <param name="OldTime"></param>
        /// <returns></returns>
        public static int Daysgone(DateTime OldTime)
        {
            DateTime today = DateTime.Now;
            TimeSpan ts = today - OldTime;
            double dnum = Math.Round(ts.TotalDays, 0);
            return Convert.ToInt32(dnum);
        }
        /// <summary>
        /// 返回Time后的小时数
        /// </summary>
        /// <param name="OldTime"></param>
        /// <returns></returns>
        public static int Hourgone(DateTime OldTime)
        {
            DateTime today = DateTime.Now;
            TimeSpan ts = today - OldTime;
            double dnum = Math.Round(ts.TotalHours, 0);
            return Convert.ToInt32(dnum);
        }
        /// <summary>
        /// 获取时间间隔，以秒为单位
        /// </summary>
        /// <param name="Time1"></param>
        /// <param name="Time2"></param>
        /// <returns></returns>
        public static string Datagone(DateTime Time1, DateTime Time2)
        {
            TimeSpan ts = new TimeSpan(Time2.Ticks - Time1.Ticks);
            return ts.Seconds.ToString();
        }
        /// <summary>
        /// 获取时间间隔，以毫秒为单位
        /// </summary>
        /// <param name="Time1">旧</param>
        /// <param name="Time2">新</param>
        /// <returns></returns>
        public static string DatagoneMilliseconds(DateTime Time1, DateTime Time2)
        {
            TimeSpan ts = new TimeSpan(Time2.Ticks - Time1.Ticks);
            return ts.TotalMilliseconds.ToString();
        }
        /// <summary>
        /// 获取时间间隔，以分为单位(Time1旧，Time2新)
        /// </summary>
        /// <param name="Time1">旧</param>
        /// <param name="Time2">新</param>
        /// <returns></returns>
        public static string DatagoneMinute(DateTime Time1, DateTime Time2)
        {
            TimeSpan ts = new TimeSpan(Time2.Ticks - Time1.Ticks);
            return ts.Minutes.ToString();
        }
        /// <summary>
        /// .NET解释引擎版本
        /// </summary>
        /// <returns></returns>
        public static string GetNetCLR()
        {
            string str = Environment.Version.Major + "." + Environment.Version.Minor + "." + Environment.Version.Build + "." + Environment.Version.Revision;
            return str;
        }
        /// <summary>
        /// 虚拟目录Session总数
        /// </summary>
        /// <returns></returns>
        public static string GetSessionCount()
        {
            return HttpContext.Current.Session.Count.ToString();
        }
        /// <summary>
        /// 服务器区域语言
        /// </summary>
        /// <returns></returns>
        public static string GetServerLanguage()
        {
            string str = HttpContext.Current.Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"].ToString();
            return str;
        }
        /// <summary>
        /// 本页执行时间
        /// </summary>
        /// <returns></returns>
        public static string GetPageprocess()
        {
            return HttpContext.Current.Server.ScriptTimeout.ToString() ;
        }
       //2014-3-14 要穿透代理
        public static string MyIp()
        {
            return GetGuestIP();
        }
        /// <summary>
        /// 获得客户端用户IP
        /// </summary>
        /// <returns></returns>
        public static string GetGuestIP()
        {
            //HTTP_X_FORWARDED_FOR透过代理服务器获取客户端IP
            string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                //如果没有代理则直接获取
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (null == result || result == String.Empty)
            {
                //还是没有获取到就用UserHostAddress获取
                result = HttpContext.Current.Request.UserHostAddress;
            }
            return result;
        }
        private string getIp()
        {
            // 穿过代理服务器取远程用户真实IP地址
            string Ip = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null)
                {
                    if (HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"] != null)
                        Ip = HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"].ToString();
                    else
                        if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
                            Ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                        else
                            Ip = "202.96.134.133";
                }
                else
                    Ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
            {
                Ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            else
            {
                Ip = "202.96.134.133";
            }
            return Ip;
        }
        /// <summary> 
        /// 取得客户端真实IP。如果有代理则取第一个非内网地址 
        /// </summary> 
        public static string IPAddress
        {

            get
            {
                string result = String.Empty;

                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (result != null && result != String.Empty)
                {
                    //可能有代理 
                    if (result.IndexOf(".") == -1)    //没有“.”肯定是非IPv4格式 
                        result = null;
                    else
                    {
                        if (result.IndexOf(",") != -1)
                        {
                            //有“,”，估计多个代理。取第一个不是内网的IP。 
                            result = result.Replace(" ", "").Replace("'", "");
                            string[] temparyip = result.Split(",;".ToCharArray());
                            for (int i = 0; i < temparyip.Length; i++)
                            {

                                if (IsIPAddress(temparyip[i])
                                    && temparyip[i].Substring(0, 3) != "10."
                                    && temparyip[i].Substring(0, 7) != "192.168"
                                    && temparyip[i].Substring(0, 7) != "172.16.")
                                {
                                    return temparyip[i];    //找到不是内网的地址 
                                }
                            }
                        }
                        else if (IsIPAddress(result)) //代理即是IP格式 
                            return result;
                        else
                            result = null;    //代理中的内容 非IP，取IP 
                    }

                }

                string IpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];


                if (null == result || result == String.Empty)
                    result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                if (result == null || result == String.Empty)
                    result = HttpContext.Current.Request.UserHostAddress;

                return result;
            }
        } 
        private static bool IsIPAddress(string str1)
        {
            if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;

            string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";

            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
            return regex.IsMatch(str1);
        } 
        /// <summary>
        /// 获得客户端主机名
        /// </summary>
        /// <returns></returns>
        public static string GetGuestHost(string ssIp)
        {
            try
            {
                System.Net.IPAddress ip = System.Net.IPAddress.Parse(ssIp);
                IPHostEntry iph;
                iph = System.Net.Dns.GetHostEntry(ip);
                return iph.HostName;
            }
            catch
            {
                return ssIp;
            }
        }
        /// <summary>
        /// 获取客户端主机名
        /// </summary>
        /// <param name="hostIp"></param>
        /// <returns></returns>
        public static string GetHostNameByPage(System.Web.UI.Page thePage)
        {
            return thePage.Request.UserHostName;//取不到主机名，取的是IP
        }


        /// <summary>
        /// 获得客户端浏览器IE
        /// </summary>
        /// <returns></returns>
        public static string GetGuestBrowse()
        {
            return HttpContext.Current.Request.Browser.Browser.ToString();
        }

        public static string GetGuestBrowsever()
        {
            return HttpContext.Current.Request.Browser.MajorVersion.ToString();
        }
        /// <summary>
        /// 获得客户端操作系统
        /// </summary>
        /// <returns></returns>
        public static string GetGuestPlatform()
        {
            return HttpContext.Current.Request.Browser.Platform.ToString();
        }
        /// <summary>
        /// 获得服务器IP
        /// </summary>
        /// <returns></returns>
        public static string GetServerIp()
        {
            return HttpContext.Current.Request.ServerVariables.Get("Local_Addr").ToString();
        }
        /// <summary>
        /// 获得服务器名称
        /// </summary>
        /// <returns></returns>
        public static string GetServerHost()
        {
            return HttpContext.Current.Request.ServerVariables.Get("Server_Name").ToString();
        }
        /// <summary>
        /// 获得服务器计算名
        /// </summary>
        /// <returns></returns>
        public static string GetServerName()
        {
            return HttpContext.Current.Server.MachineName;
        }
        /// <summary>
        /// 获得服务器操作系统
        /// </summary>
        /// <returns></returns>
        public static string GetServerOs()
        {
            return System.Environment.OSVersion.ToString();
        }
        /// <summary>
        /// 获得服务器CPU数
        /// </summary>
        /// <returns></returns>
        public static string GetServerCpu()
        {
            return Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS");
        }
        /// <summary>
        /// 获得CPU类型
        /// </summary>
        /// <returns></returns>
        public static string GetServerCpuClass()
        {
            return Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER");
        }
        /// <summary>
        /// 获得信息服务软件
        /// </summary>
        /// <returns></returns>
        public static string GetServerSoftWare()
        {
            return HttpContext.Current.Request.ServerVariables["SERVER_SOFTWARE"];
        }
        /// <summary>
        /// 获得DOTNET 版本
        /// </summary>
        /// <returns></returns>
        public static string GetServerDotNetVer()
        {
            return System.Environment.Version.ToString();
        }
        /// <summary>
        /// 获得脚本超时时间
        /// </summary>
        /// <returns></returns>
        public static string GetServerScriptTimeout()
        {
            return HttpContext.Current.Server.ScriptTimeout.ToString();
        }
        /// <summary>
        /// 获得开机运行时长
        /// </summary>
        /// <returns></returns>
        public static string GetServerTickCount()
        {
            Int32 tc = Environment.TickCount;
            Int32 seconds = tc / 1000;
            Int32 minutes = seconds / 60;
            Int32 hours = minutes / 60;
            Int32 days = hours / 60;
            Int32 lefthours = hours - days * 60;
            string stc = days.ToString() + "天" + lefthours.ToString() + "小时";
            return stc;
            //return ((Double)System.Environment.TickCount / 3600000).ToString("N2");
        }
        /// <summary>
        /// 获得进程开始时间
        /// </summary>
        /// <returns></returns>
        public static string GetServerProcessStartTime()
        {
            string starttime = "";
            try
            {
                starttime = System.Diagnostics.Process.GetCurrentProcess().StartTime.ToString();
            }
            catch
            {
                starttime = "未知";
            }
            return starttime;
        }
        /// <summary>
        /// 获得AspNet内存占用
        /// </summary>
        /// <returns></returns>
        public static string GetServerAspNetWorkingSet()
        {
            string aspnetmemory = "";
            try
            {
                aspnetmemory = ((Double)System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / 1048576).ToString("N2");
            }
            catch
            {
                aspnetmemory = "未知";
            }
            return aspnetmemory;
        }
        /// <summary>
        /// 获得AspNet CPU时间
        /// </summary>
        /// <returns></returns>
        public static string GetServerAspNetCpuTime()
        {
            string aspnetcuptime;
            try
            {
                aspnetcuptime = ((TimeSpan)System.Diagnostics.Process.GetCurrentProcess().TotalProcessorTime).TotalSeconds.ToString("N0");
            }
            catch
            {
                aspnetcuptime = "未知";
            }
            return aspnetcuptime;
        }
        /// <summary>
        /// 获得当前AspNet运行线程数
        /// </summary>
        /// <returns></returns>
        public static string GetServerCurrentThreadsNum()
        {
            int Threadcount = 0;
            foreach (System.Diagnostics.ProcessThread thread in System.Diagnostics.Process.GetCurrentProcess().Threads)
            { Threadcount++; }
            return Threadcount.ToString();
        }

        /// <summary>
        /// 获取今天气温，远程捕获
        /// </summary>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        public static string GetWeatherToday()
        {
            string strUrl = System.Configuration.ConfigurationManager.AppSettings["Weather"].Trim();
            string WeatherToday = "  没有天气预报";
            if (UrlExistsUsingSockets(strUrl))
            {
                WebRequest wreq = WebRequest.Create(strUrl);
                WebResponse wresp = (WebResponse)wreq.GetResponse();
                Stream s = wresp.GetResponseStream();
                StreamReader sr = new StreamReader(s, System.Text.Encoding.GetEncoding("utf-8"));
                string HTML = sr.ReadToEnd();
                int laststr = HTML.LastIndexOf("℃");
                if (laststr > 0)
                {
                    string Newhtml = HTML.Substring(0, laststr + 1);
                    int startstr = Newhtml.LastIndexOf(">");
                    int mylen = laststr - startstr;
                    WeatherToday = Newhtml.Substring(startstr + 1, mylen);
                    WeatherToday = " 今天天气：" + WeatherToday;
                }
            }
            return WeatherToday;
        }

        /// <summary>
        /// 方法一、检测远程url是否存在
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static bool UrlExistsUsingHttpWebRequest(string url)
        {
            try
            {
                System.Net.HttpWebRequest myRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                myRequest.Method = "HEAD";
                myRequest.Timeout = 100;
                System.Net.HttpWebResponse res = (System.Net.HttpWebResponse)myRequest.GetResponse();
                return (res.StatusCode == System.Net.HttpStatusCode.OK);
            }
            catch (System.Net.WebException we)
            {
                System.Diagnostics.Trace.Write(we.Message);
                return false;
            }
        }
        /// <summary>
        /// 方法二、检测远程url是否存在
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static bool UrlExistsUsingSockets(string url)
        {
            if (url.StartsWith("http://")) url = url.Remove(0, "http://".Length);
            try
            {
                System.Net.IPHostEntry ipHost = System.Net.Dns.GetHostEntry(url);//GetHostEntry
                return true;
            }
            catch (System.Net.Sockets.SocketException se)
            {
                System.Diagnostics.Trace.Write(se.Message);
                return false;
            }
        }
    }
}
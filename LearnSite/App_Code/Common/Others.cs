using System;
using System.Collections.Generic;
using System.Web;
namespace LearnSite.Common
{
    /// <summary>
    ///Others 的摘要说明
    /// </summary>
    public class Others
    {
        public Others()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public static void ClearClientPageCache()
        {
            //清除浏览器缓存 
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Expires = 0;
            HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.AddHeader("pragma", "no-cache");
            HttpContext.Current.Response.AddHeader("cache-control", "private");
            HttpContext.Current.Response.CacheControl = "no-cache";
        }
    }
}
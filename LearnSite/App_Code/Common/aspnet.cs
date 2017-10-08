using System;
using System.Configuration;
using System.Web;
using System.Web.Configuration;
namespace LearnSite.Common
{
    /// <summary>
    ///aspnet 的摘要说明
    /// </summary>
    public class aspnet
    {
        public aspnet()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        private static string GetAppSettings(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }

        private static void SetAppSettings(string key, string value)
        {
            string s = ConfigurationManager.AppSettings.Get(key);

            if (string.IsNullOrEmpty(s))
            {
                Configuration c = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
                AppSettingsSection a = c.GetSection("appSettings") as AppSettingsSection;
                a.Settings.Add(key, value);
                c.Save();
            }
            else
            {
                Configuration c = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
                AppSettingsSection a = c.GetSection("appSettings") as AppSettingsSection;
                a.Settings.Remove(key);
                a.Settings.Add(key, value);
                c.Save();
            }
        }

        private static void SetWebConfigLastTime()
        {
            SetAppSettings("resettime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        private static void GetWebConfigLastTime()
        {
            GetAppSettings("resettime");
        }

        /// <summary>
        /// 重启回收应用程序
        /// </summary>
        /// <returns></returns>
        public static bool RecycleApplication()
        {
            bool Success = true;

            //Method #1  
            //   It requires high security permissions, so it may not  
            //   work in your environment  
            try
            {
                HttpRuntime.UnloadAppDomain();
            }
            catch 
            {
                Success = false;
            }
            if (!Success)
            {
                //Method #2  
                //   By 'touching' the Web.config file, the application  
                //   is forced to recycle  
                try
                {
                    SetWebConfigLastTime();
                }
                catch
                {
                    Success = false;
                }

            }
            return Success;
        }


    }

}
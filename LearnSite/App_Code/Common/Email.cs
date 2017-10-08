using System;
using System.Collections.Generic;
using System.Web;
using MailLog;
namespace LearnSite.Common
{
    /// <summary>
    ///Email 的摘要说明
    /// </summary>
    public class Email
    {
        public Email()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="sitename">网站名称</param>
        /// <param name="logger">内容</param>
        /// <param name="attachurl">附件url</param>
        /// <returns></returns>
        public static bool SendEmail(string sitename, string logger, string attachurl)
        {
            return MailLog.MailLog.SendMail(sitename, logger, attachurl);//learnsite@126.com邮箱密码123!%&zxc
        }

        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="sitename">网站名称</param>
        /// <param name="logger">内容</param>
        /// <returns></returns>
        public static bool SendEmail(string sitename, string logger)
        {
            return MailLog.MailLog.SendMail(sitename, logger);
        }
    }
}
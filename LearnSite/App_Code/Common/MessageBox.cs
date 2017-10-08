using System;
using System.Text;
namespace LearnSite.Common
{
    /// <summary>
    /// 显示消息提示对话框。
    /// </summary>
    public class MessageBox
    {
        private MessageBox()
        {
        }

        ///新版本
        /// <summary>
        /// 弹出JavaScript小窗口
        /// </summary>
        /// <param name="js">窗口信息</param>
        public static void Alert(string message,System.Web.UI.Page page)
        {
            string js = @"<Script language='JavaScript'>
                     alert('" + message + "');</Script>";
            if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "alert"))
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "alert", js);
            }
        }
    }
}
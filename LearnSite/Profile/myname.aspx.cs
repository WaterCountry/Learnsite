using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Profile_myname : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            if (!IsPostBack)
            {
                showname();
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }
    protected void Btnsex_Click(object sender, EventArgs e)
    {
        string myname = TextBoxName.Text.Trim();
        if (!string.IsNullOrEmpty(myname))
        {
            if (LearnSite.Common.WordProcess.IsZh(myname))
            {
                string mySnum= Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
                LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
                sbll.ChangeSname(mySnum, myname);
                LearnSite.Common.CookieHelp.EditCookiesItem("StudentCookies", "Sname",Server.UrlEncode(myname));
                System.Threading.Thread.Sleep(500);
                string msg = "修改姓名成功！ 确定跳转";
                LearnSite.Common.WordProcess.Alert(msg, this.Page);
                Response.Redirect("~/Student/myinfo.aspx", false);
            }
            else
            {
                Labelstr.Text = "请输入中文姓名，不能有空格！";
            }
        }
        else
        {
            Labelstr.Text = "请输入你的姓名！";
        }
    }
    private void showname()
    {
        Labelname.Text = Server.UrlDecode(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sname"].ToString());
        int sclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
        int sgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
        LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();       
        if (rbll.GetRnameedit(sgrade,sclass))
        {
            Btnname.Enabled = true;
        }
        else
        {
            Btnname.Enabled = false;
        }
    }
}
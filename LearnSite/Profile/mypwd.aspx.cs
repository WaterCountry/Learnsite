using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Profile_mypwd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            if (!IsPostBack)
            {
                showbtn();
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }
    protected void Btnedit_Click(object sender, EventArgs e)
    {
        string OldPwd = TextBoxoldpwd.Text.Trim();
        string myPwd = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Spwd"].ToString();
        if (LearnSite.Common.WordProcess.GetMD5_8bit(OldPwd) == myPwd)
        {
            string Spwd = TextBoxpwd.Text.Trim();
            string Spwd0 = TextBoxpwd0.Text.Trim();
            if (LearnSite.Common.WordProcess.IsEnNum(Spwd))
            {
                if (Spwd.Length < 19)
                {
                    if (Spwd == Spwd0)
                    {
                        string Snum = Server.UrlDecode(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString());
                        LearnSite.BLL.Students bll = new LearnSite.BLL.Students();
                        bll.UpdatePwd(Snum, Spwd);
                        string strpwd = "修改密码成功！请记牢您的新密码！";
                        LearnSite.Common.WordProcess.Alert(strpwd, this.Page);
                        Btnedit.Enabled = false;
                    }
                    else
                    {
                        Labelmsg.Text = "输入新密码跟确认新密码不一样，请重新输入一致！";
                    }
                }
                else
                {
                    Labelmsg.Text = "密码长度不超过18个字符！";
                    TextBoxpwd.Text = "";
                }
            }
            else
            {
                Labelmsg.Text = "密码必须为英文字母或数字组成！";
                TextBoxpwd.Text = "";
            }
        }
        else
        {
            Labelmsg.Text = "旧密码输入错误！";
        }
    }

    private void showbtn()
    { 
    int loginm = LearnSite.Common.XmlHelp.LoginMode();//获取登录方式 0表示个人密码方式登录 1表示班级密码方式登录
    if (loginm == 0)
    {
        Btnedit.Enabled = true;
    }
    else
    {
        Btnedit.Enabled = false;
        Labelmsg.Text = "班级模式，不可修改密码！";
    }
    }
}
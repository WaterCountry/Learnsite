using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manager_teacheradd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeIsAdmin();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "教师添加页面";
            Texthname.Focus();
        }
    }
    protected void Btnadd_Click(object sender, EventArgs e)
    {
        Teacheradd();
    }

    private void Teacheradd()
    {
        string Hname = Texthname.Text.Trim();
        string Hnick = Texthnick.Text.Trim();
        string Hpwd = Texthpwd.Text.Trim();
        bool Hpermiss = Ckhpermiss.Checked;
        string Hnote = Texthnote.Text.Trim();
        if (Hname != "" && Hpwd != "" && Hnick != "")
        {
            LearnSite.Model.Teacher teacher = new LearnSite.Model.Teacher();
            teacher.Hname = Hname;
            teacher.Hpwd = Hpwd;
            teacher.Hpermiss = Hpermiss;
            teacher.Hnote = Hnote;
            teacher.Hnick = Hnick;
            LearnSite.BLL.Teacher bll = new LearnSite.BLL.Teacher();
            if (bll.ExistsHname(Hname))
            {
                Labelmsg.Text = "账号已经存在，请更换！";
            }
            else
            {
                bll.Add(teacher);
                System.Threading.Thread.Sleep(200);
                Response.Redirect("~/Manager/teacher.aspx", false);
            }
        }
        else
        {
            Labelmsg.Text = "请输入账号、昵称和密码！";
        }
    }
    protected void Btnreturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Manager/teacher.aspx", false);
    }
}

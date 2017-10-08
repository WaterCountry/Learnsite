using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manager_teacheredit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeIsAdmin();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "教师修改页面";
            Texthname.Focus();
            ShowTeacher();
        }
    }
    protected void Btnedit_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Hid"] != null)
        {
            int Hid = Int32.Parse(Request.QueryString["Hid"].ToString());
            string Hname = Texthname.Text.Trim();
            string Hnick = Texthnick.Text.Trim();
            string Hpwd = Texthpwd.Text.Trim();
            bool Hpermiss = Ckhpermiss.Checked;
            string Hnote = Texthnote.Text.Trim();
            if (Hname != "" && Hpwd != "" && Hnote != ""&&Hnick!="")
            {
                LearnSite.Model.Teacher tmodel = new LearnSite.Model.Teacher();
                tmodel.Hid = Hid;
                tmodel.Hname = Hname;
                tmodel.Hpwd = Hpwd;
                tmodel.Hpermiss = Hpermiss;
                tmodel.Hnote = Hnote;
                tmodel.Hnick = Hnick;
                tmodel.Hpath = "";
                LearnSite.BLL.Teacher bll = new LearnSite.BLL.Teacher();
                bll.Update(tmodel);
                System.Threading.Thread.Sleep(200);
                Response.Redirect("~/Manager/teacher.aspx", true);
            }
            else
            {
                Labelmsg.Text = "请输入姓名、密码和备注！";
            }
        }
    }
    protected void Btnreturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Manager/teacher.aspx", false);
    }

    private void ShowTeacher()
    {
        if (Request.QueryString["Hid"] != null)
        {
            int Hid = Int32.Parse(Request.QueryString["Hid"].ToString());
            LearnSite.Model.Teacher teacher = new LearnSite.Model.Teacher();
            LearnSite.BLL.Teacher bll = new LearnSite.BLL.Teacher();
            teacher = bll.GetModel(Hid);
            Texthname.Text = teacher.Hname;
            Texthpwd.Text = teacher.Hpwd;
            Ckhpermiss.Checked = teacher.Hpermiss;
            Texthnote.Text = teacher.Hnote;
            Texthnick.Text = teacher.Hnick;
        }  
    }
}

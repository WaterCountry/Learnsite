using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Profile_mysex : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            if (!IsPostBack)
            {
                SetSex();//如果首次加载，则显示
                CanEdit();
            }            
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }

    private void SetSex()
    {
        ListItem lim = new ListItem();
        lim.Text = "男";
        lim.Value = "男";
        DDLsex.Items.Add(lim);
        ListItem liw = new ListItem();
        liw.Text = "女";
        liw.Value = "女";
        DDLsex.Items.Add(liw);
    }
    protected void Btnsex_Click(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            string mynum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
            string mysex = DDLsex.SelectedValue;
            LearnSite.BLL.Students bll = new LearnSite.BLL.Students();
            bll.UpdateSex(mynum, mysex);
            LearnSite.Common.CookieHelp.EditCookiesItem("StudentCookies", "Sex", mysex);
            System.Threading.Thread.Sleep(500);                
            string msg = "修改性别成功！";
            LearnSite.Common.WordProcess.Alert(msg, this.Page);
            Response.Redirect("~/Profile/mychange.aspx", false);
        }
    }
    private void CanEdit()
    {
        int sclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
        int sgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
        LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
        if (rbll.GetRsexedit(sgrade, sclass))
        {
            Btnsex.Enabled = true;
        }
        else
        {
            Btnsex.Enabled = false;
            Labelstr.Text = "限制修改性别";
        }
    }
}
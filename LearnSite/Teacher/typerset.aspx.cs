using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_typerset : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "中文打字设置";
            Grade();
            ShowSelect();
        }
    }
    private void Grade()
    {
        LearnSite.BLL.Room room = new LearnSite.BLL.Room();
        DDLgrade.DataSource = room.GetGrade();
        DDLgrade.DataTextField = "Rgrade";
        DDLgrade.DataValueField = "Rgrade";
        DDLgrade.DataBind();
    }
    private void ShowSelect()
    {
        string hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();           
        string Rgrade = DDLgrade.SelectedValue;
        if (Rgrade != "")
        {
            LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
            LabelTids.Text = rbll.GetRtyper(Int32.Parse(Rgrade), Int32.Parse(hid));//要放在文章标题获取之前
        }
        LearnSite.BLL.Typer tbll = new LearnSite.BLL.Typer();
        DataListTyper.DataSource = tbll.ShowAllTitle();
        DataListTyper.DataBind();
    }
    protected void DataListTyper_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        CheckBox ck = (CheckBox)e.Item.FindControl("ChkTyper");
        string ttitle = ck.Text;
        int maxlen = 10;
        if (ttitle.Length > maxlen)
        {
            ck.ToolTip = ttitle;
            ck.Text = ttitle.Substring(0, maxlen - 3) + "…";
        }
        Label lb = (Label)e.Item.FindControl("Lbtid");
        string tidstr = lb.Text;
        string tids = LabelTids.Text;
        if (tids != "")
        {
            if (LearnSite.Common.WordProcess.StrExist(tids, tidstr, ','))
            {
                ck.Checked = true;
            }
        }
    }
    protected void BtnSelect_Click(object sender, EventArgs e)
    {
        string Rgrade = DDLgrade.SelectedValue;
        string selectStr = SelectTyper();
        if (Rgrade != "")
        {
            string hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
            LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
            rbll.SetRtyper(Int32.Parse(Rgrade), selectStr, Int32.Parse(hid));
            LearnSite.Common.WordProcess.Alert("设置成功！", this.Page);
        }
    }
    protected void BtnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/teacher/typer.aspx", false);
    }
    private string SelectTyper()
    {
        string cstr = "";
        int na = this.DataListTyper.Items.Count;
        for (int i = 0; i < na; i++)
        {
            CheckBox ck = (CheckBox)this.DataListTyper.Items[i].FindControl("ChkTyper");
            if (ck.Checked)
            {
                string tid = ((Label)this.DataListTyper.Items[i].FindControl("Lbtid")).Text;
                cstr = cstr + tid + ",";
            }
        }
        cstr = cstr.TrimEnd(',');//最除最后一个逗号字母
        return cstr;
    }
    protected void DDLgrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowSelect();
    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_typechineseedit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "拼音词语添加页面";
            if (Request.QueryString["Nid"] != null)
            {
                ShowTyperDetails();
            }
            else
            {
                Response.Redirect("~/Teacher/typechinese.aspx", false);
            }
        }
    }
    private void ShowTyperDetails()
    {
        int Nid = Int32.Parse(Request.QueryString["Nid"].ToString());
        LearnSite.Model.Chinese cmodel = new LearnSite.Model.Chinese();
        LearnSite.BLL.Chinese cbll = new LearnSite.BLL.Chinese();
        cmodel = cbll.GetModel(Nid);
        Ttitle.Text = cmodel.Ntitle;
        Tcontent.Text = cmodel.Ncontent;
    }
    protected void BtnEdit_Click(object sender, EventArgs e)
    {
        int Nid = Int32.Parse(Request.QueryString["Nid"].ToString());
        LearnSite.Model.Chinese cmodel = new LearnSite.Model.Chinese();
        cmodel.Ncontent = Tcontent.Text;
        cmodel.Nid = Nid;
        cmodel.Ntitle = Ttitle.Text.Trim();
        LearnSite.BLL.Chinese cbll = new LearnSite.BLL.Chinese();
        if (Ttitle.Text.Length > 1 && Tcontent.Text.Length > 1)
        {
            cbll.Update(cmodel);
            System.Threading.Thread.Sleep(200);
            string url = "~/Teacher/typechineseshow.aspx?Nid=" + Nid;
            Response.Redirect(url, false);
        }
    }
    protected void Btnreturn_Click(object sender, EventArgs e)
    {
        string url = "~/Teacher/typechinese.aspx";
        Response.Redirect(url, false);
    }
    protected void BtnNoSet_Click(object sender, EventArgs e)
    {
        string mystr = Tcontent.Text;
        string newstr = ClearHtml(mystr);
        Tcontent.Text = newstr;
        Labelmsg.Text = "有" + LearnSite.Common.WordProcess.CheckSpaces(Tcontent.Text) + "个空格无法清除！  当前文章长度：" + newstr.Length.ToString();
    }

    private string ClearHtml(string mystr)
    {
        string cstr = LearnSite.Common.WordProcess.DropHTML(mystr);//清除所有Html格式
        cstr = LearnSite.Common.WordProcess.NoSpaces(cstr); //采用Trim去空格        
        return cstr;
    }
}
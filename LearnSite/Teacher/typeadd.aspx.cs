using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_typeadd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "打字文章添加页面";
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        if (Ttitle.Text != "" && Tcontent.Text != "")
        {
            LearnSite.Model.Typer typer = new LearnSite.Model.Typer();
            int limlen = 210;
            typer.Tcontent = ClearHtml(Tcontent.Text.Trim(), limlen);
            typer.Ttitle = Ttitle.Text.Trim();
            typer.Ttype = 11;
            typer.Tuse = 1;
            LearnSite.BLL.Typer typerbll = new LearnSite.BLL.Typer();
            int Tid = typerbll.Add(typer);
            System.Threading.Thread.Sleep(200);
            string url = "~/Teacher/TypeShow.aspx?Tid=" + Tid;
            Response.Redirect(url, false);
        }
        else
        {
            Labelmsg.Text = "请输入文章标题及内容！";
        }
    }
    protected void Btnreturn_Click(object sender, EventArgs e)
    {
        string url = "~/Teacher/typer.aspx";
        Response.Redirect(url, false);
    }
    protected void BtnNoSet_Click(object sender, EventArgs e)
    {
        string mystr = Tcontent.Text;
        int lim = 210;
        string newstr = ClearHtml(mystr, lim);
        Tcontent.Text = newstr;
        Labelmsg.Text = "系统限制文章长度为" + lim.ToString() + "个汉字：空格有" + LearnSite.Common.WordProcess.CheckSpaces(Tcontent.Text) + "个无法清除！  当前文章长度：" + newstr.Length.ToString();
     }

    private string ClearHtml(string mystr,int lim)
    {
        string cstr = LearnSite.Common.WordProcess.DropHTML(mystr);//清除所有Html格式
        cstr = LearnSite.Common.WordProcess.NoSpaces(cstr); //采用Trim去空格        
        if (cstr.Length > lim)
        {
            cstr = cstr.Substring(0, lim);
        }
        return cstr;
    }
}

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_typeedit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "打字文章编辑页面";
            if (Request.QueryString["Tid"] != null)
            {
                ShowTyperDetails();
            }
            else
            {
                Response.Redirect("~/Teacher/typer.aspx", false);
            }
        }
    }
    protected void BtnEdit_Click(object sender, EventArgs e)
    {
        int Tid = Int32.Parse(Request.QueryString["Tid"].ToString());
        LearnSite.Model.Typer typer = new LearnSite.Model.Typer();
        typer.Tcontent = Tcontent.Text.Trim();
        typer.Tid = Tid;
        typer.Ttitle = Ttitle.Text.Trim();
        typer.Ttype =Int32.Parse( DDLtype.SelectedValue);
        typer.Tuse = Int32.Parse(DDLuse.SelectedValue);
        LearnSite.BLL.Typer typerbll = new LearnSite.BLL.Typer();
        if (typer.Ttitle.Length > 1 && typer.Tcontent.Length > 1)
        {
            typerbll.Update(typer);
            System.Threading.Thread.Sleep(200);
            string url = "~/Teacher/TypeShow.aspx?Tid=" + Tid;
            Response.Redirect(url, false);
        }
        
    }
    protected void BtnNoSet_Click(object sender, EventArgs e)
    {
        TextBox newbox = Tcontent;
        string mystr =LearnSite.Common.WordProcess.DropHTML(newbox.Text);//清除所有Html格式
        mystr = LearnSite.Common.WordProcess.NoSpaces(mystr); //采用Trim去空格
        int lim = 210;
        if (mystr.Length > lim)
        {
            mystr = mystr.Substring(0, lim);
        }
        Tcontent.Text = mystr;
        Labelmsg.Text = "系统限制文章长度为" + lim.ToString() + "个汉字：空格有" + LearnSite.Common.WordProcess.CheckSpaces(Tcontent.Text) + "个无法清除！  当前文章长度：" + mystr.Length.ToString();
    }
    private void ShowTyperDetails()
    {
        int Tid =Int32.Parse( Request.QueryString["Tid"].ToString());
        LearnSite.Model.Typer typer = new LearnSite.Model.Typer();
        LearnSite.BLL.Typer typerbll = new LearnSite.BLL.Typer();
        typer = typerbll.GetModel(Tid);
        DDLtype.SelectedValue = typer.Ttype.ToString();
        DDLuse.SelectedValue = typer.Tuse.ToString();
        Ttitle.Text = typer.Ttitle;
        Tcontent.Text = typer.Tcontent;
    }
    protected void Btnreturn_Click(object sender, EventArgs e)
    {
        string url = "~/Teacher/typer.aspx" ;
        Response.Redirect(url, false);
    }
}

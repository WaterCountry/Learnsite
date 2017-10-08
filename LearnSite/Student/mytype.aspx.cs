using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_mytype : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            LearnSite.Common.CookieHelp.KickStudent();
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            showlimittids();//保证随时更新指定文章
            if (!IsPostBack)
            {
                showalltid();
                if (Request.QueryString["Tid"] != null)
                {
                    showsimple();
                }
                else
                {
                    showrndsimple();
                }
                showtyper();
            }
            else
            {
                System.Threading.Thread.Sleep(500);//重新加载，延时0.5秒，防止乱刷屏，导致数据库锁定
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }
    private void showlimittids()
    {
        int Sgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
        int Sclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
        LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
        Labeltids.Text = rbll.GetRtyperByClass(Sgrade, Sclass);
    }
    private void showalltid()
    {
        LearnSite.BLL.Typer tp = new LearnSite.BLL.Typer();
        DLTid.DataSource = tp.ShowAllTid(Labeltids.Text);
        DLTid.DataBind();
    }
    /// <summary>
    /// 未启用
    /// </summary>
    private void showTidtyper()
    {
        string Tid = LTid.Text;
        if (Tid != "" && LearnSite.Common.WordProcess.IsNum(Tid))
        {
            LearnSite.BLL.Ptyper pt = new LearnSite.BLL.Ptyper();
            GVTyper.DataSource = pt.ShowTypeScore(Int32.Parse(Tid));
            GVTyper.DataBind();
        }
    }
    private void showtyper()
    {
        int Sgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
        int Sclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
        LearnSite.BLL.Ptyper pt = new LearnSite.BLL.Ptyper();
        GVTyper.DataSource = pt.ShowClassTypeScore(Sgrade, Sclass);
        GVTyper.DataBind();

        string Tid = LTid.Text;
        string url = "~/Student/typerclass.aspx?Tid=" + Tid;
        HyperLink2.NavigateUrl = url;
    }
    /// <summary>
    /// 随机打字范文
    /// </summary>
    private void showrndsimple()
    {
        LearnSite.Model.Typer tmodel = new LearnSite.Model.Typer();
        LearnSite.BLL.Typer tp = new LearnSite.BLL.Typer();
        tmodel = tp.GetModelRnd(Labeltids.Text);
        if (tmodel != null)
        {
            LTid.Text = tmodel.Tid.ToString();
            Ttitle.Text = tmodel.Ttitle;
            string str = tmodel.Tcontent;
            int ln = str.Length;
            int lim = 210;
            if (ln > lim)
            {
                Literal1.Text = str.Substring(0, lim);
            }
            else
            {
                Literal1.Text = str;
            }
        }
    }

    /// <summary>
    /// 指定打字范文
    /// </summary>
    private void showsimple()
    {
        string Tid = Request.QueryString["Tid"].ToString();
        if (LearnSite.Common.WordProcess.IsNum(Tid))
        {
            LearnSite.Model.Typer tmodel = new LearnSite.Model.Typer();
            LearnSite.BLL.Typer tp = new LearnSite.BLL.Typer();
            tmodel = tp.GetModel(Int32.Parse(Tid));
           
            LTid.Text = Tid;
            Ttitle.Text = tmodel.Ttitle;
            string str = tmodel.Tcontent;
            int ln = str.Length;
            int lim = 210;
            if (ln > lim)
            {
                Literal1.Text = str.Substring(0, lim); 
            }
            else
            {
                Literal1.Text = str;
            }
        }
    }

    protected void GVTyper_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView theGrid = sender as GridView;  
        int newPageIndex = 0;
        if (-2 == e.NewPageIndex)
        { 
            TextBox txtNewPageIndex = null;

            GridViewRow pagerRow = theGrid.BottomPagerRow;

            if (null != pagerRow)
            {
                txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;   
            }

            if (null != txtNewPageIndex)
            {

                newPageIndex = int.Parse(txtNewPageIndex.Text) - 1; 
            }
        }
        else
        {
            newPageIndex = e.NewPageIndex;
        }
        newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
        newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;
        theGrid.PageIndex = newPageIndex;
        showtyper(); 
    }
    protected void GVTyper_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(GVTyper.PageIndex * GVTyper.PageSize + e.Row.RowIndex + 1);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E1E8E1',this.style.fontWeight='';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#D8E0D8'; this.style.color='buttontext';this.style.cursor='default';");
        }
    }
}

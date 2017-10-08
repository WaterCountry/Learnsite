using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_studentwork : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + " 本学期作品查询";
            Btnclose.Attributes.Add("onclick", "window.opener=null;window.open('','_self'); window.close()");
            ShowWorks();
        }
    }

    private void ShowWorks()
    {
        if (Request.QueryString["Snum"] != null)
        {
            string Snum = Request.QueryString["Snum"].ToString();
            HlkCircle.NavigateUrl = "~/Teacher/stuworkcircle.aspx?Snum=" + Snum;
            LearnSite.Model.Students smodel = new LearnSite.Model.Students();
            LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
            smodel = sbll.SnumGetModel(Snum);
            LabelSnum.Text = Snum;
            LabelSname.Text = smodel.Sname;
            LabelWscore.Text = smodel.Sscore.ToString();
            int wgrade = smodel.Sgrade.Value;
            if (Request.QueryString["Sgrade"] != null)
            {
                wgrade = Int32.Parse(Request.QueryString["Sgrade"].ToString());
            }
            int wterm = Int32.Parse(LearnSite.Common.XmlHelp.GetTerm());
            if (Request.QueryString["Sterm"] != null)
            {
                wterm = Int32.Parse(Request.QueryString["Sterm"].ToString());
            }

            LearnSite.BLL.Works ws = new LearnSite.BLL.Works();
            GridViewworks.DataSource = ws.ShowThisTermWorks(Snum, wgrade, wterm);
            GridViewworks.DataBind();
        }
    }
    protected void GridViewworks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            Label lb = (Label)e.Row.FindControl("LabelWdate");
            if (lb.Text != "")
            {
                DateTime nowdate = DateTime.Now;
                DateTime workdate = DateTime.Parse(lb.Text);
                HyperLink hl = (HyperLink)e.Row.FindControl("HyperLinkWurl");
                HyperLink hlview = (HyperLink)e.Row.FindControl("HyperLinkView");
                string down = hl.ToolTip;
                string view = down + "&True";
                if (LearnSite.Common.CookieHelp.IsTeacher())
                    hlview.NavigateUrl = "~/Plugins/download.aspx?Id=" + LearnSite.Common.EnDeCode.Encrypt(view, "ls");
                else
                    hlview.NavigateUrl = "#";

                if (Request.QueryString["Stat"] == null)
                {
                    if (LearnSite.Common.Computer.Daygone(nowdate, workdate) > 30)
                    {
                        hl.NavigateUrl = "~/Plugins/download.aspx?Id=" + LearnSite.Common.EnDeCode.Encrypt(view, "ls");
                    }
                    else
                    {
                        if (HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname] != null)
                        {
                            hl.Text = "隐藏";
                        }
                    }
                }
                else
                {
                    hl.Text = "隐藏";
                }
               hl.ToolTip = " ";
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //当鼠标放上去的时候 先保存当前行的背景颜色 并给附一颜色 
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E1E8E1',this.style.fontWeight='';");
                //当鼠标离开的时候 将背景颜色还原的以前的颜色 
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                //单击行改变行背景颜色 
                e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#D8E0D8'; this.style.color='buttontext';this.style.cursor='default';");
            }
        }
    }
}
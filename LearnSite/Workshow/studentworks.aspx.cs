using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Workshow_studentworks : System.Web.UI.Page
{
    protected string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Btnclose.Attributes.Add("onclick", "window.opener=null;window.open('','_self'); window.close()");
            if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
            {
                ShowWorks();
            }
        }
    }

    private void ShowWorks()
    {
        if (Request.QueryString["Snum"] != null)
        {
            string Snum = Request.QueryString["Snum"].ToString();
            LearnSite.Model.Students smodel = new LearnSite.Model.Students();
            LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
            smodel = sbll.SnumGetModel(Snum);
            LearnSite.BLL.Works ws = new LearnSite.BLL.Works();
            DataTable dt = ws.ShowMyAllWorks(Snum);
            GridViewworks.DataSource = dt;
            GridViewworks.DataBind();
            int count = dt.Rows.Count;
            LabelSname.Text = smodel.Sname + "同学的作品库(" + count.ToString() + ")";
            this.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + " " + LabelSname.Text;

            if (count > 0)
            {
                msg = "";
                for (int i = 0; i < count; i++)
                {
                    int score = Int32.Parse(dt.Rows[i]["Wscore"].ToString()) + Int32.Parse(dt.Rows[i]["Wscore"].ToString());
                    string scorestr = score.ToString();
                    string jsondata = "[" + i.ToString() + ", " + scorestr + "],";
                    msg += jsondata;
                }
                msg = msg.TrimEnd(',');
            }
        }
    }
    protected void GridViewworks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            HyperLink hl = (HyperLink)e.Row.FindControl("HyperLinkWurl");
            hl.NavigateUrl = "~/Plugins/download.aspx?Id=" + LearnSite.Common.EnDeCode.Encrypt(hl.ToolTip,"ls");
            HyperLink hlview = (HyperLink)e.Row.FindControl("HyperLinkView");
            hlview.NavigateUrl = "~/Plugins/download.aspx?Id=" + LearnSite.Common.EnDeCode.Encrypt(hl.ToolTip+"&True", "ls");

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
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Lessons_thinkshow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
            {
                ShowThink();
            }
        }
    }

    private void ShowThink()
    {
        if (Request.QueryString["Cid"] != null)
        {
            int Fcid = Int32.Parse(Request.QueryString["Cid"].ToString());
            LearnSite.BLL.Flection flection = new LearnSite.BLL.Flection();
            if (flection.ExistsFcid(Fcid))
            {
                Repeater1.DataSource = flection.GetListCid(Fcid);
                Repeater1.DataBind();
                LearnSite.BLL.Courses cs = new LearnSite.BLL.Courses();
                LabelTitle.Text = cs.GetTitle(Fcid);
            }
            else
            {
                string url = "~/Lessons/thinkadd.aspx?Cid=" + Fcid;
                Response.Redirect(url, false);
            }
        }
    }

    protected void BtnEdit_Click(object sender, ImageClickEventArgs e)
    {
        string Cid = Request.QueryString["Cid"].ToString();
        string url = "~/Lessons/thinkedit.aspx?Cid=" + Cid;
        Response.Redirect(url, true);
    }
}

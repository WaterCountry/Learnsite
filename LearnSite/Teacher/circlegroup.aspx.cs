using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_circlegroup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            showGroups();
        }
    }

    private void showGroups()
    {
        if (Request.QueryString["Sg"] != null && Request.QueryString["Sc"] != null)
        {
            int sgrade = Int32.Parse(Request.QueryString["Sg"].ToString());
            int sclass = Int32.Parse(Request.QueryString["Sc"].ToString());

            LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
            DataListGroup.DataSource = sbll.ClassGroup(sgrade, sclass);
            DataListGroup.DataBind();

            int mid = Int32.Parse(Request.QueryString["Mi"].ToString());
            LearnSite.BLL.Mission mbll = new LearnSite.BLL.Mission();
            LabeMtitle.Text = "『 " + mbll.GetMissionTitle(mid) + " 』  " + sgrade.ToString() + "." + sclass.ToString() + "班小组作品展示";
        }
    }
    protected void DataListGroup_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        int sgrade = Int32.Parse(Request.QueryString["Sg"].ToString());
        int sclass = Int32.Parse(Request.QueryString["Sc"].ToString());
        string sid = ((Label)e.Item.FindControl("LabelSid")).Text;
        Label lbgstus = (Label)e.Item.FindControl("LabelGstus");
        DropDownList ddl = (DropDownList)e.Item.FindControl("DDLGscores");
        LinkButton btnview = (LinkButton)e.Item.FindControl("LinkBtnView");
        LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
        lbgstus.Text = sbll.GroupMember(sgrade, sclass, int.Parse(sid));
        string snum = ((Label)e.Item.FindControl("LabelSnum")).Text;
        int mid = Int32.Parse(Request.QueryString["Mi"].ToString());
        LearnSite.BLL.GroupWork gbll = new LearnSite.BLL.GroupWork();
        LearnSite.Model.GroupWork gmodel = new LearnSite.Model.GroupWork();
        gmodel = gbll.GetModelBySnum(snum, mid);
        if (gmodel != null)
        {
            ddl.SelectedValue = gmodel.Gscore.ToString();
            ddl.ToolTip = gmodel.Gid.ToString();
            btnview.CommandArgument = gmodel.Gurl;
        }
        else
        {
            ddl.SelectedValue = "0";
            ddl.Enabled = false;
            btnview.Enabled = false;
        }
    }
    protected void DataListGroup_ItemCommand(object source, DataListCommandEventArgs e)
    {
        LinkButton btnview = (LinkButton)e.Item.FindControl("LinkBtnView");
        Literal lt = (Literal)e.Item.FindControl("LiteralView");
        if (e.CommandName == "V")
        {
            string url = btnview.CommandArgument;
            string ext = LearnSite.Common.WordProcess.getext(url);
            string htmname = "index.htm";
            if (string.IsNullOrEmpty(ext))
            {
                ext = "htm";
            }
            lt.Text = LearnSite.Common.WordProcess.SelectWriteTeaNew(ext, url, true, htmname);
        }
    }
    protected void DDLGscores_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;//取触发的下拉列表框
        string gid = ddl.ToolTip;
        if (!string.IsNullOrEmpty(gid))
        {
            string score = ddl.SelectedValue;
            int sgrade = Int32.Parse(Request.QueryString["Sg"].ToString());
            string term = LearnSite.Common.XmlHelp.GetTerm();
            LearnSite.BLL.GroupWork gbll = new LearnSite.BLL.GroupWork();
            gbll.UpdateGscore(Int32.Parse(gid), Int32.Parse(score), sgrade, Int32.Parse(term));
        }
    }
    protected void LinkBtnxi_Click(object sender, EventArgs e)
    {
        int lcount = DataListGroup.Items.Count;
        if (lcount > 0)
        {
            for (int i = 0; i < lcount; i++)
            {
                Literal lt = (Literal)DataListGroup.Items[i].FindControl("LiteralView");
                lt.Visible = Visible;
            }
        }
    }
    protected void LinkBtnying_Click(object sender, EventArgs e)
    {
        int lcount = DataListGroup.Items.Count;
        if (lcount > 0)
        {
            for (int i = 0; i < lcount; i++)
            {
                Literal lt = (Literal)DataListGroup.Items[i].FindControl("LiteralView");
                lt.Visible = false;
            }
        }
    }
}
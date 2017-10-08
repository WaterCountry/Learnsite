using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_circlegroups : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            showGroups();
            showGroupWork();
        }
    }

    private void showGroups()
    {
        if (Request.QueryString["Sg"] != null && Request.QueryString["Sc"] != null)
        {
            int sgrade = Int32.Parse(Request.QueryString["Sg"].ToString());
            int sclass = Int32.Parse(Request.QueryString["Sc"].ToString());

            LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
            DLgroups.DataSource = sbll.ClassGroup(sgrade, sclass);
            DLgroups.DataBind();
            int mid = Int32.Parse(Request.QueryString["Mi"].ToString());
            LearnSite.BLL.Mission mbll = new LearnSite.BLL.Mission();
            LabeMtitle.Text = "《" + mbll.GetMissionTitle(mid) + "》" + sgrade.ToString() + "." + sclass.ToString() + "班小组作品展示";
        }
    }
    protected void DLgroups_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            int mid = Int32.Parse(Request.QueryString["Mi"].ToString());
            string snum = ((Label)e.Item.FindControl("LabelSnum")).Text;
            LearnSite.BLL.GroupWork gbll = new LearnSite.BLL.GroupWork();
            if (gbll.Exists(snum, mid))
            {
                LinkButton lb = (LinkButton)e.Item.FindControl("LbSgtitle");
                lb.BackColor = System.Drawing.Color.FromArgb(177, 210, 254);
            }
        }
    }
    private void showGroupWork()
    {
        int groupcount = DLgroups.Items.Count;
        int pos = Int32.Parse(Labelpos.Text);
        int lastpos = Int32.Parse(Labellastpos.Text);
        if (groupcount > 0 && pos>-1 && pos < groupcount)
        {
            int sgrade = Int32.Parse(Request.QueryString["Sg"].ToString());
            int sclass = Int32.Parse(Request.QueryString["Sc"].ToString());
            int mid = Int32.Parse(Request.QueryString["Mi"].ToString());
            LinkButton lb = (LinkButton)DLgroups.Items[pos].FindControl("LbSgtitle");
            LabelSgtitle.Text = lb.Text;
            lb.BorderColor = System.Drawing.Color.FromArgb(0, 102, 255);
            if (pos != lastpos)
            {
                LinkButton lblast = (LinkButton)DLgroups.Items[lastpos].FindControl("LbSgtitle");
                lblast.BorderColor = System.Drawing.Color.FromArgb(212, 212, 212);//还原上个边框颜色
            }
            Labellastpos.Text = pos.ToString();
            Label ln = (Label)DLgroups.Items[pos].FindControl("LabelSname");
            LabelLeader.Text = ln.Text;
            string sid = ((Label)DLgroups.Items[pos].FindControl("LabelSid")).Text;
            string snum = ((Label)DLgroups.Items[pos].FindControl("LabelSnum")).Text;
            LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
            Labelmember.Text = sbll.GroupMember(sgrade, sclass, int.Parse(sid));
            LearnSite.BLL.GroupWork gbll = new LearnSite.BLL.GroupWork();
            LearnSite.Model.GroupWork gmodel = new LearnSite.Model.GroupWork();
            gmodel = gbll.GetModelBySnum(snum, mid);
            if (gmodel != null)
            {
                DDLGscores.SelectedValue = gmodel.Gscore.ToString();
                DDLGscores.Enabled = true;
                Labelgid.Text = gmodel.Gid.ToString();
                string url = gmodel.Gurl;
                string ext = LearnSite.Common.WordProcess.getext(url);
                string htmname = "index.htm";
                if (string.IsNullOrEmpty(ext))
                {
                    ext = "htm";
                }
                LiteralView.Text = LearnSite.Common.WordProcess.SelectWriteTeaNew(ext, url, true, htmname);
            }
            else
            {
                DDLGscores.SelectedValue = "0";
                DDLGscores.Enabled = false;
                LiteralView.Text = "";
            }
        }
    }
    protected void DDLGscores_SelectedIndexChanged(object sender, EventArgs e)
    {
        string gid = Labelgid.Text;
        if (!string.IsNullOrEmpty(gid))
        {
            string score = DDLGscores.SelectedValue;
            int sgrade = Int32.Parse(Request.QueryString["Sg"].ToString());
            string term = LearnSite.Common.XmlHelp.GetTerm();
            LearnSite.BLL.GroupWork gbll = new LearnSite.BLL.GroupWork();
            gbll.UpdateGscore(Int32.Parse(gid), Int32.Parse(score), sgrade, Int32.Parse(term));
        }
    }
    protected void ImgBtnright_Click(object sender, ImageClickEventArgs e)
    {
        int groupcount = DLgroups.Items.Count;
        int pos = Int32.Parse(Labelpos.Text);
        pos++;
        if (pos > groupcount - 1)
            pos = 0;
        Labelpos.Text = pos.ToString();
        showGroupWork();
    }
    protected void ImgBtnLeft_Click(object sender, ImageClickEventArgs e)
    {
        int pos = Int32.Parse(Labelpos.Text);
        pos--;
        if (pos < 0)
        {
            int groupcount = DLgroups.Items.Count;
            pos = groupcount - 1;
        }
        Labelpos.Text = pos.ToString();
        showGroupWork();
    }
    protected void ImgBtnrefresh_Click(object sender, ImageClickEventArgs e)
    {
        int groupcount = DLgroups.Items.Count;
        int pos = Int32.Parse(Labelpos.Text);
        pos++;
        if (pos > groupcount - 1)
            pos = 0;
        Labelpos.Text = pos.ToString();
        showGroupWork();
    }
    protected void BtnCicle_Click(object sender, EventArgs e)
    {
        string tip = BtnCicle.Text;
        switch (tip)
        {
            case "播放":
                BtnCicle.Text = "暂停";
                break;
            case "暂停":
                BtnCicle.Text = "播放";
                break;
        }
    }
    protected void DLgroups_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "S")
        {
            Labelpos.Text = e.Item.ItemIndex.ToString();
            BtnCicle.Text = "播放";
            showGroupWork();
        }
    }
}
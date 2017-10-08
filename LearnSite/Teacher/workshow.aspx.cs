using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_workshow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            Btnreturn.Attributes.Add("onclick", "window.opener=null;window.open('','_self'); window.close()");
            if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
            {
                if (Request.QueryString["wGrade"] != null && Request.QueryString["wClass"] != null && Request.QueryString["wCid"] != null)
                {
                    ShowCid();
                    ShowWorks();
                }
            }
        }
    }

    private void ShowCid()
    {
        string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
        Labelshow.Text = Request.QueryString["wGrade"].ToString() + "年级" + Request.QueryString["wClass"].ToString() + "班---作品展示";
        int Sgrade = Int32.Parse(Request.QueryString["wGrade"].ToString());
        int Sclass = Int32.Parse(Request.QueryString["wClass"].ToString());
        string myCid = Request.QueryString["wCid"].ToString();//直接url传递
        string cterm = LearnSite.Common.XmlHelp.GetTerm();

        LearnSite.BLL.Courses cbll = new LearnSite.BLL.Courses();
        DDLCid.DataSource = cbll.ShowCidCtitle(Int32.Parse(Hid), Sgrade, Int32.Parse(cterm));
        DDLCid.DataTextField = "Ctitle";
        DDLCid.DataValueField = "Cid";
        DDLCid.DataBind();
        if (myCid != "")
        {
            DDLCid.SelectedValue = myCid;//设置为自动获取的今天本班学案Cid
        }
        ShowUploadMsort();
    }
    private void ShowUploadMsort()
    {
        string dcid = DDLCid.SelectedValue;
        if (dcid != "")
        {
            LearnSite.BLL.Mission bll = new LearnSite.BLL.Mission();
            DDLmid.DataSource = bll.GetUploadMidMtitle(Int32.Parse(dcid));
            DDLmid.DataTextField = "Mstitle";
            DDLmid.DataValueField = "Mid";
            DDLmid.DataBind();
        }
    }

    private void ShowDoneWorks()
    {
        int Sgrade = Int32.Parse(Request.QueryString["wGrade"].ToString());
        int Sclass = Int32.Parse(Request.QueryString["wClass"].ToString());
        int Wmid = Int32.Parse(DDLmid.SelectedValue);
        string mySort = RBsort.SelectedValue;
        LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
        DataListworks.DataSource = wbll.ShowClassWorksBySort(Sgrade, Sclass,Wmid, mySort);
        DataListworks.DataBind();//Wid,Sname,Wurl,Wvote,Wscore,Qwork,Wcheck
    }
    /// <summary>
    /// 分开是为了不让学案Cid重取
    /// </summary>
    private void ShowWorks()
    {
        string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
        if (Request.QueryString["wGrade"] != null && Request.QueryString["wClass"] != null)
        {
            int Sgrade = Int32.Parse(Request.QueryString["wGrade"].ToString());
            int Sclass = Int32.Parse(Request.QueryString["wClass"].ToString());
            LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
            int Syear = sbll.GetYear(Sgrade, Sclass);
            string myCid = DDLCid.SelectedValue;
            if (myCid != "" && DDLCid.Items.Count > 0)
            {
                if (DDLmid.SelectedValue != "" && DDLmid.Items.Count > 0)
                {
                    int Wmid = Int32.Parse(DDLmid.SelectedValue);
                    LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
                    ShowDoneWorks(); //独立出来方便刷新                   
                    Labelcounts.Text = DataListworks.Items.Count.ToString();

                    DataListNoworks.DataSource = wbll.ShowTodayNotWorks(Syear, Sgrade, Sclass, Wmid);//获取今天本班未提交作品的学生列表
                    DataListNoworks.DataBind();

                    Labelmsg.Text = CalculateScores();
                    LearnSite.BLL.Mission mbll = new LearnSite.BLL.Mission();
                    string Mfiletype = mbll.GetMfiletype(Wmid).ToLower();
                    showGroup();//显示小组作品
                    HLautoplay.Visible = false;
                    if (!string.IsNullOrEmpty(Mfiletype))
                    {
                        ImageType.Visible = true;
                        ImageType.ImageUrl = "~/Images/FileType/" + Mfiletype + ".gif";
                        string urlstr = Sgrade.ToString() + "&Sc=" + Sclass.ToString() + "&Ci=" + myCid + "&Mi=" + Wmid.ToString() + "&Ty=" + Mfiletype;
                        HLautoplay.Visible = true;
                        HLautoplay.NavigateUrl = "~/Teacher/circleshow.aspx?Sg=" + urlstr;
                        HLautoplay.ImageUrl = "~/Images/flashauto.png";
                    }
                }
                else
                {
                    ImageType.Visible = false;
                    HLautoplay.Visible = false;
                    HLgroupplay.Visible = false;
                }
            }
            else
            {
                Labelmsg.Text = "没找到发布的学案和活动！";
                ImageType.Visible = false;
            }
        }
    }
    protected void DataListworks_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        HyperLink hl = new HyperLink();
        hl = (HyperLink)e.Item.FindControl("HyperLink1");
        string Wurl = ((Label)e.Item.FindControl("Labelurl")).Text;
        hl.NavigateUrl = "~/Plugins/download.aspx?Id=" + LearnSite.Common.EnDeCode.Encrypt(Wurl, "ls");

        bool gflash = ((CheckBox)e.Item.FindControl("Checkwflash")).Checked;
        bool gerror = ((CheckBox)e.Item.FindControl("Checkwerror")).Checked;
        string gemotion = ((Label)e.Item.FindControl("Labelwlemotion")).Text;
        if (gemotion == "1")
        {
            hl.BackColor = System.Drawing.Color.DarkSeaGreen;
            hl.ForeColor = System.Drawing.Color.White;
        }
        HyperLink hlf = new HyperLink();
        hlf = (HyperLink)e.Item.FindControl("Hlflash");
        if (gflash)//有转换，显示
        {
            string furl = LearnSite.Common.WordProcess.SwfName(Wurl);
            hlf.NavigateUrl = "~/Plugins/download.aspx?Id=" + LearnSite.Common.EnDeCode.Encrypt(furl + "&True", "ls");
            hlf.Visible = true;
            if (gerror)
            {
                hlf.Enabled = false;
                hlf.ImageUrl = "~/Images/flasherror.png";
                hlf.ToolTip = "文档转换异常！";
            }
        }
        else
        {
            hlf.Visible = false;
        }

        Label ls = new Label();
        ls = (Label)e.Item.FindControl("Labelscore");
        int Wscore = Int32.Parse(ls.Text);
        if (Wscore == 2)
            ((LinkButton)e.Item.FindControl("LE")).BackColor = Labelscore.BackColor;
        if (Wscore == 4)
            ((LinkButton)e.Item.FindControl("LD")).BackColor = Labelscore.BackColor;
        if (Wscore == 6)
            ((LinkButton)e.Item.FindControl("LC")).BackColor = Labelscore.BackColor;
        if (Wscore == 8)
            ((LinkButton)e.Item.FindControl("LB")).BackColor = Labelscore.BackColor;
        if (Wscore == 10)
            ((LinkButton)e.Item.FindControl("LA")).BackColor = Labelscore.BackColor;
        if (Wscore == 12)
            ((LinkButton)e.Item.FindControl("LG")).BackColor = Labelscore.BackColor;
    }
    protected void DataListworks_ItemCommand(object source, DataListCommandEventArgs e)
    {
        int Wscore = 0;
        int Wid = Int32.Parse(DataListworks.DataKeys[e.Item.ItemIndex].ToString());
        LearnSite.BLL.Works ws = new LearnSite.BLL.Works();
        if (e.CommandName == "G")
        {
            Wscore = 12;
        }
        if (e.CommandName == "A")
        {
            Wscore = 10;
        }
        if (e.CommandName == "B")
        {
            Wscore = 8;
        }
        if (e.CommandName == "C")
        {
            Wscore = 6;
        }
        if (e.CommandName == "D")
        {
            Wscore = 4;
        }
        if (e.CommandName == "E")
        {
            Wscore = 2;
        }
        ws.ScoreWork(Wid, Wscore);
        System.Threading.Thread.Sleep(200);
        ShowDoneWorks();
    }
    protected void BtnA_Click(object sender, EventArgs e)
    {
        QuickSetScore("A");
        ShowDoneWorks();
    }
    protected void BtnB_Click(object sender, EventArgs e)
    {
        QuickSetScore("B");
        ShowDoneWorks();
    }
    private void QuickSetScore(string ape)
    {
        if (Request.QueryString["wGrade"] != null && Request.QueryString["wClass"] != null)
        {
            if (DDLCid.SelectedValue != "")
            {
                if (DDLmid.SelectedValue != "")
                {
                    int Sgrade = Int32.Parse(Request.QueryString["wGrade"].ToString());
                    int Sclass = Int32.Parse(Request.QueryString["wClass"].ToString());
                    int Wcid = Int32.Parse(DDLCid.SelectedValue);
                    int Wmid = Int32.Parse(DDLmid.SelectedValue);
                    LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
                    switch (ape)
                    {
                        case "A":
                            wbll.WorkSetScore(Wcid, Sgrade, Sclass, Wmid, 10);//将本班该活动未评作品全评为A
                            break;
                        case "B":
                            wbll.WorkSetScore(Wcid, Sgrade, Sclass, Wmid, 8);//将本班该活动未评作品全评为B
                            break;
                        case "K":
                            wbll.WorkSetScore(Wcid, Sgrade, Sclass, Wmid, 0);//将本班该活动未评作品全评为0
                            break;
                    }
                }
            }
        }
    }

    protected void DDLmid_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowWorks();
    }
    protected void CB_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = sender as CheckBox;
        //取得当前被选中项的索引  
        int index = (chk.NamingContainer as DataListItem).ItemIndex;
        //取得当前选中项中的某个值，最好找ID。
        Label lbl = this.DataListworks.Items[index].FindControl("Labelwid") as Label;
        LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
        wbll.CancleScoreWork(Int32.Parse(lbl.Text), chk.Checked);
        System.Threading.Thread.Sleep(200);
        ShowDoneWorks();
    }
    /// <summary>
    /// 等级分布统计，返回分布值
    /// </summary>
    private string CalculateScores()
    {
        int gc = 0;
        int ac = 0;
        int bc = 0;
        int cc = 0;
        int dc = 0;
        int ec = 0;
        int score = 0;
        foreach (DataListItem item in this.DataListworks.Items)
        {
            string thisscore = ((Label)item.FindControl("Labelscore")).Text;
            if (thisscore != "")
            {
                score = Int32.Parse(thisscore);
                switch (score)
                {
                    case 12:
                        gc++;
                        break;
                    case 10:
                        ac++;
                        break;
                    case 8:
                        bc++;
                        break;
                    case 6:
                        cc++;
                        break;
                    case 4:
                        dc++;
                        break;
                    case 2:
                        ec++;
                        break;
                }
            }
        }
        string rstr = "等级分布：G " + gc.ToString() + " .  A " + ac.ToString() + " .  B " + bc.ToString() + " .  C " + cc.ToString() + " .  D " + dc.ToString() + " .  E " + ec.ToString();
        return rstr;
    }
    protected void DataListgroup_ItemCommand(object source, DataListCommandEventArgs e)
    {
        int Ggrade = Int32.Parse(Request.QueryString["wGrade"].ToString());
        int Cterm = Int32.Parse(LearnSite.Common.XmlHelp.GetTerm());
        int Gscore = 0;
        int Gid = Int32.Parse(DataListgroup.DataKeys[e.Item.ItemIndex].ToString());
        LearnSite.BLL.GroupWork gbll = new LearnSite.BLL.GroupWork();
        string cdme = e.CommandName;
        Gscore = Int32.Parse(cdme);
        gbll.UpdateGscore(Gid, Gscore,Ggrade,Cterm); //评分
        System.Threading.Thread.Sleep(500);
        showGroup();
    }
    protected void DataListgroup_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        HyperLink hl = new HyperLink();
        hl = (HyperLink)e.Item.FindControl("HyperLinkg1");
        string gurl = ((Label)e.Item.FindControl("Labelgurl")).Text;
        hl.NavigateUrl = "~/Plugins/download.aspx?Id=" + LearnSite.Common.EnDeCode.Encrypt(gurl , "ls");
        Label ls = new Label();
        ls = (Label)e.Item.FindControl("Labelgscore");
        int Gscore = Int32.Parse(ls.Text);
        if (Gscore > 2)
        {
            string cn = "L" + ls.Text;
            ((LinkButton)e.Item.FindControl(cn)).BackColor = Labelscore.BackColor;
        }
    }
    private void showGroup()
    {
        int Ggrade = Int32.Parse(Request.QueryString["wGrade"].ToString());
        int Gclass = Int32.Parse(Request.QueryString["wClass"].ToString());

        if (DDLmid.SelectedValue != "")
        {
            string myCid = DDLCid.SelectedValue;
            int Gmid = Int32.Parse(DDLmid.SelectedValue);
            LearnSite.BLL.GroupWork gbll = new LearnSite.BLL.GroupWork();
            DataListgroup.DataSource = gbll.GetMissionGroup(Ggrade, Gclass, Gmid);
            DataListgroup.DataBind();
            if (DataListgroup.Items.Count > 0)
            {
                string urlstr = Ggrade.ToString() + "&Sc=" + Gclass.ToString() + "&Ci=" + myCid + "&Mi=" + Gmid.ToString();
                HLgroupplay.Visible = true;
                HLgroupplay.NavigateUrl = "~/Teacher/circlegroups.aspx?Sg=" + urlstr;
                HLgroupplay.ImageUrl = "~/Images/weboffice.png";
            }
            else
            {
                HLgroupplay.Visible = false;
            }
        }
    }
    protected void DDLCid_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowUploadMsort();
        ShowWorks();
    }
    protected void ImgBtnFlasherror_Click(object sender, ImageClickEventArgs e)
    {
        int Sgrade = Int32.Parse(Request.QueryString["wGrade"].ToString());
        int Sclass = Int32.Parse(Request.QueryString["wClass"].ToString());
        string myCid = DDLCid.SelectedValue;
        if (DDLmid.SelectedValue != "")
        {
            int Wmid = Int32.Parse(DDLmid.SelectedValue);
            LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
            wbll.ClearWflasherror(Sgrade, Sclass, Wmid, Int32.Parse(myCid));
        }
        System.Threading.Thread.Sleep(200);
        ShowDoneWorks();
    }
    protected void CBg_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chg = sender as CheckBox;
        //取得当前被选中项的索引  
        int index = (chg.NamingContainer as DataListItem).ItemIndex;
        //取得当前选中项中的某个值，最好找ID。
        Label lblg = this.DataListgroup.Items[index].FindControl("Labelgid") as Label;
        LearnSite.BLL.GroupWork gbll = new LearnSite.BLL.GroupWork();
        gbll.CancelGscore(Int32.Parse(lblg.Text), chg.Checked);
        System.Threading.Thread.Sleep(200);
        showGroup();
    }
    protected void Btnreflash_Click(object sender, ImageClickEventArgs e)
    {
        ShowWorks();
    }
    protected void RBsort_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowDoneWorks();
    }
    protected void BtnCk_Click(object sender, EventArgs e)
    {
        QuickSetScore("K");
        ShowDoneWorks();
    }
}
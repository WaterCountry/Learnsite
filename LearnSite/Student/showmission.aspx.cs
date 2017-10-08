using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;

public partial class Student_showmission : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            if (Request.QueryString["Mid"] != null && Request.QueryString["Cid"] != null)
            {
                LearnSite.Common.CookieHelp.KickStudent();
                if (!IsPostBack)
                {
                    ShowMission();
                    ShowIpWorkDone();
                }
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }

    private void ShowMission()
    {
        string Mid = Request.QueryString["Mid"].ToString();
        if (LearnSite.Common.WordProcess.IsNum(Mid))
        {
            LearnSite.Model.Mission model = new LearnSite.Model.Mission();
            LearnSite.BLL.Mission mn = new LearnSite.BLL.Mission();

            model = mn.GetModel(Int32.Parse(Mid));
            if (model != null)
            {
                string sSyear = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Syear"].ToString();
                string sSclass = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString();
                string sSnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
                string sLoginIp = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["LoginIp"].ToString();

                string sWcid = model.Mcid.ToString();
                string sWmid = Mid;
                string sWmsort = model.Msort.ToString();
                string sWfiletype = model.Mfiletype;
                LabelMtitle.Text = model.Mtitle;
                LabelMcid.Text = model.Mcid.ToString();
                Mcontent.InnerHtml = HttpUtility.HtmlDecode(model.Mcontent);
                LabelSnum.Text = sSnum;
                LabelMfiletype.Text = sWfiletype;
                bool isupload = model.Mupload;
                CkMupload.Checked = isupload;
                if (isupload)
                    VoteLink.Visible = true;
                else
                    VoteLink.Visible = false;
                CkMgroup.Checked = model.Mgroup;
                LabelMsort.Text = sWmsort;
                LabelMid.Text = Mid;
                upFileTypeGroup.ImageUrl = "~/Images/FileType/" + LabelMfiletype.Text.ToLower() + ".gif";
                upFileUrlGroup.Text = "小组作品";
                switch (sWfiletype)
                {
                    case "doc":
                        LabelUploadType.Text = "*.doc;*.docx";
                        break;
                    case "ppt":
                        LabelUploadType.Text = "*.ppt;*.pptx";
                        break;
                    case "xls":
                        LabelUploadType.Text = "*.xls;*.xlsx";
                        break;
                    case "office":
                        LabelUploadType.Text = "*.doc;*.docx;*.ppt;*.pptx;*.xls;*.xlsx";
                        break;
                    case "sb":
                        LabelUploadType.Text = "*.sb;*.sb2";
                        break;
                    case "flash":
                        LabelUploadType.Text = "*.swf;*.fla";
                        break;
                    default:
                        LabelUploadType.Text = "*." + sWfiletype;
                        break;
                }
                if (model.Mgroup)
                {
                    LearnSite.BLL.GroupWork gbll = new LearnSite.BLL.GroupWork();
                    string gurl = gbll.DoneGroupWorkUrl(sSnum, Int32.Parse(Mid));
                    upFileTypeGroup.Visible = true;
                    upFileUrlGroup.Visible = true;
                    upFileUrlGroup.NavigateUrl = "~/Plugins/download.aspx?Id=" + LearnSite.Common.EnDeCode.Encrypt(gurl, "ls");
                    LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
                    if (sbll.IsLeader(sSnum))//如果是组长则显示小组面板
                    {
                        Panelgroup.Visible = true;
                        showgroupWork();
                        if (gurl != "")
                        {
                            if (gbll.CheckGroupWork(sSnum, Int32.Parse(Mid)))
                            {
                                PanelGroupUp.Visible = false;
                                Labelgroupmsg.Text = "小组作品已经评价！<br/>你不可以再重新提交！";
                            }
                            else
                            {
                                Labelgroupmsg.Text = "小组作品已提交但未评价！<br/>你可以修改后重新提交！";
                            }
                        }
                        else
                        {
                            upFileTypeGroup.Visible = false;
                            upFileUrlGroup.Visible = false;
                        }
                    }
                    else
                    {
                        Panelgroup.Visible = false;
                        upFileTypeGroup.Visible = false;
                    }
                }
                else
                {
                    Panelgroup.Visible = false;
                    upFileTypeGroup.Visible = false;
                }

                if (!CkMupload.Checked)
                {
                    Panelworks.Visible = false;
                }

                ImageType.ImageUrl = "~/Images/FileType/" + LabelMfiletype.Text.ToLower() + ".gif";
            }
            else
            {
                Mcontent.InnerHtml = "此学案活动不存在！";
                Panelworks.Visible = false;
            }
        }

    }
    private bool isTeacher(string Wid, string Snum)
    {
        if (Wid != "" && Snum.StartsWith("s"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ShowIpWorkDone()
    {
        string Sname = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sname"].ToString();
        string Sgrade = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString();
        string Sclass = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString();
        string Snum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
        string Wip = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["LoginIp"].ToString();
        string Wcid = Request.QueryString["Cid"].ToString();
        string Wmid = Request.QueryString["Mid"].ToString();
        LearnSite.BLL.Works ws = new LearnSite.BLL.Works();
        string Wid = ws.WorkDone(Snum, Int32.Parse(Wcid), Int32.Parse(Wmid));//返回空字符表示不存在该记录
        string SnumDone = ws.IpWorkDoneSnum(Int32.Parse(Sgrade), Int32.Parse(Sclass), Int32.Parse(Wcid), Int32.Parse(Wmid), Wip);
        string retureUrl = ws.WorkUrl(Snum, Int32.Parse(Wmid));
        VoteLink.NavigateUrl = "~/Student/myevaluate.aspx?Mid=" + Wmid + "&Cid=" + Wcid;
        if (LearnSite.Common.XmlHelp.GetWorkIpLimit())//判断有无进行IP限制
        {
            if (Snum == SnumDone || isTeacher(Wid, Snum))
            {
                if (retureUrl != "")
                {
                    upFileUrl.Visible = true;
                    upFileType.Visible = true;
                    upFileType.ImageUrl = "~/Images/FileType/" + LabelMfiletype.Text.ToLower() + ".gif";
                    upFileUrl.Text = Server.UrlDecode(Sname);
                    upFileUrl.NavigateUrl = "~/Plugins/download.aspx?Id=" + LearnSite.Common.EnDeCode.Encrypt(retureUrl, "ls");
                }
            }
        }
        else
        {
            if (retureUrl != "")
            {
                upFileUrl.Visible = true;
                upFileType.Visible = true;
                upFileType.ImageUrl = "~/Images/FileType/" + LabelMfiletype.Text.ToLower() + ".gif";
                upFileUrl.Text = Server.UrlDecode(Sname);
                upFileUrl.NavigateUrl = "~/Plugins/download.aspx?Id=" + LearnSite.Common.EnDeCode.Encrypt(retureUrl, "ls");
            }
        }
        if (Wid != "")//判断有无作品提交
        {
            bool ischeck = ws.IsChecked(Int32.Parse(Wid));
            if (ischeck)//判断作品有无评价
            {
                Labelmsg.Text = "该作品已经评分!<br/>你不可以重新提交！";
                Panelswfupload.Visible = false;
            }
            else
            {
                if (LearnSite.Common.XmlHelp.GetWorkIpLimit())//判断有无进行IP限制
                {
                    if (Snum == SnumDone || isTeacher(Wid, Snum))
                    {
                        Labelmsg.Text = "你已经提交该活动作品.！<br/>你可以修改作品后重新提交！";
                        Panelswfupload.Visible = true;
                    }
                    else
                    {
                        Panelswfupload.Visible = false;
                        if (LabelMfiletype.Text != "htm")
                            Labelmsg.Text = SnumDone + "学号<br/>已经在该IP提交本活动作品.！";
                    }
                }
                else
                {
                    Labelmsg.Text = "你已经提交该活动作品.！！<br/>你可以修改作品后重新提交！";
                    Panelswfupload.Visible = true;
                }
            }
        }
        else
        {
            LearnSite.BLL.Mission mbll = new LearnSite.BLL.Mission();
            int minMsort = mbll.GetLastMaxMsort(Int32.Parse(Wcid), Int32.Parse(LabelMsort.Text));//任务活动中查询
            bool isExitFirstWork = ws.ExistsMyFirstWork(Int32.Parse(Wcid), Snum, minMsort);

            if (LearnSite.Common.XmlHelp.GetWorkIpLimit())//判断有无进行IP限制
            {
                if (SnumDone == "")
                {
                    if (isExitFirstWork || minMsort == 0)//如果是上个任务已经提交或是第一个任务，则显示提交按钮
                    {
                        DateTime dt = DateTime.Now;
                        string today = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day;
                        Labelmsg.Text = today;
                        Panelswfupload.Visible = true;
                    }
                    else
                    {
                        Labelmsg.Text = "请先提交前面活动作品！";
                    }
                }
                else
                {
                    Panelswfupload.Visible = false;
                    if (LabelMfiletype.Text != "htm")
                        Labelmsg.Text = SnumDone + "学号<br/>已经在该IP提交本活动作品！";
                }
            }
            else
            {
                if (isExitFirstWork || minMsort == 0)//如果是上个任务已经提交或是第一个任务，则显示提交按钮
                {
                    DateTime dt = DateTime.Now;
                    string today = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day;
                    Labelmsg.Text = today;
                    Panelswfupload.Visible = true;
                }
                else
                {
                    Labelmsg.Text = "请先提交前面的活动作品！";
                }
            }
        }
    }

    protected void GVgwork_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            HyperLink hl = (HyperLink)e.Row.FindControl("HyperLinkWurl");
            hl.NavigateUrl = "~/Plugins/download.aspx?Id=" + LearnSite.Common.EnDeCode.Encrypt(hl.ToolTip,"ls");
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
    protected void GVgwork_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string Wid = e.CommandArgument.ToString();
        int Wlscore = 0;
        if (e.CommandName == "A")
        {
            Wlscore = 10;
            updatelscore(Wid, Wlscore);
        }
        if (e.CommandName == "P")
        {
            Wlscore = 6;
            updatelscore(Wid, Wlscore);
        }
        if (e.CommandName == "E")
        {
            Wlscore = 2;
            updatelscore(Wid, Wlscore);
        }
    }

    private void updatelscore(string Wid, int Wlscore)
    {
        LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
        wbll.Updatelscore(Int32.Parse(Wid), Wlscore);
        System.Threading.Thread.Sleep(500);
        showgroupWork();
    }
    private void showgroupWork()
    {
        string aaSid = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString();
        string aaWmid = Request.QueryString["Mid"].ToString(); ;
        LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
        GVgwork.DataSource = wbll.GetGroupWorks(Int32.Parse(aaSid), Int32.Parse(aaWmid));
        GVgwork.DataBind();
    }
    }
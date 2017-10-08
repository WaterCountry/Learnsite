using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_downwork : System.Web.UI.Page
{
    public string WorkSnum = "";
    public string MyFeedback = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            LearnSite.Common.CookieHelp.KickStudent();
            if (!IsPostBack)
            {
                ShowWork();
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }

    private void ShowSelf(int Fwid, string Fnum)
    {
        LearnSite.BLL.GaugeFeedback kbll = new LearnSite.BLL.GaugeFeedback();
        string fselect = kbll.GetMyFeedback(Fwid, Fnum);
        if (fselect.EndsWith(","))
        {
            fselect = fselect.Substring(0, fselect.Length - 1);//去最后的逗号
        }
        //Response.Write("|"+fselect+"|");
        if (!string.IsNullOrEmpty(fselect))
        {
            LearnSite.BLL.GaugeItem gbll = new LearnSite.BLL.GaugeItem();
            DataListSelf.DataSource = gbll.GetMySelfMitems(fselect);
            DataListSelf.DataBind();
        }
    }
    private void ShowWork()
    {
        string Studentnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
        string StudentIp = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["LoginIp"].ToString();
        string Studentgrade = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString();
        string Studentterm = LearnSite.Common.XmlHelp.GetTerm();
        string Wfiletype, Wurl;
        string Wid = Request.QueryString["Wid"].ToString();
        if (LearnSite.Common.WordProcess.IsNum(Wid))
        {
            LearnSite.Model.Works wmodel = new LearnSite.Model.Works();
            LearnSite.BLL.Works ws = new LearnSite.BLL.Works();
            wmodel = ws.GetModel(Int32.Parse(Wid));
            string workfilename = wmodel.Wfilename;
            WorkSnum = wmodel.Wnum;
            Wurl = wmodel.Wurl;
            Wfiletype = wmodel.Wtype;
            int worklength = wmodel.Wlength.Value;
            decimal kblen = worklength / 1024;
            LbWscore.Text = wmodel.Wscore.ToString();
            LbWfscore.Text = wmodel.Wfscore.ToString();
            LabelWdate.Text = wmodel.Wdate.ToString();
            LbWself.Text = HttpUtility.HtmlDecode(wmodel.Wself);
            LbWdscore.Text = wmodel.Wdscore.ToString();
            bool Wflash = wmodel.Wflash.Value;
            DateTime workdate = wmodel.Wdate.Value;
            TimeSpan ts = DateTime.Now - workdate;
            int days = Int32.Parse(LearnSite.Common.XmlHelp.GetWorkDowntime());//获取作品查看天数

            string wgrade = wmodel.Wgrade.ToString();
            string wterm = wmodel.Wterm.ToString();
            string Wip = wmodel.Wip;
            ShowSelf(Int32.Parse(Wid), WorkSnum);
            ShowFeedback(Int32.Parse(Wid));
            ShowGauge(wmodel.Wmid.Value);
            LearnSite.BLL.Students stbll = new LearnSite.BLL.Students();
            string mySname = stbll.GetSnameBySnum(WorkSnum);
            if (mySname != "")
            {
                HLfile.Text = mySname + "." + Wfiletype;
            }
            else
            {
                HLfile.Text = "模拟学生" + WorkSnum + "." + Wfiletype;
            }
            Labelsize.Text = kblen.ToString("N2") + "kb";
            Labelgood.Text = "推荐" + LearnSite.Common.WordProcess.StrCountNew(MyFeedback, "T,").ToString() + "次";
            Labelwid.Text = Wid;
            Labeltype.Text = Wfiletype;
            Labelwurl.Text = Wurl;
            if (isOffice(Wfiletype))
            {
                if (ws.ExistsMyMissonWork(wmodel.Wmid.Value, Studentnum))
                    Buttonpreview.Visible = true;//是office文档则显示预览查看按钮，以防止因为控件下载而卡页面
                else
                    Buttonpreview.Visible = false;//如果未交作品的，则无法查看
            }
            else
            {
                Buttonpreview.Visible = false;//不是office文档则直接预览
                Literal1.Text = LearnSite.Common.WordProcess.SelectEvaluateShow(Wid, Wfiletype, Wurl, false);
            }
            ImageType.ImageUrl = "~/Images/FileType/" + Wfiletype.ToLower() + ".gif";
            if (ts.Days < days)
            {
                int waitdays = days - ts.Days;

                if (LearnSite.Common.XmlHelp.GetWorkIpLimit())///如果作品提交IP限制
                {
                    if (Studentnum == WorkSnum && StudentIp == Wip)
                        HLfile.Visible = true;
                    else
                    {
                        HLfile.Visible = false;
                        Labelmsg.Text = waitdays.ToString() + "天后可下载";
                    }
                }
                else
                {
                    if (Studentnum == WorkSnum)
                    {
                        HLfile.Visible = true;
                    }
                    else
                    {
                        HLfile.Visible = false;//否则 IP没限制或IP不同则限制几天后下载
                        Labelmsg.Text = waitdays.ToString() + "天后可下载";
                    }
                }
            }
            else
            {
                if (Studentgrade == wgrade && Studentterm == wterm)
                {
                    if (Studentnum == WorkSnum)
                    {
                        HLfile.Visible = true;//本学期的作品自己可见，别人不可下载
                    }
                    else
                    {
                        HLfile.Visible = false;//如果是本学期的作品，则无法下载，呵呵
                        Labelmsg.Text = "隐藏下载";
                        Labelmsg.ToolTip = "该作品在版权保护期内，暂时无法下载.";
                    }
                }
                else
                {
                    HLfile.Visible = true;//超过，都可下载
                }
            }

            HLfile.NavigateUrl = "~/Plugins/download.aspx?Id=" + LearnSite.Common.EnDeCode.Encrypt(Wurl,"ls");
            LearnSite.BLL.Mission mbll = new LearnSite.BLL.Mission();
            Labelmission.Text = mbll.GetMissionTitle(wmodel.Wmid.Value);
        }
    }
    private bool isOffice(string filetype)
    {
        bool isok = false;
        switch (filetype)
        {
            case "doc":
            case "docx":
            case "xls":
            case "xlsx":
            case "ppt":
            case "pptx":
            case "wps":
            case "wpp":
            case "et":
                isok = true;
                break;
        }
        return isok;
    }
    private void ShowGauge(int Wmid)
    {
        LearnSite.BLL.GaugeItem gbll = new LearnSite.BLL.GaugeItem();
        DataListGauge.DataSource = gbll.GetListMitems(Wmid);
        DataListGauge.DataBind();
    }
    private void ShowFeedback(int Wid)
    {
        LearnSite.BLL.GaugeFeedback fbll = new LearnSite.BLL.GaugeFeedback();
        MyFeedback = fbll.GetWorkFeedback(Wid);
    }
    protected void DataListGauge_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        string myMid = ((Label)e.Item.FindControl("LabelMid")).Text;
        if (MyFeedback != "")
        {
            string pattern = myMid + ",";
            int ncount = LearnSite.Common.WordProcess.StrCountNew(MyFeedback, pattern);
            if (ncount > 0)
                ((Label)e.Item.FindControl("LabelCount")).Text = ncount.ToString();
        }
    }
    protected void Buttonpreview_Click(object sender, EventArgs e)
    {
        int MySgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
        int MySclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());

        LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
        bool israuge = rbll.GetRgauge(MySgrade, MySclass);

        string Wid = Labelwid.Text;
        string Wfiletype = Labeltype.Text;
        string Wurl = Labelwurl.Text;
        if (Wid != "" && Wfiletype != "" && Wurl != "" && israuge)

            Literal1.Text = LearnSite.Common.WordProcess.SelectEvaluateShow(Wid, Wfiletype, Wurl, false);

        Buttonpreview.Visible = false;
    }
}

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_myevaluate : System.Web.UI.Page
{

    protected bool isUpload = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Mid"] != null && Request.QueryString["Cid"] != null)
        {
            if (LearnSite.Common.CookieHelp.IsStudentLogin())
            {
                LearnSite.Common.CookieHelp.KickStudent();

                if (!IsPostBack)
                {
                    ShowInfo();
                }
            }
            else
            {
                LearnSite.Common.CookieHelp.JudgeStudentCookies();
            }
        }
        else
        {
            Labelmsg.Text = "Mid和Cid传递参数为空值！";
        }
    }
    /// <summary>
    /// 是不是上周
    /// </summary>
    /// <returns></returns>
    private bool IsLastWeek()
    {
        bool isok = false;
        string days = Labelwdate.Text;
        if (!string.IsNullOrEmpty(days))
        {
            if (Int32.Parse(days) > 6)
            {
                return true;//大于6天返回真
            }
        }
        return isok;
    }

    protected void DataListvote_ItemCommand(object source, DataListCommandEventArgs e)
    {
        int dlcount = DataListvote.Items.Count;
        if (e.CommandName == "S")
        {
            string Wurl = e.CommandArgument.ToString();
            if (Wurl != "")
            {
                bool israuge = showRgaugeSet();
                string ext = LearnSite.Common.WordProcess.getext(Wurl);
                LinkButton lkbtn = (LinkButton)e.Item.FindControl("lBtnSname");
                lkbtn.BackColor = System.Drawing.Color.LightBlue;
                lkbtn.Font.Bold = true;
                Labelwname.Text = lkbtn.Text;
                string Wid = ((Label)e.Item.FindControl("LabelWid")).Text;
                if (israuge)
                    Literal1.Text = LearnSite.Common.WordProcess.SelectEvaluateShow(Wid, ext, Wurl, true);

                BtnVote.CommandArgument = e.Item.ItemIndex.ToString();

                ShowGauge(Wid);
                foreach (DataListItem item in this.DataListGauge.Items)
                {
                    CheckBox ck = (CheckBox)item.FindControl("RbMitem");
                    if (ck.Checked)
                    {
                        ck.Checked = false;
                    }
                }
                CheckBoxGood.Checked = false;
                Labelmsg.Text = "";

                if (israuge)
                {
                    BtnVote.Enabled = true;
                }
                else
                {
                    Labelmsg.Text = "当前作品互评暂停！";
                    BtnVote.Enabled = false;
                }
            }
            if (e.Item.ItemIndex == dlcount - 1)
            {
                ListWorks();
            }
        }
    }

    private bool showRgaugeSet()
    {
        int MySgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
        int MySclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());

        LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
        return rbll.GetRgauge(MySgrade, MySclass);
    }

    private void ShowFeedback(int Wid)
    {
        LearnSite.BLL.GaugeFeedback fbll = new LearnSite.BLL.GaugeFeedback();
        lbMyFeedback.Text = fbll.GetWorkFeedback(Wid);
    }
    protected void BtnVote_Click(object sender, EventArgs e)
    {
        string indexstr = BtnVote.CommandArgument;
        if (!string.IsNullOrEmpty(indexstr))
        {
            if (Request.QueryString["Mid"] != null)
            {
                if (DataListGauge.Items.Count > 0)
                {
                    SelectItems myselect = new SelectItems();
                    myselect.FindCheck(DataListGauge);
                    if (myselect.CheckCount > 0)
                    {
                        string myWmid = Request.QueryString["Mid"].ToString();
                        string myCid = Request.QueryString["Cid"].ToString();
                        string Wid = ((Label)DataListvote.Items[Int32.Parse(indexstr)].FindControl("LabelWid")).Text;
                        int Widn = Int32.Parse(Wid);
                        string Wflash = ((Label)DataListvote.Items[Int32.Parse(indexstr)].FindControl("LabelWflash")).Text;

                        LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
                        LearnSite.BLL.GaugeFeedback kbll = new LearnSite.BLL.GaugeFeedback();
                        string Wname = ((LinkButton)DataListvote.Items[Int32.Parse(indexstr)].FindControl("lBtnSname")).Text;
                        int MySid = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString());
                        string MySnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
                        string Mygrade = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString();
                        string Myclass = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString();

                        if (!kbll.ExistsFnum(Widn, MySid)) //检测是否给该同学投过票，如果未投过则。。。
                        {
                            int iv = wbll.GetWegg(Int32.Parse(myWmid), MySid);//从数据库中获取可投次数
                            if (iv > 0)
                            {
                                LearnSite.BLL.Works ws = new LearnSite.BLL.Works();
                                ws.UpdateWvote(Widn);//别人的得票数加1
                                iv = iv - 1;//我的投票次数减1
                                ws.UpdateWegg(Int32.Parse(myWmid), MySnum);

                                LearnSite.Model.GaugeFeedback kmodel = new LearnSite.Model.GaugeFeedback();
                                kmodel.Fcid = Int32.Parse(myCid);
                                kmodel.Fclass = Int32.Parse(Myclass);
                                kmodel.Fdate = DateTime.Now;
                                kmodel.Fgid = Int32.Parse(LabelMgid.Text);
                                kmodel.Fgood = CheckBoxGood.Checked;
                                kmodel.Fgrade = Int32.Parse(Mygrade);
                                kmodel.Fmid = 0;//任务编号暂时为0，无用参数
                                kmodel.Fnum = MySnum;
                                kmodel.Fscore = myselect.CheckScore;
                                kmodel.Fselect = myselect.CheckSelect;
                                kmodel.Fwid = Widn;
                                kmodel.Fsid = MySid;
                                kbll.Add(kmodel);//添加互评记录
                                Labelmsg.Text = "给" + Wname + "同学投票互评成功！";
                                System.Threading.Thread.Sleep(200);
                                int Wfscore = kbll.AvgFwid(Widn);
                                ws.UpdateWfscore(Widn, Wfscore);
                                System.Threading.Thread.Sleep(200);
                                Labelegg.Text = iv.ToString();
                                Labelwfscore.Text = ws.GetWfscore(Int32.Parse(Wid)).ToString();
                            }
                            else
                            {
                                Labelmsg.Text = "您的投票次数已经用完！";
                            }
                        }
                        else
                        {
                            Labelmsg.Text = "你已经给" + Wname + "同学投过票了！";
                        }
                    }
                    else
                    {
                        Labelmsg.Text = "未选中互评选项，无法给与投票！";
                    }
                }
                else
                {
                    Labelmsg.Text = "没有互评选项，请咨询老师给该活动添加量规!";
                }
            }
        }
        else
        {
            Labelmsg.Text = "请点击作品，预览后再进行投票！";
        }
    }

    private void ShowGauge(string Wid)
    {
        ShowFeedback(Int32.Parse(Wid));
        LearnSite.BLL.GaugeItem gbll = new LearnSite.BLL.GaugeItem();
        string myMgid = LabelMgid.Text;
        if (string.IsNullOrEmpty(myMgid) || myMgid == "0")
        {
            //当活动中未指定互评评价标准时，自动选取相应作品类型中的第一条评价标准
            DataListGauge.DataSource = gbll.GetListAutoGtype(LabelWtype.Text);
            DataListGauge.DataBind();
        }
        else
        {
            DataListGauge.DataSource = gbll.GetListMgid(LabelMgid.Text);
            DataListGauge.DataBind();
        }
    }

    private void ListWorks()
    {
        string Mid = Request.QueryString["Mid"].ToString();
        int MySgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
        int MySclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
        int MySgroup = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgroup"].ToString());
        //int MySyear = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Syear"].ToString());
        LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
        DataListvote.DataSource = wbll.ShowMissionWorksGroup(MySgrade, MySclass, Int32.Parse(Mid), MySgroup); //改回为显示作品
        DataListvote.DataBind();
    }
    /// <summary>
    /// 显示信息
    /// </summary>
    private void ShowInfo()
    {
        string Studentnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();

        int MySgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
        int MySclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
        int MySyear = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Syear"].ToString());
        int MySgroup = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgroup"].ToString());
        if (MySgroup == 0)
            Labelscope.Text = "全班";
        else
            Labelscope.Text = "组内";
        string Mid = Request.QueryString["Mid"].ToString();
        if (LearnSite.Common.WordProcess.IsNum(Mid))
        {
            LearnSite.Model.Works wmodel = new LearnSite.Model.Works();
            LearnSite.BLL.Works ws = new LearnSite.BLL.Works();
            LearnSite.BLL.Mission mbll = new LearnSite.BLL.Mission();
            LearnSite.Model.Mission mModel = new LearnSite.Model.Mission();
            mModel = mbll.GetModel(Int32.Parse(Mid));
            ImageWtype.ImageUrl = "~/Images/FileType/" + mModel.Mfiletype + ".gif";
            Labelmtitle.Text = "〖" + mModel.Mtitle + "〗";
            LabelMgid.Text = mModel.Mgid.ToString();
            Labelwmid.Text = Mid;
            LabelWtype.Text = mModel.Mfiletype;
            wmodel = ws.GetModelByStu(Int32.Parse(Mid), Studentnum);
            if (wmodel != null)
            {
                DataListvote.DataSource = ws.ShowMissionWorksGroup(MySgrade, MySclass, Int32.Parse(Mid), MySgroup);
                DataListvote.DataBind();
                Labelhow.Text = DataListvote.Items.Count.ToString();
                Labelme.Text = wmodel.Wvote.ToString();
                string workIp = wmodel.Wip;
                string worknum = wmodel.Wnum;
                DateTime workdate = wmodel.Wdate.Value;
                DateTime nowdate = DateTime.Now;
                bool worklimit = LearnSite.Common.XmlHelp.GetWorkIpLimit();
                Labelegg.Text = wmodel.Wegg.ToString();
                Labelwfscore.Text = wmodel.Wfscore.ToString();

                TimeSpan ts = nowdate - workdate;
                int lastday = ts.Days;
                Labelwdate.Text = lastday.ToString();///取短日期
                if (lastday < 30)
                {
                    if (showRgaugeSet())
                    {
                        LimitVote();//限制为可预览的投票
                    }
                    else
                    {
                        Labelmsg.Text = "当前作品互评暂停！";
                        BtnVote.Enabled = false;
                    }
                }
                else
                {
                    Labelmsg.Text = "一个月前的作品不能再投票了！";
                    BtnVote.Enabled = false;
                }
            }
            else
            {
                if (Studentnum.StartsWith("s"))
                {
                    DataListvote.DataSource = ws.ShowMissionWorksGroup(MySgrade, MySclass, Int32.Parse(Mid), MySgroup);
                    DataListvote.DataBind();
                    Labelhow.Text = DataListvote.Items.Count.ToString();
                    BtnVote.Enabled = true;
                }
                else
                {
                    Labelmsg.Text = "您未提交作品，无法互评！";
                    BtnVote.Enabled = false;
                }

            }
        }
    }

    private void LimitVote()
    {
        string filetype = LabelWtype.Text;
        switch (filetype)
        {
            case "txt":
            case "swf":
            case "doc":
            case "ppt":
            case "xls":
            case "docx":
            case "pptx":
            case "xlsx":
            case "wps":
            case "wpp":
            case "et":
            case "mm":
            case "sb":
            case "psd":
            case "jpg":
            case "png":
            case "bmp":
            case "gif":
                BtnVote.Enabled = true;//此类型可以投票
                break;
            default:
                BtnVote.Enabled = false;
                break;
        }
    }
    private bool LimitOfficeVote(bool Wflash, string filetype)
    {
        bool isOk = false;
        switch (filetype)
        {
            case "doc":
            case "ppt":
            case "xls":
            case "docx":
            case "pptx":
            case "xlsx":
            case "wps":
            case "wpp":
            case "et":
                isOk = Wflash;//如果是文档类型则返回是否转换成功的标志
                break;
            default:
                isOk = true;//否则都返回真
                break;
        }
        return isOk;
    }
    public class SelectItems
    {
        public SelectItems()
        { }
        private int _checkcount = 0;
        private int _checkscore = 0;
        private string _checkselect = "";

        public int CheckCount
        {
            set { _checkcount = value; }
            get { return _checkcount; }
        }

        public int CheckScore
        {
            set { _checkscore = value; }
            get { return _checkscore; }
        }

        public string CheckSelect
        {
            set { _checkselect = value; }
            get { return _checkselect; }
        }
        public void FindCheck(DataList dl)
        {
            foreach (DataListItem item in dl.Items)
            {
                CheckBox ck = (CheckBox)item.FindControl("RbMitem");
                if (ck.Checked)
                {
                    _checkcount++;
                    Label lbmc = (Label)item.FindControl("LbMscore");
                    Label lbmid = (Label)item.FindControl("LbMid");
                    _checkscore = _checkscore + Int32.Parse(lbmc.Text);
                    _checkselect = _checkselect + lbmid.Text + ",";
                }
            }
        }
    }
    protected void DataListGauge_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        CheckBox ck = (CheckBox)e.Item.FindControl("RbMitem");
        ck.Attributes.Add("OnClick", "return check()");

        string myMid = ((Label)e.Item.FindControl("LbMid")).Text;
        if (lbMyFeedback.Text != "")
        {
            string pattern = myMid + ",";
            int ncount = LearnSite.Common.WordProcess.StrCountNew(lbMyFeedback.Text, pattern);
            if (ncount > 0)
                ((Label)e.Item.FindControl("LabelCount")).Text = ncount.ToString();
        }
    }
    protected void DataListvote_Nothing(object sender, DataListItemEventArgs e)
    {
        //已无效代码
        if (e.Item.ItemIndex > -1)
        {
            string mySname = Server.UrlDecode(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sname"].ToString());
            string mySnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
            LinkButton lbtn = (LinkButton)e.Item.FindControl("lBtnSname");
            string Wnum = ((Label)e.Item.FindControl("LabelWnum")).Text;
            int itm = e.Item.ItemIndex + 1;
            if (itm < 10)
            {
                lbtn.Text = "作品0" + itm.ToString();
            }
            else
            {
                lbtn.Text = "作品" + itm.ToString();
            }
            if (mySnum == Wnum)
                lbtn.Text = mySname;
        }
    }
}
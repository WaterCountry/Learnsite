using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_mysurvey : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            LearnSite.Common.CookieHelp.KickStudent();
            Btnshow.Attributes.Add("onclick", "this.form.target='_blank'");

            if (!IsPostBack)
            {
                showSurvey();
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }

    private void showSurvey()
    {
        if (Request.QueryString["Vid"] != null && Request.QueryString["Cid"] != null)
        {
            int vid =Int32.Parse( Request.QueryString["Vid"].ToString());
            int cid=Int32.Parse(Request.QueryString["Cid"].ToString());
            string fnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();

            LearnSite.Model.Survey vmodel = new LearnSite.Model.Survey();
            LearnSite.BLL.Survey vbll = new LearnSite.BLL.Survey();
            vmodel = vbll.GetModel(vid);
            Lbtitle.Text = vmodel.Vtitle;
            vcontent.InnerHtml = HttpUtility.HtmlDecode(vmodel.Vcontent);
            int vtype = vmodel.Vtype.Value;
            Lbtype.Text = vtype.ToString();
            if (vtype > 0)
            {
                Lbtypecn.Text = "测验";
            }
            else
            {
                Lbtypecn.Text = "调查";
            }
            Lbsname.Text = Server.UrlDecode(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sname"].ToString());
            Lbsnum.Text = fnum;
            bool isClose = vmodel.Vclose;
            if (isClose)
            {
                Btnclock.ImageUrl = "~/Images/clockred.gif" + "?temp=" + DateTime.Now.Millisecond.ToString();
                Btnclock.ToolTip = "暂停，请咨询老师！";
            }
            else
            {
                Btnclock.ImageUrl = "~/Images/clock.gif" + "?temp=" + DateTime.Now.Millisecond.ToString();
                Btnclock.ToolTip = "开启，请开始回答！";
                showQuestion();
            }
            int myscore = GetMyScore(vid, fnum);
            if (myscore != -1024)
            {
                //如果已经回答过调查
                Btnok.Visible = false;
                Lbcheck.Text = "已完成！";
                Lbcheck.BackColor = System.Drawing.Color.Green;
                Lbfscore.Text = myscore.ToString();
                int syear = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Syear"].ToString());
                int sgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
                int sclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
            
                LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
                int cns = sbll.CountClassMate(sgrade, sclass);
                LearnSite.BLL.SurveyFeedback fbll = new LearnSite.BLL.SurveyFeedback();
                int dcn = fbll.GetSurveyStu(sgrade, sclass, cid, vid);
                if (dcn == cns)
                    Btnshow.Visible = true;
            }
            else
            {
                Lbfscore.Text = "0";
                if (isClose)
                {
                    Btnok.Visible = false;//如果关闭调查，则提交按钮无效
                }
                else
                {
                    Btnok.Visible = true;
                    if (Session[fnum + "survey" + vid] != null)
                    {
                        DateTime oldtime = DateTime.Parse(Session[fnum + "survey" + vid].ToString());
                        DateTime nowtime = DateTime.Now;
                        Lbtime.Text = LearnSite.Common.Computer.DatagoneMinute(oldtime, nowtime);
                    }
                    else
                    {
                        Session[fnum + "survey" + vid] = DateTime.Now.ToString();
                    }
                }
                Lbcheck.Text = "未完成！";
                Lbcheck.BackColor = System.Drawing.Color.Red;
                Btnshow.Visible = false;
            }
            if (fnum.IndexOf('s') > -1)
            {
                Btnshow.Visible = true;
                Btnok.Visible = false;
                Btnclock.Enabled = true;
            }
            else
            {
                Btnclock.Enabled = false;
            }
        }
    }

    private int GetMyScore(int vid, string fnum)
    {
        LearnSite.BLL.SurveyFeedback fbll = new LearnSite.BLL.SurveyFeedback();
        return fbll.ExistsScore(vid, fnum);
    }
    private void showQuestion()
    {
        if (Request.QueryString["Vid"] != null && Request.QueryString["Cid"] != null)
        {
            string qvid = Request.QueryString["Vid"].ToString();
            LearnSite.BLL.SurveyQuestion qbll = new LearnSite.BLL.SurveyQuestion();
            DataListonly.DataSource = qbll.GetListByQvid(Int32.Parse(qvid));
            DataListonly.DataBind();
        }
    }
    protected void DataListonly_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        string qid = DataListonly.DataKeys[e.Item.ItemIndex].ToString();
        if (!string.IsNullOrEmpty(qid))
        {
            RadioButtonList rbl = (RadioButtonList)e.Item.FindControl("RBLselect");
            LearnSite.BLL.SurveyItem mbll = new LearnSite.BLL.SurveyItem();
            rbl.DataSource = mbll.GetListItemHashtable(Int32.Parse(qid));
            rbl.DataTextField = "Key";
            rbl.DataValueField = "Value";
            rbl.DataBind();
        }
    }
    protected void Btnok_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Vid"] != null && Request.QueryString["Cid"] != null)
        {
            string fnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
            int vid = Int32.Parse(Request.QueryString["Vid"].ToString());
            int limitTime = Int32.Parse(LbLimitTime.Text);
            if (GetMyScore(vid, fnum) == -1024)
            {
                int qcount = DataListonly.Items.Count;//考题数
                if (qcount > 0)
                {
                    //如果有题目则分析
                    int scount = 0;//做题数
                    string midselect = "";
                    foreach (DataListItem item in this.DataListonly.Items)
                    {
                        RadioButtonList rblm = (RadioButtonList)item.FindControl("RBLselect");
                        if (rblm.SelectedIndex > -1)
                        {
                            scount++;
                            midselect = midselect + rblm.SelectedValue + ",";
                        }
                    }

                    if (qcount == scount)
                    {
                        if (midselect.EndsWith(","))
                            midselect = midselect.Substring(0, midselect.Length - 1);
                        int syear = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Syear"].ToString());
                        int sgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
                        int sclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
                        int cid = Int32.Parse(Request.QueryString["Cid"].ToString());
                        int sid = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString());

                        LearnSite.Model.SurveyFeedback fmodel = new LearnSite.Model.SurveyFeedback();
                        fmodel.Fnum = fnum;
                        fmodel.Fyear = syear;
                        fmodel.Fgrade = sgrade;
                        fmodel.Fclass = sclass;
                        fmodel.Fterm = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["ThisTerm"].ToString());
                        fmodel.Fcid = cid;
                        fmodel.Fvid = vid;
                        fmodel.Fvtype = Int32.Parse(Lbtype.Text);
                        fmodel.Fselect = midselect;
                        LearnSite.BLL.SurveyItem mbll = new LearnSite.BLL.SurveyItem();

                        int tt = Int32.Parse(Lbtime.Text) / 60;
                        int myscore = mbll.GetItemScore(midselect);
                        if (tt > limitTime)
                        {
                            if (tt - limitTime > scount)
                                fmodel.Fscore = 0;//限制一下扣分，最低分为0
                            else
                                fmodel.Fscore = myscore + limitTime - tt;
                        }
                        else
                        {
                            fmodel.Fscore = myscore;
                        }
                        fmodel.Fdate = DateTime.Now;
                        fmodel.Fsid = sid;
                        LearnSite.BLL.SurveyFeedback fbll = new LearnSite.BLL.SurveyFeedback();
                        if (fbll.Add(fmodel) > 0)
                        {
                            showSurvey();
                        }
                    }
                    else
                    {
                        string msg = "您还有题目未做好，请仔细查看！";
                        LearnSite.Common.WordProcess.Alert(msg, this.Page);
                    }
                }
            }
            else
            {
                Btnok.Enabled = false;
            }
        }
    }

    protected void Btnshow_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Vid"] != null && Request.QueryString["Cid"] != null)
        {
            int syear = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Syear"].ToString());
            int sgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
            int sclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
            int vid = Int32.Parse(Request.QueryString["Vid"].ToString());
            int cid = Int32.Parse(Request.QueryString["Cid"].ToString());
            string fnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
            string rnd = LearnSite.Common.WordProcess.RndStrnum(6);
            Session[fnum + "surveyrnd"] = rnd;
            string ty="12";
            string url = "mysurveyclass.aspx?Vid=" + vid + "&Cid=" + cid + "&Syear=" + syear + "&Sgrade=" + sgrade + "&Sclass=" + sclass + "&Rnd=" + rnd+"&Type="+ty ;
            Response.Redirect(url, true);
        }
    }
    protected void Btnclock_Click(object sender, ImageClickEventArgs e)
    {
        string fnum = Server.UrlDecode(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString());
        if (fnum.IndexOf('s') > -1)
        {
            int vid = Int32.Parse(Request.QueryString["Vid"].ToString());
            LearnSite.BLL.Survey vbll = new LearnSite.BLL.Survey();
            vbll.UpdateVclose(vid);
            showSurvey();
        }
    }
}
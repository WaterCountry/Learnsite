using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Student_mysurveyclass : System.Web.UI.Page
{
    public string itemandcount;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
                Btnclose.Attributes.Add("onclick", "window.opener=null;window.open('','_self'); window.close()");
                Btnclose.Text = "关闭";
            if (!IsPostBack)
            {
                btnselect();
                if (!IsRight())
                {
                    string url = "mysurvey.aspx";
                    LearnSite.Common.WordProcess.AlertJump("现在将跳转到调查页面，请完成调查后按查看结果打开！", url, this.Page);
                }
                else
                {
                    showSurvey(); //显示班级调查统计
                }
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }

    private void btnselect()
    {
        if (Request.QueryString["Type"] != null)
        {
            HLreturn.Visible = false;
        }
        else
        {
            Btnclose.Visible = false;
            if (Request.QueryString["Vid"] != null && Request.QueryString["Cid"] != null)
            {
                int vid = Int32.Parse(Request.QueryString["Vid"].ToString());
                int cid = Int32.Parse(Request.QueryString["Cid"].ToString());
                string url = "~/Student/mysurvey.aspx?Cid="+cid+"&Vid="+vid;
                HLreturn.NavigateUrl = url;
            }
        }
    }

    private bool IsRight()
    {
        bool bl = false;
        string fnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
        if (Session[fnum + "surveyrnd"] != null && Request.QueryString["Rnd"] != null)
        {
            string surveyrnd = Session[fnum + "surveyrnd"].ToString();
            string rnd = Request.QueryString["Rnd"].ToString();
            if (surveyrnd == rnd)
            {
                bl = true;
                //测试用，判断是不是从正确考试入口进入的
            }
        }
        return bl;
    }

    private void showSurvey()
    {
        if (Request.QueryString["Vid"] != null && Request.QueryString["Cid"] != null)
        {
            int vid = Int32.Parse(Request.QueryString["Vid"].ToString());
            int syear = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Syear"].ToString());
            int sgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
            int sclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
            int sterm = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["ThisTerm"].ToString());
            int cid = Int32.Parse(Request.QueryString["Cid"].ToString());
            LearnSite.Model.Survey vmodel = new LearnSite.Model.Survey();
            LearnSite.BLL.Survey vbll = new LearnSite.BLL.Survey();
            vmodel = vbll.GetModel(vid);
            int vtype = vmodel.Vtype.Value;
            Lbtitle.Text = "《" + vmodel.Vtitle + "》班级统计分析";
            vcontent.InnerHtml = HttpUtility.HtmlDecode(vmodel.Vcontent);
            Lbdate.Text = vmodel.Vdate.ToString();
            Lbsgrade.Text = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString();
            string fnum = Server.UrlDecode(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString());
            Lbsclass.Text = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString();
            bool isClose = vmodel.Vclose;
            if (!isClose)
            {
                surveyClass();//更新统计
            }
            else
            {
                itemandcount = surveyClassView(syear, sgrade, sclass, sterm, cid, vid);//历史统计显示
            }
            if (fnum.IndexOf('s') > -1)
            {
                Btnrefresh.Visible = true;//限制为模拟学生可以进行统计（方便老师，减少数据库查询）
            }
            else
            {
                Btnrefresh.Visible = false;
            }
            showQuestion();//显示问题
            LearnSite.BLL.SurveyFeedback fbll = new LearnSite.BLL.SurveyFeedback();
            Lbstus.Text = fbll.GetSurveyStu(sgrade, sclass, cid, vid).ToString() + "人";
            stuList.InnerHtml = "未参与同学列表：<br />" + fbll.GetNoSurveyStu(sgrade, sclass, cid, vid);
            HLclassrank.NavigateUrl = "mysurveyrank.aspx?Vid=" + vid;
            HLclassrank.Visible = true;            

        }
    }

    private void showQuestion()
    {
        if (Request.QueryString["Vid"] != null)
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
            LearnSite.BLL.SurveyItem mbll = new LearnSite.BLL.SurveyItem();
            GridView gv = (GridView)e.Item.FindControl("GridView1");
            DataTable ddt = mbll.GetListByMqid(Int32.Parse(qid)).Tables[0];
            gv.DataSource = DeelTable(ddt);
            gv.DataBind();
        }
    }
    protected void Btnrefresh_Click(object sender, EventArgs e)
    {
        if (!CBclose.Checked)
        {
            showSurvey();
        }
    }
    private void surveyClass()
    {
        if (Request.QueryString["Vid"] != null && Request.QueryString["Cid"] != null)
        {
            int syear = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Syear"].ToString());
            int sgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
            int sclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
            int sterm = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["ThisTerm"].ToString());
            int vid = Int32.Parse(Request.QueryString["Vid"].ToString());
            int cid = Int32.Parse(Request.QueryString["Cid"].ToString());
            LearnSite.BLL.SurveyFeedback fbll = new LearnSite.BLL.SurveyFeedback();
            string allselect = fbll.GetClassFselect(sgrade, sclass, vid);
            if (!string.IsNullOrEmpty(allselect))
            {
                LearnSite.BLL.SurveyItem mbll = new LearnSite.BLL.SurveyItem();
                itemandcount = mbll.GetListItemAndCount(vid, allselect);
                // LearnSite.Common.WordProcess.Alert(itemandcount, this.Page);//调试信息

                string[] itemcountStr = itemandcount.Split('|');
                LearnSite.Model.SurveyClass ymodel = new LearnSite.Model.SurveyClass();
                ymodel.Ycid = cid;
                ymodel.Yclass = sclass;
                ymodel.Ycount = itemcountStr[1].ToString();
                ymodel.Ydate = DateTime.Now;
                ymodel.Ygrade = sgrade;
                int yscore = fbll.GetClassYscore(syear, sgrade, sclass, sterm, vid);
                ymodel.Yscore = yscore;
                Lbscore.Text = yscore.ToString();
                ymodel.Yselect = itemcountStr[0].ToString();
                ymodel.Yterm = sterm;
                ymodel.Yvid = vid;
                ymodel.Yyear = syear;
                LearnSite.BLL.SurveyClass ybll = new LearnSite.BLL.SurveyClass();
                int yid = ybll.ExistsClass(syear, sgrade, sclass, sterm, vid);
                if (yid > 0)
                {
                    //更新
                    ymodel.Yid = yid;
                    ybll.UpdateClass(ymodel);
                }
                else
                {
                    ybll.Add(ymodel);
                }
            }
        }
    }
    private string surveyClassView(int Yyear, int Ygrade, int Yclass, int Yterm, int Ycid, int Yvid)
    {
        string str = "";
        LearnSite.BLL.SurveyClass Ycbll = new LearnSite.BLL.SurveyClass();
        LearnSite.Model.SurveyClass ycmodel = new LearnSite.Model.SurveyClass();
        ycmodel = Ycbll.GetModelByClass(Yyear, Ygrade, Yclass, Yterm, Ycid, Yvid);
        if (ycmodel != null)
        {
            str = ycmodel.Yselect.ToString() + "|" + ycmodel.Ycount.ToString();
            Lbscore.Text = ycmodel.Yscore.ToString();
        }
        return str;
    }
    private string Mcount(string Mid)
    {
        if (!string.IsNullOrEmpty(itemandcount))
        {
            string[] mcstr = itemandcount.Split('|');
            string[] mstr = mcstr[0].Split(',');
            string[] cstr = mcstr[1].Split(',');
            int ln = mstr.Length;
            string count = "";
            for (int i = 0; i < ln; i++)
            {
                if (mstr[i] == Mid)
                {
                    count = cstr[i];
                    break;
                }
            }
            return count;
        }
        else
        {
            return "";
        }
    }

    /// <summary>
    /// 处理内存表
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    private DataTable DeelTable(DataTable dt)
    {
        int dc = dt.Rows.Count;
        dt.Columns.Add("Mnum");
        dt.Columns.Add("Mhit");
        dt.Columns.Add("Mper");
        if (dc > 0)
        {
            int allhit = 0;
            for (int i = 0; i < dc; i++)
            {
                string mid = dt.Rows[i]["Mid"].ToString();
                dt.Rows[i]["Mnum"] = i + 1;
                string hit = Mcount(mid);
                dt.Rows[i]["Mhit"] = hit;
                if (hit != "")
                    allhit = allhit + Int32.Parse(hit);
            }
            if (allhit > 0)
            {
                for (int k = 0; k < dc; k++)
                {
                    if (dt.Rows[k]["Mhit"] != null && dt.Rows[k]["Mhit"].ToString() != "")
                    {
                        int myhit = Int32.Parse(dt.Rows[k]["Mhit"].ToString());
                        dt.Rows[k]["Mper"] = (myhit * 100 / allhit).ToString() + "%";
                    }
                }
            }
        }
        return dt;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (Request.QueryString["Vid"] != null)
        {
            string qvid = Request.QueryString["Vid"].ToString();
            if (e.Row.RowIndex > -1)
            {
                HyperLink hl = (HyperLink)e.Row.FindControl("HyperLink1");
                string jsstr = "mate('" + hl.ToolTip + "','"+qvid+"');";
                hl.Attributes.Add("onclick", jsstr);
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
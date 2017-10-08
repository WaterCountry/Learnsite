using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Survey_surveyquestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "调查题添加页面";
        if (Request.QueryString["Vid"] != null && Request.QueryString["Cid"] != null)
        {
            if (!IsPostBack)
            {
                showQuestion();
            }
        }
        else
        {
            Btnadd.Enabled = false;        
        }
    }
    protected string myCid()
    {
        if (Request.QueryString["Cid"] != null)
        {
            return Request.QueryString["Cid"].ToString();
        }
        else
        {
            return "";
        }
    }
    private void showQuestion()
    {
        if (Request.QueryString["Qid"] != null)
        {
            string qid = Request.QueryString["Qid"].ToString();
            LearnSite.Model.SurveyQuestion qmodel = new LearnSite.Model.SurveyQuestion();
            LearnSite.BLL.SurveyQuestion qbll = new LearnSite.BLL.SurveyQuestion();
            qmodel = qbll.GetModel(Int32.Parse(qid));
            mcontent.InnerText = HttpUtility.HtmlDecode(qmodel.Qtitle);
            Btnadd.Text = "修改试题";
        }
    }

    protected void Btnadd_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Vid"] != null && Request.QueryString["Cid"] != null)
        {
            string fckstr = LearnSite.Common.WordProcess.ClearPP(mcontent.InnerText);
            if (fckstr.Length > 2)
            {
                int cid =Int32.Parse( Request.QueryString["Cid"].ToString());
                int vid = Int32.Parse(Request.QueryString["Vid"].ToString());
                LearnSite.Model.SurveyQuestion qmodel = new LearnSite.Model.SurveyQuestion();
                qmodel.Qcid = cid;
                qmodel.Qcount = 0;
                string Qtitle = HttpUtility.HtmlEncode(fckstr);
                qmodel.Qtitle = Qtitle;
                qmodel.Qvid = vid;
                LearnSite.BLL.SurveyQuestion qbll = new LearnSite.BLL.SurveyQuestion();
                if (Request.QueryString["Qid"] != null)
                {
                    int Qid = Int32.Parse(Request.QueryString["Qid"].ToString());
                    qbll.UpdateQtitle(Qid, Qtitle);
                    System.Threading.Thread.Sleep(200);
                    string url = "~/Survey/survey.aspx?Cid=" + cid + "&Vid=" + vid;
                    Response.Redirect(url, true);
                }
                else
                {
                    qbll.Add(qmodel);
                    System.Threading.Thread.Sleep(200);
                    string url = "~/Survey/survey.aspx?Cid=" + cid + "&Vid=" + vid;
                    Response.Redirect(url, true);
                }
            }
            else
            {
                Labelmsg.Text = "调查试题文字太少！";
            }
        }
    }
    protected void BtnSurvey_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Vid"] != null && Request.QueryString["Cid"] != null)
        {
            string cid = Request.QueryString["Cid"].ToString();
            string vid = Request.QueryString["Vid"].ToString();
            string url = "~/Survey/survey.aspx?Cid=" + cid + "&Vid=" + vid;
            Response.Redirect(url, true);
        }
    }
}
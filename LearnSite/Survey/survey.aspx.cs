using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Survey_survey : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "调查显示页面";
        
            if (!IsPostBack)
            {
                showsurvey();
                showquestion();
                showCalculate();

                if (Request.QueryString["Cold"] != null)
                {
                    BtnEdit.Enabled = false;
                    Btnadd.Enabled = false;
                }
            }
    }
    
    private void showCalculate()
    {
        if (Request.QueryString["Vid"] != null)
        {
            int vid = Int32.Parse(Request.QueryString["Vid"].ToString());
            LearnSite.BLL.Survey vbll = new LearnSite.BLL.Survey();
            vbll.Updatevtotal(vid);// 统计调查卷下的试题数
            vbll.Updatevscore(vid);// 统计调查卷下的试题总分
            
        }
    }
    private void showsurvey()
    {
        if (Request.QueryString["Vid"] != null)
        {
            string vid = Request.QueryString["Vid"].ToString();
            LearnSite.Model.Survey vmodel = new LearnSite.Model.Survey();
            LearnSite.BLL.Survey vbll = new LearnSite.BLL.Survey();
            vmodel = vbll.GetModel(Int32.Parse(vid));
            Lbave.Text = vmodel.Vaverage.ToString();
            bool isClose =  vmodel.Vclose;
            if (isClose)
            {
                Btnclock.ImageUrl = "~/Images/clockred.gif" + "?temp=" + DateTime.Now.Millisecond.ToString();
                Btnclock.ToolTip = "调查暂停！";
            }
            else
            {
                Btnclock.ImageUrl = "~/Images/clock.gif" + "?temp=" + DateTime.Now.Millisecond.ToString();
                Btnclock.ToolTip = "调查开启！";
            }
            Lbdate.Text = vmodel.Vdate.ToString();
            Lbscore.Text = vmodel.Vscore.ToString();
            Lbtitle.Text = vmodel.Vtitle;
            if (vmodel.Vtype.Value == 0)
                Lbtype.Text = "调查";
            else
                Lbtype.Text = "测验";
            vcontent.InnerHtml = HttpUtility.HtmlDecode(vmodel.Vcontent);
        }
    }

    private void showquestion()
    {
        if (Request.QueryString["Vid"] != null)
        {
            string vid = Request.QueryString["Vid"].ToString();
            LearnSite.BLL.SurveyQuestion qbll = new LearnSite.BLL.SurveyQuestion();
            GVQuestion.DataSource = qbll.GetListByQvid(Int32.Parse(vid));
            GVQuestion.DataBind();
        }
    }
    protected void Btnadd_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Vid"] != null && Request.QueryString["Cid"] != null)
        {
            string cid = Request.QueryString["Cid"].ToString();
            string vid = Request.QueryString["Vid"].ToString();
            string url = "~/Survey/SurveyQuestion.aspx?Cid=" + cid + "&Vid=" + vid;
            Response.Redirect(url, false);
        }
    }
    protected void GVQuestion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            string Qid = e.CommandArgument.ToString();
            LearnSite.BLL.SurveyItem mbll = new LearnSite.BLL.SurveyItem();
            mbll.DelAllMqid(Int32.Parse(Qid));
            LearnSite.BLL.SurveyQuestion qbll = new LearnSite.BLL.SurveyQuestion();
            qbll.Delete(Int32.Parse(Qid));
            System.Threading.Thread.Sleep(200);
            showquestion();
            showCalculate();
            showsurvey();
        }
    }
    protected void GVQuestion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
            string strjs = "if(confirm('您确定要删除该调查试题吗？删除试题的同时将同时删除该试题所有选项！'))return true;else return false; ";
            ((LinkButton)e.Row.FindControl("BtnDel")).OnClientClick = strjs;

            int Qid =Int32.Parse( GVQuestion.DataKeys[e.Row.RowIndex].Value.ToString());
            LearnSite.BLL.SurveyItem mbll = new LearnSite.BLL.SurveyItem();
            Label lb = (Label)(e.Row.FindControl("Labelcount"));
            lb.Text ="("+ mbll.GetItemCount(Qid).ToString()+")";
        }
    }
    protected void Btnreturn_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Cid"] != null)
        {
            string cid = Request.QueryString["Cid"].ToString();
            string url = "~/Teacher/CourseShow.aspx?Cid=" + cid;
            Response.Redirect(url, false);
        }
    }
    protected void BtnEdit_Click(object sender, ImageClickEventArgs e)
    {
        if (Request.QueryString["Vid"] != null && Request.QueryString["Cid"] != null)
        {
            string cid = Request.QueryString["Cid"].ToString();
            string vid = Request.QueryString["Vid"].ToString();
            string url = "~/Survey/surveyadd.aspx?Vid=" + vid + "&Cid=" + cid ;
            Response.Redirect(url, true);
        }
    }
    protected void Btnclock_Click(object sender, ImageClickEventArgs e)
    {
        if (Request.QueryString["Vid"] != null )
        {
            int vid = Int32.Parse(Request.QueryString["Vid"].ToString());
            LearnSite.BLL.Survey vbll = new LearnSite.BLL.Survey();
            vbll.UpdateVclose(vid);
            System.Threading.Thread.Sleep(500);
            showsurvey();
        }
    }
}
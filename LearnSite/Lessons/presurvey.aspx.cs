using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Lessons_presurvey : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.IsTeacherCookies();
        if (!IsPostBack)
        {
            showSurvey();
        }
    }

    private void showSurvey()
    {
        if (Request.QueryString["Vid"] != null && Request.QueryString["Cid"] != null)
        {
            string vid = Request.QueryString["Vid"].ToString();

            LearnSite.Model.Survey vmodel = new LearnSite.Model.Survey();
            LearnSite.BLL.Survey vbll = new LearnSite.BLL.Survey();
            vmodel = vbll.GetModel(Int32.Parse(vid));
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
            showQuestion();
            bool isClose = vmodel.Vclose;
            if (isClose)
            {
                Btnclock.ImageUrl = "~/Images/clockred.gif" + "?temp=" + DateTime.Now.Millisecond.ToString();
                Btnclock.ToolTip = "调查已经关闭，请咨询老师！";
            }
            else
            {
                Btnclock.ImageUrl = "~/Images/clock.gif" + "?temp=" + DateTime.Now.Millisecond.ToString();
                Btnclock.ToolTip = "调查开启，请开始回答！";
            }
        }
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
}
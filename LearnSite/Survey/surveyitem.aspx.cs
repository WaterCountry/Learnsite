using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Survey_surveyitem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "调查题选项添加页面";
        if (!IsPostBack)
        {
            showquestion();//显示调查题目
            showitem();//显示该题所有选项
        }
    }
    private void showquestion()
    {
        if (Request.QueryString["Qid"] != null)
        {
            string Mqid = Request.QueryString["Qid"].ToString();
            LearnSite.BLL.SurveyQuestion qbll = new LearnSite.BLL.SurveyQuestion();
            LabelQtitle.Text=HttpUtility.HtmlDecode(qbll.GetTitle(Int32.Parse(Mqid)));
        }
    }
    private void showitem()
    {
        if (Request.QueryString["Qid"] != null)
        {
            string Mqid=Request.QueryString["Qid"].ToString();
            LearnSite.BLL.SurveyItem mbll = new LearnSite.BLL.SurveyItem();
            GVSurveyItem.DataSource = mbll.GetListByMqid(Int32.Parse(Mqid));
            GVSurveyItem.DataBind();
        }
    }
    protected void Btnadd_Click(object sender, EventArgs e)
    {
        string Mitem = LearnSite.Common.WordProcess.ClearPP(mcontent.InnerText);
        if (Request.QueryString["Qid"] != null && Request.QueryString["Qvid"] != null && Mitem.Length > 0)
        {
            string Mqid = Request.QueryString["Qid"].ToString();
            string Mvid = Request.QueryString["Qvid"].ToString();
            string Mcid = Request.QueryString["Qcid"].ToString();
            LearnSite.Model.SurveyItem model = new LearnSite.Model.SurveyItem();
            model.Mcount = 0;
            model.Mitem = HttpUtility.HtmlEncode( Mitem);
            model.Mqid = Int32.Parse(Mqid);
            model.Mscore = Int32.Parse(DDLscore.SelectedValue);
            model.Mvid = Int32.Parse(Mvid);
            model.Mcid = Int32.Parse(Mcid);
            LearnSite.BLL.SurveyItem mbll = new LearnSite.BLL.SurveyItem();
            if (Btnadd.Text == "添加")
            {
                mbll.Add(model);
            }
            else
            {
                model.Mid = Int32.Parse(LabelMid.Text);
                mbll.Update(model);
                Btnadd.Text = "添加";
                LabelMid.Text = "";
            }
            System.Threading.Thread.Sleep(200);
            showitem();
            mcontent.InnerText = "";
        }
    }
    protected void Btnreturn_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Qvid"] != null && Request.QueryString["Qcid"] != null)
        {
            string vid = Request.QueryString["Qvid"].ToString();
            string cid = Request.QueryString["Qcid"].ToString();
            string url = "~/Survey/survey.aspx?Cid=" + cid + "&Vid=" + vid;
            Response.Redirect(url, true);
        }
    }
    protected void GVSurveyItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);            
            string strjs = "if(confirm('您确定要删除该选项吗?'))return true;else return false; ";
            ((LinkButton)e.Row.FindControl("BtnDel")).OnClientClick = strjs;
        }
    }
    protected void GVSurveyItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string Mid = e.CommandArgument.ToString();
        LearnSite.BLL.SurveyItem mbll = new LearnSite.BLL.SurveyItem();

        if (e.CommandName == "Del")
        {
            mbll.Delete(Int32.Parse(Mid));
        }
        if (e.CommandName == "Edt")
        {
            LabelMid.Text = Mid;
            LearnSite.Model.SurveyItem model = new LearnSite.Model.SurveyItem();
            model = mbll.GetModel(Int32.Parse(Mid));
            mcontent.InnerText = HttpUtility.HtmlDecode(model.Mitem);//暂时从数据库获取选项内容，不会直接从表格中获取
            DDLscore.SelectedValue = model.Mscore.Value.ToString();
            Btnadd.Text = "修改";
        }
        System.Threading.Thread.Sleep(200);
        showitem();
    }

    protected string myCid()
    {
        return Request.QueryString["Qcid"].ToString();
    }

  
}
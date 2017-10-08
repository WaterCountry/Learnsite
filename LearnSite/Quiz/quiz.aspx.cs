using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Quiz_quiz : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "试题管理页面";
        if (!IsPostBack)
        {           
            if (Session["Qtype"] != null)
                DDLqtype.SelectedIndex = Int32.Parse(Session["Qtype"].ToString());
            ShowClass();
            ListQuiz();
            ShowQuizXml();
        }
    }

    private void ShowClass()
    {
        DDLclass.DataSource = LearnSite.Common.TypeNameList.QuizCourseType();
        DDLclass.DataBind();
        if (Session["classtype"] != null)
            DDLclass.SelectedValue = Session["classtype"].ToString();
    }
    private void ListQuiz()
    {
        int Qtype = DDLqtype.SelectedIndex;
        string Qclass = DDLclass.SelectedValue;
        LearnSite.BLL.Quiz bll = new LearnSite.BLL.Quiz();
        if (Qclass == "全部显示")
        {
            GVQuiz.DataSource = bll.GetListByQtype(Qtype);
        }
        else
        {
            GVQuiz.DataSource = bll.GetListByQtypeQclass(Qtype, Qclass);
        }
        if (Qtype == 2)
            GVQuiz.PageSize = 20;
        else
            GVQuiz.PageSize = 10;
        GVQuiz.AllowPaging = true;
        if (Session["pageindex"] != null)
        {
            GVQuiz.PageIndex = int.Parse(Session["pageindex"].ToString());
        }
        GVQuiz.DataBind();
        if (GVQuiz.Rows.Count == 0)
        {
            Btnexport.Visible = false;
        }
    }
    protected void GVQuiz_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView theGrid = sender as GridView;  // refer to the GridView
        int newPageIndex = 0;

        if (-2 == e.NewPageIndex)
        { // when click the "GO" Button
            TextBox txtNewPageIndex = null;

            GridViewRow pagerRow = theGrid.BottomPagerRow;

            if (null != pagerRow)
            {
                txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;   // refer to the TextBox with the NewPageIndex value
            }

            if (null != txtNewPageIndex)
            {

                newPageIndex = int.Parse(txtNewPageIndex.Text) - 1; // get the NewPageIndex
            }
        }
        else
        {  // when click the first, last, previous and next Button
            newPageIndex = e.NewPageIndex;
        }

        // check to prevent form the NewPageIndex out of the range
        newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
        newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;
        theGrid.PageIndex = newPageIndex;
        Session["pageindex"] = theGrid.PageIndex;
        ListQuiz();
        GVQuiz.DataBind();
    }
    protected void GVQuiz_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(GVQuiz.PageIndex * GVQuiz.PageSize + e.Row.RowIndex + 1);
            LinkButton lbtn = e.Row.Cells[8].Controls[0] as LinkButton;
            lbtn.Attributes.Add("onclick", "return   confirm('您确定要删除该试题吗？');");
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
    protected void Btnadd_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Quiz/quizadd.aspx", false);
    }
    protected void Btnimport_Click(object sender, EventArgs e)
    {
        if (FileUploadquiz.HasFile)
        {
            Labelmsg.Text = LearnSite.Store.Quizbag.XmltoQuiz(FileUploadquiz);
            System.Threading.Thread.Sleep(1000);
            ListQuiz();
        }
    }
    protected void GVQuiz_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int Qid = Convert.ToInt32(((GridView)sender).DataKeys[index].Values["Qid"].ToString());
            LearnSite.BLL.Quiz bll = new LearnSite.BLL.Quiz();
            bll.Delete(Qid);//删除本试题
            System.Threading.Thread.Sleep(1000);
            ListQuiz();           
        }        
         
    }
    protected void Btnlist_Click(object sender, EventArgs e)
    {
        Session["pageindex"] = null;
        ListQuiz();
        Session["Qtype"] = DDLqtype.SelectedIndex.ToString();
        Labelmsg.Text = "";
        if (Session["classtype"] != null)
            Session["classtype"] = null;
    }
    protected void Btnexport_Click(object sender, EventArgs e)
    {
        LearnSite.Store.Quizbag.QuizXml();
        Btnexport.Enabled = false;
        ShowQuizXml();
    }
    private void ShowQuizXml()
    {
        HlkQuizxml.NavigateUrl = LearnSite.Store.Quizbag.Xmlurl();
        if (HlkQuizxml.NavigateUrl == "")
            HlkQuizxml.Visible = false;
        else
        {
            HlkQuizxml.Visible = true;
        }
    }
    protected void Btngradeset_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Quiz/quizselect.aspx", false);
    }
}

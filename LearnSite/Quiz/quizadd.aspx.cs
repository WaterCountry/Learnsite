using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Quiz_quizadd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "试题添加页面"; 
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            if (!IsPostBack)
            {
                DDLclass.DataSource = LearnSite.Common.TypeNameList.CourseType();
                DDLclass.DataBind();
            }
        }
    }
    protected void Btnadd_Click(object sender, EventArgs e)
    {
        string Qestion = Request.Form["textareaItem"].Trim();
        if (Qestion != "")
        {
            int qtype = DDLqtype.SelectedIndex;  //0为单选题，1为多选题，2为判断题
            int qscore = Int32.Parse(DDLqscore.SelectedValue);
            string qanalyze = TextBoxqanalyze.Text;
            string qanswer = GetQanswer();

            LearnSite.Model.Quiz model = new LearnSite.Model.Quiz();
            model.Qanalyze = HttpUtility.HtmlEncode(qanalyze);//分析编码
            model.Qanswer = qanswer;
            model.Qscore = qscore;
            model.Qtype = qtype;
            model.Question = HttpUtility.HtmlEncode(Qestion);//题目编码
            model.Qclass = DDLclass.SelectedValue;
            model.Qselect = false;
            LearnSite.BLL.Quiz bll = new LearnSite.BLL.Quiz();
            bll.Add(model);
            Labelmsg.Text = "添加试题成功！";
            TextBoxqanalyze.Text = "";
            //ClearKindeditor();
        }
        else
        {
            Labelmsg.Text = "请先添加试题！";
        }
    }
    private void ClearKindeditor()
    {
        System.Text.StringBuilder scriptstr = new System.Text.StringBuilder();
        scriptstr.Append("<script>");
        scriptstr.Append(" editor.html(''); ");
        scriptstr.Append("</script>");
        if (!Page.ClientScript.IsStartupScriptRegistered(this.GetType(), "clearcontent"))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "clearcontent", scriptstr.ToString());
        }
    }
    private string GetQanswer()
    {
        string qanswer = "";
        int qtype = DDLqtype.SelectedIndex;  //0为单选题，1为多选题，2为判断题
        if (qtype == 0)
        {
            qanswer = RBLselect.SelectedValue;//对单选题的值
        }
        if (qtype == 1)     
        {
            foreach (ListItem li in CBLselect.Items)
            {
                if (li.Selected == true)
                {
                    qanswer += li.Value;//遍历取选中值 （多选题）
                }
            }            
        }
        if (qtype == 2)
        {
            qanswer = RBLjudge.SelectedValue;//取判断值的值
        }


        return qanswer;
    }
    protected void Btnreturn_Click(object sender, EventArgs e)
    {
        Session["classtype"] = DDLclass.SelectedValue;
        Response.Redirect("~/Quiz/quiz.aspx", false);
    }
    protected void DDLqtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLqtype.SelectedIndex == 0)
        {
            RBLselect.Visible = true;
            CBLselect.Visible = false;
            RBLjudge.Visible = false;
        }
        if (DDLqtype.SelectedIndex == 1)
        {
            RBLselect.Visible = false;
            CBLselect.Visible = true;
            RBLjudge.Visible = false;
        }
        if (DDLqtype.SelectedIndex == 2)
        {
            RBLselect.Visible = false;
            CBLselect.Visible = false;
            RBLjudge.Visible = true;
        }
    }
}

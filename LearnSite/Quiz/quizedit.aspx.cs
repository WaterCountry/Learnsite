using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Quiz_quizedit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (Request.QueryString["Qid"] != null)
        {
            if (!IsPostBack)
            {
                Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "试题编辑页面";
                DDLclass.DataSource = LearnSite.Common.TypeNameList.CourseType();
                DDLclass.DataBind();
                ShowQuiz();
            }
        }
        else
        {
            Response.Redirect("~/Quiz/quiz.aspx", false);
        }
    }
    protected void Btnreturn_Click(object sender, EventArgs e)
    {
        Session["classtype"] = DDLclass.SelectedValue;
        Response.Redirect("~/Quiz/quiz.aspx", false);
    }
    protected void Btnedit_Click(object sender, EventArgs e)
    {
        string Qestion = mcontent.InnerText;
        if (Qestion != "")
        {
            int Qid = Int32.Parse(Request.QueryString["Qid"].ToString());
            int qtype = DDLqtype.SelectedIndex;  //0为单选题，1为多选题，2为判断题
            int qscore = Int32.Parse(DDLqscore.SelectedValue);
            string qanalyze = TextBoxqanalyze.Text;
            string qanswer = GetQanswer();

            LearnSite.Model.Quiz model = new LearnSite.Model.Quiz();
            model.Qid = Qid;
            model.Qanalyze = HttpUtility.HtmlEncode(qanalyze);//分析编码
            model.Qanswer = qanswer;
            model.Qscore = qscore;
            model.Qtype = qtype;
            model.Question = HttpUtility.HtmlEncode(Qestion);//题目编码
            model.Qclass = DDLclass.SelectedValue;
            model.Qselect = false;
            LearnSite.BLL.Quiz bll = new LearnSite.BLL.Quiz();
            bll.Update(model);
            System.Threading.Thread.Sleep(500);
            Session["classtype"] = DDLclass.SelectedValue;
            Response.Redirect("~/Quiz/quiz.aspx", false);
        }
        else
        {
            Labelmsg.Text = "请先添加试题！";
        }
    }

    private void ShowQuiz()
    {
        int Qid = Int32.Parse(Request.QueryString["Qid"].ToString());
        LearnSite.BLL.Quiz bll = new LearnSite.BLL.Quiz();
        LearnSite.Model.Quiz model = new LearnSite.Model.Quiz();
        model = bll.GetModel(Qid);
        DDLqtype.SelectedIndex = model.Qtype.Value;
        DDLqscore.SelectedValue = model.Qscore.ToString();
        mcontent.InnerText = HttpUtility.HtmlDecode(model.Question);
        SetQtype(model.Qtype.Value, model.Qanswer);
        TextBoxqanalyze.Text = model.Qanalyze;
        DDLqtype.Enabled = false;
        string qclass = model.Qclass;
        if (qclass != "")
        {
            int dd = DDLclass.Items.Count;
            for (int i = 0; i < dd; i++)
            {
                if (DDLclass.Items[i].Text == qclass)
                {
                    DDLclass.SelectedValue = qclass;
                    break;
                }
            }
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

    private void SetQtype(int Qtype,string Qanswer)
    {
        if (Qtype == 0)
        {
            DDLqtype.SelectedIndex = 0;
            RBLselect.Visible = true;
            RBLselect.SelectedValue = Qanswer;
            CBLselect.Visible = false;
            RBLjudge.Visible = false;
        }
        if (Qtype == 1)
        {
            DDLqtype.SelectedIndex = 1;
            RBLselect.Visible = false;
            CBLselect.Visible = true;
            SetCBL(Qanswer);
            RBLjudge.Visible = false;
        }
        if (Qtype == 2)
        {
            DDLqtype.SelectedIndex = 2;
            RBLselect.Visible = false;
            CBLselect.Visible = false;
            RBLjudge.Visible = true;
            RBLjudge.SelectedValue = Qanswer;
        }    
    }

    private void SetCBL(string Qanswer)
    {
        foreach (char c in Qanswer)
        {
            foreach (ListItem li in CBLselect.Items)
            {
                if (li.Value==c.ToString())
                {
                    li.Selected=true;//遍历设置选中 （多选题）
                }
            }
        
        }
    }
}

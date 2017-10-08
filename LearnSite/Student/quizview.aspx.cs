using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_quizview : System.Web.UI.Page
{
    public string[] WrongsStr;
    public bool isanswer = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        Btnreturn.Attributes.Add("onclick", "window.opener=null;window.open('','_self'); window.close()");
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            if (!IsPostBack)
            {
                ListQuiz();
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }

    private void ShowQuiz()
    {
        string Rnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
        if (Session[Rnum + "quizQids"] != null)
        {
            string[] qstr = (string[])Session[Rnum + "quizQids"];
            foreach (string ch in qstr)
            {
                Response.Write(ch);
                Response.Write("，");
            }
        }
        else
        {
            Response.Write("查看答案超时！");
        }
    }

    private void ListQuiz()
    {
        string Rnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
        int Qgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
        int Qclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
        string myname = Server.UrlDecode(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sname"].ToString());
        LearnSite.BLL.QuizGrade qgbll = new LearnSite.BLL.QuizGrade();
        isanswer = qgbll.GetQanswer(Qgrade, Qclass);

        if (Request.QueryString["Rid"] != null)
        {
            int Rid = Int32.Parse(Request.QueryString["Rid"].ToString());
            LearnSite.BLL.Result rbll = new LearnSite.BLL.Result();
            LearnSite.Model.Result rmodel = new LearnSite.Model.Result();
            rmodel = rbll.GetModel(Rid);
            if (rmodel != null)
            {
                string mystr = rmodel.Rhistory;
                if (mystr != "")
                {
                    WrongsStr = rmodel.Rwrong.Split(',');
                    string[] qmystr = mystr.Split(',');
                    LearnSite.BLL.Quiz bll = new LearnSite.BLL.Quiz();
                    GVQuiz.DataSource = bll.GetListByQidArray(qmystr);
                    GVQuiz.DataBind();
                    Labeltitle.Text = " 〖" + myname + " 随机生成的" + rmodel.Rdate.Value.ToShortDateString() + "号历史记录试卷参考答案〗";
                }
            }
            //显示本次测验试卷参考答案
        }
        else
        {
            if (Session[Rnum + "quizQids"] != null)
            {
                WrongsStr = Session[Rnum + "quizWrong"].ToString().Split(',');//将错题号保存
                string[] qstr = (string[])Session[Rnum + "quizQids"];
                LearnSite.BLL.Quiz bll = new LearnSite.BLL.Quiz();
                GVQuiz.DataSource = bll.GetListByQidArray(qstr);
                GVQuiz.DataBind();

               // Labelmsg.Text = "所有题编号：" + Session[Rnum + "quizRightWrong"].ToString() + "<br/>正确题编号：" + Session[Rnum + "quizRight"].ToString() + "<br/>错误题编号：" + Session[Rnum + "quizWrong"].ToString();
            }
            if (Session[Rnum + "quizrnd"] != null)
            {
                Labeltitle.Text = " 〖" + myname + " 随机生成的" + Session[Rnum + "quizrnd"].ToString() + "号试卷参考答案〗";
                Session[Rnum + "quizrnd"] = null;
            }
        }

    }
    protected void GVQuiz_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (isanswer)//如果显示答案，则错题变红
            {
                string qid = e.Row.Cells[0].Text;
                foreach (string ch in WrongsStr)
                {
                    if (qid == ch)//如果不是正确的，编号背景变红
                    {
                        e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                    }
                }
            }
            e.Row.Cells[5].Visible = isanswer;
            e.Row.Cells[6].Visible = isanswer;
            //当鼠标放上去的时候 先保存当前行的背景颜色 并给附一颜色 
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E1E8E1',this.style.fontWeight='';");
            //当鼠标离开的时候 将背景颜色还原的以前的颜色 
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            //单击行改变行背景颜色 
            e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#D8E0D8'; this.style.color='buttontext';this.style.cursor='default';");
        }
    }
}

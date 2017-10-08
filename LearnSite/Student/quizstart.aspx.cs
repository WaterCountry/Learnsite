using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_quizstart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            LearnSite.Common.CookieHelp.KickStudent();

            if (!IsPostBack)
            {
                if (!IsRight())
                    Response.Redirect("~/Student/myquiz.aspx", false);
                else
                    listQuiz();//列表设置的随机试题
                Btnquiz.Attributes.Add("onclick", "return   confirm('您确定要提交答案吗？');");
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }

    private bool IsRight()
    {
        bool bl = false;
        string Rnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
        if (Session[Rnum + "quizrnd"] != null && Request.QueryString["Rnd"] != null)
        {
            string quizrnd = Session[Rnum + "quizrnd"].ToString();
            string rnd = Request.QueryString["Rnd"].ToString();
            if (quizrnd == rnd)
            {
                bl = true;
                //Labelmsg.Text =quizrnd+ "=" + rnd;//测试用，判断是不是从正确考试入口进入的
            }
        }
        return bl;
    }

    private void listQuiz()
    {
        string Rnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
        int Qgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
        int Qclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
        LearnSite.Model.QuizGrade qmodel = new LearnSite.Model.QuizGrade();
        LearnSite.BLL.QuizGrade qbll = new LearnSite.BLL.QuizGrade();
        qmodel = qbll.GetModelByQobjRclass(Qgrade, Qclass);
        if (qmodel != null)
        {
            if (qmodel.Qopen.Value)
            {
                string selectclass = qmodel.Qclass;
                if (!string.IsNullOrEmpty(selectclass))
                {
                    bool isok = true;
                    if (isok)
                    {
                        LearnSite.BLL.Quiz bll = new LearnSite.BLL.Quiz();
                        DataListonly.DataSource = bll.GetListByQtypeNum(0, qmodel.Qonly.Value, selectclass);
                        DataListonly.DataBind();
                        DataListmore.DataSource = bll.GetListByQtypeNum(1, qmodel.Qmore.Value, selectclass);
                        DataListmore.DataBind();
                        DataListjudge.DataSource = bll.GetListByQtypeNum(2, qmodel.Qjudge.Value, selectclass);
                        DataListjudge.DataBind();
                        Btnquiz.Enabled = true;
                        if (string.IsNullOrEmpty(qmodel.Qanswer.ToString()))
                        {
                            Session[Rnum + "quizQanswer"] = "false";
                        }
                        else
                        {
                            Session[Rnum + "quizQanswer"] = qmodel.Qanswer.ToString();
                        }
                    }
                    selectclass = selectclass.Replace("'", "");
                    showscope.InnerHtml = Qgrade.ToString() + "年级试题范围：<br />" + selectclass.Replace(",", "<br />");//增加试题范围提示
                }
                else
                {
                    Labelmsg.Text = "老师没有选择试题范围！";
                }
            }
            else
            {
                Btnquiz.Enabled = false;
                Labelmsg.Text = "测验暂停，请咨询老师开启！";
            }
        }
        else
        {
            Btnquiz.Enabled = false;
            Labelmsg.Text = "请咨询老师设置本班测验！";
        }
    }

    private void finished()
    {
        string Rnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
        Session[Rnum + "quizRight"] = "-1";
        Session[Rnum + "quizWrong"] = "-1";
        int allscore = CalculateOnly(Rnum) + CalculateMore(Rnum) + CalculateJudge(Rnum);
        Labelallscore.Text = allscore.ToString();

        if (allscore > 0)
        {
            string Quizrights = Session[Rnum + "quizRight"].ToString();
            string Quizwrongs = Session[Rnum + "quizWrong"].ToString();
            string Quizqids = Quizrights + "," + Quizwrongs;
            Session[Rnum + "quizRightWrong"] = Quizqids;
            LearnSite.BLL.Quiz qbll = new LearnSite.BLL.Quiz();
            qbll.UpdateQright(Quizrights);
            qbll.UpdateQwrong(Quizwrongs);
            System.Threading.Thread.Sleep(200);
            qbll.UpdateQaccuracy(Quizqids);
        }
    }

    private int CalculateOnly(string Rnum)
    {
        int allscore = 0;
        int wrong = 0;
        foreach (DataListItem item in this.DataListonly.Items)
        {
            string answer = ((Label)item.FindControl("Labelanswer")).Text;
            string up = ((RadioButtonList)item.FindControl("RBLselect")).SelectedValue;
            int thisscore = Int32.Parse(((Label)item.FindControl("Labelscore")).Text);
            if (answer == up)
            {
                allscore += thisscore;
                Session[Rnum + "quizRight"] = Session[Rnum + "quizRight"].ToString() + "," + DataListonly.DataKeys[item.ItemIndex].ToString();
            }
            else
            {
                wrong++;
                Session[Rnum + "quizWrong"] = Session[Rnum + "quizWrong"].ToString() + "," + DataListonly.DataKeys[item.ItemIndex].ToString();
            }
        }
        //Labelonly.Text = "得分" + allscore.ToString() + " 有" + wrong + "题错误";
        return allscore;
    }
    private int CalculateMore(string Rnum)
    {
        int allscore = 0;
        int wrong = 0;
        foreach (DataListItem item in this.DataListmore.Items)
        {
            string answer = ((Label)item.FindControl("Labelanswerm")).Text;
            CheckBoxList cblmore = (CheckBoxList)item.FindControl("CBLselect");
            string up = "";
            foreach (ListItem li in cblmore.Items)
            {
                if (li.Selected == true)
                {
                    up += li.Value;//遍历取选中值 （多选题）
                }
            }
            int thisscore = Int32.Parse(((Label)item.FindControl("Labelscorem")).Text);
            int getscore = 0;
            if (up == answer)
            {
                getscore = thisscore;
                Session[Rnum + "quizRight"] = Session[Rnum + "quizRight"].ToString() + "," + DataListmore.DataKeys[item.ItemIndex].ToString();

            }
            else
            {
                if (up.Length > 0)
                {
                    int i = 0;
                    foreach (char ch in up)
                    {
                        if (answer.IndexOf(ch) > -1)
                        {
                            i++;//查找提交回答是否包含在答案内
                        }
                    }

                    if (i == up.Length)
                    {
                        getscore = thisscore / 2;//如果全部包含在答案内给一半分数
                    }
                }
                wrong++;
                Session[Rnum + "quizWrong"] = Session[Rnum + "quizWrong"].ToString() + "," + DataListmore.DataKeys[item.ItemIndex].ToString();

            }
            allscore += getscore;
        }
        //Labelmore.Text = "得分" + allscore.ToString() + " 有" + wrong + "题错误";
        return allscore;
    }
    private int CalculateJudge(string Rnum)
    {
        int allscore = 0;
        int wrong = 0;
        foreach (DataListItem item in this.DataListjudge.Items)
        {
            string answer = ((Label)item.FindControl("Labelanswerj")).Text;
            string up = ((RadioButtonList)item.FindControl("RBLjudge")).SelectedValue;
            int thisscore = Int32.Parse(((Label)item.FindControl("Labelscorej")).Text);
            if (answer == up)
            {
                allscore += thisscore;
                Session[Rnum + "quizRight"] = Session[Rnum + "quizRight"].ToString() + "," + DataListjudge.DataKeys[item.ItemIndex].ToString();

            }
            else
            {
                wrong++;
                Session[Rnum + "quizWrong"] = Session[Rnum + "quizWrong"].ToString() + "," + DataListjudge.DataKeys[item.ItemIndex].ToString();
            }
        }
        //Labeljudge.Text = "得分" + allscore.ToString() + " 有" + wrong + "题错误";
        return allscore;
    }
    protected void Btnquiz_Click(object sender, EventArgs e)
    {
        finished();
        string strscore = Labelallscore.Text;
        if (strscore != "")
        {
            int Rscore = Int32.Parse(strscore);
            if (Rscore > 0)
            {
                int Rsid = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString());
                string Rnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
                int Rgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
                int Rterm = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["ThisTerm"].ToString());
                DateTime dt = DateTime.Now;
                string odate = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day;
                DateTime Rdate = DateTime.Parse(odate);
                Labelmsg.Text = Rdate.ToString();
                string Rwrong = "";
                string Rhistory = "";
                if (Session[Rnum + "quizRightWrong"] != null)
                {
                    Rwrong = Session[Rnum + "quizWrong"].ToString();
                    Rhistory = Session[Rnum + "quizRightWrong"].ToString();
                }
                LearnSite.Model.Result model = new LearnSite.Model.Result();
                model.Rdate = Rdate;
                model.Rnum = Rnum;
                model.Rscore = Rscore;
                model.Rhistory = Rhistory;
                model.Rwrong = Rwrong;
                model.Rgrade = Rgrade;
                model.Rterm = Rterm;
                model.Rsid = Rsid;
                LearnSite.BLL.Students stbll = new LearnSite.BLL.Students();
                LearnSite.BLL.Result bll = new LearnSite.BLL.Result();
                if (bll.ExistsBynumdate(Rsid, Rdate))
                {
                    if (Session[Rnum + "quizrnd"] != null)
                    {
                        if (bll.UpdateToday(model))
                        {
                            Labelmsg.Text = "你获得了更好的成绩，已经更新成功！";
                            PrintQids();
                            HLanswer.Enabled = true;
                            HLanswer.Visible = true;
                        }
                        else
                        {
                            Labelmsg.Text = "你刚才的成绩比原来的差，不作更新，可以重新再测验！";
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Student/myquiz.aspx", false);//查看过答案
                    }
                }
                else
                {
                    bll.Add(model);//增加今天测验成绩记录
                    Labelmsg.Text = "测验成绩成功提交！";
                    PrintQids();
                    HLanswer.Enabled = true;
                    HLanswer.Visible = true;
                }
                bll.GetAverage(Rsid, Rgrade, Rterm);//计算获得该学号的测验成绩总平均值,更新该学号的测验总成绩
                Btnquiz.Enabled = false;
            }
            else
            {
                Labelmsg.Text = "测验成绩为零，提交无效！";
            }
        }
        else
        {
            Labelmsg.Text = "请先进行测验！";
        }
    }
    /// <summary>
    /// 获取随机试卷的试题编号，并保存到Session中
    /// </summary>
    private void PrintQids()
    {
        int nqidonly = DataListonly.DataKeys.Count;
        int nqidmore = DataListmore.DataKeys.Count;
        int nqidjudge = DataListjudge.DataKeys.Count;
        string[] myqids = new string[nqidonly + nqidmore + nqidjudge];
        for (int i = 0; i < nqidonly; i++)
        {
            myqids[i] = DataListonly.DataKeys[i].ToString();
        }

        for (int j = 0; j < nqidmore; j++)
        {
            myqids[nqidonly + j] = DataListmore.DataKeys[j].ToString();
        }

        for (int k = 0; k < nqidjudge; k++)
        {
            myqids[nqidonly + nqidmore + k] = DataListjudge.DataKeys[k].ToString();
        }
        string Rnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
        Session[Rnum + "quizQids"] = myqids;
    }
}

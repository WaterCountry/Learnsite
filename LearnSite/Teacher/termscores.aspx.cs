using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_termscores : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "学期总评";
            Lbterm.Text = LearnSite.Common.XmlHelp.GetTerm();
            Grade();
            Getclass();
            DDLgrade.SelectedIndex = 0;
            DDLclass.SelectedIndex = 0;
            showstudents();
        }
    }
    protected void BtnScore_Click(object sender, EventArgs e)
    {
        DateTime nowtime1 = DateTime.Now;
        LearnSite.BLL.Students stu = new LearnSite.BLL.Students();
        stu.ClearAllScores();
        stu.UpdateBestSquiz();//取回测验最高成绩
        stu.ThisTeamScoresNew();//批量更新所教所有班级当前学期作品总积分和表现总积分、调查测验分

        stu.ThisTeamGroupScores();//批量更新所教班级当前学期小组合作分

        stu.UpdateWebScore();// 更新学生表中网页制作成绩
        stu.UpdateStscore();// 更新学生表的打字成绩
        stu.UpdateSfscore();//更新学生表的指法成绩
        stu.UpdateSchinese();//更新学生表的中文拼音成绩

        int persscore = int.Parse(DDLwork.SelectedValue);
        int persquiz = int.Parse(DDLquiz.SelectedValue);
        int perswscore = int.Parse(DDLweb.SelectedValue);
        int perstscore = int.Parse(DDLtyper.SelectedValue);
        int perattitude = int.Parse(DDLattitude.SelectedValue);
        int persurvey = int.Parse(DDLsurvey.SelectedValue);
        stu.UpdateAllScore(persscore, persquiz, perswscore, perstscore, perattitude,persurvey);//登录账号教师所教班级按设定百分比计算总分
        DateTime nowtime2 = DateTime.Now;
        Labelmsg.Text = "统计用时：" + LearnSite.Common.Computer.DatagoneMilliseconds(nowtime1, nowtime2) + "毫秒";
        System.Threading.Thread.Sleep(200);
        showstudents();
    }
    protected void Btnape_Click(object sender, EventArgs e)
    {
        DateTime nowtime1 = DateTime.Now;
        LearnSite.BLL.Students stus = new LearnSite.BLL.Students();
        int perA = int.Parse(DDLA.SelectedValue);
        int perE = int.Parse(DDLE.SelectedValue);
        stus.TermAPE(perA, perE);//开始自动评价汇总，perA百分比为A优秀，perE百分比为E待合格，其余为合格
        DateTime nowtime2 = DateTime.Now;
        System.Threading.Thread.Sleep(1000);
        LearnSite.BLL.TermTotal mbll = new LearnSite.BLL.TermTotal();
        mbll.TermScore();//生成学期统计表
        Labelmsg.Text = "自动评价用时：" + LearnSite.Common.Computer.DatagoneMilliseconds(nowtime1, nowtime2) + "毫秒，本学期成绩自动存档成功！";
        showstudents();
    }
    protected void BtnExcel_Click(object sender, EventArgs e)
    {
        LearnSite.BLL.Students stu = new LearnSite.BLL.Students();
        stu.TermExcel();
    }

    private void showstudents()
    {
        int Sgrade = Int32.Parse(DDLgrade.SelectedValue.ToString());
        int Sclass = Int32.Parse(DDLclass.SelectedValue.ToString());
        LearnSite.BLL.Students stus = new LearnSite.BLL.Students();
        GVCourse.DataSource = stus.GetListTerm(Sgrade, Sclass);
        GVCourse.DataBind();
        Btnape.ToolTip = "开始自动评价汇总：" + DDLA.SelectedValue + "%为A优秀，" + DDLE.SelectedValue + "%为E待合格，其余为合格，并保存当前学期成绩表";
    }
    protected void GVCourse_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
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

    private void Grade()
    {
        LearnSite.BLL.Room room = new LearnSite.BLL.Room();
        DDLgrade.DataSource = room.GetGrade();
        DDLgrade.DataTextField = "Rgrade";
        DDLgrade.DataValueField = "Rgrade";
        DDLgrade.DataBind();
    }
    private void Getclass()
    {
        LearnSite.BLL.Room room = new LearnSite.BLL.Room();
        DDLclass.DataSource = room.GetClass();
        DDLclass.DataTextField = "Rclass";
        DDLclass.DataValueField = "Rclass";
        DDLclass.DataBind();
    }
    protected void Btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Teacher/works.aspx", false);
    }
    protected void BtnScoresNo_Click(object sender, EventArgs e)
    {
        LearnSite.BLL.Works works = new LearnSite.BLL.Works();
        works.WorkNoScoreSetP();
        Labelmsg.Text = "所教班级未评作品已经被评为C即6分！";
        System.Threading.Thread.Sleep(500);
        showstudents();
    }
    protected void BtnQuizbest_Click(object sender, EventArgs e)
    {
        LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
        sbll.UpdateBestSquiz();
        Labelmsg.Text = "将所有学生的测验统计成绩更新为本学期其测验最高分！";
        System.Threading.Thread.Sleep(500);
        showstudents();
    }
    protected void DDLgrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(DDLgrade.SelectedValue))
        {
            LearnSite.BLL.Room room = new LearnSite.BLL.Room();
            DDLclass.DataSource = room.GetLimitClass(Int32.Parse(DDLgrade.SelectedValue));
            DDLclass.DataTextField = "Rclass";
            DDLclass.DataValueField = "Rclass";
            DDLclass.DataBind();
            showstudents();
        }
    }
    protected void DDLclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        showstudents();
        Labelmsg.Text = "";
    }
    protected void Btntermview_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Teacher/termview.aspx", false);
    }
    protected void BtnScoresE_Click(object sender, EventArgs e)
    {

    }
}

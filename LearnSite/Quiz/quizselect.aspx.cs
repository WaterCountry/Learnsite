using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Quiz_quizselect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "测验设置";
            Grade();
            ShowSelect();
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
    private void ShowSelect()
    {
        getold();
        DataListGrade.DataSource = LearnSite.Common.TypeNameList.CourseType();
        DataListGrade.DataBind();

        string hidstr = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
        Session[hidstr + "oldselect"] = null;
    }
    protected void BtnSelect_Click(object sender, EventArgs e)
    {
        string hidstr = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
        string selectstr = SelectClass();
        if (selectstr != "" && DDLgrade.SelectedValue != "")
        {
            int Qobj = int.Parse(DDLgrade.SelectedValue);
            LearnSite.BLL.QuizGrade bll = new LearnSite.BLL.QuizGrade();
            LearnSite.Model.QuizGrade model = new LearnSite.Model.QuizGrade();
            model.Qobj = Qobj;
            model.Qclass = selectstr;
            model.Qhid = Int32.Parse(hidstr);
            model.Qonly = Int32.Parse(DDLOnly.SelectedValue);
            model.Qmore = Int32.Parse(DDLMore.SelectedValue);
            model.Qjudge = Int32.Parse(DDLJudge.SelectedValue);
            model.Qopen = Quizpower.Checked;
            model.Qanswer = Quizanswer.Checked;
            string qid = bll.ExistsQobj(Qobj, Int32.Parse(hidstr));
            if (qid!="")
            {
                model.Qid = Int32.Parse(qid);
                bll.Update(model);//如果存在则更新设置
            }
            else
            {
                bll.Add(model);//如果不存在则添加设置
            }
            string quizstr = "暂停";
            if (Quizpower.Checked)
                quizstr = "启用";
            Labelmsg.Text = "你给" + DDLgrade.SelectedValue + "年级选择的测验类型为：" + selectstr + "<br/><br/>单选题数：" + DDLOnly.SelectedValue + "多选题数：" + DDLMore.SelectedValue + "判断题数：" + DDLJudge.SelectedValue + "<br/><br/>当前年级测验开关状态：" + quizstr;
        }
        else
        {
            Labelmsg.Text = "提交选择失败！请至少选择一项";
        }
    }
    private string SelectClass()
    {
        string cstr = "";
        int na = this.DataListGrade.Items.Count;
        for(int i=0;i<na;i++)
        {
            CheckBox ck = (CheckBox)this.DataListGrade.Items[i].FindControl("ChkGrade");
            if (ck.Checked)
            {
                string thstr = "'" + ck.Text + "'";
                cstr = cstr + thstr+",";
            }            
        }
            cstr = cstr.TrimEnd(',');//最除最后一个逗号字母
        return cstr;
    }
    private void getold()
    {
        if (DDLgrade.Items.Count > 0 && DDLgrade.SelectedValue != "")
        {
            string hidstr = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
            string sgrade = DDLgrade.SelectedValue;
            if (LearnSite.Common.WordProcess.IsNum(sgrade))
            {
                int Qobj = int.Parse(sgrade);
                LearnSite.BLL.QuizGrade bll = new LearnSite.BLL.QuizGrade();
                LearnSite.Model.QuizGrade model = new LearnSite.Model.QuizGrade();
                model = bll.GetModelByQobjQhid(Qobj, Int32.Parse(hidstr));
                if (model != null)
                {
                    Session[hidstr + "oldselect"] = model.Qclass;
                    DDLOnly.SelectedValue = model.Qonly.ToString();
                    DDLMore.SelectedValue = model.Qmore.ToString();
                    DDLJudge.SelectedValue = model.Qjudge.ToString();
                    Quizpower.Checked = model.Qopen.Value;
                    if (model.Qanswer != null)
                        Quizanswer.Checked = model.Qanswer.Value;
                }
            }
        }
    }
    protected void DataListGrade_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        string hidstr = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
        if (Session[hidstr + "oldselect"]!=null)
        {
            CheckBox ck = (CheckBox)e.Item.FindControl("ChkGrade");
            if (!string.IsNullOrEmpty(ck.Text))
            {
                if (LearnSite.Common.WordProcess.wordExist(ck.Text, Session[hidstr + "oldselect"].ToString()))
                    ck.Checked = true;
            }
        }
    }
    protected void DDLgrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowSelect();
        Labelmsg.Text = "";
    }
    protected void BtnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Quiz/quiz.aspx", false);
    }
}
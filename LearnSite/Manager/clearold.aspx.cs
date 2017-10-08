using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manager_clearold : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeIsAdmin();
        if (!IsPostBack)
        {
            showGradeSclass();
            ButtonClear.Attributes["OnClick"] = "return confirm('您确定要清除过期记录操作吗？');";
            ButtonClearStudent.Attributes["OnClick"] = "return confirm('您确定要清除该班级学生操作吗？');";
        }
    }
    protected void ButtonClear_Click(object sender, EventArgs e)
    {
        int oldyear = Int32.Parse(DDLyear.SelectedValue);

        LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
        int wct = wbll.DeleteOldyear(oldyear);//清理作品记录

        LearnSite.BLL.Signin sbll = new LearnSite.BLL.Signin();
        int sct = sbll.DeleteOldyear(oldyear);//清理签到记录

        LearnSite.BLL.Result rbll = new LearnSite.BLL.Result();
        int rct = rbll.DeleteOldyear(oldyear);//清理测验记录

        Labelmsg.Text = "执行结果：清理作品记录" + wct.ToString() + "条、清理签到记录" + sct.ToString() + "条、清理测验记录" + rct.ToString() + "条";

    }
    protected void ButtonClearTyper_Click(object sender, EventArgs e)
    {
        LearnSite.BLL.Ptyper pbll = new LearnSite.BLL.Ptyper();
        int pct = pbll.DeleteAll();
        Labelmsg.Text = "执行结果：清除中文打字成绩共" + pct.ToString() + "条";
    }
    protected void ButtonClearFinger_Click(object sender, EventArgs e)
    {
        LearnSite.BLL.Pfinger fbll = new LearnSite.BLL.Pfinger();
        int fct = fbll.ClearTb();
        Labelmsg.Text = "执行结果：清除指法练习成绩共" + fct.ToString() + "条";
    }

    protected void ButtonClearStudent_Click(object sender, EventArgs e)
    {
        if (CheckBoxDel.Checked)
        {
            string cip = Page.Request.UserHostAddress;//客户端IP
            string sip = LearnSite.Common.Computer.GetServerIp();//服务器IP
            if (cip == sip)
            {
                string countstu = TextBoxcount.Text;
                if (countstu != "" && countstu != "0")
                {
                    int sgrade = Int32.Parse(DDLgrade.SelectedValue);
                    int sclass = Int32.Parse(DDLclass.SelectedValue);
                    LearnSite.BLL.Webstudy wbll = new LearnSite.BLL.Webstudy();
                    wbll.DelWebClass(sgrade, sclass);
                    LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
                    int delcount= sbll.DeleteClassMate(sgrade, sclass);//清空该班级学生
                    Labelmsg.Text = "您请空了" + DDLgrade.SelectedValue + "年级" + DDLclass.SelectedValue + "班所有学生共" + delcount.ToString() + "位！";
                    
                    int syear = sbll.GetYear(sgrade, sclass);
                    LearnSite.BLL.Signin gbll = new LearnSite.BLL.Signin();
                    gbll.DelSignClass(sgrade, sclass, syear);
                    //清空签到
                    LearnSite.BLL.Works kbll = new LearnSite.BLL.Works();
                    kbll.DelClass(sgrade, sclass, syear);
                    //清空作品
                    LearnSite.BLL.SurveyFeedback fbll = new LearnSite.BLL.SurveyFeedback();
                    fbll.DelClass(sgrade, sclass, syear);
                    //清空调查
                    LearnSite.BLL.TopicReply rbll = new LearnSite.BLL.TopicReply();
                    rbll.DelClass(sgrade, sclass, syear);
                    //清空讨论
                }
                else
                {
                    Labelmsg.Text = "无学生记录可清空！";
                }
            }
            else
            {
                Labelmsg.Text = "此操作只能在服务器上浏览该页面才能执行，谢谢！";
            }
        }
        else
        {
            Labelmsg.Text = "请在确认操作选项上打勾！";
        }
    }
    private void showGradeSclass()
    {
        LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
        DDLgrade.DataSource = rbll.GetAllGrade();
        DDLgrade.DataTextField = "Rgrade";
        DDLgrade.DataValueField = "Rgrade";
        DDLgrade.DataBind();
        DDLclass.DataSource = rbll.GetClass();
        DDLclass.DataTextField = "Rclass";
        DDLclass.DataValueField = "Rclass";
        DDLclass.DataBind();
        totalclass();
    }
    protected void DDLgrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        totalclass();
        Labelmsg.Text = "您选择了"+DDLgrade.SelectedValue+"年级"+DDLclass.SelectedValue+"班";
    }
    protected void DDLclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        totalclass();
        Labelmsg.Text = "您选择了" + DDLgrade.SelectedValue + "年级" + DDLclass.SelectedValue + "班";
    }
    private void totalclass()
    {
        if (DDLgrade.SelectedValue != null && DDLclass.SelectedValue != null)
        {
            int sgrade = Int32.Parse(DDLgrade.SelectedValue);
            int sclass = Int32.Parse(DDLclass.SelectedValue);
            LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
            TextBoxcount.Text = sbll.CountClassMate(sgrade, sclass).ToString();//获取该班级人数
        }
    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_studentadd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            GradeClass();
            SetSnum();
            SetSyear();
            SetSex();
        }        
    }
    /// <summary>
    /// Snum,Syear,Sgrade,Sclass,Sname,Spwd,Sex,Saddress,Sphone,Sparents,Sheadtheacher,Sscore,Sattitude
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Btnadd_Click(object sender, EventArgs e)
    {
        string snum = Tsnum.Text;
        if (Tsname.Text != "" && LearnSite.Common.WordProcess.IsNum(snum))
        {
            LearnSite.BLL.Students stubll = new LearnSite.BLL.Students();
            if (!stubll.ExistsSnum(snum))
            {
                LearnSite.Model.Students student = new LearnSite.Model.Students();
                student.Snum = snum;
                student.Syear = Int32.Parse(DDLyear.SelectedValue);
                student.Sgrade = Int32.Parse(DDLgrade.SelectedValue);
                student.Sclass = Int32.Parse(DDLclass.SelectedValue);
                student.Sname = Tsname.Text.Trim();
                student.Sex = DDLsex.SelectedValue;
                student.Spwd = Tspwd.Text;
                student.Saddress = Tsaddress.Text.Trim();
                student.Sphone = Tsphone.Text.Trim();
                student.Sparents = Tsparents.Text.Trim();
                student.Sheadtheacher = Tsheadtheacher.Text.Trim();
                student.Sscore = 0;
                student.Sattitude = 0;
                int NewSid = stubll.AddStudent(student);

                System.Threading.Thread.Sleep(500);
                string url = "~/Teacher/studentshow.aspx?Sid=" + NewSid.ToString();
                Response.Redirect(url, false);
            }
            else
            {
                Labelmsg.Text = "该学号已经存在，请重新填写学号！";
            }
        }
        else
        {
            Labelmsg.Text = "姓名、入学年份不能为空！";
        }
    }

    private void GradeClass()
    {
        LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
        DDLgrade.DataSource = rm.GetGrade();
        DDLgrade.DataTextField = "Rgrade";
        DDLgrade.DataValueField = "Rgrade";
        DDLgrade.DataBind();
        if(Request.QueryString["Sgrade"]!=null)
        DDLgrade.SelectedValue = Request.QueryString["Sgrade"].ToString();
        DDLgrade.Enabled = false;
        int Rgrade = Int32.Parse(DDLgrade.SelectedValue);
        DDLclass.DataSource = rm.GetLimitClass(Rgrade);
        DDLclass.DataTextField = "Rclass";
        DDLclass.DataValueField = "Rclass";
        DDLclass.DataBind();
        if (Request.QueryString["Sclass"] != null)
        DDLclass.SelectedValue = Request.QueryString["Sclass"].ToString();
        DDLclass.Enabled = false;
    }

    private void SetSyear()
    {
        int mSgrade = Int32.Parse(DDLgrade.SelectedValue);
        LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
        string mSyear = sbll.GetYear(mSgrade);
        ListItem li = new ListItem();
        li.Text = mSyear;
        li.Value = mSyear;
        DDLyear.Items.Add(li);
        DDLyear.Enabled = false;
    }
    private void SetSex()
    {
        ListItem lim = new ListItem();
        lim.Text = "男";
        lim.Value = "男";
        DDLsex.Items.Add(lim);
        ListItem liw = new ListItem();
        liw.Text = "女";
        liw.Value = "女";
        DDLsex.Items.Add(liw);
    }
    /// <summary>
    /// 自动设置学号最大值
    /// </summary>
    private void SetSnum()
    {
        int Sgrade = Int32.Parse(DDLgrade.SelectedValue);
        int Sclass = Int32.Parse(DDLclass.SelectedValue);
        LearnSite.BLL.Students stubbl = new LearnSite.BLL.Students();
        long NewSnum = stubbl.GetMaxSnum(Sgrade,Sclass);
        LearnSite.BLL.DelStudents dbll = new LearnSite.BLL.DelStudents();
        Tsnum.Text = dbll.GetNewSnum(NewSnum);//获取删除列表中不存在的新学号
    }
    protected void DDLgrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLgrade.SelectedItem != null)
        {
            int Rgrade = Int32.Parse(DDLgrade.SelectedValue);
            LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
            DDLclass.DataSource = rm.GetLimitClass(Rgrade);
            DDLclass.DataBind();
            SetSnum();
        }
    }

}

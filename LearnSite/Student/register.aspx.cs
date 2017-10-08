using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            int Qgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
            int Qclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
            OpenJump(Qgrade, Qclass);//跳转选择
        }
        else
        {
            if (!IsPostBack)
            {
                SetGrade();
                SetClass();
                SetSex();
            }
        }
    }
    protected void BtnRegister_Click(object sender, EventArgs e)
    {
        string g = DDLgrade.SelectedValue;
        string c = DDLclass.SelectedValue;
        string x = DDLsex.SelectedValue;
        string n = Tsname.Text.Trim();
        if (g.Length > 0 && c.Length > 0 && x.Length > 0 && n.Length > 0 && n.Length < 10)
        {
            if (LearnSite.Common.WordProcess.IsChina(n))
            {
                int Sgrade = Int32.Parse(g);
                int Sclass = Int32.Parse(c);
                LearnSite.BLL.Students stubll = new LearnSite.BLL.Students();
                long NewSnum = stubll.GetMaxSnum(Sgrade, Sclass);
                LearnSite.BLL.DelStudents dbll = new LearnSite.BLL.DelStudents();
                string mySyear = stubll.GetYear(Sgrade);
                int Syear = Int32.Parse(mySyear);
                LearnSite.Model.Students student = new LearnSite.Model.Students();
                student.Syear = Syear;
                student.Sgrade = Sgrade;
                student.Sclass = Sclass;
                student.Sname = n;
                student.Sex = DDLsex.SelectedValue;
                string myPwd = LearnSite.Common.WordProcess.GetRandomNumber(3);
                student.Spwd = myPwd;
                student.Saddress = "";
                student.Sphone = "";
                student.Sparents = "";
                student.Sheadtheacher = "在线注册";
                student.Sscore = 0;
                student.Sattitude = 0;
                string Tsnum = dbll.GetNewSnum(NewSnum);//获取删除列表中不存在的新学号
                student.Snum = Tsnum;
                int Sid = stubll.AddStudent(student);
                if (Sid > 1)
                {
                    student.Sid = Sid;//修正注册后cookies中的Sid值  2014-9-28号

                    System.Threading.Thread.Sleep(200);
                    LearnSite.Common.WordProcess.Alert("注册成功，你的学号为" + Tsnum + "密码为" + myPwd + "请牢记！", this.Page);
                    string lbip = Page.Request.UserHostAddress;
                    int Qterm = Int32.Parse(LearnSite.Common.XmlHelp.GetTerm());
                    if (LearnSite.Common.CookieHelp.SetStudentCookies(student, lbip))//写cookies
                    {
                        DateTime LoginTime = DateTime.Now;
                        LearnSite.BLL.Signin gbll = new LearnSite.BLL.Signin();
                        gbll.SigninToday(Tsnum, LoginTime, lbip, Sgrade, Qterm, Sid, n, Sclass, Syear);//签到
                        System.Threading.Thread.Sleep(200);
                        OpenJump(Sgrade, Sclass);//跳转选择
                    }
                }
                else
                {
                    labelmsg.Text = "自动申请的学号已被使用，请点击注册继续申请！";
                }
            }
            else
            {
                labelmsg.Text = "注册名必须为中文！";
            }
        }
        else
        {
            labelmsg.Text = "注册失败！<br/>（当前无班级可注册或姓名长度超过限制！）";
        }
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
    private void SetGrade()
    {
        LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
        DDLgrade.DataSource = rm.GetAllRegGrade();
        DDLgrade.DataTextField = "Rgrade";
        DDLgrade.DataValueField = "Rgrade";
        DDLgrade.DataBind();
    }

    private void SetClass()
    {
        string Cgrade=DDLgrade.SelectedValue;
        if (string.IsNullOrEmpty(Cgrade))
        {
            BtnRegister.Enabled = false;
            labelmsg.Text = "暂停注册";
        }
        else
        {
            int Rgrade = Int32.Parse(Cgrade);
            LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
            DDLclass.DataSource = rm.GetRegClass(Rgrade);
            DDLclass.DataTextField = "Rclass";
            DDLclass.DataValueField = "Rclass";
            DDLclass.DataBind();
            labelmsg.Text = "";
        }
    }

    protected void DDLgrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetClass();
    }

    private void OpenJump(int Sgrade, int Sclass)
    {
        LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
        string Cid = rm.IsRopenRcid(Sgrade, Sclass);

        if (string.IsNullOrEmpty(Cid))
        {
            Response.Redirect("~/Student/myinfo.aspx", false);//如果返回Cid为空
        }
        else
        {
            string myurl = "~/Student/showcourse.aspx?Cid=" + Cid;//快速模式为真，且返回Cid不为空
            Response.Redirect(myurl, true);
        }
    }
    protected void BtnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/index.aspx", false);
    }
}
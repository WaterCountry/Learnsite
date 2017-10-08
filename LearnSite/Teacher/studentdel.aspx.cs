using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_studentdel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "学生删除页面";
            if (Request.QueryString["Sid"] != null)
            {
                int Sid = Int32.Parse(Request.QueryString["Sid"].ToString());
                LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
                LabeSname.Text = "&nbsp;&nbsp;" + sbll.GetSnameBySid(Sid) + "&nbsp;&nbsp;";
            }
            else
            {
                Response.Redirect("~/Teacher/student.aspx", false);
            }
        }
    }
    protected void LinkBtnDel_Click(object sender, EventArgs e)
    {
        int Sid = Int32.Parse(Request.QueryString["Sid"].ToString());
        LearnSite.BLL.Students stu = new LearnSite.BLL.Students();
        //将删除的学生添加到删除列表中，以便将来恢复
        LearnSite.Model.Students smodel = new LearnSite.Model.Students();
        smodel = stu.GetModel(Sid);//获取要删除的学生实体
        LearnSite.Model.DelStudents dmodel = new LearnSite.Model.DelStudents();
        dmodel.Daddress = smodel.Saddress;
        dmodel.Dclass = smodel.Sclass;
        dmodel.Dgrade = smodel.Sgrade;
        dmodel.Dheadtheacher = smodel.Sheadtheacher;
        dmodel.Dname = smodel.Sname;
        dmodel.Dnum = smodel.Snum;
        dmodel.Dparents = smodel.Sparents;
        dmodel.Dphone = smodel.Sphone;
        dmodel.Dsex = smodel.Sex;
        dmodel.Dyear = smodel.Syear;
        LearnSite.BLL.DelStudents dbll = new LearnSite.BLL.DelStudents();
        dbll.Add(dmodel);//在学生删除表增加该学生

        stu.Delete(Sid);//学生表中删除该学生
        System.Threading.Thread.Sleep(500);
        string url = "~/Teacher/student.aspx?Sgrade=" + Request.QueryString["Sgrade"].ToString() + "&&Sclass=" + Request.QueryString["Sclass"].ToString();
        Response.Redirect(url, false);
    }
    protected void LinkBtncancel_Click(object sender, EventArgs e)
    {
        string url = "~/Teacher/student.aspx?Sgrade=" + Request.QueryString["Sgrade"].ToString() + "&&Sclass=" + Request.QueryString["Sclass"].ToString();
        Response.Redirect(url, false);
    }
}

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_delstudents : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();

        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "已删除学生恢复页面";
            showDelstudents();
        }
    }

    private void showDelstudents()
    {
            if (Request.QueryString["Sgrade"] != null && Request.QueryString["Sclass"] != null)
            {
                int Dgrade =Int32.Parse( Request.QueryString["Sgrade"].ToString());
                int Dclass = Int32.Parse(Request.QueryString["Sclass"].ToString());
                LearnSite.BLL.DelStudents dbll = new LearnSite.BLL.DelStudents();
                GVStudent.DataSource = dbll.GetListLimit(Dgrade, Dclass);
                GVStudent.DataBind();
                Labelgradeclass.Text = Dgrade.ToString() + "年级" + Dclass.ToString() + "班";
            }
    }
    protected void GVStudent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string getDid = e.CommandArgument.ToString();
        LearnSite.BLL.DelStudents dbll = new LearnSite.BLL.DelStudents();
        if (e.CommandName.Equals("Revive"))
        {

            LearnSite.Model.DelStudents dmodel = new LearnSite.Model.DelStudents();
            dmodel = dbll.GetModel(Int32.Parse(getDid));//获取该删除学生实体

            LearnSite.Model.Students student = new LearnSite.Model.Students();
            string Snum = dmodel.Dnum;
            student.Snum = Snum;
            student.Syear = dmodel.Dyear;
            student.Sgrade = dmodel.Dgrade;
            student.Sclass = dmodel.Dclass;
            student.Sname = dmodel.Dname;
            student.Sex = dmodel.Dsex;
            student.Spwd = "12345";
            student.Saddress = dmodel.Daddress;
            student.Sphone = dmodel.Dphone;
            student.Sparents = dmodel.Dparents;
            student.Sheadtheacher = dmodel.Dheadtheacher;
            student.Sscore = 0;
            student.Sattitude = 0;
            LearnSite.BLL.Students stubll = new LearnSite.BLL.Students();
            int newSid = stubll.AddStudent(student);//恢复该学生
            dbll.Delete(Int32.Parse(getDid));//在删除列表中去除该学生
            System.Threading.Thread.Sleep(200);
            LearnSite.DBUtility.DbHelperSQL.UpdateStudentNewSid(Snum, newSid);
            showDelstudents();
        }
        if (e.CommandName.Equals("Del"))
        {
            dbll.Delete(Int32.Parse(getDid));//永久删除该学生账号
            showDelstudents();
            LearnSite.Common.WordProcess.Alert("永久删除该学生账号后，请不要重用这个学生学号以防作品等数据关联。因为作品等关联学号并未作删除！", this.Page);
        }
    }
    protected void GVStudent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
            string stuname = e.Row.Cells[2].Text;
            string strjs = "if(confirm('您确定要恢复" +stuname + "同学账号吗?'))return true;else return false; ";
            ((LinkButton)e.Row.FindControl("BtnRevive")).OnClientClick = strjs;
            string deljs = "if(confirm('您确定要永久删除" + stuname + "同学账号吗?'))return true;else return false; ";
            ((LinkButton)e.Row.FindControl("LinkBtnDel")).OnClientClick = deljs;
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
    protected void LinkBtncancel_Click(object sender, EventArgs e)
    {
        string url = "~/Teacher/student.aspx?Sgrade=" + Request.QueryString["Sgrade"].ToString() + "&Sclass=" + Request.QueryString["Sclass"].ToString();
        Response.Redirect(url, false);
    }
}
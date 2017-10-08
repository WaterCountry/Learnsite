using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_studentedit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Sid"] != null)
            {
                if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
                {
                    SetSex();
                    myGrade();
                    ShowStudent();
                }
            }
        }
    }
    protected void BtnsEdit_Click(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            int Sid = Int32.Parse(Request.QueryString["Sid"].ToString());
            LearnSite.Model.Students student = new LearnSite.Model.Students();
            student.Sid = Sid;
            student.Syear = Int32.Parse(Tsyear.Text.Trim());
            student.Sgrade = Int32.Parse(DDLgrade.SelectedValue);
            student.Sclass = Int32.Parse(DDLclass.SelectedValue);
            student.Sname = Tsname.Text.Trim();
            student.Sex = DDLsex.SelectedValue;
            student.Spwd = Tspwd.Text.Trim();
            student.Saddress = Tsaddress.Text.Trim();
            student.Sphone = Tsphone.Text.Trim();
            student.Sparents = Tsparents.Text.Trim();
            student.Sheadtheacher = Tsheadtheacher.Text.Trim();
            LearnSite.BLL.Students stubll = new LearnSite.BLL.Students();
            stubll.Update(student);
            Btnsedit.Text = "修改成功";
            string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
            if (DDLclass.SelectedValue != Session[Hid + "class"].ToString())
            {
                //如果转班，再生成ftp目录，并修改ftp账号中的 Access和HomeDir值（传递Syear,Sclass,Snum）
                LearnSite.Ftp.Disk.CreateOneDir(Tsyear.Text.Trim(), DDLclass.SelectedValue, Tsnum.Text);
                LearnSite.Ftp.Reg.RegEditFtp(Tsnum.Text, Tsyear.Text.Trim(), DDLclass.SelectedValue);
            }
            System.Threading.Thread.Sleep(1000);
            string url = "~/Teacher/studentshow.aspx?Sid=" + Sid;
            Response.Redirect(url, false);
        }
    }
    private void ShowStudent()
    {
        int Sid =Int32.Parse( Request.QueryString["Sid"].ToString());
        LearnSite.Model.Students student = new LearnSite.Model.Students();
        LearnSite.BLL.Students stubll = new LearnSite.BLL.Students();
        student = stubll.GetModel(Sid);
        Tsnum.Text = student.Snum;
        Tsyear.Text = student.Syear.ToString();
        DDLgrade.SelectedValue = student.Sgrade.ToString();
        myClass(student.Sgrade.Value);//2014-10-28修改　因为初始化的年级获取的班级数不足，所以要获取
        DDLclass.SelectedValue = student.Sclass.ToString();
        Tsname.Text = student.Sname;
        Tspwd.Text = student.Spwd;
        DDLsex.SelectedValue = student.Sex;
        Tsaddress.Text = student.Saddress;
        Tsphone.Text = student.Sphone;
        Tsparents.Text = student.Sparents;
        Tsheadtheacher.Text = student.Sheadtheacher;
        Tsscore.Text = student.Sscore.ToString();
        Tsattitude.Text = student.Sattitude.ToString();
        string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
        Session[Hid + "grade"] = DDLgrade.SelectedValue;
        Session[Hid + "class"] = DDLclass.SelectedValue;
    }
    protected void Btnreturn_Click(object sender, EventArgs e)
    {
        string url = "~/Teacher/student.aspx" ;        
        Response.Redirect(url, false);
    }

    private void myGrade()
    {
        LearnSite.BLL.Room room = new LearnSite.BLL.Room();
        DDLgrade.DataSource = room.GetGrade();
        DDLgrade.DataTextField = "Rgrade";
        DDLgrade.DataValueField = "Rgrade";
        DDLgrade.DataBind();
    }
    private void myClass(int Rgrade)
    {
        LearnSite.BLL.Room room = new LearnSite.BLL.Room();
        DDLclass.DataSource = room.GetLimitAllClass(Rgrade);
        DDLclass.DataTextField = "Rclass";
        DDLclass.DataValueField = "Rclass";
        DDLclass.DataBind();
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
}

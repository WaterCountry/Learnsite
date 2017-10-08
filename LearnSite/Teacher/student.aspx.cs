using System;
using System.Collections.Generic;
using System.Web;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Teacher_student : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();

        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "学生浏览页面";
            BtnNoGroup.Attributes["OnClick"] = "return confirm('您确定要解除本班所有学生的分组及组长吗？');";
            BtnSpwdInit.Attributes["OnClick"] = "return confirm('您确定要将本班学生的个人密码初始化为12345吗？');";
            if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
            {
                GradeClass();
                ShowStudents();
                profileSet();
                addStuJs(DDLgrade.SelectedValue, DDLclass.SelectedValue);
            }
        }
    }

    protected void GVStudent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(GVStudent.PageIndex * GVStudent.PageSize + e.Row.RowIndex + 1);

            HyperLink hl = (HyperLink)e.Row.FindControl("Hlname");
            string sid = hl.ToolTip;
            string sgrade = DDLgrade.SelectedValue;
            string sclass = DDLclass.SelectedValue;

            string jsstr = "stuShow('" + sid + "', '" + sgrade + "', '" + sclass + "');";
            hl.Attributes.Add("onclick", jsstr);

            string strjs = "if(confirm('您确定更新" + e.Row.Cells[1].Text + "学号的密码吗?'))return true;else return false; ";
            ((ImageButton)e.Row.FindControl("ImageButton1")).OnClientClick = strjs;

            string sleader = ((Label)e.Row.FindControl("LabelSleader")).Text.ToLower();
            string vpath = "~/Images/gcard.gif";
            ImageButton mbtn = (ImageButton)e.Row.FindControl("ImageBtnGroup");
            if (sleader == "true")
            {
                vpath = "~/Images/gflag.gif";//如果是组长的话,换图标
                string gjs = "if(confirm('您确定撤销" + e.Row.Cells[1].Text + "学号的组长任命吗?'))return true;else return false; ";
                mbtn.OnClientClick = gjs;
                mbtn.ToolTip = "点击卸任这位组长职位";
            }
            else
            {
                string sgjs = "if(confirm('您确定任命" + e.Row.Cells[1].Text + "学号的同学为组长吗?'))return true;else return false; ";
                mbtn.OnClientClick = sgjs;
                mbtn.ToolTip = "点击任命这位同学为组长";

                LinkButton lbtn = (LinkButton)e.Row.FindControl("LinkBtnQuit");
                if (lbtn.Text != "")
                {
                    string lbjs = "if(confirm('您确定将" + e.Row.Cells[1].Text + "学号的同学退组吗?'))return true;else return false; ";
                    lbtn.OnClientClick = lbjs;
                    lbtn.ToolTip = "点击将这位同学退组";
                }
            }
            mbtn.ImageUrl = vpath + "?temp=" + DateTime.Now.Millisecond.ToString();


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
    private void GradeClass()
    {
        LearnSite.BLL.Room room = new LearnSite.BLL.Room();
        DDLgrade.DataSource = room.GetGrade();
        DDLgrade.DataTextField = "Rgrade";
        DDLgrade.DataValueField = "Rgrade";
        DDLgrade.DataBind();
        string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
        if (Session[Hid + "grade"] != null)
        {
            DDLgrade.SelectedValue = Session[Hid + "grade"].ToString();
        }
        int Rgrade = Int32.Parse(DDLgrade.SelectedValue);
        LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
        DDLclass.DataSource = rm.GetLimitClass(Rgrade);
        DDLclass.DataTextField = "Rclass";
        DDLclass.DataValueField = "Rclass";
        DDLclass.DataBind();
        if (Session[Hid + "class"] != null)
        {
            DDLclass.SelectedValue = Session[Hid + "class"].ToString();
        }
    }
    private void ShowStudents()
    {
        int Sgrade = Int32.Parse(DDLgrade.SelectedValue.ToString());
        int Sclass = Int32.Parse(DDLclass.SelectedValue.ToString());
        LearnSite.BLL.Students stus = new LearnSite.BLL.Students();
        DataSet ds = stus.GetListStudents(Sgrade, Sclass);
        Label1.Text = "学生总数"+ds.Tables[0].Rows.Count.ToString()+"位";
        GVStudent.DataSource = ds;
        GVStudent.DataBind();
        ds.Dispose();
    }

    private void addStuJs(string sgrade,string sclass)
    {
        if (sgrade != "" && sclass != "")
        {
            string jsstradd = "stuAdd('" + sgrade + "', '" + sclass + "');";
            HkaddStu.Attributes.Add("onclick", jsstradd);
        }
    }
    protected void DDLgrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLgrade.SelectedItem != null)
        {
            int Rgrade = Int32.Parse(DDLgrade.SelectedValue);
            LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
            DDLclass.DataSource = rm.GetLimitClass(Rgrade);
            DDLclass.DataBind();
            GVStudent.PageIndex = 0;
            ShowStudents();
            profileSet();
            addStuJs(DDLgrade.SelectedValue, DDLclass.SelectedValue);
            if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
            {
                string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
                Session[Hid + "grade"] = DDLgrade.SelectedValue;
                Session[Hid + "class"] = DDLclass.SelectedValue;
            }
        }
        Labelmsg.Text = "";
    }
    protected void DDLclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
            Session[Hid + "grade"] = DDLgrade.SelectedValue;
            Session[Hid + "class"] = DDLclass.SelectedValue;
        }
        GVStudent.PageIndex = 0;
        ShowStudents();
        profileSet();
        addStuJs(DDLgrade.SelectedValue, DDLclass.SelectedValue);
        Labelmsg.Text = "";
    }
    protected void BtnExcel_Click(object sender, EventArgs e)
    {
        LearnSite.BLL.Students stu = new LearnSite.BLL.Students();
        stu.StudentsToExcel();
    }
    protected void GVStudent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("ChangePwd"))
        {
            int mySid = Convert.ToInt32(e.CommandArgument.ToString());
            string myPwd = LearnSite.Common.WordProcess.GenerateRandomNum(2);
            LearnSite.BLL.Students bll = new LearnSite.BLL.Students();
            bll.UpdateSidPwd(mySid.ToString(), myPwd);
            ShowStudents();
            string ch = "你的新密码是：" + myPwd;
            LearnSite.Common.WordProcess.Alert(ch, this.Page);
        }
        if (e.CommandName.Equals("ChangeGroup"))
        {
            int mySid = Convert.ToInt32(e.CommandArgument.ToString());
            LearnSite.BLL.Students bll = new LearnSite.BLL.Students();
            bll.ChangeSleader(mySid);
            System.Threading.Thread.Sleep(300);
            ShowStudents();
        }

        if (e.CommandName.Equals("QuitGroup"))
        {
            int mySid = Convert.ToInt32(e.CommandArgument.ToString());
            LearnSite.BLL.Students bll = new LearnSite.BLL.Students();
            bll.QuitThitGroup(mySid);
            System.Threading.Thread.Sleep(300);
            ShowStudents();
        }

    }
    protected void BtnSpell_Click(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            int Hid = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString());
            string InitPwd = TextBoxPwd.Text.Trim();
            if (LearnSite.Common.WordProcess.IsEnNum(InitPwd))
            {
                LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
                Labelmsg.Text = sbll.SpwdToSpell(Hid, InitPwd);//如果初始化密码格式为字母或数字，则进行转换 
                System.Threading.Thread.Sleep(1000);
                ShowStudents();
            }
            else
            {
                TextBoxPwd.Text = "";
                Labelmsg.Text = "请输入原初始化密码，只能为字母或数字！";
            }
        }
    }
    protected void BtnRevive_Click(object sender, EventArgs e)
    {
        int Sgrade = Int32.Parse(DDLgrade.SelectedValue.ToString());
        int Sclass = Int32.Parse(DDLclass.SelectedValue.ToString());
        string url = "~/Teacher/delstudents.aspx?Sgrade=" + Sgrade + "&Sclass=" + Sclass;
        Response.Redirect(url, false);
    }
    protected void BtnNoGroup_Click(object sender, EventArgs e)
    {
        int Sgrade = Int32.Parse(DDLgrade.SelectedValue.ToString());
        int Sclass = Int32.Parse(DDLclass.SelectedValue.ToString());
        LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
        Labelmsg.Text = "解除本班分组共" + sbll.NoGroup(Sgrade, Sclass).ToString() + "位同学！";
        System.Threading.Thread.Sleep(500);
        ShowStudents();
    }
    protected void DDLgroupMax_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLgroupMax.SelectedValue != "")
        {
            int Sgrade = Int32.Parse(DDLgrade.SelectedValue.ToString());
            int Sclass = Int32.Parse(DDLclass.SelectedValue.ToString());
            LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
            int groupMax = Int32.Parse(DDLgroupMax.SelectedValue);
            rbll.SetRgroupMax(Sgrade, Sclass, groupMax);
        }
    }

    private void profileSet()
    {
        int Sgrade = Int32.Parse(DDLgrade.SelectedValue.ToString());
        int Sclass = Int32.Parse(DDLclass.SelectedValue.ToString());
        LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
        LearnSite.Model.Room rmodel = new LearnSite.Model.Room();
        rmodel = rbll.GetModel(Sgrade, Sclass);
        Ckclass.Checked = rmodel.Rclassedit;
        Ckphoto.Checked = rmodel.Rphotoedit;
        Cksex.Checked = rmodel.Rsexedit;
        Ckname.Checked = rmodel.Rnameedit;
        Ckreg.Checked = rmodel.Rreg;
        string gmax = rbll.GetRgroupMax(Sgrade, Sclass).ToString();
        for (int i = 0; i < DDLgroupMax.Items.Count; i++)
        {
            if (DDLgroupMax.Items[i].Value == gmax)
            {
                DDLgroupMax.SelectedValue = rbll.GetRgroupMax(Sgrade, Sclass).ToString();
                break;
            }
        }        
    }
    protected void Ckclass_CheckedChanged(object sender, EventArgs e)
    {
        int Sgrade = Int32.Parse(DDLgrade.SelectedValue.ToString());
        int Sclass = Int32.Parse(DDLclass.SelectedValue.ToString());
        LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
        rbll.SetRclassedit(Sgrade, Sclass, Ckclass.Checked);
    }
    protected void Ckphoto_CheckedChanged(object sender, EventArgs e)
    {
        int Sgrade = Int32.Parse(DDLgrade.SelectedValue.ToString());
        int Sclass = Int32.Parse(DDLclass.SelectedValue.ToString());
        LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
        rbll.SetRphotoedit(Sgrade, Sclass, Ckphoto.Checked);

    }
    protected void Cksex_CheckedChanged(object sender, EventArgs e)
    {
        int Sgrade = Int32.Parse(DDLgrade.SelectedValue.ToString());
        int Sclass = Int32.Parse(DDLclass.SelectedValue.ToString());
        LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
        rbll.SetRsexedit(Sgrade, Sclass,Cksex.Checked);
    }
    protected void Ckname_CheckedChanged(object sender, EventArgs e)
    {
        int Sgrade = Int32.Parse(DDLgrade.SelectedValue.ToString());
        int Sclass = Int32.Parse(DDLclass.SelectedValue.ToString());
        LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
        rbll.SetRnameedit(Sgrade, Sclass,Ckname.Checked);
    }
    protected void GVStudent_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView theGrid = sender as GridView;  // refer to the GridView
        int newPageIndex = 0;

        if (-2 == e.NewPageIndex)
        { // when click the "GO" Button
            TextBox txtNewPageIndex = null;

            GridViewRow pagerRow = theGrid.BottomPagerRow;

            if (null != pagerRow)
            {
                txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;   // refer to the TextBox with the NewPageIndex value
            }

            if (null != txtNewPageIndex)
            {

                newPageIndex = int.Parse(txtNewPageIndex.Text) - 1; // get the NewPageIndex
            }
        }
        else
        {  // when click the first, last, previous and next Button
            newPageIndex = e.NewPageIndex;
        }

        // check to prevent form the NewPageIndex out of the range
        newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
        newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;
        theGrid.PageIndex = newPageIndex;
        ShowStudents(); 
    }
    protected void Btngroups_Click(object sender, EventArgs e)
    {
        int Sgrade = Int32.Parse(DDLgrade.SelectedValue.ToString());
        int Sclass = Int32.Parse(DDLclass.SelectedValue.ToString());
        string url = "~/Teacher/grouping.aspx?Sgrade=" + Sgrade + "&Sclass=" + Sclass;
        Response.Redirect(url, false);
    }
    protected void Ckreg_CheckedChanged(object sender, EventArgs e)
    {
        int Sgrade = Int32.Parse(DDLgrade.SelectedValue.ToString());
        int Sclass = Int32.Parse(DDLclass.SelectedValue.ToString());
        LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
        rbll.SetRreg(Sgrade, Sclass, Ckreg.Checked);
        if (Ckreg.Checked)
        {
            Ckreg.ToolTip = "允许在线注册为本班学员！";
        }
        else
        {
            Ckreg.ToolTip = "禁止在线注册为本班学员！";
        }
    }
    protected void BtnSpwdInit_Click(object sender, EventArgs e)
    {
        int Sgrade = Int32.Parse(DDLgrade.SelectedValue.ToString());
        int Sclass = Int32.Parse(DDLclass.SelectedValue.ToString());
        LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
        sbll.UpdateMyClassPwd(Sgrade, Sclass);
        System.Threading.Thread.Sleep(500);
        ShowStudents();
    }
}

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_coursecreate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "学案创建页面";
            Labelterm.Text = "第" + LearnSite.Common.XmlHelp.GetTerm() + "学期";
            ShowTypename();
            Grade();
            CksMax();
        }
    }

    private void CksMax()
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            int Cterm = Int32.Parse(LearnSite.Common.XmlHelp.GetTerm());
            int Cobj = Int32.Parse(DDLcobj.SelectedValue);
            int Chid = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString());//教师编号
            LearnSite.BLL.Courses bll = new LearnSite.BLL.Courses();
            int cksn = bll.CksMaxValue(Cterm, Cobj,Chid);
            int courseperiod = Int32.Parse(LearnSite.Common.XmlHelp.GetTypeName("CoursePeriod"));
            if (cksn > courseperiod)
            {
                BtnCreate.Enabled = false;
                Labelmsg.Text = "超过本学期设定的课时数! <br/>请到website.xml中修改CoursePeriod值!";
            }
            else
            {
                DDLCks.SelectedValue = bll.CksMaxValue(Cterm, Cobj,Chid).ToString();
                BtnCreate.Enabled = true;
                Labelmsg.Text = "";
            }
        }
    }
    protected void BtnCreate_Click(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            if (Texttitle.Text != "")
            {
                string ctitle = Texttitle.Text.Trim();
                LearnSite.Model.Courses course = new LearnSite.Model.Courses();
                course.Ctitle = Texttitle.Text;
                course.Cclass = DDLclass.SelectedValue;
                course.Cobj = Int32.Parse(DDLcobj.SelectedValue);
                course.Cterm = Int32.Parse(LearnSite.Common.XmlHelp.GetTerm());
                course.Cks = Int32.Parse(DDLCks.SelectedValue);
                course.Chid = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString());//教师编号
                course.Cdate = DateTime.Now;
                course.Cpublish = Checkcpublish.Checked;
                LearnSite.BLL.Courses cbll = new LearnSite.BLL.Courses();
                int GetCid = cbll.Create(course);
                Labelmsg.Text = GetCid.ToString();
                if (GetCid > 0)
                {
                    LearnSite.Store.CourseStore.CreateStore(GetCid);//创建新学案时，同时创建学案保存路径
                    string urlstr = "~/Teacher/courseedit.aspx?Cid=" + GetCid;
                    Response.Redirect(urlstr, false);
                }
                else
                {
                    Labelmsg.Text = "创建新学案失败！";
                }
            }
            else
            {
                Labelmsg.Text = "请输入新建学案名称！";
            }
        }
    }

    private void Grade()
    {
        LearnSite.BLL.Room room = new LearnSite.BLL.Room();
        DDLcobj.DataSource = room.GetAllGrade();
        DDLcobj.DataTextField = "Rgrade";
        DDLcobj.DataValueField = "Rgrade";        
        DDLcobj.DataBind();
        if (Request.QueryString["Cgrade"] != null)
        {
            DDLcobj.SelectedValue = Request.QueryString["Cgrade"].ToString();
        }
    }
    protected void Btnreturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Teacher/course.aspx", false);
    }
    protected void DDLcobj_SelectedIndexChanged(object sender, EventArgs e)
    {
        CksMax();
    }
    private void ShowTypename()
    {
        DDLclass.DataSource = LearnSite.Common.TypeNameList.CourseType();
        DDLclass.DataBind();
        DDLCks.DataSource = LearnSite.Common.TypeNameList.CoursePeriod();
        DDLCks.DataBind();
    }
}

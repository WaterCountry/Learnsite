using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_missionadd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (Request.QueryString["Mcid"] != null)
        {
            if (!IsPostBack)
            {
                Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "学案活动添加页面";
                ShowTypename();
                ShowMgid();
                ShowCourseFiled();
            }
        }
        else
        {
            Response.Redirect("~/Teacher/course.aspx", false);
        }
    }
    protected string myCid()
    {
        if (Request.QueryString["Mcid"] != null)
        {
            return Request.QueryString["Mcid"].ToString();
        }
        else
        {
            return "";
        }
    }
    private void ShowMgid()
    {
        string hidstr = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
        LearnSite.BLL.Gauge gbll = new LearnSite.BLL.Gauge();
        DDLMgid.DataSource=gbll.GetListGauge(Int32.Parse(hidstr));
        DDLMgid.DataTextField = "Gtitle";
        DDLMgid.DataValueField = "Gid";
        DDLMgid.DataBind();
    }

    private void ShowCourseFiled()
    {
        if (Request.QueryString["Mcid"] != null)
        {
            int Mcid = Int32.Parse(Request.QueryString["Mcid"].ToString());
            LearnSite.BLL.Courses cbll = new LearnSite.BLL.Courses();
            LearnSite.Model.Courses cmodel = new LearnSite.Model.Courses();
            cmodel = cbll.GetModel(Mcid);
            DDLmfiletype.SelectedValue = cmodel.Cfiletype;
        }
    }
    protected void Btnadd_Click(object sender, EventArgs e)
    {
        string fckstr = Request.Form["textareaItem"].Trim();
        if (Texttitle.Text != "" && fckstr != "")
        {
            if (Request.QueryString["Mcid"] != null)
            {
                string Mcidstr = Request.QueryString["Mcid"].ToString();
                int Mcid = Int32.Parse(Mcidstr);
                string coursePath = LearnSite.Store.CourseStore.CoursePath(Mcidstr);
                if (CheckRemote.Checked)
                    fckstr = LearnSite.Common.ImageDown.UploadRemote(fckstr, coursePath);
                LearnSite.BLL.Mission missionbll = new LearnSite.BLL.Mission();
                LearnSite.Model.Mission mission = new LearnSite.Model.Mission();
                LearnSite.BLL.ListMenu lbll = new LearnSite.BLL.ListMenu();
                int maxSort = lbll.GetMaxLsort(Mcid) + 1;
                mission.Mcid = Mcid;
                mission.Mtitle = Texttitle.Text.Trim();
                mission.Msort = maxSort;
                bool uploadcan= CheckUpload.Checked;
                mission.Mupload = uploadcan;
                if (uploadcan)
                    mission.Mcategory = 0;//有作业提交
                else
                    mission.Mcategory = 1;//无作业提交
                mission.Mexample = "";
                mission.Mpublish = CheckPublish.Checked;
                mission.Mcontent = HttpUtility.HtmlEncode(fckstr);
                mission.Mfiletype = DDLmfiletype.SelectedValue;
                mission.Mdate = DateTime.Now;
                mission.Mhit = 0;
                mission.Mgroup = CheckGroup.Checked;
                if (DDLMgid.SelectedValue != "")
                    mission.Mgid = Int32.Parse(DDLMgid.SelectedValue);
                else
                    mission.Mgid = 0;
                int mid= missionbll.Add(mission);
                LearnSite.Model.ListMenu lmodel = new LearnSite.Model.ListMenu();
                lmodel.Lcid = Mcid;
                lmodel.Lshow = CheckPublish.Checked;
                lmodel.Lsort = maxSort;
                lmodel.Ltitle = Texttitle.Text.Trim();
                if (uploadcan)
                    lmodel.Ltype = 1;
                else
                    lmodel.Ltype = 6;//描述页面
                lmodel.Lxid = mid;
                lbll.Add(lmodel);
                System.Threading.Thread.Sleep(500);
                //Labelmsg.Text = "添加学案活动成功";
                string url = "~/Teacher/courseshow.aspx?Cid=" + Mcid.ToString();
                Response.Redirect(url, false);
            }
        }
        else
        {
            Labelmsg.Text = "内容及标题不能为空！";
        }
    }
    protected void BtnCourse_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Mcid"] != null)
        {
            string Cid = Request.QueryString["Mcid"].ToString();
            string url = "~/Teacher/courseshow.aspx?Cid=" + Cid;
            Response.Redirect(url, false);
        }
    }
    private void ShowTypename()
    {
        DDLmfiletype.DataSource = LearnSite.Common.TypeNameList.WorksType();
        DDLmfiletype.DataBind();
    }
}

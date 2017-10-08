using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Teacher_programadd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (Request.QueryString["Mcid"] != null)
        {
            if (!IsPostBack)
            {
                Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "编程主题添加";
                ShowMgid();
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
    protected void Btnadd_Click(object sender, EventArgs e)
    {
        string fckstr = Request.Form["textareaItem"].Trim();
        if (Texttitle.Text != "" && fckstr != "" )
        {
            if (Request.QueryString["Mcid"] != null)
            {
                string Mcidstr = Request.QueryString["Mcid"].ToString();
                int Mcid = Int32.Parse(Mcidstr);
                LearnSite.BLL.Mission missionbll = new LearnSite.BLL.Mission();
                LearnSite.BLL.ListMenu lbll = new LearnSite.BLL.ListMenu();
                int maxSort = lbll.GetMaxLsort(Mcid) + 1;
                string exampleurl = "~/Statics/cat.sb2";//实例路径
                if (Fupload.HasFile)
                {
                    string sbfilename = Fupload.FileName;
                    string savePath = LearnSite.Store.CourseStore.GetSaveUrl("Course", Mcidstr);
                    string shortFileName = Path.GetFileName(sbfilename);
                    string savefilename = savePath + shortFileName;
                    string sbpath = this.Server.MapPath(savefilename);
                    Fupload.SaveAs(sbpath);
                    exampleurl = savefilename;
                }

                LearnSite.Model.Mission mission = new LearnSite.Model.Mission();
                mission.Mcid = Mcid;
                mission.Mtitle = Texttitle.Text.Trim();
                mission.Msort = maxSort;
                mission.Mupload = true;
                mission.Mcategory = 2;//编程页面


                mission.Mexample = exampleurl;//编程实例
                mission.Microworld = CheckMicoWorld.Checked;
                mission.Mpublish = CheckPublish.Checked;
                mission.Mcontent = HttpUtility.HtmlEncode(fckstr);
                mission.Mfiletype = "sb2";
                mission.Mdate = DateTime.Now;
                mission.Mhit = 0;
                mission.Mgroup = false;
                if (DDLMgid.SelectedValue != "")
                    mission.Mgid = Int32.Parse(DDLMgid.SelectedValue);
                else
                    mission.Mgid = 0;
                int mid = missionbll.Add(mission);
                LearnSite.Model.ListMenu lmodel = new LearnSite.Model.ListMenu();
                lmodel.Lcid = Mcid;
                lmodel.Lshow = CheckPublish.Checked;
                lmodel.Lsort = maxSort;
                lmodel.Ltitle = Texttitle.Text.Trim();
                lmodel.Ltype = 5;//页面类型为5 编程
                lmodel.Lxid = mid;
                lbll.Add(lmodel);
                System.Threading.Thread.Sleep(500);
                string url = "~/Teacher/courseshow.aspx?Cid=" + Mcid.ToString();
                Response.Redirect(url, false);
            }

        }
        else
        {
            Labelmsg.Text = "请填写标题或选择实例！";
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

    private void ShowMgid()
    {
        string hidstr = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
        LearnSite.BLL.Gauge gbll = new LearnSite.BLL.Gauge();
        DDLMgid.DataSource = gbll.GetListGauge(Int32.Parse(hidstr));
        DDLMgid.DataTextField = "Gtitle";
        DDLMgid.DataValueField = "Gid";
        DDLMgid.DataBind();
    }
}
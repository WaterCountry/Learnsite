using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Teacher_programedit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (Request.QueryString["Mcid"] != null && Request.QueryString["Mid"] != null)
        {
            if (!IsPostBack)
            {
                Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "编程修改页面";
                ShowMgid();
                missionview();
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
    protected void Btnedit_Click(object sender, EventArgs e)
    {
        string fckstr = mcontent.InnerText;
        if (Texttitle.Text != "" && fckstr != "")
        {
            if (Request.QueryString["Mcid"] != null && Request.QueryString["Mid"] != null)
            {
                string Mcid = Request.QueryString["Mcid"].ToString();
                string Mid = Request.QueryString["Mid"].ToString();
                string Example = HlExample.NavigateUrl;
                if (Fupload.HasFile)
                {
                    string savePath = LearnSite.Store.CourseStore.GetSaveUrl("Course", Mcid);
                    string sbfilename = Fupload.FileName;
                    string shortFileName = Path.GetFileName(sbfilename);
                    string savefilename = savePath + shortFileName;
                    string sbpath = this.Server.MapPath(savefilename);
                    Fupload.SaveAs(sbpath);
                    Example = savefilename;
                }
                LearnSite.Model.Mission mission = new LearnSite.Model.Mission();
                mission.Mid = Int32.Parse(Mid);
                mission.Mtitle = Texttitle.Text.Trim();
                mission.Mupload = true;
                mission.Mcategory = 2;//编程页面
                mission.Mexample = Example;//编程实例
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
                LearnSite.BLL.Mission missionbll = new LearnSite.BLL.Mission();
                missionbll.Update(mission);

                LearnSite.Model.ListMenu lmodel = new LearnSite.Model.ListMenu();
                LearnSite.BLL.ListMenu lbll = new LearnSite.BLL.ListMenu();

                lmodel.Lcid = Int32.Parse(Mcid);
                lmodel.Lxid = Int32.Parse(Mid);
                lmodel.Ltype = 5;//页面类型为5 编程
                lmodel.Lshow = CheckPublish.Checked;
                lmodel.Ltitle = Texttitle.Text.Trim();
                lbll.UpdateMenuThree(lmodel);
                System.Threading.Thread.Sleep(500);
                string url = "~/Teacher/programshow.aspx?Mcid=" + Mcid + "&Mid=" + Mid;
                Response.Redirect(url, false);
            }
            else
            {
                Labelmsg.Text = "取不到主题编号Mid！";
            }
        }
        else
        {
            Labelmsg.Text = "内容及标题不能为空！";
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
    private void missionview()
    {
        if (Request.QueryString["Mid"] != null)
        {
            int Mid = Int32.Parse(Request.QueryString["Mid"].ToString());
            LearnSite.Model.Mission mission = new LearnSite.Model.Mission();
            LearnSite.BLL.Mission missionbll = new LearnSite.BLL.Mission();
            mission = missionbll.GetModel(Mid);
            CheckPublish.Checked = mission.Mpublish;
            Texttitle.Text = mission.Mtitle;
            mcontent.InnerText = HttpUtility.HtmlDecode(mission.Mcontent);
            string sburl = mission.Mexample;
            CheckMicoWorld.Checked = mission.Microworld;
            string filename = LearnSite.Common.WordProcess.getshortfname(sburl);
            HlExample.Text = filename;
            HlExample.NavigateUrl = sburl;

            string mgid = mission.Mgid.ToString();
            if (DDLMgid.Items.FindByValue(mgid) != null)
                DDLMgid.SelectedValue = mgid;
        }
    }
    protected void BtnCourse_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Mcid"] != null && Request.QueryString["Mid"] != null)
        {
            string Mcid = Request.QueryString["Mcid"].ToString();
            string url = "~/Teacher/courseshow.aspx?Cid=" + Mcid.ToString();
            Response.Redirect(url, false);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_missionedit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (Request.QueryString["Mcid"] != null && Request.QueryString["Mid"] != null && Request.QueryString["Lid"] != null)
        {
            if (!IsPostBack)
            {
                Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "学案活动编辑页面";
                ShowTypename();
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
    private void ShowMgid()
    {
        string hidstr = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
        LearnSite.BLL.Gauge gbll = new LearnSite.BLL.Gauge();
        DDLMgid.DataSource = gbll.GetListGauge(Int32.Parse(hidstr));
        DDLMgid.DataTextField = "Gtitle";
        DDLMgid.DataValueField = "Gid";
        DDLMgid.DataBind();
    }
    protected void BtnCourse_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Mcid"] != null && Request.QueryString["Mid"] != null && Request.QueryString["Lid"] != null)
        {
            string Mcid = Request.QueryString["Mcid"].ToString();
            string Mid = Request.QueryString["Mid"].ToString();
            string Lid = Request.QueryString["Lid"].ToString();
            string url = "~/Teacher/missionshow.aspx?Mcid=" + Mcid + "&Mid=" + Mid + "&Lid=" + Lid;
            Response.Redirect(url, false);
        }
    }
    protected void Btnedit_Click(object sender, EventArgs e)
    {
        string fckstr = mcontent.InnerText;
        if (Texttitle.Text != "" && fckstr != "")
        {
            if (Request.QueryString["Mcid"] != null && Request.QueryString["Mid"] != null && Request.QueryString["Lid"] != null)
            {
                string Mcid = Request.QueryString["Mcid"].ToString();
                string Mid = Request.QueryString["Mid"].ToString();
                string Lid = Request.QueryString["Lid"].ToString();

                string coursePath = LearnSite.Store.CourseStore.CoursePath(Mcid);
                if (CheckRemote.Checked)
                    fckstr = LearnSite.Common.ImageDown.UploadRemote(fckstr, coursePath);

                LearnSite.Model.Mission mission = new LearnSite.Model.Mission();
                mission.Mid = Int32.Parse(Mid);
                mission.Mtitle = Texttitle.Text.Trim();
                bool uploadcan = CheckUpload.Checked;
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
                LearnSite.BLL.Mission missionbll = new LearnSite.BLL.Mission();
                missionbll.Update(mission);

                LearnSite.Model.ListMenu lmodel = new LearnSite.Model.ListMenu();
                LearnSite.BLL.ListMenu lbll = new LearnSite.BLL.ListMenu();

                lmodel.Lid = Int32.Parse(Lid);
                if (uploadcan)
                    lmodel.Ltype = 1;
                else
                    lmodel.Ltype = 6;//描述页面
                lmodel.Lshow = CheckPublish.Checked;
                lmodel.Ltitle = Texttitle.Text.Trim();
                lbll.UpdateMenuMission(lmodel);//专用活动分类更新

                System.Threading.Thread.Sleep(500);
                string url = "~/Teacher/missionshow.aspx?Mcid=" + Mcid + "&Mid=" + Mid + "&Lid=" + Lid;
                Response.Redirect(url, false);
            }
            else
            {
                Labelmsg.Text = "取不到活动编号Mid！";
            }
        }
        else
        {
            Labelmsg.Text = "内容及标题不能为空！";
        }
    }
    private void missionview()
    {
        if (Request.QueryString["Mid"] != null)
        {
            int Mid = Int32.Parse(Request.QueryString["Mid"].ToString());
            LearnSite.Model.Mission mission = new LearnSite.Model.Mission();
            LearnSite.BLL.Mission missionbll = new LearnSite.BLL.Mission();
            mission = missionbll.GetModel(Mid);
            mcontent.InnerText= HttpUtility.HtmlDecode(mission.Mcontent);
            DDLmfiletype.SelectedValue = mission.Mfiletype;
            CheckPublish.Checked = mission.Mpublish;
            Texttitle.Text = mission.Mtitle;
            CheckUpload.Checked = mission.Mupload;
            CheckGroup.Checked = mission.Mgroup;
            string mgid = mission.Mgid.ToString();
            if (DDLMgid.Items.FindByValue(mgid) != null)
                DDLMgid.SelectedValue = mgid;
        }
    }
    private void ShowTypename()
    {
        DDLmfiletype.DataSource = LearnSite.Common.TypeNameList.WorksType();
        DDLmfiletype.DataBind();
    }
}

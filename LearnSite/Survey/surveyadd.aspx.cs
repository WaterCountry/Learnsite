using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Survey_surveyadd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "课堂调查添加修改页面";
        if (Request.QueryString["Cid"] != null)
        {
            if (!IsPostBack)
            {
                showsurvey();
            }
        }
        else
        {
            Response.Redirect("~/Teacher/course.aspx", false);
        }
    }
    protected string myCid()
    {
        if (Request.QueryString["Cid"] != null)
        {
            return Request.QueryString["Cid"].ToString();
        }
        else
        {
            return "";
        }
    }
    private void showsurvey()
    {
        if (Request.QueryString["Vid"] != null)
        {
            Btnadd.Text = "修改调查";
            string vid = Request.QueryString["Vid"].ToString();
            LearnSite.Model.Survey vmodel = new LearnSite.Model.Survey();
            LearnSite.BLL.Survey vbll = new LearnSite.BLL.Survey();
            vmodel = vbll.GetModel(Int32.Parse(vid));
            Texttitle.Text = vmodel.Vtitle;
            DDLvtype.SelectedValue = vmodel.Vtype.ToString();
            CheckClose.Checked = vmodel.Vclose;
            mcontent.InnerText = HttpUtility.HtmlDecode(vmodel.Vcontent);
        }
    }
    protected void Btnadd_Click(object sender, EventArgs e)
    {
        string fckstr = mcontent.InnerText;
        if (Texttitle.Text != "" && fckstr != "")
        {
            if (Request.QueryString["Cid"] != null)
            {
                string hidstr = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
                int Vcid = Int32.Parse(Request.QueryString["Cid"].ToString());
                LearnSite.BLL.Survey vbll = new LearnSite.BLL.Survey();
                LearnSite.Model.Survey vmodel = new LearnSite.Model.Survey();
                vmodel.Vcid = Vcid;
                vmodel.Vclose = CheckClose.Checked;
                vmodel.Vcontent = HttpUtility.HtmlEncode(fckstr);
                vmodel.Vdate = DateTime.Now;
                vmodel.Vhid = Int32.Parse(hidstr);
                bool vp = false;
                vmodel.Vpoint = vp;
                vmodel.Vtitle = Texttitle.Text.Trim();
                int Vtype = Int32.Parse(DDLvtype.SelectedValue);
                vmodel.Vtype = Vtype;
                //Vcid,Vhid,Vtitle,Vcontent,Vtype,Vclose,Vpoint,Vdate
                string url = "";

                LearnSite.Model.ListMenu lmodel = new LearnSite.Model.ListMenu();
                LearnSite.BLL.ListMenu lbll = new LearnSite.BLL.ListMenu();
                lmodel.Lcid = Vcid;
                lmodel.Lshow = true;
                lmodel.Lsort = lbll.GetMaxLsort(Vcid) + 1;
                lmodel.Ltitle = Texttitle.Text.Trim();
                lmodel.Ltype = 2;

                if (Request.QueryString["Vid"] != null)
                {
                    int vid = Int32.Parse(Request.QueryString["Vid"].ToString());
                    vmodel.Vid = vid;
                    vbll.UpdateSurvey(vmodel);//更新到调查表中
                    lmodel.Lxid = vid;
                    lbll.UpdateLtitle(lmodel);//更新到导航中
                    LearnSite.BLL.SurveyFeedback fkbll = new LearnSite.BLL.SurveyFeedback();
                    fkbll.UpdateFvtype(vid, Vtype);//2014-3-15修订，如果调查类型改变的话，同时也改变调查结果记录中的类型
                    url = "~/Survey/survey.aspx?Cid=" + Vcid + "&Vid=" + vid;

                    System.Threading.Thread.Sleep(500);
                }
                else
                {
                    int newvid = vbll.Addsurvey(vmodel);//增加到调查表中
                    lmodel.Lxid = newvid;
                    lbll.Add(lmodel);//增加到导航中
                    System.Threading.Thread.Sleep(500);
                    url = "~/Survey/survey.aspx?Cid=" + Vcid + "&Vid=" + newvid ;
                }
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
        if (Request.QueryString["Cid"] != null)
        {
            string Cid = Request.QueryString["Cid"].ToString();
            string url = "~/Teacher/courseshow.aspx?Cid=" + Cid;
            Response.Redirect(url, false);
        }
    }
}
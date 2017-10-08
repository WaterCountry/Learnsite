using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_softview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            if (Request.QueryString["Fid"] != null)
            {
                if (!IsPostBack)
                {
                    Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "下载资源浏览页面";
                    ShowFile();
                }
            }
            else
            {
                Response.Redirect("~/Teacher/soft.aspx", false);
            }
        }
    }

    private void ShowFile()
    {
        if (Request.QueryString["Fid"] != null)
        {
            string Fidstr = Request.QueryString["Fid"].ToString();
            if (LearnSite.Common.WordProcess.IsNum(Fidstr))
            {
                int Fid = Int32.Parse(Fidstr);
                LearnSite.Model.Soft smodel = new LearnSite.Model.Soft();
                LearnSite.BLL.Soft st = new LearnSite.BLL.Soft();
                smodel = st.GetModel(Fid);
                Labeltitle.Text = smodel.Ftitle;
                Labelhit.Text = smodel.Fhit.ToString();
                Labeldate.Text = smodel.Fdate.ToString();
                Labelfiletype.Text = smodel.Ffiletype;
                string typestr = Labelfiletype.Text;
                if (typestr == "")
                {
                    typestr = "read";
                    ImageDown.Visible = false;
                }
                ImageType.ImageUrl = "~/Images/FileType/" + typestr.ToLower() + ".gif";
                Labelclass.Text = smodel.Fclass;
                string furl = smodel.Furl;
                HLurl.NavigateUrl = furl;
                if (furl == "")
                {
                    LBtnfile.Visible = false;
                    ImageDown.Visible = false;
                }
                Labelopen.Text = smodel.Fopen.ToString();
                Labelcontent.Text = HttpUtility.HtmlDecode(smodel.Fcontent);
            }
        }
    }
    protected void BtnEdit_Click(object sender, ImageClickEventArgs e)
    {
        if (Request.QueryString["Fid"] != null)
        {
            string Fid = Request.QueryString["Fid"].ToString();
            string url = "~/Teacher/softedit.aspx?Fid=" + Fid;
            Response.Redirect(url, false);
        }
    }
    protected void Btnreturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Teacher/soft.aspx", false);
    }
    protected void LBtnfile_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Fid"] != null && HLurl.NavigateUrl != "")
        {
            LearnSite.Common.FileDown.DownLoadOut(HLurl.NavigateUrl);
        }
    }
    protected void BtnReturnSmall_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Teacher/soft.aspx", false);
    }
}
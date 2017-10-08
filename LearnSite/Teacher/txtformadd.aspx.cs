using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_txtformadd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (Request.QueryString["Mcid"] != null)
        {
            if (!IsPostBack)
            {
                Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "学案表单添加页面";

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
        if (Texttitle.Text != "" && fckstr != "")
        {
            if (Request.QueryString["Mcid"] != null)
            {
                string Mcidstr = Request.QueryString["Mcid"].ToString();
                int Mcid = Int32.Parse(Mcidstr);
                LearnSite.BLL.TxtForm tbll = new LearnSite.BLL.TxtForm();
                LearnSite.Model.TxtForm tmode = new LearnSite.Model.TxtForm();
                LearnSite.BLL.ListMenu lbll = new LearnSite.BLL.ListMenu();
                int maxSort = lbll.GetMaxLsort(Mcid) + 1;
                tmode.Mcid = Mcid;
                tmode.Mtitle = Texttitle.Text.Trim();
                tmode.Mpublish = CheckPublish.Checked;
                tmode.Mcontent = HttpUtility.HtmlEncode(fckstr);
                tmode.Mdate = DateTime.Now;
                tmode.Mhit = 0;
                int mid = tbll.Add(tmode); 
                LearnSite.Model.ListMenu lmodel = new LearnSite.Model.ListMenu();
                lmodel.Lcid = Mcid;
                lmodel.Lshow = CheckPublish.Checked;
                lmodel.Lsort = maxSort;
                lmodel.Ltitle = Texttitle.Text.Trim();
                lmodel.Ltype = 4;//表单类型为4
                lmodel.Lxid = mid;
                lbll.Add(lmodel);
                System.Threading.Thread.Sleep(500);
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
}
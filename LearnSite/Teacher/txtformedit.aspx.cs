using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_txtformedit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (Request.QueryString["Mcid"] != null && Request.QueryString["Mid"] != null)
        {
            if (!IsPostBack)
            {
                Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "学案表单编辑页面";
                txtformview();
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
    protected void BtnCourse_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Mcid"] != null && Request.QueryString["Mid"] != null)
        {
            string Mcid = Request.QueryString["Mcid"].ToString();
            string Mid = Request.QueryString["Mid"].ToString();
            string url = "~/Teacher/txtformshow.aspx?Mcid=" + Mcid + "&Mid=" + Mid;
            Response.Redirect(url, false);
        }
    }
    protected void Btnedit_Click(object sender, EventArgs e)
    {
        string fckstr = mcontent.InnerText;
        string mtitle = Texttitle.Text.Trim();
        if (mtitle != "" && fckstr != "")
        {
            if (Request.QueryString["Mcid"] != null && Request.QueryString["Mid"] != null)
            {
                Labelmsg.Text = "无";
                string Mcid = Request.QueryString["Mcid"].ToString();
                string Mid = Request.QueryString["Mid"].ToString();
                LearnSite.Model.TxtForm tmode = new LearnSite.Model.TxtForm();
                tmode.Mid = Int32.Parse(Mid);
                tmode.Mcid = Int32.Parse(Mcid);
                tmode.Mtitle = mtitle;
                tmode.Mpublish = CheckPublish.Checked;
                tmode.Mcontent = HttpUtility.HtmlEncode(fckstr);
                tmode.Mdate = DateTime.Now;
                tmode.Mhit = 0;
                tmode.Mdelete = false;
                LearnSite.BLL.TxtForm tfmbll = new LearnSite.BLL.TxtForm();
                tfmbll.Update(tmode);

                //string msg = tmode.Mtitle + "<br>\r\n" + tmode.Mcontent + "<br>\r\n" + tmode.Mdate.ToString() + "<br>\r\n" + tmode.Mpublish.ToString() + "<br>\r\n" + Mid;
                // Labelmsg.Text = msg;

                LearnSite.Model.ListMenu lmodel = new LearnSite.Model.ListMenu();
                LearnSite.BLL.ListMenu lbll = new LearnSite.BLL.ListMenu();

                lmodel.Lcid = Int32.Parse(Mcid);
                lmodel.Lxid = Int32.Parse(Mid);
                lmodel.Ltype = 4;
                lmodel.Lshow = CheckPublish.Checked;
                lmodel.Ltitle = mtitle;
                lbll.UpdateMenuThree(lmodel);

                System.Threading.Thread.Sleep(500);
                string url = "~/Teacher/txtformshow.aspx?Mcid=" + Mcid + "&Mid=" + Mid;
                Response.Redirect(url, false);
            }
            else
            {
                Labelmsg.Text = "取不到表单编号Mid！";
            }
        }
        else
        {
            Labelmsg.Text = "内容及标题不能为空！";
        }
    }
    private void txtformview()
    {
        if (Request.QueryString["Mid"] != null)
        {
            int Mid = Int32.Parse(Request.QueryString["Mid"].ToString());
            LearnSite.Model.TxtForm tmodel = new LearnSite.Model.TxtForm();
            LearnSite.BLL.TxtForm tbll = new LearnSite.BLL.TxtForm();
            tmodel = tbll.GetModel(Mid);

            mcontent.InnerText = HttpUtility.HtmlDecode(tmodel.Mcontent);
            CheckPublish.Checked = tmodel.Mpublish;
            Texttitle.Text =tmodel.Mtitle;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Lessons_thinkadd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
            {
                if (Request.QueryString["Cid"] != null)
                {
                    string Cid = Request.QueryString["Cid"].ToString();
                    LearnSite.BLL.Courses cs = new LearnSite.BLL.Courses();
                    Texttitle.Text = cs.GetTitle(Int32.Parse(Cid));
                }
            }
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
    protected void Btnadd_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Cid"] != null)
        {
            string Fcid = Request.QueryString["Cid"].ToString();
            string Fcontent =HttpUtility.HtmlEncode( Request.Form["textareaItem"].Trim());
            Labelmsg.Text = "添加反思成功";
            LearnSite.Model.Flection flection = new LearnSite.Model.Flection();
            flection.Fcontent = Fcontent;
            flection.Fdate = DateTime.Now;
            flection.Fhid = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString());//教师编号
            flection.Fcid = Int32.Parse(Fcid);
            LearnSite.BLL.Flection flectionbll = new LearnSite.BLL.Flection();
            flectionbll.Add(flection);
            System.Threading.Thread.Sleep(500);
            string url = "~/Lessons/thinkshow.aspx?Cid=" +Int32.Parse( Fcid);
            Response.Redirect(url, false);
        }
    }
    protected void BtnCourse_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Cid"] != null)
        {
            string Cid = Request.QueryString["Cid"].ToString();
            string url = "~/Teacher/course.aspx" ;
            Response.Redirect(url, false);
        }
        else
        {
            Labelmsg.Text = "没有主学案";
        }
    }
}

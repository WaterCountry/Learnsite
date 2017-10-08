using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_ipstudent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + " 本机登录学生名列表查询";
            Btnclose.Attributes.Add("onclick", "window.opener=null;window.open('','_self'); window.close()");
            Showname();
        }
    }

    private void Showname()
    {
        if (Request.QueryString["Qip"] != null)
        {
            string Qip = Request.QueryString["Qip"].ToString();
            LabelIp.Text = Qip;
            LearnSite.BLL.Signin gbll = new LearnSite.BLL.Signin();
            GridViewworks.DataSource = gbll.GetIpStudents(Qip);
            GridViewworks.DataBind();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_Stud : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string stuName = "";
            if (Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname]!= null)
                stuName = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sname"].ToString();
            this.Page.Title = LearnSite.Common.XmlHelp.SiteTitle() +"—"+ HttpUtility.UrlDecode(stuName);
        }
    }


}

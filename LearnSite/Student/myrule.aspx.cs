using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_myrule : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Btnreturn.Attributes.Add("onclick", "window.opener=null;window.open('','_self'); window.close()");
        this.Page.Title =LearnSite.Common.CookieHelp.SetMainPageTitle()+ " 课堂守则";
    }
}

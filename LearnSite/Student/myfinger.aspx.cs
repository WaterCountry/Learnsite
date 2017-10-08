using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_myfinger : System.Web.UI.Page
{
    protected string mysnum;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            if (!IsPostBack)
            {
                showSnum();
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }
    private void showSnum()
    {
        mysnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
        LearnSite.BLL.Pfinger fbll = new LearnSite.BLL.Pfinger();
        oldspd.InnerText = "历史记录：" + fbll.GetPsnum(mysnum) + "个/分";
    }
    
}
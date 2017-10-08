using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_autonomic : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            LearnSite.Common.CookieHelp.KickStudent();
            if (!IsPostBack)
            {
                showMy();
                showCategory();
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }
    protected string GetdownUrl(string aurl)
    {
        aurl = "../Plugins/download.aspx?Id=" + LearnSite.Common.EnDeCode.Encrypt(aurl + "&True", "ls");
        return aurl;
    }
    protected string strcut(string str)
    {
        if (str.Length > 16)
            return LearnSite.Common.WordProcess.CnCutString(str, 16, "...");
        else
            return str;
    }
    private void showMy()
    {
        int mySid = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString());
        LearnSite.BLL.Autonomic abll = new LearnSite.BLL.Autonomic();
        RepMy.DataSource = abll.GetMyList(mySid);//Aid,Aurl,Ftitle
        RepMy.DataBind();
    }
    protected string ListNews(object yid, int num, string css, int len)
    {
        LearnSite.BLL.Autonomic abll = new LearnSite.BLL.Autonomic();
        string html = abll.GetListTopHtml(Convert.ToInt32(yid), num, css, len);
        return html;
    }
    private void showCategory()
    {
        LearnSite.BLL.Soft fbll = new LearnSite.BLL.Soft();
        DLCategory.DataSource = fbll.GetListCategory();
        DLCategory.DataBind();
    }
    protected void DLCategory_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string yid = DLCategory.DataKeys[e.Item.ItemIndex].ToString();
            HyperLink hk = (HyperLink)(e.Item.FindControl("HLYtitle"));
            string url = "~/Student/autonomiccategory.aspx?yid=" + yid;
            hk.NavigateUrl = url;
        }
    }
}
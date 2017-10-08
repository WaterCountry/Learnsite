using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_txtform : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            if (Request.QueryString["Mid"] != null && Request.QueryString["Cid"] != null)
            {
                LearnSite.Common.CookieHelp.KickStudent();
                if (!IsPostBack)
                {
                    ShowTxtForm();
                }
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }

    protected string showMid()
    {
        if (Request.QueryString["Mid"] != null)
            return Request.QueryString["Mid"].ToString();
        else
            return "0";
    }

    private void ShowTxtForm()
    {
        string Mid = Request.QueryString["Mid"].ToString();
        string Wsid = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString();
        if (LearnSite.Common.WordProcess.IsNum(Mid))
        {

            LearnSite.Model.TxtForm tmodel = new LearnSite.Model.TxtForm();
            LearnSite.BLL.TxtForm tbll = new LearnSite.BLL.TxtForm();

            tmodel = tbll.GetModel(Int32.Parse(Mid));
            if (tmodel != null)
            {
                LabelMtitle.Text = tmodel.Mtitle;

                LearnSite.Model.TxtFormBack rmodel = new LearnSite.Model.TxtFormBack();
                LearnSite.BLL.TxtFormBack rbll = new LearnSite.BLL.TxtFormBack();
                int Rid = rbll.GetRid(Wsid, Mid);
                if (Rid > 0)
                {
                    rmodel = rbll.GetModel(Rid);
                    Mcontent.InnerHtml = HttpUtility.HtmlDecode(rmodel.Rwords);
                }
                else
                {
                    Mcontent.InnerHtml = HttpUtility.HtmlDecode(tmodel.Mcontent);
                }
            }
            Hlresult.NavigateUrl = "~/Student/txtformresult.aspx?Mid=" + Mid;
        }
    }
   
}
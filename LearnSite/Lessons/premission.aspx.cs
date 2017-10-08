using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Lessons_premission : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.IsTeacherCookies();
        if (!IsPostBack)
        {
            if (Request.QueryString["Mid"] != null)
            {
                ShowMission();
            }
        }
    }
    private void ShowMission()
    {
        string Mid = Request.QueryString["Mid"].ToString();
        if (LearnSite.Common.WordProcess.IsNum(Mid))
        {
            LearnSite.Model.Mission model = new LearnSite.Model.Mission();
            LearnSite.BLL.Mission mn = new LearnSite.BLL.Mission();
            model = mn.GetModel(Int32.Parse(Mid));
            if (model != null)
            {
                string sWcid = model.Mcid.ToString();
                string sWmid = Mid;
                string sWmsort = model.Msort.ToString();
                string sWfiletype = model.Mfiletype;
                LabelMtitle.Text = model.Mtitle;
                Mcontents.InnerHtml = HttpUtility.HtmlDecode(model.Mcontent);
                LabelMdate.Text = model.Mdate.Value.ToShortDateString();
                LabelMfiletype.Text = sWfiletype;
                CkMupload.Checked = model.Mupload;
                CkMgroup.Checked = model.Mgroup;
                ImageType.ImageUrl = "~/Images/FileType/" + LabelMfiletype.Text.ToLower() + ".gif";
            }
            else
            {
                Mcontents.InnerHtml = "此学案活动不存在！";
            }
        }
    }
}
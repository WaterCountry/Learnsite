using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_showcourse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            if (Request.QueryString["Cid"] != null)
            {
                LearnSite.Common.CookieHelp.KickStudent();
                if (!IsPostBack)
                {
                    ShowCourse();
                    ShowOldWorks(); 
                }
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }

    }
    private void ShowCourse()
    {
        string Cid = Request.QueryString["Cid"].ToString();
        if (LearnSite.Common.WordProcess.IsNum(Cid))
        {
            LearnSite.Model.Courses model = new LearnSite.Model.Courses();
            LearnSite.BLL.Courses cs = new LearnSite.BLL.Courses();
            model = cs.GetModel(Int32.Parse(Cid));
            if (model != null)
            {
                LabelCtitle.Text = model.Ctitle;
                Ccontent.InnerHtml = HttpUtility.HtmlDecode(model.Ccontent);
            }
            else
            {
                Ccontent.InnerHtml = "此学案不存在！";
            } 
        }
    }
    private void ShowOldWorks()
    {
        string Cid = Request.QueryString["Cid"].ToString();
        if (LearnSite.Common.WordProcess.IsNum(Cid))
        {
            int Sid = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString());
            int Sgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
            int Sterm = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["ThisTerm"].ToString());
            LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
            GVold.DataSource = wbll.ShowLastWorks(Sid,Sgrade,Sterm,Int32.Parse(Cid));
            GVold.DataBind();
        }
    }
    protected void GVold_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //当鼠标放上去的时候 先保存当前行的背景颜色 并给附一颜色 
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E1E8E1',this.style.fontWeight='';");
            //当鼠标离开的时候 将背景颜色还原的以前的颜色 
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            //单击行改变行背景颜色 
            e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#D8E0D8'; this.style.color='buttontext';this.style.cursor='default';");
        }
    }

}

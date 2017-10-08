using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_mycode : System.Web.UI.Page
{
    public int sbcount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showCount();
        }
    }
    protected string FixUrl(string imgurl)
    {
        return  imgurl.Replace("~", "..");
    }

    protected void showCount()
    {
        LearnSite.BLL.Works bll = new LearnSite.BLL.Works();
        sbcount = bll.GetSbCount();
        if (sbcount > 0)
        {
            int Pcount = sbcount / 12 + 1;
            LabelCount.Text = Pcount.ToString();
            int cpage = 1;
            LabelCurrent.Text = cpage.ToString();
            showProject(cpage);
        }
    }
    protected void showProject(int page)
    {
        //显示推荐的作品
        LabelCurrent.Text = page.ToString();
        page = page - 1;

        LearnSite.BLL.Works bll = new LearnSite.BLL.Works();
        Repeater1.DataSource = bll.GetSb(page,sbcount);
        Repeater1.DataBind();
    }

    protected void Linkfront_Click(object sender, EventArgs e)
    {
        string p = LabelCurrent.Text;
        int page = Int32.Parse(p);
        if (page > 1)
        {
            page = page - 1;
            showProject(page);
        }
    }
    protected void Linknext_Click(object sender, EventArgs e)
    {
        string p = LabelCurrent.Text;
        int page = Int32.Parse(p);
        string c = LabelCount.Text;
        int Pcount = Int32.Parse(c);
        if (page < Pcount)
        {
            page = page + 1;
            showProject(page);
        }
    }
}
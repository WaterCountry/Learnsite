using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_mycourse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            LearnSite.Common.CookieHelp.KickStudent();

            if (!IsPostBack)
            {
                ShowStudent();
                getoldCids();
                ShowKc();
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }

    private void ShowKc()
    {
        shownew();
        showold();
    }

    private void getoldCids()
    {
        int Cterm = Int32.Parse(LearnSite.Common.XmlHelp.GetTerm());
        int Cgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
        string mySnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
        LearnSite.BLL.SurveyFeedback fbll = new LearnSite.BLL.SurveyFeedback();
        string fcids = fbll.ShowStuFeedbackCids(mySnum, Cterm, Cgrade);
        LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
        string wcids = wbll.ShowStuDoneWorkCids(mySnum, Cterm, Cgrade);
        LearnSite.BLL.TopicReply tbll = new LearnSite.BLL.TopicReply();
        string tcids = tbll.ShowStuDoneReplyCids(mySnum, Cterm, Cgrade);
        LearnSite.BLL.TxtFormBack mbll = new LearnSite.BLL.TxtFormBack();
        string mcids = mbll.ShowStuDoneBackCids(mySnum, Cterm, Cgrade);
        string allcids = "";
        if (wcids != "")
            allcids = allcids + wcids;
        if (fcids != "")
            allcids = allcids + fcids;
        if (tcids != "")
            allcids = allcids + tcids;
        if (mcids != "")
            allcids = allcids + mcids;
        LabelCids.Text = LearnSite.Common.WordProcess.SimpleWordsNew(allcids);
    }

    private void shownew()
    {
        int Cterm = Int32.Parse(LearnSite.Common.XmlHelp.GetTerm());
        string Cnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
        int Cgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
        int Chid = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Rhid"].ToString());
        LearnSite.BLL.Courses cs = new LearnSite.BLL.Courses();
        GridViewnewkc.DataSource = cs.ShowNewCourseNew(Cgrade, Cterm, Chid, LabelCids.Text);
        GridViewnewkc.DataBind();
    }
    private void showold()
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            LearnSite.BLL.Courses cs = new LearnSite.BLL.Courses();
            GridViewdonekc.DataSource = cs.ShowDoneCourseNew(LabelCids.Text);
            GridViewdonekc.DataBind();
        }
    }
    private void ShowStudent()
    {
        int mySid = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString());
        int Sgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
        int Sclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
        Labelip.Text = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["LoginIp"].ToString();
        snum.Text = Server.UrlDecode(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString());
        sclass.Text = Sgrade.ToString() + "." + Sclass.ToString() + "班";
        sname.Text = Server.UrlDecode(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sname"].ToString());
        string ssex = Server.UrlDecode(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sex"].ToString());
        sscore.Text = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sscore"].ToString();
        sattitude.Text = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sattitude"].ToString();
        LearnSite.BLL.Students dbll = new LearnSite.BLL.Students();
        sleadername.Text = Server.UrlDecode(dbll.GetLeader(mySid));
        string murl = LearnSite.Common.Photo.GetStudentPhotoUrl(snum.Text, ssex);
        Imageface.ImageUrl = murl + "?temp=" + DateTime.Now.Millisecond.ToString();
        LabelRank.Text = Server.UrlDecode(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["RankImage"].ToString());
        int myscores = int.Parse(sscore.Text);
        LabelRank.ToolTip = "你当前的等级为：" + myscores / 3 + "级";

        LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
        Session["myClassCid"] = rbll.GetRcid(Sgrade, Sclass);
    }
    protected void GridViewnewkc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1 && Session["myClassCid"] != null)
        {
            string myCid = e.Row.Cells[0].Text;
            if (Session["myClassCid"].ToString() == myCid)
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(225, 252, 224);
                e.Row.Font.Bold = true;
                Image img = (Image)e.Row.FindControl("ImageLeaf");
                img.ImageUrl = "~/Images/leaf.gif";
            }
        }
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
    
    protected void GridViewnewkc_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView theGrid = sender as GridView;  // refer to the GridView
        int newPageIndex = 0;

        if (-2 == e.NewPageIndex)
        { // when click the "GO" Button
            TextBox txtNewPageIndex = null;

            GridViewRow pagerRow = theGrid.BottomPagerRow;

            if (null != pagerRow)
            {
                txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;   // refer to the TextBox with the NewPageIndex value
            }

            if (null != txtNewPageIndex)
            {

                newPageIndex = int.Parse(txtNewPageIndex.Text) - 1; // get the NewPageIndex
            }
        }
        else
        {  // when click the first, last, previous and next Button
            newPageIndex = e.NewPageIndex;
        }

        // check to prevent form the NewPageIndex out of the range
        newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
        newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;
        theGrid.PageIndex = newPageIndex;
        shownew();
    }
    protected void GridViewdonekc_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView theGrid = sender as GridView;  // refer to the GridView
        int newPageIndex = 0;

        if (-2 == e.NewPageIndex)
        { // when click the "GO" Button
            TextBox txtNewPageIndex = null;

            GridViewRow pagerRow = theGrid.BottomPagerRow;

            if (null != pagerRow)
            {
                txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;   // refer to the TextBox with the NewPageIndex value
            }

            if (null != txtNewPageIndex)
            {

                newPageIndex = int.Parse(txtNewPageIndex.Text) - 1; // get the NewPageIndex
            }
        }
        else
        {  // when click the first, last, previous and next Button
            newPageIndex = e.NewPageIndex;
        }

        // check to prevent form the NewPageIndex out of the range
        newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
        newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;
        theGrid.PageIndex = newPageIndex;
        showold();
    }
    protected void GridViewdonekc_RowDataBound(object sender, GridViewRowEventArgs e)
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

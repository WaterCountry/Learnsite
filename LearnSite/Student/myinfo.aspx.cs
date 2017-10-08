using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_myinfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            LearnSite.Common.CookieHelp.KickStudent();
            if (!IsPostBack)
            {
                ShowStudent();
                ShowOnline();
                ShowScoreTop();
                ShowWorksTop();
                ShowSelf();
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }

    private void ShowSelf()
    {
        int loginm = LearnSite.Common.XmlHelp.LoginMode();//获取登录方式： 0 表示个人密码方式登录  1 表示班级密码方式登录
        if (loginm == 0)
        {
            BtnExit.CssClass = "buttonimg";
            BtnExit.Text = "平台退出";
        }
        else
        {
            BtnExit.CssClass = "buttonnone";
        }
    }

    private void ShowScoreTop()
    {
        int Qgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
        LearnSite.BLL.Students st = new LearnSite.BLL.Students();
        GridViewscore.DataSource = st.ShowTopScore(Qgrade);
        GridViewscore.DataBind();
    }

    private void ShowWorksTop()
    {
        int Sgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
        int Syear = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Syear"].ToString());
        int Sterm = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["ThisTerm"].ToString());
        LearnSite.BLL.Works ws = new LearnSite.BLL.Works();
        GridViewwork.DataSource = ws.ShowBestWork(Sgrade, Syear, Sterm);
        GridViewwork.DataBind();    
    }
    private void ShowOnline()
    {
        DateTime today=DateTime.Now;
        int Qyear=today.Year;
        int Qmonth=today.Month;
        int Qday=today.Day;
        int Qgrade=Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
        int Qclass=Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
        LearnSite.BLL.Signin sg = new LearnSite.BLL.Signin();
        DataListonline.DataSource = sg.OnlineToday(Qgrade, Qclass, Qyear, Qmonth, Qday);
        DataListonline.DataBind();
    }

    private void ShowStudent()
    {
        Labelip.Text = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["LoginIp"].ToString();
        string mySnum = Server.UrlDecode(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString());
        int mySid = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString());
        snum.Text = mySnum;
        string Sgrade = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString();
        string Sclass = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString();
        sclass.Text = Sgrade + "." + Sclass + "班";
        sname.Text = Server.UrlDecode(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sname"].ToString());
        string ssex = Server.UrlDecode(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sex"].ToString());
        sscore.Text = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sscore"].ToString();
        sattitude.Text = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sattitude"].ToString();
        int Sgroup = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgroup"].ToString());
        int Sterm = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["ThisTerm"].ToString());
        LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
        string[] tem = wbll.ShowLastWorkSelf(mySid);//2012-12-14修
        string mywid = tem[0];
        string myself = tem[1];
        if (mywid != "" && myself != "")
        {
            LabelWself.Text = HttpUtility.HtmlDecode(myself);
            Hlwork.NavigateUrl = "~/Student/downwork.aspx?Wid=" + mywid;
            Hlwork.Visible = true;
        }
        else
        {
            Hlwork.Visible = false;
        }
        LearnSite.BLL.Students dbll = new LearnSite.BLL.Students();
        string leader = dbll.GetLeaderByGroup(Sgroup);//根据自己的组号，获取组长姓名
        if (leader != "")
        {
            HLgroup.Text = Server.UrlDecode(leader);
        }
        else
        {
            HLgroup.Text = "申请组队";
            HLgroup.NavigateUrl = "~/Profile/mygroup.aspx";
        }
        string murl = LearnSite.Common.Photo.GetStudentPhotoUrl(snum.Text, ssex);
        Imageface.ImageUrl = murl + "?temp=" + DateTime.Now.Millisecond.ToString();

        int myscores = int.Parse(sscore.Text);
        LearnSite.BLL.GroupWork gwbll = new LearnSite.BLL.GroupWork();
        int groupscore = gwbll.GetGscoreAll(Sgroup, Int32.Parse(Sgrade), Int32.Parse(Sclass), Sterm);
        string myrank = Server.UrlDecode(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["RankImage"].ToString());
        string grouprank = LearnSite.Common.Rank.RankImage(groupscore, false);
        LabelRank.Text = myrank + "<br/>" + grouprank;
        string mytip = "你的学分等级为：" + myscores / 3 + "级";
        string grouptip = "你的小组等级为：" + groupscore / 3 + "级";
        LabelRank.ToolTip = mytip + " \r " + grouptip;//tooltip换行原来是\r
    }
    protected void GridViewscore_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
        ShowScoreTop();
    }
    protected void GridViewscore_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(GridViewscore.PageIndex * GridViewscore.PageSize + e.Row.RowIndex + 1);
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
    protected void GridViewwork_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
        ShowWorksTop();
    }
    protected void GridViewwork_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(GridViewwork.PageIndex * GridViewwork.PageSize + e.Row.RowIndex + 1);
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
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            string mySnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
            LearnSite.Common.App.AppUserRemove(mySnum);//移除网站全局变量列表中该用户名

            LearnSite.Common.CookieHelp.ClearStudentCookies();
            Session.Abandon();//取消当前会话   
            Session.Clear();//清除当前浏览器进程所有session 
            System.Threading.Thread.Sleep(300);
            string rurl = "~/index.aspx?qt=" + DateTime.Now.Millisecond.ToString();
            Response.Redirect(rurl, false);
        }
    }
    protected void BtnProfile_Click(object sender, EventArgs e)
    {
        string url = "~/Profile/mychange.aspx";
        Response.Redirect(url, false);
    }
    protected void DataListonline_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        string sleader = ((Label)e.Item.FindControl("LabelSleader")).Text.ToLower();
        string sgroup = ((Label)e.Item.FindControl("LabelSgroup")).Text;
        string qnum = ((Label)e.Item.FindControl("LabelQnum")).Text;
        HyperLink hl = (HyperLink)e.Item.FindControl("HyperQname");
        hl.NavigateUrl = "~/Teacher/studentwork.aspx?Snum=" + qnum;//本学期作品浏览
        string vpath = "~/Images/gcard.gif";
        Image imga = new Image();
        imga = (Image)e.Item.FindControl("Imageflag");
        if (sleader == "true")
        {
            vpath = "~/Images/gflag.gif";//如果是组长的话,换图标
            imga.ToolTip = sgroup + "小组组长";
        }
        else
        {
            if (sgroup == "")
            {
                imga.ToolTip = "未分组";
                vpath = "~/Images/ncard.gif";//如果未分组,换图标
            }
            else
            {
                imga.ToolTip = sgroup + "小组成员";
            }
        }
        imga.ImageUrl = vpath + "?temp=" + DateTime.Now.Millisecond.ToString();
    }
}

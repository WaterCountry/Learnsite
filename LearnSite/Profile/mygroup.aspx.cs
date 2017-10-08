using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Profile_mygroup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            if (!IsPostBack)
            {
                showGroupmsg();
                showGroup();
                showworks();
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }

    }

    private void showGroupmsg()
    {
        int mysid = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString());
        LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
        string sgtitle = sbll.GetSgtitle(mysid);
        if (!string.IsNullOrEmpty(sgtitle))
        {
            PanelSgtitle.Visible = true;
            TextBox1.Text = sgtitle;
        }
        else
        {
            PanelSgtitle.Visible = false;
        }
    }
    private void showGroup()
    {
        int sgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
        int sclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
        LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
        GVgroup.DataSource = sbll.ClassGroup(sgrade, sclass);
        GVgroup.DataBind();
    }

    protected void GVgroup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
            string sid = GVgroup.DataKeys[e.Row.RowIndex].Value.ToString();
            int sgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
            int sclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
            Label lb = (Label)e.Row.FindControl("Labelmember");
            LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
            lb.Text = sbll.GroupMember(sgrade, sclass, int.Parse(sid));

            string strjs = "if(confirm('您确定参加该小组吗?'))return true;else return false; ";
            ((LinkButton)e.Row.FindControl("LinkButton1")).OnClientClick = strjs;

            //当鼠标放上去的时候 先保存当前行的背景颜色 并给附一颜色 
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E1E8E1',this.style.fontWeight='';");
            //当鼠标离开的时候 将背景颜色还原的以前的颜色 
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            //单击行改变行背景颜色 
            e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#D8E0D8'; this.style.color='buttontext';this.style.cursor='default';");
        }
    }
    protected void GVgroup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddGroup"))
        {
            string sid = e.CommandArgument.ToString();
            LearnSite.BLL.Students bll = new LearnSite.BLL.Students();
            int sgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
            int sclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
            int mysid = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString());
            string snum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
            int groupmax = 6;
            LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
            groupmax = rbll.GetRgroupMax(sgrade, sclass); // LearnSite.Common.XmlHelp.GetGroupMax();

            if (bll.GetGroupCount(sgrade, sclass, Int32.Parse(sid)) < groupmax + 1)
            {
                bll.AddThisGroup(snum, Int32.Parse(sid));//每小组人数少于小组上限则可参加
                System.Threading.Thread.Sleep(500);
                showGroup();
            }
            else
            {
                string ch = "小组人数已满" + groupmax + "位，请参加其他小组！";
                LearnSite.Common.WordProcess.Alert(ch, this.Page);
            }
        }
    }
    protected void GVwork_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);

            //当鼠标放上去的时候 先保存当前行的背景颜色 并给附一颜色 
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E1E8E1',this.style.fontWeight='';");
            //当鼠标离开的时候 将背景颜色还原的以前的颜色 
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            //单击行改变行背景颜色 
            e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#D8E0D8'; this.style.color='buttontext';this.style.cursor='default';");
        }
    }

    private void showworks()
    {
        string snum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
        LearnSite.BLL.GroupWork gbll = new LearnSite.BLL.GroupWork();
        GVwork.DataSource = gbll.GetMyWorks(snum);
        GVwork.DataBind();
    }
    protected void BtnSgtitle_Click(object sender, EventArgs e)
    {
        string inputmsg = TextBox1.Text.Trim();
        int wcount = inputmsg.Length;
        if (wcount > 2 && wcount < 9)
        {
            int mysid = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString());
            LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
            if (sbll.UpdateSgtitle(mysid, inputmsg) > 0)
                LearnSite.Common.WordProcess.Alert("修改成功！", this.Page);
            else
                LearnSite.Common.WordProcess.Alert("修改失败！", this.Page);

        }
        else
            LearnSite.Common.WordProcess.Alert("请输入2～6个汉字长度！", this.Page);
    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_grouping : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
            {
                showStudents();
                showGroups();
            }
        }
    }
    private void showStudents()
    {
        if (Request.QueryString["Sgrade"] != null && Request.QueryString["Sclass"] != null)
        {
            int sgrade = Int32.Parse(Request.QueryString["Sgrade"].ToString());
            int sclass = Int32.Parse(Request.QueryString["Sclass"].ToString());
            LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
            string sort = RBsort.SelectedValue;
            DLclass.DataSource = sbll.NoGroupStudents(sgrade, sclass, sort);
            DLclass.DataBind();
            labelclass.Text = sgrade.ToString() + "." + sclass.ToString() + "班(" + DLclass.Items.Count.ToString() + "位)";
        }
    }
    private void showGroups()
    {
        if (Request.QueryString["Sgrade"] != null && Request.QueryString["Sclass"] != null)
        {
            int sgrade = Int32.Parse(Request.QueryString["Sgrade"].ToString());
            int sclass = Int32.Parse(Request.QueryString["Sclass"].ToString());

            LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
            GVGroups.DataSource = sbll.ClassGroup(sgrade, sclass);
            GVGroups.DataBind();
        }
    }
    protected void GVGroups_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            int sid = Convert.ToInt32(GVGroups.DataKeys[e.Row.RowIndex].Value);
            int sgrade = Int32.Parse(Request.QueryString["Sgrade"].ToString());
            int sclass = Int32.Parse(Request.QueryString["Sclass"].ToString());
            DataList dl = (DataList)e.Row.Cells[3].FindControl("DLgstu");
            LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
            if (dl != null)
            {
                dl.DataSource = sbll.GroupMembers(sgrade, sclass, sid);
                dl.DataBind();
            }
            Label lb = (Label)e.Row.FindControl("LabelSscores");
            lb.Text = sbll.MyGroupSscores(sgrade, sclass, sid);
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
    protected void DLgstu_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "Q")
        {
            if (CkQuit.Checked)
            {
                LearnSite.Common.WordProcess.Alert("请先将下面选中的成员锁定退组取消打勾后再尝试！", this.Page);
            }
            else
            {
                int sid = Int32.Parse(e.CommandArgument.ToString());
                LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
                sbll.QuitThitGroup(sid);
                System.Threading.Thread.Sleep(300);
                showStudents();
                showGroups();
            }
        }
    }
    protected void GVGroups_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("A"))
        {
            int sgroup = Convert.ToInt32(e.CommandArgument);

            foreach (DataListItem item in this.DLclass.Items)
            {
                bool Rcheck = ((CheckBox)item.FindControl("SelectStu")).Checked;
                if (Rcheck)
                {
                    Label lb = (Label)item.FindControl("LabelSid");
                    int selectSid = Int32.Parse(lb.Text);
                    LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
                    sbll.AddThisGroup(selectSid, sgroup);
                }
            }
            System.Threading.Thread.Sleep(300);
            showStudents();
            showGroups();
        }
    }
    protected void RBsort_SelectedIndexChanged(object sender, EventArgs e)
    {
        showStudents();
    }
    protected void GVGroups_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GVGroups.EditIndex = -1;
        showGroups();
    }
    protected void GVGroups_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GVGroups.EditIndex = e.NewEditIndex;
        showGroups();
    }
    protected void GVGroups_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string Sid = GVGroups.DataKeys[GVGroups.EditIndex][0].ToString();
        string Sgtitle = ((TextBox)(GVGroups.Rows[e.RowIndex].FindControl("TBoxSgtitle"))).Text.Trim();
        int tcount = Sgtitle.Length;
        if (tcount > 2 && tcount < 9)
        {
            LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
            sbll.UpdateSgtitle(Int32.Parse(Sid), Sgtitle);
            GVGroups.EditIndex = -1;
            showGroups(); ;
        }
        else
        {
            LearnSite.Common.WordProcess.Alert("请输入2～6个汉字长度！", this.Page);
        }
    }
}
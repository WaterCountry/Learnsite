using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manager_roomselect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeIsAdmin();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "班级选择页面";
            ShowRoom();
        }
    }
    protected void DLroom_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        Label lbgrade = (Label)e.Item.FindControl("LabelRgrade");
        Label lbclass = (Label)e.Item.FindControl("LabelRclass");
        Label lbhid = (Label)e.Item.FindControl("LabelRhid");
        ((HyperLink)e.Item.FindControl("Rgradeclass")).Text = lbgrade.Text +"."+ lbclass.Text;
        if (Request.QueryString["Hid"] != null)
        {
            int hid = Int32.Parse(Request.QueryString["Hid"].ToString());

            if (lbhid.Text != "")
            {
                int roomhid = Int32.Parse(lbhid.Text);//获取班级表中的hid值

                if (roomhid == 0)//如果
                {
                    ((CheckBox)e.Item.FindControl("CheckRoom")).Enabled = true;
                    ((HyperLink)e.Item.FindControl("Rgradeclass")).ToolTip = "可选";
                }

                if (roomhid != 0)//如果
                {
                    ((CheckBox)e.Item.FindControl("CheckRoom")).Enabled = false;
                    ((CheckBox)e.Item.FindControl("CheckRoom")).Checked = true;
                    ((HyperLink)e.Item.FindControl("Rgradeclass")).ToolTip = "不可选";
                    ((HyperLink)e.Item.FindControl("Rgradeclass")).BackColor = Labelother.BackColor;
                }

                if (hid == roomhid)
                {
                    ((HyperLink)e.Item.FindControl("Rgradeclass")).BackColor = Labelselect.BackColor;
                    ((HyperLink)e.Item.FindControl("Rgradeclass")).ToolTip = "已选中";
                    ((CheckBox)e.Item.FindControl("CheckRoom")).Enabled = true;
                }
            }
        }

    }

    protected void Btnselect_Click(object sender, EventArgs e)
    {
        foreach (DataListItem item in this.DLroom.Items )
        {
            int Rid =Int32.Parse( ((Label)item.FindControl("LabelRid")).Text);
            bool Rcheck = ((CheckBox)item.FindControl("CheckRoom")).Checked;
            bool Renable = ((CheckBox)item.FindControl("CheckRoom")).Enabled;
            if (Renable)
            {
                int Rhid = 0;
                if (Rcheck)
                {
                     Rhid = Int32.Parse(Request.QueryString["Hid"].ToString());//该记录设为
                }
                LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
                rm.UpdateRhid(Rid, Rhid);
                System.Threading.Thread.Sleep(100);
            }
        }
        System.Threading.Thread.Sleep(1000);
        ShowRoom();
    }
    private void ShowRoom()
    {
        LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
        DLroom.DataSource = rm.GetAllList();
        DLroom.DataBind();    
    }

    protected void Btnreturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Manager/teacher.aspx", false);
    }
}

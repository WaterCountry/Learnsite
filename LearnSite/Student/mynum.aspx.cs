using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_mynum : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GradeClass();
            ListSnum();
        }

    }
    private void ListSnum()
    {
        if (DDLgrade.SelectedValue != null && DDLclass.SelectedValue != null)
        {
            int Sgrade = Int32.Parse(DDLgrade.SelectedValue);
            int Sclass = Int32.Parse(DDLclass.SelectedValue);
            LearnSite.BLL.Students st = new LearnSite.BLL.Students();
            DataListsnum.DataSource = st.GetNameNum(Sgrade, Sclass);
            DataListsnum.DataBind();
        }
    }

    private void GradeClass()
    {
        LearnSite.BLL.Room room = new LearnSite.BLL.Room();
        DDLgrade.DataSource = room.GetGrade();
        DDLgrade.DataTextField = "Rgrade";
        DDLgrade.DataValueField = "Rgrade";
        DDLgrade.DataBind();

        LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
        DDLclass.DataSource = rm.GetClass();
        DDLclass.DataTextField = "Rclass";
        DDLclass.DataValueField = "Rclass";
        DDLclass.DataBind();
    }
    protected void DataListsnum_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        HyperLink hl = new HyperLink();
        hl = (HyperLink)e.Item.FindControl("HLSnum");
        string snum = hl.ToolTip;
        hl.NavigateUrl = "~/index.aspx?mySnum=" + snum;
        string ssex = "无";
        Image img = new Image();
        img = (Image)e.Item.FindControl("ImageStu");
        img.ImageUrl = LearnSite.Common.Photo.GetStuPhotoUrl(snum, ssex);
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        ListSnum();
        if (LearnSite.Common.XmlHelp.LoginMode() == 1)
        {
            //LoginMode为1 表示班级模式，则显示密码，否则不执行下面代码
            ShowPwd();
        }
    }
    private void ShowPwd()
    {
        int Sgrade = Int32.Parse(DDLgrade.SelectedValue);
        int Sclass = Int32.Parse(DDLclass.SelectedValue);
        LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
        TextBoxPwd.Text = rm.GetRoomPwd(Sgrade, Sclass);
    }
}
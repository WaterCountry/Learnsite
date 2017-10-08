using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manager_createroom : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeIsAdmin();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "班级设置页面";
            ShowAllRoom();
        }
    }
    protected void Btncreate_Click(object sender, EventArgs e)
    {
        DateTime nowtime1 = DateTime.Now;
        LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
        rm.DeleteAll();
        System.Threading.Thread.Sleep(500);

        int Rgrademin = Int32.Parse(DDLgrademin.SelectedValue);
        int Rgrademax = Int32.Parse(DDLgrademax.SelectedValue);
        int Rclassmax = Int32.Parse(DDLclassmax.SelectedValue);
        rm.CreateRoom(Rgrademin, Rgrademax, Rclassmax);

        System.Threading.Thread.Sleep(500);
        ShowAllRoom();
        DateTime nowtime2 = DateTime.Now;
        Labelmsg.Text = "自动生成所有班级 用时" + LearnSite.Common.Computer.Datagone(nowtime1, nowtime2) + "秒";
    }

    private void ShowAllRoom()
    {
        LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
        GVclass.DataSource = rm.GetAllList();
        GVclass.DataBind();
        DDLclassmax.DataSource = LearnSite.Common.TypeNameList.ClassMaxSet();
        DDLclassmax.DataBind();
        DDLclassmax.SelectedValue = "8";
    }

    protected void GVclass_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(GVclass.PageIndex * GVclass.PageSize + e.Row.RowIndex + 1);
            LinkButton lbtn = e.Row.Cells[4].Controls[0] as LinkButton;
            lbtn.Attributes.Add("onclick", "return   confirm('您确定要删除该班级吗？');");
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
    protected void GVclass_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
        ShowAllRoom();
    }
    protected void GVclass_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int Rid = Convert.ToInt32(((GridView)sender).DataKeys[index].Values["Rid"].ToString());
            LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
            rm.Delete(Rid);
            System.Threading.Thread.Sleep(500);
            ShowAllRoom();
        }
    }
    protected void BtncreateOne_Click(object sender, EventArgs e)
    {
        string tgrade = TextBoxGrade.Text.Trim();
        string tclass = TextBoxClass.Text.Trim();
        if (tgrade != "" && tclass != "")
        {
            if (LearnSite.Common.WordProcess.IsNum(tgrade) && LearnSite.Common.WordProcess.IsNum(tclass))
            {
                int rgrade = Int32.Parse(tgrade);
                int rclass = Int32.Parse(tclass);
                LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
                if (!rbll.ExistsGC(rgrade, rclass))
                {
                    rbll.AddRoom(rgrade, rclass);//如果不存在则添加该班级
                    Labelmsg.Text = "添加"+tgrade+"年级"+tclass+"班成功，请查看！";
                    System.Threading.Thread.Sleep(500);
                    ShowAllRoom();
                }
                else
                {
                    Labelmsg.Text = "该班级已经存在，不能再添加！";
                }
            }
            else
            {
                Labelmsg.Text = "年级、班级请输入数字表示";
            }
        }
        else
        {
            Labelmsg.Text = "年级、班级为空，请输入！";
        }
    }
}

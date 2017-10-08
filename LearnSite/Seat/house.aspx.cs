using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Seat_house : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeIsAdmin();
        if (!IsPostBack)
            showhouse();
    }
    protected void GVHouse_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
            LinkButton lbtn = (LinkButton)e.Row.FindControl("LinkButtonDel");
            lbtn.Attributes.Add("onclick", "return   confirm('您确定要删除这个机房吗？');");
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
    private void showhouse()
    { 
        LearnSite.BLL.House hbll=new LearnSite.BLL.House();
        GVHouse.DataSource = hbll.GetListHouse();
        GVHouse.DataBind();
        CkBox.Checked = LearnSite.Common.XmlHelp.GetHouseMode();
    }
    protected void GVHouse_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            string hid = e.CommandArgument.ToString();
            LearnSite.BLL.House hbll = new LearnSite.BLL.House();
            hbll.Delete(Int32.Parse(hid));
            System.Threading.Thread.Sleep(200);
            showhouse();
        }
    }
    protected void Buttonadd_Click(object sender, EventArgs e)
    {
        string hname = TextBoxHname.Text.Trim();
        int hln=hname.Length;
        if (hln > 0 && hln < 50)
        {
            LearnSite.Model.House model = new LearnSite.Model.House();
            model.Hname = hname;
            model.Hseat = "";
            LearnSite.BLL.House bll = new LearnSite.BLL.House();
            bll.Add(model);//增加一个机房

            System.Threading.Thread.Sleep(200);
            showhouse();
            TextBoxHname.Text = "";
        }
    }
    protected void CkBox_CheckedChanged(object sender, EventArgs e)
    {
        if (!LearnSite.Common.XmlHelp.SetHouseMode(CkBox.Checked.ToString()))
        {
            CkBox.Checked = false;
            string msg = "website.xml配置文档中不存在参数HouseMode，无法启用！";
            LearnSite.Common.WordProcess.Alert(msg, this.Page);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_shareview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["wGrade"] != null && Request.QueryString["wClass"] != null)
                {
                    int Sgrade = Int32.Parse(Request.QueryString["wGrade"].ToString());
                    int Sclass = Int32.Parse(Request.QueryString["wClass"].ToString());
                    showStudents();
                    this.Page.Title = Sgrade.ToString() + "年级" + Sclass.ToString() + "班学生网盘" + DateTime.Now.ToShortDateString() + "存档情况";
                }
                else
                {
                    Response.Write("请重新登录！");
                }
            }
        }
    }
    private void showStudents()
    {
        int Sgrade = Int32.Parse(Request.QueryString["wGrade"].ToString());
        int Sclass = Int32.Parse(Request.QueryString["wClass"].ToString());
        LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
        GVdisk.DataSource = sbll.GetStudentsSnumSname(Sgrade, Sclass);
        GVdisk.DataBind();
    }
    protected void GVdisk_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string snum = e.Row.Cells[0].Text;
            DataList dl = (DataList)e.Row.Cells[2].FindControl("DLfile");
            LearnSite.BLL.ShareDisk kbll = new LearnSite.BLL.ShareDisk();
            dl.DataSource = kbll.GetSnumList(snum);
            dl.DataBind();
            e.Row.Cells[3].Text = dl.Items.Count.ToString();
        }
    }
    protected void DLfile_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        Image img = (Image)e.Item.FindControl("Imgtype");
        string fext = LearnSite.Common.ShareDisk.extGif(img.ToolTip);
        img.ImageUrl = "~/Images/FileType/" + fext + ".gif";
    }
}
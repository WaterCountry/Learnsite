using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_softcategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "资源分类设置页面";
            showCategory();
        }
    }
    private void showCategory()
    {
        LearnSite.BLL.SoftCategory ybll = new LearnSite.BLL.SoftCategory();
        GVCategory.DataSource = ybll.GetListOrder();
        GVCategory.DataBind();
    }
    protected void GVCategory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GVCategory.EditIndex = -1;
        showCategory();
    }
    protected void GVCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Del":
            case "Top":
            case "Bottom":
                int RowIndex = Convert.ToInt32(e.CommandArgument);
                int yid = Convert.ToInt32(((Label)GVCategory.Rows[RowIndex].FindControl("LabelYid")).Text);
                LearnSite.BLL.SoftCategory ybll = new LearnSite.BLL.SoftCategory();
                if (e.CommandName == "Del")
                {
                    LearnSite.BLL.Soft fbll = new LearnSite.BLL.Soft();
                    if (!fbll.ExistYid(yid))
                    {
                        ybll.Delete(yid);
                        System.Threading.Thread.Sleep(200);
                        showCategory();
                    }
                    else
                    {
                        LearnSite.Common.WordProcess.Alert("无法删除该类别，因为有此类别的资源存在！", this.Page);
                    }
                }
                if (e.CommandName == "Top")
                {
                    if (RowIndex == 0)
                    {
                        ybll.initYsort(); //如果首行，初始化序号
                    }
                    if (RowIndex > 0)
                    {
                        int topyid = Convert.ToInt32(((Label)GVCategory.Rows[RowIndex - 1].FindControl("LabelYid")).Text);//获取上个导航编号
                        ybll.UpdateYsort(yid, false);//当前导航减１向上
                        ybll.UpdateYsort(topyid, true);//上个导航增１向下
                    }
                    System.Threading.Thread.Sleep(500);
                    showCategory();
                }
                if (e.CommandName == "Bottom")
                {
                    int rowscount = GVCategory.Rows.Count;
                    if (RowIndex < rowscount - 1)
                    {
                        int bottomyid = Convert.ToInt32(((Label)GVCategory.Rows[RowIndex + 1].FindControl("LabelYid")).Text);//获取下个导航编号
                        ybll.UpdateYsort(bottomyid, false);//下个导航减１向上
                        ybll.UpdateYsort(yid, true);//当前导航增１向下
                        System.Threading.Thread.Sleep(500);
                        showCategory();
                    }
                }
                break;
        }
    }
    protected void GVCategory_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GVCategory.EditIndex = e.NewEditIndex;
        showCategory();
    }
    protected void GVCategory_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string yid = GVCategory.DataKeys[GVCategory.EditIndex][0].ToString();
        string ytitle = ((TextBox)(GVCategory.Rows[e.RowIndex].FindControl("TBoxYtitle"))).Text.Trim();
        if (ytitle.Length > 1)
        {
            LearnSite.BLL.SoftCategory ybll = new LearnSite.BLL.SoftCategory();
            if (ybll.UpdateYtitle(Int32.Parse(yid), ytitle))
            {
                GVCategory.EditIndex = -1;
                showCategory();
                LearnSite.Common.WordProcess.Alert(yid + "“" + ytitle + "”修改成功！", this.Page);
            }
            else
            {
                GVCategory.EditIndex = -1;
                showCategory();
                LearnSite.Common.WordProcess.Alert(yid+":“" + ytitle + "”修改失败！", this.Page);
            }
        }
        else
        {
            LearnSite.Common.WordProcess.Alert("类别名称不能为空！", this.Page);
        }
    }
    protected void GVCategory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string strjs = "if(confirm('您确定要删除该资源类别吗?'))return true;else return false; ";
            ((LinkButton)e.Row.FindControl("BtnDel")).OnClientClick = strjs;
        }
    }
    protected void Btnadd_Click(object sender, EventArgs e)
    {
            string ytitle = TextBoxNewYtitle.Text.Trim();
            if (!string.IsNullOrEmpty(ytitle))
            {
                LearnSite.BLL.SoftCategory ybll = new LearnSite.BLL.SoftCategory();
                LearnSite.Model.SoftCategory ymodel = new LearnSite.Model.SoftCategory();
                ymodel.Ysort = ybll.GetMaxYsort();
                ymodel.Ytitle = ytitle;
                ymodel.Ycontent = "";
                ymodel.Yopen = false;//不限制
                ybll.Add(ymodel);
                System.Threading.Thread.Sleep(500);
                showCategory();
                TextBoxNewYtitle.Text = "";
            }
            else
            {
                LearnSite.Common.WordProcess.Alert("请输入资源类别！", this.Page);
            }
       
    }
    protected void Btnreturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Teacher/soft.aspx");
    }
}
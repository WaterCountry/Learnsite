using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Teacher_coursetotal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            Btnreturn.Attributes.Add("onclick", "window.opener=null;window.open('','_self'); window.close()");
            if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
            {
                if (Request.QueryString["wGrade"] != null && Request.QueryString["wClass"] != null && Request.QueryString["wCid"] != null)
                {
                    ShowCid();
                    showtotal();
                }
            }
        }

    }
    private void ShowCid()
    {
        string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
        LabelGradeClass.Text = Request.QueryString["wGrade"].ToString() + "年级" + Request.QueryString["wClass"].ToString() + "班";
        int Sgrade = Int32.Parse(Request.QueryString["wGrade"].ToString());
        int Sclass = Int32.Parse(Request.QueryString["wClass"].ToString());
        string myCid = Request.QueryString["wCid"].ToString();//直接url传递
        string cterm = LearnSite.Common.XmlHelp.GetTerm();

        LearnSite.BLL.Courses cbll = new LearnSite.BLL.Courses();
        DDLCid.DataSource = cbll.ShowCidCtitle(Int32.Parse(Hid), Sgrade, Int32.Parse(cterm));
        DDLCid.DataTextField = "Ctitle";
        DDLCid.DataValueField = "Cid";
        DDLCid.DataBind();
        if (myCid != "")
        {
            DDLCid.SelectedValue = myCid;//设置为自动获取的今天本班学案Cid
        }
    }
    private void showtotal()
    {
        string cidSelect = DDLCid.SelectedValue;
        if (!string.IsNullOrEmpty(cidSelect))
        {
            DateTime dt1 = DateTime.Now;
            int Sgrade = Int32.Parse(Request.QueryString["wGrade"].ToString());
            int Sclass = Int32.Parse(Request.QueryString["wClass"].ToString());
            int Cid = Int32.Parse(cidSelect);
            LearnSite.BLL.Courses cbll = new LearnSite.BLL.Courses();
            string Ctitle = cbll.GetTitle(Cid);
            DataTable dt = cbll.CourseTotals(Cid, Sgrade, Sclass);
            string clm = RBsort.SelectedValue;
            if (dt.Columns.Contains("汇总"))
            {
                if (clm == "汇总")
                    dt.DefaultView.Sort = clm + " desc";
                else
                    dt.DefaultView.Sort = clm + " asc";
            }
            else
                dt.DefaultView.Sort = " 学号 asc";
            GridViewclass.DataSource = dt.DefaultView.ToTable();
            GridViewclass.DataBind();

            LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
            DataTable dtg = sbll.groupscores(Sgrade, Sclass, dt, Cid);
            string sl = RBsortGroup.SelectedValue;
            switch (sl)
            {
                case "0":
                    break;//默认排序
                case "1":
                    dtg.DefaultView.Sort = "Sgscore desc,Svscore desc";
                    break;
                case "2":
                    dtg.DefaultView.Sort = "Svscore desc,Sgscore desc";
                    break;
                case "3":
                    dtg.DefaultView.Sort = "Sgwork desc,Svscore desc,Sgscore desc";
                    break;
                case "4":
                    dtg.DefaultView.Sort = "Sgattitude desc,Sgwork desc,Svscore desc,Sgscore desc";
                    break;//Sgattitude
            }
            DataList1.DataSource = dtg.DefaultView.ToTable();
            DataList1.DataBind();
            if (DataList1.Items.Count == 0)
            {
                RBsortGroup.Visible = false;
            }
            dtg.Dispose();
            dt.Dispose();//强制释放
            DateTime dt2 = DateTime.Now;
            Labelmsg.Text = "汇总费时：" + LearnSite.Common.Computer.DatagoneMilliseconds(dt1, dt2) + "毫秒";

            this.Page.Title = LabelGradeClass.Text + "《" + DDLCid.SelectedItem.Text + "》学习汇总";
        }
    }

    protected void GridViewclass_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
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
    protected void Btnreflash_Click(object sender, ImageClickEventArgs e)
    {
        showtotal();
    }
    protected void ImageBtnExcel_Click(object sender, ImageClickEventArgs e)
    {
        string title = LabelGradeClass.Text + "《" + DDLCid.SelectedItem.Text + "》学习汇总";
        title=title.Replace(" ", "");
        string filename = title + ".xls";
        filename = HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8);//解决导出EXCEL时乱码的问
        string style = @"<style> .text { mso-number-format:\@; } </script> "; //解决第一位字符为零时不显示的问题

        Response.ClearContent();
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.ContentType = "application/excel";
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename);

        System.IO.StringWriter sw = new System.IO.StringWriter();//定义一个字符串写入对象
        HtmlTextWriter htw = new HtmlTextWriter(sw);//将html写到服务器控件输出流

        this.GridViewclass.RenderControl(htw);//将控件GRIDVIEW中的内容输出到HTW中
        Response.Write(style);
        Response.Write(sw);
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }
    protected void RBsort_SelectedIndexChanged(object sender, EventArgs e)
    {
        showtotal();
    }
    protected void RBsortGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        showtotal();
    }
    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        string sl = RBsortGroup.SelectedValue;
        switch (sl)
        {
            case "1":
                Label lball = (Label)e.Item.FindControl("LabelGroup");
                lball.Font.Bold = true;
                lball.Font.Size = 11;
                break;
            case "2":
                Label lbave = (Label)e.Item.FindControl("LabelAvg");
                lbave.Font.Bold = true;
                lbave.Font.Size = 11;
                break;
            case "3":
                Label lbgrp = (Label)e.Item.FindControl("LabelCooperation");
                lbgrp.Font.Bold = true;
                lbgrp.Font.Size = 11;
                break;
            case "4":
                Label lbatt = (Label)e.Item.FindControl("Labelattitude");
                lbatt.Font.Bold = true;
                lbatt.Font.Size = 11;
                break;
        }
    }
    protected void DDLCid_SelectedIndexChanged(object sender, EventArgs e)
    {
        showtotal();
    }
}
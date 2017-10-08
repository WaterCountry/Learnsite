using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Teacher_overallmerit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         LearnSite.Common.CookieHelp.JudgeTeacherCookies();
         if (!IsPostBack)
         {
             showSgrade();
         }
    }
    private void showSgrade()
    {
        LearnSite.BLL.TermTotal tbll = new LearnSite.BLL.TermTotal();
        DDLYear.DataSource = tbll.TyearList();
        DDLYear.DataTextField = "Tyear";
        DDLYear.DataValueField = "Tyear";
        DDLYear.DataBind();

        LearnSite.BLL.Room room = new LearnSite.BLL.Room();
        CBgrades.DataSource = room.GetAllGrade().Tables[0];
        CBgrades.DataTextField = "Rgrade";
        CBgrades.DataValueField = "Rgrade";
        CBgrades.DataBind();
        int count=CBgrades.Items.Count;
        for(int i=0;i<count;i++)
        {
        CBgrades.Items[i].Selected=true;
        }
    }

    private void showGv()
    {
        string tyear = DDLYear.SelectedValue;
        if (tyear != null)
        {
            string cgrades = "";
            int count = CBgrades.Items.Count;
            for (int i = 0; i < count; i++)
            {
                if (CBgrades.Items[i].Selected)
                    cgrades = cgrades + "," + CBgrades.Items[i].Value;
            }

            LearnSite.BLL.TermTotal tbll = new LearnSite.BLL.TermTotal();
            DateTime d1 = DateTime.Now;
            GVapes.DataSource = tbll.GetMerit(Int32.Parse(tyear), cgrades);
            GVapes.DataBind();
            DateTime d2 = DateTime.Now;
            Labelmsg.Text = "学生总数：" + GVapes.Rows.Count.ToString() + "位，查询费时：" + LearnSite.Common.Computer.DatagoneMilliseconds(d1, d2) + "毫秒";
        }
    }
    protected void Btnsearch_Click(object sender, EventArgs e)
    {
        showGv();
    }
    protected void Btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Teacher/termview.aspx", false);
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }
    protected void BtnExcel_Click(object sender, EventArgs e)
    {
        string title = DDLYear.SelectedValue + "级各学期综合评定汇总表";
        title = title.Replace(" ", "");
        string filename = title + ".xls";
        filename = HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8);//解决导出EXCEL时乱码的问
        string style = @"<style> .text { mso-number-format:\@; } </script> "; //解决第一位字符为零时不显示的问题

        Response.ClearContent();
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.ContentType = "application/excel";
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename);

        System.IO.StringWriter sw = new System.IO.StringWriter();//定义一个字符串写入对象
        HtmlTextWriter htw = new HtmlTextWriter(sw);//将html写到服务器控件输出流

        this.GVapes.RenderControl(htw);//将控件GRIDVIEW中的内容输出到HTW中
        Response.Write(style);
        Response.Write(sw);
        Response.End();
    }
    protected void BtnImport_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
           string str=  LearnSite.Common.DataExcel.SavekaoxuExcel(FileUpload1);
           if (str != "")
           {
               DataSet ds = LearnSite.Common.DataExcel.ExcelToDataSet(str);
               LearnSite.Common.DataExcel.KaoxutoStudents(ds);
           }
        }
    }
}
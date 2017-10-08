using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manager_divide : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeIsAdmin();
    }
    protected void Btndivide_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string strfilename = LearnSite.Common.DataExcel.SavedivideExcel(FileUpload1);
            if (strfilename != "")
            {
                DateTime nowtime1 = DateTime.Now;
                string msg = LearnSite.Common.DataExcel.DivideClass(strfilename);
                DateTime nowtime2 = DateTime.Now;
                Labelmsg.Text = msg + " 耗时" + LearnSite.Common.Computer.Datagone(nowtime1, nowtime2) + "秒";
                Btndivide.Enabled = false;
            }
        }
        else
        {
            Labelmsg.Text = "请选择上传分班表格！";
        }
    }
}
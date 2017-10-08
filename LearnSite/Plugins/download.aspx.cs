using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Plugins_download : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Id"] != "")
            {
                string result = LearnSite.Common.EnDeCode.Decrypt(Request.QueryString["Id"].ToString(), "ls");
                string [] strtemp=result.Split('&');
                string filename = strtemp[0];
                if (strtemp.Length>1)
                {
                   Literal1.Text= LearnSite.Common.FileDown.DownLoadView(filename);//在页面上输出预览
                }
                else
                {
                    LearnSite.Common.FileDown.DownLoadOut(filename);//在页面上输出流下载 
                }
            }
        }
    }
}
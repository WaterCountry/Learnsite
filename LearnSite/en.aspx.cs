using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class en : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Buttonen.Attributes["OnClick"] = "return confirm('您确定要重新导入英文字典吗？');";
    }
    protected void Buttonen_Click(object sender, EventArgs e)
    {
        string cip = Page.Request.UserHostAddress;//客户端IP
        string sip = LearnSite.Common.Computer.GetServerIp();//服务器IP
        if (cip == sip)
        {
            LearnSite.BLL.English ebll = new LearnSite.BLL.English();
            ebll.DeleteAll();
            System.Threading.Thread.Sleep(200);
            Labelmsg.Text = LearnSite.DBUtility.UpdateGrade.UpdateTableEnglish();
            Buttonen.Enabled = false;
        }
        else
        {
            Labelmsg.Text = "请在服务器上操作有效！";
        }
    }
}
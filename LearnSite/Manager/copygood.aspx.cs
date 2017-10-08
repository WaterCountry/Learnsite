using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manager_copygood : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeIsAdmin();
        if (!IsPostBack)
        {
            Btnbackup.Attributes["OnClick"] = "return confirm('您确定将所有推荐作品备份到GoodStore目录下吗？');";
        }
    }
    protected void Btnbackup_Click(object sender, EventArgs e)
    {
        DateTime dt1 = DateTime.Now;
        string msg = LearnSite.Common.BackupGood.Backup();
        DateTime dt2 = DateTime.Now;
        Labelmsg.Text = msg + "<br/>费时：" + LearnSite.Common.Computer.DatagoneMilliseconds(dt1, dt2) + "毫秒";
    }
}
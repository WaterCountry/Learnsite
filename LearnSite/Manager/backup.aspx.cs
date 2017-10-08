using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manager_backup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeIsAdmin();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "数据库备份页面";
            Btnbackup.Attributes.Add("OnClick", "document.getElementById('Textcmd').value='正在备份数据库中......'; document.getElementById('Loading').style.display= '';");
            showbackds();
        }
    }
    protected void Btnbackup_Click(object sender, EventArgs e)
    {
        Labelmsg.Text = LearnSite.DBUtility.DbBackup.BakupMyDb();
        showbackds();
    }

    private void showbackds()
    {
        DlDbBackup.DataSource = LearnSite.DBUtility.DbBackup.BackUpFileList();
        DlDbBackup.DataBind();
    }
    protected void DlDbBackup_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        HyperLink hl = new HyperLink();
        hl = (HyperLink)e.Item.FindControl("HLfname");
        string Wurl = ((Label)e.Item.FindControl("Labelurl")).Text;
        hl.NavigateUrl = "~/Plugins/download.aspx?Id=" + LearnSite.Common.EnDeCode.Encrypt( Wurl,"ls");
        ImageButton imgbtn = new ImageButton();
        imgbtn = (ImageButton)e.Item.FindControl("ImgBtnReStore");
        imgbtn.Attributes.Add("onclick", "return   confirm('您确定要将当前数据库恢复到该备份日期状态吗？');");
    }
    protected void DlDbBackup_ItemCommand(object source, DataListCommandEventArgs e)
    {
        string dbbackurla = e.CommandArgument.ToString();
        if (LearnSite.DBUtility.DbBackup.IsTodayBackUp())
        {
            string shortdbbackname = LearnSite.Common.WordProcess.getshortfname(dbbackurla);
            Labelmsg.Text = "你选择的备份文件为：" + shortdbbackname + " 操作结果：" + LearnSite.DBUtility.DbBackup.RestoreDb(dbbackurla);
            System.Threading.Thread.Sleep(500);
        }
        else
        {
            Labelmsg.Text = "你未备份今天的数据库，无法进行恢复操作！";
        }
    }
}
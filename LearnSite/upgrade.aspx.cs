using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UpGrade : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
            checkdatabase();
        else
            System.Threading.Thread.Sleep(1000);
    }
    protected void Btnupgrade_Click(object sender, EventArgs e)
    {
        if (LearnSite.DBUtility.UpdateGrade.TableExistCheck())
        {
            if (!LearnSite.DBUtility.UpdateGrade.VersionCheck())
            {
                Labelmsg.Text = "升级完毕，请删除本页面！以免数据库出错！";

                string ch = "数据库已经更新到最新版，点击跳回教师首页！";
                LearnSite.Common.CookieHelp.ClearTeacherCookies();
                LearnSite.Common.CookieHelp.ClearStudentCookies();//教师退出的话把本机模拟学生角色登录的学生平台也退出
                System.Threading.Thread.Sleep(200);
                LearnSite.Common.WordProcess.Alert(ch, this.Page);
                HttpRuntime.Close();//清除缓存，释放资源
                Response.Redirect("~/Teacher/index.aspx", false);
            }
            else
            {                
                string ch = "已经更新过了！请点击跳回教师首页！\n若要求运行一次则补丁已经修正！";
                LearnSite.Common.WordProcess.Alert(ch, this.Page);
                Response.Redirect("~/Teacher/index.aspx", false);
            }
        }
        else
        {
            string ch = "很抱歉您创建的数据库为空，不存在数据表，请仔细查看或请求帮助！";
            LearnSite.Common.WordProcess.Alert(ch, this.Page);
        }
    }

    private void checkdatabase()
    {
        if (!LearnSite.DBUtility.SqlHelper.DatabaseExist())//如果数据库不存在
        {
            Panel1.Visible = true;
            showPanel();
            Btnupgrade.Enabled = false;
            string msgstr = "";
            if (MasterDbExist(TextBoxSqlServer.Text, TextBoxDbUser.Text, TextBoxDbPwd.Text))
            {
                msgstr = "数据库服务名称、账号、密码正确！<br/><br/>如果数据库已创建，请在下面修改为正确数据库名称！<br/><br/>如果未创建新数据库，请填写你的新数据库名称！";
            }
            else
            {
                msgstr = "数据库服务名称、账号、密码不正确！请改修改正确！";
            }
            Labelmsg.Text = msgstr;
        }
        else
        {
            Panel1.Visible = false;
            Btnupgrade.Enabled = true;
            Labelmsg.Text = "数据库连接正常！第一次安装请点击上面执行更新，以便导入英文字典！";
        }
    }

    private void showPanel()
    {
        string myconnstr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlServer"].ConnectionString;
        string[] constr = LearnSite.DBUtility.DbLinkEdit.ReadSqlConfig(myconnstr);
        TextBoxSqlServer.Text = constr[0];
        TextBoxDbName.Text = constr[1];
        TextBoxDbUser.Text = constr[2];
        TextBoxDbPwd.Text = constr[3];
    }
    private bool MasterDbExist(string dbserver, string dbuser, string dbpwd)
    {
        string masterdb = "master";
        string masterConnstring = String.Format("Data Source={0};Initial Catalog={1};uid={2};pwd={3};", dbserver, masterdb, dbuser, dbpwd);
        return LearnSite.DBUtility.DbLinkEdit.DatabaseExist(masterConnstring);
    }
    protected void Buttonedit_Click(object sender, EventArgs e)
    {
        string dbserver = TextBoxSqlServer.Text;
        string dbname = TextBoxDbName.Text;
        string dbuser = TextBoxDbUser.Text;
        string dbpwd = TextBoxDbPwd.Text;
        if (dbserver != "" && dbname != "" && dbuser != "" && dbpwd != "")
        {
            string teststr = dbserver + dbname + dbuser + dbpwd;
            if (teststr.IndexOf(';') < 0 && teststr.IndexOf('=') < 0)
            {
                string namevalue = "SqlServer";
               // string ftpnamevalue = "Ftp";
               // string ftpdbname = dbname + ftpnamevalue;
                if (MasterDbExist(dbserver, dbuser, dbpwd))
                {
                    Buttonedit.Enabled = false;
                    LearnSite.DBUtility.DbLinkEdit.WriteSqlConfig(namevalue, dbserver, dbname, dbuser, dbpwd);
                   // LearnSite.DBUtility.DbLinkEdit.WriteSqlConfig(ftpnamevalue, dbserver, ftpdbname, dbuser, dbpwd);
                    string url = "~/UpGrade.aspx";
                    LearnSite.Common.WordProcess.Alert("Webconfig修改成功！", this.Page);
                    Response.Redirect(url, false);
                }
                else
                {
                    Labelmsg.Text = "数据库服务器名称、账号、密码填写可能错误！";
                }
            }
            else
            {
                Labelmsg.Text = "请不要填写非法字符，浪费时间！";
            }
        }
        else
        {
            Labelmsg.Text = "请填写正确的数据库服务器名称等！";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
public partial class Teacher_infomation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            verChecking();
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "教师基本信息页面";
            if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
            {
                Listmyclass();
                ShowTeacher();
            }
            else
            {
                Response.Redirect("~/Teacher/index.aspx", true);
            }
            ShowTimePass();
        }
    }

    protected void Btnlogout_Click(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            int Rhid = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString());
            LearnSite.BLL.Room bll = new LearnSite.BLL.Room();
            bll.UnlineClass(Rhid);//将所教的当前上课班级变为不上课
            LearnSite.Common.CookieHelp.ClearTeacherCookies();
            LearnSite.Common.CookieHelp.ClearStudentCookies();//教师退出的话把本机模拟学生角色登录的学生平台也退出
            LearnSite.Common.App.AppUserMatchRemove("s" + Rhid.ToString());//教师退出时移除全局变量中模拟学生
            LearnSite.Common.App.CurrentClassRemove(Rhid);//教师退出时移除全局变量中当前上课班级的学生
            Session.Abandon();//取消当前会话
            Session.RemoveAll();   
            Session.Clear();//清除当前浏览器进程所有session
            LearnSite.Common.Others.ClearClientPageCache();
            System.Threading.Thread.Sleep(300);
            string rurl = "~/Teacher/index.aspx?qt=" + DateTime.Now.Millisecond.ToString();
            Response.Redirect(rurl, false);
        }
    }

    private void Listmyclass()
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            int Rhid = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString());
            LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
            DLmyclass.DataSource = rm.GetMyClassList(Rhid);
            DLmyclass.DataBind();
        }
    }
    protected void DLmyclass_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        int r = 217;
        int g = 238;
        int b = 219;
        string str = ((Label)e.Item.FindControl("LabelRset")).Text;
        string reg = ((Label)e.Item.FindControl("LabelRreg")).Text;
        HyperLink hl = (HyperLink)e.Item.FindControl("HyperRgradeclass");
        if (str != "")
        {
            bool Rset = bool.Parse(str);
            if (Rset)
            {
                hl.BackColor = Color.FromArgb(r, g, b);
                hl.ToolTip = "正在上课班级";
            }
        }
        if (reg != "")
        {
            bool Rreg = bool.Parse(reg);
            if (Rreg)
            {
                hl.BorderColor = Color.LightSkyBlue;
                hl.ToolTip = hl.ToolTip + "(可注册)";
            }
        }
    }

    private void ShowTeacher()
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            Labelwelcome.Text = "欢迎回来，" + Server.UrlDecode(Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hnick"].ToString());
            Labelterm.Text = "第" + LearnSite.Common.XmlHelp.GetTerm() + "学期";
            if (!LearnSite.DBUtility.DbBackup.IsWeeksBackUp())
            {
                Labelmsg.Text = "<br/><br/>每周定时" + LearnSite.DBUtility.DbBackup.BakupMyDb();
            }
        }
    }
    /// <summary>
    /// 显示登录时间和过去时间
    /// </summary>
    private void ShowTimePass()
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
            bool Hpermiss = bool.Parse(Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hpermiss"].ToString());
            if (Hpermiss)
            {
                LearnSite.Common.CookieHelp.ClearTeacherCookies();
            }
            else
            {
                if (DLmyclass.Items.Count == 0)
                {
                    Labelmsg.Text = "该教师账号未选择任教班级，只可查阅学案列表！";
                    Labelmsg.ForeColor = System.Drawing.Color.Red;
                    Labelmsg.Font.Bold = true;
                }
                else
                {
                    if (Session[Hid + "teacherlogintime"] != null)
                    {
                        string LoginTime = Session[Hid + "teacherlogintime"].ToString();
                        DateTime time1 = DateTime.Parse(LoginTime);
                        DateTime time2 = DateTime.Now;
                        string msg = "您的登录时间是：" + LoginTime + " 已经过去" + LearnSite.Common.Computer.DatagoneMinute(time1, time2) + "分钟了！";
                        Labelmsg.Text = msg;
                    }
                }
            }

        }
    }
    private void verChecking()
    {
        if (!LearnSite.DBUtility.UpdateGrade.TableCheck())
        {
            string ch = "您的数据库未更新，现在将跳到更新程序UpGrade.aspx，请执行更新，不影响原有数据！";
            LearnSite.Common.WordProcess.Alert(ch, this.Page);
            Response.Redirect("~/UpGrade.aspx", false);
        }
    }
}

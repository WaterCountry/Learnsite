using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manager_upgrade : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeIsAdmin(); 
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "学年升级页面";
            Btnupgrade.Attributes["OnClick"] = "return confirm('您确定要执行学年升级吗？');document.getElementById('Textcmd').value='正在执行学年升级中......'; document.getElementById('Loading').style.display= '';";

            Textthisyear.Text = DateTime.Now.ToLongDateString();
        }
        if (Session["Upgraded"] != null)
        {
            Btnupgrade.Enabled = false;
            Labelmsg.Text = "您已经执行过学年升级操作了，注意请不要重复操作！";
        }
    }
    /// <summary>
    /// 先删除学生表中的毕业学生，再删除Ftp用户中的记录，再删除Signin表中的记录， 最后删除Web用户
    /// 学年升级（只是年级改变）不影响Ftp账号使用（因为Ftp账号是根据Syear,Sclass,Snum入学年份、班级、学号创建
    /// Ftp目录也是根据Syear,Sclass,Snum入学年份、班级、学号创建）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Btnupgrade_Click(object sender, EventArgs e)
    {
        LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
        if (rm.GradeCount() > 0)
        {            
                DateTime nowtime1 = DateTime.Now;
                LearnSite.BLL.Students st = new LearnSite.BLL.Students();
                if (st.GetCounts() > 0)
                {
                    st.Upgrade();//所有年级升一级，并删除班级表中不存在班级的学生
                    System.Threading.Thread.Sleep(1000);
                    LearnSite.BLL.Signin sg = new LearnSite.BLL.Signin();
                    sg.Upgrade();// 在签到表中删除学生表中不存在班级的学生
                    if (LearnSite.Ftp.FtpHelper.DatabaseExist())//如果ftp数据库存在的话
                    {
                        System.Threading.Thread.Sleep(1000);
                        LearnSite.Ftp.Reg.Upgrade();//ftp数据库中删除学生表中不存在学号的账号（根据已经清除过的学生表与网页表对比）                        
                    }
                    LearnSite.BLL.Webstudy ws = new LearnSite.BLL.Webstudy();
                    ws.Upgrade();//学年升级，删除Webstudy中学号不在Students的记录
                    DateTime nowtime2 = DateTime.Now;
                    Labelmsg.Text = "学年升级成功！ 用时" + LearnSite.Common.Computer.Datagone(nowtime1, nowtime2) + "秒";
                    Btnupgrade.Enabled = false;
                    LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
                    rbll.UpdateRhidNone();//清除班级选择
                    LearnSite.BLL.Courses bll = new LearnSite.BLL.Courses();
                    bll.HideCourse();//将所有发布的学案收回，处于未发布状态
                    LearnSite.Common.WordProcess.Alert("请给任课老师重新选择新学期任教班级!", this.Page);
                    Session["Upgraded"] = "Done";
                }
                else
                {
                    Labelmsg.Text = "没有学生";
                }
        }
        else
        {
            Labelmsg.Text = "班级未设置！";
        }
    }
   
}

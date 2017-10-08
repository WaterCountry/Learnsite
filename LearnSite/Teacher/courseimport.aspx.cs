using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_courseimport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showgrade();
        }
    }
    private void ListImportCourse()
    {
        string cids = LabelnewCids.Text;
        if (!string.IsNullOrEmpty(cids))
        {
            LearnSite.BLL.Courses cbll = new LearnSite.BLL.Courses();
            GVCourse.DataSource = cbll.getCidsCourses(cids);
            GVCourse.DataBind();
        }
    }
    protected void Btnimport_Click(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            int Hid = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString());
            if (Labelgrade.Text != "")
            {
                int cobj = Int32.Parse(Labelgrade.Text);
                string msg = "";
                DateTime nowtime1 = DateTime.Now;
                int res = LearnSite.Store.ImportCourse.PackageUpload(FudPackage, cobj, Hid);
                DateTime nowtime2 = DateTime.Now;
                if (res > 0)
                {
                    msg = "导入学案包成功！";
                    string cids=LabelnewCids.Text;
                    if (string.IsNullOrEmpty(cids))
                        LabelnewCids.Text = res.ToString();
                    else
                        LabelnewCids.Text = cids + "," + res.ToString();//将导入的学案新编号自动存放在隐藏标签里，以便学案列表
                    
                    ListImportCourse();//显示当前导入的学案列表
                }
                else
                {
                    switch (res)
                    {
                        case -1:
                            msg = "信息？";
                            break;
                        case -2:
                            msg = "已解压成功，但导入失败！";
                            break;
                        case -3:
                            msg = "学案包无内容，不能导入学案！";
                            break;
                        case -4:
                            msg = "不是LearnSite信息技术学习平台导出的学案包，请仔细查看！";
                            break;
                        case -5:
                            msg = "学案包不是rar或zip文件格式";
                            break;
                        case -6:
                            msg = "无上传学案包";
                            break;
                    }
                }
                Labelmsg.Text = msg + " 用时：" + LearnSite.Common.Computer.Datagone(nowtime1, nowtime2);
            }
        }
    }
    private void showgrade()
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
            if (Session[Hid + "importgrade"] != null)
                Labelgrade.Text = Session[Hid + "importgrade"].ToString();
        }
    }
    protected void Btnreturn_Click(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
            if (Labelgrade.Text != "")
                Session[Hid + "grade"] = Labelgrade.Text;
            Response.Redirect("~/Teacher/course.aspx", false);
        }
    }
}

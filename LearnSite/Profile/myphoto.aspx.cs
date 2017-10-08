using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Profile_myphoto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            CanUpload();
            if (!IsPostBack)
            {
                showphoto();
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }
    protected void Btnphoto_Click(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            string mynum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
            string mysex = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sex"].ToString();
            string rs = LearnSite.Common.Photo.PhotoUpload(PhotoFileUpload, mynum);
            string msg = "您好！发生了什么事？请告诉您的老师！";
            switch (rs)
            {
                case "0":
                    msg = "请选择你要上传的相片！";
                    break;
                case "1":
                    msg = "相片提交成功！";
                    ClearClientPageCache();
                    showphoto();
                    break;
                case "2":
                    msg = "相片提交成功！由于相片尺寸过大，自动缩小至320宽度！";
                    ClearClientPageCache();
                    showphoto();
                    break;
                case "3":
                    msg = "不是真实的图片格式，请仔细查看！（改后缀无效）";
                    break;
                case "4":
                    msg = "相片大小不能超过1024KB！";
                    break;
                case "5":
                    msg = "相片要求为gif或jpg图片格式！";
                    break;
            }
            LearnSite.Common.WordProcess.Alert(msg, this.Page);
        }
    }
    public void ClearClientPageCache()
    {
        LearnSite.Common.Others.ClearClientPageCache();
        System.Threading.Thread.Sleep(1000);//延时一秒
    }
    private void showphoto()
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            string mynum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
            string mysex = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sex"].ToString();
            string imgurl = LearnSite.Common.Photo.GetStudentPhotoUrl(mynum, mysex);
            Imageface.ImageUrl = imgurl + "?temp=" + DateTime.Now.Millisecond.ToString();
            ((Image)Master.FindControl("Imageface")).ImageUrl = imgurl;
        }
    }
    private void CanUpload()
    {
        int sclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
        int sgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
        LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
        if (rbll.GetRphotoedit(sgrade, sclass))
        {
            Btnphoto.Enabled = true;
        }
        else
        {
            Btnphoto.Enabled = false;
            Labelstr.Text = "限制修改相片";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_package : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
            {
                if (Request.QueryString["Cid"] != null)
                {
                    LabelCid.Text = Request.QueryString["Cid"].ToString();
                    int Cid = Int32.Parse(Request.QueryString["Cid"]);
                    LearnSite.BLL.Courses cbll = new LearnSite.BLL.Courses();
                    LabelCtitle.Text = cbll.GetTitle(Cid);//显示标题
                    ShowPackage();
                    showfilelist();
                }
            }
        }        
    }
    private void CreatePackage()
    {
        if (LabelCtitle.Text != "")
        {
            int Cid = Int32.Parse(Request.QueryString["Cid"]);
            if (LearnSite.Store.XmlCourse.CourseToXml(Cid))   //创建xml文件
            {
                //如果创建成功，则学案目录压缩打包
                LearnSite.Store.Package.ZipToPackageFile(Cid);
                Labelmsg.Text = "创建成功！";
            }
            else  //不成功，说明学案不存在
            {
                Labelmsg.Text = "创建失败，请查看学案是否存在或者设置网站根目录下的Store目录为everyone可读写！";
            }
        }
    }
    /// <summary>
    /// 检验学案包是否存在
    /// </summary>
    private void ShowPackage()
    {
        string Cid = LabelCid.Text ;
        if (LearnSite.Store.Package.PackageExists(Cid))
        {
            Labelinfo.Text = LearnSite.Store.Package.PachageAttrible(Cid);
            Panelinfo.Visible = true;
            BtnZip.Text = "重打";
        }
        else
        {
            Panelinfo.Visible = false;
        }
    }
    protected void Btnreturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Teacher/course.aspx", false);
    }

    protected void Btndown_Click(object sender, EventArgs e)
    {
        string Cid = LabelCid.Text;
        string CtitelFileName = HttpUtility.UrlDecode(LabelCtitle.Text.Replace(" ", "")) + ".zip";//要去空格，不然firefox下载文件名和后缀不全
        string myurl = "~/Package/" + Cid + ".cs";
        LearnSite.Common.FileDown.DownPackageFile(myurl, CtitelFileName);//学案包专用下载输出
    }
    protected void BtnZip_Click(object sender, EventArgs e)
    {
        CreatePackage();
        ShowPackage();
        showfilelist();
        BtnZip.Enabled = false;
        string xmlurl = "~/Package/read.xml";
        string xmlpath = Server.MapPath(xmlurl);
        System.IO.File.Delete(xmlpath);//先删除，不存在不引发异常
        LearnSite.BLL.Courses cbll = new LearnSite.BLL.Courses();
        cbll.CreatPackageNameList(xmlpath);//生成学案包目录对照表
    }

    private void showfilelist()
    {
        string cidDir = "~/Store/"+LabelCid.Text;
        Dlfilelist.DataSource = LearnSite.DBUtility.DbBackup.FileList(cidDir);
        Dlfilelist.DataBind();    
    }

    protected void Dlfilelist_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        HyperLink hl = new HyperLink();
        hl = (HyperLink)e.Item.FindControl("HLfname");
        string Wurl = ((Label)e.Item.FindControl("Labelurl")).Text;
        hl.NavigateUrl = "~/Plugins/download.aspx?Id=" + LearnSite.Common.EnDeCode.Encrypt(Wurl, "ls");
    }
}

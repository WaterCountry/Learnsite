using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_workpackage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
            {
                GradeClass();
                getSyear();
                ShowCid();
                showpag();
            }
            else
            {
                Button1.Enabled = false;
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string spath = "~/homework/";
        string syear = Labelyear.Text;
        string sgrade = DDLgrade.SelectedValue;
        string sclass = DDLclass.SelectedValue;
        string cid = DDLCid.SelectedValue;
        if (syear != "" && sgrade != "" && sclass != "" && cid != "")
        {
            string cidworkpath = spath + syear + "/" + sgrade + "/" + sclass;
            string filename = syear + "_" + sgrade + "_" + sclass + "_" + cid;
            string cidworkpag = cidworkpath + "/" + filename + ".rar";
            string rpath = Server.MapPath(cidworkpag);
            if (System.IO.File.Exists(rpath))
            {
                System.IO.File.Delete(rpath);
            }
            string cdir = Server.MapPath(cidworkpath + "/" + cid);
            if (System.IO.Directory.Exists(cdir))
            {
                string savedir = Server.MapPath(cidworkpath);
                LearnSite.Store.SharpZip.PackFiles(rpath, cdir);
                System.Threading.Thread.Sleep(30000);
                LearnSite.Common.WordProcess.Alert("打包完毕，点击确认！", this.Page);
                showpag();
            }
        }
    }

    private void ShowCid()
    {
        string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
        string cterm = LearnSite.Common.XmlHelp.GetTerm();
        string dgrade = DDLgrade.SelectedValue;
        if (dgrade != "")
        {
            LearnSite.BLL.Courses cbll = new LearnSite.BLL.Courses();
            DDLCid.DataSource = cbll.ShowCidCtitle(Int32.Parse(Hid),Int32.Parse(dgrade), Int32.Parse(cterm));
            DDLCid.DataTextField = "Ctitle";
            DDLCid.DataValueField = "Cid";
            DDLCid.DataBind();
        }
    }

    private void GradeClass()
    {
        string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
        
        LearnSite.BLL.Room room = new LearnSite.BLL.Room();
        DDLgrade.DataSource = room.GetGrade();
        DDLgrade.DataTextField = "Rgrade";
        DDLgrade.DataValueField = "Rgrade";
        DDLgrade.DataBind();
        if (Session[Hid + "grade"] != null)
        {
            DDLgrade.SelectedValue = Session[Hid + "grade"].ToString();
        }
        int Rgrade = Int32.Parse(DDLgrade.SelectedValue);
        LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
        DDLclass.DataSource = rm.GetLimitClass(Rgrade);
        DDLclass.DataTextField = "Rclass";
        DDLclass.DataValueField = "Rclass";
        DDLclass.DataBind();
        if (Session[Hid + "class"] != null)
        {
            DDLclass.SelectedValue = Session[Hid + "class"].ToString();
        }
    }
    protected void DDLgrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowCid();
        getSyear();
        if (DDLgrade.SelectedValue != null)
        {
            int Rgrade = Int32.Parse(DDLgrade.SelectedValue);
            LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
            DDLclass.DataSource = rm.GetLimitClass(Rgrade);
            DDLclass.DataBind();
        }
        showpag();
    }
    protected void DDLclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        showpag();
    }

    private void getSyear()
    {
        string dgrade = DDLgrade.SelectedValue;
        if (dgrade != "")
        {
            LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
            Labelyear.Text= sbll.GetYear(Int32.Parse(dgrade));           
        }
    }
    private void showpag()
    { 
        string spath="~/homework/";
        string syear = Labelyear.Text;
        string sgrade = DDLgrade.SelectedValue;
        string sclass = DDLclass.SelectedValue;
        string cid=DDLCid.SelectedValue;
        if (syear != "" && sgrade != "" && sclass != "" && cid!="")
        {
            string cidworkpath = spath + syear + "/" + sgrade + "/" + sclass;
            string filename = syear + "_" + sgrade + "_" + sclass + "_" + cid;
            string cidworkpag = cidworkpath + "/" + filename + ".rar";
            string rpath=Server.MapPath(cidworkpag);
            if (System.IO.File.Exists(rpath))
            {
                HyperLink1.Text = DDLCid.SelectedItem.Text + ".rar";
                HyperLink1.NavigateUrl = cidworkpag;
                System.IO.FileInfo fi = new System.IO.FileInfo(rpath);
                
                Labelmsg.Text = "文件大小："+(fi.Length/1024).ToString()+"kb   打包日期："+fi.LastWriteTime.ToString();
            }
            else
            {
                HyperLink1.Text = "";
                HyperLink1.NavigateUrl = "";
                Labelmsg.Text = "未打包";
            }
        }
    }
    protected void DDLCid_SelectedIndexChanged(object sender, EventArgs e)
    {
        showpag();
    }
}
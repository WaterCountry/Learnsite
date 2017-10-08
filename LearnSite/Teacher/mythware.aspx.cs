using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Teacher_mythware : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "极域班级模型ClassModel";
            showXmlList();
        }
    }
    protected void BtnBuild_Click(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            string hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();//教师编号
            string pm = TextBoxRoom.Text;
            if (string.IsNullOrEmpty(pm))
            {
                Labelmsg.Text = "请输入电脑室名称！";
            }
            else
            {
                if (FuClassModel.HasFile)
                {
                    string savepath = LearnSite.Common.ClassModel.ClassModeSavePath(hid);
                    string xmlpath = LearnSite.Common.ClassModel.SaveClassModelXml(FuClassModel, hid, savepath);
                    if (!string.IsNullOrEmpty(xmlpath))
                    {
                            DateTime dt1 = DateTime.Now;
                            int ipCount= LearnSite.Common.ClassModel.SetIpxy(xmlpath, pm);//读取班级模型中ＩＰ的x和y坐标到计算机表中
                            if (CkMachine.Checked)
                            {
                                LearnSite.Common.ClassModel.PreMachineName(xmlpath);
                                System.Threading.Thread.Sleep(200);//预处理为主机名
                            }
                            int weeks = Int32.Parse(DDLmonth.SelectedValue);
                            XmlDocument xmldoc = LearnSite.Common.ClassModel.ReadXml(xmlpath);
                            string msg = LearnSite.Common.ClassModel.SetAllStuName(Int32.Parse(hid), xmldoc, savepath, weeks, xmlpath);
                            DateTime dt2 = DateTime.Now;
                            Labelmsg.Text = "读取极域模型中电脑IP数共："+ipCount.ToString() + "台，费时：" + LearnSite.Common.Computer.DatagoneMilliseconds(dt1, dt2) + "毫秒<br>"+msg;
                            showXmlList();
                    }
                }
                else
                {
                    Labelmsg.Text = "请选择你原有的班级模型xml或cls格式文件！";
                }
            }
        }
        else
        {
            string ch = "请登录后执行操作！";
            LearnSite.Common.WordProcess.Alert(ch, this.Page);
        }
    }

    private void showXmlList()
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            string hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();//教师编号
            string savepath = LearnSite.Common.ClassModel.ClassModeSavePath(hid);
            System.Threading.Thread.Sleep(100);
            string newdir = LearnSite.DBUtility.DbBackup.GetLastDir(savepath);
            if (!string.IsNullOrEmpty(savepath) && !string.IsNullOrEmpty(newdir))
            {
                string fdir = "~/ClassModel/" + hid + "/" + newdir;
                Dlfilelist.DataSource = LearnSite.DBUtility.DbBackup.FileList(fdir);//虚拟目录
                Dlfilelist.DataBind();
                Labeldirhid.Text = savepath;
                Labeldir.Text = fdir;
            }
        }
    }
    protected void ImgBtnDown_Click(object sender, ImageClickEventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            string hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();//教师编号

            string rarpath = Labeldirhid.Text;
            string rardir = Labeldir.Text;
            string rardirpath =Server.MapPath(rardir);
            if (!string.IsNullOrEmpty(rarpath) && !string.IsNullOrEmpty(rardir))
            {
                string downrarpath = rarpath + @"\ClassMode" + hid + ".zip";
                System.IO.File.Delete(downrarpath);//不在不引发异常
                System.Threading.Thread.Sleep(100);
                if (System.IO.Directory.Exists(rardirpath))
                {
                    LearnSite.Store.SharpZip.PackFiles(downrarpath, rardirpath);
                    string url = "~/ClassModel/" + hid + "/ClassMode" + hid + ".zip";
                    LearnSite.Common.FileDown.DownLoadold(url);
                }
            }
        }
    }
}
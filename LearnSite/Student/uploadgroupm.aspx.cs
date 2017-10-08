using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LitJson;

public partial class Student_uploadgroupm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname] != null)
        {
            uploadgroupwork();
        }
        else
        {
            showError("你没有权限!");
        }
    }

    private void uploadgroupwork()
    {
        HttpPostedFile group_upload = Request.Files["imgFilegroup"];
        int maxSize = 104857600;//定义上传最大值为100MB

        if (group_upload != null)
        {
            // Get the data
            string Gtype = group_upload.FileName.Substring(group_upload.FileName.LastIndexOf(".") + 1).ToLower(); 
            string Gmid = Request.QueryString["mid"].ToString();
            string Gnum = Request.QueryString["num"].ToString();
            LearnSite.BLL.Mission mbll = new LearnSite.BLL.Mission();
            LearnSite.Model.Mission mmodel = new LearnSite.Model.Mission();
            mmodel = mbll.GetModel(Int32.Parse(Gmid));
            string Gextention = mmodel.Mfiletype;
            string Gcid = mmodel.Mcid.ToString();
            string limitext = Gextention;//初始化，随意
            switch (Gextention)
            {
                case "doc":
                    limitext = "*.doc;*.docx";
                    break;
                case "ppt":
                    limitext = "*.ppt;*.pptx";
                    break;
                case "xls":
                    limitext = "*.xls;*.xlsx";
                    break;
                case "office":
                    limitext = "*.doc;*.docx;*.ppt;*.pptx;*.xls;*.xlsx";
                    break;
                case "sb":
                    limitext = "*.sb;*.sb2";
                    break;
                default:
                    limitext = "*." + Gextention;
                    break;
            }
            if (Gtype == Gextention || limitext.Contains(Gtype))
            {
                if (group_upload.InputStream != null || group_upload.InputStream.Length < maxSize)
                {
                    LearnSite.BLL.Signin sn = new LearnSite.BLL.Signin();
                    LearnSite.Model.Signin qmodel = sn.GetModelm(Gnum);
                    string Ggroup = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString();//取组长
                    string Gyear = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Syear"].ToString();
                    string Ggrade = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString();
                    string Gclass = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString();
                    string Gip = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["LoginIp"].ToString();
                    string LoginTime = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["LoginTime"].ToString();
                    string Gterm = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["ThisTerm"].ToString();

                    if (qmodel != null)
                    {
                        Gyear = qmodel.Qsyear.ToString();
                        Ggrade = qmodel.Qgrade.ToString();
                        Gclass = qmodel.Qclass.ToString();
                        Gip = qmodel.Qip;
                        LoginTime = qmodel.Qdate.ToString();
                        Gterm = qmodel.Qterm.ToString();
                    }


                    int Glengh = group_upload.ContentLength;
                    DateTime Gdate = DateTime.Now;


                    LearnSite.BLL.GroupWork gbll = new LearnSite.BLL.GroupWork();
                    bool gdone = gbll.DoneGroupWork(Gnum, Int32.Parse(Gmid));
                    string MySavePath = LearnSite.Common.WorkUpload.GetWurl(Gyear, Ggrade, Gclass, Gcid, Gmid);//获得作品保存路径（如果不存在，自动创建）

                    string NewFileName = "g" + Gnum + Gcid + "_" + Gmid + "." + Gtype;
                    string Gurl = MySavePath + "/" + NewFileName;
                    string saveFilename = Server.MapPath(Gurl);

                    //如果作品未提交，提交作品 
                    if (!gdone)
                    {
                        int Gtime = LearnSite.Common.Computer.TimePassed();
                        LearnSite.Model.GroupWork gmodel = new LearnSite.Model.GroupWork();
                        gmodel.Gcheck = false;
                        gmodel.Gcid = Int32.Parse(Gcid);
                        gmodel.Gclass = Int32.Parse(Gclass);
                        gmodel.Gdate = DateTime.Now;
                        gmodel.Gfilename = NewFileName;
                        gmodel.Ggrade = Int32.Parse(Ggrade);
                        gmodel.Ghit = 0;
                        gmodel.Gip = Gip;
                        gmodel.Glengh = Glengh;
                        gmodel.Gmid = Int32.Parse(Gmid);
                        gmodel.Gnote = "";
                        gmodel.Gnum = Gnum;
                        gmodel.Grank = -1;
                        gmodel.Gscore = 0;
                        LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
                        gmodel.Gstudents = sbll.GroupSnum(Gnum);
                        gmodel.Gterm = Int32.Parse(Gterm);
                        gmodel.Gtime = Gtime;
                        gmodel.Gtype = Gtype;
                        gmodel.Gurl = Gurl;
                        gmodel.Gvote = 0;
                        gmodel.Ggroup = Int32.Parse(Ggroup);
                        gbll.Add(gmodel);//添加小组作品提交记录 
                    }
                    else
                    {
                        //已交则不更新记录
                    }
                    group_upload.SaveAs(saveFilename);//保存或更新提交作品
                    Hashtable hash = new Hashtable();
                    hash["error"] = 0;
                    Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                    Response.Write(JsonMapper.ToJson(hash));
                    Response.End();
                }
                else
                {
                    showError("选择的文件大小超过限制!(最大为10MB)");
                }
            }
            else
            {
                showError("选择的文件类型错误!");
            }
        }
        else
        {
            showError("请选择文件!");
        }
    }

    private void showError(string message)
    {
        Hashtable hash = new Hashtable();
        hash["error"] = 1;
        hash["message"] = message;
        Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
        Response.Write(JsonMapper.ToJson(hash));
        Response.End();
    }
}
using System;
using System.Collections;
using System.Web;
using LitJson;

public partial class Student_uploadworkm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname] != null)
        {
            uploadthiswork();
        }
        else
        {
            showError("你没有权限!");
        }
    }

    private void uploadthiswork()
    {
        HttpPostedFile work_upload = Request.Files["imgFile"];
        int maxSize = 104857600;//定义上传最大值为100MB

        if (work_upload != null)
        {
            string Wmid = Request.QueryString["mid"].ToString();
            string Wnum = Request.QueryString["num"].ToString();
            LearnSite.BLL.Mission mbll = new LearnSite.BLL.Mission();
            LearnSite.Model.Mission mmodel = new LearnSite.Model.Mission();
            mmodel = mbll.GetModel(Int32.Parse(Wmid));

            string Wcid = mmodel.Mcid.ToString();
            string Wmsort = mmodel.Msort.ToString();
            string Wfiletype = work_upload.FileName.Substring(work_upload.FileName.LastIndexOf(".") + 1).ToLower();
            string Wextention = mmodel.Mfiletype;              
            string limitext=Wextention;//初始化，随意
            switch (Wextention)
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
                        limitext = "*." + Wextention;
                        break;
                }
            if (Wfiletype == Wextention||limitext.Contains(Wfiletype))
            {
                if (work_upload.InputStream != null || work_upload.InputStream.Length < maxSize)
                {
                    int Wlength = work_upload.ContentLength;
                    string Syear = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Syear"].ToString();
                    string Sgrade = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString();
                    string Sclass = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString();
                    string Wsid = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString();
                    string Wip = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["LoginIp"].ToString();
                    string Sname = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sname"].ToString();
                    string Wterm = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["ThisTerm"].ToString();
                    DateTime Wdate = DateTime.Now;
                    bool checkcan = true;

                    LearnSite.BLL.Works ws = new LearnSite.BLL.Works();
                    string Wid = ws.WorkDone(Wnum, Int32.Parse(Wcid), Int32.Parse(Wmid));//返回空字符表示不存在该记录
                    if (Wid != "")
                    {
                        if (!ws.IsChecked(Int32.Parse(Wid)))
                        {
                            //如果未评价，则重新提交修改作品
                            string MySavePath = LearnSite.Common.WorkUpload.GetWurl(Syear, Sgrade, Sclass, Wcid, Wmid);//获得作品保存路径（如果不存在，自动创建）
                            string RndTime = LearnSite.Common.WordProcess.GetRandomNum(99).ToString();
                            string OnlyFileName = Wnum + "_" + Wcid + "_" + Wmid + "_" + RndTime;
                            string NewFileName = OnlyFileName + "." + Wfiletype;
                            string Wurl = MySavePath + "/" + NewFileName;
                            string resaveFilename = Server.MapPath(Wurl);
                            ws.UpdateWorkUp(Int32.Parse(Wid), Wurl, NewFileName, Wlength, Wdate, checkcan,"");//更新Wfilename, Wurl,Wlength, Wdate
                            LearnSite.BLL.Signin sn = new LearnSite.BLL.Signin();
                            sn.UpdateQwork(Int32.Parse(Wsid), Int32.Parse(Wcid));//更新今天签到表中的作品数量                            
                            work_upload.SaveAs(resaveFilename);//保存提交作品 

                            Hashtable hash = new Hashtable();
                            hash["error"] = 0;
                            Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                            Response.Write(JsonMapper.ToJson(hash));
                            Response.End();
                        }
                        else
                        {
                            showError("老师已经评价了!");
                        }
                    }
                    else
                    {
                        //如果作品未提交，提交作品(Wnum, Wcid,Wmid,Wmsort, Wfilename, Wurl,Wlength, Wdate, Wip, Wtime)                            
                        string MySavePath = LearnSite.Common.WorkUpload.GetWurl(Syear, Sgrade, Sclass, Wcid, Wmid);//获得作品保存路径（如果不存在，自动创建）
                        string RndTime = LearnSite.Common.WordProcess.GetRandomNum(99).ToString();
                        string OnlyFileName = Wnum + "_" + Wcid + "_" + Wmid + "_" + RndTime;
                        string NewFileName = OnlyFileName + "." + Wfiletype;
                        string Wurl = MySavePath + "/" + NewFileName;
                        string Wtime = LearnSite.Common.Computer.TimePassed().ToString();

                        LearnSite.Model.Works wmodel = new LearnSite.Model.Works();
                        wmodel.Wnum = Wnum;
                        wmodel.Wcid = Int32.Parse(Wcid);
                        wmodel.Wmid = Int32.Parse(Wmid);
                        wmodel.Wmsort = Int32.Parse(Wmsort);
                        wmodel.Wfilename = NewFileName;
                        wmodel.Wtype = Wextention;
                        wmodel.Wurl = Wurl;
                        wmodel.Wlength = Wlength;
                        wmodel.Wdate = Wdate;
                        wmodel.Wip = Wip;
                        wmodel.Wtime = Wtime;
                        wmodel.Wcan = checkcan;
                        wmodel.Wcheck = false;
                        wmodel.Wegg = 12;//设定票数为12张
                        wmodel.Whit = 0;
                        wmodel.Wgrade = Int32.Parse(Sgrade);
                        wmodel.Wterm = Int32.Parse(Wterm);
                        wmodel.Wsid = Int32.Parse(Wsid);
                        wmodel.Wclass = Int32.Parse(Sclass);
                        wmodel.Wname = HttpUtility.UrlDecode(Sname);
                        wmodel.Wyear = Int32.Parse(Syear);

                        switch (Wfiletype)
                        {
                            case "doc":
                            case "ppt":
                            case "xls":
                            case "docx":
                            case "pptx":
                            case "xlsx":
                            case "wps":
                            case "dps":
                            case "et":
                                wmodel.Woffice = true;
                                break;
                            default:
                                wmodel.Woffice = false;
                                break;
                        }
                        wmodel.Wflash = false;
                        wmodel.Werror = false;
                        string saveFilename = Server.MapPath(Wurl);
                        ws.AddWorkUp(wmodel);//添加作品提交记录
                        LearnSite.BLL.Signin sn = new LearnSite.BLL.Signin();
                        sn.UpdateQwork(Int32.Parse(Wsid), Int32.Parse(Wcid));//更新今天签到表中的作品数量

                        work_upload.SaveAs(saveFilename);//保存提交作品
                        Hashtable hash = new Hashtable();
                        hash["error"] = 0;

                        Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                        Response.Write(JsonMapper.ToJson(hash));
                        Response.End();
                    }
                }
                else
                {
                    showError("选择的文件大小超过限制!(最大为100MB)");
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
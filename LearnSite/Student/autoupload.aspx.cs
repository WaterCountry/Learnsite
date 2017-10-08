using System;
using System.Collections.Generic;
using System.Web;
using System.Collections;
using LitJson;

public partial class Student_autoupload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname] != null)
        {
            uploadautonomic();
        }
        else
        {
            showError("你没有权限!");
        }
    }

    private void uploadautonomic()
    {
        HttpPostedFile work_upload = Request.Files["imgFile"];
        int maxSize = 10485760;//定义上传最大值为10MB

        if (work_upload != null)
        {
            int ayid = Int32.Parse(Request.QueryString["yid"].ToString());
            int afid = Int32.Parse(Request.QueryString["fid"].ToString());
            int asid = Int32.Parse(Request.QueryString["sid"].ToString());

            string atype = work_upload.FileName.Substring(work_upload.FileName.LastIndexOf(".") + 1).ToLower();
            string limitext = LearnSite.Common.XmlHelp.GetTypeName("WorksType");

            if (limitext.Contains(atype))
            {
                if (work_upload.InputStream != null || work_upload.InputStream.Length < maxSize)
                {
                    int alength = work_upload.ContentLength;
                    string ayear = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Syear"].ToString();
                    string agrade = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString();
                    string aclass = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString();
                    string anum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
                    string aip = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["LoginIp"].ToString();
                    string aname = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sname"].ToString();
                    string aterm = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["ThisTerm"].ToString();
                    DateTime adate = DateTime.Now;
                    //bool checkcan = true;
                    /// Asid,Anum,Aname,Ayid,Afid,Atype,Afilename,Aurl,Alength,Ascore,Adate,Aip,Ayear,Agrade,Aclass,Aterm,Aoffice
                    LearnSite.BLL.Autonomic abll = new LearnSite.BLL.Autonomic();
                    int aid = abll.Exists(asid, afid);//返回空字符表示不存在该记录
                    if (aid > 0)
                    {
                        if (!abll.ExistsCheck(aid))
                        {
                            //如果未评价，则重新提交修改作品Atype Afilename Aurl Alength Adate Aflash Aid                            
                            string MySavePath = LearnSite.Common.WorkUpload.GetAurl(asid);//获得作品保存路径（如果不存在，自动创建）
                            string RndTime = LearnSite.Common.WordProcess.GetRandomNum(99).ToString();
                            string OnlyFileName = anum + "_" + afid.ToString() + "_" + RndTime;
                            string NewFileName = OnlyFileName + "." + atype;
                            string aurl = MySavePath + "/" + NewFileName;
                            string resaveFilename = Server.MapPath(aurl);
                            abll.UpdateAutonomic(atype, NewFileName, aurl, alength, adate, false, aid);
                            work_upload.SaveAs(resaveFilename);//保存提交作品 
                            showSuccess("重新提交作品成功！");
                        }
                        else
                        {
                            showError("老师已评分!");
                        }
                    }
                    else
                    {
                        //如果作品未提交Asid,Anum,Aname,Ayid,Afid,Atype,Afilename,Aurl,Alength,Adate,Aip,Ayear,Agrade,Aclass,Aterm,Aoffice                           
                        string MySavePath = LearnSite.Common.WorkUpload.GetAurl(asid);//获得作品保存路径（如果不存在，自动创建）
                        string RndTime = LearnSite.Common.WordProcess.GetRandomNum(99).ToString();
                        string OnlyFileName = anum + "_" + afid.ToString() + "_" + RndTime;
                        string NewFileName = OnlyFileName + "." + atype;
                        string aurl = MySavePath + "/" + NewFileName;
                        string resaveFilename = Server.MapPath(aurl);

                        LearnSite.Model.Autonomic amodel = new LearnSite.Model.Autonomic();
                        amodel.Asid = asid;
                        amodel.Anum = anum;
                        amodel.Aname = HttpUtility.UrlDecode(aname);
                        amodel.Ayid = ayid;
                        amodel.Afid = afid;
                        amodel.Atype = atype;
                        amodel.Afilename = NewFileName;
                        amodel.Aurl = aurl;
                        amodel.Alength = alength;
                        amodel.Adate = adate;
                        amodel.Aip = aip;
                        amodel.Ayear = Int32.Parse(ayear);
                        amodel.Agrade = Int32.Parse(agrade);
                        amodel.Aclass = Int32.Parse(aclass);
                        amodel.Aterm = Int32.Parse(aterm);
                        switch (atype)
                        {
                            case "doc":
                            case "ppt":
                            case "xls":
                            case "docx":
                            case "pptx":
                            case "xlsx":
                            case "wps":
                            case "wpp":
                            case "et":
                                amodel.Aoffice = true;
                                break;
                            default:
                                amodel.Aoffice = false;
                                break;
                        }
                        amodel.Aflash = false;
                        amodel.Aerror = false;

                        string saveFilename = Server.MapPath(aurl);
                        abll.AddAutonomic(amodel);

                        work_upload.SaveAs(saveFilename);//保存提交作品
                        showSuccess("保存作品成功！");
                    }
                }
                else
                {
                    showError("选择的文件大小超过限制!(最大为10MB)");
                }
            }
            else
            {
                showError("选择的文件类型不允许!");
            }
        }
        else
        {
            showError("请选择文件!");
        }
    }
    private void showSuccess(string message)
    {
        Hashtable hash = new Hashtable();
        hash["error"] = 0;
        hash["message"] = message;
        Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
        Response.Write(JsonMapper.ToJson(hash));
        Response.End();
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
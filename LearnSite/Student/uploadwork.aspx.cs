using System;
using System.Collections.Generic;
using System.Web;

public partial class Student_uploadwork : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        uploadthiswork();
    }

    private void uploadthiswork()
    {
        if (Request.Files["Filedata"] != null)
        {
            try
            {
                HttpPostedFile work_upload = Request.Files["Filedata"];
                string Wmid = Request.QueryString["mid"].ToString();
                string Wnum = Request.QueryString["num"].ToString();
                string info = HttpUtility.UrlDecode(Request.QueryString["info"].ToString());
                LearnSite.BLL.Mission mbll = new LearnSite.BLL.Mission();
                LearnSite.Model.Mission mmodel = new LearnSite.Model.Mission();
                mmodel = mbll.GetModel(Int32.Parse(Wmid));

                string Wcid = mmodel.Mcid.ToString();
                string Wmsort = mmodel.Msort.ToString();
                string Wfiletype = work_upload.FileName.Substring(work_upload.FileName.LastIndexOf(".") + 1).ToLower(); 
               // string Wextention = mmodel.Mfiletype;
                int Wlength = work_upload.ContentLength;
                //Syear | Sgrade | Sclass | Sid | Sname | Wip | Sterm | LoginTime
                string[] infoarray = info.Split('|');

                string Syear = infoarray[0];
                string Sgrade = infoarray[1];
                string Sclass = infoarray[2];
                string Wsid = infoarray[3];
                string Sname = infoarray[4];
                string Wip = infoarray[5];
                string Wterm = infoarray[6];
                string Logintime = infoarray[7];

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
                        try
                        {
                            ws.UpdateWorkUp(Int32.Parse(Wid), Wurl, NewFileName, Wlength, Wdate, checkcan,"");//更新Wfilename, Wurl,Wlength, Wdate

                            //LearnSite.BLL.Signin sn = new LearnSite.BLL.Signin();
                            //sn.UpdateQwork(Int32.Parse(Wsid), Int32.Parse(Wcid));//更新今天签到表中的作品数量
                            work_upload.SaveAs(resaveFilename);//保存提交作品 
                            Response.StatusCode = 200;
                            Response.Write(NewFileName);
                        }
                        catch (Exception ex)
                        {
                            Response.StatusCode = 200;
                            Response.Write(ex);
                        }
                        finally
                        {
                            Response.End();
                        }
                    }
                    else
                    {
                        Response.StatusCode = 200;
                        Response.Write("老师已经评价了!");
                        Response.End();
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
                    string Wtime = LearnSite.Common.Computer.TimePassed(Logintime).ToString();

                    LearnSite.Model.Works wmodel = new LearnSite.Model.Works();
                    wmodel.Wnum = Wnum;
                    wmodel.Wcid = Int32.Parse(Wcid);
                    wmodel.Wmid = Int32.Parse(Wmid);
                    wmodel.Wmsort = Int32.Parse(Wmsort);
                    wmodel.Wfilename = NewFileName;
                    wmodel.Wtype = Wfiletype;
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
                    try
                    {
                        ws.AddWorkUp(wmodel);//添加作品提交记录
                        LearnSite.BLL.Signin sn = new LearnSite.BLL.Signin();
                        sn.UpdateQwork(Int32.Parse(Wsid), Int32.Parse(Wcid));//更新今天签到表中的作品数量

                        work_upload.SaveAs(saveFilename);//保存提交作品
                        Response.StatusCode = 200;
                        Response.Write(NewFileName);
                    }
                    catch (Exception ex)
                    {
                        Response.StatusCode = 200;
                        Response.Write(ex);
                    }
                    finally
                    {
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = 200;
                Response.Write(ex);
            }
            finally
            {
                Response.End();
            }
        }
    }
}
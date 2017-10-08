using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_uploadgroup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        uploadgroupwork();
    }

    private void uploadgroupwork()
    {
        if (Request.Files["Filedata"] != null)
        {
            try
            {
                // Get the data
                HttpPostedFile group_upload = Request.Files["Filedata"];
                string Gtype = group_upload.FileName.Substring(group_upload.FileName.LastIndexOf(".") + 1).ToLower();
                string Gmid = Request.QueryString["mid"].ToString();
                string Gnum = Request.QueryString["num"].ToString();
                string info = HttpUtility.UrlDecode(Request.QueryString["info"].ToString());

                LearnSite.BLL.Mission mbll = new LearnSite.BLL.Mission();
                LearnSite.Model.Mission mmodel = new LearnSite.Model.Mission();
                mmodel = mbll.GetModel(Int32.Parse(Gmid));

               // string Gextention = mmodel.Mfiletype;
                string Gcid = mmodel.Mcid.ToString();

                //Syear | Sgrade | Sclass | Sid | Sname | Wip | Sterm | LoginTime
                string[] infoarray = info.Split('|');
                string Ggroup = infoarray[3];//取组长
                string Gyear = infoarray[0];
                string Ggrade = infoarray[1];
                string Gclass = infoarray[2];
                string Gip = infoarray[5];
                string LoginTime = infoarray[7];
                string Gterm = infoarray[6];
                
                LearnSite.BLL.Signin sn = new LearnSite.BLL.Signin();
                LearnSite.Model.Signin qmodel = sn.GetModelm(Gnum);
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
                try
                {
                    group_upload.SaveAs(saveFilename);//保存提交作品
                    Response.StatusCode = 200;
                    Response.Write(NewFileName);
                }
                catch (Exception ex)
                {
                    Response.StatusCode = 200;
                    Response.Write(ex);
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
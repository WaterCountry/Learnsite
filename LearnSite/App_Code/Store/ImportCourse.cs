using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
namespace LearnSite.Store
{
    /// <summary>
    ///ImportCourse 的摘要说明
    /// </summary>
    public class ImportCourse
    {
        public ImportCourse()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 保存上传学案包，获得保存文件名及物理路径
        /// </summary>
        /// <param name="fudpackage"></param>
        /// <returns></returns>
        public static int PackageUpload(FileUpload fudpackage, int cobj, int Hid)
        {
            int msg = -1;
            string uploadfile = fudpackage.PostedFile.FileName;
            string saveFile = "";
            if (uploadfile != "")
            {
                string fileName = uploadfile.Substring(uploadfile.LastIndexOf("\\") + 1);
                string fileType = fileName.Substring(fileName.LastIndexOf(".") + 1);
                string ftl = fileType.ToLower();
                if (ftl == "rar" || ftl == "zip")
                {
                    string rnd = DateTime.Now.Millisecond.ToString();
                    string savePath = CreateTempPath();//保存物理路径
                    saveFile = Checkbdir(savePath) + "file_" + rnd + "." + ftl;//保存文件名物理路径
                    fudpackage.SaveAs(saveFile);//按随机文件名保存
                    if (LearnSite.Store.SharpZip.UnpackFilesXml(saveFile, savePath)) //将上传的学案包中的xml文件解压到当前文件夹下                      
                    {
                        msg = -2;
                        int newCid = ImportXml(savePath, cobj, Hid); //从当前文件夹读取 xml文件，将学案导入到数据库中（自动修改学案内容的链接地址）
                        if (newCid != -1)    //再创建新Cid文件夹，再学案包再解压到新Cid文件夹中。
                        {
                            string newCidPath = HttpContext.Current.Server.MapPath(LearnSite.Store.CourseStore.CreateStore(newCid));
                            LearnSite.Store.SharpZip.UnpackFiles(saveFile, newCidPath);//将学案包重新解压到新学案目录下
                            DelOtherFiles(newCidPath);//递归调用自己，直接删除目录及子目录下后缀为asp|aspx|exe的非法文件
                            //LearnSite.Store.XmlCourse.CourseToXml(newCid);//再在新建Cid文件夹中重建xml 

                            msg = newCid;//导入成功则返回学案自动编号
                        }
                        else
                        {
                            msg = -3;
                        }
                    }
                    else
                    {
                        msg = -4;
                    }
                    System.Threading.Thread.Sleep(1000);
                    Directory.Delete(savePath, true);//最后删除临时文件夹
                }
                else
                {
                    msg = -5;
                }
            }
            else
            {
                msg = -6;
            }
            return msg;
        }
        /// <summary>
        /// 返回生成的临时解包文件夹
        /// </summary>
        /// <returns></returns>
        private static string CreateTempPath()
        {
            string tempPath = "~/tempcs" + "/";
            string tempRealPath = HttpContext.Current.Server.MapPath(tempPath);
            if (Directory.Exists(tempRealPath))
            {
                Directory.Delete(tempRealPath,true);
            }
            Directory.CreateDirectory(tempRealPath);
            return tempRealPath;
        }
        private static int ImportXml(string xmlPath, int cobj, int Hid)
        {
            int newCid = -1;
            string xmlFile = Checkbdir(xmlPath) + "Course.xml";
            if (File.Exists(xmlFile))
            {
                DataSet ds = new DataSet();
                DataTable dtCourse = new DataTable();
                DataTable dtMission = new DataTable();
                DataTable dtTopicDicuss = new DataTable();
                DataTable dtSurvey = new DataTable();
                DataTable dtSurveyQuestion = new DataTable();
                DataTable dtSurveyItem = new DataTable();
                DataTable dtTxtForm = new DataTable();
                DataTable dtListMenu = new DataTable();

                ds.ReadXml(xmlFile);//读取xml文件到ds
                if (ds.Tables.Contains("Course"))
                    dtCourse = ds.Tables["Course"];//获得学案表course
                if (ds.Tables.Contains("Mission"))
                    dtMission = ds.Tables["Mission"];//获得活动表mission 
                if (ds.Tables.Contains("TopicDiscuss"))
                    dtTopicDicuss = ds.Tables["TopicDiscuss"];//获得讨论表
                if (ds.Tables.Contains("Survey"))
                    dtSurvey = ds.Tables["Survey"];//获得调查表
                if (ds.Tables.Contains("SurveyQuestion"))
                    dtSurveyQuestion = ds.Tables["SurveyQuestion"];//获得调查试题表
                if (ds.Tables.Contains("SurveyItem"))
                    dtSurveyItem = ds.Tables["SurveyItem"];//获得调查试题选项表
                if (ds.Tables.Contains("TxtForm"))
                    dtTxtForm = ds.Tables["TxtForm"];
                if (ds.Tables.Contains("ListMenu"))
                    dtListMenu = ds.Tables["ListMenu"];//获得学案导航表

                if (dtCourse != null && dtMission != null)
                {
                    newCid = CreateCourse(dtCourse, cobj, Hid);//创建新学案，返回学案编号
                    CreateMission(dtMission, newCid);//将活动添加到新学案下
                    if (dtTopicDicuss != null)
                        CreateTopicDiscuss(dtTopicDicuss, newCid, Hid);
                    if (dtSurvey != null)
                        CreateSurvey(dtSurvey, dtSurveyQuestion, dtSurveyItem, newCid, Hid);//循环解决三张表编号关联
                    if (dtTxtForm != null)
                        CreateTxtForm(dtTxtForm, newCid);
                    //要重建导航内编号
                    LearnSite.BLL.ListMenu lbll = new BLL.ListMenu();
                    lbll.importmenu(newCid);
                    if (dtListMenu != null)
                        lbll.importupsort(dtListMenu, newCid);
                }
                dtCourse.Dispose();
                dtMission.Dispose();
                dtTopicDicuss.Dispose();
                dtSurvey.Dispose();
                dtSurveyQuestion.Dispose();
                dtSurveyItem.Dispose();
                dtTxtForm.Dispose();
                dtListMenu.Dispose();
                ds.Dispose();
            }
            return newCid;
        }

        /// <summary>
        /// 添加学案调查试题选项ok
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Cid"></param>
        private static void CreateSurveyItem(DataTable dtm, int Cid, int oldQid, int newQid, int newVid)
        {
            int dCount = dtm.Rows.Count;
            LearnSite.BLL.SurveyItem bll = new LearnSite.BLL.SurveyItem();
            if (dCount > 0)
            {
                for (int k = 0; k < dCount; k++)
                {
                    LearnSite.Model.SurveyItem model = new LearnSite.Model.SurveyItem();
                    model = bll.GetModel(dtm, k);
                    int oldMcid = model.Mcid.Value;
                    string thisMcontent = model.Mitem;
                    model.Mcid = Cid;//更换成新学案编号
                    string oldstr = "Store/" + oldMcid.ToString();
                    string newstr = "Store/" + Cid.ToString();
                    model.Mitem = thisMcontent.Replace(oldstr, newstr);//替换链接地址
                    model.Mqid = newQid;
                    model.Mvid = newVid;
                    bll.Add(model);//增加学案调查试题选项
                }
            }
        }

        /// <summary>
        /// 添加学案调查试题ok
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Cid"></param>
        private static void CreateSurveyQuestion(DataTable dt,DataTable dtitems, int Cid,int oldVid,int newVid)
        {
            int dCount = dt.Rows.Count;
            LearnSite.BLL.SurveyQuestion bll = new LearnSite.BLL.SurveyQuestion();
            if (dCount > 0)
            {
                for (int j = 0; j < dCount; j++)
                {
                    LearnSite.Model.SurveyQuestion model = new LearnSite.Model.SurveyQuestion();
                    model = bll.GetModel(dt, j);
                    if (model.Qvid.Value == oldVid)
                    {
                        model.Qvid = newVid;//如果是这个调查的试题，则换成新的
                        int oldMcid = model.Qcid.Value;
                        string thisMcontent = model.Qtitle;
                        model.Qcid = Cid;//更换成新学案编号
                        string oldstr = "Store/" + oldMcid.ToString();
                        string newstr = "Store/" + Cid.ToString();
                        model.Qtitle = thisMcontent.Replace(oldstr, newstr);//替换链接地址
                        int oldQid = model.Qid;
                        int newQid = bll.Add(model);//增加学案调查试题
                        if (dtitems != null)
                        {
                            DataView dv = new DataView(dtitems);
                            dv.RowFilter = "Mqid=" + oldQid.ToString();//直接过滤得到该试题的选项
                            CreateSurveyItem(dv.ToTable(), Cid, oldQid, newQid, newVid);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 添加学案调查ok
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Cid"></param>
        private static void CreateSurvey(DataTable dt,DataTable dtquestion,DataTable dtitem, int Cid,int Hid)
        {
            int dCount = dt.Rows.Count;
            LearnSite.BLL.Survey bll = new LearnSite.BLL.Survey();
            for (int i = 0; i < dCount; i++)
            {
                LearnSite.Model.Survey model = new LearnSite.Model.Survey();
                model = bll.GetModel(dt, i);
                int oldMcid = model.Vcid.Value;
                string thisMcontent = model.Vcontent;
                model.Vcid = Cid;//更换成新学案编号
                model.Vhid = Hid;//换成导入老师
                string oldstr = "Store/" + oldMcid.ToString();
                string newstr = "Store/" + Cid.ToString();
                model.Vcontent = thisMcontent.Replace(oldstr, newstr);//替换链接地址
                int oldvid = model.Vid;
                int newvid = bll.Add(model);//增加学案调查
                if (dtquestion != null)
                    CreateSurveyQuestion(dtquestion, dtitem, Cid, oldvid, newvid);
            }
        }
        /// <summary>
        /// 添加表单
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Cid"></param>
        /// <param name="Hid"></param>
        private static void CreateTxtForm(DataTable dt, int Cid)
        {
            int dCount = dt.Rows.Count;
            LearnSite.BLL.TxtForm bll = new BLL.TxtForm();
            for (int i = 0; i < dCount; i++)
            {
                LearnSite.Model.TxtForm model = new Model.TxtForm();
                model = bll.DataRowToModel(dt.Rows[i]);
                int oldMcid = model.Mcid.Value;
                string thisMcontent = model.Mcontent;
                model.Mcid = Cid;
                string oldstr = "Store/" + oldMcid.ToString();
                string newstr = "Store/" + Cid.ToString();
                model.Mcontent = thisMcontent.Replace(oldstr, newstr);//替换链接地址
                bll.Add(model);
            }        
        }

        /// <summary>
        /// 添加学案讨论ok
        /// </summary>
        /// <param name="dtTopicDiscuss"></param>
        /// <param name="Cid"></param>
        private static void CreateTopicDiscuss(DataTable dt, int Cid, int Hid)
        {
            int dCount = dt.Rows.Count;
            LearnSite.BLL.TopicDiscuss bll = new LearnSite.BLL.TopicDiscuss();
           for (int i = 0; i < dCount; i++)
            {
                LearnSite.Model.TopicDiscuss model = new LearnSite.Model.TopicDiscuss();
                model = bll.GetModel(dt, i);
                int oldMcid = model.Tcid.Value;
                string thisMcontent = model.Tcontent;
                model.Tcid = Cid;//更换成新学案编号
                model.Tteacher = Hid;//换成导入老师
                string oldstr = "Store/" + oldMcid.ToString();
                string newstr = "Store/" + Cid.ToString();
                model.Tcontent = thisMcontent.Replace(oldstr, newstr);//替换链接地址
                bll.Add(model);//增加学案讨论
            }
        }
        /// <summary>
        /// 添加学案活动ok
        /// </summary>
        /// <param name="dtMission"></param>
        /// <param name="Cid"></param>
        private static void CreateMission(DataTable dtMission,int Cid)
        {
            int mCount = dtMission.Rows.Count;
            LearnSite.BLL.Mission bll = new LearnSite.BLL.Mission();
            for (int i = 0; i < mCount; i++)
            {
                LearnSite.Model.Mission ms = new LearnSite.Model.Mission();
                ms = bll.GetTableModel(dtMission, i);
                int oldMcid = ms.Mcid.Value;
                string thisMcontent = ms.Mcontent;
                ms.Mcid = Cid;//更换成新学案编号
                string oldstr = "Store/" + oldMcid.ToString();
                string newstr = "Store/" + Cid.ToString();
                ms.Mcontent = thisMcontent.Replace(oldstr, newstr);//替换链接地址
                string thisMexample = ms.Mexample;
                ms.Mexample = thisMexample.Replace(oldstr, newstr);
                ms.Mgroup = false;
                bll.Add(ms);//增加学案活动
            }        
        }

        
        /// <summary>
        /// 新建学案，返回学案编号
        /// </summary>
        /// <param name="dtCourse"></param>
        /// <returns></returns>
        private static int CreateCourse(DataTable dtCourse, int cobj, int Hid)
        {
            int newCid = 0;
            LearnSite.Model.Courses cs = new LearnSite.Model.Courses();
            LearnSite.BLL.Courses bll = new LearnSite.BLL.Courses();
            cs = bll.GetTableModel(dtCourse);//获得学案的model
            int thisCid = cs.Cid;
            string thisCcontent = cs.Ccontent;
            cs.Cterm = Int32.Parse(LearnSite.Common.XmlHelp.GetTerm());//替换成当前学期
            cs.Cdate = DateTime.Now;
            cs.Cobj = cobj;//替换成当前年级
            cs.Cks = bll.CksMaxValue(cs.Cterm.Value, cs.Cobj.Value, Hid);//替换成当前年级的最新课节
            cs.Chid = Hid;
            cs.Cfiletype = "txt";
            newCid = bll.Add(cs);//获得新增学案的Cid

            string oldstr = "Store/" + thisCid.ToString();
            string newstr = "Store/" + newCid.ToString();
            string newCcontent = thisCcontent.Replace(oldstr, newstr);//替换链接地址
            bll.UpdateCcontent(newCid, newCcontent);//更新内容

            return newCid;
        }
        /// <summary>
        /// 检验物理路径最后一个字符是否缺少"\"
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string Checkbdir(string str)
        {
            if (!str.EndsWith("\\"))
            {
                str = str + "\\";
            }
            return str;
        }

        /// <summary>
        /// 递归调用自己，直接删除目录下后缀为asp|aspx的非法文件
        /// </summary>
        /// <param name="strDir">物理目录地址</param>
        private static void DelOtherFiles (string strDir)
        {
            if (Directory.Exists(strDir))
            {
                string[] strDirs = Directory.GetDirectories(strDir);
                string[] strFiles = Directory.GetFiles(strDir);
                foreach (string strFile in strFiles)
                {
                    bool isdel = false;
                    string[] strType = GetFileType();
                    foreach (string myType in strType)
                    {
                        if (myType == GetSingleFileType(strFile))
                        {
                            isdel = true;
                        }
                    }
                    if (isdel)
                    {
                        File.Delete(strFile);
                    }
                }

                foreach (string strdir in strDirs)
                {
                    DelOtherFiles(strdir);                       
                }

            }

        }


        /// <summary>
        /// 获取非法文件的后缀名的集合
        /// </summary>
        /// <returns></returns>
        private static string[] GetFileType()
        {
            string AllFileType = "asp|aspx";
            string[] GetFileTypes = AllFileType.Split(new char[] { '|' });
            return GetFileTypes;
        }

        /// <summary>
        /// 获取文件名后缀
        /// </summary>
        /// <param name="myfile"></param>
        /// <returns></returns>
        private static string GetSingleFileType(string myfile)
        {
            return myfile.Substring(myfile.LastIndexOf(".") + 1).ToLower();
        }
    }
}
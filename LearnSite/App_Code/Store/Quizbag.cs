using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
namespace LearnSite.Store
{
    /// <summary>
    ///Quizbag 的摘要说明
    /// </summary>
    public class Quizbag
    {
        public Quizbag()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static void QuizXml()
        {
            LearnSite.BLL.Quiz bll = new LearnSite.BLL.Quiz();
            DataSet ds = bll.GetAllList();
            string xmlFile =QuizStorage()+ "Quiz.xml";

            string rarfileurl = "~/Quiz/Quiz.rar";
            string rarFile = HttpContext.Current.Server.MapPath(rarfileurl);

            if (File.Exists(xmlFile))
                File.Delete(xmlFile);//如果已经存在则删除
            if (ds != null)
            {
                ds.DataSetName = "LearnSite";
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].TableName = "Quiz";
                    ds.WriteXml(xmlFile);//生成xml文件
                    LearnSite.Store.SharpZip.PackFiles(rarFile, QuizStorage());//把Storage文件夹打包
                    File.Delete(xmlFile);
                }
            }
            ds.Dispose();
        
        }

        private static bool Uploadxml(FileUpload fud)
        {
            bool isright = false;
            string uploadfile = fud.PostedFile.FileName;
            string fileType = uploadfile.Substring(uploadfile.LastIndexOf(".") + 1);
            if (fileType.ToLower() == "rar")
            {
                string rarfileurl = "~/Quiz/UploadQuiz.rar";//上传包存放路径
                string rarFile = HttpContext.Current.Server.MapPath(rarfileurl);
                fud.SaveAs(rarFile);//保存上传
                LearnSite.Store.SharpZip.UnpackQuizFiles(rarFile, QuizStorage());//解压上传包，独立用，与学案解压区别
                File.Delete(rarFile);//删除上传包
                isright = true;
            }
            return isright;
        }
        /// <summary>
        /// 从上传的试题包xml中导入试题到数据库
        /// </summary>
        /// <returns></returns>
        public static string XmltoQuiz(FileUpload fud)
        {
            string msg = "";
            if (Uploadxml(fud))
            {
                string xmlFile = QuizStorage() + "Quiz.xml";
                if (File.Exists(xmlFile))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(xmlFile);
                    DataTable dt = ds.Tables["Quiz"];
                    if (dt != null)
                    {
                        int count = dt.Rows.Count;
                        if (count > 0)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                LearnSite.Model.Quiz model = new LearnSite.Model.Quiz();
                                if (dt.Rows[i]["Qid"].ToString() != "")
                                {
                                    model.Qid = int.Parse(dt.Rows[i]["Qid"].ToString());
                                }
                                if (dt.Rows[i]["Qtype"].ToString() != "")
                                {
                                    model.Qtype = int.Parse(dt.Rows[i]["Qtype"].ToString());
                                }
                                model.Question = dt.Rows[i]["Question"].ToString();
                                model.Qanswer = dt.Rows[i]["Qanswer"].ToString();
                                model.Qanalyze = dt.Rows[i]["Qanalyze"].ToString();
                                if (dt.Rows[i]["Qscore"].ToString() != "")
                                {
                                    model.Qscore = int.Parse(dt.Rows[i]["Qscore"].ToString());
                                }
                                model.Qclass = dt.Rows[i]["Qclass"].ToString();
                                if (dt.Rows[i]["Qselect"].ToString() != "")
                                {
                                    if ((dt.Rows[i]["Qselect"].ToString() == "1") || (dt.Rows[i]["Qselect"].ToString().ToLower() == "true"))
                                    {
                                        model.Qselect = true;
                                    }
                                    else
                                    {
                                        model.Qselect = false;
                                    }
                                }
                                LearnSite.BLL.Quiz bll = new LearnSite.BLL.Quiz();
                                bll.Add(model);
                            }
                            msg = "成功导入" + count.ToString() + "条试题！";
                            File.Delete(xmlFile);//删除上传文件
                            ds.Dispose();
                        }
                        else
                        {
                            msg = "试题包为空！";
                        }
                    }
                    else
                    {
                        msg = "试题包不正确！";
                    }
                }
                else
                {
                    msg = "试题包不存在！";
                }
            }
            else
            {
                msg = "试题包格式不正确！";
            }
            return msg;
        }
        /// <summary>
        /// 获取下载链接文件，无返回空字符""
        /// </summary>
        /// <returns></returns>
        public static string Xmlurl()
        {
            string xmlfileurl= "~/Quiz/Quiz.rar";
            string xmlFile = HttpContext.Current.Server.MapPath(xmlfileurl);
            if (File.Exists(xmlFile))
                return xmlfileurl;
            else
                return "";
        
        }
        /// <summary>
        /// 返回试题资源存放物理路径带/，不存在则自动创建
        /// </summary>
        /// <returns></returns>
        private static string QuizStorage()
        {
            string storage = "~/Quiz/Storage/";
            string spath= HttpContext.Current.Server.MapPath( storage);
            if (!Directory.Exists(spath))
                Directory.CreateDirectory(spath);
            if (!spath.EndsWith(@"\"))
                spath += @"\";
            return spath;
        }
    }
}
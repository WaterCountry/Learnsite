using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Data;
using System.Text;
namespace LearnSite.Store
{
    /// <summary>
    ///XmlCourse 的摘要说明
    /// </summary>
    public class XmlCourse
    {
        public XmlCourse()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        private static string CourseSavePath = "~/Store/";
        /// <summary>
        /// 返回学案目录的物理地址（自动创建不存在目录）
        /// </summary>
        /// <param name="Cid"></param>
        /// <returns></returns>
        public static string CourseCidPath(int Cid)
        {
            string mapsavePath = HttpContext.Current.Server.MapPath(CourseSavePath);
            if (!Directory.Exists(mapsavePath))
            {
                Directory.CreateDirectory(mapsavePath);
            }
            string mapcidpath = HttpContext.Current.Server.MapPath(CourseSavePath + Cid.ToString());
            if (!Directory.Exists(mapcidpath))
            {
                Directory.CreateDirectory(mapcidpath);
            }
            return mapcidpath;
        }
        /// <summary>
        /// 学案xml序列化（不用）
        /// </summary>
        /// <param name="Cid"></param>
        /// <returns></returns>
        public static bool SerialiazeCourse(int Cid)
        {
            LearnSite.Model.Courses course = new LearnSite.Model.Courses();
            LearnSite.BLL.Courses bll = new LearnSite.BLL.Courses();
            course = bll.GetModel(Cid);
            string xmlCoursePath = CourseCidPath(Cid);
            if (!Directory.Exists(xmlCoursePath)) //如果不存在该目录，则创建
                Directory.CreateDirectory(xmlCoursePath);
            string xmlCourseName = xmlCoursePath + @"\" + Cid + ".xml";
            if (File.Exists(xmlCourseName))
                File.Delete(xmlCourseName);
            XmlSerializer xs = new XmlSerializer(typeof(LearnSite.Model.Courses));
            Stream stream = new FileStream(xmlCourseName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            xs.Serialize(stream, course);
            stream.Close();

            if (File.Exists(xmlCourseName))
            {
                return true;
            }
            {
                return false;
            }

        }


        ////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///  读取指定Cid学案生成xml到指定Cid目录
        /// </summary>
        /// <param name="Cid"></param>
        /// <returns></returns>
        public static bool CourseToXml(int Cid)
        {
            bool iscreate = false;
            DataSet ds = LearnSite.DBUtility.DbHelperSQL.CourseMissionQuery(Cid);

            string xmlFilePath = CourseCidPath(Cid);
            if (!Directory.Exists(xmlFilePath)) //如果不存在该目录，则创建
                Directory.CreateDirectory(xmlFilePath);
            string xmlFile = xmlFilePath + @"\Course.xml";

            if (ds != null)
            {
                try
                {
                    if (File.Exists(xmlFile))
                    {
                        NotReadOnly(xmlFile);//去只读
                        File.Delete(xmlFile);//如果已经存在则删除，不存在不会引发异常
                    }
                    ds.DataSetName = "LearnSite";
                    ds.WriteXml(xmlFile, XmlWriteMode.WriteSchema);//生成xml文件
                    iscreate = true;
                }
                catch 
                {
                    string msg = "创建失败，网站根目录下Store的子目录"+Cid+"中Course.xml为只读！";
                    LearnSite.Common.WordProcess.jsdialog(msg);
                    iscreate = false;
                }
            }

            ds.Dispose();
            return iscreate;
        }

        //导出dataset数据到XML文件
        //Author:Quietwalk
        //2010-09-09
        public static bool ExportCourseToXMl(int Cid)
        {
            DataSet ds = LearnSite.DBUtility.DbHelperSQL.CourseMissionQuery(Cid);

            string xmlFilePath = CourseCidPath(Cid);
            if (!Directory.Exists(xmlFilePath)) //如果不存在该目录，则创建
                Directory.CreateDirectory(xmlFilePath);
            string strXMLPath = xmlFilePath + @"\Course.xml";

            FileInfo XMLfile = new FileInfo(strXMLPath);
            bool bReturnValue = false;

            try
            {
                if (ds != null && ds.Tables.Count > 0 && XMLfile.Exists)
                {
                    ds.WriteXml(strXMLPath, XmlWriteMode.WriteSchema);
                    bReturnValue = true;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return bReturnValue;
        }



        /// <summary>
        /// 将指定Cid从xml内容读到model中
        /// </summary>
        /// <param name="Cid"></param>
        /// <returns></returns>
        public static LearnSite.Model.Courses course(int Cid)
        {
            LearnSite.Model.Courses cs = new LearnSite.Model.Courses();
            LearnSite.BLL.Courses bll = new LearnSite.BLL.Courses();
            string xmlFile = XmlFilename(Cid);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            if (xmlFile != "")
            {
                ds.ReadXml(xmlFile);//读取xml文件到ds
                dt = ds.Tables["Course"];
                cs = bll.GetTableModel(dt);
                ds.Dispose();
                return cs;
            }
            else
            {
                ds.Dispose();
                return null;
            }
        }
        /// <summary>
        /// 将指定Mcid指定Tsort的活动从xml内容读到model中
        /// </summary>
        /// <param name="Cid"></param>
        /// <param name="Tsort"></param>
        /// <returns></returns>
        public static LearnSite.Model.Mission mission(int Mcid, int Tsort)
        {
            LearnSite.Model.Mission ms = new LearnSite.Model.Mission();
            LearnSite.BLL.Mission bll = new LearnSite.BLL.Mission();
            string xmlFile = XmlFilename(Mcid);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            if (xmlFile != "")
            {
                ds.ReadXml(xmlFile);//读取xml文件到ds
                dt = ds.Tables["Mission"];
                ms = bll.GetTableModel(dt, Tsort);
                ds.Dispose();
                return ms;
            }
            else
            {
                ds.Dispose();
                return null;
            }

        }
        /// <summary>
        /// 将指定Cid的xml文件中的Course转换成DataView
        /// </summary>
        /// <param name="Cid"></param>
        /// <returns></returns>
        public static DataView XmlToCourse(int Cid)
        {
            DataSet ds = new DataSet();
            DataView dv = new DataView();
            string xmlFile = XmlFilename(Cid);
            if (xmlFile != "")
            {
                ds.ReadXml(xmlFile);//读取xml文件到ds
                if (ds.Tables["Course"] != null)
                    dv = ds.Tables["Course"].DefaultView;
            }
            return dv;

        }
        /// <summary>
        /// 将指定Cid的xml文件中的Mission节转换成DataView
        /// </summary>
        /// <param name="Cid"></param>
        /// <returns></returns>
        public static DataView XmlToMission(int Cid)
        {
            DataSet ds = new DataSet();
            DataView dv = new DataView();
            string xmlFile = XmlFilename(Cid);
            if (xmlFile != "")
            {
                ds.ReadXml(xmlFile);//读取xml文件到ds
                if (ds.Tables["Mission"] != null)
                    dv = ds.Tables["Mission"].DefaultView;
            }
            return dv;

        }
        /// <summary>
        /// 获取指定Cid学案的Xml文件名，如果存在返回物理路径，否则返回空字符
        /// </summary>
        /// <param name="Cid"></param>
        /// <returns></returns>
        public static string XmlFilename(int Cid)
        {
            string xmlFilePath = CourseCidPath(Cid);
            string xmlFile = xmlFilePath + @"\Course.xml";
            if (File.Exists(xmlFile))
            {
                return xmlFile;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 去文件只读
        /// </summary>
        /// <param name="realpath">物理路径</param>
        private static void NotReadOnly(string realpath)
        {
            if (File.GetAttributes(realpath) == FileAttributes.ReadOnly)
            {
                File.SetAttributes(realpath, FileAttributes.Normal);
            }
        }
        /// <summary>
        /// 判断该学案下的Course.xml是否为只读
        /// </summary>
        /// <param name="Cid"></param>
        /// <returns></returns>
        public static bool IsReadOnly(int Cid)
        {
            bool isRy = false;
            string xmlFilePath = CourseCidPath(Cid);
            string xmlFile = xmlFilePath + @"\Course.xml";
            if (File.GetAttributes(xmlFile) == FileAttributes.ReadOnly)
            {
                isRy = true;
            }
            return isRy;
        }
    }
}
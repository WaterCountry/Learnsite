using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web;
using System.Xml;
using System.IO;
using System.Text;
using System.Data;

namespace LearnSite.Common
{
    /// <summary>
    ///classmodel 的摘要说明
    /// </summary>
    public class ClassModel
    {
        public ClassModel()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private static string XGetRoot(XmlDocument xmldoc)
        {
            string rootStr = "classmodel";
            XmlNode root = xmldoc.SelectSingleNode(rootStr);
            XmlNodeList nodeList = root.ChildNodes;
            if (nodeList.Count > 0)
            {
                XmlNode childnode = nodeList[0];
                string nodename = childnode.Name;
                if (nodename != "student")
                {
                    rootStr = "students";                
                }
            }
            return rootStr;
        }

        /// <summary>
        /// 将上传的原模型预处理为对应的主机名
        /// </summary>
        /// <param name="xmlpath"></param>
        public static void PreMachineName(string xmlpath)
        {
            XmlDocument xmlcmp = ReadXml(xmlpath);
            {
                LearnSite.BLL.Computers pbll = new BLL.Computers();
                DataTable dtcomputer = pbll.GetPipPmachine();
                XmlNode root = xmlcmp.SelectSingleNode("classmodel");
                XmlNodeList nodeList = root.ChildNodes;
                if (nodeList[0].Name != "student")
                {
                    XmlNode newroot = root.SelectSingleNode("students");
                    nodeList = newroot.ChildNodes;
                }
                foreach (XmlNode xn in nodeList)
                {
                    XmlNode addressNode = xn.SelectSingleNode("address");
                    string ip = addressNode.Attributes["IP"].Value;
                    string Sname = GetMachineByIp(dtcomputer, ip);　//根据IP，获取主机名
                    if (!string.IsNullOrEmpty(Sname) && !WordProcess.IsZh(Sname))
                    {
                        XmlNode nameNode = xn.SelectSingleNode("name");
                        nameNode.InnerText = Sname;//如果有则修改
                    }
                }
                xmlcmp.Save(xmlpath);//保存生成的主机名模型
            };
        }
        /// <summary>
        /// 创建所教班级的模型xml
        /// </summary>
        /// <param name="hid"></param>
        /// <param name="xdoc"></param>
        /// <param name="savepath"></param>
        /// <param name="months"></param>
        /// <returns></returns>
        public static string SetAllStuName(int hid, XmlDocument xdoc, string savepath, int weeks, string xmlfile)
        {
            string result = "这是默认值：创建失败！";
            XmlNode root = xdoc.SelectSingleNode("classmodel");
            XmlNodeList nodeListtest = root.ChildNodes;
            if (nodeListtest[0].Name != "student")
            {
                root = root.SelectSingleNode("students");
            }
            if (root != null)
            {
                XmlNodeList nodeList = root.ChildNodes;
                int stucount = nodeList.Count;
                if (stucount > 0)
                {
                    BLL.Signin gbll = new BLL.Signin();
                    DataTable allclassstu = gbll.GetQnameQip(hid, weeks);
                    int allclassCount = allclassstu.Rows.Count;
                    if (allclassCount > 0)
                    {
                        //string teststr = ClassModeSavePath(hid.ToString()) + @"\test" + hid + "all.xml";
                        //allclassstu.WriteXml(teststr);//测试输出
                        BLL.Room bll = new BLL.Room();
                        DataTable dtclass = bll.GetMyGradeClass(hid);
                        int dtcscount = dtclass.Rows.Count;
                        result = "信息提示：有签到的任教" + dtcscount.ToString() + "个班级共" + allclassstu.Rows.Count.ToString() + "人 |";
                        int right = 0;
                        int wrong = 0;
                        int miss = 0;
                        for (int i = 0; i < dtcscount; i++)
                        {
                            string rgrade = dtclass.Rows[i]["Rgrade"].ToString();
                            string rclass = dtclass.Rows[i]["Rclass"].ToString();
                            string classname = rgrade + "年级" + rclass + "班";
                            DataView dv = allclassstu.DefaultView;
                            dv.RowFilter = "Qgrade=" + rgrade + " and Qclass=" + rclass;
                            DataTable dt = dv.ToTable();
                            //dt.Columns.Remove("Qgrade");
                            //dt.Columns.Remove("Qclass");//清理掉无用字段
                            //string testclassstr = ClassModeSavePath(hid.ToString()) + @"\test" + rgrade + "_" + rclass +".xml";
                            //dt.WriteXml(testclassstr);//测试输出
                            int thiscount = dt.Rows.Count;
                            if (thiscount > 0)
                            {
                                try
                                {
                                    SetStuName(dt, classname, savepath, xmlfile);//创建该班级的模型
                                    right++;
                                    result = result + rgrade + "." + rclass + "(" + thiscount.ToString() + ")";//生成该班级模型                        
                                }
                                catch (Exception ex)
                                {
                                    wrong++;
                                    result = result + rgrade + "." + rclass + "(??)";
                                    string msgtype = "班级模型" + rgrade + "." + rclass + "班模型生成出错";
                                    LearnSite.Common.Log.Addlog(msgtype, ex.Message);
                                }
                            }
                            else
                            {
                                miss++;
                                result = result + rgrade + "." + rclass + "(0)";
                            }
                        }
                        result = result + "<br/><br/>创建成功" + right.ToString() + "个班级模型xml文件，失效" + miss.ToString() + "个，出错" + wrong.ToString() + "个。";
                        DelUpXml(xmlfile);//删除上传的xml文件
                    }
                    else
                    {
                        result = "没有在指定时间内签到的班级学生，无法根据签到IP生成相应的班级模型！";
                    }
                }
                else
                {
                    result = "上传的原有班级模型xml文件中学生机为0，无法创建!";
                }
            }
            else
            {
                result = "上传的原有班级模型xml文件中无classmodel根节点!";
            }
            return result;

        }

        /// <summary>
        /// 设置班级模型名称及修改IP对应的学生姓名
        /// </summary>
        /// <param name="xdoc"></param>
        /// <param name="classname"></param>
        public static void SetStuName( DataTable dts, string classname, string savepath,string xmlfile)
        {
            XmlDocument xmlsave = new XmlDocument();
            string xmltype = xmlfile.Substring(xmlfile.LastIndexOf(".") + 1);
            xmlsave = ReadXml(xmlfile);

            XmlNode root = xmlsave.SelectSingleNode("classmodel");//这行是专门用于设置模型的班级姓名，请匆改动。
            root.Attributes["name"].Value = classname;//设置班级模型名称

            XmlNodeList nodeList = root.ChildNodes;
            if (nodeList[0].Name != "student")
            {
                XmlNode newroot = root.SelectSingleNode("students");
                nodeList = newroot.ChildNodes;
            }

            foreach (XmlNode xn in nodeList)
            {
                XmlNode addressNode = xn.SelectSingleNode("address");
                string ip = addressNode.Attributes["IP"].Value;
                StuMsg theStu = GetStuNameByIp(dts, ip);　//根据IP，获取该班级该IP就座的学生姓名
                if (theStu != null && theStu.isOk)
                {
                    XmlNode nameNode = xn.SelectSingleNode("name");
                    nameNode.InnerText = theStu.Sname;//如果有就座学生则修改
                    XmlNode groupNode = xn.SelectSingleNode("group");
                    if (theStu.Sleader)
                        groupNode.Attributes["leader"].Value = "1";
                    else
                        groupNode.Attributes["leader"].Value = "0";
                    if (theStu.Sgroup > 0)
                        groupNode.InnerText = theStu.Sgtitle;
                }
            }
            string savefile = TodaySavePath(savepath) + @"\" + classname + "." + xmltype;
            if (File.Exists(savefile))
                File.Delete(savefile);            
            xmlsave.Save(savefile);//保存生成的新班级模型
        }

        /// <summary>
        /// 根据班级模型读取IP的x坐标和y坐标
        /// </summary>
        /// <param name="xdoc"></param>
        /// <param name="classname"></param>
        public static int SetIpxy(string xmlfile,string Pm)
        {
            XmlDocument xmlsave = new XmlDocument();
            xmlsave = ReadXml(xmlfile);
            
            XmlNode root = xmlsave.SelectSingleNode("classmodel");
            XmlNodeList nodeList = root.ChildNodes;
            if (nodeList[0].Name != "student")
            {
                XmlNode newroot = root.SelectSingleNode("students");
                nodeList = newroot.ChildNodes;
            }

            LearnSite.BLL.Computers cbll = new BLL.Computers();
            foreach (XmlNode xn in nodeList)
            {
                XmlNode addressNode = xn.SelectSingleNode("address");
                string ip = addressNode.Attributes["IP"].Value;
                XmlNode posNode = xn.SelectSingleNode("posThumb");
                string px = posNode.Attributes["x"].Value;
                string py = posNode.Attributes["y"].Value;
                string machine = xn.SelectSingleNode("name").InnerText;//主机名
                if (WordProcess.IsZh(machine)) machine = "";//如果班级模型的主机名是中文，则留空

                if (!cbll.UpdateIpPxPy(ip, Int32.Parse(px), Int32.Parse(py), Pm))
                {
                //如果没更新，则插入一条
                    LearnSite.Model.Computers cmodel = new Model.Computers();
                    cmodel.Pdate = DateTime.Now;
                    cmodel.Pip = ip;
                    cmodel.Plock = true;
                    cmodel.Pm = Pm;
                    cmodel.Pmachine = machine;
                    cmodel.Px = Int32.Parse(px);
                    cmodel.Py = Int32.Parse(py);
                    cbll.AddModel(cmodel);
                }
            }
            return nodeList.Count;
        }

        /// <summary>
        /// 根据IP返回姓名
        /// </summary>
        /// <param name="dtclass"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        private static StuMsg GetStuNameByIp(DataTable dtclass, string ip)
        {
            StuMsg mystu = new StuMsg();
            int dcount = dtclass.Rows.Count;
            LearnSite.BLL.Students sbll = new BLL.Students();
            for (int i = 0; i < dcount; i++)
            {
                string dtip = dtclass.Rows[i]["Qip"].ToString();
                if (dtip.Equals(ip))
                {
                    mystu.Sname = dtclass.Rows[i]["Qname"].ToString();
                    mystu.Sleader = bool.Parse(dtclass.Rows[i]["Sleader"].ToString());
                    int sgroup = 0;
                    if (dtclass.Rows[i]["Sgroup"] != null && dtclass.Rows[i]["Sgroup"].ToString()!="")
                        sgroup = Int32.Parse(dtclass.Rows[i]["Sgroup"].ToString());
                    mystu.Sgroup = sgroup;
                    if (mystu.Sleader)
                        mystu.Sgtitle = dtclass.Rows[i]["Sgtitle"].ToString();
                    else
                    {
                        if (sgroup > 0)
                            mystu.Sgtitle = sbll.GetSgtitle(sgroup);
                        else
                            mystu.Sgtitle = "";
                    }
                    mystu.isOk = true;
                    break;
                }
            }
            return mystu;
        }
        /// <summary>
        /// 获取图片的长度和宽度属性，超大图片按指定比例缩小
        /// </summary>
        private class StuMsg
        {
            private string _Sname;
            private string _Sgtitle;
            private int _Sgroup = 0;
            private bool _Sleader=false;
            private bool _isOk = false;
            public string Sname
            {
                set { _Sname = value; }
                get { return _Sname; }
            }
            public string Sgtitle
            {
                set { _Sgtitle = value; }
                get { return _Sgtitle; }
            }
            public int Sgroup
            {
                set { _Sgroup = value; }
                get { return _Sgroup; }
            }
            public bool Sleader
            {
                set { _Sleader = value; }
                get { return _Sleader; }
            }
            public bool isOk
            {
                set { _isOk = value; }
                get { return _isOk; }
            }
        }
        /// <summary>
        /// 根据IP返回主机名
        /// </summary>
        /// <param name="dtcomputer"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        private static string GetMachineByIp(DataTable dtcomputer, string ip)
        {
            string myMachine = "";
            int dcount = dtcomputer.Rows.Count;
            for (int i = 0; i < dcount; i++)
            {
                string dtip = dtcomputer.Rows[i]["Pip"].ToString();
                if (dtip.Equals(ip))
                {
                    myMachine = dtcomputer.Rows[i]["Pmachine"].ToString();
                    break;
                }
            }
            return myMachine;
        }
        /// <summary>
        /// 读取上传模型中的学生数
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static int CountXmlStu(XmlDocument xdoc)
        {
            XmlNode root = xdoc.SelectSingleNode("classmodel");
            if (root != null)
            {

                XmlNodeList nodeList = root.ChildNodes;
                if (nodeList[0].Name != "student")
                {
                    XmlNode newroot = root.SelectSingleNode("students");
                    nodeList = newroot.ChildNodes;
                }

                return nodeList.Count;
            }
            else
                return 0;
        }
        /// <summary>
        /// 读取上传的原有班级模型xml，返回xmldocument类型
        /// </summary>
        /// <param name="xmlfile"></param>
        /// <returns></returns>
        public static XmlDocument ReadXml(string xmlfile)
        {
            if (File.Exists(xmlfile))
            {
                XmlDocument xdoc = new XmlDocument();
                try
                {
                    xdoc.Load(xmlfile);
                    return xdoc;
                }
                catch
                {
                    return null;
                }
            }
            else
                return null;
        }
        /// <summary>
        /// 清除上传的xml文件
        /// </summary>
        /// <param name="xmlfile"></param>
        public static void DelUpXml(string xmlfile)
        {
            File.Delete(xmlfile);
        }
        /// <summary>
        /// 上传保存xml文件，返回保存物理路径
        /// </summary>
        /// <param name="FileUpXml"></param>
        /// <returns></returns>
        public static string SaveClassModelXml(FileUpload FileUpXml, string hid,string savepath)
        {
            string xmlfile = FileUpXml.PostedFile.FileName;
            string xmltype = xmlfile.Substring(xmlfile.LastIndexOf(".") + 1);
            string newxml = hid + DateTime.Now.Millisecond.ToString();
            string xmlpath = "";
            if (xmltype == "xml" || xmltype == "cls")
            {
                xmlpath =savepath+@"\"+ newxml + "." + xmltype;
                FileUpXml.PostedFile.SaveAs(xmlpath);               
            }
            return xmlpath;
        }
        /// <summary>
        /// 班级模型生成保存的物理路径
        /// </summary>
        /// <returns></returns>
        public static string ClassModeSavePath(string hid)
        {
            string rootpath = HttpContext.Current.Server.MapPath("~/ClassModel/").ToString();
            if (!Directory.Exists(rootpath))
                Directory.CreateDirectory(rootpath);
            string xmlpath = rootpath + hid;
            if (!Directory.Exists(xmlpath))
                Directory.CreateDirectory(xmlpath);
            return xmlpath;
        }
        /// <summary>
        /// 返回今天保存的路径，后面缺少\
        /// </summary>
        /// <returns></returns>
        private static string TodaySavePath(string savepath)
        {
            DateTime today = DateTime.Now;
            string todaystr = today.Year.ToString() + "_" + today.Month.ToString() + "_" + today.Day.ToString();
            string tosavepath = savepath+@"\"+ todaystr;
            if (!Directory.Exists(tosavepath))
            {
                Directory.CreateDirectory(tosavepath);
            }
            return tosavepath;
        }
    }
}
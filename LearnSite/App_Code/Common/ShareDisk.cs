using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

namespace LearnSite.Common
{
    /// <summary>
    ///ShareDisk 的摘要说明
    /// </summary>
    public class ShareDisk
    {
        public ShareDisk()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public static string shareDir = "~/ShareDisk/";
        private static int sharelimit = Common.XmlHelp.ShareDiskLimit();
        /// <summary>
        /// 获取网盘的物理路径，如果没有目录则创建
        /// </summary>
        /// <param name="Syear">入学年份</param>
        /// <param name="Sclass">班级</param>
        /// <param name="Dir">学号或组号</param>
        /// <returns></returns>
        public static string GetSharePath(string Syear, string Sclass, string Dir)
        {
            string sharePath = HttpContext.Current.Server.MapPath(shareDir);
            if (!Directory.Exists(sharePath))
                Directory.CreateDirectory(sharePath);

            string SyearPath = sharePath + Syear.ToString();//入学年份物理路径
            if (!Directory.Exists(SyearPath))
                Directory.CreateDirectory(SyearPath);

            string Sclasspath = SyearPath + @"\" + Sclass;//班级物理路径
            if (!Directory.Exists(Sclasspath))
                Directory.CreateDirectory(Sclasspath);

            string Dirpath = Sclasspath + @"\" + Dir;//学号或组号物理路径
            if (!Directory.Exists(Dirpath))
                Directory.CreateDirectory(Dirpath);
            return Dirpath;
        }
        /// <summary>
        /// 返回短文件名（无后缀）
        /// </summary>
        /// <param name="fname"></param>
        /// <returns></returns>
        private static string getsinglefilename(string fname)
        {
            int ln = fname.LastIndexOf(".");
            string mypath = fname.Substring(0, ln);
            return mypath;
        }
        /// <summary>
        /// 取扩展名（如jpg）不含点
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private static string getextions(string filename)
        {
            string ext = Path.GetExtension(filename).ToLower();
            return ext.Replace(".", "");
        }
        /// <summary>
        /// 返回物理路径中的文件名称（3011006_92_171_8_122）
        /// </summary>
        /// <param name="fname">物理路径</param>
        /// <returns></returns>
        public static string getpathfilename(string fname)
        {
            return Path.GetFileNameWithoutExtension(fname);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FileUp"></param>
        /// <param name="Syear"></param>
        /// <param name="Sclass"></param>
        /// <param name="Sname"></param>
        /// <param name="Snum"></param>
        /// <param name="Sgroup"></param>
        /// <param name="isGroup"></param>
        /// <returns></returns>
        public static string SaveFile(FileUpload FileUp, string Syear, string Sgrade, string Sclass, string Sname, string Snum, string Sgroup, bool isGroup)
        {
            string Dir = Snum;
            if (isGroup)
                Dir = Sgroup;
            string limitType = "exe|asp|aspx|asxh|php";
            string result = "未知错误！";
            int flen = FileUp.PostedFile.ContentLength;
            int k = 1024;
            if (flen / k / k < sharelimit)
            {
                string savefile = FileUp.PostedFile.FileName;
                string shortf = getpathfilename(savefile);
                shortf = LearnSite.Common.WordProcess.FilterSpecial(shortf);
                string ftype = getextions(savefile);
                if (!limitType.Contains(ftype))//不是限制类型
                {
                    Sname = LearnSite.Common.WordProcess.FilterSpecial(Sname);
                    string newfile = shortf + "." + ftype;
                    if (isGroup) newfile = shortf + "_" + Sname.Trim() + "." + ftype;
                    string savepath = GetSharePath(Syear, Sclass, Dir) + @"\" + newfile;
                    try
                    {
                        FileUp.PostedFile.SaveAs(savepath);
                        result = "保存" + shortf + "文件到网盘成功!";
                        Model.ShareDisk kmodel = new Model.ShareDisk();
                        kmodel.Kown = isGroup;//是否小组文档
                        kmodel.Kyear = Int32.Parse(Syear);
                        kmodel.Kgrade = Int32.Parse(Sgrade);
                        kmodel.Kclass = Int32.Parse(Sclass);
                        kmodel.Kgroup = Int32.Parse(Sgroup);
                        kmodel.Knum = Snum;
                        kmodel.Kname = Sname;
                        kmodel.Kfilename = newfile;
                        kmodel.Kfsize = flen;
                        kmodel.Kfurl = shareDir + Syear + "/" + Sclass + "/" + Dir + "/" + newfile;
                        kmodel.Kftpe = ftype;
                        kmodel.Kfdate = DateTime.Now;
                        BLL.ShareDisk kbll = new BLL.ShareDisk();
                        kbll.Add(kmodel);//记录到数据库
                    }
                    catch
                    {
                        string msgtype = "网盘上传出错";
                        string msg = "当前上传路径为" + savepath;
                        LearnSite.Common.Log.Addlog(msgtype, msg);
                    }
                }
                else
                {
                    result = "保存失败！文件类型不能为" + limitType;
                }
            }
            else
            {
                result = "上传的文件大小不能超过" + sharelimit.ToString() + "MB!";
            }
            return result;
        }

        public static bool DelFile(string furl)
        {
            bool isok = true;
            if (!string.IsNullOrEmpty(furl))
            {
                try
                {
                    string fpath = HttpContext.Current.Server.MapPath(furl);
                    File.Delete(fpath);
                }
                catch
                {
                    isok = false;
                }
            }
            return isok;
        }

        /// <summary>
        /// 网盘类
        /// </summary>
        public class DiskInfo
        {
            private DataView _dw = null;//文件列表
            private float _dsize = 0;//网盘空间
            private int _dlimit = sharelimit;//网盘上限
            private float _dleft = 0;//网盘剩余
            private string _durl;//网盘虚拟目录
            private string _droot = shareDir;//网盘根虚拟目录 "~/ShareDisk/";
            private string _dpath;//网盘物理路径
            private bool _dupload = true;
            private int _dcount = 0;//文件数
            /// <summary>
            /// 网盘虚拟目录
            /// </summary>
            /// <param name="Sgroup"></param>
            /// <param name="Snum"></param>
            public DiskInfo(string Syear, string Sclass, string Dir, bool IsGroup)
            {
                if (IsGroup)
                    _dlimit = sharelimit * 2;
                _durl = _droot + Syear + "/" + Sclass + "/" + Dir + "/";
                _dpath = GetSharePath(Syear, Sclass, Dir);
                if (!string.IsNullOrEmpty(_durl))
                {
                    DataSet ds = new DataSet();
                    ds.Tables.Add();
                    ds.Tables[0].TableName = "disktable";
                    ds.Tables[0].Columns.Add("fname", typeof(String));
                    ds.Tables[0].Columns.Add("fsize", typeof(String));
                    ds.Tables[0].Columns.Add("furl", typeof(String));
                    ds.Tables[0].Columns.Add("fdate", typeof(DateTime));
                    ds.Tables[0].Columns.Add("ftype", typeof(String));
                    if (Directory.Exists(_dpath))
                    {
                        DirectoryInfo di = new DirectoryInfo(_dpath);
                        FileInfo[] fis = di.GetFiles();
                        float dsizes = 0;
                        foreach (FileInfo fi in fis)
                        {
                            DataRow row;
                            row = ds.Tables[0].NewRow();
                            string fname = fi.Name;
                            string fext = getextions(fname);

                            row[0] = fname; //文件名
                            float fl = fi.Length;
                            dsizes = dsizes + fl;
                            row[1] = (fl / 1024).ToString("0") + "kb";//大小
                            row[2] = _durl + fname;//超连接
                            row[3] = fi.CreationTime;//创建日期
                            row[4] = "~/Images/FileType/" + fext + ".gif";//文件类型图标
                            ds.Tables[0].Rows.Add(row);
                            _dcount++;
                        }
                        ds.AcceptChanges();
                        _dw = ds.Tables[0].DefaultView;
                        _dw.Sort = "fdate desc";
                        float k = 1024;
                        _dsize = dsizes / k / k;
                        _dleft = _dlimit - _dsize;
                        if (_dleft < 0)
                            _dupload = false;//如果空余空间不足，则不能上传
                    }
                    ds.Dispose();
                }
            }
            /// <summary>
            /// 网盘已占用MB
            /// </summary>
            public float Dsize
            {
                set { _dsize = value; }
                get { return _dsize; }
            }
            /// <summary>
            /// 网盘存储上限MB
            /// </summary>
            public int Dlimit
            {
                set { _dlimit = value; }
                get { return _dlimit; }
            }
            /// <summary>
            /// 网盘剩余
            /// </summary>
            public float Dleft
            {
                set { _dleft = value; }
                get { return _dleft; }
            }
            /// <summary>
            /// 网盘虚拟目录
            /// </summary>
            public string Durl
            {
                set { _durl = value; }
                get { return _durl; }
            }
            /// <summary>
            /// 网盘根虚拟目录
            /// </summary>
            public string Droot
            {
                set { _droot = value; }
                get { return _droot; }
            }
            /// <summary>
            /// 网盘物理路径
            /// </summary>
            public string Dpath
            {
                set { _dpath = value; }
                get { return _dpath; }
            }
            /// <summary>
            /// 是否可上传
            /// </summary>
            public bool Dupload
            {
                set { _dupload = value; }
                get { return _dupload; }
            }
            /// <summary>
            /// 文件数
            /// </summary>
            public int Dcount
            {
                set { _dcount = value; }
                get { return _dcount; }
            }
            /// <summary>
            /// 网盘文件列表
            /// </summary>
            public DataView Dw
            {
                set { _dw = value; }
                get { return _dw; }
            }
        }

        public static string extGif(string ext)
        {
            string worktype = LearnSite.Common.XmlHelp.GetTypeName("WorksType");
            if (worktype.Contains(ext))
                return ext;
            else
                return "unknown";
        }
    }
}
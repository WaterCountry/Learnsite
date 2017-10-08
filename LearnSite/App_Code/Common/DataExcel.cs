using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Web.UI.WebControls;
namespace LearnSite.Common
{
    /// <summary>
    ///DataToExcel 的摘要说明
    /// </summary>
    public class DataExcel
    {
        public DataExcel()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 把从StudentsExcel表追加到Students中的数据删除掉
        /// 
        /// </summary>
        public static int DropFromStudents()
        {
            int scount = LearnSite.DBUtility.DbHelperSQL.TableCounts("Students");
            string mysql = "DELETE FROM Students where Snum in (select Snum from StudentsExcel)";
            LearnSite.DBUtility.DbHelperSQL.ExecuteSql(mysql);
            System.Threading.Thread.Sleep(1000);
            int acount = LearnSite.DBUtility.DbHelperSQL.TableCounts("Students");

            return acount - scount;

        }
        /// <summary>
        /// 将数据从StudentsExcel表追加到Students中
        /// </summary>
        public static int AppendToStudents()
        {
            int scount = LearnSite.DBUtility.DbHelperSQL.TableCounts("Students");
            string mysql = "INSERT INTO Students (Snum,Syear, Sgrade, Sclass, Sname, Spwd, Sex, Saddress, Sphone, Sparents, Sheadtheacher) SELECT Snum,Syear, Sgrade, Sclass, Sname, Spwd, Sex, Saddress, Sphone, Sparents, Sheadtheacher FROM StudentsExcel";
            LearnSite.DBUtility.DbHelperSQL.ExecuteSql(mysql);
            System.Threading.Thread.Sleep(1000);
            int acount = LearnSite.DBUtility.DbHelperSQL.TableCounts("Students");
            return acount - scount;
        }

        /// <summary>
        /// 上传保存Excel文件，返回保存物理路径
        /// </summary>
        /// <param name="FileUpExcel"></param>
        /// <returns></returns>
        public static string Saveupexcel(FileUpload FileUpExcel)
        {
            if (FileUpExcel.HasFile)
            {
                string excelfile = FileUpExcel.PostedFile.FileName;
                string exceltype = WordProcess.getext(excelfile); 
                string newexcel = "student" + DateTime.Now.Millisecond.ToString();
                string excelpath = HttpContext.Current.Server.MapPath("~/UpExcel/").ToString();
                if (exceltype.ToLower() == "xls")
                {

                    if (!Directory.Exists(excelpath))
                    {
                        Directory.CreateDirectory(excelpath);
                    }
                    string savepath = excelpath + newexcel + "." + exceltype;
                    FileUpExcel.PostedFile.SaveAs(savepath);
                    return savepath;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 将DataSet导入到临时表StudentsExcel中
        /// </summary>
        /// <param name="upexcelpath"></param>
        public static string DataSettoStudentsExcel(DataSet ds, bool pinying)
        {
            string msg = "";
            if (ds != null)
            {
                int count = ds.Tables[0].Rows.Count;
                if (count > 0)
                {
                    int columnscount = ds.Tables[0].Columns.Count;
                    int isright = 0;
                    string[] strColumn = { "学号", "入学年度", "年级", "班级", "姓名", "密码", "性别", "家庭住址", "联系电话", "家长姓名", "班主任" };
                    for (int k = 0; k < columnscount; k++)
                    {
                        string strname = ds.Tables[0].Columns[k].ColumnName;
                        foreach (string str in strColumn)
                        {
                            if (strname == str)
                                isright++;
                        }
                    }
                    if (isright == strColumn.Length)
                    {
                        int wrong = 0;
                        LearnSite.Model.StudentsExcel stu = new LearnSite.Model.StudentsExcel();
                        LearnSite.BLL.StudentsExcel stubll = new LearnSite.BLL.StudentsExcel();
                        for (int i = 0; i < count; i++)
                        {
                            string strnum = ds.Tables[0].Rows[i]["学号"].ToString().Trim();
                            if (LearnSite.Common.WordProcess.IsNum(strnum))
                            {
                                string stryear = ds.Tables[0].Rows[i]["入学年度"].ToString();
                                string strgrade = ds.Tables[0].Rows[i]["年级"].ToString();
                                string strclass = ds.Tables[0].Rows[i]["班级"].ToString();

                                stu.Snum = strnum;
                                stu.Syear = int.Parse(stryear);
                                stu.Sgrade = int.Parse(strgrade);
                                stu.Sclass = int.Parse(strclass);
                                stu.Sname = ds.Tables[0].Rows[i]["姓名"].ToString().Replace(" ", "").Trim();//去空格，以防提交的作品名称找不到路径
                                if (pinying)
                                {
                                    string spellname = LearnSite.Common.Gbk2Spell.Chinese.FirstLetter(stu.Sname);//取姓名的拼音缩写为密码
                                    if (LearnSite.Common.WordProcess.IsEnNum(spellname))
                                    {
                                        stu.Spwd = spellname;//如果缩写为字母或数字则采用
                                    }
                                    else
                                    {
                                        stu.Spwd = ds.Tables[0].Rows[i]["密码"].ToString();//否则用原密码
                                    }
                                }
                                else
                                {
                                    stu.Spwd = ds.Tables[0].Rows[i]["密码"].ToString();
                                }
                                stu.Sex = ds.Tables[0].Rows[i]["性别"].ToString();
                                stu.Saddress = ds.Tables[0].Rows[i]["家庭住址"].ToString();
                                stu.Sphone = ds.Tables[0].Rows[i]["联系电话"].ToString();
                                stu.Sparents = ds.Tables[0].Rows[i]["家长姓名"].ToString();
                                stu.Sheadtheacher = ds.Tables[0].Rows[i]["班主任"].ToString();

                                stubll.AddFromExcelDs(stu);
                            }
                            else
                            {
                                wrong++;
                            }
                        }
                        int right = count - wrong;
                        msg = "成功获取" + right + "条数据到临时表！<br/>请点下一步导入数据，即可将数据导入数据库！";
                        if (wrong > 0)
                            msg = msg + " 学号错误(非数字格式)共" + wrong + "条数据";
                    }
                    else
                    {
                        msg = "Excel数据格式不正确，请参考学生导入模板";
                    }
                }
                else
                {
                    msg = "无数据！";
                }
            }
            else
            {
                msg = "没有选择Excel上传或不是标准的Excel格式，<br/>请用Excel打开再保存试试！";
            }
            return msg;
        }

        /// <summary>
        /// 将Excel绑定到ds中
        /// 返回ds
        /// </summary>
        public static DataSet ExcelToDataSet(string filepath)
        {
            DataSet ds = new DataSet();
            if (File.Exists(filepath))
            {
                string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties='Excel 8.0;IMEX=1'";
                OleDbConnection conn = new OleDbConnection(strCon);
                try
                {
                    conn.Open();
                    DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string tblname = "[" + schemaTable.Rows[0][2].ToString().Trim() + "]";
                    string Sql = "select * from " + tblname;
                    OleDbDataAdapter mycommand = new OleDbDataAdapter(Sql, conn);
                    mycommand.Fill(ds, tblname);
                    conn.Close();
                    return ds;
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 清空临时表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void ClearUserexcel()
        {
            string mysql = "DELETE FROM StudentsExcel";
            LearnSite.DBUtility.DbHelperSQL.ExecuteSql(mysql);
        }
        /// <summary>
        /// excel文件是否已经存在
        /// </summary>
        /// <returns></returns>
        public static bool Isfileup(string filepath)
        {
            if (File.Exists(filepath))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 临时表内记录重复检验
        /// </summary>
        public static bool Checkrepeat()
        {
            string mysql = "select count(1) from StudentsExcel where Snum in (select Snum from StudentsExcel group by Snum having count(Snum) > 1)";
            return LearnSite.DBUtility.DbHelperSQL.Exists(mysql);
        }
        /// <summary>
        /// 临时表内记录重复检验
        /// </summary>
        public static DataSet CheckrepeatDs()
        {
            string mysql = "select Snum,count(Snum) as Scount from StudentsExcel group by Snum having count(Snum) > 1";
            return LearnSite.DBUtility.DbHelperSQL.Query(mysql);
        }
        /// <summary>
        /// 检验临时表与原用户表是否有重复记录
        /// 返回值为真有重复，为假无重复
        /// </summary>
        /// <returns></returns>
        public static bool Checkrepeatall()
        {
            string mysql = "select count(1) Snum from Students where Snum  in ( select Snum from StudentsExcel)";
            return LearnSite.DBUtility.DbHelperSQL.Exists(mysql);
        }
        /// <summary>
        /// 返回有平台中有数据重复的班级学生
        /// </summary>
        /// <returns></returns>
        public static DataTable Checkrepeatallds()
        {
            string mysql = "select Snum,Sgrade,Sclass from Students where Snum  in ( select Snum from StudentsExcel)";
            return LearnSite.DBUtility.DbHelperSQL.Query(mysql).Tables[0];
        }
        public static void DataSetToExcel(DataSet ds, string FileName)
        {
            try
            {
                HttpResponse resp;
                resp = HttpContext.Current.Response;
                resp.Buffer = true;
                resp.Charset = "gb2312";
                resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                resp.AppendHeader("Content-disposition", "attachment;filename=" + FileName + ".xls");
                resp.ContentType = "application/ms-excel";

                //变量定义 
                string colHeaders = null;
                string Is_item = null;

                //显示格式定义//////////////// 


                //文件流操作定义 
                //FileStream fs=new FileStream(FileName,FileMode.Create,FileAccess.Write); 
                //StreamWriter sw=new StreamWriter(fs,System.Text.Encoding.GetEncoding("GB2312")); 

                StringWriter sfw = new StringWriter();
                //定义表对象与行对象，同时用DataSet对其值进行初始化 
                System.Data.DataTable dt = ds.Tables[0];
                DataRow[] myRow = dt.Select();
                int i = 0;
                int cl = dt.Columns.Count;

                //取得数据表各列标题，各标题之间以\t分割，最后一个列标题后加回车符 
                for (i = 0; i < cl; i++)
                {
                    //if(i==(cl-1))  //最后一列，加\n 
                    // colHeaders+=dt.Columns[i].Caption.ToString(); 
                    //else 
                    colHeaders += dt.Columns[i].Caption.ToString() + "\t";
                }
                sfw.WriteLine(colHeaders);
                //sw.WriteLine(colHeaders); 

                //逐行处理数据 
                foreach (DataRow row in myRow)
                {
                    //当前数据写入 
                    for (i = 0; i < cl; i++)
                    {
                        //if(i==(cl-1)) 
                        //   Is_item+=row[i].ToString()+"\n"; 
                        //else 
                        Is_item += row[i].ToString() + "\t";
                    }
                    sfw.WriteLine(Is_item);
                    //sw.WriteLine(Is_item); 
                    Is_item = null;
                }
                resp.Write(sfw);
                //resp.Clear(); 
                resp.End();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 将Excel绑定到ds中
        /// 返回ds
        /// </summary>
        private static DataSet ExcelEnToDataSet()
        {
            string filepath = HttpContext.Current.Server.MapPath("~/en.xls");
            DataSet ds = new DataSet();
            if (File.Exists(filepath))
            {
                string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties='Excel 8.0;IMEX=1'";
                OleDbConnection conn = new OleDbConnection(strCon);
                conn.Open();
                DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string tblname = "[" + schemaTable.Rows[0][2].ToString().Trim() + "]";
                string Sql = "select * from " + tblname;
                OleDbDataAdapter mycommand = new OleDbDataAdapter(Sql, conn);
                mycommand.Fill(ds, tblname);
                conn.Close();
                return ds;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 将DataSet导入到英文词典表English中
        /// </summary>
        /// <param name="upexcelpath"></param>
        public static string DataSettoEnglish()
        {
            DataSet ds = ExcelEnToDataSet();
            string msg = "";
            if (ds != null)
            {
                int count = ds.Tables[0].Rows.Count;
                int columnscount = ds.Tables[0].Columns.Count;
                int isright = 0;
                string[] strColumn = { "Eword", "Emeaning","Elevel" };
                for (int k = 0; k < columnscount; k++)
                {
                    string strname = ds.Tables[0].Columns[k].ColumnName;
                    foreach (string str in strColumn)
                    {
                        if (strname == str)
                            isright++;
                    }
                }
                if (isright == strColumn.Length)
                {
                    if (count > 0)
                    {
                        LearnSite.Model.English emodel = new Model.English();
                        LearnSite.BLL.English ebll = new BLL.English();
                        int right = 0;
                        for (int i = 0; i < count; i++)
                        {
                            string Eword = ds.Tables[0].Rows[i]["Eword"].ToString();//英语单词
                            string Emeaning = ds.Tables[0].Rows[i]["Emeaning"].ToString();
                            string Elevelstr = ds.Tables[0].Rows[i]["Elevel"].ToString();
                            int Elevel = 9;
                            if (Elevelstr != "")
                                Elevel = Int32.Parse(Elevelstr);
                            if (Eword != "")
                            {
                                emodel.Eword = Eword;
                                emodel.Emeaning = Emeaning.Replace("<br>","<br />");
                                emodel.Elevel = Elevel;
                                if (ebll.Add(emodel) > 0)
                                {
                                    right++;
                                }
                            }
                        }
                        msg = "Excel表格数据共" + count + "条，导入数据库成功共" + right + "条";
                    }
                    else
                    {
                        msg = "无数据！";
                    }
                }
                else
                {
                    msg = "Excel数据格式不正确，请导入原en.xls";
                }
            }
            else
            {
                msg = "网站UpExcel目录下无en.xls文件！";
            }
            return msg;
        }


        private static DataSet ExcelCzToDataSet()
        {
            string filepath = HttpContext.Current.Server.MapPath("~/xx.xls");
            DataSet ds = new DataSet();
            if (File.Exists(filepath))
            {
                string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties='Excel 8.0;IMEX=1'";
                OleDbConnection conn = new OleDbConnection(strCon);
                conn.Open();
                DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string tblname = "[" + schemaTable.Rows[0][2].ToString().Trim() + "]";
                string Sql = "select * from " + tblname;
                OleDbDataAdapter mycommand = new OleDbDataAdapter(Sql, conn);
                mycommand.Fill(ds, tblname);
                conn.Close();
                return ds;
            }
            else
            {
                return null;
            }
        }

        public static string DataSetCztoEnglish()
        {
            DataSet ds = ExcelCzToDataSet();
            string msg = "";
            if (ds != null)
            {
                int count = ds.Tables[0].Rows.Count;
                int columnscount = ds.Tables[0].Columns.Count;
                int isright = 0;
                string[] strColumn = { "Eword" };
                for (int k = 0; k < columnscount; k++)
                {
                    string strname = ds.Tables[0].Columns[k].ColumnName;
                    foreach (string str in strColumn)
                    {
                        if (strname == str)
                            isright++;
                    }
                }
                if (isright == strColumn.Length)
                {
                    if (count > 0)
                    {
                        LearnSite.BLL.English ebll = new BLL.English();
                        int Elevel = 0;
                        for (int i = 0; i < count; i++)
                        {
                            string Eword = ds.Tables[0].Rows[i]["Eword"].ToString();//英语单词
                            if(Eword!="")
                            ebll.UpdateElevel(Eword, Elevel);
                        }
                        msg = "Excel表格数据共" + count + "条，导入数据库成功共" + ebll.CountLevel(Elevel) + "条";
                    }
                    else
                    {
                        msg = "无数据！";
                    }
                }
                else
                {
                    msg = "Excel数据格式不正确，请导入原cz.xls";
                }
            }
            else
            {
                msg = "网站UpExcel目录下无en.xls文件！";
            }
            return msg;
        }

        /// <summary>
        /// 将Excel绑定到ds中
        /// 返回ds
        /// </summary>
        public static DataSet ExcelHostnameToDataSet( string filepath)
        {           
            DataSet ds = new DataSet();
            if (File.Exists(filepath))
            {
                string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties='Excel 8.0;IMEX=1'";
                OleDbConnection conn = new OleDbConnection(strCon);
                conn.Open();
                DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string tblname = "[" + schemaTable.Rows[0][2].ToString().Trim() + "]";
                string Sql = "select * from " + tblname;
                OleDbDataAdapter mycommand = new OleDbDataAdapter(Sql, conn);
                mycommand.Fill(ds, tblname);
                conn.Close();
                return ds;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 上传保存Excel文件，返回保存物理路径
        /// </summary>
        /// <param name="FileUpExcel"></param>
        /// <returns></returns>
        public static string SaveIpExcel(FileUpload FileUpExcel)
        {

            string excelfile = FileUpExcel.PostedFile.FileName;
            string exceltype = excelfile.Substring(excelfile.LastIndexOf(".") + 1);
            string newexcel = "Ip" + DateTime.Now.Millisecond.ToString();
            string excelpath = HttpContext.Current.Server.MapPath("~/UpExcel/").ToString();
            if (exceltype == "xls")
            {
                if (!Directory.Exists(excelpath))
                {
                    Directory.CreateDirectory(excelpath);
                }
                string savepath = excelpath + newexcel + "." + exceltype;
                FileUpExcel.PostedFile.SaveAs(savepath);
                return savepath;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 上传保存Excel文件，返回保存物理路径
        /// </summary>
        /// <param name="FileUpExcel"></param>
        /// <returns></returns>
        public static string SaveHostnameExcel(FileUpload FileUpExcel)
        {

            string excelfile = FileUpExcel.PostedFile.FileName;
            string exceltype = excelfile.Substring(excelfile.LastIndexOf(".") + 1);
            string newexcel = "hostname"+DateTime.Now.Millisecond.ToString();
            string excelpath = HttpContext.Current.Server.MapPath("~/UpExcel/").ToString();
            if (exceltype == "xls")
            {
                if (!Directory.Exists(excelpath))
                {
                    Directory.CreateDirectory(excelpath);
                }
                string savepath = excelpath + newexcel + "." + exceltype;
                FileUpExcel.PostedFile.SaveAs(savepath);
                return savepath;
            }
            else
            {
                return "";
            }
        }
        public static string DataSettoIps(string savepath,int Ihid)
        {
            DataSet ds = ExcelHostnameToDataSet(savepath);
            string msg = "";
            if (ds != null)
            {
                int count = ds.Tables[0].Rows.Count;
                int columnscount = ds.Tables[0].Columns.Count;
                int isright = 0;
                string[] strColumn = { "电脑编号", "IP列表" };
                for (int k = 0; k < columnscount; k++)
                {
                    string strname = ds.Tables[0].Columns[k].ColumnName;
                    foreach (string str in strColumn)
                    {
                        if (strname == str)
                            isright++;
                    }
                }
                if (isright == strColumn.Length)
                {
                    if (count > 0)
                    {
                        Model.Ip model = new Model.Ip();
                        BLL.Ip bll = new BLL.Ip();
                        int right = 0;
                        DateTime dt = DateTime.Now;
                        for (int i = 0; i < count; i++)
                        {
                            string inum = ds.Tables[0].Rows[i]["电脑编号"].ToString().Trim();
                            string iip = ds.Tables[0].Rows[i]["IP列表"].ToString().Trim();

                            if (LearnSite.Common.WordProcess.IsNum(inum) && !string.IsNullOrEmpty(iip))
                            {
                                model.Ihid = Ihid;
                                model.Inum = Int32.Parse(inum);
                                model.Iip = iip;
                                if (bll.Add(model)>0)
                                    right++;//增加一条IP
                            }
                        }
                        int repc = count - right;
                        msg = "Excel表格数据共" + count + "条，导入数据库成功共" + right + "条，重复" + repc + "条";
                    }
                    else
                    {
                        msg = "无数据！";
                    }
                }
                else
                {
                    msg = "Excel数据格式不正确，请导入电脑编号和IP列表对应的Excel表格";
                }
            }
            else
            {
                msg = "上传的文件找不到！";
            }
            return msg;
        }
        public static string DataSettoComputers(string savepath)
        {
            DataSet ds = ExcelHostnameToDataSet(savepath);
            string msg = "";
            if (ds != null)
            {
                int count = ds.Tables[0].Rows.Count;
                int columnscount = ds.Tables[0].Columns.Count;
                int isright = 0;
                string[] strColumn = { "ip", "hostname" };
                for (int k = 0; k < columnscount; k++)
                {
                    string strname = ds.Tables[0].Columns[k].ColumnName;
                    foreach (string str in strColumn)
                    {
                        if (strname == str)
                            isright++;
                    }
                }
                if (isright == strColumn.Length)
                {
                    if (count > 0)
                    {
                        Model.Computers cmodel = new Model.Computers();
                        BLL.Computers cbll = new BLL.Computers();
                        int right = 0;
                        DateTime dt=DateTime.Now;
                        for (int i = 0; i < count; i++)
                        {
                            string Pip = ds.Tables[0].Rows[i]["ip"].ToString();
                            string Pmachine = ds.Tables[0].Rows[i]["hostname"].ToString();
                            
                            if (!string.IsNullOrEmpty(Pip) &&!string.IsNullOrEmpty(Pmachine))
                            {
                                cmodel.Pdate = dt;
                                cmodel.Pip = Pip;
                                cmodel.Plock = true;
                                cmodel.Pmachine = Pmachine;

                                if (!cbll.ExistsIp(Pip))
                                {
                                    if (cbll.Add(cmodel) > 0)
                                    {
                                        right++;
                                    }
                                }
                                else
                                {
                                    if (cbll.UpdateIp(Pip, Pmachine))
                                    {
                                        right++;
                                    }
                                }

                            }
                        }
                        int repc=count-right;
                        msg = "Excel表格数据共" + count + "条，导入数据库成功共" + right + "条，重复"+repc+"条";
                    }
                    else
                    {
                        msg = "无数据！";
                    }
                }
                else
                {
                    msg = "Excel数据格式不正确，请导入ip和hostname对应的Excel表格";
                }
            }
            else
            {
                msg = "网站UpExcel目录下无hostname.xls文件！";
            }
            return msg;
        }
        /// <summary>
        /// 上传保存Excel文件，返回保存物理路径
        /// </summary>
        /// <param name="FileUpExcel"></param>
        /// <returns></returns>
        public static string SavekaoxuExcel(FileUpload FileUpExcel)
        {
            string excelfile = FileUpExcel.PostedFile.FileName;
            string exceltype = excelfile.Substring(excelfile.LastIndexOf(".") + 1);
            string newexcel = "kaoxu" + DateTime.Now.Millisecond.ToString();
            string excelpath = HttpContext.Current.Server.MapPath("~/UpExcel/").ToString();
            if (exceltype == "xls")
            {

                if (!Directory.Exists(excelpath))
                {
                    Directory.CreateDirectory(excelpath);
                }
                string newfilename = excelpath + newexcel + "." + exceltype;
                FileUpExcel.PostedFile.SaveAs(newfilename);
                return newfilename;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 将DataSet导入到临时表StudentsExcel中
        /// </summary>
        /// <param name="upexcelpath"></param>
        public static string KaoxutoStudents(DataSet ds)
        {
            string msg = "";
            if (ds != null)
            {
                int count = ds.Tables[0].Rows.Count;
                if (count > 0)
                {
                    int columnscount = ds.Tables[0].Columns.Count;
                    int isright = 0;
                    string[] strColumn = {"报名序号", "姓名"};
                    for (int k = 0; k < columnscount; k++)
                    {
                        string strname = ds.Tables[0].Columns[k].ColumnName;
                        foreach (string str in strColumn)
                        {
                            if (strname == str)
                                isright++;
                        }
                    }
                    if (isright == strColumn.Length)
                    {
                        LearnSite.BLL.Students stubll = new LearnSite.BLL.Students();
                        for (int i = 0; i < count; i++)
                        {
                            string kaoxu = ds.Tables[0].Rows[i]["报名序号"].ToString().Trim();
                            string sname = ds.Tables[0].Rows[i]["姓名"].ToString().Replace(" ", "").Trim();
                            stubll.UpdateKaoxu(kaoxu, sname);
                        }
                    }
                    else
                    {
                        msg = "Excel数据格式不正确";
                    }
                }
                else
                {
                    msg = "无数据！";
                }
            }
            else
            {
                msg = "没有选择Excel上传或不是标准的Excel格式，<br/>请用Excel打开再保存试试！";
            }
            return msg;
        }
        /// <summary>
        /// 上传保存Excel文件，返回保存物理路径
        /// </summary>
        /// <param name="FileUpExcel"></param>
        /// <returns></returns>
        public static string SavedivideExcel(FileUpload FileUpExcel)
        {
            string excelfile = FileUpExcel.PostedFile.FileName;
            string exceltype = excelfile.Substring(excelfile.LastIndexOf(".") + 1);
            string newexcel = "divide"+DateTime.Now.Millisecond.ToString();
            string excelpath = HttpContext.Current.Server.MapPath("~/UpExcel/").ToString();
            if (exceltype == "xls")
            {

                if (!Directory.Exists(excelpath))
                {
                    Directory.CreateDirectory(excelpath);
                }
                string newfilename = excelpath + newexcel + "." + exceltype;
                FileUpExcel.PostedFile.SaveAs(newfilename);
                return newfilename;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 将Divide Excel绑定到ds中
        /// 返回ds
        /// </summary>
        private static DataSet DivideExcelToDS(string filepath)
        {
            DataSet ds = new DataSet();
            if (File.Exists(filepath))
            {
                string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties='Excel 8.0;IMEX=1'";
                OleDbConnection conn = new OleDbConnection(strCon);
                conn.Open();
                DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string tblname = "[" + schemaTable.Rows[0][2].ToString().Trim() + "]";
                string Sql = "select * from " + tblname;
                OleDbDataAdapter mycommand = new OleDbDataAdapter(Sql, conn);
                mycommand.Fill(ds, tblname);
                conn.Close();
                return ds;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 使用上传的分班表格，对原来的学生进行重新分班
        /// </summary>
        /// <param name="upexcelpath"></param>
        public static string DivideClass(string filepath)
        {
            DataSet ds = DivideExcelToDS(filepath);
            string msg = "";
            if (ds != null)
            {
                int count = ds.Tables[0].Rows.Count;
                int columnscount = ds.Tables[0].Columns.Count;
                int isright = 0;
                string[] strColumn = { "年级", "班级", "姓名" };
                for (int k = 0; k < columnscount; k++)
                {
                    string strname = ds.Tables[0].Columns[k].ColumnName;
                    foreach (string str in strColumn)
                    {
                        if (strname == str)
                            isright++;
                    }
                }
                if (isright == strColumn.Length)
                {
                    if (count > 0)
                    {
                        LearnSite.BLL.Students sbll = new BLL.Students();
                        int right = 0;
                        int formatwrong = 0;
                        for (int i = 0; i < count; i++)
                        {
                            string Sgrade = ds.Tables[0].Rows[i]["年级"].ToString().Trim();
                            string Sclass = ds.Tables[0].Rows[i]["班级"].ToString().Trim();
                            string Sname = ds.Tables[0].Rows[i]["姓名"].ToString().Trim();
                            if (LearnSite.Common.WordProcess.IsNum(Sgrade) && LearnSite.Common.WordProcess.IsNum(Sclass))
                            {
                                if (sbll.UpdateDivide(Int32.Parse(Sgrade), Int32.Parse(Sclass), Sname))
                                {
                                    right++;
                                }
                            }
                            else
                            {
                                formatwrong++;
                            }

                        }
                        msg = "Excel表格数据共" + count + "位，重新分班成功共" + right + "位<br />年级和班级格式错误共" + formatwrong + "位";
                    }
                    else
                    {
                        msg = "无数据！";
                    }
                }
                else
                {
                    msg = "Excel数据格式不正确!";
                }
            }
            else
            {
                msg = "没有上传分班表格divide.xls！";
            }
            return msg;
        }

    }
}
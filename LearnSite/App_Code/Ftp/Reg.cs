using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
namespace LearnSite.Ftp
{
    /// <summary>
    ///Reg 的摘要说明
    /// </summary>
    public class Reg
    {
        static string Savepath = HttpContext.Current.Server.MapPath("~/FtpSpace/");//用户目录的存放的位置,真实物理路径， 以“\”结尾
        static string ServAccess = "|RWAMLCDP";//用户目录的权限
        //static bool QuotaEnable = true;//是否限制空间大小
        //static int QuotaMax = 1048576 * 30;//限制空间大小(单位:M)。当开启限制空间大小功能时，才有效
        static int PassType = 0;//密码保存类型0: 规则密码
        //static string Email = "wzsyzx@126.com";
        //static bool Expirationtime = false;//是否限制使用时间
        static DateTime Expiration = DateTime.Now.AddYears(3);//'帐号到期后的处理：0为删除，1为禁用。当开启限制使用时间功能时，才有效
        static int Expirationtype = 1;//帐号使用期限天数（单位:年）当开启限制使用时间功能时，才有效
        public Reg()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 删除单个Ftp用户
        /// </summary>
        /// <param name="User"></param>
        public static void DeleteOne(string User)
        {
            string mysql = "delete from User_accounts where User='" + User + "'";
            LearnSite.Ftp.FtpDb.ExecuteSql(mysql);
        }
        /// <summary>
        /// 学年升级时，删除Ftp表中不存在Students表中用户的账号
        /// </summary>
        public static void Upgrade()
        {
            string mysql = "select Wnum from Webstudy where Wnum not in (select Snum from Students)";
            DataSet ds = DBUtility.DbHelperSQL.GetDataSet(mysql);
            int countis = ds.Tables[0].Rows.Count;
            if (countis > 0)
            {
                for (int i = 0; i < countis; i++)
                {
                    string Wnum = ds.Tables[0].Rows[i]["Wnum"].ToString();
                    DeleteOne(Wnum);
                }
            }
        }

        /// <summary>
        /// //Students学生表中读取记录，如果Ftp中不存在则添加账户
        /// 批量Ftp用户注册专用方式二
        /// </summary>
        /// <returns></returns>
        public static string RegAllFtpTwo(int QuotaMax)
        {
            string mysql = "SELECT Snum,Syear,Sclass,Spwd FROM Students ";//从学生表中读取导入用户数据
            DataSet ds = DBUtility.DbHelperSQL.GetDataSet(mysql);
            int countis = ds.Tables[0].Rows.Count;
            int createcount = 0;
            int existcount = 0;
            if (countis > 0)
            {
                for (int i = 0; i < countis; i++)
                {
                    string Snum = ds.Tables[0].Rows[i]["Snum"].ToString();
                    string Syear = ds.Tables[0].Rows[i]["Syear"].ToString();
                    string Sclass = ds.Tables[0].Rows[i]["Sclass"].ToString();
                    string Spwd = ds.Tables[0].Rows[i]["Spwd"].ToString();
                    if (RegsaveFtp(Snum, Spwd, Syear, Sclass))
                    {
                        createcount++;
                    }
                    else
                    {
                        existcount++;
                    }
                }
            }

            string ftpsql = "UPDATE User_accounts SET [QuotaEnable]=1 ,[QuotaMax]=" + QuotaMax + ", [PassType]=" + PassType + ",[Expiration]='" + Expiration + "',[Expirationtype]=" + Expirationtype;
            LearnSite.Ftp.FtpDb.ExecuteSql(ftpsql);
            ds.Dispose();
            string msg = " 要求创建" + countis + "个用户|已生成" + createcount + "个新用户|已存在" + existcount + "用户";
            return msg;
        }

        /// <summary>
        /// //学生表中读取Webstudy表中不存在的数据  导入到Ftp表中,并在Ftp表中创建新用户
        /// 批量Ftp用户注册专用
        /// </summary>
        /// <returns></returns>
        public static string RegAllFtp(int QuotaMax)
        {
            string mysql = "SELECT Snum,Syear,Sclass,Spwd FROM Students where  Snum not in(select Wnum from Webstudy)";//从学生表中读取导入用户数据
            DataSet ds = DBUtility.DbHelperSQL.GetDataSet(mysql);
            int countis = ds.Tables[0].Rows.Count;
            int createcount = 0;
            int existcount = 0;
            if (countis > 0)
            {
                for (int i = 0; i < countis; i++)
                {
                    string Snum = ds.Tables[0].Rows[i]["Snum"].ToString();
                    string Syear = ds.Tables[0].Rows[i]["Syear"].ToString();
                    string Sclass = ds.Tables[0].Rows[i]["Sclass"].ToString();
                    string Spwd = ds.Tables[0].Rows[i]["Spwd"].ToString();
                    if (RegsaveFtp(Snum, Spwd, Syear, Sclass))
                    {
                        createcount++;
                    }
                    else
                    {
                        existcount++;
                    }
                }
            }

            string ftpsql = "UPDATE User_accounts SET [QuotaEnable]=1 ,[QuotaMax]=" + QuotaMax + ", [PassType]=" + PassType + ",[Expiration]='" + Expiration + "',[Expirationtype]=" + Expirationtype;
            LearnSite.Ftp.FtpDb.ExecuteSql(ftpsql);
            System.Threading.Thread.Sleep(500);
            string msg = " 要求创建" + countis + "个用户|已生成" + createcount + "个新用户|已存在" + existcount + "用户";
            return msg;
        }
        /// <summary>
        /// 注册一个新Ftp用户，成功返回真，已存在返回假
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userpwd"></param>
        /// <param name="userclass"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static bool RegsaveFtp(string Snum, string Spwd, string Syear, string Sclass)
        {
            bool iscreate = false;
            string mysql = "select id from User_accounts where [User]='" + Snum + "'";
            if (!LearnSite.Ftp.FtpDb.ExecuteSqlExist(mysql))
            {
                string classpath = Savepath + Syear + "\\" + Sclass;
                string HomeDir = classpath + "\\" + Snum;
                string Access = HomeDir + ServAccess;
                string Password = LearnSite.Common.WordProcess.Serv_u_Md5(Spwd);
                string strsql = "INSERT INTO User_accounts ([User], [Access], [Password], [HomeDir]) VALUES ('" + Snum + "','" + Access + "','" + Password + "','" + HomeDir + "')";
                iscreate = true;//不存在该用户则创建
                LearnSite.Ftp.FtpDb.ExecuteSql(strsql);
                LearnSite.BLL.Webstudy wbll = new BLL.Webstudy();
                wbll.UpdateWpwd2(Snum, Spwd);
            }
            return iscreate;
        }
        /// <summary>
        /// 根据学号，修改Ftp账号中的地址和目录权限
        /// </summary>
        /// <param name="Snum"></param>
        /// <param name="Syear"></param>
        /// <param name="Sclass"></param>
        public static void RegEditFtp(string Snum, string Syear, string Sclass)
        {
            string classpath = Savepath + Syear + "\\" + Sclass;
            string User = Snum;
            string HomeDir = classpath + "\\" + User;
            string Access = HomeDir + ServAccess;
            string str = "UPDATE User_accounts  SET [Access] = '" + Access + "',[HomeDir]='" + HomeDir + "' WHERE [User] = '" + Snum + "'";
            LearnSite.Ftp.FtpDb.ExecuteSql(str);
        }
        /// <summary>
        /// 查询Ftp用户表
        /// </summary>
        /// <returns></returns>
        public static DataSet ListFtp()
        {
            string mysql = "SELECT id,[User],HomeDir,Expiration,QuotaMax,QuotaCurrent FROM User_accounts";
            return LearnSite.Ftp.FtpDb.Query(mysql);
        }

        /// <summary>
        /// 查询Ftp用户表密码
        /// </summary>
        /// <returns></returns>
        public static string FindFtpPwd(string User)
        {
            string mysql = "SELECT Password FROM User_accounts WHERE [User]='" + User + "'";
            return LearnSite.Ftp.FtpDb.FindString(mysql);
        }

        /// <summary>
        /// 更新serv_u中用户密码
        /// </summary>
        /// <param name="username"></param>
        /// <param name="newpwd"></param>
        public static void Updatepwd(string username, string newpwd)
        {
            newpwd = LearnSite.Common.WordProcess.Serv_u_Md5(newpwd);
            string str = "UPDATE User_accounts  SET [Password] = '" + newpwd + "' WHERE [User] = '" + username + "'";
            LearnSite.Ftp.FtpDb.ExecuteSql(str);
           // System.Threading.Thread.Sleep(200); 不知道当时延时为何
        }

        /// <summary>
        /// 从Ftp表中读取空间占用情况到Web表中
        /// </summary>
        public static void WebUserSpace()
        {
            string sql = "SELECT [User],QuotaCurrent FROM User_accounts";
            DataSet ds = LearnSite.Ftp.FtpDb.Query(sql);
            int counts = ds.Tables[0].Rows.Count;
            for (int i = 0; i < counts; i++)
            {
                string qct = ds.Tables[0].Rows[i]["QuotaCurrent"].ToString();
                if (!string.IsNullOrEmpty(qct))
                {
                    string User = ds.Tables[0].Rows[i]["User"].ToString();
                    int QuotaCurrent = Int32.Parse(qct);

                    string Qsql = "UPDATE Webstudy SET WquotaCurrent =" + QuotaCurrent + " Where Wnum='" + User + "'";
                    LearnSite.DBUtility.DbHelperSQL.ExecuteSql(Qsql);
                }
            }
        }

        /// <summary>
        /// 批量随机更改Ftp及Web用户密码,无参数
        /// </summary>
        /// <param name="conn"></param>
        public static string UpdatePwdRandom(int pwdlen, string pwdmethod)
        {
            string mysql = "SELECT [Wnum] FROM Webstudy";
            DataSet ds = LearnSite.DBUtility.DbHelperSQL.Query(mysql);
            int countis = ds.Tables[0].Rows.Count;

            string newRad = "12345";
            string strmsg = "未创建Ftp账号,批量更新密码无效！";
            if (countis > 0)
            {
                for (int i = 0; i < countis; i++)
                {
                    string User = ds.Tables[0].Rows[i]["Wnum"].ToString();
                    newRad = LearnSite.Common.WordProcess.GetRandomSelect(pwdlen, pwdmethod);

                    string newPwd = LearnSite.Common.WordProcess.Serv_u_Md5(newRad);

                    string strftp = "UPDATE User_accounts  SET [Password] = '" + newPwd + "' WHERE [User] = '" + User + "'";
                    LearnSite.Ftp.FtpDb.ExecuteSql(strftp);
                    string strweb = "UPDATE Webstudy  SET [Wpwd] = '" + newRad + "' WHERE [Wnum] = '" + User + "'";
                    LearnSite.DBUtility.DbHelperSQL.ExecuteSql(strweb);
                }
                strmsg = "批量更新密码的账号数目为" + countis.ToString() + "位";
            }
            return strmsg;
        }

        /// <summary>
        /// 同步Ftp及Web用户密码为个人登录密码
        /// </summary>
        /// <param name="conn"></param>
        public static string UpdateStuPwd(string Hid)
        {
            string mysqlone = "update Webstudy set Wpwd=Spwd from Students,Webstudy,Room where Wnum=Snum and Sgrade=Rgrade and Sclass=Rclass and Rhid=" + Hid;
            LearnSite.DBUtility.DbHelperSQL.ExecuteSql(mysqlone);//同步

            string mysql = "SELECT Snum,Spwd FROM Students";
            DataSet ds = LearnSite.DBUtility.DbHelperSQL.Query(mysql);
            int countis = ds.Tables[0].Rows.Count;
            string strmsg = "同步0个账号密码";
            if (countis > 0)
            {
                for (int i = 0; i < countis; i++)
                {
                    string User = ds.Tables[0].Rows[i]["Snum"].ToString();
                    string Pwd = ds.Tables[0].Rows[i]["Spwd"].ToString();

                    string newPwd = LearnSite.Common.WordProcess.Serv_u_Md5(Pwd);

                    string strftp = "UPDATE User_accounts  SET [Password] = '" + newPwd + "' WHERE [User] = '" + User + "'";
                    LearnSite.Ftp.FtpDb.ExecuteSql(strftp);
                }
                strmsg = "同步所教班级ftp密码为个人登录密码共" + countis.ToString() + "位";
            }
            return strmsg;
        }
    }
}
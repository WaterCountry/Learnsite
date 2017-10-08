using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Drawing;
using LearnSite.Common.DXFImport;

namespace LearnSite.Common
{
    /// <summary>
    ///WordProcess 的摘要说明
    /// </summary>
    public class WordProcess
    {
        private static Regex RegEmail = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$");//w 英文字母或数字的字符串，和 [a-zA-Z0-9] 语法一样 
        private static Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");//检测是否包含有中文字符
        private static Regex RegUsername = new Regex(@"^[A-Za-z0-9]+$");//检测是否英文字母或数字
        private static Regex RegEnglish = new Regex(@"^[A-Za-z]+$");//检测是否英文字母或数字
        private static Regex RegNum = new Regex(@"^[0-9]+$");//检测是否数字
        private static Regex RegCnEnNum = new Regex("^[a-zA-Z0-9_\u4e00-\u9fa5]+$");//只含有汉字、数字、字母、下划线，下划线位置不限
        private static Regex RegIsNum = new Regex("^-?(0|([0].[0-9]*[1-9])|([1-9]+((.[0-9]*[1-9])|([0-9]*))))$");//只允许数字（正数、负数、小数、0）
        public WordProcess()
        {
            //TODO: 在此处添加构造函数逻辑
        }
        public static string SysVerUpdate()
        {
            return "2017-9-16";
        }
        public static string SystemVersion()
        {
            return " 版本" + NewVersion();
        }
        public static string NewVersion()
        {
            return "v1.250";
        }
        /// 旧版本 √
        /// <summary>
        /// 弹出JavaScript小窗口
        /// </summary>
        /// <param name="js">窗口信息</param>
        public static void jsdialog(string message)
        {
            string js = @"<Script language='JavaScript'>
                     alert('" + message + "');</Script>";
            HttpContext.Current.Response.Write(js);
        }
        /// <summary>
        /// 弹出JavaScript小窗口
        /// </summary>
        /// <param name="js">窗口信息</param>
        public static void Alert(string message, System.Web.UI.Page page)
        {
            string js = "alert('"+message+"');";
            page.ClientScript.RegisterStartupScript(page.GetType(),"", js, true);
        }
        /// <summary>
        /// 弹出JavaScript小窗口
        /// </summary>
        /// <param name="js">窗口信息</param>
        public static void AlertOld(string message, System.Web.UI.Page page)
        {
            string js = @"<Script language='JavaScript'>
                     alert('" + message + "');</Script>";
            if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "alert"))
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "alert", js);
            }
        }
        /// <summary>
        /// 弹出窗口并重定向到其它页面
        /// </summary>
        /// <param name="m">提示信息内容</param>
        /// <param name="u">重定向到的页面</param>
        public static void AlertJump(string message, string url, System.Web.UI.Page page)
        {
            string js = @"<Script language='JavaScript'>
                     alert('" + message + "');window.location.href='" + url + "';</Script>";
            if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "alert"))
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "alert", js);
            }
        }
        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        public static void CloseWindow()
        {
            string js = @"<Script language='JavaScript'>
                    window.close();  
                  </Script>";
            HttpContext.Current.Response.Write(js);
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 刷新父窗口
        /// </summary>
        public static void RefreshParent()
        {
            string js = @"<Script language='JavaScript'>
                    parent.location.reload();
                  </Script>";
            HttpContext.Current.Response.Write(js);
        }
        /// <summary>
        /// 只含有汉字、数字、字母、下划线，下划线位置不限
        /// </summary>
        /// <param name="StrInput"></param>
        /// <returns></returns>
        public static bool IsCnEnNum(string StrInput)
        {
            Match m = RegCnEnNum.Match(StrInput);
            return m.Success;
        }
        /// <summary>
        /// 是否数字
        /// </summary>
        /// <param name="StrInput"></param>
        /// <returns></returns>
        public static bool IsNum(string StrInput)
        {
            Match m = RegNum.Match(StrInput);
            return m.Success;
        }
        /// <summary>
        /// 允许数字（正数、负数、小数、0）
        /// </summary>
        /// <param name="StrInput"></param>
        /// <returns></returns>
        public static bool IsIntNum(string StrInput)
        {
            Match m = RegIsNum.Match(StrInput);
            return m.Success;
        }
        /// <summary>
        /// 检查字符串是否超过指定的最大长度
        /// </summary>
        /// <param name="sqlInput">输入字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns></returns>			
        public static bool StrLength(string StrInput)
        {
            if (StrInput.Length > 8)
            {
                return false;//超过8个字符，返回假
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 检测是否是英文字母
        /// </summary>
        /// <param name="StrInput"></param>
        /// <returns></returns>
        public static bool IsEnglish(string StrInput)
        {
            Match m = RegEnglish.Match(StrInput);
            return m.Success;
        }
        /// <summary>
        /// 检测是否有中文字符
        /// </summary>
        /// <param name="StrInput"></param>
        /// <returns></returns>
        public static bool IsZh(string StrInput)
        {
            Match m = RegCHZN.Match(StrInput);
            return m.Success;
        }
        /// <summary>
        /// 检测是否英文字母或数字
        /// </summary>
        /// <param name="StrInput"></param>
        /// <returns></returns>
        public static bool IsEnNum(string StrInput)
        {
            Match m = RegUsername.Match(StrInput);
            return m.Success;
        }

        /// <summary>
        /// MD5加密处理
        /// </summary>
        /// <param name="toHash"></param>
        /// <returns></returns>
        public static string Hash(string toHash)
        {
            MD5CryptoServiceProvider crypto = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.UTF7.GetBytes(toHash);
            bytes = crypto.ComputeHash(bytes);
            StringBuilder sb = new StringBuilder();
            foreach (byte num in bytes)
            {
                sb.AppendFormat("{0:x2}", num);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 根据配置对指定字符串进行 MD5 加密
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetMD5(string s)
        {
            s = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5").ToString();
            return s.ToLower();
        }
        /// <summary>
        /// 根据配置对指定字符串进行 MD5 加密取8位
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetMD5_8bit(string s)
        {
            s = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5").ToString();
            s = s.Substring(0, 8);
            return s.ToLower();
        }
        /// <summary>
        /// 根据配置对指定字符串进行 MD5 加密取n位；注意不要超过原加密后长度
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetMD5_Nbit(string s,int n)
        {
            s = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5").ToString();
            s = s.Substring(0, n);
            return s.ToLower();
        }
        /// <summary>
        /// 转换成小写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StrToLower(string str)
        {
            return str.ToLower();
        }
        /// <summary>
        /// 生成Serv_uMd5密码
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string Serv_u_Md5(string pwd)
        {
            string rdoms = "";  //定义字符串
            Random ran = new Random();
            rdoms += Convert.ToChar(ran.Next(26) + 'a').ToString() + Convert.ToChar(ran.Next(26) + 'a').ToString(); //将随机产生的两个字母相连.例如:a+b=ab
            string strmd5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(rdoms + pwd, "MD5");  //把两位随机字母和md5连接并再次进行MD5加密
            return rdoms + strmd5; //将两位随机字母与加密后的MD5值再次连接

        }
        private static char[] constant =
            {
            '0','1','2','3','4','5','6','7','8','9',
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'           
            };
        /// <summary>
        /// 任意数字和大小写字母的随机数的产生
        /// 参数为产生字母个数
        /// </summary>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static string GenerateRandom(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(36);

            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(36)]);
            }
            return newRandom.ToString();

        }

        private static char[] constantnum =
            {
            '0','1','2','3','5','6','7','8','9'           
            };

        /// <summary>
        /// 任意数字的随机数的产生
        /// 参数为生成位数
        /// </summary>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static string GenerateRandomNum(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(9);

            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constantnum[rd.Next(9)]);
            }
            return newRandom.ToString();

        }
        /// <summary>
        /// 随机生成指定整数大小以内的数字，返回整型int
        /// </summary>
        /// <param name="NumMax"></param>
        /// <returns></returns>
        public static int GetRandomNum(int NumMax)
        {
            if (NumMax > 0)
            {
                Random s = new Random();  　//建立了一个复杂random类型的对象s。注意new语句和括号不能少。
                int r;                     //建立简单int型对象r。             
                r = s.Next(NumMax);       //让复杂s对象的next子项（方法）生出一个NumMax以内的随机数，存入简单对象r中。 
                return r;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 去除所有Html格式
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string DropHTML(string strHtml)
        {
            string[] aryReg ={
          @"<script[^>]*?>.*?</script>",
          @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""''])(\\[""''tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
          @"([\r])[\s]+",
          @"&(quot|#34);",
          @"&(amp|#38);",
          @"&(lt|#60);",
          @"&(gt|#62);", 
          @"&(nbsp|#160);", 
          @"&(iexcl|#161);",
          @"&(cent|#162);",
          @"&(pound|#163);",
          @"&(copy|#169);",
          @"&#(\d+);",
          @"-->",
          @"<!--.*"         
         };

            string[] aryRep = {
           "",
           "",
           "",
           "\"",
           "&",
           "<",
           ">",
           " ",
           "\xa1",//chr(161),
           "\xa2",//chr(162),
           "\xa3",//chr(163),
           "\xa9",//chr(169),
           "",
           "\r",
           ""  
          };

            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, aryRep[i]);
            }

            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            strOutput.Replace("\r", "");
            return strOutput;

        }
        /// <summary>
        /// 去除所有Html格式
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string DropHTMLnew(string strHtml)
        {
            string[] aryReg ={
          @"<script[^>]*?>.*?</script>",
          @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""''])(\\[""''tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
          @"([\r])[\s]+",
          @"&(quot|#34);",
          @"&(amp|#38);",
          @"&(lt|#60);",
          @"&(gt|#62);",  
          @"&(iexcl|#161);",
          @"&(cent|#162);",
          @"&(pound|#163);",
          @"&(copy|#169);",
          @"&#(\d+);",
          @"-->",
          @"<!--.*"         
         };

            string[] aryRep = {
           "",
           "",
           "",
           "\"",
           "&",
           "<",
           ">",
           " ",
           "\xa1",//chr(161),
           "\xa2",//chr(162),
           "\xa3",//chr(163),
           "\xa9",//chr(169),
           "",
           ""  
          };

            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, aryRep[i]);
            }

            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            return strOutput;
        }
        /// <summary>
        /// 采用Trim()去空格，有中文去空格
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static string NoSpaces(string ch)
        {
            int mod = 2;//取模求余设置
            ch = killspace(ch, mod);
            mod = 3;
            ch = killspace(ch, mod);
            ch = ch.Replace("  ", " ");
            return ch;
        }

        private static string killspace(string ch, int mod)
        {
            string newstr = "";
            ch = ch.Trim();
            int ln = ch.Length;
            if (ln > mod)
            {
                int le = ln % mod;
                if (IsZh(ch.Substring(0, le).ToString()))
                {
                    newstr = ch.Substring(0, le).Trim();
                }
                else
                {
                    newstr = ch.Substring(0, le).ToString();
                }
                for (int i = 0; i < ch.Length - le; i = i + mod)
                {
                    if (IsZh(ch.Substring(0, le).ToString()))
                    {
                        newstr = newstr + ch.Substring(le + i, mod).Trim();
                    }
                    else
                    {
                        newstr = newstr + ch.Substring(le + i, mod).ToString();
                    }
                }
            }
            return newstr;
        }
        /// <summary>
        /// 返回空格个数，空格ascii编码为32
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static string CheckSpaces(string ch)
        {
            int counts = 0;
            string mystr = "";
            CharEnumerator myenum = ch.GetEnumerator();
            while (myenum.MoveNext())
            {
                byte[] array = new byte[1];
                array = System.Text.Encoding.ASCII.GetBytes(myenum.Current.ToString());
                int asciiCode = (short)(array[0]);
                if (asciiCode == 32)
                {
                    counts++;
                }
                else
                {
                    mystr += myenum.Current.ToString();
                }
            }
            return counts.ToString();
        }
        /// <summary>
        /// 返回该字符串第一字符的ASCII编码
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static string StrToASCII(string ch)
        {
            byte[] ascii = Encoding.ASCII.GetBytes(ch);

            return ascii.ToString();

        }
        /// <summary>
        /// 密码加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string PwdEncode(string data)
        {
            int outstr = Int32.Parse(data) + IpLastnum();
            return outstr.ToString();
        }

        /// <summary>
        /// 密码解密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string PwdDecode(string data)
        {
            int outstr = Int32.Parse(data) - IpLastnum();
            return outstr.ToString();
        }

        /// <summary>
        /// 取IP地址最后位
        /// </summary>
        /// <param name="myip"></param>
        /// <returns></returns>
        private static int IpLastnum()
        {
            string myip = LearnSite.Common.Computer.GetGuestIP();
            int islast = myip.LastIndexOf('.') + 1;
            return Int32.Parse(myip.Substring(islast, myip.Length - islast));
        }
        /// <summary>
        /// 将数组转化为可in查询条件
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static string ArraySqlstr(string[] array)
        {
            int n = array.Length;
            string sqlstr = "";
            if (n > 0)
            {
                for (int i = 0; i < n; i++)
                {
                    sqlstr = sqlstr + "'" + array[i].ToString() + "'" + ",";
                }
                if (sqlstr.EndsWith(","))
                {
                    sqlstr = sqlstr.Substring(0, sqlstr.Length - 1);
                }
            }
            return sqlstr;
        }

        public static string ServerUrl()
        {
            string url = "http://" + Computer.GetServerIp();
            return url;
        }

        /// <summary>
        /// 生成指定长度的随机数字串
        ///</summary>
        ///<param name="length">长度</param>
        ///<returns></returns>
        public static string RndStrnum(int length)
        {
            Random rnd = new Random();
            string returnstring = "";
            int i = 1;
            while (i <= length)
            {
                returnstring += rnd.Next(0, 9).ToString();
                i++;
            }
            return returnstring;
        }

        ///<summary>
        /// 生成指定长度的随机字符串
        ///</summary>
        ///<param name="length">长度</param>
        ///<param name="IsUpper">是否大写</param>
        ///<returns></returns>
        public static string RndStrword(int length)
        {
            Random rnd = new Random();
            string returnstring = "";
            int i = 1;

            while (i <= length)
            {
                returnstring += ((char)rnd.Next(97, 122)).ToString();
                i++;
            }
            return returnstring;
        }

        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="length">目标字符串的长度</param>
        /// <param name="useNum">是否包含数字</param>
        /// <param name="useLow">是否包含小写字母</param>
        /// <returns>指定长度的随机字符串</returns>
        public static string GetRnd(int length, bool useNum, bool useStr)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = "";

            if (useNum == true) { str += "0123456789"; }
            if (useStr == true) { str += "abcdefghijklmnopqrstuvwxyz"; }

            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }

            return s;
        }


        static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        static string strSource = "abcdefghijklmnopqrstuvwxyz";
        static string strallSource = "1234567890abcdefghijklmnopqrstuvwxyz";
        /// <summary>
        /// 获取指定长度的随机数字字母串
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        static string GetRandomNumberString(int length)
        {
            StringBuilder strPwd = new StringBuilder();
            Random random = new Random(GetRandomSeed());
            for (int i = 0; i < length; i++)
            {
                strPwd.Append(strallSource.Substring(random.Next(0, 35), 1));
            }
            return strPwd.ToString();
        }

        /// <summary>
        /// 获取指定长度的随机字母串
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        static string GetRandomString(int length)
        {
            StringBuilder strPwd = new StringBuilder();
            Random random = new Random(GetRandomSeed());
            for (int i = 0; i < length; i++)
            {
                strPwd.Append(strSource.Substring(random.Next(0, 25), 1));
            }
            return strPwd.ToString();
        }


        /// <summary>
        /// 生成指定长度的随机数字串
        ///</summary>
        ///<param name="length">长度</param>
        ///<returns></returns>
        public static string GetRandomNumber(int length)
        {
            StringBuilder numPwd = new StringBuilder();
            Random random = new Random(GetRandomSeed());
            for (int i = 0; i < length; i++)
            {
                numPwd.Append(random.Next(0, 9).ToString());
            }
            return numPwd.ToString();
        }
        /// <summary>
        /// 生成随机字符串类型选择
        /// </summary>
        /// <param name="length">长度</param>
        /// <param name="method">类型</param>
        /// <returns></returns>
        public static string GetRandomSelect(int length, string method)
        {
            switch (method)
            {
                case "0":
                    return GetRandomNumber(length);
                case "1":
                    return GetRandomString(length);
                case "2":
                    return GetRandomNumberString(length);
                default:
                    return "12345";
            }
        }
        /// <summary>
        /// 取ＩＰ的网段，前三位
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string GetIpGate(string ip)
        {
            return ip.Substring(0, ip.LastIndexOf('.'));
        }

        /// <summary>
        /// 根据加密类型对密码进行加密
        /// </summary>
        /// <param name="pwd">密码</param>
        /// <param name="encrypt">加密方式</param>
        /// <returns>加密后的密码</returns>
        public static string EncryptPassword(string pwd, string encrypt)
        {
            string m_EncryptPassword = pwd;
            switch (encrypt.ToLower())
            {
                case "md5":
                    m_EncryptPassword = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(m_EncryptPassword, "MD5");
                    break;
            }
            return m_EncryptPassword;
        }

        /// <summary>
        /// 根据文档名称，返回相对应的swf名称
        /// </summary>
        /// <param name="officename"></param>
        /// <returns></returns>
        public static string SwfName(string officename)
        {
            string[] spstr = officename.Split('.');
            string ft = spstr[1].ToLower();
            string swfFile = spstr[0] + ".swf";
            return swfFile;
        }
        /// <summary>
        /// 获取作品虚拟路径中文件名中的学号，如果无路径则返回""
        /// </summary>
        /// <param name="Wurl"></param>
        /// <returns></returns>
        public static string GetSnum(string Wurl)
        {
            string mySnum = "";
            if (Wurl != "")
            {
                int ln = Wurl.LastIndexOf("/") + 1;
                string shortfname = Wurl.Substring(ln, Wurl.Length - ln);
                string[] splitfname = shortfname.Split('_');
                mySnum = splitfname[0];
            }
            return mySnum;
        }
        /// <summary>
        /// 获取作品虚拟路径中文件名中的Mid任务号，如果无路径则返回""
        /// </summary>
        /// <param name="Wurl"></param>
        /// <returns></returns>
        public static string GetMid(string Wurl)
        {
            string myMid = "";
            if (Wurl != "")
            {
                string[] splitfname = Wurl.Split('/');
                myMid = splitfname[6];
            }
            return myMid;
        }
        /// <summary>
        ///  获取作品虚拟路径中文件名中的学号，如果无路径则返回""（htm网页作品记录）
        /// </summary>
        /// <param name="Wurl"></param>
        /// <returns></returns>
        public static string GetSnumhtm(string Wurl)
        {
            string mySnum = "";
            if (Wurl != "")
            {
                string[] splitfname =Wurl.Split('/');
                mySnum = splitfname[4];
            }
            return mySnum;
        }
        /// <summary>
        /// 虚拟路径
        /// </summary>
        /// <param name="flashurl"></param>
        /// <returns></returns>
        public static string flashhtml(string flashurl)
        {
            string str = "";
            string flashpath = HttpContext.Current.Server.MapPath(flashurl);
            if (File.Exists(flashpath))
            {
                string shtm = Common.Template.flashhtm();
                if (shtm != "")
                {
                    string jsurl = flashurl.Replace("~", "..");
                    string fw = "550";
                    string fh = "400";

                    //LearnSite.Common.SwfInfo fi = new SwfInfo(flashpath);

                    HttpContext.Current.Session["fw"] = fw;
                    HttpContext.Current.Session["fh"] = fh;
                    shtm = shtm.Replace("666", fw);
                    shtm = shtm.Replace("555", fh);
                    shtm = shtm.Replace("1.swf", jsurl);
                    str = shtm;
                }
            }
            return str;
        }

        /// <summary>
        /// office文档的flash链接，isflex表示是不是采用flexpaper预览
        /// </summary>
        /// <param name="flashurl"></param>
        /// <param name="isflex"></param>
        /// <returns></returns>
        public static string flashofficehtml(string flashurl, bool isflex)
        {
            string str = "";
            if (!isflex)
                str = Common.Template.flashhtm();
            else
                str = Common.Template.officehtm();

            string flashpath = HttpContext.Current.Server.MapPath(flashurl);
            if (File.Exists(flashpath))
            {
                if (str != "")
                {
                    string jsurl = flashurl.Replace("~", "..");
                    str = str.Replace("1.swf", jsurl);
                }
            }
            else
            {
                str = "Office文档未转换成swf,暂时无法预览！";
            }
            return str;
        }
        /// <summary>
        /// office文档的文档链接，isflex表示是不是采用flexpaper预览
        /// </summary>
        /// <param name="Wurl"></param>
        /// <param name="isflex"></param>
        /// <returns></returns>
        public static string flashofficehtmltea(string Wurl, bool isflex)
        {
            string str = "";
            if (!isflex)
                str = Common.Template.flashhtm();
            else
                str = Common.Template.officehtm();
            string ext = LearnSite.Common.WordProcess.getext(Wurl);
            string extions = "." + ext;
            string flashurl = Wurl.Replace(extions, ".swf");
            string flashpath = HttpContext.Current.Server.MapPath(flashurl);
            if (File.Exists(flashpath))
            {
                if (str != "")
                {
                    string jsurl = flashurl.Replace("~", "..");
                    str = str.Replace("1.swf", jsurl);
                }
            }
            else
            {
                //采用weboffice控件浏览
                string ofurl=Wurl.Replace("~","");
                str = Common.Template.webofficetwohtm();
                str = str.Replace("1.doc", ofurl);
                str = str.Replace("999999", getOfficeExt(ext));
            }
            return str;
        }
        /// <summary>
        /// office文档的flash链接，isflex表示是不是采用flexpaper预览
        /// </summary>
        /// <param name="flashurl"></param>
        /// <param name="isflex"></param>
        /// <returns></returns>
        public static string flashofficehtmlev(string Wid, string Wurl, bool isflex)
        {
            string str = "";
            if (!isflex)
                str = Common.Template.flashhtm();
            else
                str = Common.Template.officehtm();
            string ext = LearnSite.Common.WordProcess.getext(Wurl);
            string extions = "." + ext;
            string flashurl = Wurl.Replace(extions, ".swf");
            string flashpath = HttpContext.Current.Server.MapPath(flashurl);
            if (File.Exists(flashpath))
            {
                if (str != "")
                {
                    string jsurl = flashurl.Replace("~", "..");
                    str = str.Replace("1.swf", jsurl);
                }
            }
            else
            {
                //采用weboffice控件浏览
                str = Common.Template.webofficehtm();
                str = str.Replace("888888", Wid);
                str = str.Replace("999999", getOfficeExt(ext));
            }
            return str;
        }
        private static string getOfficeExt(string ext)
        {
            switch (ext)
            { 
                case "docx":
                    ext = "doc";
                    break;
                case "pptx":
                    ext = "ppt";
                    break;
                case "xlsx":
                    ext = "xls";
                    break;
            }
            return ext;
        }
        
        public static string photohtmlauto(string photourl)
        {
            string str = "";
            string pExt = getext(photourl);
            bool toswf = true;
            if (pExt == "gif"||pExt=="wmf")
                toswf = false;
           
            string photopath = HttpContext.Current.Server.MapPath(photourl);
            if (File.Exists(photopath))
            {
                string shtm = Common.Template.photohtm(toswf);
                if (shtm != "")
                {
                    if (isPicNew(photopath))
                    {
                        int pWidth = 10;
                        int pHeight = 10;
                        string jsurl = photourl.Replace("~", "..");
                        if (pExt != "psd")
                        {
                            try
                            {
                                System.Drawing.Bitmap bm = new System.Drawing.Bitmap(photopath);//photopath图片的物理路径
                                if (bm.Width < 700)
                                {
                                    pWidth = bm.Width;
                                    pHeight = bm.Height;
                                }
                                else
                                {
                                    pWidth = 610;
                                    double rw = bm.Width;
                                    double rh = bm.Height;
                                    double rn = rw / 610;
                                    rh = rh / rn;
                                    pHeight = Convert.ToInt32(rh);
                                }
                                if (pExt == "bmp")
                                {
                                    string savepath = photopath.Replace(".bmp", ".jpg");
                                    if (!File.Exists(savepath))
                                    {
                                        bm.Save(savepath, System.Drawing.Imaging.ImageFormat.Jpeg);//未转换则，另存bmp为jpg
                                    }
                                    jsurl = jsurl.Replace(".bmp", ".jpg");//同时将虚拟路径的后缀改转换的后缀
                                }
                                if (pExt == "wmf")
                                {
                                    pWidth = bm.Width;//按原图显示
                                    pHeight = bm.Height;
                                }
                            }
                            catch
                            {
                                str = "图片异常，读取失败！";
                            }
                        }
                        else
                        {
                            string jpgPsd = FileDown.PsdToJpg(photourl);//psd 要专门转换
                            jsurl = jsurl.Replace(".psd", ".jpg");//同时将虚拟路径的后缀改转换的后缀
                            string wlpath = HttpContext.Current.Server.MapPath(jpgPsd);
                            if (File.Exists(wlpath))
                            {
                                System.Drawing.Bitmap bmjpg = new System.Drawing.Bitmap(wlpath);
                                if (bmjpg.Width > 610)
                                {
                                    pWidth = 610;
                                    double rw = bmjpg.Width;
                                    double rh = bmjpg.Height;
                                    double rn = rw / 610;
                                    rh = rh / rn;
                                    pHeight = Convert.ToInt32(rh);
                                }
                                else
                                {
                                    pWidth = bmjpg.Width;
                                    pHeight = bmjpg.Height;
                                }
                            }
                        } 
                        shtm = shtm.Replace("140", pWidth.ToString());
                        shtm = shtm.Replace("105", pHeight.ToString());
                        shtm = shtm.Replace("1.png", jsurl);
                        str = shtm;
                    }
                    else
                    {
                        str = "该作品类型不是真实的" + pExt + "格式！";
                    }
                }
            }
            return str;
        }

        public static string photoswfauto(string photourl)
        {
            string str = "<br/><br/> ~o~ 找不到图片!<br/> ";
            string photopath = HttpContext.Current.Server.MapPath(photourl);

            if (File.Exists(photopath))
            {
                string pExt = getext(photourl);
                if (isPicNew(photopath))
                {
                    FileNameInfo fni = new FileNameInfo(photourl);
                    string md5fname = GetMD5_8bit(fni.Fname);
                    string swfurl = fni.Path + md5fname + ".swf";
                    string swfpath = HttpContext.Current.Server.MapPath(swfurl);

                    int pWidth = 20;
                    int pHeight = 20;
                    int mlimit = 1440;//转换时限制的最大宽度
                    int showlimit = 610;//显示时限制的最大宽度
                    if (fni.Ext == "wmf")
                        showlimit = 1440;
                    string jsurl = swfurl.Replace("~", "..");
                    Bitmap bmp;
                    if (!File.Exists(swfpath))
                    {
                        if (fni.Ext == "psd")
                        {
                            bmp = psdToBmp.myImg(photopath);
                            pWidth = bmp.Width;
                            pHeight = bmp.Height;
                            Common.ImagetoSwf.ConvertImgToSwf(WaterMark.SetText(bmp, mlimit), swfpath);
                        }
                        else
                        {
                            try
                            {
                                bmp = new Bitmap(photopath);
                                pWidth = bmp.Width;
                                pHeight = bmp.Height;
                                Common.ImagetoSwf.ConvertImgToSwf(WaterMark.SetText(bmp, mlimit), swfpath);
                            }
                            catch
                            {
                                str = "图片异常，读取失败！";
                            }
                        }
                    }
                    else
                    {
                        FileInfo fswf = new FileInfo(swfpath);
                        FileInfo fimg = new FileInfo(photopath);
                        if (DateTime.Compare(fimg.LastWriteTime, fswf.LastWriteTime) > 0)
                        {
                            fswf = null;
                            fimg = null;
                            if (fni.Ext == "psd")
                            {
                                bmp = psdToBmp.myImg(photopath);
                                pWidth = bmp.Width;
                                pHeight = bmp.Height;
                                Common.ImagetoSwf.ConvertImgToSwf(WaterMark.SetText(bmp, mlimit), swfpath);
                            }
                            else
                            {
                                try
                                {
                                    bmp = new Bitmap(photopath);
                                    pWidth = bmp.Width;
                                    pHeight = bmp.Height;
                                    Common.ImagetoSwf.ConvertImgToSwf(WaterMark.SetText(bmp, mlimit), swfpath);
                                }
                                catch
                                {
                                    str = "图片异常，读取失败！";
                                }
                            }
                        }
                        else
                        {
                            if (fni.Ext == "psd")
                            {
                                bmp = psdToBmp.myImg(photopath);
                                pWidth = bmp.Width;
                                pHeight = bmp.Height;
                            }
                            else
                            {
                                try
                                {
                                    bmp = new Bitmap(photopath);
                                    pWidth = bmp.Width;
                                    pHeight = bmp.Height;
                                }
                                catch
                                {
                                    str = "图片异常，读取失败！";
                                }
                            }
                        }                        
                    }
                    if (pWidth > showlimit)
                    {
                        pHeight = pHeight * showlimit / pWidth;
                        pWidth = showlimit;
                    }

                    string shtm = Common.Template.flashhtm();
                    shtm = shtm.Replace("666", pWidth.ToString());
                    shtm = shtm.Replace("555", pHeight.ToString());
                    shtm = shtm.Replace("1.swf", jsurl);
                    str = shtm;
                }
                else
                {
                    str = "该作品类型不是真实的" + pExt + "格式！";
                }
            }
            return str;
        }

        public static string mp3html(string mp3url,string ext)
        {
            string str = "";
            string shtm = Common.Template.mp3htm(ext);
            string mp3path = HttpContext.Current.Server.MapPath(mp3url);
            if (File.Exists(mp3path))
            {
                if (shtm != "")
                {                    
                    string jsurl = mp3url.Replace("~", "..");
                    //if (ext == "mp3")
                     //   jsurl = "../" + jsurl;
                    str = shtm.Replace("1.mp3", jsurl);
                }
            }
            return str;
        }
        public static string flvhtml(string flvurl)
        {
            string str = "";
            string pExt = getext(flvurl);
            string shtm = Common.Template.flvhtm();
            string flvpath = HttpContext.Current.Server.MapPath(flvurl);
            if (File.Exists(flvpath))
            {
                string jsurl = flvurl.Replace("~", "..");
                //jsurl = ServerUrl() + jsurl;
                str = shtm.Replace("1.flv", jsurl);
            }
            else
            {
                str = "flv视频文件不存在，请查看！";
            }
            return str;
        }

        public static string mmhtml(string mmurl)
        {
            string str = "";
            string shtm = Common.Template.freemindhtm();
            string mmpath = HttpContext.Current.Server.MapPath(mmurl);
            if (File.Exists(mmpath))
            {
                if (shtm != "")
                {
                    string jsurl = mmurl.Replace("~", "..");
                    str = shtm.Replace("map.mm", jsurl);
                }
            }
            return str;
        }

        public static string sbnewhtml(string Wid)
        {
            string str = "";
            string shtm = Common.Template.scratchflashnewhtm(); // Common.Template.scratchflashnewhtm();新的显示方法
            //fileName    workId      stuName
            if (shtm != "")
            {
                str = shtm.Replace("workId", Wid);
            }

            return str;
        }

        public static string sbhtml(string sburl)
        {
            string str = "";
            string shtm = Common.Template.scratchflashhtm(); // Common.Template.scratchhtm();
            string mmpath = HttpContext.Current.Server.MapPath(sburl);
            if (File.Exists(mmpath))
            {
                if (shtm != "")
                {
                    string jsurl = sburl.Replace("~", "../..");
                    str = shtm.Replace("Friend.sb", jsurl);
                }
            }
            return str;
        }
        public static string daehtml(string daeurl)
        {
            string str = "";
            string shtm = Common.Template.sketchuphtm(); // Common.Template.scratchhtm();
            string mmpath = HttpContext.Current.Server.MapPath(daeurl);
            if (File.Exists(mmpath))
            {
                if (shtm != "")
                {
                    string jsurl = daeurl.Replace("~", "../..");
                    str = shtm.Replace("monster.dae", jsurl);
                }
            }
            return str;
        }
        public static string dxfhtml(string dxfurl)
        {
            string str = "";
            string shtm = Common.Template.photohtm(false); // Common.Template.scratchhtm();
            string dxfpath = HttpContext.Current.Server.MapPath(dxfurl);
            dxftojpg(dxfpath);
            if (File.Exists(dxfpath))
            {
                if (shtm != "")
                {
                    string jsurl = dxfurl.Replace("~", "..");
                    jsurl = jsurl.Replace(".dxf", ".jpg");
                    shtm = shtm.Replace("140", "800");
                    shtm = shtm.Replace("105", "600");
                    shtm = shtm.Replace("1.png", jsurl);
                    str = shtm;
                }
            }
            return str;
        }
        private static void dxftojpg(string dxfpath)
        {
            try
            {
                CADImage FCADImage = new CADImage();
                FCADImage.Base.X = 400;
                FCADImage.Base.Y = 300;
                FCADImage.LoadFromFile(dxfpath);

                Image img = new Bitmap(800, 600);
                Graphics g = Graphics.FromImage(img);
                FCADImage.Draw(g);

                string gfile = dxfpath.Replace(".dxf", ".jpg");
                img.Save(gfile);
            }
            catch { }

        }

        public static string changeExt(string filename,string ext)
        {
            int finddot = filename.LastIndexOf('.');
            return SubString(filename, finddot + 1) + ext;
        }

        /// <summary>
        ///根据txt文件的相对路径，读txt文件内容，返回字符串
        /// </summary>
        /// <param name="txturl">txt文件的相对路径</param>
        /// <returns></returns>
        public static string txtstr(string txturl)
        {
            string str = "没有文本文件";
            string txtpath = HttpContext.Current.Server.MapPath(txturl);
            if (File.Exists(txtpath))
            {
                string strTemp = File.ReadAllText(txtpath, Encoding.GetEncoding("GB2312"));
                if (!string.IsNullOrEmpty(strTemp))
                {
                    strTemp = strTemp.Replace(">", "&gt;");
                    strTemp = strTemp.Replace("<", "&lt;");
                    strTemp = strTemp.Replace(" ", "&nbsp;");
                    strTemp = strTemp.Replace("\n", "<br />");
                    strTemp = strTemp.Replace("\r", "<br />");
                    strTemp = strTemp.Replace("\r\n", "<br />");
                    strTemp = strTemp.Replace("<br /><br />", "<br />");
                    str = "<div style='text-align:left;'><br />" + strTemp + "<br /></div>";
                }
            }
            return str;
        }
        /// <summary>
        /// 直接显示，不转换为swf
        /// </summary>
        /// <param name="txturl"></param>
        /// <returns></returns>
        public static string txtswf(string txturl)
        {
            string str = "没有文本文件";
            string txtpath = HttpContext.Current.Server.MapPath(txturl);
            if (File.Exists(txtpath))
            {
                string strTemp = File.ReadAllText(txtpath, Encoding.GetEncoding("GB2312"));
                if (!string.IsNullOrEmpty(strTemp))
                {
                    strTemp = strTemp.Replace(">", "&gt;");
                    strTemp = strTemp.Replace("<", "&lt;");
                    strTemp = strTemp.Replace(" ", "&nbsp;");
                    strTemp = strTemp.Replace("\r\n", "<br />");
                    strTemp = strTemp.Replace("\n", "<br />");
                    strTemp = strTemp.Replace("\r", "<br />");
                    strTemp = strTemp.Replace("<br /><br />", "<br />");
                }
                str = "<div style='width:600px;text-align:left;word-wrap:break-word; font:12px/1.5'>" + strTemp + "<br /></div>";
            }
            return str;
        }

        public static string txtswfold(string txturl)
        {
            string str = "没有文本文件";
            string txtpath = HttpContext.Current.Server.MapPath(txturl);
            string swfurl = getpath(txturl) + ".swf";
            string swfpath = getpath(txtpath) + ".swf";
            bool issleep = false;
            string jsurl = swfurl.Replace("~", "..");

            if (File.Exists(txtpath))
            {
                string strTemp = File.ReadAllText(txtpath, Encoding.GetEncoding("GB2312"));
                if (!string.IsNullOrEmpty(strTemp))
                {
                    if (!File.Exists(swfpath))
                    {
                        issleep = true;
                        ImagetoSwf.ConvertTextFileToSwf(strTemp, swfpath);
                    }
                    else
                    {
                        FileInfo fswf = new FileInfo(swfpath);
                        FileInfo ftxt = new FileInfo(txtpath);
                        if (DateTime.Compare(ftxt.LastWriteTime, fswf.LastWriteTime) > 0)
                        {
                            issleep = true;
                            ImagetoSwf.ConvertTextFileToSwf(strTemp, swfpath);
                        }
                    }
                    if (issleep)
                    {
                        string shtm = Common.Template.flashhtm();
                        shtm = shtm.Replace("666", "610");
                        shtm = shtm.Replace("555", "500");
                        shtm = shtm.Replace("1.swf", jsurl);
                        str = shtm;
                    }
                }
            }
            return str;
        }
        /// <summary>
        /// 判断字符串word是否在words中
        /// </summary>
        /// <param name="word">要找的字符</param>
        /// <param name="words">母字符串</param>
        /// <returns></returns>
        public static bool wordExist(string word, string words)
        {
            if (words.IndexOf(word) > -1)
                return true;//包含
            else
                return false;
        }
        /// <summary>
        /// 根据学期1,2返回上，下
        /// </summary>
        /// <param name="strnum"></param>
        /// <returns></returns>
        public static string ChineseTerm(string strnum)
        {
            string str = "";
            switch (strnum)
            {
                case "1":
                    str = "上";
                    break;
                case "2":
                    str = "下";
                    break;
            }
            return str;
        }
        public static string ChineseNum(string strnum)
        {
            if (IsNum(strnum))
            {
                int num = Int32.Parse(strnum);
                string[] mycn = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十二", "十三", "十四", "十五", "十六", "十七", "十八", "十九", "二十", "二十一" };
                if (num > 0 && num < 21)
                    return mycn[num];
                else
                    return num.ToString();
            }
            else
            {
                return "无";
            }
        }
        public static string ChineseNum(int num)
        {
            string[] mycn = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十二", "十三", "十四", "十五", "十六", "十七", "十八", "十九", "二十", "二十一" };
            if (num > 0 && num < 21)
                return mycn[num];
            else
                return num.ToString();
        }

        public static int MathNum(string ChineseNum)
        {
            int n = 0;
            string[] mycn = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十二", "十三", "十四", "十五", "十六", "十七", "十八", "十九", "二十", "二十一" };
            for (int i = 0; i < mycn.Length; i++)
            {
                if (mycn[i] == ChineseNum)
                {
                    n = i;
                    break;
                }
            }
            return n;
        }

        /// <summary>
        /// ~/Homework/3011/1/1/92/171/3011006_92_171_8_122.rar
        /// 返回虚拟路径中的文件名称（3011006_92_171_8_122）
        /// </summary>
        /// <param name="fname"></param>
        /// <returns></returns>
        public static string getsinglefilename(string fname)
        {
            if (!string.IsNullOrEmpty(fname))
            {
                int aa = fname.LastIndexOf("/");
                int bb = fname.LastIndexOf(".");
                int ln = bb - aa - 1;
                string mypath = fname.Substring(aa, ln);
                return mypath;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 返回完整文件名中.后缀前面的部分（不包括.）
        /// </summary>
        /// <param name="fname"></param>
        /// <returns></returns>
        public static string getpath(string fname)
        {
            string mypath = fname.Substring(0, fname.LastIndexOf("."));
            return mypath;
        }
        /// <summary>
        /// 返回_分隔符的第一个字符串
        /// </summary>
        /// <param name="fname"></param>
        /// <returns></returns>
        public static string get_path(string fname)
        {
            string[] mypath = fname.Split('_');
            return mypath[0];
        }
        /// <summary>
        /// 返回完整文件名中\前面的部分（不包括\）
        /// </summary>
        /// <param name="fname"></param>
        /// <returns></returns>
        public static string getbackpath(string fname)
        {
            string mypath = fname.Substring(0, fname.LastIndexOf(@"\"));
            return mypath;
        }
        /// <summary>
        /// 返回完整文件名中/前面的部分（不包括/）
        /// </summary>
        /// <param name="fname"></param>
        /// <returns></returns>
        public static string getfrontpath(string fname)
        {
            string mypath = fname.Substring(0, fname.LastIndexOf("/"));
            return mypath;
        }
        /// <summary>
        /// 如doc、ppt 无点
        /// </summary>
        /// <param name="fname"></param>
        /// <returns></returns>
        public static string getext(string fname)
        {
            int dd = fname.LastIndexOf(".") + 1;
            if (dd > 1)
            {
                string myext = fname.Substring(dd, fname.Length - dd);
                return myext.ToLower();
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 取扩展名（如.jpg）含点
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string getextions(string filename)
        {
            if (filename != "")
                return filename.Substring(filename.LastIndexOf('.')).ToLower();
            else
                return "";
        }
        /// <summary>
        /// 返回虚拟路径文件的短文件名
        /// </summary>
        /// <param name="fnameurl"></param>
        /// <returns></returns>
        public static string getshortfname(string fnameurl)
        {
            if (fnameurl.Trim() != "")
            {
                int ee = fnameurl.LastIndexOf("/") + 1;
                return fnameurl.Substring(ee, fnameurl.Length - ee);
            }
            else
                return "";
        }
        public static string SelectWrite(string filetype, string downfilename, bool isflex)
        {
            string str = "";
            switch (filetype)
            {
                case "htm":
                    string siteurl = downfilename.Replace("~", "..") + Isindex(downfilename);
                    str = "<Iframe name='ls2012' frameborder='0' src='" + siteurl + "' width='100%' height='600'>浏览器不支持嵌入式框架</iframe>";
                    break;
                case "txt":
                case "pas":
                case "c":
                    str = txtstr(downfilename);
                    break;
                case "swf":
                    str = flashhtml(downfilename);
                    break;
                case "doc":
                case "docx":
                case "xls":
                case "xlsx":
                case "ppt":
                case "pptx":
                case "wps":
                case "dps":
                case "et":
                    string swfname = downfilename.Replace(filetype, "swf");
                    str = flashofficehtml(swfname, isflex);
                    break;
                case "mht":
                    string mhturl = downfilename.Replace("~", "..");
                    str = "<Iframe name='ls2012' frameborder='0' src='" + mhturl + "' width='100%' height='600'>浏览器不支持嵌入式框架</iframe>";
                    break;
                case "mm":
                    str = mmhtml(downfilename);
                    break;
                case "sb":
                case "sb2":
                    str = sbhtml(downfilename);
                    break;
                case "dae":
                    str = daehtml(downfilename);
                    break;
                case "psd":
                case "jpg":
                case "png":
                case "bmp":
                case "gif":
                    str = photohtmlauto(downfilename);
                    break;
                case "flv":
                    str = flvhtml(downfilename);
                    break;
                case "mp3":
                case "wma":
                case "rm":
                case "ra":
                case "ram":
                case "asf":
                case "mid":
                case "wmv":
                case "wav":
                case "avi":
                case "mpg":
                    str = mp3html(downfilename, filetype);
                    break;
                default:
                    str = "<div style='text-align: center;font-size: 9pt;'>该文件格式无法支持在线预览~!</div>";
                    break;
            }
            return str;//返回预览的html生成页面
        }

        public static string SelectWriteNew(string filetype, string downfilename, bool isflex)
        {
            string str = "";
            switch (filetype)
            {
                case "htm":
                    string siteurl = downfilename.Replace("~", "..") ;//+ Isindex(downfilename);
                    str = "<Iframe name='ls2012' frameborder='0' src='" + siteurl + "' width='100%' height='600'>浏览器不支持嵌入式框架</iframe>";
                    break;
                case "txt":
                case "pas":
                case "c":
                case "lgo":
                    str = txtswf(downfilename);
                    break;
                case "swf":
                    str = flashhtml(downfilename);
                    break;
                case "doc":
                case "docx":
                case "xls":
                case "xlsx":
                case "ppt":
                case "pptx":
                case "wps":
                case "dps":
                case "et":
                    str = flashofficehtmltea(downfilename, isflex);
                    break;
                case "mht":
                    string mhturl = downfilename.Replace("~", "..");
                    str = "<Iframe name='ls2012' frameborder='0' src='" + mhturl + "' width='100%' height='600'>浏览器不支持嵌入式框架</iframe>";
                    break;
                case "mm":
                    str = mmhtml(downfilename);
                    break;
                case "sb":
                case "sb2":
                    str = sbhtml(downfilename);
                    break;
                case "dae":
                    str = daehtml(downfilename);
                    break;
                case "psd":
                case "jpg":
                case "png":
                case "bmp":
                    str = photoswfauto(downfilename);
                    break;
                case "gif":
                case "wmf":
                    str = photohtmlauto(downfilename);
                    break;
                case "flv":
                    str = flvhtml(downfilename);
                    break;
                case "mp3":
                case "wma":
                case "rm":
                case "ra":
                case "ram":
                case "asf":
                case "mid":
                case "wmv":
                case "wav":
                case "avi":
                case "mpg":
                    str = mp3html(downfilename,filetype);
                    break;
                default:
                    str = "<div style='text-align: center;font-size: 9pt;'>该文件格式无法支持在线预览~!</div>";
                    break;
            }
            return str;//返回预览的html生成页面
        }

        public static string SelectEvaluateShow(string Wid,string filetype, string downfilename, bool isflex)
        {
            string str = "";
            switch (filetype)
            {
                case "":
                case "htm":
                    string siteurl = downfilename.Replace("~", "..") ;//+ Isindex(downfilename);
                    str = "<Iframe name='ls2012' frameborder='0' src='" + siteurl + "' width='100%' height='600'>浏览器不支持嵌入式框架</iframe>";
                    break;
                case "txt":
                case "pas":
                case "c":
                case "cpp":
                case "frm":
                case "as":
                case "java":
                case "xml":
                case "ino":
                case "lgo":
                    str = txtswf(downfilename);
                    break;
                case "swf":
                    str = flashhtml(downfilename);
                    break;
                case "doc":
                case "docx":
                case "xls":
                case "xlsx":
                case "ppt":
                case "pptx":
                case "wps":
                case "dps":
                case "et":
                    str = flashofficehtmlev(Wid,downfilename,isflex);
                    break;
                case "mht":
                    string mhturl = downfilename.Replace("~", "..");
                    str = "<Iframe name='ls2012' frameborder='0' src='" + mhturl + "' width='100%' height='600'>浏览器不支持嵌入式框架</iframe>";
                    break;
                case "mm":
                    str = mmhtml(downfilename);
                    break;
                case "sb":
                case "sb2":
                case "sbx":
                    str = sbnewhtml(Wid);
                    break;
                case "dae":
                    str = daehtml(downfilename);
                    break;
                case "dxf":
                    str = dxfhtml(downfilename);
                    break;
                case "psd":
                case "bmp":
                    str = photoswfauto(downfilename);
                    break;
                case "jpg":
                case "png":
                case "wmf":
                case "gif":
                    str = photohtmlauto(downfilename);
                    break;
                case "flv":
                    str = flvhtml(downfilename);
                    break;
                case "mp3":
                case "wma":
                case "rm":
                case "ra":
                case "ram":
                case "asf":
                case "mid":
                case "wmv":
                case "wav":
                case "avi":
                case "mpg":
                case "mp4":
                case "3gp":
                case "mkv":
                case "mov":
                    str = mp3html(downfilename, filetype);
                    break;
                default:
                    str = "<div style='text-align: center;font-size: 9pt;'>该文件格式无法支持在线预览~!</div>";
                    break;
            }
            return str;//返回预览的html生成页面
        }
        public static string Isindex(string Wurl)
        {
            string str = "index.htm";
            string furl = Wurl + str;
            string fpath = HttpContext.Current.Server.MapPath(furl);
            if (File.Exists(fpath))
                return str;
            else
                return "default.htm";
        }
        public static string SelectWriteStuNew(string filetype, string downfilename, bool isflex)
        {
            string str = "";
            switch (filetype)
            {
                case "":
                case "htm":
                    string siteurl = downfilename.Replace("~", "..");// + Isindex(downfilename);
                    str = "<Iframe name='ls2012' frameborder='0' src='" + siteurl + "' width='100%' height='600'>浏览器不支持嵌入式框架</iframe>";
                    break;
                case "txt":
                case "pas":
                case "c":
                case "cpp":
                case "frm":
                case "as":
                case "java":
                case "xml":
                case "ino":
                case "lgo":
                    str = txtswf(downfilename);
                    break;
                case "swf":
                    str = flashhtml(downfilename);
                    break;
                case "doc":
                case "docx":
                case "xls":
                case "xlsx":
                case "ppt":
                case "pptx":
                case "wps":
                case "dps":
                case "et":
                    str =flashofficehtmltea(downfilename, isflex);
                    break;
                case "mht":
                    string mhturl = downfilename.Replace("~", "..");
                    str = "<Iframe name='ls2012' frameborder='0' src='" + mhturl + "' width='100%' height='600'>浏览器不支持嵌入式框架</iframe>";
                    break;
                case "mm":
                    str = mmhtml(downfilename);
                    break;
                case "sb":
                case "sb2":
                    str = sbhtml(downfilename);
                    break;
                case "dae":
                    str = daehtml(downfilename);
                    break;
                case "psd":
                case "jpg":
                case "png":
                case "bmp":
                    str = photoswfauto(downfilename);
                    break;
                case "gif":
                case "wmf":
                    str = photohtmlauto(downfilename);
                    break;
                case "flv":
                    str = flvhtml(downfilename);
                    break;
                case "mp3":
                case "wma":
                case "rm":
                case "ra":
                case "ram":
                case "asf":
                case "mid":
                case "wmv":
                case "wav":
                case "avi":
                case "mpg":
                    str = mp3html(downfilename, filetype);
                    break;
                default:
                    str = "<div style='text-align: center;font-size: 9pt;'>该文件格式无法支持在线预览~!</div>";
                    break;
            }
            return str;//返回预览的html生成页面
        }

        public static string SelectWriteTeaNew(string filetype, string downfilename, bool isflex,string htmname)
        {
            string str = "";
            switch (filetype)
            {
                case "htm":
                    string siteurl = downfilename.Replace("~", "..") + htmname;
                    str = "<Iframe name='ls2012' frameborder='0' src='" + siteurl + "' width='100%' height='1024'>浏览器不支持嵌入式框架</iframe>";
                    break;
                case "txt":
                case "pas":
                case "c":
                case "cpp":
                case "frm":
                case "as":
                case "java":
                case "xml":
                case "ino":
                case "lgo":
                case "py":
                    str = txtswf(downfilename);
                    break;
                case "swf":
                    str = flashhtml(downfilename);
                    break;
                case "doc":
                case "docx":
                case "xls":
                case "xlsx":
                case "ppt":
                case "pptx":
                case "wps":
                case "dps":
                case "et":
                    str = flashofficehtmltea(downfilename, isflex);
                    break;
                case "mht":
                    string mhturl = downfilename.Replace("~", "..");
                    str = "<Iframe name='ls2012' frameborder='0' src='" + mhturl + "' width='100%' height='600'>浏览器不支持嵌入式框架</iframe>";
                    break;
                case "mm":
                    str = mmhtml(downfilename);
                    break;
                case "sb":
                case "sb2":
                    str = sbhtml(downfilename);
                    break;
                case "dae":
                    str = daehtml(downfilename);
                    break;
                case "dxf":
                    str = dxfhtml(downfilename);
                    break;
                case "psd":
                case "bmp":
                    str = photoswfauto(downfilename);
                    break;
                case "jpg":
                case "png":
                case "gif":
                case "wmf":
                case "svg":
                    str = photohtmlauto(downfilename);
                    break;
                case "flv":
                case "mp4":
                    str = flvhtml(downfilename);
                    break;
                case "mp3":
                case "wma":
                case "rm":
                case "ra":
                case "ram":
                case "asf":
                case "mid":
                case "wmv":
                case "wav":
                case "avi":
                case "mpg":
                case "3gp":
                case "mkv":
                case "mov":
                    str = mp3html(downfilename, filetype);
                    break;
                default:
                    str = "<div style='text-align: center;font-size: 14pt;'>该文件格式无法支持在线预览~!</div>";
                    break;
            }
            string mydown = downfilename.Replace("~", "..");
            string url = "<br /><br /><div style='text-align: center;font-size: 9pt; color:#333333;'>点击下载查看：<a href='" + mydown + "' target='_blank'>" + mydown + "</a> 大小：" + Filesize(downfilename) + "kb</div><br />";
            str = str + url;
            return str;//返回预览的html生成页面
        }
        /// <summary>
        /// 返回超链接文件大小（kb）
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string Filesize(string url)
        {
            string path = HttpContext.Current.Server.MapPath(url);
            if (File.Exists(path))
            {
                FileInfo fi = new FileInfo(path);
                int len = Convert.ToInt32(fi.Length / 1024);
                return len.ToString();
            }
            else
                return "空";
        }
         public static string SelectWriteDisplayNew(string filetype, string downfilename, bool isflex, string htmname)
        {
            string str = "";
            switch (filetype)
            {
                case "htm":
                    string siteurl = downfilename.Replace("~", "..") + htmname;
                    str = "<Iframe name='ls2012' frameborder='0' src='" + siteurl + "' width='100%' height='1024'>浏览器不支持嵌入式框架</iframe>";
                    break;
                case "txt":
                case "pas":
                case "c":
                case "lgo":
                    str = txtswf(downfilename);
                    break;
                case "swf":
                    str = flashhtml(downfilename);
                    break;
                case "doc":
                case "docx":
                case "xls":
                case "xlsx":
                case "ppt":
                case "pptx":
                    str = flashofficehtmltea(downfilename, isflex);
                    break;
                case "mht":
                    string mhturl = downfilename.Replace("~", "..");
                    str = "<Iframe name='ls2012' frameborder='0' src='" + mhturl + "' width='100%' height='600'>浏览器不支持嵌入式框架</iframe>";
                    break;
                case "mm":
                    str = mmhtml(downfilename);
                    break;
                case "sb":
                case "sb2":
                    str = sbhtml(downfilename);
                    break;
                case "psd":
                case "jpg":
                case "png":
                case "bmp":
                    str = photoswfauto(downfilename);
                    break;
                case "gif":
                case "wmf":
                    str = photohtmlauto(downfilename);
                    break;
                case "flv":
                case "mp4":
                    str = flvhtml(downfilename);
                    break;
                case "mp3":
                case "wma":
                case "rm":
                case "ra":
                case "ram":
                case "asf":
                case "mid":
                case "wmv":
                case "wav":
                case "avi":
                case "mpg":
                case "3gp":
                case "mkv":
                case "mov":
                    str = mp3html(downfilename, filetype);
                    break;
                default:
                    str = "<div style='text-align: center;font-size: 9pt;'>该文件格式无法支持在线预览~!</div>";
                    break;
            }
            return str;//返回预览的html生成页面
        }
        /// <summary>
        /// 统计字符串中某字符出现次数
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="ch">某字符</param>
        /// <returns></returns>
        public static int charCount(string str,char ch)
        {
            int count = 0;
            foreach (char c in str)
            {
                if (c == ch)
                    count++;
            }
            return count;
        }

        /// <summary>
        /// 统计子字符串在字符串中的出现次数
        /// </summary>
        /// <param name="mStr">母字符串</param>
        /// <param name="sStr">子字符串</param>
        /// <param name="pStr">分隔符</param>
        /// <returns></returns>
        public static int StrCount(string mStr, string sStr, char pStr)
        {
            int cn = 0;
            string[] mySplits = mStr.Split(pStr);
            foreach (string ch in mySplits)
            {
                if (ch == sStr)
                    cn++;
            }
            return cn;
        }
        /// <summary>
        /// 子字符串在母字符串中存在（有分隔符）mStr母字符串 sStr子字符串 pStr分隔符
        /// </summary>
        /// <param name="mStr">母字符串</param>
        /// <param name="sStr">子字符串</param>
        /// <param name="pStr">分隔符</param>
        /// <returns></returns>
        public static bool StrExist(string mStr, string sStr, char pStr)
        {
            bool isExist = false;
            string[] mySplits = mStr.Split(pStr);
            foreach (string ch in mySplits)
            {
                if (ch == sStr)
                {
                    isExist = true;
                    return true;//如果出现，马上跳出循环
                }
            }
            return isExist;
        }
        /// <summary>
        /// 统计子字符串在字符串中的出现次数
        /// </summary>
        /// <param name="mStr">母字符串</param>
        /// <param name="sStr">子字符串</param>
        /// <returns></returns>
        public static int StrCountNew(string mStr, string sStr)
        {
            return Regex.Matches(mStr, sStr).Count;
        }
        /// <summary>
        /// 是否字符串全为中文
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool IsChina(string ch)
        {
            bool res = false;
            if (!string.IsNullOrEmpty(ch))
            {
                res = true;
                foreach (char c in ch)
                {
                    if (!IsChinese(c))
                    {
                        res = false;
                        break;
                    }                
                }
            }
            return res;
        }
        //字符是否为汉字
        public static bool IsChinese(char c)
        {
            return (int)c >= 0x4E00 && (int)c <= 0x9FA5;
        }
        //获得字节长度
        private static int getLengthb(string str)
        {
            return System.Text.Encoding.Default.GetByteCount(str);
        }

        /// <summary>
        /// 取得固定长度的字符串(按单字节截取)。
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="resultLength">截取长度</param>
        /// <returns></returns>
        public static string SubString(string source, int resultLength)
        {
            //判断字符串长度是否大于截断长度
            if (System.Text.Encoding.Default.GetByteCount(source) > resultLength)
            {
                //判断字串是否为空
                if (source == null)
                {
                    return "";
                }

                //初始化
                int i = 0, j = 0;

                //为汉字或全脚符号长度加2否则加1
                foreach (char newChar in source)
                {
                    if ((int)newChar > 127)
                    {
                        i += 2;
                    }
                    else
                    {
                        i++;
                    }
                    if (i > resultLength)
                    {
                        source = source.Substring(0, j);
                        break;
                    }
                    j++;
                }
                source = source + "..";
            }
            return source;
        }
        /// <summary>
        /// 判断是不是图片文件 
        /// </summary>
        /// <param name="picPath">物理路径</param>
        /// <param name="ext">后缀，无点，小写</param>
        /// <returns></returns>
        public static bool isPic(string picPath, string ext)
        {
            bool isok = false;
            try
            {
                FileStream fs = new FileStream(picPath, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(fs);
                string fileClass;
                byte buffer;
                byte[] b = new byte[2];
                buffer = reader.ReadByte();
                b[0] = buffer;
                fileClass = buffer.ToString();
                buffer = reader.ReadByte();
                b[1] = buffer;
                fileClass += buffer.ToString();
                reader.Close();
                fs.Close();
                switch (fileClass)
                {
                    case "255216":
                        if (ext == "jpg")
                            isok = true;
                        break;
                    case "13780":
                        if (ext == "png")
                            isok = true;
                        break;
                    case "6677":
                        if (ext == "bmp")
                            isok = true;
                        break;
                    case "5666":
                        if (ext == "psd")
                            isok = true;
                        break;
                    case "7173":
                    case "91109":
                        if (ext == "gif")
                            isok = true;
                        break;
                }
                return isok;
            }
            catch
            {
                return isok;
            }
        }
        /// <summary>
        /// 判断是不是图片文件
        /// bmp,jpg,gif,pcx,png,psd,ras,sgi,tiff
        /// </summary>
        /// <param name="picPath">物理路径</param>
        /// <returns></returns>
        public static bool isPicNew(string picPath)
        {
            return ImageCheck.CheckImageType(picPath);
        }
        /// <summary>
        /// 判断是不是图片文件 （不行）
        /// </summary>
        /// <param name="picPath">物理路径</param>
        /// <param name="ext">后缀，无点，小写</param>
        /// <returns></returns>
        public static bool isPicNew(Stream myStream, string ext)
        {
            bool isok = false;
            string fileClass;
            byte[] b = new byte[2];
            myStream.Read(b, 0, 2);
            fileClass = b[0].ToString() + b[1].ToString();
            myStream.Close();
            switch (fileClass)
            {
                case "255216":
                    if (ext == "jpg")
                        isok = true;
                    break;
                case "13780":
                    if (ext == "png")
                        isok = true;
                    break;
                case "6677":
                    if (ext == "bmp")
                        isok = true;
                    break;
                case "5666":
                    if (ext == "psd")
                        isok = true;
                    break;
                case "7173":
                case "91109":
                    if (ext == "gif")
                        isok = true;
                    break;
                default:
                    HttpContext.Current.Response.Write("</br> 特征码 " + fileClass + "  </br>");
                    break;
            }
            return isok;
        }
        /// <summary>
        /// 获取集合中不重复列表
        /// </summary>
        /// <param name="Words"></param>
        /// <returns></returns>
        public static string SimpleWords(string Words)
        {
            string newstr = "";
            if (Words != "")
            {
                string[] temps = Words.Split(',');
                int ln = temps.Length;
                if (ln > 0)
                {
                    for (int i = 0; i < ln; i++)
                    {
                        string cc = temps[i] + ",";
                        if (newstr.IndexOf(cc) > -1)
                        {
                            continue;//如果存在就继续
                        }
                        else
                        {
                            newstr = newstr + cc;//不存在就添加
                        }
                    }
                    if (newstr.EndsWith(","))
                        newstr = newstr.Substring(0, newstr.Length - 1);
                }
            }
            return newstr;
        }


        /// <summary>
        /// 获取集合中不重复列表
        /// </summary>
        /// <param name="Words"></param>
        /// <returns></returns>
        public static string SimpleWordsNew(string Words)
        {
            string newstr = "";
            Words = Words.Replace(" ", "");
            Words = Words.Replace(",,", ",");
            if (Words != "")
            {
                string[] temps = Words.Split(',');
                int ln = temps.Length;
                if (ln > 0)
                {
                    for (int i = 0; i < ln; i++)
                    {
                        string cc = temps[i] + ",";
                        if (newstr.IndexOf(cc) > -1)
                        {
                            continue;//如果存在就继续
                        }
                        else
                        {
                            newstr = newstr + cc;//不存在就添加
                        }
                    }
                    if (newstr.EndsWith(","))
                        newstr = newstr.Substring(0, newstr.Length - 1);
                }
            }
            return newstr;
        }
        /// <summary>
        /// 过滤文件名字符串中的特殊字符
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string FilterSpecial(string strHtml)
        {
            if (string.Empty == strHtml)
            {
                return strHtml;
            }
            string[] aryReg = {"@","{","}", "'", "delete", "?", "<", ">", "%", "\\", ",", ".", ">=", "=<", ";", "||", "[", "]", "&", "/", "|", " ", "''","^","`","#","*","~" };
            for (int i = 0; i < aryReg.Length; i++)
            {
                strHtml = strHtml.Replace(aryReg[i], string.Empty);
            }
            return strHtml.Trim();
        }
        /// <summary>
        /// 是否存在特殊符号（空字符，返回假）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ExistSpecial(string str)
        {
            bool isSpecial = false;
            if (!string.IsNullOrEmpty(str))
            {
                char[] aryReg = {'@','-','{','}', '?', '<', '>', '%', '\\', ',', '.', '$', '=', ';', '[', ']', '&', '/', '|', ' ', '"', '^', '`', '#', '*', '~', '\'' };

                for (int i = 0; i < aryReg.Length; i++)
                {
                    if (str.IndexOf(aryReg[i]) > -1)
                    {
                        isSpecial = true;//如果存在特殊符号
                        break;
                    }
                }
            }
            return isSpecial;
        }
        /// <summary>
        /// 无路径的文件名（包含后缀），过滤处理后返回新文件名（不包含后缀），abc.jpg
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string FilterFileName(string filename)
        {
            if (!string.IsNullOrEmpty(filename))
            {
                int lastdot = filename.LastIndexOf('.');
                if (lastdot > 0)
                {
                    string shortfilename = filename.Substring(0, lastdot);
                    shortfilename = FilterSpecial(shortfilename);//过滤文件名中的特殊符号
                    return shortfilename;
                }
                else
                {
                    return filename;
                }
            }
            else
                return filename;
        }

        public static string ClearPP(string txt)
        {
            return txt.Replace("<p>", "").Replace("</p>", "");        
        }
        /// <summary>
        /// 文章更新图标提示一天、周、一月
        /// </summary>
        /// <param name="OldTime"></param>
        /// <returns></returns>
        public static string GetDaysGoneImg(DateTime OldTime)
        {            
            int a=1,b=7,c=30;//一天、周、一月
            string imgurl = "";
            int days = Computer.Daysgone(OldTime);
            if (days < c)
            {
                string period = "month";
                string title = "一月内";

                if (days < b)
                {
                    period = "week";
                    title = "一周内";
                }
                if (days < a)
                {
                    period = "day";
                    title = "一天内";
                }
                imgurl = "<img alt='' title='" + title + "小时' src='../Images/new_" + period + ".gif' />";
            }
            return imgurl;
        }
        /// <summary>
        /// 文章更新图标提示一天、周、一月
        /// </summary>
        /// <param name="OldTime"></param>
        /// <returns></returns>
        public static string ReturnDaysGoneImg(DateTime OldTime)
        {
            int a = 1, b = 7, c = 30;//一天、周、一月
            int days = Computer.Daysgone(OldTime);
            string imgurl = "~/Images/new_none.gif";
            if (days < c)
            {
                string period = "month";

                if (days < b)
                {
                    period = "week";
                }
                if (days < a)
                {
                    period = "day";
                }
                imgurl = "~/Images/new_" + period + ".gif";
            }
            return imgurl;
        }
        /// <summary>
        /// 获取文件类型图标，不带点，如jpg
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static string GetFileTypeImg(string ext)
        {
            string imgurl = "<img alt='' src='../Images/FileType/" + ext + ".gif' />";
            return imgurl;
        }
        public static string GetGoodImg(bool good)
        {
            string imgurl = "<img alt='' title='优秀' src='../Images/new_none.gif' />";
            if (good)
                imgurl = "<img alt='' title='优秀' src='../Images/new_good.gif' />";
            return imgurl;
        }
        public static string ReturnGoodImg(bool good)
        {
            string imgurl = "~/Images/new_none.gif";
            if (good)
                imgurl = "~/Images/new_good.gif";
            return imgurl;
        }
        /// <summary>
        /// 截取中英文混合字符串
        /// </summary>
        /// <param name="original">原始字符串</param>
        /// <param name="length">截取长度</param>
        /// <param name="fill">截取串小于原始串时,尾部附加字符串</param>
        /// <returns></returns>
        public static String CnCutString(String original, Int32 length, String fill)
        {
            Regex CnRegex = new Regex("[\u4e00-\u9fa5]+", RegexOptions.Compiled);
            Char[] CharArray = original.ToCharArray();
            Int32 tmplength = 0;
            for (Int32 i = 0; i < CharArray.Length; i++)
            {
                tmplength += CnRegex.IsMatch(CharArray[i].ToString()) ? 2 : 1;
                if (tmplength > length) return original.Substring(0, i - fill.Length) + fill;
            }
            return original;
        }

    }
}
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Collections;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
namespace LearnSite.DBUtility
{
    /// <summary>
    ///UpdateGrade 的摘要说明
    /// </summary>
    public class UpdateGrade
    {
        public UpdateGrade()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 版本更新检测，false为未更新
        /// 每次更新数据库时，请更改检测的表或字段
        /// </summary>
        /// <returns></returns>
        public static bool VersionCheck()
        {
            if (DbHelperSQL.TableCounts("English") < 10)
                return false;
            else
                return true;
        }
        public static bool TableExistCheck()
        {
            string CheckTabel = "Students";
            return DbHelperSQL.TabExists(CheckTabel);
        }

        public static bool TableCheck()
        {
            return true;
        }

        public static string UpdateTableEnglish()
        {
            return "";
        }
    }
}

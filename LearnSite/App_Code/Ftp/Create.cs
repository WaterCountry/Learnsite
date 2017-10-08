using System;
using System.Collections.Generic;
using System.Web;
namespace LearnSite.Ftp
{
    /// <summary>
    ///Create 的摘要说明
    /// </summary>
    public class Create
    {
        public Create()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static string FtpUserCreate(int QuotaMax)
        {
            LearnSite.BLL.Webstudy wb = new LearnSite.BLL.Webstudy();
            wb.AddAll();//从学生表读取Webstudy表中不存在的数据，插入Webstudy表中
            string msg= LearnSite.Ftp.Reg.RegAllFtpTwo(QuotaMax);
            return msg;
        }
        /// <summary>
        /// Ftp目录及Web数据表生成
        /// </summary>
        /// <returns></returns>
        public static string FtpDirCreate()
        {
            return LearnSite.Ftp.Disk.CreateFtpDir();
        }


    }

    
}
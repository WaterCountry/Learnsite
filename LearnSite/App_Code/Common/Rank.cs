using System;
using System.Collections.Generic;
using System.Web;
namespace LearnSite.Common
{
    /// <summary>
    ///Rank 的摘要说明
    /// </summary>
    public class Rank
    {
        public Rank()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 获取等级图标
        /// </summary>
        /// <param name="Scores">积分</param>
        /// <param name="up">升级值</param>
        /// <returns></returns>
        public static string RankImage(int Scores,bool isnew)
        {
            int up = 3;//设置默认升级值
            string stargray = "<img src='../Images/stargray.gif' style='border-width:0px;' />";
            if(isnew)
                stargray = "<img src='../Images/leafgray.gif' style='border-width:0px;' />";
            string myRankImage = stargray;
            if (Scores > 0)
            {
                string[] rt = {"starhalf", "star", "moon", "sun", "crown" };//图片类型
                string[] rf= {"leafhalf", "leaf", "flower", "fruit", "mature" };//图片类型
                if (isnew)
                    rt = rf;
                int[] rg = { up, up * 3, up * 9, up * 27 };//升级值
                int[] rc = new int[4];
                rc[3] = Scores / rg[3];
                rc[2] = (Scores % rg[3]) / rg[2];
                rc[1] = ((Scores % rg[3]) % rg[2]) / rg[1];
                rc[0] = (((Scores % rg[3]) % rg[2]) % rg[1]) / rg[0];
                int rcleft = (((Scores % rg[3]) % rg[2]) % rg[1]) % rg[0];
                string tempimage = "";
                for (int i = 3; i > -1; i--)
                {
                    tempimage = tempimage + getimage(rc[i], rt[i+1]);
                }
                if (rcleft > 0)
                {
                    tempimage = tempimage + getimage(1, rt[0]);//如果不够星级的，为半星级
                }
                string upgif = "<img src='../Images/up.gif' style='border-width:0px;' />";
                myRankImage = tempimage+upgif;
            }
            return myRankImage;
        }

        private static string getimage(int counts, string imagetype)
        {
            string rankgif = "<img src='../Images/" + imagetype + ".gif' style='border-width:0px;' />";
            string images = "";
            for (int i = 1; i < counts + 1; i++)
            {
                images = images + rankgif;
            }
            return images;
        }
    }
}
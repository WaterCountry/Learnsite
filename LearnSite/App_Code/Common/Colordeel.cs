using System;
using System.Collections.Generic;
using System.Web;
using System.Drawing;
namespace LearnSite.Common
{
    /// <summary>
    ///Color 的摘要说明
    /// </summary>
    public class ColorDeel
    {
        public ColorDeel()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static Color GroupColor(int Sgroup)
        {
            Sgroup = Sgroup * 9;//拉大相邻号之间的色差

            int redcolor = 60 + Sgroup % 16;
            int greecolor = 100 + Sgroup % 64;
            int bluecolor = 80 + Sgroup % 128;

            return Color.FromArgb(redcolor, greecolor, bluecolor);
           // return Color.FromArgb(greecolor, greecolor, greecolor);
        }
    }

}
using System;
using System.Collections.Generic;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Drawing2D;

namespace LearnSite.Common
{
    /// <summary>
    ///WaterMark 的摘要说明
    /// </summary>
    public class WaterMark
    {
        public WaterMark()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public static Image SetText(Bitmap bp,int mlimit)
        {
            int ow = bp.Width;
            int oh = bp.Height;
            string WatemarkText = ow.ToString() + "×" + oh.ToString();
            int mwidth = ow;
            int mheight = oh;            
            if (mwidth > mlimit)
            {
                mheight = mheight * mlimit / mwidth;
                mwidth = mlimit;
            }
            Image image = new Bitmap(bp, mwidth, mheight);
            bp.Dispose();
            if (mwidth > 100)
            {
                string WatemarkFont = "Arial";               //水印字体
                int WatemarkFontSize = 12;
                int WatemarkPosY = mheight - 20;
                int WatemarkPosX = 2 + WordProcess.GetRandomNum(mwidth - 100);//水平方向上随机取X坐标
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);                
                //将图片绘制到graphics中
                g.DrawImage(image, 0, 0, mwidth, mheight);
                //设置文字的属性
                System.Drawing.Font f = new System.Drawing.Font(WatemarkFont, WatemarkFontSize);
                System.Drawing.Font ff = new System.Drawing.Font(WatemarkFont, WatemarkFontSize);
                //设置字体的颜色
                System.Drawing.Brush b = new System.Drawing.SolidBrush(System.Drawing.Color.Gray);
                System.Drawing.Brush bb = new System.Drawing.SolidBrush(System.Drawing.Color.White);
                //写字
                g.DrawString(WatemarkText, ff, bb, WatemarkPosX - 1, WatemarkPosY - 1);//写白字
                g.DrawString(WatemarkText, f, b, WatemarkPosX, WatemarkPosY);//写灰字
                //释放graphics
                g.Dispose();
            }
            return image;
        }
    }
}
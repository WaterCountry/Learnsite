using System;
using System.Collections.Generic;
using System.Web;
using System.Drawing;
using SimplePsd;
namespace LearnSite.Common
{
    /// <summary>
    ///psdToBmp 的摘要说明
    /// </summary>
    public class psdToBmp
    {
        public psdToBmp()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public static Bitmap myImg(string fpath)
        {
            CPSD psd = new CPSD();//新取一个变量
            int res = psd.Load(fpath);
            if (res == 0)
            {
                return Bitmap.FromHbitmap(psd.GetHBitmap());
            }
            else
            {
                Bitmap bm = new Bitmap(2, 2);
                return bm;
            }
        }
    }
}
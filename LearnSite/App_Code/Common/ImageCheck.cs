using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace LearnSite.Common
{
    /// <summary>
    /// 实现通过文件头2个字节判断图片的格式。
    /// </summary>
    public class ImageCheck
    {
        static ImageCheck()
        {
            _imageTag = InitImageTag();
        }
        private static SortedDictionary<int, ImageType> _imageTag;

        public static readonly string ErrType = ImageType.None.ToString();

        private static SortedDictionary<int, ImageType> InitImageTag()
        {
            SortedDictionary<int, ImageType> list = new SortedDictionary<int, ImageType>();

            list.Add((int)ImageType.BMP, ImageType.BMP);
            list.Add((int)ImageType.JPG, ImageType.JPG);
            list.Add((int)ImageType.GIF, ImageType.GIF);
            list.Add((int)ImageType.PCX, ImageType.PCX);
            list.Add((int)ImageType.PNG, ImageType.PNG);
            list.Add((int)ImageType.PSD, ImageType.PSD);
            list.Add((int)ImageType.RAS, ImageType.RAS);
            list.Add((int)ImageType.SGI, ImageType.SGI);
            list.Add((int)ImageType.TIFF, ImageType.TIFF);
            list.Add((int)ImageType.WMF, ImageType.WMF);
            return list;

        }

        /// <summary>  
        /// 通过文件头判断图像文件的类型  
        /// </summary>  
        /// <param name="path"></param>  
        /// <returns></returns>  
        public static string CheckImageTypeName(string path)
        {
            return CheckImageType(path).ToString();
        }
        /// <summary>  
        /// 通过文件头判断图像文件的类型  
        /// </summary>  
        /// <param name="path"></param>  
        /// <returns></returns>  
        public static bool CheckImageType(string path)
        {
            byte[] buf = new byte[2];
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    int i = sr.BaseStream.Read(buf, 0, buf.Length);
                    if (i != buf.Length)
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
            return CheckImageType(buf);
        }

        /// <summary>  
        /// 通过文件的前两个自己判断图像类型  
        /// </summary>  
        /// <param name="buf">至少2个字节</param>  
        /// <returns></returns>  
        public static bool CheckImageType(byte[] buf)
        {
            if (buf == null || buf.Length < 2)
            {
                return false;
            }

            int key = (buf[1] << 8) + buf[0];
            ImageType s;
            if (_imageTag.TryGetValue(key, out s))
            {
                return true;
            }
            return false;
        }

    }

    /// <summary>  
    /// 图像文件的类型  
    /// </summary>  
    public enum ImageType
    {
        None = 0,
        BMP = 0x4D42,
        JPG = 0xD8FF,
        GIF = 0x4947,
        PCX = 0x050A,
        PNG = 0x5089,
        PSD = 0x4238,
        RAS = 0xA659,
        SGI = 0xDA01,
        TIFF = 0x4949,
        WMF = 0xCDD7
    }
}
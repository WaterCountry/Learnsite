using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.GZip;
namespace LearnSite.Store
{
    /// <summary>
    ///SharpZip 的摘要说明
    /// </summary>
    public class SharpZip
    {
        public SharpZip()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }


        /// <summary>
        /// Create a Zip archive.压缩
        /// </summary>        /// 
        /// <param name="filename">The filename.压缩后的文件名（包含物理路径）</param>
        /// <param name="directory">The directory to Zip.待压缩的文件夹（包含物理路径）</param>
        public static void PackFiles(string filename, string directory)
        {
            try
            {
                FastZip fz = new FastZip();
                fz.CreateEmptyDirectories = true;
                fz.CreateZip(filename, directory, true, "");
                fz = null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Unpacks the files.解压缩
        /// </summary>
        /// <param name="file">The file.待解压文件名（包含物理路径）</param>
        /// <param name="dir">解压到哪个目录中（包含物理路径）</param>
        /// <returns>if succeed return true,otherwise false.</returns>
        public static bool UnpackFiles(string file, string dir)
        {
            try
            {
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                ZipInputStream s = new ZipInputStream(File.OpenRead(file));

                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {

                    string directoryName = Path.GetDirectoryName(theEntry.Name);
                    string fileName = Path.GetFileName(theEntry.Name);
                    string fType = Path.GetExtension(fileName).ToLower();//获取后缀名
                    if (directoryName != String.Empty)
                        Directory.CreateDirectory(dir + directoryName);

                    if (fileName != String.Empty && fType != ".db" && fType != ".xml")
                    {
                        FileStream streamWriter = File.Create(dir + theEntry.Name);

                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }

                        streamWriter.Close();
                    }
                }
                s.Close();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Unpacks the files.解压缩
        /// </summary>
        /// <param name="file">The file.待解压文件名（包含物理路径）</param>
        /// <param name="dir">解压到哪个目录中（包含物理路径）</param>
        /// <returns>if succeed return true,otherwise false.</returns>
        public static bool UnpackQuizFiles(string file, string dir)
        {
            try
            {
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                ZipInputStream s = new ZipInputStream(File.OpenRead(file));

                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {

                    string directoryName = Path.GetDirectoryName(theEntry.Name);
                    string fileName = Path.GetFileName(theEntry.Name);
                    string fType = Path.GetExtension(fileName).ToLower();//获取后缀名
                    if (directoryName != String.Empty)
                        Directory.CreateDirectory(dir + directoryName);

                    if (fileName != String.Empty && fType != ".db" )
                    {
                        FileStream streamWriter = File.Create(dir + theEntry.Name);

                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }

                        streamWriter.Close();
                    }
                }
                s.Close();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Unpacks the files.解压缩其中中xml文件
        /// </summary>
        /// <param name="file">The file.待解压文件名（包含物理路径）</param>
        /// <param name="dir">解压到哪个目录中（包含物理路径）</param>
        /// <returns>if succeed return true,otherwise false.</returns>
        public static bool UnpackFilesXml(string file, string dir)
        {
            ZipInputStream s = new ZipInputStream(File.OpenRead(file));
            try
            {
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                ZipEntry theEntry;
                int i = 0;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    i++;
                    string directoryName = Path.GetDirectoryName(theEntry.Name);
                    string fileName = Path.GetFileName(theEntry.Name);

                    if (directoryName != String.Empty)
                        Directory.CreateDirectory(dir + directoryName);
                    string fileNameType = Path.GetExtension(fileName).ToLower();//获取后缀名
                    if (fileNameType == ".xml")
                    {
                        FileStream streamWriter = File.Create(dir + theEntry.Name);
                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                        streamWriter.Close();
                        s.Close();
                        return true;
                    }
                    if (i > 100)
                    {
                        s.Close();
                        return false;
                    }
                }
                s.Close();
                return false;
            }
            catch (Exception)
            {
                s.Close();                
                return false;
            }
        }
    }
}
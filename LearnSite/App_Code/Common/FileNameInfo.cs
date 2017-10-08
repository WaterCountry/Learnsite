using System.Text;
namespace LearnSite.Common
{
    /// <summary>
    ///FileNameInfo 自定义取完整文件名中的路径、文件名称、后缀
    /// </summary>
    public class FileNameInfo
    {
        private string path, fname, ext;
        public FileNameInfo(string filename)
        {
            char s1 = '/';
            char s2 = '\\';
            char ss = '1';
            if (filename.IndexOf(s1) > 0)
            {
                ss = s1;//如果是虚拟路径
            }
            if (filename.IndexOf(s2) > 0)
            {
                ss = s2; //如果是物理路径
            }
            if (ss != '1')
            {
                int sn = filename.LastIndexOf(ss);//获取最后一个/的位置
                int dn = filename.LastIndexOf('.');//获取最后一个.的位置
                int ln = filename.Length;
                path = filename.Substring(0, sn + 1);
                fname = filename.Substring(sn + 1, dn - sn - 1);
                ext = filename.Substring(dn + 1, ln - dn - 1).ToLower();
            }
        }
        public string Path
        {
            get
            {
                return this.path;
            }
        }
        public string Fname
        {
            get
            {
                return this.fname;
            }
        }
        public string Ext
        {
            get
            {
                return this.ext;
            }
        }
    }
}
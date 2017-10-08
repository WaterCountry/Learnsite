using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using LitJson;
using System.Collections.Generic;

public partial class kindeditor_aspnet_file_manager_json : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            if (LearnSite.Common.XmlHelp.RightSite())
            {
                filemanagerRequest();
            }
            else
            {
                Response.Write("权限不足，无法浏览!");
                Response.End();     
            }
        }
        else
        {
            Response.Write("权限不足，无法浏览!");
            Response.End();
        }
    }

    public void filemanagerRequest()
    {
        string ty = Request.QueryString["Ty"].ToString();
        string cid = Request.QueryString["Cid"].ToString();
        //根目录路径，相对路径
        String rootPath = LearnSite.Store.CourseStore.GetStoreUrl(ty, cid);
        //根目录URL，可以指定绝对路径
        String rootUrl = rootPath.Replace("~", "..");
        //图片扩展名
        String fileTypes = "gif,jpg,jpeg,png,bmp";

        String currentPath = "";
        String currentUrl = "";
        String currentDirPath = "";
        String moveupDirPath = "";

        String dirPath = Server.MapPath(rootPath);
        String dirName = Request.QueryString["dir"];

        //根据path参数，设置各路径和URL
        String path = Request.QueryString["path"];
        path = String.IsNullOrEmpty(path) ? "" : path;
        if (path == "")
        {
            currentPath = dirPath;
            currentUrl = rootUrl;
            currentDirPath = "";
            moveupDirPath = "";
        }
        else
        {
            currentPath = dirPath + path;
            currentUrl = rootUrl + path;
            currentDirPath = path;
            moveupDirPath = Regex.Replace(currentDirPath, @"(.*?)[^\/]+\/$", "$1");
        }

        //排序形式，name or size or type
        String order = Request.QueryString["order"];
        order = String.IsNullOrEmpty(order) ? "" : order.ToLower();

        //不允许使用..移动到上一级目录
        if (Regex.IsMatch(path, @"\.\."))
        {
            Response.Write("Access is not allowed.");
            Response.End();
        }
        //最后一个字符不是/
        if (path != "" && !path.EndsWith("/"))
        {
            Response.Write("Parameter is not valid.");
            Response.End();
        }
        //目录不存在或不是目录
        if (!Directory.Exists(currentPath))
        {
            Response.Write("Directory does not exist.");
            Response.End();
        }

        //遍历目录取得文件信息
        string[] dirList = Directory.GetDirectories(currentPath);
        string[] fileList = Directory.GetFiles(currentPath);

        switch (order)
        {
            case "size":
                Array.Sort(dirList, new NameSorter());
                Array.Sort(fileList, new SizeSorter());
                break;
            case "type":
                Array.Sort(dirList, new NameSorter());
                Array.Sort(fileList, new TypeSorter());
                break;
            case "name":
            default:
                Array.Sort(dirList, new NameSorter());
                Array.Sort(fileList, new NameSorter());
                break;
        }

        Hashtable result = new Hashtable();
        result["moveup_dir_path"] = moveupDirPath;
        result["current_dir_path"] = currentDirPath;
        result["current_url"] = currentUrl;
        result["total_count"] = dirList.Length + fileList.Length;
        List<Hashtable> dirFileList = new List<Hashtable>();
        result["file_list"] = dirFileList;
        for (int i = 0; i < dirList.Length; i++)
        {
            DirectoryInfo dir = new DirectoryInfo(dirList[i]);
            Hashtable hash = new Hashtable();
            hash["is_dir"] = true;
            hash["has_file"] = (dir.GetFileSystemInfos().Length > 0);
            hash["filesize"] = 0;
            hash["is_photo"] = false;
            hash["filetype"] = "";
            hash["filename"] = dir.Name;
            hash["datetime"] = dir.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
            dirFileList.Add(hash);
        }
        for (int i = 0; i < fileList.Length; i++)
        {
            FileInfo file = new FileInfo(fileList[i]);
            Hashtable hash = new Hashtable();
            hash["is_dir"] = false;
            hash["has_file"] = false;
            hash["filesize"] = file.Length;
            bool isphoto = (Array.IndexOf(fileTypes.Split(','), file.Extension.Substring(1).ToLower()) >= 0);
            hash["is_photo"] = isphoto;
            hash["filetype"] = file.Extension.Substring(1);
            hash["filename"] = file.Name;
            hash["datetime"] = file.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
            if (isphoto)
            {
                try
                {
                    System.Drawing.Image img = System.Drawing.Bitmap.FromFile(file.FullName);
                    hash["photo_w"] = img.Width;
                    hash["photo_h"] = img.Height;
                    img.Dispose();
                }
                catch
                {
                    hash["photo_w"] = 60;
                    hash["photo_h"] = 60;
                }

            }
            else
            {
                hash["photo_w"] = 80;
                hash["photo_h"] = 80;
            }
            dirFileList.Add(hash);
        }
        Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
        Response.Write(JsonMapper.ToJson(result));
        Response.End();
    }

    public class NameSorter : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x == null && y == null)
            {
                return 0;
            }
            if (x == null)
            {
                return -1;
            }
            if (y == null)
            {
                return 1;
            }
            FileInfo xInfo = new FileInfo(x.ToString());
            FileInfo yInfo = new FileInfo(y.ToString());

            return xInfo.FullName.CompareTo(yInfo.FullName);
        }
    }

    public class SizeSorter : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x == null && y == null)
            {
                return 0;
            }
            if (x == null)
            {
                return -1;
            }
            if (y == null)
            {
                return 1;
            }
            FileInfo xInfo = new FileInfo(x.ToString());
            FileInfo yInfo = new FileInfo(y.ToString());

            return xInfo.Length.CompareTo(yInfo.Length);
        }
    }

    public class TypeSorter : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x == null && y == null)
            {
                return 0;
            }
            if (x == null)
            {
                return -1;
            }
            if (y == null)
            {
                return 1;
            }
            FileInfo xInfo = new FileInfo(x.ToString());
            FileInfo yInfo = new FileInfo(y.ToString());

            return xInfo.Extension.CompareTo(yInfo.Extension);
        }
    }
}
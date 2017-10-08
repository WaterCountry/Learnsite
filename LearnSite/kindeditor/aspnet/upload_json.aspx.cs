using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Globalization;
using LitJson;

public partial class kindeditor_aspnet_upload_json : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Ty"] != null && Request.QueryString["Cid"] != null)
        {
            if (LearnSite.Common.XmlHelp.RightSite())
            {
                UploadRequest();
            }
            else
            {
                showError("请查看website.xml下参数HostName的值是否包含你的服务器IP或域名！");
            }
        }
        else
        {
            showError("没有传递参数的值，无法上传！");
        }
    }

    public void UploadRequest()
    {
        string ty = Request.QueryString["Ty"].ToString();
        string cid = Request.QueryString["Cid"].ToString();
        //文件保存目录路径
        string savePath = LearnSite.Store.CourseStore.GetSaveUrl(ty, cid);

        //文件保存目录URL
        string saveUrl = savePath.Replace("~", "..");
        string uploadType = "gif,jpg,jpeg,png,bmp,tif,tiff,swf,flv,f4v,mp3,wav,wma,wmv,mid,avi,mpg,mp4,asf,rm,rmvb,doc,docx,xls,xlsx,ppt,pptx,htm,html,txt,zip,rar,gz,bz2,flv,psd,fla,e,chm,mht,sb,sb2,pdf";
        string getxmluploadType = LearnSite.Common.XmlHelp.GetKindeditor();
        if (getxmluploadType != "0")
            uploadType = getxmluploadType;
        //定义允许上传的文件扩展名
        Hashtable extTable = new Hashtable();
        extTable.Add("image", "gif,jpg,jpeg,png,bmp,tif,tiff");
        extTable.Add("flash", "swf");
        extTable.Add("flv", "flv,mp4,f4v");
        extTable.Add("scratch", "sb,sb2");
        extTable.Add("freemind", "mm");
        extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,mp4,asf,rm,rmvb");
        extTable.Add("file", uploadType);

        //最大文件大小
        int maxSize = 524288000;//500MB

        HttpPostedFile imgFile = Request.Files["imgFile"];
        if (imgFile == null)
        {
            showError("请选择文件。");
        }

        String dirPath = Server.MapPath(savePath);

        String dirName = Request.QueryString["dir"];
        if (String.IsNullOrEmpty(dirName))
        {
            dirName = "image";
        }
        if (!extTable.ContainsKey(dirName))
        {
            showError("目录名不正确。");
        }

        String fileName = imgFile.FileName;
        String fileExt = Path.GetExtension(fileName).ToLower();//包含点的后缀名 如 .jpg
        if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
        {
            showError("上传文件大小超过限制。");
        }

        if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
        {
            showError("上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");
        }

        //String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
        String shortFileName = Path.GetFileName(fileName);
        String FilterFileName = LearnSite.Common.WordProcess.FilterFileName(shortFileName) + fileExt;
        switch (fileExt)
        {
            case ".mp4":
            case ".flv":
                //FilterFileName = LearnSite.Common.WordProcess.FilterSpecial(FilterFileName);
                //FilterFileName = LearnSite.Common.Gbk2Spell.Chinese.Convert(FilterFileName);
                break;
            default:
                break;//不转换
        }
           
        String filePath = dirPath + FilterFileName;

        imgFile.SaveAs(filePath);

        String fileUrl = saveUrl + FilterFileName;
        Hashtable hash = new Hashtable();
        hash["error"] = 0;
        hash["url"] = fileUrl;
        Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
        Response.Write(JsonMapper.ToJson(hash));
        Response.End();
    }

    private void showError(string message)
    {
        Hashtable hash = new Hashtable();
        hash["error"] = 1;
        hash["message"] = message;
        Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
        Response.Write(JsonMapper.ToJson(hash));
        Response.End();
    }
}
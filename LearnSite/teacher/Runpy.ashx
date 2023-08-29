<%@ WebHandler Language="C#" Class="Runpy" %>

using System;
using System.Web;
using System.IO;

public class Runpy : IHttpHandler {

    protected bool isplt = false;
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (context.Request.QueryString["file"] != null)
        {
            string Pyfile = context.Request.QueryString["file"].ToString();
            string Pypath = context.Server.MapPath(context.Server.UrlDecode(Pyfile));            
            if (File.Exists(Pypath))
            {
                string msg = "File is exist,ok!";
                msg = runpython(Pypath);
                if (string.IsNullOrEmpty(msg))
                    msg = "服务器上未安装python或程序无输出信息";
                context.Response.Write(msg);                
            }
            else
                context.Response.Write("Not find the file!");
        }
        else
            context.Response.Write("wrong querystring!");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    private string checkpython(string filepath) {

        string strTemp = File.ReadAllText(filepath, System.Text.Encoding.UTF8);
        string word = "plt.show()";
        if (strTemp.IndexOf(word) > -1) {
            string newTemp = @"from io import BytesIO
import base64
bio = BytesIO()
plt.savefig(bio, format='png', bbox_inches='tight', pad_inches=0.0)
data = base64.encodebytes(bio.getvalue()).decode()
src = 'data:image/png;base64,' + str(data)
print(src)";
            strTemp = strTemp.Replace(word, newTemp);
            string datenume="plt"+ DateTime.Now.Millisecond.ToString()+".py";
            string temppath = "~/homework/" + datenume;
            string Pypath = HttpContext.Current.Server.MapPath(temppath);
            System.IO.File.WriteAllText(Pypath, strTemp);
            isplt = true;
            return Pypath;
        }
        return filepath;
    }
    
    private string runpython(string filepath) {
        string newfilepath = checkpython(filepath);
        string pyCmd = "python";
        System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo(pyCmd, newfilepath);
        procStartInfo.RedirectStandardOutput = true;
        procStartInfo.UseShellExecute = false;
        procStartInfo.CreateNoWindow = true;
        using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(procStartInfo))
        {
            using (StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                if (isplt) {
                    string str = "<img src='" + result + "' />";
                    File.Delete(newfilepath);
                    return str;                    
                }
                return result;
            }
        }
    }


}
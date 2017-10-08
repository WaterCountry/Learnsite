<%@ WebHandler Language="C#" Class="saveXml" %>

using System;
using System.IO;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;

public class saveXml : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        //string sketch_name = context.Request.QueryString["sketch_name"].ToString().Trim();
        string sketch_name = context.Request.Form["sketch_name"];//post读取参数内容
        string getData = context.Request.Form["content"];
        
        string store = "~/ardublockly/save/" + sketch_name + "/";
        string savePath = System.Web.HttpContext.Current.Request.MapPath(store);
        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);
        string savename = savePath + sketch_name;
        string savexml = savename + ".xml";
        string savecpp = savename + ".ino";
        //StreamReader reader = new StreamReader(context.Request.InputStream);
        //string getData = reader.ReadToEnd();
        if (getData.Length > 0)
        {
            string spitword = "##WENZHOU%WARMTOWN##";//传递数据的分割符，请勿修改。
            string[] doc = Regex.Split(getData, spitword);
            string header = "//" + sketch_name + Environment.NewLine;
            try
            {
                File.WriteAllText(savexml, doc[0]);
                string cppstr = header + doc[1];
                File.WriteAllText(savecpp, cppstr);

                context.Response.Write("saved success:" + store);
            }
            catch (Exception ec)
            {
                context.Response.Write("unluckly:"+ec.Message);
            }
        }
        else
            context.Response.Write("nothing:some thing wrong with saving.");
    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
  
}
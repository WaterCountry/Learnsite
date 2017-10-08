<%@ WebHandler Language="C#" Class="SaveChinese" %>

using System;
using System.Web;

public class SaveChinese : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string Ptotal = context.Request.QueryString["Apples"].ToString();
        string Pspeed = context.Request.QueryString["Speed"].ToString();
        int result = 0;

        LearnSite.BLL.Pchinese pcbll = new LearnSite.BLL.Pchinese();
        //更新一条记录，如是不存在，则插入一条记录
        result = pcbll.UpdateChineseType(Int32.Parse(Ptotal), Int32.Parse(Pspeed));
        
        context.Response.Write(result.ToString());
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
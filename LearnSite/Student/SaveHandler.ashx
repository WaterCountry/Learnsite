<%@ WebHandler Language="C#" Class="SaveHandler" %>
using System;
using System.Web;

public class SaveHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string mspd = HttpContext.Current.Request.Form["mspd"];
        string rstr = "0";
        LearnSite.BLL.Pfinger pbll = new LearnSite.BLL.Pfinger();
        if (pbll.saveSpd(mspd))
            rstr = "1";
        context.Response.Write(rstr);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
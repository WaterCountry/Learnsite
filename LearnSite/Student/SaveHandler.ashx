<%@ WebHandler Language="C#" Class="SaveHandler" %>
using System;
using System.Web;

public class SaveHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string mysnum = context.Request.QueryString["Mysnum"].ToString();
        string myspd = context.Request.QueryString["Myspd"].ToString();
        string rstr = "0";
        LearnSite.BLL.Pfinger pbll = new LearnSite.BLL.Pfinger();
        if (pbll.saveSpd(mysnum, myspd))
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
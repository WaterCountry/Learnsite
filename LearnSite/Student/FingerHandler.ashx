<%@ WebHandler Language="C#" Class="FingerHandler" %>

using System;
using System.Web;

public class FingerHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string myelevel = context.Request.QueryString["MyElevel"].ToString();
        string eh = "";
        if (!string.IsNullOrEmpty(myelevel))
        {
            LearnSite.BLL.English bl = new LearnSite.BLL.English();
            eh = bl.GetLevelwords(Int32.Parse(myelevel));
        }
        context.Response.Write(eh);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
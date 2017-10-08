<%@ WebHandler Language="C#" Class="GenericHandler1" %>

using System;
using System.Web;
using System.IO;

public class GenericHandler1 : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "application/octet-stream";
        if (context.Request.QueryString["id"] != null )
        {
            string id = context.Request.QueryString["id"].ToString();
            LearnSite.BLL.Works bll = new LearnSite.BLL.Works();
            bll.SaveProject(id);
        }
    }
        
    public bool IsReusable {
        get {
            return false;
        }
    }    
    
}
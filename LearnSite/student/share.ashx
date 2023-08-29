<%@ WebHandler Language="C#" Class="share" %>

using System;
using System.Web;

public class share : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string isGroup = context.Request.QueryString["isGroup"].ToString();
        string result = LearnSite.Common.ShareDisk.SaveFileNew(bool.Parse(isGroup));
        context.Response.Write(result);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
<%@ WebHandler Language="C#" Class="saveform" %>

using System;
using System.Web;

public class saveform : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string result = savemyform();
        context.Response.Write(result);
    }

    private string savemyform()
    {
        LearnSite.BLL.TxtFormBack tkbll = new LearnSite.BLL.TxtFormBack();
        return tkbll.SaveFormContent();
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}
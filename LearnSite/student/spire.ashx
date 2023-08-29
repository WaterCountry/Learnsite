<%@ WebHandler Language="C#" Class="spire" %>

using System;
using System.Web;

public class spire : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";


        string mid = HttpContext.Current.Request.Form["mid"];
        string num = HttpContext.Current.Request.Form["num"];
        bool ok = LearnSite.Common.SpireOffice.OfficeToPng(Int32.Parse(mid), num);
        if(ok)
            context.Response.Write("office to png Succeed!");   
        else
            context.Response.Write("office to png Wrong!");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
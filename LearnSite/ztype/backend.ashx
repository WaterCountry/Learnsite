<%@ WebHandler Language="C#" Class="backend" %>

using System;
using System.Web;

public class backend : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string Sscore = HttpContext.Current.Request.Form["score"];

        if (!String.IsNullOrEmpty(Sscore) )
        {
            int score = Int32.Parse(Sscore);            
            LearnSite.BLL.Students bll = new LearnSite.BLL.Students();
            bll.updateSztype(score);
            context.Response.Write("saved");
        }
        else
            context.Response.Write("lost");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
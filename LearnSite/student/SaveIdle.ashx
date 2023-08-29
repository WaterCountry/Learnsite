<%@ WebHandler Language="C#" Class="SaveIdle" %>

using System;
using System.Web;

public class SaveIdle : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string pid = HttpContext.Current.Request.Form["pid"];
        string score = HttpContext.Current.Request.Form["score"];
        string cid = HttpContext.Current.Request.Form["cid"];
        string nid = HttpContext.Current.Request.Form["nid"];
        string lid = HttpContext.Current.Request.Form["lid"];
        string answer = HttpContext.Current.Request.Form["answer"];
        
        
        LearnSite.BLL.Solves bll = new LearnSite.BLL.Solves();
        bool result = bll.SaveScore(Int32.Parse(pid), Int32.Parse(score), answer, Int32.Parse(nid), Int32.Parse(cid), Int32.Parse(lid));
        if(result)     
            context.Response.Write("保存成功!");
        else
            context.Response.Write("测评已通过!");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
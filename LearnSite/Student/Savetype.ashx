<%@ WebHandler Language="C#" Class="Savetype" %>

using System;
using System.Web;

public class Savetype : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string Ptid = context.Request.QueryString["Ptid"].ToString();
        string TypeScore = context.Request.QueryString["Ts"].ToString();
        LearnSite.BLL.Ptyper bll = new LearnSite.BLL.Ptyper();
        context.Response.Write(bll.Savemytype(Ptid, TypeScore));
    } 
        
    public bool IsReusable {
        get {
            return false;
        }
    }    

}
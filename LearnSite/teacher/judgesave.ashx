<%@ WebHandler Language="C#" Class="judgesave" %>

using System;
using System.Web;

public class judgesave : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.QueryString["id"] != null)
        {
            string id = context.Request.QueryString["id"].ToString();
            LearnSite.BLL.JudgeArg jbll = new LearnSite.BLL.JudgeArg();
            try
            {
                if (context.Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
                {

                    LearnSite.Model.TeaCook tcook = new LearnSite.Model.TeaCook();
                    int newid= jbll.Addsave(Int32.Parse(id),tcook.Hid);
                    context.Response.Write(newid);
                }
                else
                    context.Response.Write(-1);

            }
            catch(Exception ex)
            {
                context.Response.Write(ex.ToString());
            }
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
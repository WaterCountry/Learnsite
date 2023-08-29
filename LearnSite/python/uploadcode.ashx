<%@ WebHandler Language="C#" Class="uploadcode" %>

using System;
using System.Web;

public class uploadcode : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.QueryString["id"] != null)
        {
            string id = context.Request.QueryString["id"].ToString();
            LearnSite.BLL.TurtleAnswer bll = new LearnSite.BLL.TurtleAnswer();
            try
            {
                int newid = bll.Addsave();
                context.Response.Write(newid);

            }
            catch (Exception ex)
            {
                context.Response.Write(ex.ToString());
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
<%@ WebHandler Language="C#" Class="getproject" %>

using System;
using System.Web;
using System.IO;
public class getproject : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.QueryString["id"] != null)
        {
            string id=context.Request.QueryString["id"].ToString();
            LearnSite.BLL.Works bll = new LearnSite.BLL.Works();
            bll.SworkToBytes(id);           
        }
        else
            context.Response.BinaryWrite(null);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
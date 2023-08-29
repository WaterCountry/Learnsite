﻿<%@ WebHandler Language="C#" Class="uploadpython" %>

using System;
using System.Web;

public class uploadpython : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.QueryString["id"] != null)
        {
            string id = context.Request.QueryString["id"].ToString();
            LearnSite.BLL.Works bll = new LearnSite.BLL.Works();
            try
            {
                if (context.Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname] != null)
                {
                    bll.SavePython(id);
                    context.Response.Write("保存成功！");
                }
                else
                    context.Response.Write("保存失败！");

            }
            catch
            {
                context.Response.Write("保存失败！");
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
<%@ WebHandler Language="C#" Class="saveseat" %>

using System;
using System.Web;

public class saveseat : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string myhid = context.Request.QueryString["Hid"].ToString();
        string mycollects = context.Request.QueryString["Collects"].ToString();
        string rstr = "0";
        bool isok = false;
        if (HttpContext.Current.Request.Cookies[LearnSite.Common.CookieHelp.mngCookieNname] != null)//如果是管理员则
        {
            isok = savehouse(myhid, mycollects);
        }
        if (isok)
        {
            rstr = "1";
        }
        context.Response.Write(rstr);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    private bool savehouse(string hid,string hseat)
    {
        LearnSite.BLL.House bll = new LearnSite.BLL.House();
        return bll.UpdateHseat(Int32.Parse(hid), hseat);
    }
}
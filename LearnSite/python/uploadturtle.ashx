<%@ WebHandler Language="C#" Class="uploadturtle" %>

using System;
using System.IO;
using System.Web;

public class uploadturtle : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        int res = 0;
        int hid = 0;
        int sid = 0;
        if (context.Request.QueryString["id"] != null)
        {
            LearnSite.Model.TeaCook tcook = new LearnSite.Model.TeaCook();
            LearnSite.Model.Cook cook = new LearnSite.Model.Cook();
            if (tcook != null) hid = tcook.Hid;
            if (cook != null) sid = cook.Sid;

            string id = context.Request.QueryString["id"].ToString();
            LearnSite.BLL.Turtle bll = new LearnSite.BLL.Turtle();
            try
            {
                res = bll.Upload(id, hid, sid);
                context.Response.Write(res);
            }
            catch
            {
                res = -1;
            }

        }
        context.Response.Write(res);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }


}
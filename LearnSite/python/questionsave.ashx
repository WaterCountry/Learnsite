<%@ WebHandler Language="C#" Class="questionsave" %>

using System;
using System.Web;

public class questionsave : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.QueryString["id"] != null)
        {
            string id = context.Request.QueryString["id"].ToString();
            LearnSite.BLL.TurtleQuestion bll = new LearnSite.BLL.TurtleQuestion();
            try
            {
                LearnSite.Model.TeaCook tcook = new LearnSite.Model.TeaCook();
                if (tcook.IsExist())
                {
                    int newid = bll.Addsave(tcook.Hid);
                    context.Response.Write(newid);
                }
                else
                    context.Response.Write(-1);

            }
            catch (Exception ex)
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
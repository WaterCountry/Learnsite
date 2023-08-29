<%@ WebHandler Language="C#" Class="push" %>

using System;
using System.Web;

public class push : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string Title = context.Request.QueryString["Title"].ToString();
        string Level = context.Request.QueryString["Level"].ToString();
        string Step = context.Request.QueryString["Step"].ToString();
        string Note = HttpContext.Current.Request.Form["Note"];
        string myLevel = HttpContext.Current.Request.Form["Level"];
        
        LearnSite.Model.Cook cook = new LearnSite.Model.Cook();
        LearnSite.BLL.Game bll = new LearnSite.BLL.Game();
        if (bll.ExistsSave(cook.Sid, Title, Int32.Parse(Level)))
        {
            bll.UpdateSave(cook.Sid, Title, Int32.Parse(Level), Note, Int32.Parse(Step));
        }
        else
        {
            LearnSite.Model.Game model = new LearnSite.Model.Game();
            model.Gsid = cook.Sid;
            model.Gsname = HttpUtility.UrlDecode(cook.Sname);
            model.Gtitle = Title;
            model.Gsave = Int32.Parse(Level);
            if (!string.IsNullOrEmpty(myLevel))
            {
                model.Gsave = Int32.Parse(myLevel); 
            }
            model.Gnote = Note;
            model.Gnum = Int32.Parse(Step);
            model.Gdate = DateTime.Now;
            model.Gscore = 2;

            bll.Add(model);         
                    
        }
        
        context.Response.Write("ok");
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
<%@ WebHandler Language="C#" Class="ChineseHandler" %>

using System;
using System.Web;

public class ChineseHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string Nid = context.Request.QueryString["Nid"].ToString();
        string ChineseWords = GetWords(Nid);

        context.Response.Write(FormatWords(ChineseWords));
    }

    private string GetWords(string Nid)
    {
        LearnSite.BLL.Chinese cbll = new LearnSite.BLL.Chinese();
        return cbll.GetContent(Nid);
    }
    private string FormatWords(string words)
    {
        int wcount = words.Length;
        words = words.Trim();
        words = words.Replace('\r', ' ');
        words = words.Replace('\n', ' ');
        words = words.Replace("  ", " ");
        words = words.Replace("  ", " ");
        string[] sArray = words.Split(new char[] { '，', '。', '；', '？', '！','“','”', '：', ' ', '　' });
        string temp = "";
        foreach (string c in sArray)
        {
            if (LearnSite.Common.WordProcess.IsZh(c))
                temp = temp + c.Trim() + "|";
        }
        temp = temp.TrimEnd('|');
        return temp;
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
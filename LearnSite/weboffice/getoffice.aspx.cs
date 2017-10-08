using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
public partial class weboffice_getoffice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        string id = Request.QueryString["id"];
        if (id == null || id == "")
        {
            Response.End();
            return;
        }
        else
        {
            LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
            string officeurl = wbll.GetWorkWurl(Int32.Parse(id));
            if (officeurl != "")
            {
                string officepath = MapPath(officeurl);
                if (File.Exists(officepath))
                {
                    FileStream myfileStream;
                    long fileSize;
                    myfileStream = new FileStream(officepath, FileMode.Open);
                    fileSize = myfileStream.Length;
                    byte[] Buffer = new byte[(int)fileSize];
                    myfileStream.Read(Buffer, 0, (int)fileSize);
                    myfileStream.Close();
                    Response.BinaryWrite(Buffer);
                    Response.End();
                }
            }
        }
        Response.End();
    }
}
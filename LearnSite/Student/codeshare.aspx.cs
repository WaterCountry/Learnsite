using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_codeshare : System.Web.UI.Page
{
    protected string Id = "";
    protected string Microworld = "true";
    protected string Owner = "";
    protected string Pic ="";
    protected string Titles = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showScratch();
        }
    }

    protected void showScratch()
    {
        if (Request.QueryString["id"] != null)
        {
            int wid = Int32.Parse(Request.QueryString["id"].ToString());
            LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
            LearnSite.Model.Works model = new LearnSite.Model.Works();

            model = wbll.GetModel(wid);
            Id = wid.ToString();
            LabelTitle.Text =model.Wtitle + " 作者：" + model.Wname;
            Owner = Owner + " 作者：" + model.Wname;
            Pic ="http://"+ Request.Url.Host + model.Wthumbnail.Replace("~", "");
            Titles = model.Wtitle;
        }
        else
        {
            Pic = Request.ApplicationPath + "Images/thumbnail.png";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_codeproject : System.Web.UI.Page
{
    protected string Filename = "";
    protected string Id = "";
    protected string Microworld = "false";
    protected string Owner = "";
    protected string Titles = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowMission();
        }
    }
    private void ShowMission()
    {
        if (Request.QueryString["id"] != null)
        {
            int wid = Int32.Parse(Request.QueryString["id"].ToString());
            LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
            LearnSite.Model.Works model = new LearnSite.Model.Works();
            string ipwid = "ip" + LearnSite.Common.Computer.MyIp().Replace('.', 'a') + "_" + wid.ToString();
            if (Session[ipwid] == null)
            {
                wbll.UpdateWhit(wid);
                Session[ipwid] = wid;
            }

            model = wbll.GetModel(wid);
            Id = wid.ToString();
            Owner = model.Wname;
            Titles = model.Wtitle;
        }
    }
}
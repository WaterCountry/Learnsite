using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_stuworkcircle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + " 本学期作品查询";
            Btnclose.Attributes.Add("onclick", "window.opener=null;window.open('','_self'); window.close()");
            ReadWork();
            showflash();
        }
    }

    private void ReadWork()
    {
        if (Request.QueryString["Snum"] != null)
        {
            string Snum = Request.QueryString["Snum"].ToString();
            LearnSite.Model.Students smodel = new LearnSite.Model.Students();
            LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
            smodel = sbll.SnumGetModel(Snum);
            LabelSnum.Text = Snum;
            LabelSname.Text = smodel.Sname;
            LabelWscore.Text = smodel.Sscore.ToString();
            int wgrade = smodel.Sgrade.Value;
            if (Request.QueryString["Sgrade"] != null)
            {
                wgrade = Int32.Parse(Request.QueryString["Sgrade"].ToString());
            }
            int wterm = Int32.Parse(LearnSite.Common.XmlHelp.GetTerm());
            if (Request.QueryString["Sterm"] != null)
            {
                wterm = Int32.Parse(Request.QueryString["Sterm"].ToString());
            }

            LearnSite.BLL.Works ws = new LearnSite.BLL.Works();
            DDLstore.DataSource = ws.ShowThisTermWorksCircle(Snum, wgrade, wterm);
            DDLstore.DataTextField = "Mtitle";
            DDLstore.DataValueField = "Wurl";
            DDLstore.DataBind();
            int curindex = Int32.Parse(lbcurindex.Text);
            if (DDLstore.Items.Count > 0)
            {
                int allindex = DDLstore.Items.Count - 1;
                if (curindex == allindex)
                    lbcurindex.Text = "0";
                if (curindex < allindex)
                    DDLstore.SelectedIndex = curindex;
            }
        }
    }
    private void showflash()
    {
        int icn = DDLstore.Items.Count;
        if (icn > 0)
        {
            string Wurl = DDLstore.SelectedValue;
            string ext = LearnSite.Common.WordProcess.getext(Wurl);
            string htmname = "";
            if (string.IsNullOrEmpty(ext))
            {
                ext = "htm";
            }
            int cur = DDLstore.SelectedIndex + 1;
            lbcount.Text = cur.ToString() + "/" + icn.ToString();
            Literal1.Text = LearnSite.Common.WordProcess.SelectWriteDisplayNew(ext, Wurl, true, htmname);
        }
        else
        {
            Literal1.Text = "当前没有学生作品";
        }
    }
    protected void DDLstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        showflash();
    }
    protected void ImgBtnLeft_Click(object sender, ImageClickEventArgs e)
    {
        int sdx = DDLstore.SelectedIndex;
        if (sdx > 0)
        {
            DDLstore.SelectedIndex = sdx - 1;
        }
        showflash();
    }
    protected void ImgBtnright_Click(object sender, ImageClickEventArgs e)
    {
        int sdx = DDLstore.SelectedIndex;
        if (sdx < DDLstore.Items.Count - 1)
        {
            DDLstore.SelectedIndex = sdx + 1;
        }
        showflash();
    }
    protected void ImgBtn_Click(object sender, ImageClickEventArgs e)
    {
        lbcount.Text = "0";
        int curindex = DDLstore.SelectedIndex;//保存当前索引位置
        ReadWork();
        if (DDLstore.Items.Count > curindex)
            DDLstore.SelectedIndex = curindex;
        showflash();
    }
}
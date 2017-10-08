using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Workshow_search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            showyears();
            showWgrade();
            showWclass();
            showWterm();
            showWcid(); 
            showWmid();
            showWorks();
        }
    }

    private void showyears()
    {
        Rblyear.Items.Clear();
        LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
        Rblyear.DataSource = wbll.ShowWyears();
        Rblyear.DataTextField = "Wyear";
        Rblyear.DataValueField = "Wyear";
        Rblyear.DataBind();
        int icount = Rblyear.Items.Count;
        if (icount > 0)
        {
            Rblyear.SelectedIndex = icount - 1;
        }
    }
    private void showWgrade()
    {
        Rblgrade.Items.Clear();
        string yearselect = Rblyear.SelectedValue;
        if (!string.IsNullOrEmpty(yearselect))
        {
            LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
            Rblgrade.DataSource = wbll.ShowWgrades(Int32.Parse(yearselect));
            Rblgrade.DataTextField = "Wgrade";
            Rblgrade.DataValueField = "Wgrade";
            Rblgrade.DataBind();
            if (Rblgrade.Items.Count > 0)
                Rblgrade.SelectedIndex = 0;
        }
    }
    private void showWclass()
    {
        Rblclass.Items.Clear();
        string gradeselect = Rblgrade.SelectedValue;
        if (!string.IsNullOrEmpty(gradeselect))
        {
            string yearselect = Rblyear.SelectedValue;
            LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
            Rblclass.DataSource = wbll.ShowWclass(Int32.Parse(yearselect), Int32.Parse(gradeselect));
            Rblclass.DataTextField = "Wclass";
            Rblclass.DataValueField = "Wclass";
            Rblclass.DataBind();
            if (Rblclass.Items.Count > 0)
                Rblclass.SelectedIndex = 0;
        }
    }
    private void showWterm()
    {
        string thisTerm = LearnSite.Common.XmlHelp.GetTerm();
        Rblterm.SelectedValue = thisTerm;
    }
    private void showWcid()
    {
        Rblcourse.Items.Clear();
        string classselect = Rblclass.SelectedValue;
        if (!string.IsNullOrEmpty(classselect))
        {
            string yearselect = Rblyear.SelectedValue;
            string gradeselect = Rblgrade.SelectedValue;
            string termselect = Rblterm.SelectedValue;
            LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
            Rblcourse.DataSource = wbll.ShowWclassWcids(Int32.Parse(yearselect), Int32.Parse(gradeselect), Int32.Parse(classselect), Int32.Parse(termselect));
            Rblcourse.DataTextField = "Wcid";//要改为序号
            Rblcourse.DataValueField = "Wcid";
            Rblcourse.DataBind();
            int mcount=Rblcourse.Items.Count;
            if (mcount > 0)
            {
                Rblcourse.SelectedIndex = 0;
                for (int i = 0; i < mcount; i++)
                {
                    int ks = i + 1;
                    Rblcourse.Items[i].Text = ks.ToString();
                }
            }
        }
    }
    private void showWmid()
    {
        Rblmission.Items.Clear();
        string cidselect = Rblcourse.SelectedValue;
        if (!string.IsNullOrEmpty(cidselect))
        {
            string yearselect = Rblyear.SelectedValue;
            string gradeselect = Rblgrade.SelectedValue;
            string termselect = Rblterm.SelectedValue;
            string classselect = Rblclass.SelectedValue;
            LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
            Rblmission.DataSource = wbll.ShowWclassWmids(Int32.Parse(yearselect), Int32.Parse(gradeselect), Int32.Parse(classselect), Int32.Parse(termselect), Int32.Parse(cidselect));
            Rblmission.DataTextField = "Wmid";
            Rblmission.DataValueField = "Wmid";
            Rblmission.DataBind();
            int ncount = Rblmission.Items.Count;
            if (ncount > 0)
            {
                Rblmission.SelectedIndex = 0;
                for (int i = 0; i < ncount; i++)
                {
                    int ms = i + 1;
                    Rblmission.Items[i].Text = ms.ToString();
                }
            }
        }
    }
    private void showWorks()
    {
        string midselect = Rblmission.SelectedValue;
        string cidselect = Rblcourse.SelectedValue;
        if (!string.IsNullOrEmpty(midselect) && !string.IsNullOrEmpty(cidselect))
        {
            string yearselect = Rblyear.SelectedValue;
            string gradeselect = Rblgrade.SelectedValue;
            string classselect = Rblclass.SelectedValue;
            string termselect = Rblterm.SelectedValue;
            string displaymodel = Rbldisplay.SelectedValue;
            LearnSite.BLL.Courses cbll = new LearnSite.BLL.Courses();
            string ctitle = cbll.GetTitle(Int32.Parse(cidselect));
            LearnSite.BLL.Mission mbll = new LearnSite.BLL.Mission();
            string mtitle = mbll.GetMissionTitle(Int32.Parse(midselect));
            LabelTitle.Text = "【" + ctitle + "】" + mtitle;
            LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
            switch (displaymodel)
            {
                case "1":
                    DLworks.DataSource = wbll.ShowWclassWorks(Int32.Parse(yearselect), Int32.Parse(gradeselect), Int32.Parse(classselect), Int32.Parse(termselect), Int32.Parse(midselect));
                    DLworks.DataBind();
                    divlist.Visible = true;
                    divview.Visible = false;
                    Literal1.Visible = false;
                    break;
                case "2":
                    DDLstore.DataSource = wbll.ShowWclassWorks(Int32.Parse(yearselect), Int32.Parse(gradeselect), Int32.Parse(classselect), Int32.Parse(termselect), Int32.Parse(midselect));
                    DDLstore.DataTextField = "Wname";
                    DDLstore.DataValueField = "Wurl";
                    DDLstore.DataBind();
                    divlist.Visible = false;
                    divview.Visible = true;
                    Literal1.Visible = true;
                    if (DDLstore.Items.Count > 0)
                    {
                        DDLstore.SelectedIndex = 0;
                        showflash();
                    }
                    break;
            }
        }
        else
        {
            LabelTitle.Text = "没有作品！";
            divlist.Visible = false;
            divview.Visible = false;
            Literal1.Visible = false;
        }
    }
    private void showflash()
    {
        int icn = DDLstore.Items.Count;
        if (icn > 0)
        {
            string Wurl = DDLstore.SelectedValue;
            string ext = LearnSite.Common.WordProcess.getext(Wurl);
            string htmname = "index.htm";
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
    protected void ImgBtnleft_Click(object sender, ImageClickEventArgs e)
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
    protected void DDLstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        showflash();
    }
    protected void Rblyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        showWgrade();
        showWclass();
        showWterm();
        showWcid();
        showWmid();
        showWorks();
    }
    protected void Rblgrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        showWclass();
        showWterm();
        showWcid();
        showWmid();
        showWorks();
    }
    protected void Rblclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        showWterm();
        showWcid();
        showWmid();
        showWorks();
    }
    protected void Rblterm_SelectedIndexChanged(object sender, EventArgs e)
    {
        showWcid();
        showWmid();
        showWorks();
    }
    protected void Rblcourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        showWmid();
        showWorks();
    }
    protected void Rblmission_SelectedIndexChanged(object sender, EventArgs e)
    {
        showWorks();
    }
    protected void Rbldisplay_SelectedIndexChanged(object sender, EventArgs e)
    {
        showWorks();
    }
    protected void DLworks_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "S")
        {
            string Wurl = e.CommandArgument.ToString();
            if (Wurl != "")
            {
                string ext = LearnSite.Common.WordProcess.getext(Wurl);
                string htmname = "index.htm";
                if (string.IsNullOrEmpty(ext))
                {
                    ext = "htm";
                }
                Literal1.Text = LearnSite.Common.WordProcess.SelectWriteTeaNew(ext, Wurl, true, htmname);
                Literal1.Visible = true;
            }
        }
    }
}
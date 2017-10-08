using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_softadd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "资源添加页面";
            GetCategory();
        }
    }
    private void GetCategory()
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
            LearnSite.BLL.SoftCategory ybll = new LearnSite.BLL.SoftCategory();
            ddlcategory.DataSource = ybll.GetListCategory();
            ddlcategory.DataTextField = "Ytitle";
            ddlcategory.DataValueField = "Yid";
            ddlcategory.DataBind();
            if (Session[Hid + "SoftCateGory"] != null)
            {
                int index = Convert.ToInt32(Session[Hid + "SoftCateGory"]);
                if (index > 0 && index < ddlcategory.Items.Count)
                    ddlcategory.SelectedIndex = index;
            }
        }
    }
    protected void Btnadd_Click(object sender, EventArgs e)
    {
        AddSoft();
    }
    private void AddSoft()
    {
        if (Texttitle.Text != "" && ddlcategory.SelectedValue != "")
        {
            string Fhid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
            LearnSite.Model.Soft soft = new LearnSite.Model.Soft();
            soft.Fyid = Int32.Parse(ddlcategory.SelectedValue);
            string fclass=DDLclass.SelectedValue;
            soft.Fclass = fclass;
            soft.Fup = false;
            if (fclass == "教程" || fclass == "微课")
                soft.Fup = true;
            soft.Fcontent = HttpUtility.HtmlEncode(Request.Form["textareaItem"].Trim());
            soft.Fdate = DateTime.Now;
            soft.Ffiletype = "";
            soft.Fhit = 0;
            soft.Ftitle = Texttitle.Text.Trim();
            soft.Ffiletype = "";
            soft.Furl = "";
            soft.Fhide = CheckBoxFhide.Checked;
            soft.Fopen = Int32.Parse(DDLopen.SelectedValue);
            
            if (!CheckBoxFhid.Checked)
                soft.Fhid = Int32.Parse(Fhid);
            else
                soft.Fhid = -1;
            if (soft.Ftitle != "" && soft.Fcontent != "")
            {
                if (FUsoft.PostedFile.FileName != "")
                {
                    string uploadfile = FUsoft.PostedFile.FileName;
                    soft.Ffiletype = uploadfile.Substring(uploadfile.LastIndexOf(".") + 1);
                    soft.Furl = LearnSite.Common.Fileupload.Fupload(FUsoft);
                }
                LearnSite.BLL.Soft bll = new LearnSite.BLL.Soft();
                bll.Add(soft);
                Labelmsg.Text = "添加学案资源成功！";
                System.Threading.Thread.Sleep(500);
                //Labelmsg.Text = Labelmsg.Text + soft.Furl;
                Response.Redirect("~/Teacher/soft.aspx", false);
            }
            else
            {
                Labelmsg.Text = "清添加学案资源内容介绍！";
            }
        }
        else
        {
            Labelmsg.Text = "请添加学案资源标题或无资源分类！";
        }

    }


    protected void Btnreturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Teacher/soft.aspx", false);
    }
}



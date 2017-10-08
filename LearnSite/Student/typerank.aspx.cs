using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_typerank : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showwai();
        }
    }

    private int gettop()
    {
        string ntop = Labeltop.Text.Trim();
        int np = Int32.Parse(ntop);
        return np;
    }
    private void showwai()
    {
        LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
        DataList_wai.DataSource = rbll.GetAllGrade();
        DataList_wai.DataBind();
        DataList_enwai.DataSource = DataList_wai.DataSource;
        DataList_enwai.DataBind();

        LearnSite.BLL.Ptyper pbll = new LearnSite.BLL.Ptyper();

        DataList_allc.DataSource = pbll.ShowSchoolTopTypeScore(gettop());
        DataList_allc.DataBind();

        LearnSite.BLL.Pfinger fbll = new LearnSite.BLL.Pfinger();

        DataList_enall.DataSource = fbll.ShowSchoolTopFingerScore(gettop());
        DataList_enall.DataBind();
    }
    protected void DataList_wai_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        Label lb = (Label)e.Item.FindControl("Labelgrade");
        DataList dl = (DataList)e.Item.FindControl("DataList_li");
        LearnSite.BLL.Ptyper pbll=new LearnSite.BLL.Ptyper();
        int gd=Int32.Parse(lb.Text);
        dl.DataSource = pbll.ShowTopTypeScore(gd, gettop());
        dl.DataBind();
        ((Label)e.Item.FindControl("Labelgrade")).Text = LearnSite.Common.WordProcess.ChineseNum(gd) + "年级";
    }
    protected void DataList_li_ItemDataBound(object sender, DataListItemEventArgs e)
    {
         Label lbli = (Label)e.Item.FindControl("Labelpsnum");
         Image mg = (Image)e.Item.FindControl("Image1");
         mg.ImageUrl = LearnSite.Common.Photo.GetStudentPhotoUrl(lbli.Text, "男");
    }
    protected void DataList_enwai_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        Label enlb = (Label)e.Item.FindControl("enLabelgrade");
        DataList endl = (DataList)e.Item.FindControl("DataList_enli");
        LearnSite.BLL.Pfinger fbll = new LearnSite.BLL.Pfinger();
        int egd = Int32.Parse(enlb.Text);
        endl.DataSource = fbll.ShowTopFingerScore(egd, gettop());
        endl.DataBind(); ((Label)e.Item.FindControl("enLabelgrade")).Text = LearnSite.Common.WordProcess.ChineseNum(egd) + "年级";    
    }
    protected void DataList_enli_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        Label enlbli = (Label)e.Item.FindControl("enLabelpsnum");
        Image enmg = (Image)e.Item.FindControl("enImage1");
        enmg.ImageUrl = LearnSite.Common.Photo.GetStudentPhotoUrl(enlbli.Text, "女");
    }
    protected void DataList_allc_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        Label lbli = (Label)e.Item.FindControl("allcLabelpsnum");
        Image mg = (Image)e.Item.FindControl("allcImage1");
        mg.ImageUrl = LearnSite.Common.Photo.GetStudentPhotoUrl(lbli.Text, "男");
    }
    protected void DataList_enall_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        Label enlbli = (Label)e.Item.FindControl("lenLabelpsnum");
        Image enmg = (Image)e.Item.FindControl("lenImage1");
        enmg.ImageUrl = LearnSite.Common.Photo.GetStudentPhotoUrl(enlbli.Text, "女");
    }
}
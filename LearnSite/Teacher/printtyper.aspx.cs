using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_printtyper : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showGrade();
        }
    }
    protected void Btnbrowse_Click(object sender, EventArgs e)
    {
        showtops();
    }

    private void showGrade()
    {
        LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
        DDLgrade.DataSource = rbll.GetAllGrade();
        DDLgrade.DataTextField = "Rgrade";
        DDLgrade.DataValueField = "Rgrade";
        DDLgrade.DataBind();
    }
    private void showtops()
    {
        if (DDLgrade.SelectedValue != "")
        {
            int grade = Int32.Parse(DDLgrade.SelectedValue);
            int top = Int32.Parse(DDLtop.SelectedValue);
            string tys = DDLtype.SelectedValue;
            switch (tys)
            { 
                case "0":
                    LearnSite.BLL.Ptyper pbll = new LearnSite.BLL.Ptyper();
                    DataList_A4.DataSource = pbll.ShowTopTypeScore(grade, top);
                    DataList_A4.DataBind();
                    break;
                case "1":
                    LearnSite.BLL.Pfinger fbll = new LearnSite.BLL.Pfinger();
                    DataList_A4.DataSource = fbll.ShowTopFingerScoreAs(grade, top);
                    DataList_A4.DataBind();
                    break;            
            }
        }
    }
    protected void DataList_A4_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        Label lbli = (Label)e.Item.FindControl("psnum");
        Image mg = (Image)e.Item.FindControl("Image1");
        mg.ImageUrl = LearnSite.Common.Photo.GetStudentPhotoUrl(lbli.Text, "女");
    }
}
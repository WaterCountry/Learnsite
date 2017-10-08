using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_myseat : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showhouse();
            showSeat();
        }
    }

    private void showhouse()
    {
        LearnSite.BLL.Computers cbll = new LearnSite.BLL.Computers();
        DDLhouse.DataSource = cbll.CmpRoom();
        DDLhouse.DataTextField = "Pm";
        DDLhouse.DataValueField = "Pm";
        DDLhouse.DataBind();    
    }
    private void showSeat()
    {
        string croom = DDLhouse.SelectedValue;
        if (!string.IsNullOrEmpty(croom))
        {
            LearnSite.BLL.Computers cbll = new LearnSite.BLL.Computers();
            LearnSite.Model.SeatCollect seact = new LearnSite.Model.SeatCollect();
            seact = cbll.GetSeat(croom);//select Pip,Pmachine,Px,Py from Computers
            DataList1.DataSource = seact.Dt;
            DataList1.DataBind();
            DataList1.RepeatColumns = seact.Column;
            Labelnum.Text = "当前机房电脑数量：" + seact.Online.ToString() + "台  列数为" + seact.Column.ToString();
        }
    }
    protected void DDLhouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        showSeat();
    }
    protected void DataList1_DataBinding(object sender, EventArgs e)
    {

    }
    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
       Label hlpm = (Label)e.Item.FindControl("Labelpm");
       Label hlip = (Label)e.Item.FindControl("Labelpip");
       if (string.IsNullOrEmpty(hlpm.Text))
       {
           if (string.IsNullOrEmpty(hlip.Text))
           {
               hlpm.Text = "空地";
               hlpm.ForeColor = System.Drawing.Color.Red;
           }
           else
           {
               hlpm.Text = "电脑";
               hlpm.ForeColor = System.Drawing.Color.DarkGreen;   
           }
       }
    }
}
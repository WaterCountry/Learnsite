using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_gaugeitem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "自定义评价描述页面";
            showGauge();
            showGaugeItem();
        }
    }

    private void showGaugeItem()
    {
        if (Request.QueryString["Gid"] != null)
        {
            string Mgid = Request.QueryString["Gid"].ToString();
            LearnSite.BLL.GaugeItem mbll = new LearnSite.BLL.GaugeItem();
            GVGaugeItem.DataSource = mbll.GetListMgid(Mgid);
            GVGaugeItem.DataBind();
            int currentMsort = GVGaugeItem.Rows.Count + 1;
            DDLsort.SelectedValue = currentMsort.ToString();
        }
    }
    private void showGauge()
    {
        if (Request.QueryString["Gid"] != null)
        {
            string Mgid = Request.QueryString["Gid"].ToString();
            LearnSite.BLL.Gauge gbll = new LearnSite.BLL.Gauge();
            LabelGtitle.Text = gbll.GetGtitle(Int32.Parse(Mgid));
        }
    }

    protected void Btnadd_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Gid"] != null)
        {
            string Mgid = Request.QueryString["Gid"].ToString();
            string Mitem = TextBoxMitem.Text.Trim();
            if (!string.IsNullOrEmpty(Mitem))
            {
                LearnSite.Model.GaugeItem mmodel = new LearnSite.Model.GaugeItem();
                mmodel.Mgid = Int32.Parse(Mgid);
                mmodel.Mitem = Mitem;
                mmodel.Mscore = Int32.Parse(DDLscore.SelectedValue);
                mmodel.Msort = Int32.Parse(DDLsort.SelectedValue);
                LearnSite.BLL.GaugeItem mbll = new LearnSite.BLL.GaugeItem();
                mbll.Add(mmodel);
                System.Threading.Thread.Sleep(200);
                showGaugeItem();
                TextBoxMitem.Text = "";
            }
            else
            {
                LearnSite.Common.WordProcess.Alert("请输入评价描述！", this.Page);
            }
        }
    }
    protected void GVGaugeItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            string Mid = e.CommandArgument.ToString();
            LearnSite.BLL.GaugeItem mbll = new LearnSite.BLL.GaugeItem();
            mbll.Delete(Int32.Parse(Mid));
            System.Threading.Thread.Sleep(200);
            showGaugeItem();
        }
    }
    protected void GVGaugeItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string strjs = "if(confirm('您确定要删除该评价描述吗?'))return true;else return false; ";
            ((LinkButton)e.Row.FindControl("BtnDel")).OnClientClick = strjs;
        }
    }
    protected void Btnreturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Teacher/gauge.aspx");
    }
    protected void GVGaugeItem_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GVGaugeItem.EditIndex = e.NewEditIndex;
        showGaugeItem();
    }
    protected void GVGaugeItem_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GVGaugeItem.EditIndex = -1;
        showGaugeItem();
    }
    protected void GVGaugeItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string mid = GVGaugeItem.DataKeys[GVGaugeItem.EditIndex][0].ToString();
        string item = ((TextBox)(GVGaugeItem.Rows[e.RowIndex].FindControl("TextBoxMitem"))).Text.Trim();
        string score = ((TextBox)(GVGaugeItem.Rows[e.RowIndex].FindControl("TextBoxMscore"))).Text.Trim();
        if (LearnSite.Common.WordProcess.IsIntNum(score))
        {
            LearnSite.Model.GaugeItem gmodel = new LearnSite.Model.GaugeItem();
            gmodel.Mid = Int32.Parse(mid);
            gmodel.Mitem = item;
            gmodel.Mscore = Int32.Parse(score);
            LearnSite.BLL.GaugeItem gbll = new LearnSite.BLL.GaugeItem();
            if (gbll.UpdateMitem(gmodel))//更新修改
            {
                GVGaugeItem.EditIndex = -1;
                showGaugeItem();
            }
        }
        else
        {
            LearnSite.Common.WordProcess.Alert("分值请输入数字！", this.Page);
        }
    }
}
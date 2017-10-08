using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_txtformresult : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            if (Request.QueryString["Mid"] != null)
            {
                if (!IsPostBack)
                {
                    ShowMtitle();
                    Showtxtform();
                }
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }
    /// <summary>
    /// 匹配为假
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    protected string UnEdit(string str)
    {
        string oldstr = "contenteditable=\"true\"";
        string newstr = "contenteditable=\"false\"";
        return str.Replace(oldstr, newstr);    
    }
    private void ShowMtitle()
    {
        string Sgrade = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString();
        string Sclass = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString();

        string mid = Request.QueryString["Mid"].ToString();
        LearnSite.BLL.TxtForm tbll = new LearnSite.BLL.TxtForm();
        string mtitle = tbll.GetMtitle(Int32.Parse(mid));
        LbMtitle.Text = mtitle + "—" + Sgrade + "年级" + Sclass + "班" + "填写列表";
    }
    private void Showtxtform()
    {
        string Syear = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Syear"].ToString();
        string Sgrade = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString();
        string Sclass = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString();
        string Sid = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString();
        string mid = Request.QueryString["Mid"].ToString();
        LearnSite.BLL.TxtFormBack kbll = new LearnSite.BLL.TxtFormBack();
        if (kbll.GetRid(Sid, mid) > 0)
        {
            GVtxtform.DataSource = kbll.GetListclass(Sgrade, Sclass, mid);
            GVtxtform.DataBind();
            Labelreplycount.Text = GVtxtform.Rows.Count.ToString();
            Labelnostu.Text = kbll.GetUndoStus(Int32.Parse(Sgrade), Int32.Parse(Sclass), Int32.Parse(mid));
        }
        else
        {
            Labelnostu.Text = "您未填写表单，暂时无法查看！";
        }
    }

    protected void ImageBtnFresh_Click(object sender, ImageClickEventArgs e)
    {
        Showtxtform();
    }
    protected void GVtxtform_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            Label lb = (Label)e.Row.FindControl("Labelfloor");
            string mynum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
            ImageButton imbtngood = (ImageButton)e.Row.FindControl("ImageButtonGood");
            ImageButton imbtnless = (ImageButton)e.Row.FindControl("ImageButtonless");
            Label lbdate = (Label)e.Row.FindControl("Labeldate");
            Label lbsnum = (Label)e.Row.FindControl("Labelsnum");
            lb.Text = Convert.ToString(e.Row.RowIndex + 1);
            if (mynum.IndexOf("s") > -1)
            {
                imbtngood.Visible = true;
                imbtnless.Visible = true;
                ImageBtngoodall.Visible = true;
                imbtngood.ToolTip = "加分！";

            }
            else
            {
                imbtngood.Visible = false;
                imbtnless.Visible = false;
                ImageBtngoodall.Visible = false;
                DateTime today = DateTime.Now;
                DateTime replaydate = DateTime.Parse(lbdate.Text);
                ImageButton imgbtnagree = (ImageButton)e.Row.FindControl("ImageButtonAgree");
                if (LearnSite.Common.Computer.Daygone(today, replaydate) > 5)
                    imgbtnagree.Visible = false;
                if (lbsnum.Text == mynum)
                    imgbtnagree.Visible = false;
            }
        }
    }
    protected void GVtxtform_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string eid = e.CommandArgument.ToString();
        LearnSite.BLL.TxtFormBack kbll = new LearnSite.BLL.TxtFormBack();
        if (e.CommandName == "Good")
        {
            int index = Convert.ToInt32(eid);
            kbll.UpdateScore(index, 2);
            System.Threading.Thread.Sleep(500);
            Showtxtform();
        }
        if (e.CommandName == "Less")
        {
            int index = Convert.ToInt32(eid);
            kbll.UpdateScore(index, -2);
            System.Threading.Thread.Sleep(500);
            Showtxtform();
        }

        if (e.CommandName == "Agree")
        {
            int index = Convert.ToInt32(eid);
            string sid = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString();
            if (Session["txtform" + sid] != null)
            {
                int agcount = Int32.Parse(Session["txtform" + sid].ToString());
                if (agcount < 10)
                {
                    if (Session["txtform" + sid + "Agree" + index.ToString()] == null)
                    {
                        Session["txtform" + sid + "Agree" + index.ToString()] = "T";
                        Session["txtform" + sid] = agcount + 1;
                        kbll.UpdateAgree(index);
                        System.Threading.Thread.Sleep(500);
                        Showtxtform();
                    }
                    else
                    {
                        LearnSite.Common.WordProcess.Alert("您已经点赞过了！", this.Page);
                    }
                }
                else
                {
                    LearnSite.Common.WordProcess.Alert("最多点赞9次！", this.Page);
                }
            }
            else
            {
                Session["txtform" + sid] = 1;
                kbll.UpdateAgree(index);
                System.Threading.Thread.Sleep(500);
                Showtxtform();
            }
        }
    }
    protected void ImageBtngoodall_Click(object sender, ImageClickEventArgs e)
    {
        string Syear = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Syear"].ToString();
        string Sgrade = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString();
        string Sclass = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString();
        string mid = Request.QueryString["Mid"].ToString();
        LearnSite.BLL.TxtFormBack kbll = new LearnSite.BLL.TxtFormBack();
        kbll.UpdateAllScore(Syear, Sgrade, Sclass, mid, 6);
        System.Threading.Thread.Sleep(500);
        Showtxtform();

    }
}
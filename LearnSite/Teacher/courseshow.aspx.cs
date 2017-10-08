using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_courseshow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "学案显示页面";
            showcourse();
            showmenu();
            if (Request.QueryString["Cold"] != null)
            {
                BtnEdit.Enabled = false;
                LinkBtnAdd.Enabled = false;
                LinkBtnAddSurvey.Enabled = false;
                LinkBtnAddTopic.Enabled = false;
                LinkBtnAddTxtForm.Enabled = false;
            }
        }
    }

    private void showcourse()
    {
        if (Request.QueryString["Cid"] != null)
        {
            string Cid = Request.QueryString["Cid"].ToString();
            LearnSite.Model.Courses model = new LearnSite.Model.Courses();
            LearnSite.BLL.Courses cs = new LearnSite.BLL.Courses();
            model = cs.GetModel(Int32.Parse(Cid));
            if (model != null)
            {
                LabelCtitle.Text = model.Ctitle;
                LabelCdate.Text = model.Cdate.ToString();
                LabelCclass.Text = model.Cclass;
                LabelCobj.Text = model.Cobj.ToString();
                LabelCterm.Text = model.Cterm.ToString();
                LabelCks.Text = model.Cks.ToString();
                Ccontent.InnerHtml = HttpUtility.HtmlDecode(model.Ccontent);
            }
        }
    }
    private void showmenu()
    {
        if (Request.QueryString["Cid"] != null)
        {
            string Cid = Request.QueryString["Cid"].ToString();
            LearnSite.BLL.ListMenu lbll = new LearnSite.BLL.ListMenu();
            GVlistmenu.DataSource = lbll.GetMenu(Int32.Parse(Cid));
            GVlistmenu.DataBind();
        }
    }
    protected void LinkBtnAdd_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Cid"] != null)
        {
            string Mcid = Request.QueryString["Cid"].ToString();
            string url = "~/Teacher/missionadd.aspx?Mcid=" + Mcid;
            Response.Redirect(url, false);
        }
    }
    protected void LinkBtnReturn_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Cold"] != null)
        {
            Response.Redirect("~/Teacher/courseold.aspx", false);
        }
        else
        {
            Response.Redirect("~/Teacher/course.aspx", false);
        }
    }
    protected void LinkBtnAddTopic_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Cid"] != null)
        {
            string Mcid = Request.QueryString["Cid"].ToString();
            string url = "~/Teacher/topicadd.aspx?Mcid=" + Mcid;
            Response.Redirect(url, true);
        }
    }
    protected void LinkBtnAddSurvey_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Cid"] != null)
        {
            string Vcid = Request.QueryString["Cid"].ToString();
            string url = "~/Survey/surveyadd.aspx?Cid=" + Vcid;
            Response.Redirect(url, true);
        }
    }
    protected void LinkBtnAddTxtForm_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Cid"] != null)
        {
            string Vcid = Request.QueryString["Cid"].ToString();
            string url = "~/Teacher/txtformadd.aspx?Mcid=" + Vcid;
            Response.Redirect(url, true);
        }
    }
    protected void LinkBtnProgram_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Cid"] != null)
        {
            string Vcid = Request.QueryString["Cid"].ToString();
            string url = "~/Teacher/programadd.aspx?Mcid=" + Vcid;
            Response.Redirect(url, true);
        }
    }

    protected void BtnEdit_Click(object sender, ImageClickEventArgs e)
    {
        if (Request.QueryString["Cid"] != null)
        {
            string Cid = Request.QueryString["Cid"].ToString();
            string url = "~/Teacher/courseedit.aspx?Cid=" + Cid;
            Response.Redirect(url, true);
        }
    }
    protected void GVlistmenu_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int RowIndex = Convert.ToInt32(e.CommandArgument);
        int Lid = Convert.ToInt32(((Label)GVlistmenu.Rows[RowIndex].FindControl("LabelLid")).Text);
        int lxid = Convert.ToInt32(((Label)GVlistmenu.Rows[RowIndex].FindControl("LabelLxid")).Text);
        string ltype = ((Label)GVlistmenu.Rows[RowIndex].FindControl("LabelLtype")).Text;
        //int lsort = Convert.ToInt32(((Label)GVlistmenu.Rows[RowIndex].FindControl("LabelLsort")).Text);        
        int Lcid = Int32.Parse(Request.QueryString["Cid"].ToString());
        LearnSite.BLL.ListMenu lbll = new LearnSite.BLL.ListMenu();
        if (e.CommandName == "P")
        {
            switch (ltype)
            {
                case "1"://活动
                case "5"://编程
                case "6"://描述
                    LearnSite.BLL.Mission mbll = new LearnSite.BLL.Mission();
                    mbll.UpdateMpublish(lxid);
                    lbll.UpdateLshow(Lid);
                    break;
                case "2"://调查
                    LearnSite.BLL.Survey vbll = new LearnSite.BLL.Survey();
                    vbll.UpdateVclose(lxid);
                    lbll.UpdateLshow(Lid);
                    break;
                case "3"://讨论
                    LearnSite.BLL.TopicDiscuss tbll = new LearnSite.BLL.TopicDiscuss();
                    tbll.UpdateTclose(lxid);
                    lbll.UpdateLshow(Lid);
                    break;
                case "4"://表单
                    LearnSite.BLL.TxtForm tbmbll = new LearnSite.BLL.TxtForm();
                    tbmbll.UpdateMpublish(lxid);
                    lbll.UpdateLshow(Lid);
                    break;
            }
        }
        if (e.CommandName == "D")
        {
            switch (ltype)
            {
                case "1"://活动
                case "5"://编程
                case "6"://描述
                    LearnSite.BLL.Mission mbll = new LearnSite.BLL.Mission();
                    mbll.DeleteMission(lxid);//假删除任务
                    lbll.Delete(Lid);//删除导航
                    break;
                case "2"://调查
                    LearnSite.BLL.Survey vbll = new LearnSite.BLL.Survey();
                    LearnSite.BLL.SurveyQuestion qbll = new LearnSite.BLL.SurveyQuestion();
                    if (!qbll.ExistsByQvid(lxid))
                    {
                        vbll.Delete(lxid);//删除调查
                        lbll.Delete(Lid);//删除导航
                    }
                    else
                    {
                        string msg = "该调查卷存在试题，请先删除试题！";
                        LearnSite.Common.WordProcess.Alert(msg, this.Page);
                    }
                    break;
                case "3"://讨论
                    LearnSite.BLL.TopicDiscuss tbll = new LearnSite.BLL.TopicDiscuss();
                    tbll.Delete(lxid);//删除讨论
                    lbll.Delete(Lid);//删除导航
                    break;

                case "4"://表单
                    LearnSite.BLL.TxtForm tfmbll = new LearnSite.BLL.TxtForm();
                    tfmbll.Delete(lxid);//删除表单
                    lbll.Delete(Lid);//删除导航
                    break;
            }
        }

        if (e.CommandName == "Top")
        {
            if (RowIndex == 0)
            {
                lbll.Lsortnew(Lcid);//如果首行，初始化序号
            }
            if (RowIndex > 0)
            {
                int toplid = Convert.ToInt32(((Label)GVlistmenu.Rows[RowIndex - 1].FindControl("LabelLid")).Text);//获取上个导航编号
                lbll.UpdateLsort(Lid, false);//当前导航减１向上
                lbll.UpdateLsort(toplid, true);//上个导航增１向下
            }
            System.Threading.Thread.Sleep(500);
            lbll.UpdateMissonListMene(Lcid, lxid);//活动序号同步
        }
        if (e.CommandName == "Bottom")
        {
            int rowscount = GVlistmenu.Rows.Count;
            if (RowIndex < rowscount - 1)
            {
                int bottomlid = Convert.ToInt32(((Label)GVlistmenu.Rows[RowIndex + 1].FindControl("LabelLid")).Text);//获取下个导航编号
                lbll.UpdateLsort(bottomlid, false);//下个导航减１向上
                lbll.UpdateLsort(Lid, true);//当前导航增１向下
                System.Threading.Thread.Sleep(500);
                lbll.UpdateMissonListMene(Lcid, lxid);//活动序号同步
            }
        }

        System.Threading.Thread.Sleep(200);
        showmenu();
    }
    protected void GVlistmenu_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            HyperLink hl = (HyperLink)e.Row.FindControl("HlLtitle");
            string lxid = ((Label)e.Row.FindControl("LabelLxid")).Text;
            string ltype = ((Label)e.Row.FindControl("LabelLtype")).Text;
            string lid = ((Label)e.Row.FindControl("LabelLid")).Text;
            string Cid = Request.QueryString["Cid"].ToString();
            string Cold = "";
            if (Request.QueryString["Cold"] != null)
            {
                Cold = "&Cold=T";
            }
            switch (ltype)
            {
                case "1":                    
                    ((Label)e.Row.FindControl("Label4")).Text = "练习";
                    hl.NavigateUrl = "MissionShow.aspx?Mcid=" + Cid + "&Mid=" + lxid + "&Lid=" + lid + Cold;
                    break;
                case "6"://描述
                    ((Label)e.Row.FindControl("Label4")).Text = "阅读";
                    hl.NavigateUrl = "MissionShow.aspx?Mcid=" + Cid + "&Mid=" + lxid + "&Lid=" + lid + Cold;
                    break;
                case "2":
                    ((Label)e.Row.FindControl("Label4")).Text = "调查";
                    hl.NavigateUrl = "~/Survey/survey.aspx?Cid=" + Cid + "&Vid=" + lxid + "&Lid=" + lid + Cold;
                    break;
                case "3":
                    ((Label)e.Row.FindControl("Label4")).Text = "讨论";
                    hl.NavigateUrl = "topicshow.aspx?Tcid=" + Cid + "&Tid=" + lxid + "&Lid=" + lid + Cold;
                    break;
                case "4":
                    ((Label)e.Row.FindControl("Label4")).Text = "表单";
                    hl.NavigateUrl = "txtformshow.aspx?Mcid=" + Cid + "&Mid=" + lxid + "&Lid=" + lid + Cold;
                    break;
                case "5"://编程                    
                    ((Label)e.Row.FindControl("Label4")).Text = "编程";
                    hl.NavigateUrl = "programshow.aspx?Mcid=" + Cid + "&Mid=" + lxid + "&Lid=" + lid + Cold;
                    break;
            }

            string strjs = "if(confirm('您确定要删除吗?'))return true;else return false; ";
            ((LinkButton)e.Row.FindControl("LinkBtnDel")).OnClientClick = strjs;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //当鼠标放上去的时候 先保存当前行的背景颜色 并给附一颜色 
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E1E8E1',this.style.fontWeight='';");
            //当鼠标离开的时候 将背景颜色还原的以前的颜色 
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            //单击行改变行背景颜色 
            e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#D8E0D8'; this.style.color='buttontext';this.style.cursor='default';");
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        showmenu();
    }
}

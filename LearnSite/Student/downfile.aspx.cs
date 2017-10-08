using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_downfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            LearnSite.Common.CookieHelp.KickStudent();
            if (!IsPostBack)
            {
                ShowFile();
                ShowList();
                ShowTime();
                ShowUpload();
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }

    protected string strcut(string  str)
    {
        if (str.Length > 20)
            return LearnSite.Common.WordProcess.CnCutString(str,20,"...");
        else
            return str;
    }
    private void ShowUpload()
    {
        if (Request.QueryString["Fid"] != null)
        {
            string ch = Labelclass.Text;
            switch (ch)
            {
                case "微课":
                case "教程":
                    {
                        Panelswfupload.Visible = true;
                        string Fid = Request.QueryString["Fid"].ToString();
                        string Sid = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString();
                        LearnSite.BLL.Autonomic abll = new LearnSite.BLL.Autonomic();
                        LearnSite.Model.Autonomic amodel = new LearnSite.Model.Autonomic();
                        amodel = abll.GetModel(Int32.Parse(Sid), Int32.Parse(Fid));
                        if (amodel != null)
                        {
                            if (amodel.Acheck)
                                Panelswfupload.Visible = false;
                            else
                                Panelswfupload.Visible = true;

                            upFileUrl.Visible = true;
                            upFileType.Visible = true;
                            upFileType.ImageUrl = "~/Images/FileType/" + amodel.Atype.ToLower() + ".gif";
                            upFileUrl.Text = Server.UrlDecode(amodel.Afilename);
                            upFileUrl.NavigateUrl = "~/Plugins/download.aspx?Id=" + LearnSite.Common.EnDeCode.Encrypt(amodel.Aurl,"ls");
                        }
                        break;
                    }
                default:
                    Panelswfupload.Visible = false;
                    break;
            }
        }
    }
    private void ShowTime()
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            string ch = Labelclass.Text;
            switch (ch)
            {
                case "微课":
                case "教程":
                case "资料":
                    {
                        LBtnfile.Visible = true;
                        break;
                    }
                case "软件":
                    {
                        LearnSite.BLL.Soft st = new LearnSite.BLL.Soft();
                        if (st.IsDownCan())
                        {
                            LBtnfile.Visible = true;
                            Labelmsg.Text = "";
                        }
                        else
                        {
                            Labelcontent.Text = "<br/><br/><br/><div style='text-align: center'>隐藏内容</div><br/><br/><br/>";
                            Labelmsg.Text = "登录" + LearnSite.Common.XmlHelp.GetDowntime().ToString() + "分钟后可下载";
                            LBtnfile.Visible = false;
                        }
                        break;
                    }
                case "游戏":
                    {
                        string Wnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
                        LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
                        int todayScore = wbll.GetTodayWorkScores(Wnum);
                        if (todayScore > Int32.Parse(Labelopen.Text) - 1)
                        {
                            LBtnfile.Visible = true;
                            Labelmsg.Text = "你的作品平均得"+todayScore.ToString()+"分";
                        }
                        else
                        {
                            Labelcontent.Text = "<br/><br/><br/><div style='text-align: center'>隐藏内容</div><br/><br/><br/>";
                            Labelmsg.Text = "今天作品赚取"+todayScore.ToString()+"学分不够使用！";
                            LBtnfile.Visible = false;
                        }
                        break;
                    }
            }
            if (HLurl.NavigateUrl == "")
            {
                LBtnfile.Visible = false;
            }
        }
    }
    private void ShowFile()
    {
        if (Request.QueryString["Fid"] != null)
        {
            string Fidstr = Request.QueryString["Fid"].ToString();
            if (LearnSite.Common.WordProcess.IsNum(Fidstr))
            {
                LabelFid.Text = Fidstr;
                LabelSid.Text = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString();
                int Fid = Int32.Parse(Fidstr);
                LearnSite.Model.Soft smodel = new LearnSite.Model.Soft();
                LearnSite.BLL.Soft st = new LearnSite.BLL.Soft();
                smodel = st.GetModel(Fid);
                Labeltitle.Text = smodel.Ftitle;
                Labelhit.Text = smodel.Fhit.ToString();
                Labeldate.Text = smodel.Fdate.ToString();
                Labelfiletype.Text = smodel.Ffiletype;
                string typestr = Labelfiletype.Text;
                if (typestr == "")
                {
                    typestr = "read";
                    ImageDown.Visible = false;
                }
                ImageType.ImageUrl = "~/Images/FileType/" + typestr.ToLower() + ".gif";
                Labelclass.Text = smodel.Fclass;
                HLurl.NavigateUrl = smodel.Furl;
                Labelopen.Text = smodel.Fopen.ToString();
                Labelcontent.Text = HttpUtility.HtmlDecode(smodel.Fcontent);
                LabelFyid.Text= smodel.Fyid.ToString();
                LBtnfile.Visible = false;
                st.UpdateFhit(Fid);
            }
        }
    }
    private void ShowList()
    {
        string Rhid = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Rhid"].ToString(); 
        LearnSite.BLL.Soft st = new LearnSite.BLL.Soft();
        string fyid = LabelFyid.Text;
        if (!string.IsNullOrEmpty(fyid))
        {
            int yid = Int32.Parse(fyid);
            GVSoft.DataSource = st.GetShowSoftList(Rhid, yid);
            GVSoft.DataBind();
            LearnSite.BLL.SoftCategory ybll = new LearnSite.BLL.SoftCategory();
            GVSoft.HeaderRow.Cells[0].Text = ybll.GetTitle(yid);
        }
    }
    protected void LBtnfile_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Fid"] != null && HLurl.NavigateUrl != "")
        {
            LearnSite.Common.FileDown.DownLoadOut(HLurl.NavigateUrl);
        }
    }
    protected void GVSoft_RowDataBound(object sender, GridViewRowEventArgs e)
    {
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
    protected void GVSoft_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView theGrid = sender as GridView;  // refer to the GridView
        int newPageIndex = 0;

        if (-2 == e.NewPageIndex)
        { // when click the "GO" Button
            TextBox txtNewPageIndex = null;

            GridViewRow pagerRow = theGrid.BottomPagerRow;

            if (null != pagerRow)
            {
                txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;   // refer to the TextBox with the NewPageIndex value
            }

            if (null != txtNewPageIndex)
            {

                newPageIndex = int.Parse(txtNewPageIndex.Text) - 1; // get the NewPageIndex
            }
        }
        else
        {  // when click the first, last, previous and next Button
            newPageIndex = e.NewPageIndex;
        }

        // check to prevent form the NewPageIndex out of the range
        newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
        newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;
        theGrid.PageIndex = newPageIndex;
        ShowList();
    }
}

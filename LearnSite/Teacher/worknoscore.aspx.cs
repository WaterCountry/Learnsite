using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Teacher_worknoscore : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        ImageBtnDel.Attributes["OnClick"] = "return confirm('您确定要删除该作品吗？');";
        if (!IsPostBack)
        {
            if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
            {
                if (Request.QueryString["Cid"] != null)
                {
                    int Wcid = Int32.Parse(Request.QueryString["Cid"].ToString());
                    LearnSite.BLL.Courses cbll = new LearnSite.BLL.Courses();
                    string ctitle = cbll.GetTitle(Wcid);
                    LabeCtitle.Text = "《" + ctitle + "》";
                    Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "《" + ctitle + "》未评作品列表页面";
                    ShowClass();
                    Readwork();
                    showflash();
                }
                else
                {
                    Response.Redirect("~/Teacher/works.aspx", false);
                }
            }
        }
    }
    protected void Btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Teacher/works.aspx", false);
    }

    private void ShowClass()
    {
        int Wcid = Int32.Parse(Request.QueryString["Cid"].ToString());
        LearnSite.BLL.Works ws = new LearnSite.BLL.Works();
        ListItem lm = new ListItem();
        lm.Text = "全部";
        lm.Value = "-1";
        DDLclass.Items.Add(lm);
        DataTable dt = ws.GetListNoWcheckClass(Wcid);
        int dcount = dt.Rows.Count;
        if (dcount > 0)
        {
            for (int i = 0; i < dcount; i++)
            {
                string dclass = dt.Rows[i][0].ToString();
                ListItem lsm = new ListItem();
                lsm.Text = dclass;
                lsm.Value = dclass;
                DDLclass.Items.Add(lsm);
            }
        }
        dt.Dispose();
        DDLclass.SelectedValue = "-1";
    }
    private void Readwork()
    {
        int Wcid = Int32.Parse(Request.QueryString["Cid"].ToString());
        int Wclass = Int32.Parse(DDLclass.SelectedValue);
        LearnSite.BLL.Works ws = new LearnSite.BLL.Works();
        DDLstore.DataSource = ws.GetListNoWcheckWork(Wcid, Wclass);
        DDLstore.DataTextField = "Sname";
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
    private void showflash()
    {
        RBLselect.ClearSelection();
        int icn = DDLstore.Items.Count;
        if (icn > 0)
        {
            bool ishtm = false;
            string Wurl = DDLstore.SelectedValue;
            string ext = LearnSite.Common.WordProcess.getext(Wurl);
            string htmname = "";
            if (string.IsNullOrEmpty(ext))
            {
                ext = "htm";
                ishtm = true;
                DDLhtmlname.Visible = true;
                htmname = DDLhtmlname.SelectedValue;
            }
            else
            {
                DDLhtmlname.Visible = false;
            }
            int cur = DDLstore.SelectedIndex + 1;
            Labelnum.Text = cur.ToString() + "/" + icn.ToString();
            if (ishtm)
            {
                string mynum = LearnSite.Common.WordProcess.GetSnumhtm(Wurl);
                Labelnum.ToolTip = mynum;
                int Wcid = Int32.Parse(Request.QueryString["Cid"].ToString());
                LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
                LabelMid.Text = wbll.GetHtmMid(Wcid, mynum);
            }
            else
            {
                Labelnum.ToolTip = LearnSite.Common.WordProcess.GetSnum(Wurl);
                LabelMid.Text = LearnSite.Common.WordProcess.GetMid(Wurl);
            }
            GetScore(LabelMid.Text, Labelnum.ToolTip);
            lbcount.Text = cur.ToString() + "/" + icn.ToString();
            Literal1.Text = LearnSite.Common.WordProcess.SelectWriteTeaNew(ext, Wurl, true, htmname);
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
    private void GetScore(string Wmid, string mySnum)
    {
        if (!string.IsNullOrEmpty(Wmid))
        {
            string Wcid = Request.QueryString["Cid"].ToString();
            LearnSite.BLL.Mission mbll = new LearnSite.BLL.Mission();
            LabelMtitle.Text = mbll.GetMissionTitle(Int32.Parse(Wmid));

            LearnSite.BLL.Works ws = new LearnSite.BLL.Works();

            string[] myscoreself = ws.GetmyScoreWself(Wcid, Wmid, mySnum);

            string myscore = myscoreself[0].ToString();
            TextBoxWself.Text = myscoreself[1].ToString();
            TextBoxWdsocre.Text = myscoreself[2].ToString();
            if (myscore != "")
            {
                int ascore = Int32.Parse(myscore);
                switch (ascore)
                {
                    case 12:
                        RBLselect.SelectedValue = "G";
                        break;
                    case 10:
                        RBLselect.SelectedValue = "A";
                        break;
                    case 8:
                        RBLselect.SelectedValue = "B";
                        break;
                    case 6:
                        RBLselect.SelectedValue = "C";
                        break;
                    case 4:
                        RBLselect.SelectedValue = "D";
                        break;
                    case 2:
                        RBLselect.SelectedValue = "E";
                        break;
                    case 0:
                        RBLselect.SelectedValue = "O";
                        break;
                }
            }
        }
    }
    protected void RBLselect_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Wcid = Request.QueryString["Cid"].ToString();
        string Wmid = LabelMid.Text;
        string Wnum = Labelnum.ToolTip;
        if (Wnum != "")
        {
            string selectStr = RBLselect.SelectedValue;
            int myscore = 0;
            switch (selectStr)
            {
                case "G":
                    myscore = 12;
                    break;
                case "A":
                    myscore = 10;
                    break;
                case "B":
                    myscore = 8;
                    break;
                case "C":
                    myscore = 6;
                    break;
                case "D":
                    myscore = 4;
                    break;
                case "E":
                    myscore = 2;
                    break;
                case "O":
                    myscore = 0;
                    break;
            }
            string wself = HttpUtility.HtmlEncode(TextBoxWself.Text.Trim());
            LearnSite.BLL.Works ws = new LearnSite.BLL.Works();
            if (wself == "")
            {
                switch (myscore)
                {
                    case 12:
                        wself = "你的作品已进入收藏榜";
                        break;
                    case 10:
                        wself = "你的作品很优秀";
                        break;
                    case 8:
                        wself = "你的作品良好";
                        break;
                    case 6:
                        wself = "你的作品一般";
                        break;
                    case 4:
                        wself = "你的作品有待改进";
                        break;
                    case 2:
                        wself = "你的作品不完整";
                        break;
                }
            }
            string wdscorestr = TextBoxWdsocre.Text;
            int wdscore = 0;
            if (LearnSite.Common.WordProcess.IsNum(wdscorestr))
                wdscore = Int32.Parse(wdscorestr);

            ws.Updatemscoreself(Wcid, Wmid, Wnum, myscore, wself, wdscore);//打分并评语
            if (DDLstore.Items.Count > 0)
            {
                int sindex = DDLstore.SelectedIndex;
                if (sindex < DDLstore.Items.Count - 1)
                {
                    int curindex = sindex + 1;
                    DDLstore.SelectedIndex = curindex;//保存当前索引位置
                    lbcurindex.Text = curindex.ToString();
                    showflash();
                }
            }
        }
    }
    protected void ImgBtn_Click(object sender, ImageClickEventArgs e)
    {
        lbcount.Text = "0";
        int curindex = DDLstore.SelectedIndex;//保存当前索引位置
        Readwork();
        if (DDLstore.Items.Count > curindex)
            DDLstore.SelectedIndex = curindex;
        showflash();
    }
    protected void DDLclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbcount.Text = "0";
        Readwork();
        showflash();
    }
    protected void ImageBtnDel_Click(object sender, ImageClickEventArgs e)
    {
        string Wnum = Labelnum.ToolTip;
        string Wmid = LabelMid.Text;
        if (Wnum != "" && Wmid != "")
        {
            LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
            wbll.Delmywork(Int32.Parse(Wmid), Wnum);
            System.Threading.Thread.Sleep(200);//延时
            Readwork();
            showflash();
        }
    }
}
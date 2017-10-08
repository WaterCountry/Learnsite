using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Teacher_circleshow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        Btnflash.Attributes.Add("onclick", "this.form.target='_self'");
        ImageBtnDel.Attributes["OnClick"] = "return confirm('您确定要删除该作品吗？');";
        if (!IsPostBack)
        {
            ReadMtitle();
            Readwork();
            showFlashLoop();
            showflash();
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
                Labelnum.ToolTip = LearnSite.Common.WordProcess.GetSnumhtm(Wurl);
            }
            else
            {
                Labelnum.ToolTip = LearnSite.Common.WordProcess.GetSnum(Wurl);
            }
            Literal1.Text = LearnSite.Common.WordProcess.SelectWriteTeaNew(ext, Wurl, true, htmname);
            GetScore(Labelnum.ToolTip);
            if (ext == "sb" || ext == "sb2")
            {
                string Mid = Request.QueryString["Mi"].ToString();
                string Wnum = Labelnum.ToolTip;
                LearnSite.BLL.Works wbll=new LearnSite.BLL.Works();
                string wid = wbll.GetWid(Mid, Wnum);
                if (!string.IsNullOrEmpty(wid))
                {
                    Hlcode.NavigateUrl = "~/Student/codeproject.aspx?id=" + wid;
                    Hlcode.Visible = true;
                    Hlshare.NavigateUrl = "~/Student/codeshare.aspx?id=" + wid;
                    Hlshare.Visible = true;
                }
            }
        }
        else
        {
            Literal1.Text = "当前没有学生作品";
        }
    }

    private void Readwork()
    {
        if (Request.QueryString["Sg"] != null && Request.QueryString["Sc"] != null && Request.QueryString["Ci"] != null && Request.QueryString["Mi"] != null)
        {
            int Sgrade = Int32.Parse(Request.QueryString["Sg"].ToString());
            int Sclass = Int32.Parse(Request.QueryString["Sc"].ToString());
            int Cid = Int32.Parse(Request.QueryString["Ci"].ToString());
            int Mid = Int32.Parse(Request.QueryString["Mi"].ToString());
            int Wscore = 0;
            if (CkselectG.Checked)
                Wscore = 12;
            if (CheckselectA.Checked)
                Wscore = 10;
            LearnSite.BLL.Works ws = new LearnSite.BLL.Works();
            DDLstore.DataSource = ws.ShowCircleWorksSelect(Sgrade, Sclass, Cid, Mid, Wscore);
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
    }
    private void ReadMtitle()
    {
        if (Request.QueryString["Mi"] != null)
        {
            int Mid = Int32.Parse(Request.QueryString["Mi"].ToString());
            LearnSite.BLL.Mission mbll = new LearnSite.BLL.Mission();
            LabeMtitle.Text = "〖" + mbll.GetMissionTitle(Mid) + "〗";
        }
    }
    protected void Btnflash_Click(object sender, EventArgs e)
    {
        int mc = DDLstore.Items.Count;
        if (mc > 0)
        {
            int curindex = DDLstore.SelectedIndex;//保存当前索引位置
            lbcurindex.Text = curindex.ToString();
            Readwork();
            DDLstore.SelectedIndex = curindex;
            showflash();
        }
    }
    protected void Btnrestart_Click(object sender, EventArgs e)
    {
        if (DDLstore.Items.Count > 0)
        {
            lbcurindex.Text = "0";
            Readwork();
            showflash();
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
    protected void Btnstop_Click(object sender, EventArgs e)
    {
        if (Btnstop.Text == "暂停")
        {
            Btnstop.Text = "继续";
        }
        else
        {
            Btnstop.Text = "暂停";
        }
        showflash();
    }
    private void GetScore(string mySnum)
    {
        if (Request.QueryString["Mi"] != null)
        {
            int Mid = Int32.Parse(Request.QueryString["Mi"].ToString());
            LearnSite.BLL.Works ws = new LearnSite.BLL.Works();
            string[] myscoreself = ws.GetmyScoreWself(Mid, mySnum);
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
        if (Request.QueryString["Mi"] != null)
        {
            int Mid = Int32.Parse(Request.QueryString["Mi"].ToString());
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
                string teaself = TextBoxWself.Text.Trim();
                if (teaself.Length > 200)
                    teaself = teaself.Substring(0, 198);
                string wself = HttpUtility.HtmlEncode(teaself);
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
                ws.Updatemscoreself(Mid, Wnum, myscore, wself, wdscore);//打分并评语
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
    }

    protected void ImgBtn_Click(object sender, ImageClickEventArgs e)
    {
        int mc = DDLstore.Items.Count;
        if (mc > 0)
        {
            int curindex = DDLstore.SelectedIndex;//保存当前索引位置
            curindex++;
            lbcurindex.Text = curindex.ToString();
            if (curindex < mc)
            {
                DDLstore.SelectedIndex = curindex;
                showflash();
            }
            else
            {
                Readwork();
                showflash();
            }
        }
    }
    protected void CkFlash_CheckedChanged(object sender, EventArgs e)
    {
        bool isloop = CkFlash.Checked;
        string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
        Session["FlashLoop" + Hid] = isloop.ToString();
        showflash();
    }

    private void showFlashLoop()
    {
        string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
        if (Session["FlashLoop" + Hid] != null)
        {
            string flashLoop = Session["FlashLoop" + Hid].ToString();
            CkFlash.Checked = bool.Parse(flashLoop);
        }
    }
    protected void ImageBtnDel_Click(object sender, ImageClickEventArgs e)
    {
        int Mid = Int32.Parse(Request.QueryString["Mi"].ToString());
        string Wnum = Labelnum.ToolTip;
        if (Wnum != "")
        {
            LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
            wbll.Delmywork(Mid, Wnum);
            System.Threading.Thread.Sleep(200);//延时
            Readwork();
            showflash();
        }
    }
    protected void ImgBtnTextbox_Click(object sender, ImageClickEventArgs e)
    {
        string flag = ImgBtnTextbox.CommandName;
        switch (flag)
        {
            case "v":
                ImgBtnTextbox.CommandName = "h";
                TextBoxWself.Visible = false;
                break;
            default:
                ImgBtnTextbox.CommandName = "v";
                TextBoxWself.Visible = true;
                break;
        }
    }

    protected void CkselectG_CheckedChanged(object sender, EventArgs e)
    {
        lbcurindex.Text = "0";
        Readwork();
        showflash();
    }
    protected void CheckselectA_CheckedChanged(object sender, EventArgs e)
    {
        lbcurindex.Text = "0";
        Readwork();
        showflash();
    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Teacher_softnomic : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        Btnflash.Attributes.Add("onclick", "this.form.target='_self'");
        Btndel.Attributes["OnClick"] = "return confirm('您确定要删除该作品吗？');";
        if (!IsPostBack)
        {
            showSoftcategory();
            listsoft();
            Readwork();
            showFlashLoop();
            showflash();
        }
    }
    private void showSoftcategory()
    {
        LearnSite.BLL.Soft fbll = new LearnSite.BLL.Soft();
        DDLCategory.DataSource = fbll.GetListCategory();
        DDLCategory.DataTextField = "Ytitle";
        DDLCategory.DataValueField = "Yid";
        DDLCategory.DataBind();
    }
    private void listsoft()
    {
        string ayid = DDLCategory.SelectedValue;
        if (!string.IsNullOrEmpty(ayid))
        {
            LearnSite.BLL.Soft fbll = new LearnSite.BLL.Soft();
            DDLsoft.DataSource = fbll.GetListnomic(Int32.Parse(ayid));
            DDLsoft.DataTextField = "Ftitle";
            DDLsoft.DataValueField = "Fid";
            DDLsoft.DataBind();
        }
    }
    private void showflash()
    {
        RBLselect.ClearSelection();
        int icn = DDLstore.Items.Count;
        if (icn > 0)
        {
            string Wurl = DDLstore.SelectedValue;
            string ext = LearnSite.Common.WordProcess.getext(Wurl);
            string htmname = "";
            int cur = DDLstore.SelectedIndex + 1;
            Labelnum.Text = cur.ToString() + "/" + icn.ToString();
            Labelnum.ToolTip = LearnSite.Common.WordProcess.GetSnum(Wurl);

            GetScore(Labelnum.ToolTip);
            Literal1.Text = LearnSite.Common.WordProcess.SelectWriteTeaNew(ext, Wurl, true, htmname);
        }
        else
        {
            Literal1.Text = "当前没有学生作品";
        }
    }

    private void Readwork()
    {
        string afid=DDLsoft.SelectedValue;
        if (!string.IsNullOrEmpty(afid))
        {
            LearnSite.BLL.Autonomic abll = new LearnSite.BLL.Autonomic();
            DDLstore.DataSource = abll.GetListCircleNomic(Int32.Parse(afid));
            DDLstore.DataTextField = "Aname";
            DDLstore.DataValueField = "Aurl";
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
        string afid = DDLsoft.SelectedValue;
        if (!string.IsNullOrEmpty(afid))
        {
            LearnSite.BLL.Autonomic abll = new LearnSite.BLL.Autonomic();
            string[] myscoreself = abll.GetScoreSelf(Int32.Parse(afid), mySnum);
            string myscore = myscoreself[0].ToString();
            TextBoxWself.Text = myscoreself[1].ToString();
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
        string afid = DDLsoft.SelectedValue;
        string Anum = Labelnum.ToolTip;
        if (Anum != "")
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
            string aself = HttpUtility.HtmlEncode(TextBoxWself.Text.Trim());
            LearnSite.BLL.Autonomic abll = new LearnSite.BLL.Autonomic();
            if (aself == "")
            {
                switch (myscore)
                {
                    case 12:
                        aself = "你的作品已进入收藏榜";
                        break;
                    case 10:
                        aself = "你的作品很优秀";
                        break;
                    case 8:
                        aself = "你的作品良好";
                        break;
                    case 6:
                        aself = "你的作品一般";
                        break;
                    case 4:
                        aself = "你的作品有待改进";
                        break;
                    case 2:
                        aself = "你的作品不完整";
                        break;
                }
            }
            abll.UpdateScoreSelf(Int32.Parse(afid), Anum, myscore, aself);//打分并评语
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

    protected void Btndel_Click(object sender, EventArgs e)
    {
        string afid = DDLsoft.SelectedValue;
        string anum = Labelnum.ToolTip;
        if (anum != "")
        {
            LearnSite.BLL.Autonomic abll = new LearnSite.BLL.Autonomic();
            abll.Delbynum(Int32.Parse(afid), anum);
            System.Threading.Thread.Sleep(200);//延时
            Readwork();
            showflash();
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
    protected void DDLCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        listsoft();
        Readwork();
        showFlashLoop();
        showflash();
    }
    protected void DDLsoft_SelectedIndexChanged(object sender, EventArgs e)
    {
        Readwork();
        showFlashLoop();
        showflash();
    }
}
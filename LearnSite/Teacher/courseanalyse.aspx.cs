using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Teacher_courseanalyse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            ShowCourseInfo();
        }
    }

    private void ShowCourseInfo()
    {
        if (Request.QueryString["Cid"] != null)
        {
            int Cid = Int32.Parse(Request.QueryString["Cid"].ToString());
            LearnSite.Model.Courses course = new LearnSite.Model.Courses();
            LearnSite.BLL.Courses bll = new LearnSite.BLL.Courses();
            course = bll.GetModel(Cid);
            Labeltitle.Text = course.Ctitle;
            //Wid,Wurl,Wname,Wscore
            LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
            DataTable dt = wbll.GetCourseWorks(Cid);
            DataTable dtgood = dt.Clone();
            int count = dt.Rows.Count;
            int g = 0;
            int a = 0;
            int b = 0;
            int c = 0;
            int d = 0;
            int e = 0;
            int f = 0;
            decimal all = 0;
            decimal averge = 0;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    int wscore = Int32.Parse(dt.Rows[i][3].ToString());
                    all += wscore;
                    switch (wscore)
                    {
                        case 12:
                            g++;
                            dtgood.ImportRow(dt.Rows[i]);
                            break;
                        case 10:
                            a++;
                            break;
                        case 8:
                            b++;
                            break;
                        case 6:
                            c++;
                            break;
                        case 4:
                            d++;
                            break;
                        case 2:
                            e++;
                            break;
                        case 0:
                            f++;
                            break;
                    }
                }
                averge = all / count;

                DDLstore.DataSource = dtgood;
                DDLstore.DataTextField = "Wname";
                DDLstore.DataValueField = "Wurl";
                DDLstore.DataBind();
                lbcount.Text = count.ToString();
                if (DDLstore.Items.Count > 0)
                {
                    DDLstore.SelectedIndex = 0;
                    ShowFlash();
                    divview.Visible = true;
                }
                else
                {
                    Literal1.Text = "暂无评分G的收藏作品！";
                }
            }
            Labeldistribution.Text = "作品总数：" + count + " 评分分布：G " + g + " . A " + a + " . B " + b + " . C " + c + " . D " + d + " . E" + e + " 作品均分：" + averge.ToString("f1");
            dt.Dispose();
        }
    }

    private void ShowFlash()
    {
        int icn = DDLstore.Items.Count;
        if (icn > 0)
        {
            string Wurl = DDLstore.SelectedValue;
            string ext = LearnSite.Common.WordProcess.getext(Wurl);
            string htmname = "";
            if (string.IsNullOrEmpty(ext))
            {
                ext = "htm";
            }
            int cur = DDLstore.SelectedIndex + 1;
            lbcount.Text = cur.ToString() + "/" + icn.ToString();
            Literal1.Text = LearnSite.Common.WordProcess.SelectWriteTeaNew(ext, Wurl, true, htmname);
        }
        else
        {
            Literal1.Text = "当前没有学生作品";
        }
    }
    protected void ImgBtnLeft_Click(object sender, ImageClickEventArgs e)
    {
        int sdx = DDLstore.SelectedIndex;
        if (sdx > 0)
        {
            DDLstore.SelectedIndex = sdx - 1;
        }
        ShowFlash();
    }
    protected void ImgBtnright_Click(object sender, ImageClickEventArgs e)
    {
        int sdx = DDLstore.SelectedIndex;
        if (sdx < DDLstore.Items.Count - 1)
        {
            DDLstore.SelectedIndex = sdx + 1;
        }
        ShowFlash();
    }
    protected void DDLstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowFlash();
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Teacher/course.aspx", false);
    }
}
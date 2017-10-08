using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Lessons_prescm : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowListMenu();
        }
    }

    private void AddLessonFirst(string CurWay, string Cid)
    {
        MenuItem mic = new MenuItem();
        mic.Text = "本课首页";
        mic.ImageUrl = "~/Images/lesson.png";
        mic.SeparatorImageUrl = "~/Images/separate.png";
        mic.NavigateUrl = "~/Lessons/precourse.aspx?Cid=" + Cid;
        Menuact.Items.Add(mic);//添加本课导学菜单
    }

    private void AddReturn()
    {
        MenuItem ms = new MenuItem();
        ms.Text = "返回";
        ms.ImageUrl = "~/Images/return.png";
        ms.NavigateUrl = "~/Lessons/mylesson.aspx";
        Menuact.Items.Add(ms);
    }

    private void ShowListMenu()
    {
        if (Request.QueryString["Cid"] != null)
        {
            string Cid = Request.QueryString["Cid"].ToString();
            string Uploadmode = LearnSite.Common.XmlHelp.GetUploadMode();
            if (LearnSite.Common.WordProcess.IsNum(Cid))
            {
                string CurWay = "";
                LearnSite.BLL.Courses cbll = new LearnSite.BLL.Courses();
                string Ctitle = cbll.GetTitle(Int32.Parse(Cid));

                AddLessonFirst(CurWay, Cid);
                LearnSite.BLL.ListMenu lbll = new LearnSite.BLL.ListMenu();
                DataTable dt = lbll.GetShowedMenu(Int32.Parse(Cid)).Tables[0];
                int dcount = dt.Rows.Count;
                if (dcount > 0)
                {
                    string myLid = "";
                    if (Request.QueryString["Lid"] != null)
                    {
                        myLid = Request.QueryString["Lid"].ToString();
                    }
                    for (int i = 0; i < dcount; i++)
                    {
                        string Lid = dt.Rows[i]["Lid"].ToString();
                        string Lsort = dt.Rows[i]["Lsort"].ToString();
                        string Ltype = dt.Rows[i]["Ltype"].ToString();
                        string Lxidstr = dt.Rows[i]["Lxid"].ToString();
                        string Ltitlestr = dt.Rows[i]["Ltitle"].ToString();
                        MenuItem ma = new MenuItem();
                        ma.Text = Ltitlestr;
                        ma.SeparatorImageUrl = "~/Images/separate.png";
                        switch (Ltype)
                        {
                            case "1"://活动
                                ma.ImageUrl = "~/Images/mission.png";
                                ma.NavigateUrl = "~/Lessons/premission.aspx?Cid=" + Cid + "&Mid=" + Lxidstr + "&Lid=" + Lid + "&Lsort=" + Lsort;
                                break;
                            case "2"://调查
                                ma.ImageUrl = "~/Images/survey.png";
                                ma.NavigateUrl = "~/Lessons/presurvey.aspx?Cid=" + Cid + "&Vid=" + Lxidstr + "&Lid=" + Lid + "&Lsort=" + Lsort;
                                break;
                            case "3"://讨论
                                ma.ImageUrl = "~/Images/topic.png";
                                ma.NavigateUrl = "~/Lessons/pretopicdiscuss.aspx?Cid=" + Cid + "&Tid=" + Lxidstr + "&Lid=" + Lid + "&Lsort=" + Lsort;
                                break;
                        }
                        if (myLid == Lid)
                        {
                            CurWay = Ltitlestr;
                            ma.Selected = true;
                        }
                        Menuact.Items.Add(ma);//添加活动菜单
                    }
                }
                dt.Dispose();
                AddReturn();
                this.Page.Title = Ctitle + "—>" + CurWay;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Student_Scm : System.Web.UI.MasterPage
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
        mic.Text = "学案首页";
        mic.ImageUrl = "~/Images/home.gif";
        mic.SeparatorImageUrl = "~/Images/separate.gif";
        mic.NavigateUrl = "~/Student/showcourse.aspx?Cid=" + Cid;
        Menuact.Items.Add(mic);//添加本课导学菜单
    }

    private void AddReturn()
    {
        MenuItem ms = new MenuItem();
        ms.Text = "返回";
        ms.ImageUrl = "~/Images/back.gif";
        ms.NavigateUrl = "~/Student/mycourse.aspx";
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
                string mUrl;
                if (Uploadmode == "0")
                    mUrl = "active";
                else
                    mUrl = "mission";

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
                        ma.SeparatorImageUrl = "~/Images/separate.gif";
                        switch (Ltype)
                        {
                            case "1"://活动
                                ma.ImageUrl = "~/Images/mission.png";
                                ma.NavigateUrl = "~/Student/show" + mUrl + ".aspx?Cid=" + Cid + "&Mid=" + Lxidstr + "&Lid=" + Lid + "&Lsort=" + Lsort;
                                break;
                            case "2"://调查
                                ma.ImageUrl = "~/Images/survey.png";
                                ma.NavigateUrl = "~/Student/mysurvey.aspx?Cid=" + Cid + "&Vid=" + Lxidstr + "&Lid=" + Lid + "&Lsort=" + Lsort;
                                break;
                            case "3"://讨论
                                ma.ImageUrl = "~/Images/topic.png";
                                ma.NavigateUrl = "~/Student/topicdiscuss.aspx?Cid=" + Cid + "&Tid=" + Lxidstr + "&Lid=" + Lid + "&Lsort=" + Lsort;
                                break;
                            case "4"://表单
                                ma.ImageUrl = "~/Images/inquiry.png";
                                ma.NavigateUrl = "~/Student/txtform.aspx?Cid=" + Cid + "&Mid=" + Lxidstr + "&Lid=" + Lid + "&Lsort=" + Lsort;
                                break;
                            case "5"://编程
                                ma.ImageUrl = "~/Images/program.png";
                                ma.NavigateUrl = "~/Student/program.aspx?Cid=" + Cid + "&Mid=" + Lxidstr + "&Lid=" + Lid + "&Lsort=" + Lsort;
                                break;
                            case "6"://描述
                                ma.ImageUrl = "~/Images/description.png";
                                ma.NavigateUrl = "~/Student/description.aspx?Cid=" + Cid + "&Mid=" + Lxidstr + "&Lid=" + Lid + "&Lsort=" + Lsort;
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
                string stuName = "";
                if (Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sname"] != null)
                    stuName = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sname"].ToString();
                this.Page.Title = HttpUtility.UrlDecode(stuName) + " " + Ctitle + "—>" + CurWay;
            }
        }
    }
}

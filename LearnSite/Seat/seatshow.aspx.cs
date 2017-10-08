using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Seat_seatshow : System.Web.UI.Page
{
    protected string firstshows = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if(!IsPostBack)
            showOld();
    }
    private void showOld()
    {
        if (Request.QueryString["Hid"] != null && Request.QueryString["Sgrade"] != null && Request.QueryString["Sclass"] != null)
        {
            string hid = Request.QueryString["Hid"].ToString();
            if (LearnSite.Common.WordProcess.IsNum(hid))
            {
                string sgrade = Request.QueryString["Sgrade"].ToString();
                string sclass = Request.QueryString["Sclass"].ToString();
                LearnSite.BLL.House bll = new LearnSite.BLL.House();
                LearnSite.Model.House model = new LearnSite.Model.House();
                model = bll.GetModel(Int32.Parse(hid));
                bool hostnameshow = false;
                if (model != null)
                {
                    LabelTitle.Text = model.Hname;
                    firstshows = model.Hseat;
                    LearnSite.BLL.Ip pbll = new LearnSite.BLL.Ip();
                    StoreMsg.InnerText = pbll.GetSiginStudentStr(Int32.Parse(sgrade), Int32.Parse(sclass), Int32.Parse(hid), hostnameshow);
                    if (firstshows.IndexOf('-') > -1)
                    {
                        string[] old_collects = firstshows.Split('-');
                        if (old_collects.Length > 3)
                        {
                            string slnum = old_collects[0];
                            string sallnum = old_collects[1];
                            string ssortway = old_collects[2];
                            myhouse.Text = viewseats(Int32.Parse(slnum), Int32.Parse(sallnum), ssortway);
                        }
                    }
                }
            }
            else
            {
                LearnSite.Common.WordProcess.Alert("参数格式错误", this.Page);
            }
        }
    }
    

    private string viewseats(int lnum, int allnum, string sort)
    {
        string context = "";
        int hnum = allnum / lnum;//估算每行电脑数

        if (hnum == 0)
            lnum = 1;
        int cmp = 0;
        for (int i = 0; i < lnum; i++)
        {
            string hdiv = "<div id='computer-place" + i.ToString() + "' class='computer-place'>\r\n";
            context = context + hdiv;
            for (int j = 0; j < hnum; j++)
            {
                cmp++;
                if (cmp > allnum)
                {
                    break;
                }
                else
                {
                    int cname = 888;
                    if (sort == "0")
                        cname = i * hnum + j + 1;//第几列*行数+当前行号；按纵向次序编号
                    else
                    {
                        cname = j * lnum + i + 1;//按横向次序编号
                    }
                    string studiv = "<div id='" + cname + "' class='computer' title='?' tabindex='0'>" + cname + "</div>\r\n";
                    context = context + studiv;
                }
            }
            if (i == lnum - 1 && cmp < allnum) //如果是最后一列；并且摆放的电脑少于总数
            {
                int leftnum = allnum - hnum * lnum;//如果行列摆放后有多余电脑，则放在最后一列
                if (leftnum > 0)
                {
                    for (int k = 0; k < leftnum; k++)
                    {
                        cmp++;
                        string studiv = "<div id='" + cmp + "' class='computer' title='?' tabindex='0'>" + cmp + "</div>\r\n";
                        context = context + studiv;
                    }
                }
            }
            context = context + "</div>\r\n";
        }
        return context;
    }
      
    protected void Ckhostname_CheckedChanged(object sender, EventArgs e)
    {
        showOld();
    }
    protected void reflashStudent_Click(object sender, ImageClickEventArgs e)
    {
        showOld();
    }
}
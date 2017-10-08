using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Seat_computer : System.Web.UI.Page
{
    protected string firstshow = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeIsAdmin();
        if (!IsPostBack)
        {
            firstshow = getHseat();
            showOld();
        }
    }
    public string getHid()
    {
        if (Request.QueryString["Hid"] != null)
            return Request.QueryString["Hid"].ToString();
        else
            return "0";
    }
    private string getHseat()
    {
        if (Request.QueryString["Hid"] != null)
        {
            string hid = Request.QueryString["Hid"].ToString();
            LearnSite.BLL.House bll = new LearnSite.BLL.House();
            return bll.GetHseat(Int32.Parse(hid));
        }
        else
            return "";
    }

    private void showOld()
    {
        if (firstshow.Length > 10)
        {
            string[] old_collects = firstshow.Split('-');
            string slnum = old_collects[0];
            string sallnum = old_collects[1];
            string ssortway = old_collects[2];
            myhouse.Text = createseats(Int32.Parse(slnum), Int32.Parse(sallnum), ssortway);
        }
    }
    protected void Buttoninit_Click(object sender, EventArgs e)
    {
        firstshow = "";
        LearnSite.Common.Others.ClearClientPageCache();
        string sort = RadioBtnSelect.SelectedValue;
        if (LearnSite.Common.WordProcess.IsNum(ddll.SelectedValue) && LearnSite.Common.WordProcess.IsNum(TextBoxall.Text))
        {
            int lnum = Int32.Parse(ddll.SelectedValue);
            int allnum = Int32.Parse(TextBoxall.Text);
            int limitnum = 150;
            if (allnum < limitnum)
            {
                myhouse.Text = createseats(lnum, allnum, sort);
            }
            else
            {
                myhouse.Text = "电脑总数上限为" + limitnum + "台！";
            }
        }
    }

    private string createseats(int lnum, int allnum, string sort)
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
                    string studiv = "<div id='" + cname + "' class='computer'>" + cname + "</div>\r\n";
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
                        string studiv = "<div id='" + cmp + "' class='computer'>" + cmp + "</div>\r\n";
                        context = context + studiv;
                    }
                }
            }
            context = context + "</div>\r\n";
        }
        return context;
    }
}
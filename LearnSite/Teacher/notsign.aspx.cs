using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_notsign : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
            {
                if (Request.QueryString["Qname"] != null)
                {
                    Labelname.Text = Server.UrlDecode(Request.QueryString["Qname"].ToString());
                    Showdone();
                }
            }
        }
    }
    protected void Btnnotsign_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Nnum"] != null && TextBox1.Text.Trim()!="")
        {
            string Nnum = Request.QueryString["Nnum"].ToString();
            LearnSite.BLL.NotSign bll = new LearnSite.BLL.NotSign();
            if (bll.ExistsToday(Nnum))
            {
                //存在则更新
                bll.UpdateNote(Nnum, TextBox1.Text.Trim());
                Labelmsg.Text = "修改缺席备注成功！";
                System.Threading.Thread.Sleep(500);
            }
            else
            {
                //不存在则添加
                LearnSite.Model.NotSign model = new LearnSite.Model.NotSign();
                DateTime dt = DateTime.Now;
                int Nday = dt.Day;
                int Nmonth = dt.Month;
                int Nyear = dt.Year;
                model.Nnum = Nnum;
                model.Ndate = dt;
                model.Nday = Nday;
                model.Nmonth = Nmonth;
                model.Nweek = dt.DayOfWeek.ToString();
                model.Nyear = Nyear;
                model.Nnote = TextBox1.Text.Trim();
                model.Ngrade =Int32.Parse( Request.QueryString["Ngrade"].ToString());
                model.Nterm = Int32.Parse(LearnSite.Common.XmlHelp.GetTerm());
                int results=bll.Add(model);
                if ( results> 0)
                    Labelmsg.Text = "添加缺席备注成功！";
                System.Threading.Thread.Sleep(500);
            }
        }
    }

    private void Showdone()
    {
        if (Request.QueryString["Nnum"] != null)
        {
            string Nnum = Request.QueryString["Nnum"].ToString();
            LearnSite.BLL.NotSign bll = new LearnSite.BLL.NotSign();
            string Nnote=bll.GetNoteToday(Nnum);
            TextBox1.Text = Nnote;
            if (Nnote.Trim() != "")
            {
                Btnnotsign.Text = "修改";
            }
            else
            {
                Btnnotsign.Text = "添加";
            }
        }
    }
}
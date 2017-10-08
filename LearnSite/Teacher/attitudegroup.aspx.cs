using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_attitudegroup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
            {
                showAttitude();
            }
        }
    }
    protected void Btnattitude_Click(object sender, EventArgs e)
    {
        string qgroup = TextBox2.Text.Trim();
        string qgscore = DDLatt.SelectedValue;
        if (qgroup.Length > 2 && Request.QueryString["Sg"] != null && Request.QueryString["Qcid"] != null)
        {
            try
            {
                int qgscoreto = Int32.Parse(qgscore);
                string sgroup = Request.QueryString["Sg"].ToString();
                int Qcid = Int32.Parse(Request.QueryString["Qcid"].ToString());
                LearnSite.BLL.Signin gbll = new LearnSite.BLL.Signin();
                int gn = gbll.UpdateSgroup(Int32.Parse(sgroup), qgroup, qgscoreto,Qcid);
                Labelmsg.Text = "成功评价小组表现，当前小组共有" + gn + "位同学！";
            }
            catch
            {
                Labelmsg.Text = "该页面被您修改过，评分值请用数字！<br/>（采用英语输入法数字）";
            }
        }
    }
    private void showAttitude()
    {
        if (Request.QueryString["Sg"] != null && Request.QueryString["Ld"] != null && Request.QueryString["Qd"] != null)
        {
            Labelname.Text = Server.UrlDecode(Request.QueryString["Ld"].ToString());
            int Qid = Int32.Parse(Request.QueryString["Qd"].ToString());
            LearnSite.BLL.Signin sgbll = new LearnSite.BLL.Signin();
            LearnSite.Model.Signin sgmodel = new LearnSite.Model.Signin();
            sgmodel = sgbll.GetModel(Qid);
            TextBox2.Text = sgmodel.Qgroup;
            DDLatt.SelectedValue = sgmodel.Qgscore.ToString();
        }
    }

}
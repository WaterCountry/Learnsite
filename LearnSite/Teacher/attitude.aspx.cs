using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_attitude : System.Web.UI.Page
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
    private void showAttitude()
    {
        if (Request.QueryString["Qid"] != null)
        {
            if (Request.QueryString["Qname"] != null)
                Labelname.Text = Server.UrlDecode(Request.QueryString["Qname"].ToString());

            int Qid = Int32.Parse(Request.QueryString["Qid"].ToString());
            LearnSite.BLL.Signin sgbll = new LearnSite.BLL.Signin();
            LearnSite.Model.Signin sgmodel = new LearnSite.Model.Signin();
            sgmodel = sgbll.GetModel(Qid);
            int att = sgmodel.Qattitude.Value;
            if (att != 0 && att > -5 && att < 3)
            {
                RBLattitude.SelectedValue = att.ToString();
            }
            TextBox2.Text = sgmodel.Qnote;
            DDLatt.SelectedValue = att.ToString();
        }
    }
    protected void Btnattitude_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Qid"] != null && Request.QueryString["Qcid"] != null)
        {
            int Qid = Int32.Parse(Request.QueryString["Qid"].ToString());
            int Qcid = Int32.Parse(Request.QueryString["Qcid"].ToString());
            try
            {
                int Qattitude = Int32.Parse(DDLatt.SelectedValue);
                if (Qattitude != 0)
                {
                    string Qnote = "";
                    if (TextBox2.Text.Trim() == "" && RBLattitude.SelectedIndex > -1)//如果自定义评语为空，则取选项内容
                    {
                        Qnote = RBLattitude.Items[RBLattitude.SelectedIndex].Text;
                    }
                    else
                    {
                        if (RBLattitude.SelectedIndex > -1)
                        {
                            Qnote = RBLattitude.Items[RBLattitude.SelectedIndex].Text + "..." + TextBox2.Text.Trim();
                        }
                        else
                        {
                            Qnote = TextBox2.Text.Trim();
                        }
                    }
                    LearnSite.BLL.Signin sg = new LearnSite.BLL.Signin();
                    sg.UpdateAttitude(Qid, Qattitude, Qnote,Qcid);//表现评价时记录学案编号cid
                    Labelmsg.Text = "对" + Labelname.Text + "学习表现评价成功！";
                }
            }
            catch
            {
                Labelmsg.Text = "该页面被您修改过，评分值请用数字！<br/>（采用英语输入法数字）";
            }
        }
    }

    protected void RBLattitude_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DDLatt.SelectedValue = RBLattitude.SelectedValue;
        }
        catch
        {
            Labelmsg.Text = "该页面被您修改过，表现选项的分值必须在评分列表中能找到！";
        }
    }
}

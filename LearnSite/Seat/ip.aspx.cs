using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Seat_ip : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeIsAdmin();
        if (!IsPostBack)
            showIp();
    }
    protected void ButtonIp_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Hid"] != null)
        {
            string hid = Request.QueryString["Hid"].ToString();
            string ipgate = TextBoxIpGate.Text.Trim();
            string ipbegin = TextBoxIpBegin.Text.Trim();
            string ipend = TextBoxIpEnd.Text.Trim();

            //检验IP， 清除旧列表，创建新列表
            if (ipgate.Length > 6 && LearnSite.Common.WordProcess.IsNum(ipbegin) && LearnSite.Common.WordProcess.IsNum(ipend))
            {
                LearnSite.BLL.Ip ibll = new LearnSite.BLL.Ip();
                int Ihid = Int32.Parse(hid);

                ibll.DeleteIhid(Ihid);
                System.Threading.Thread.Sleep(200);

                int firstnum = Int32.Parse(ipbegin);
                int lastnum = Int32.Parse(ipend);
                int count = System.Math.Abs(lastnum - firstnum) + 1;
                bool ispositive = true;//默认正数
                if (lastnum - firstnum < 0)
                    ispositive = false;//如果负数
                if (!ipgate.EndsWith("."))
                    ipgate = ipgate + ".";//如果网段最后位没带点则加上
                LearnSite.Model.Ip model = new LearnSite.Model.Ip();
                LearnSite.BLL.Ip bll = new LearnSite.BLL.Ip();
                for (int i = 0; i < count; i++)
                {
                    string ipstr = ipgate + firstnum.ToString();
                    if (ispositive)
                        firstnum++;
                    else
                        firstnum--;
                    model.Ihid = Ihid;
                    model.Inum = i + 1;
                    model.Iip = ipstr;
                    bll.Add(model);
                }
                System.Threading.Thread.Sleep(200);
                showIp();

            }
            else
            {
                LearnSite.Common.WordProcess.Alert("Ip网段和范围填写不正确！", this.Page);
            }
        }
    }
    private void showIp()
    {
        if (Request.QueryString["Hid"] != null)
        {
            string hid = Request.QueryString["Hid"].ToString();
            LearnSite.BLL.Ip ibll = new LearnSite.BLL.Ip();
            GVip.DataSource = ibll.GetHouseIp(Int32.Parse(hid));
            GVip.DataBind();
        }
    }
    protected void GVip_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string iid = GVip.DataKeys[GVip.EditIndex][0].ToString();
        string iip = ((TextBox)(GVip.Rows[e.RowIndex].FindControl("TextBoxIp"))).Text.Trim();
        if (iip.Length < 16)
        {
            if (Request.QueryString["Hid"] != null)
            {
                string hid = Request.QueryString["Hid"].ToString();
                LearnSite.BLL.Ip bll = new LearnSite.BLL.Ip();
                if (!bll.ExistsIp(Int32.Parse(hid), iip))
                {
                    bll.UpdateIip(iip, Int32.Parse(iid));
                }
                else
                {
                    LearnSite.Common.WordProcess.Alert("该IP已经存在列表中，请匆重复！", this.Page);
                }
            }
        }
        else
        {
            LearnSite.Common.WordProcess.Alert("请填写完整的IP！", this.Page);
        }
        GVip.EditIndex = -1;
        showIp();
    }
    protected void GVip_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GVip.EditIndex = -1;
        showIp();
    }
    protected void GVip_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GVip.EditIndex = e.NewEditIndex;
        showIp();
    }
    protected void GVip_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //当鼠标放上去的时候 先保存当前行的背景颜色 并给附一颜色 
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E1E8E1',this.style.fontWeight='';");
            //当鼠标离开的时候 将背景颜色还原的以前的颜色 
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            //单击行改变行背景颜色 
            e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#D8E0D8'; this.style.color='buttontext';this.style.cursor='default';");
        }
    }
    protected void ButtonIpExcel_Click(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.mngCookieNname] != null)
        {
            if (FileUploadip.HasFile)
            {
                if (Request.QueryString["Hid"] != null)
                {
                    string hid = Request.QueryString["Hid"].ToString();
                    string aa = LearnSite.Common.DataExcel.SaveIpExcel(FileUploadip);
                    if (!string.IsNullOrEmpty(aa))
                    {
                        LearnSite.BLL.Ip bll = new LearnSite.BLL.Ip();
                        bll.DeleteIhid(Int32.Parse(hid));//清空该机房的电脑IP对应表
                        Labelmsg.Text = LearnSite.Common.DataExcel.DataSettoIps(aa, Int32.Parse(hid));
                        System.Threading.Thread.Sleep(500);
                        showIp();
                    }
                }
            }
            else
            {
                Labelmsg.Text = "请选择要导入电脑编号与IP对应表格！";
            }
        }
        else
        {
            string ch = "请登录后执行操作！";
            LearnSite.Common.WordProcess.Alert(ch, this.Page);
        }
    }
}
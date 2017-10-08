using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_computers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "IP地址与机器名对应浏览页面";
            showIpMachine();
            //string deljs = "if(confirm('您确定删除所有IP记录吗? \\n 注意：请在学生登录前删除！'))return true;else return false; ";
            // BtnDelete.OnClientClick = deljs;
        }
    }

    private void showIpMachine()
    {
        LearnSite.BLL.Computers cbll = new LearnSite.BLL.Computers();
        GVComputer.DataSource = cbll.GetListOrderBy(Radiobtnorder.SelectedValue);
        GVComputer.DataBind();
        CheckBoxhostname.Checked = LearnSite.Common.XmlHelp.GetAutoHostName();
    }
    protected void GVComputer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
            if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
            {
                string strjs = "if(confirm('您确定删除该条记录吗?'))return true;else return false; ";
                ((LinkButton)e.Row.FindControl("LinkButton1")).OnClientClick = strjs;
            }
        }

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
    protected void Radiobtnorder_SelectedIndexChanged(object sender, EventArgs e)
    {
        showIpMachine();
    }
    protected void GVComputer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            if (e.CommandName.Equals("Lock"))
            {
                string myPid = e.CommandArgument.ToString();
                LearnSite.BLL.Computers cbll = new LearnSite.BLL.Computers();
                cbll.UpLock(Int32.Parse(myPid));
                System.Threading.Thread.Sleep(500);
                showIpMachine();
            }
            if (e.CommandName.Equals("Del"))
            {
                string myPid = e.CommandArgument.ToString();
                LearnSite.BLL.Computers cbll = new LearnSite.BLL.Computers();
                cbll.Delete(Int32.Parse(myPid));
                System.Threading.Thread.Sleep(500);
                showIpMachine();
            }
            if (e.CommandName.Equals("Upd"))
            {
                string myPid = e.CommandArgument.ToString();
                int i = ((GridViewRow)((Button)e.CommandSource).NamingContainer).RowIndex;
                //(该方法不需要html中的绑定,取id.刚刚看到的,加上来,好方法.......)

                TextBox tb = (TextBox)GVComputer.Rows[i].FindControl("TextBoxp");
                LearnSite.BLL.Computers cbll = new LearnSite.BLL.Computers();
                cbll.UpdateByPid(Int32.Parse(myPid), tb.Text.Trim());
                System.Threading.Thread.Sleep(500);
                showIpMachine();
            }
            if (e.CommandName.Equals("Can"))
            {
                System.Threading.Thread.Sleep(500);
                showIpMachine();
            }
        }
        else
        {
            string ch = "请登录后执行操作！";
            LearnSite.Common.WordProcess.Alert(ch, this.Page);
        }
    }
    protected void BtnUnlock_Click(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            LearnSite.BLL.Computers cbll = new LearnSite.BLL.Computers();
            cbll.UnLockAll();//全体解锁
            System.Threading.Thread.Sleep(500);
            showIpMachine();
        }
        else
        {
            string ch = "请登录后执行操作！";
            LearnSite.Common.WordProcess.Alert(ch, this.Page);
        }
    }

    protected void BtnOnlock_Click(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            LearnSite.BLL.Computers cbll = new LearnSite.BLL.Computers();
            cbll.OnLockAll();//全体加锁
            System.Threading.Thread.Sleep(500);
            showIpMachine();
        }
        else
        {
            string ch = "请登录后执行操作！";
            LearnSite.Common.WordProcess.Alert(ch, this.Page);
        }
    }
    protected void BtnImport_Click(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            if (FuHostnameIp.HasFile)
            {
                string aa = LearnSite.Common.DataExcel.SaveHostnameExcel(FuHostnameIp);
                if (!string.IsNullOrEmpty(aa))
                {
                    Labelmsg.Text = LearnSite.Common.DataExcel.DataSettoComputers(aa);
                    System.Threading.Thread.Sleep(500);
                    showIpMachine();
                }
            }
            else
            {
                Labelmsg.Text = "请选择要导入的Excel主机名与IP绑定表格！";
            }
        }
        else
        {
            string ch = "请登录后执行操作！";
            LearnSite.Common.WordProcess.Alert(ch, this.Page);
        }
    }
    protected void BtnRefresh_Click(object sender, EventArgs e)
    {
        showIpMachine();
    }
    protected void CheckBoxhostname_CheckedChanged(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            LearnSite.Common.XmlHelp.SetAutoHostName(CheckBoxhostname.Checked.ToString());
            Labelmsg.Text = "学生机主机名获取设置修改成功！";
        }
        else
        {
            string ch = "请登录后执行操作！";
            LearnSite.Common.WordProcess.Alert(ch, this.Page);
        }
    }
    protected void BtnDelAll_Click(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            LearnSite.BLL.Computers cbll = new LearnSite.BLL.Computers();
            cbll.DeleteAll();
            System.Threading.Thread.Sleep(500);
            showIpMachine();
        }
        else
        {
            string ch = "请登录后执行操作！";
            LearnSite.Common.WordProcess.Alert(ch, this.Page);
        }
    }
}
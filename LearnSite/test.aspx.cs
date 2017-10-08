using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        showdatetime();
    }

    private void showdatetime()
    {
        DateTime today = DateTime.Now;
        string msg = "短日期："+today.ToShortDateString()+"<br/><br/>长日期："+today.ToString();
        Label1.Text = msg;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string str="abc";
        int aa = Int32.Parse(str);
    }
}
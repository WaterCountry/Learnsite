using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LabelVer.Text = LearnSite.Common.WordProcess.SystemVersion();
            LabelVer.ToolTip = LearnSite.Common.WordProcess.SysVerUpdate();
        }
    }
}

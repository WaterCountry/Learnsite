using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Lessons_pretopicdiscuss : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.IsTeacherCookies();
        if (!IsPostBack)
        {
            showtopic();
            showreply();
        }
    }

    private void showreply()
    {
        int rtid = Int32.Parse(Request.QueryString["Tid"].ToString());
        LearnSite.BLL.TopicReply rbll = new LearnSite.BLL.TopicReply();
        Labelreplycount.Text = "共  贴";
        Labelreplycountbtm.Text = Labelreplycount.Text;        
    }    
    private void showtopic()
    {
        if (Request.QueryString["Tid"] != null)
        {
            int tid = Int32.Parse(Request.QueryString["Tid"].ToString());
            LearnSite.BLL.TopicDiscuss tbll = new LearnSite.BLL.TopicDiscuss();
            LearnSite.Model.TopicDiscuss tmodel = new LearnSite.Model.TopicDiscuss();
            tmodel = tbll.GetModel(tid);//获取主题讨论实体
            Labeltopic.Text = tmodel.Ttitle;
            TcloseCheck.Checked = tmodel.Tclose;

            Topics.InnerHtml = "<br/>&nbsp;讨论内容：" + HttpUtility.HtmlDecode(tmodel.Tcontent) ;
            LearnSite.BLL.Courses cbll = new LearnSite.BLL.Courses();
            Labelcourse.Text = cbll.GetTitle(tmodel.Tcid.Value);//获取学案名称
            
            LearnSite.BLL.TopicReply rbll = new LearnSite.BLL.TopicReply(); 
           
            TopicsResult.InnerHtml = "<br/>&nbsp;老师总结："  ;

            if (tmodel.Tclose)
            {
                Btnword.Enabled = false;//不可发表讨论
                Btnclock.ImageUrl = "~/Images/clockred.gif" + "?temp=" + DateTime.Now.Millisecond.ToString();
                Btnclock.ToolTip = "当前主题讨论关闭！";
            }
            else
            {
                Btnword.Enabled = true;//可发表讨论
                Btnclock.ImageUrl = "~/Images/clock.gif" + "?temp=" + DateTime.Now.Millisecond.ToString();
                Btnclock.ToolTip = "当前主题讨论开启!";
            }
        }
    }
}
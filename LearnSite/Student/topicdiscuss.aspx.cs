using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_topicdiscuss : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            LearnSite.Common.CookieHelp.KickStudent();

            if (!IsPostBack)
            {
                showtopic();
                showreply();
            }            
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }
    private void showreply()
    {
        int sgrade =Int32.Parse( Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
        int sclass =Int32.Parse( Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
        string rsnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
        int  rsid =Int32.Parse( Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString());
        int rtid = Int32.Parse(Request.QueryString["Tid"].ToString());
        LearnSite.BLL.TopicReply rbll = new LearnSite.BLL.TopicReply();
        if (Btnword.Text == "老师总结")
        {
            GVtopicDiscuss.DataSource = rbll.GetClassList(sgrade, sclass, rtid);
            GVtopicDiscuss.DataBind();
            Labelreplycount.Text = "共" + GVtopicDiscuss.Rows.Count.ToString() + "贴";
            Labelreplycountbtm.Text = Labelreplycount.Text;
        }
        else
        {
            if (rbll.ReplyCount(rtid, rsid) > 0)
            {
                GVtopicDiscuss.DataSource = rbll.GetClassList(sgrade, sclass, rtid);
                GVtopicDiscuss.DataBind();
                Labelreplycount.Text = "共" + GVtopicDiscuss.Rows.Count.ToString() + "贴";
                Labelreplycountbtm.Text = Labelreplycount.Text;
            }
        }
        Labelnostu.Text = rbll.GetNoReplay(sgrade, sclass, rtid);
    }
    protected void Btnword_Click(object sender, EventArgs e)
    {
        int rsid =Int32.Parse( Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString());
        string rsnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
        string rip = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["LoginIp"].ToString();
        string rgrade = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString();
        string rclass = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString();
        string ryear = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Syear"].ToString();            
        string rterm = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["ThisTerm"].ToString();
        int rtid = Int32.Parse(Request.QueryString["Tid"].ToString());
        int rcid = Int32.Parse(Request.QueryString["Cid"].ToString());
        string rwords = Request.Form["textareaWord"].Trim();
        string tempcut = LearnSite.Common.WordProcess.DropHTML(rwords);
        int counts = tempcut.Length;
        int rscore = 0;
        bool rban = false;

        if (counts > 5 && counts < 500)
        {
            rwords = rwords.Replace("<br />", "");
            rwords = rwords.Replace(",", "，");

            LearnSite.BLL.TopicReply rbll = new LearnSite.BLL.TopicReply();
            LearnSite.Model.TopicReply rmodel = new LearnSite.Model.TopicReply();
            rmodel.Rban = rban;
            rmodel.Rip = rip;
            rmodel.Rscore = rscore;
            rmodel.Rsnum = rsnum;
            rmodel.Rtid = rtid;
            rmodel.Rtime = DateTime.Now;
            rmodel.Rwords = HttpUtility.HtmlEncode(rwords);
            rmodel.Rgrade = Int32.Parse(rgrade);
            rmodel.Rterm = Int32.Parse(rterm);
            rmodel.Rcid = rcid;
            rmodel.Rclass = Int32.Parse(rclass);
            rmodel.Rsid = rsid;
            rmodel.Ryear = Int32.Parse(ryear);
            rmodel.Redit = false;
            rmodel.Ragree = 0;
            int myreply = rbll.ReplyCount(rtid, rsid);//获取该学号回复数
            if (Btnword.Text == "老师总结")
            {
                if (myreply > 0)
                {
                    rbll.UpdateTeacher(rtid, rsid, rwords);//更新老师总结
                }
                else
                {
                    rbll.Add(rmodel);//增加老师总结
                }

                Labeldiscuss.Text = "总结成功！";
            }
            else
            {
                bool historyban = rbll.Isban(rtid, rsid);
                if (!historyban)
                {
                    if (myreply < 1)
                    {
                        rbll.Add(rmodel);//增加一条回复
                        Labeldiscuss.Text = "回复成功！";
                    }
                    else
                    {
                        if (rbll.Isedit(rtid, rsid))
                        {
                            rbll.UpdateOne(rmodel);//修改一条回复
                            Labeldiscuss.Text = "修改成功！";
                        }
                        else
                        {
                            Labeldiscuss.Text = "默认回复最多为1次！";
                        }
                    }
                }
                else
                {
                    Labeldiscuss.Text = "你在回复中存在违纪言论已被老师禁言！";
                }
            }
            System.Threading.Thread.Sleep(500);
            showtopic();
            showreply();
        }
        else
        {
            Labeldiscuss.Text = "回复不能少于6个汉字或超过300个汉字，谢谢！";
        }
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

            Topics.InnerHtml = "讨论内容：" + HttpUtility.HtmlDecode(tmodel.Tcontent) ;
            LearnSite.BLL.Courses cbll = new LearnSite.BLL.Courses();
            string mynum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();

            int sgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
            int sclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
            int syear = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Syear"].ToString());
            int hid = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Rhid"].ToString());
            
            string teasnum = "s" + hid + syear.ToString() + sgrade.ToString() + sclass.ToString();
            LearnSite.BLL.TopicReply rbll = new LearnSite.BLL.TopicReply(); 
            string treply = rbll.getteareply(tid, teasnum);
            TopicsResult.InnerHtml = "老师总结：" + HttpUtility.HtmlDecode(treply) ;

            if (mynum == teasnum)
            {
                Btnclock.Enabled = true;
                Btnword.Text = "老师总结";
                ImageBtngoodall.Visible = true;
            }
            if (tmodel.Tclose)
            {
                Btnword.Enabled = false;//不可发表讨论
                Btnclock.ImageUrl = "~/Images/clockred.gif" + "?temp=" + DateTime.Now.Millisecond.ToString();
                Btnclock.ToolTip = "主题讨论暂停！";
            }
            else
            {
                Btnword.Enabled = true;//可发表讨论
                Btnclock.ImageUrl = "~/Images/clock.gif" + "?temp=" + DateTime.Now.Millisecond.ToString();
                Btnclock.ToolTip = "主题讨论开启!";
            }
        }
    }

    protected void GVtopicDiscuss_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string eid = e.CommandArgument.ToString();
        LearnSite.BLL.TopicReply rbll = new LearnSite.BLL.TopicReply();
        if (e.CommandName == "Good")
        {
            int index = Convert.ToInt32(eid);
            rbll.Updatescore(index);
            System.Threading.Thread.Sleep(500);
            showreply();
        }
        if (e.CommandName == "Less")
        {
            int index = Convert.ToInt32(eid);
            rbll.Lessscore(index);
            System.Threading.Thread.Sleep(500);
            showreply();
        }
        if (e.CommandName == "Reply")
        {
            int index = Convert.ToInt32(eid);
            rbll.UpdateEdit(index);
            System.Threading.Thread.Sleep(500);
            showreply();
        }
        if (e.CommandName == "Del")
        {
            int index = Convert.ToInt32(eid);
            rbll.Delete(index);
            System.Threading.Thread.Sleep(500);
            showreply();
        }
        if (e.CommandName == "Agree")
        {
            int index = Convert.ToInt32(eid);
            string sid = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString();
            if (Session["Topic" + sid] != null)
            {
                int agcount = Int32.Parse(Session["Topic" + sid].ToString());
                if (agcount < 10)
                {
                    if (Session["Topic" + sid + "Agree" + index.ToString()] == null)
                    {
                        Session["Topic" + sid + "Agree" + index.ToString()] = "T";
                        Session["Topic" + sid] = agcount + 1;
                        rbll.UpdateAgree(index);
                        System.Threading.Thread.Sleep(500);
                        showreply();
                    }
                    else
                    {
                        LearnSite.Common.WordProcess.Alert("您已经点赞过了！", this.Page);
                    }
                }
                else
                {
                    LearnSite.Common.WordProcess.Alert("最多点赞9次！", this.Page);
                }
            }
            else
            {
                Session["Topic" + sid] = 1;
                rbll.UpdateAgree(index);
                System.Threading.Thread.Sleep(500);
                showreply();
            }
        }
    }
    protected void GVtopicDiscuss_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            Label lb = (Label)e.Row.FindControl("Labelfloor");
            Label lbsc = (Label)e.Row.FindControl("Labelscore");
            Label lbdate = (Label)e.Row.FindControl("Labeldate");
            Label lbagree = (Label)e.Row.FindControl("Labelagree");
            Image imgagree = (Image)e.Row.FindControl("Imageagree");
            Label lbsnum = (Label)e.Row.FindControl("Labelsnum");
            if (lbagree.Text != "")
            {
                int agree = Int32.Parse(lbagree.Text);
                if (agree > 9)
                    imgagree.Visible = true;
            }
            lb.Text = Convert.ToString( e.Row.RowIndex + 1);
            string mynum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
            int score = Int32.Parse(lbsc.Text);
            if (score > 0)
            {
                Image im = (Image)e.Row.FindControl("Imageflag");
                im.ImageUrl ="~/Images/topichot.png";
            }
                ImageButton imbtn = (ImageButton)e.Row.FindControl("ImageButtonDel");
                ImageButton imbtngood = (ImageButton)e.Row.FindControl("ImageButtonGood");
                ImageButton imbtnedit = (ImageButton)e.Row.FindControl("ImageButtonEdit");
                ImageButton imbtnless = (ImageButton)e.Row.FindControl("ImageButtonless");
                CheckBox ckbtn = (CheckBox)e.Row.FindControl("Ckedit");
            if (mynum.IndexOf("s")>-1)
            {
                imbtn.Visible = true;
                imbtngood.Visible = true;
                imbtnedit.Visible = true;
                imbtnless.Visible = true;
                imbtnedit.ToolTip = "不允许学生修改！";
                imbtngood.ToolTip = "加分！";
                imbtn.ToolTip = "删除！";
                Label lbsname = (Label)e.Row.FindControl("Labelsname");
                string strdeljs = "if(confirm('您确定删除" + lbsname.Text + "同学帖子吗?'))return true;else return false; ";
                
                imbtn.OnClientClick = strdeljs;
                if (ckbtn.Checked)
                {
                    imbtnedit.ImageUrl = "~/Images/ed.gif";
                    imbtnedit.ToolTip = "允许学生回复修改！";
                }
            }
            else
            {
                imbtn.Visible = false;
                imbtngood.Visible = false;
                imbtnedit.Visible = false;
                imbtnless.Visible = false;
                DateTime today = DateTime.Now;
                DateTime replaydate = DateTime.Parse(lbdate.Text);
                ImageButton imgbtnagree=(ImageButton)e.Row.FindControl("ImageButtonAgree");
                if (LearnSite.Common.Computer.Daygone(today, replaydate) > 5)
                    imgbtnagree.Visible = false;
                if(lbsnum.Text==mynum)
                    imgbtnagree.Visible = false;
            }
        }
    }
    protected void Btnclock_Click(object sender, ImageClickEventArgs e)
    {
        if (Btnword.Text == "老师总结")
        {
            bool chbtn =false;

            if (TcloseCheck.Checked)
                chbtn = false;//取反
            else
                chbtn = true;

            int tid = Int32.Parse(Request.QueryString["Tid"].ToString());
            LearnSite.BLL.TopicDiscuss tdbll = new LearnSite.BLL.TopicDiscuss();
            tdbll.UpdateTclose(tid, chbtn);//更新
            System.Threading.Thread.Sleep(500);
            showtopic();            
        }
    }
    protected void ImageBtnFresh_Click(object sender, ImageClickEventArgs e)
    {
        showreply();
        //showtopic();
        System.Threading.Thread.Sleep(500);
    }
    protected void ImageBtngoodall_Click(object sender, ImageClickEventArgs e)
    {
        string mynum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
        if (mynum.IndexOf("s") > -1)
        {
            int tid = Int32.Parse(Request.QueryString["Tid"].ToString());
            int sgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
            int sclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
            int syear = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Syear"].ToString());
            LearnSite.BLL.TopicReply rbll = new LearnSite.BLL.TopicReply();
            rbll.UpdateAllscore(tid, sgrade, sclass, syear);
            System.Threading.Thread.Sleep(500);
            showreply();
        }
    }
}
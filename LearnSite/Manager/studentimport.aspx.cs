using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Manager_studentimport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeIsAdmin();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "新生数据导入页面";
            ButtonClear.Attributes["OnClick"] = "return confirm('您确定要清除导入的数据吗？');";
            ButtonInsert.Attributes.Add("OnClick", "document.getElementById('Textcmd').value='正在获取数据中...... '; document.getElementById('Loading').style.display= '';");
            ButtonAppend.Attributes.Add("OnClick", "document.getElementById('Textcmd').value='正在导入数据中......'; document.getElementById('Loading').style.display= '';");

            ButtonAppend.Enabled = false;
            ButtonClear.Enabled = false;
        }
    }
    protected void ButtonInsert_Click(object sender, EventArgs e)
    {
        string msg = "";
        DateTime nowtime1 = DateTime.Now;
        ///上传Excel时，清空临时表， 将excel 读到ds中，再从ds读存到临时表中
        string savepath = LearnSite.Common.DataExcel.Saveupexcel(FileUpExcel);
        if (savepath != "")
        {
            LearnSite.Common.DataExcel.ClearUserexcel();
            System.Threading.Thread.Sleep(200);
            msg = LearnSite.Common.DataExcel.DataSettoStudentsExcel(LearnSite.Common.DataExcel.ExcelToDataSet(savepath), CheckBox1.Checked);
            if (LearnSite.Common.WordProcess.wordExist("功", msg))
            {
                ButtonAppend.Enabled = true;
            }
            else
            {
                System.IO.File.Delete(savepath);//不成功则删除上传的文件
            }
        }
        else
        {
            msg = "请选择上传的Excel文件！";
        }
        DateTime nowtime2 = DateTime.Now;
        Labelmsg.Text = msg + " 用时" + LearnSite.Common.Computer.Datagone(nowtime1, nowtime2) + "秒";
    }
    protected void ButtonAppend_Click(object sender, EventArgs e)
    {
        if (!LearnSite.Common.DataExcel.Checkrepeatall())
        {
            if (!LearnSite.Common.DataExcel.Checkrepeat())
            {
                Labelmsg.Text = "追导入数据操作完成！ 新增" + LearnSite.Common.DataExcel.AppendToStudents() + "条记录";
                GVrepeat.Visible = false;
                ButtonClear.Enabled = true;
            }
            else
            {
                DataSet ds = LearnSite.Common.DataExcel.CheckrepeatDs();//临时表内记录重复检验
                int dcount = ds.Tables[0].Rows.Count;
                if (dcount < 20)
                {
                    GVrepeat.Visible = true;
                    GVrepeat.DataSource = ds;
                    GVrepeat.DataBind();
                    Labelmsg.Text = "Excel中学号有重复，导入数据失败！请查看下面重复列表.";
                }
                else
                {
                    Labelmsg.Text = "学号重复超过20条，自行检查看上传的Excel数据，不再显示重复列表.";
                }
            }
        }
        else
        {
            DataTable dt = LearnSite.Common.DataExcel.Checkrepeatallds();
            GVrepeat.Visible = true;
            GVrepeat.DataSource = dt;
            GVrepeat.DataBind();
            Labelmsg.Text = "跟原平台的学号有重复，导入失败！请查看下面平台学号重复的列表.";
        }
        ButtonAppend.Enabled = false;
    }
    protected void ButtonClear_Click(object sender, EventArgs e)
    {
        Labelmsg.Text = "已经清除" + LearnSite.Common.DataExcel.DropFromStudents().ToString() +"条导入数据成功！请重新导入数据！";
        ButtonClear.Enabled = false;
    }

}

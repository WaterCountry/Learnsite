<%@ Page Title="" Language="C#" MasterPageFile="~/Manager/Manage.master"  StylesheetTheme="Teacher"  AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Manager_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div class="manageplace" >
    <div style=" margin: auto; border: 1px solid #E0E0E0; width: 480px; ">
        <div style="background-color: #EEEEEE; height: 18px;">
        系统说明</div>
        <br />
        <div style="padding: 6px; margin: auto; width: 431px; text-align: left; background-color: #FAFAFA; ">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 本系统是在原来ITMS1.0的基本上重新编写而成。数据库结构优化，代码基于<br />
            <br />
            三层架构原理编写，可扩展性强。系统分三种角色登录：学生、教师、管理员。<br />
            &nbsp;&nbsp;&nbsp;
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 教师平台功能：学案管理、学生管理、作品管理、签到管理、网页管理、打<br />
            <br />
            字管理、资源管理七大功能。<br />
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 管理后台功能：系统设置、教师管理、班级设置、新生导入、空间生成、学年<br />
            <br />
            升班七项。<br />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 历史版本：ITMS1.0—&gt;Magnet2.2—&gt;LearnSite1.100—&gt;LearnSite1.2.1.8<br />
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            2009年8月&nbsp;&nbsp; 温州水乡<br />
        </div>
        <br />
        </div>
    <br />
    <br />
                    <asp:Button ID="Btnlogout" runat="server" BackColor="#E6E6E6" 
                        BorderColor="#D4D4D4" BorderWidth="1px" Font-Names="Arial" 
                        Font-Size="9pt" Text="系统退出" Width="80px" 
            onclick="Btnlogout_Click" />
       
    <br />
       
    <br />
       
    <br />
    <asp:TextBox ID="TextBox1" runat="server"  SkinID="TextBoxindex" ReadOnly="true" Width="80px">操作流程图：</asp:TextBox>
    <asp:TextBox ID="TextBox3" runat="server"  SkinId="TextBoxaa"
        ReadOnly="True" Width="56px">班级设置</asp:TextBox>
    <asp:TextBox ID="TextBox7" runat="server"  SkinId="TextBoxbb"  ReadOnly="True" Width="22px">→</asp:TextBox>
    <asp:TextBox ID="TextBox2" runat="server"  SkinId="TextBoxaa" ReadOnly="True" Width="56px">教师管理</asp:TextBox>
    <asp:TextBox ID="TextBox8" runat="server"  SkinId="TextBoxbb"  ReadOnly="True" Width="22px">→</asp:TextBox>
    <asp:TextBox ID="TextBox4" runat="server"  SkinId="TextBoxaa" ReadOnly="True" Width="56px">新生导入</asp:TextBox>
    <asp:TextBox ID="TextBox9" runat="server" SkinId="TextBoxbb"  ReadOnly="True" Width="22px">→</asp:TextBox>
    <asp:TextBox ID="TextBox5" runat="server"  SkinId="TextBoxaa"  ReadOnly="True" Width="56px">空间生成</asp:TextBox>
    <br />
    <br />
    <b>管理员</b>：创建全校完整班级列表-&gt;添加教师并给教师选择指定的班级-&gt;使用学生excel模板导入新生<br />
    <br />
    <b>教师</b>：教师平台登录-&gt;备课(使用活动、调查、讨论完成学案设计)-&gt;上课(查看签到、查看作品等情况)-&gt;评价-&gt;反思<br />
    <br />
    <b>学生</b>：学生平台登录-&gt;浏览当前学案-&gt;完成导学及准备部分-&gt;完成课堂活动、调查、讨论等-&gt;作品展示-&gt;师生互动小结<br />
    <br />
       
</div>

</asp:Content>


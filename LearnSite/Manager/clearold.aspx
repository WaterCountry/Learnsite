<%@ Page Title="" Language="C#" MasterPageFile="~/Manager/Manage.master"  StylesheetTheme="Teacher"   AutoEventWireup="true" CodeFile="clearold.aspx.cs" Inherits="Manager_clearold" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div class="manageplace" >
    <div style=" margin: auto; border: 1px solid #E0E0E0; width: 480px; ">
        <div style="background-color: #EEEEEE; height: 18px;">
        数据清理</div>
        <br />
        <div style="border: 1px solid #CCCCCC; margin: auto; width: 322px; text-align:center; background-color: #FDF2D9;">
            <br />
        请选择清理 
        <asp:DropDownList ID="DDLyear" runat="server" Font-Size="9pt">
                <asp:ListItem Selected="True" Value="3">三年前</asp:ListItem>
                <asp:ListItem Value="5">五年前</asp:ListItem>
            </asp:DropDownList>
        的数据<br />
            <br />
            <asp:Button ID="ButtonClear" runat="server" SkinID="BtnNormal" Text="执行清理" 
                ToolTip="提示：将指定年前的作品记录、签到记录、讨论记录删除！" onclick="ButtonClear_Click" />
            <br />
            <br />
            (指作品记录、签到记录和测验记录)<br />
            <br />
        </div>
        <br />
        <div style="border: 1px solid #CCCCCC; margin: auto; width: 322px; text-align:center; background-color: #EFFDE8;">
            <br />
            <asp:Button ID="ButtonClearTyper" runat="server" SkinID="BtnLong" Text="清除全校中文打字成绩" 
                ToolTip="提示：将清除全校中文打字成绩！" onclick="ButtonClearTyper_Click"  />
            <br />
            <br />           
            <asp:Button ID="ButtonClearFinger" runat="server" SkinID="BtnLong" Text="清除全校指法练习成绩" 
                ToolTip="提示：将清除全校指法练习成绩！" onclick="ButtonClearFinger_Click"  />
            <br />
        
            <br />
        </div>
        <br />
    <div style="border: 1px solid #CCCCCC; margin: auto; width: 322px; text-align:center; background-color: #FEECE9;">
            请选择要清空学生的班级：<br />
            <br />
            <asp:DropDownList ID="DDLgrade" 
            runat="server" Font-Size="9pt" 
            Width="40px" EnableTheming="True" AutoPostBack="True" 
            onselectedindexchanged="DDLgrade_SelectedIndexChanged">
        </asp:DropDownList>
            年级<asp:DropDownList ID="DDLclass" runat="server" Font-Size="9pt" Width="40px" 
            AutoPostBack="True" onselectedindexchanged="DDLclass_SelectedIndexChanged">
        </asp:DropDownList>
            班级<br />
            <br />
            当前学生数：<asp:TextBox ID="TextBoxcount" runat="server" SkinID="TextBoxaa"></asp:TextBox>
            <br />
            <br />
            <asp:CheckBox ID="CheckBoxDel" runat="server" Text="确认操作" />
            <br />
            <br />
            <asp:Button ID="ButtonClearStudent" runat="server" SkinID="BtnLong" Text="清空该班级所有学生" 
                ToolTip="提示：将清空该班级的所有学生及其作品、签到、调查、讨论等记录，无法恢复！" onclick="ButtonClearStudent_Click"  />
            <br />
            <br />
        </div>
    <br />
    <asp:Label ID="Labelmsg" runat="server" SkinID="LabelMsgRed">清理前注意备份数据库！</asp:Label>       
    <br />
    <br />
    </div>
    <br />
    <br />
    <br />
</div>
</asp:Content>

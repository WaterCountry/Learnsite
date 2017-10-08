<%@ Page Title="" Language="C#" MasterPageFile="~/Manager/Manage.master" AutoEventWireup="true" CodeFile="teacheradd.aspx.cs" Inherits="Manager_teacheradd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div class="manageplace" >
        <div  class="teacheradd">
            <div style="border: 1px solid #EEEEEE; height: 20px; background-color:#AEB5B9;">教师添加</div>
            <br />
            <br />
            <br />
            账号：<asp:TextBox ID="Texthname" runat="server" BorderColor="#DDDDDD" 
                BorderStyle="Solid" BorderWidth="1px" Width="120px"></asp:TextBox>
            <br />
            <br />
            昵称：<asp:TextBox ID="Texthnick" runat="server" BorderColor="#DDDDDD" 
                BorderStyle="Solid" BorderWidth="1px" Width="120px"></asp:TextBox>
            <br />
            <br />
            密码：<asp:TextBox ID="Texthpwd" runat="server" BorderColor="#DDDDDD" 
                BorderStyle="Solid" BorderWidth="1px" Width="120px"></asp:TextBox>
            <br />
            <br />
            权限： <asp:CheckBox ID="Ckhpermiss" runat="server" Text="是否设置为管理员" />
            <br />
            <br />
            备注：<asp:TextBox ID="Texthnote" runat="server" BorderColor="#DDDDDD"  
                BorderStyle="Solid" BorderWidth="1px" Height="60px" TextMode="MultiLine" 
                Width="120px"></asp:TextBox>
            <br />
            <br />
            <br />
                    <asp:Button ID="Btnadd" runat="server" BackColor="#E6E6E6" 
                        BorderColor="#D4D4D4" BorderWidth="1px" Font-Names="Arial" 
                Font-Size="9pt" Text="确定" Width="60px" Height="20px" 
                onclick="Btnadd_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Btnreturn" runat="server" BackColor="#E6E6E6" 
                        BorderColor="#D4D4D4" BorderWidth="1px" Font-Names="Arial" 
                Font-Size="9pt" Text="返回" Width="60px" Height="20px" 
                onclick="Btnreturn_Click" />
                    <br />
            <br />
            <br />
        </div>
        <br />
        <br />
        <asp:Label ID="Labelmsg" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <br />
        <br />
        <br />
</div>
</asp:Content>


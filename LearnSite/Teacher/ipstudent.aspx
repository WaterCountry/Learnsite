<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ipstudent.aspx.cs" Inherits="Teacher_ipstudent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center; font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt;">    
        <br />
&nbsp;
        本机IP<asp:Label ID="LabelIp" runat="server" Font-Bold="True"></asp:Label>
        登录过最近三个月内的学生列表<br />
        <br />
        <div style="margin: auto; width: 98%;">
    <asp:GridView ID="GridViewworks" runat="server"  Width="100%"  AutoGenerateColumns="False" EnableModelValidation="True" 
            BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
            CellPadding="4" Font-Size="9pt" GridLines="Horizontal">
        <Columns>
            <asp:BoundField DataField="Qip" HeaderText="IP地址" />
            <asp:BoundField DataField="Qmachine" HeaderText="主机名" />
            <asp:BoundField DataField="Sname" HeaderText="学生名" />
            <asp:BoundField DataField="Sgrade" HeaderText="年级" />
            <asp:BoundField DataField="Sclass" HeaderText="班级" />
            <asp:BoundField DataField="Sex" HeaderText="性别" />
            <asp:BoundField DataField="Sscore" HeaderText="学分" />
            <asp:BoundField DataField="Qdate" HeaderText="日期"/>
            <asp:BoundField DataField="Saddress" HeaderText="住址" />
            <asp:BoundField DataField="Sheadtheacher" HeaderText="班主任" />
        </Columns>
        <FooterStyle BackColor="White" ForeColor="#333333" />
        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />        
        <RowStyle BackColor="White" ForeColor="#333333" HorizontalAlign="Left" />
        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
    </div>
        <br />
        <br />
        <br />
     <asp:Button ID="Btnclose" runat="server"   Text="关闭" BackColor="WhiteSmoke" 
            BorderColor="#CCCCCC" BorderStyle="None" Font-Size="9pt" Height="20px" 
            Width="60px" />
        <br />
        <br />
        <asp:Label ID="Labelmsg" runat="server"></asp:Label>
        <br />
        <br />
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mysurveyrank.aspx.cs" Inherits="Student_mysurveyrank" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <style type="text/css">
*
{
    margin-right: 0px;
    margin-left: 0px;
}
    </style>
    <title>课堂小测验班级排行</title>
</head>
<body>
<form id="form1" runat="server">
<div>
<div style=" text-align: center;">
<br />
<asp:Label ID="Labeltitle" runat="server" Font-Bold="True" Font-Size="12pt" 
        ForeColor="#273F4B"></asp:Label>
                        <br />
                        <br />
                        <asp:GridView ID="GridViewclass" runat="server" 
                            AutoGenerateColumns="False"                         
                            onrowdatabound="GridViewclass_RowDataBound" 
                            Width="500px" TabIndex="1" CellPadding="3" BorderColor="#336699" 
                        BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" 
        Font-Size="9pt" HorizontalAlign="Center" EnableModelValidation="True" 
        GridLines="Horizontal" >
                            <RowStyle ForeColor="#003366" />
                            <Columns>
                                <asp:BoundField HeaderText="序号" />
                                <asp:BoundField DataField="Sgrade" HeaderText="年级" />
                                <asp:BoundField DataField="Sclass" HeaderText="班级" />
                                <asp:BoundField DataField="Snum" HeaderText="学号" />
                                <asp:BoundField DataField="Sname" HeaderText="姓名" />
                                <asp:BoundField DataField="Fscore" HeaderText="成绩" />
                            </Columns>  
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#CCDBE3" Font-Bold="True" ForeColor="#273F4B" />                          
                        </asp:GridView>
                <br />
    <asp:Label ID="Labelmsg" runat="server" Font-Size="9pt"></asp:Label>
    <br />
                <br />
        <asp:Button ID="Btnreturn" runat="server"  Text="关闭" Height="20px" 
                    Width="80px" BackColor="#CCDBE3" BorderStyle="Solid" Font-Size="9pt" 
                        ForeColor="Black" BorderColor="#336699" BorderWidth="1px"/>
                <br />
                <br />
        <br />
    </div>
</div>
</form>
</body>
</html>

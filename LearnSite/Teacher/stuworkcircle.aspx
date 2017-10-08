<%@ Page Language="C#" AutoEventWireup="true" CodeFile="stuworkcircle.aspx.cs" Inherits="Teacher_stuworkcircle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center; font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt;">    
        <br />
        学号：<asp:Label ID="LabelSnum" runat="server"></asp:Label>
&nbsp;姓名：<asp:Label ID="LabelSname" runat="server"></asp:Label>
&nbsp;作品总分：<asp:Label ID="LabelWscore" runat="server"></asp:Label>
&nbsp;
        本学期作品列表<br />
        <br />
        <div style="margin: auto; width: 98%;">
<center>
    <div style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt">
        <asp:ImageButton ID="ImgBtnLeft" runat="server" ImageUrl="~/Images/left.png" 
            onclick="ImgBtnLeft_Click" Width="16px" />
    <asp:DropDownList ID="DDLstore" runat="server" 
            Font-Bold="True" Width="300px" AutoPostBack="True" Font-Size="12pt" 
            onselectedindexchanged="DDLstore_SelectedIndexChanged">
        <asp:ListItem></asp:ListItem>
        </asp:DropDownList>
        <asp:ImageButton ID="ImgBtnright" runat="server" 
            ImageUrl="~/Images/right.png" onclick="ImgBtnright_Click" />
         <br />
            <asp:Label ID="lbcount" runat="server"></asp:Label>
        <asp:ImageButton ID="ImgBtn" runat="server" ImageUrl="~/Images/refresh.gif" 
            onclick="ImgBtn_Click" ToolTip="循环展播专用刷新" />
         <br />
        </div>        
        <div style=" font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt; margin: 2px; " >
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </div>
        </center>
    </div>
         <asp:Label ID="lbcurindex" runat="server" Text="0" Visible="False"></asp:Label>
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

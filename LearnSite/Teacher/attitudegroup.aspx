<%@ Page Title="" Language="C#" StylesheetTheme="Teacher" ValidateRequest="false"  AutoEventWireup="true" CodeFile="attitudegroup.aspx.cs" Inherits="Teacher_attitudegroup" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title> 
    <style type="text/css">
    .by{margin:0px}
    .phold{margin: auto; width:360px; text-align: center;font-size: 9pt;font-family: Arial;}
    .hearder{ background-color: #939CA2;height: 18px;text-align: center;line-height: 18px;}
    </style>
</head>
<body class="by">
    <form id="form1" runat="server">
    <div  class="phold" >
    <div  class="hearder">  
        对<asp:Label ID="Labelname" runat="server" Font-Bold="True"></asp:Label>&nbsp;同学小组评价
     </div>    
        <br />
        小组评语：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 评分：<asp:DropDownList 
            ID="DDLatt" runat="server" Font-Size="9pt">
            <asp:ListItem>10</asp:ListItem>
            <asp:ListItem>9</asp:ListItem>
            <asp:ListItem>8</asp:ListItem>
            <asp:ListItem>7</asp:ListItem>
            <asp:ListItem>6</asp:ListItem>
            <asp:ListItem Selected="True">5</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>-1</asp:ListItem>
            <asp:ListItem>-2</asp:ListItem>
            <asp:ListItem>-3</asp:ListItem>
            <asp:ListItem>-4</asp:ListItem>
            <asp:ListItem>-5</asp:ListItem>
        </asp:DropDownList>    
        <br />
        <asp:TextBox ID="TextBox2" runat="server" BackColor="#FFE7CE" Height="60px" 
            TextMode="MultiLine" ToolTip="填写好自定义评语后，请手动上面您的评分！" 
            Width="240px"></asp:TextBox>
        <br />
    <asp:Label ID="Labelmsg" runat="server"></asp:Label>
        <br />
    <br />
    <asp:Button ID="Btnattitude" runat="server"  Text="确定"  
        onclick="Btnattitude_Click"  SkinID="BtnNormal"  />
</div>
    </form>
</body>
</html>


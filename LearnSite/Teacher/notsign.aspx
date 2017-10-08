<%@ Page Title="" Language="C#" StylesheetTheme="Teacher" AutoEventWireup="true"   CodeFile="notsign.aspx.cs" Inherits="Teacher_notsign" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        对<asp:Label ID="Labelname" runat="server" Font-Bold="True"></asp:Label>&nbsp;同学缺席备注
     </div>    
        <br />
        缺席原因：<br />
        <asp:TextBox ID="TextBox1" runat="server" Width="220px" Height="112px" 
        BackColor="#FFE7CE" TextMode="MultiLine"></asp:TextBox>
        <br />
    <asp:Label ID="Labelmsg" runat="server"></asp:Label>
        <br />
    <asp:Button ID="Btnnotsign" runat="server"  Text="确定"  
        onclick="Btnnotsign_Click"  SkinID="BtnNormal"  />
</div>
</form>
</body>
</html>


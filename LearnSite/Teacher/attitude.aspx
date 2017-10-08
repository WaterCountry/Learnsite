<%@ Page Title="" Language="C#" StylesheetTheme="Teacher" ValidateRequest="false" AutoEventWireup="true" CodeFile="attitude.aspx.cs" Inherits="Teacher_attitude" %>

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
<body  class="by">
    <form id="form1" runat="server">
    <div  class="phold" >
    <div  class="hearder">  
        对  
    <asp:Label ID="Labelname" runat="server" Font-Bold="True"></asp:Label>
     &nbsp;同学评分
     </div>
    
        <br />
        选择评分：<asp:DropDownList 
            ID="DDLatt" runat="server" Font-Size="9pt">
            <asp:ListItem>5</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>0</asp:ListItem>
            <asp:ListItem>-1</asp:ListItem>
            <asp:ListItem>-2</asp:ListItem>
            <asp:ListItem>-3</asp:ListItem>
            <asp:ListItem>-4</asp:ListItem>
            <asp:ListItem>-5</asp:ListItem>
        </asp:DropDownList>
    <div style="margin: auto; width: 300px">
    <asp:RadioButtonList ID="RBLattitude" runat="server" 
            Height="120px" onselectedindexchanged="RBLattitude_SelectedIndexChanged" 
            Width="100%" RepeatColumns="2" AutoPostBack="True" >
        <asp:ListItem Value="2" >乐于助人</asp:ListItem>
        <asp:ListItem Value="1">表现优秀</asp:ListItem>
        <asp:ListItem Value="-1">有开小差</asp:ListItem>
        <asp:ListItem Value="-2">乱扔垃圾</asp:ListItem>
        <asp:ListItem Value="-3">上课迟到</asp:ListItem>
        <asp:ListItem Value="-4">损坏公物</asp:ListItem>
    </asp:RadioButtonList>
    </div>
        自定义课堂评语：<br />
        <asp:TextBox ID="TextBox2" runat="server" BackColor="#FFE7CE" Height="60px" ToolTip="填写好自定义评语后，请手动上面您的评分！" 
            Width="240px" TextMode="MultiLine"></asp:TextBox>
        <br />
    <asp:Label ID="Labelmsg" runat="server"></asp:Label>
    <br />
    <asp:Button ID="Btnattitude" runat="server"  Text="确定"  
        onclick="Btnattitude_Click"  SkinID="BtnNormal"  />
</div>
    </form>
</body>
</html>


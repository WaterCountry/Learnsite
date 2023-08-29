<%@ page language="C#" autoeventwireup="true" inherits="Student_mysurveymate, LearnSite" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div style="border: 1px solid #C7D8DA; margin: auto; font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 11pt; ">
        <div  id="Mitem" runat ="server" style="margin: auto;background-color: #DFE9EA;">
        <strong>选中本选项的同学列表</strong></div>
        <br />
       <div style=" margin: auto">
        <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate> 
         <div style="border-color: #4D7477; padding: 2px; border-width: 1px;   border-bottom-style: dashed; height: 20px; line-height: 20px;">
         &nbsp;&nbsp;&nbsp;&nbsp;<%# Eval("Key")%>&nbsp;&nbsp;<%# Eval("Value")%></div>
        </ItemTemplate>
        </asp:Repeater>
        </div>
    </div>
    </div>
    </form>
</body>
</html>

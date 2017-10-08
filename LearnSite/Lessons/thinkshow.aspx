<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="thinkshow.aspx.cs" Inherits="Lessons_thinkshow" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
<div id="background" style="text-align: center; width:100%">
    <div style="width: 780px; margin:auto;">        
        <div  style="border: 1px solid #939CA2; height: 20px; text-align:center; font-size: 12pt; font-family: 宋体; font-weight: bold;background-color:#939CA2 ; margin:auto; ">
       〖课后思考〗<asp:Label ID="LabelTitle" runat="server" Text=""></asp:Label>
        &nbsp;&nbsp;<asp:ImageButton ID="BtnEdit" runat="server" ToolTip="点击修改" 
                     ImageUrl="~/Images/edit.gif" onclick="BtnEdit_Click" />         
    </div> 
    <div id="course" style="border: 1px solid #E3E3E3; margin:auto;">
    <br />    
    <asp:Repeater ID="Repeater1" runat="server">   
      <ItemTemplate>      
       <div  style=" width:780px;  font-size:9pt; text-align:left; margin:auto;">	
       <br />	
		&nbsp;&nbsp;&nbsp;&nbsp;<%# HttpUtility.HtmlDecode( Eval("Fcontent").ToString())%><br /><div style=" text-align:right">
		<%# Eval("Fdate")%>
		</div>
		<br />
		</div>		
		</ItemTemplate>
     </asp:Repeater>
    </div>        
    <br />
        <div style=" text-align:center">
        <asp:Label ID="Labelmsg" runat="server" Font-Size="9pt"></asp:Label>
        <br />              
        </div>
    </div>
        </div>
    </form>
</body>
</html>


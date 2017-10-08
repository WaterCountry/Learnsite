<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="typeshow.aspx.cs" Inherits="Teacher_typeshow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<br />
<div  class="typeplace"> 

    <asp:Repeater ID="Repeater1" runat="server">   
    <ItemTemplate>   
    <div style="width: 600px; font-size: 9pt;  border: whitesmoke 1px solid;  "> 	
    <div  style="height: 20px;;font-size: 12pt; font-family: 宋体;font-weight: bold ;background-color: whitesmoke ; text-align:center"><%#Eval("Ttitle")%></div>    
        <br />
        <div style="   font-size: 9pt; font-family:Arial;">
        <table width="600px"  >
			<tr>			
			<td style="width: 200px; height: 22px; text-align:center">			
			文章编号：<%# Eval("Tid")%>												
			</td>
			<td style="width: 200px; height: 22px; text-align:center">文章类型：<%#Eval("Ttype")%></td>
			<td style="width: 200px; height:22px; text-align:right">文章用途：<%#Eval("Tuse")%>  &nbsp;&nbsp; 
            <asp:ImageButton ID="BtnEdit" runat="server" ToolTip="点击修改" 
            ImageUrl="~/Images/edit.gif" onclick="BtnEdit_Click"  style="width: 16px" />
			</td>
			</tr>
		</table>
        <div >
        <div  style=" width:600px; word-break:break-all; word-wrap:break-word; text-align:left;">		
		<%# Eval("Tcontent")%>
		</div>
     </div>
	</div>   	
     <br/>      				
	</ItemTemplate>
 </asp:Repeater>
              <br />
              <asp:Button ID="Btnreturn" runat="server"  Text="返回" OnClick="Btnreturn_Click"  SkinID="BtnNormal" />
    <br />
 <br /> 
</div> 
</asp:Content>


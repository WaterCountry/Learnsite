<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master"  StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="typechineseshow.aspx.cs" Inherits="Teacher_typechineseshow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<br />
<div  class="typeplace"> 

    <asp:Repeater ID="Repeater1" runat="server">   
    <ItemTemplate>   
    <div style="width: 600px; font-size: 9pt;  border: whitesmoke 1px solid;  "> 	
    <div  style="height: 20px;;font-size: 12pt; font-family: 宋体;font-weight: bold ;background-color: whitesmoke ; text-align:center">
    <%#Eval("Ntitle")%>&nbsp;&nbsp; 
            <asp:ImageButton ID="BtnEdit" runat="server" ToolTip="点击修改" 
            ImageUrl="~/Images/edit.gif" onclick="BtnEdit_Click"  style="width: 16px" />
    </div>    
        <br />
        <div  style=" width:600px; word-break:break-all; word-wrap:break-word; text-align:left;">		
		<%# Eval("Ncontent")%>
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


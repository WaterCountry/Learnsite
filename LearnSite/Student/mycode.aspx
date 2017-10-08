<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Stud.master" AutoEventWireup="true" CodeFile="mycode.aspx.cs" Inherits="Student_mycode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cphs" Runat="Server">
<div class="divindex">
    <asp:Repeater ID="Repeater1" runat="server">
    <ItemTemplate>
  <div class="thumbbox">    
   <a href="codeproject.aspx?id=<%#Eval("Wid")%>"  target="_blank">
      <img class="timg"  src="<%#FixUrl(Eval("Wthumbnail").ToString())%>"   alt="..." />
    </a>
      <div class="text-center">
      <p><%#Eval("Wyear")%>级<%#Eval("Wgrade")%>(<%#Eval("Wclass")%>)班  <%#Eval("Wname")%>
             <img src="../Images/money.gif" alt="阅读" /><%#Eval("Whit")%>
      </p> 
      </div>
  </div>

    </ItemTemplate>
    </asp:Repeater>    
</div>
<div class="divindex">
<div class="text-center">
    <asp:LinkButton ID="LinkFront" runat="server" onclick="Linkfront_Click">上一页</asp:LinkButton>
    &nbsp;&nbsp;当前第<asp:Label ID="LabelCurrent" runat="server" ></asp:Label>页
    &nbsp;总共<asp:Label ID="LabelCount" runat="server" ></asp:Label>页
    &nbsp;&nbsp;<asp:LinkButton ID="LinkNext" runat="server" onclick="Linknext_Click">下一页</asp:LinkButton>
    </div>
</div>
</asp:Content>


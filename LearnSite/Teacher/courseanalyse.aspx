<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" AutoEventWireup="true" CodeFile="courseanalyse.aspx.cs" Inherits="Teacher_courseanalyse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div style=" font-size:9pt; text-align:center;">
    <br />
    <br />
    <asp:Label ID="Labeltitle" runat="server" Font-Bold="True" Font-Size="12pt"></asp:Label>
    &nbsp;<br />
    <br />
      &nbsp;<asp:Label ID="Labeldistribution" runat="server" Font-Bold="False"></asp:Label>
    <br />
    <br />
    <div id="divview" runat="server" visible="false"   style="margin: auto; text-align: center;">
            <asp:ImageButton ID="ImgBtnLeft" runat="server" ImageUrl="~/Images/left.png" 
            onclick="ImgBtnLeft_Click" Width="16px" />
    <asp:DropDownList ID="DDLstore" runat="server" 
            Font-Bold="True" Width="100px" AutoPostBack="True" Font-Size="12pt" 
            onselectedindexchanged="DDLstore_SelectedIndexChanged">
        <asp:ListItem></asp:ListItem>
        </asp:DropDownList>
        <asp:ImageButton ID="ImgBtnright" runat="server" 
            ImageUrl="~/Images/right.png" onclick="ImgBtnright_Click" />
            <br />
            <asp:Label ID="lbcount" runat="server"></asp:Label>
        </div>
    <asp:ImageButton ID="ImageButton1" runat="server" 
        ImageUrl="~/Images/return.gif" Width="16px" onclick="ImageButton1_Click" />
    <br />
        <div style="padding: 2px; margin: auto; text-align: center; font-size: 9pt;">
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            <br />
            <br />
        </div>
<br />
</div>
</asp:Content>


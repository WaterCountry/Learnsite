<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="coursecreate.aspx.cs" Inherits="Teacher_coursecreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
 <div  class="placehold">
    <br />
     <br />
     <br />
     <br />
     <br />
        <div  class ="create">
            <div  class="phead">
     学案创建</div>
            <br />
            <br />
&nbsp;&nbsp; 学案名称：<asp:TextBox ID="Texttitle" runat="server" Width="280px"  SkinID="TextBoxNormal"></asp:TextBox>
            <br />
            <br />
&nbsp;&nbsp; 学案分类：<asp:DropDownList ID="DDLclass" runat="server" Width="100px" 
            Font-Size="9pt">
            </asp:DropDownList>
            <br />
            <br />
            &nbsp;&nbsp; 教学年级：<asp:DropDownList ID="DDLcobj" runat="server" Width="100px" 
            Font-Size="9pt" AutoPostBack="True" 
                onselectedindexchanged="DDLcobj_SelectedIndexChanged">
                </asp:DropDownList>        
            <br />
            <br />
&nbsp;&nbsp; 按排课节：<asp:DropDownList ID="DDLCks" runat="server" Font-Size="8pt"
            Width="50px" Font-Names="Arial">          
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox ID="Checkcpublish" runat="server" Text="是否发布" 
            Checked="True" />
            <br />
            <br />
            <div class="courseshow">
                <asp:Label ID="Labelterm" runat="server" Text="Label"></asp:Label>
            </div>
            <br />
        </div>
        <br />
        <br />
                    <asp:Button ID="BtnCreate" runat="server"  Text="创建学案"  onclick="BtnCreate_Click"  SkinID="BtnNormal"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Btnreturn" runat="server"  Text="学案返回" onclick="Btnreturn_Click" SkinID="BtnNormal" />
                    <br />
                    <br />
     <br />           
        <asp:Label ID="Labelmsg" runat="server"></asp:Label>           
        <br />
        <br />
     <br />           
        </div>
</asp:Content>


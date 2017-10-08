<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master"  StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="typeadd.aspx.cs" Inherits="Teacher_typeadd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div  class="placehold">
<br />
    <div  class="typediv">
        &nbsp; 中文文章标题：<asp:TextBox ID="Ttitle" runat="server"  Width="220px"  
            SkinID="TextBoxNormal"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnNoSet" runat="server" Text="清除格式" OnClick="BtnNoSet_Click"  SkinID="BtnNormal"  ToolTip="系统限制汉字长度为210个" />
                </div>
    <div  class="typediv">
        <asp:TextBox ID="Tcontent" runat="server" Height="180px" MaxLength="300" TextMode="MultiLine"
            Width="650px" BorderColor="#DFDFDF" BorderStyle="Solid" BorderWidth="1px" 
            BackColor="White" ></asp:TextBox>
        <br />
     <div  class="typedivcenter">
         <br />
              <asp:Button ID="BtnAdd" runat="server"  Text="添加" OnClick="BtnAdd_Click"  SkinID="BtnNormal" />&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Button ID="Btnreturn" runat="server"  Text="返回" OnClick="Btnreturn_Click"  SkinID="BtnNormal" /><br />
               <br />
               <asp:Label ID="Labelmsg" runat="server">系统限制文章长度为210个汉字，添加时自动去空格并裁剪！</asp:Label>
         <br />
         </div>
         </div>
         <br />
         <br />           
        </div>
</asp:Content>


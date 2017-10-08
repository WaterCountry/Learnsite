<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher" Validaterequest="false" AutoEventWireup="true" CodeFile="typeedit.aspx.cs" Inherits="Teacher_typeedit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div   class="placehold">
<br />
    <div  class="typedivhead">
        &nbsp; 文章标题：<asp:TextBox ID="Ttitle" runat="server"  Width="220px"  SkinID="TextBoxNormal"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="Ttitle" Font-Size="9pt" Width="10px"></asp:RequiredFieldValidator>
        文章用途：<asp:DropDownList ID="DDLuse" runat="server" Font-Size="9pt"
            Width="71px" Font-Names="Arial">
            <asp:ListItem Selected="True" Value="11">中文练习</asp:ListItem>
            <asp:ListItem Value="12">中文比赛</asp:ListItem>
            <asp:ListItem Value="21">英文练习</asp:ListItem>
            <asp:ListItem Value="22">英文比赛</asp:ListItem>
        </asp:DropDownList>
        文章范围：<asp:DropDownList ID="DDLtype" runat="server" Font-Size="8pt"
            Width="34px" Font-Names="Arial">
            <asp:ListItem>0</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
        </asp:DropDownList>
        &nbsp;
        <asp:Button ID="BtnNoSet" runat="server" Text="清除格式" OnClick="BtnNoSet_Click"  SkinID="BtnNormal"  ToolTip="系统限制汉字长度为210个" />
                </div>
    <div  class="typediv">
        <asp:TextBox ID="Tcontent" runat="server" Height="180px" MaxLength="300" TextMode="MultiLine"
            Width="650px" BorderColor="#DFDFDF" BorderStyle="Solid" BorderWidth="1px" 
            BackColor="White" ></asp:TextBox>
        <br />
     <div  class="typedivcenter">
         <br />
              <asp:Button ID="BtnEdit" runat="server" Text="修改" OnClick="BtnEdit_Click" SkinID="BtnNormal"  />&nbsp;&nbsp;&nbsp;
              <asp:Button ID="Btnreturn" runat="server"  Text="返回" OnClick="Btnreturn_Click"  SkinID="BtnNormal" /><br />
               <br />
               <asp:Label ID="Labelmsg" runat="server" ></asp:Label>
         <br />
         </div>
         </div><br />
         <br />           
        </div>
</asp:Content>


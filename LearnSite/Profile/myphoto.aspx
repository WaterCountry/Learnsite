<%@ Page Title="" Language="C#" MasterPageFile="~/Profile/Pf.master"  StylesheetTheme="Student"  AutoEventWireup="true" CodeFile="myphoto.aspx.cs" Inherits="Profile_myphoto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cstu" Runat="Server">
<br />
<center>
    <asp:Panel ID="Panel1" runat="server" Width="330px">
        <asp:Image ID="Imageface" runat="server" />
        <br />
        <br />
        <asp:FileUpload ID="PhotoFileUpload" runat="server" Font-Size="9pt" 
            Width="200px" />
        &nbsp;<asp:Button ID="Btnphoto" runat="server" Enabled="False" Height="20px" 
            onclick="Btnphoto_Click" SkinID="buttonSkin" Text="相片提交" Width="80px" />
        <br />
        <br />
        <asp:Label ID="Labelstr" runat="server" SkinID="LabelMsgRed"></asp:Label>
        <br />
    </asp:Panel>
    <br />
    说明：相片类型为jpg或gif格式图片，大小不超过1024KB,宽高不限制，过大则自动缩小为宽度320像素。
    <br />
    <br />
</center>
    <br />
</asp:Content>


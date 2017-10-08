<%@ Page Language="C#"  StylesheetTheme="Teacher"  AutoEventWireup="true" CodeFile="workpackage.aspx.cs" Inherits="Teacher_workpackage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: auto; text-align: center;font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt">
          班级作品打包<br />
          <br />
          <asp:DropDownList ID="DDLgrade" runat="server" Font-Size="9pt" 
            Width="50px" EnableTheming="True" AutoPostBack="True" 
                onselectedindexchanged="DDLgrade_SelectedIndexChanged">
        </asp:DropDownList>
            年级 
            <asp:DropDownList ID="DDLclass" runat="server" Font-Size="9pt" 
            Width="50px" EnableTheming="True" AutoPostBack="True" 
                onselectedindexchanged="DDLclass_SelectedIndexChanged">
        </asp:DropDownList>
            班级&nbsp;&nbsp;&nbsp;学案名称：<asp:DropDownList ID="DDLCid" runat="server" Font-Names="Arial" 
              Font-Size="9pt" AutoPostBack="True" 
              onselectedindexchanged="DDLCid_SelectedIndexChanged">
          </asp:DropDownList>
          &nbsp;
          <asp:Button ID="Button1" runat="server" Text="作品打包" onclick="Button1_Click" 
              SkinID="BtnNormal" />
          <br />
          <br />
          <asp:Label ID="Labelyear" runat="server" Visible="False"></asp:Label>
          <br />
          <div style="border: 1px dashed #CCCCCC; width: 300px; margin: auto; background-color: #FFFFFF;">
              <br />
          <br />
          <asp:HyperLink ID="HyperLink1" runat="server">本学案作品包下载</asp:HyperLink>
          <br />
          <br />
          <asp:Label ID="Labelmsg" runat="server">打包时请耐心等待几秒！</asp:Label>
          </div>
          <br />
          </div>
    </form>
</body>
</html>

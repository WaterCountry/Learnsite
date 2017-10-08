<%@ Page Title="" Language="C#" StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="studentedit.aspx.cs" Inherits="Teacher_studentedit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
<div  class="placehold">
    学生基本信息
    <div class="teacherdiv">    
         <table width="100%" style="border-right: WhiteSmoke 1px solid; border-top: WhiteSmoke 1px solid; border-left: WhiteSmoke 1px solid; border-bottom: WhiteSmoke 1px solid; text-align: left; font-size: 9pt;">
            <tr>
                <td style="width: 180px; height: 22px ">
                    &nbsp;学号
                    <asp:TextBox ID="Tsnum" runat="server"  SkinID="TextBoxNormal" Width="110px" ReadOnly="True"  ToolTip="学号不可修改！" ></asp:TextBox></td>
                <td style="width: 180px; height: 22px ">
                    &nbsp;姓名
                    <asp:TextBox ID="Tsname" runat="server"  SkinID="TextBoxNormal" Width="110px" 
                        BackColor="Cornsilk"></asp:TextBox></td>
                <td style="width: 180px; height: 22px ">
                    &nbsp;入学
                    <asp:TextBox ID="Tsyear" runat="server"  SkinID="TextBoxNormal" Width="110px" ReadOnly="True"  ></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 180px; height: 22px">
                    &nbsp;年级
                    <asp:DropDownList ID="DDLgrade" runat="server" Font-Size="9pt" 
                Width="60px" BackColor="Cornsilk" Enabled="False">
    </asp:DropDownList></td>
                <td style="width: 180px; height: 22px">
                    &nbsp;班级
                    <asp:DropDownList ID="DDLclass" runat="server" Font-Size="9pt" Width="60px" 
                        BackColor="Cornsilk">
    </asp:DropDownList></td>
                <td style="width: 180px; height: 22px">
                    &nbsp;主任
                    <asp:TextBox ID="Tsheadtheacher" runat="server"  SkinID="TextBoxNormal" 
                        Width="110px" BackColor="Cornsilk"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 180px; height: 22px">
                    &nbsp;密码
                    <asp:TextBox ID="Tspwd" runat="server"  SkinID="TextBoxNormal" Width="110px" 
                        ToolTip="密码可修改！" BackColor="Cornsilk"></asp:TextBox></td>
                <td style="width: 180px; height: 22px">
                    &nbsp;性别
                    <asp:DropDownList ID="DDLsex" runat="server" Font-Size="9pt" Width="60px" 
                        BackColor="Cornsilk">
    </asp:DropDownList></td>
                <td style="width: 180px; height: 22px">
                    &nbsp;父母 <asp:TextBox ID="Tsparents" runat="server"  SkinID="TextBoxNormal" 
                        Width="110px" BackColor="Cornsilk"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 180px; height: 22px">
                    &nbsp;表现 <asp:TextBox ID="Tsattitude" runat="server"  SkinID="TextBoxNormal" Width="110px"   ReadOnly="True" ToolTip="表现不可修改！"></asp:TextBox></td>
                <td style="width: 180px; height: 22px">
                    &nbsp;成绩
                    <asp:TextBox ID="Tsscore" runat="server"  SkinID="TextBoxNormal" Width="110px" ReadOnly="True" ToolTip="成绩不可修改！"></asp:TextBox></td>
                <td style="width: 180px; height: 22px">
                    &nbsp;电话 <asp:TextBox ID="Tsphone" runat="server"  SkinID="TextBoxNormal" 
                        Width="110px" BackColor="Cornsilk"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="height: 22px" colspan="3">
                    &nbsp;地址<asp:TextBox ID="Tsaddress" runat="server"  SkinID="TextBoxNormal" 
                        Width="510px" BackColor="Cornsilk"></asp:TextBox>&nbsp;</td>
            </tr>
        </table>       			
</div>
    <br />
    <asp:Button ID="Btnsedit" runat="server"  OnClick="BtnsEdit_Click" Text="修改"  SkinID="BtnNormal" />
    </div> 
    </form>
</body>
</html>


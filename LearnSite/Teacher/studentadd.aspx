<%@ Page Title="" Language="C#"  StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="studentadd.aspx.cs" Inherits="Teacher_studentadd" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
<div  class="placehold">
    学生基本信息<br />
    <div class="teacherdiv">    
         <table width="100%" style="border-right: WhiteSmoke 1px solid; border-top: WhiteSmoke 1px solid; border-left: WhiteSmoke 1px solid; border-bottom: WhiteSmoke 1px solid; text-align: left; font-size: 9pt;">
            <tr>
                <td style="width: 180px; height: 22px ">
                    &nbsp;学号
                    <asp:TextBox ID="Tsnum" runat="server" BorderColor="Gainsboro"
                        BorderStyle="Solid" BorderWidth="1px" Width="110px"  
                        ToolTip="自动生成！" ></asp:TextBox></td>
                <td style="width: 180px; height: 22px ">
                    &nbsp;姓名
                    <asp:TextBox ID="Tsname" runat="server" BackColor="Cornsilk" BorderColor="#E0E0E0"
                        BorderStyle="Solid" BorderWidth="1px" Width="110px"></asp:TextBox></td>
                <td style="width: 180px; height: 22px ">
                    &nbsp;入学
                    <asp:DropDownList ID="DDLyear" runat="server" Font-Size="9pt" Width="60px" 
                        BackColor="Cornsilk">
    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 180px; height: 22px">
                    &nbsp;年级
                    <asp:DropDownList ID="DDLgrade" runat="server" Font-Size="9pt" 
                Width="60px" BackColor="Cornsilk">
    </asp:DropDownList></td>
                <td style="width: 180px; height: 22px">
                    &nbsp;班级
                    <asp:DropDownList ID="DDLclass" runat="server" Font-Size="9pt" Width="60px" 
                        BackColor="Cornsilk">
    </asp:DropDownList></td>
                <td style="width: 180px; height: 22px">
                    &nbsp;主任
                    <asp:TextBox ID="Tsheadtheacher" runat="server" BackColor="Cornsilk" BorderColor="#E0E0E0"
                        BorderStyle="Solid" BorderWidth="1px" Width="110px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 180px; height: 22px">
                    &nbsp;密码
                    <asp:TextBox ID="Tspwd" runat="server" BackColor="White" BorderColor="Gainsboro"
                        BorderStyle="Solid" BorderWidth="1px" Width="110px" ReadOnly="True" ToolTip="密码不可修改！">12345</asp:TextBox></td>
                <td style="width: 180px; height: 22px">
                    &nbsp;性别
                    <asp:DropDownList ID="DDLsex" runat="server" Font-Size="9pt" Width="60px" 
                        BackColor="Cornsilk">
    </asp:DropDownList></td>
                <td style="width: 180px; height: 22px">
                    &nbsp;父母 <asp:TextBox ID="Tsparents" runat="server" BackColor="Cornsilk" BorderColor="#E0E0E0"
                        BorderStyle="Solid" BorderWidth="1px" Width="110px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 180px; height: 22px">
                    &nbsp;表现 <asp:TextBox ID="Tsattitude" runat="server" BackColor="White" BorderColor="Gainsboro"
                        BorderStyle="Solid" BorderWidth="1px" Width="110px"   ReadOnly="True" ToolTip="表现不可修改！">0</asp:TextBox></td>
                <td style="width: 180px; height: 22px">
                    &nbsp;成绩
                    <asp:TextBox ID="Tsscore" runat="server" BackColor="White" BorderColor="Gainsboro"
                        BorderStyle="Solid" BorderWidth="1px" Width="110px" ReadOnly="True" ToolTip="成绩不可修改！">0</asp:TextBox></td>
                <td style="width: 180px; height: 22px">
                    &nbsp;电话 <asp:TextBox ID="Tsphone" runat="server" BackColor="Cornsilk" BorderColor="#E0E0E0"
                        BorderStyle="Solid" BorderWidth="1px" Width="110px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="height: 22px" colspan="3">
                    &nbsp;地址 
                    <asp:TextBox ID="Tsaddress" runat="server" BackColor="Cornsilk" BorderColor="#E0E0E0"
                        BorderStyle="Solid" BorderWidth="1px" Width="508px"></asp:TextBox>&nbsp;</td>
            </tr>
        </table>       			
</div>
    <asp:Label ID="Labelmsg" runat="server"></asp:Label>
    <br />
    <asp:Button ID="Btnadd" runat="server"  OnClick="Btnadd_Click" Text="添加" SkinID="BtnNormal" />
    <br />
    </div> 
    </form>
</body>
</html>


<%@ Page Title="" Language="C#" StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="studentshow.aspx.cs" Inherits="Teacher_studentshow" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
<div  class="placehold">
    学生基本信息
    <div >     
    <asp:Repeater ID="Repeater1" runat="server">   
    <ItemTemplate>   	
    <div class="teacherdiv">
    <table width="100%" style="border-right: WhiteSmoke 1px solid; border-top: WhiteSmoke 1px solid; border-left: WhiteSmoke 1px solid; border-bottom: WhiteSmoke 1px solid; text-align: left; font-size: 9pt;">
            <tr>                                        
                <td style="width: 160px; height: 22px ">
                    姓名：<%#Eval("Sname")%>
                </td>
                <td style="width: 160px; height: 22px ">
                    </td>
                <td style="width: 120px; height: 22px ; text-align:right">
                    <asp:LinkButton ID="LinkButton1" runat="server"  Font-Size="9pt"  ForeColor="Black" Font-Underline="false"  OnClick="LinkEdit_Click" 
                     ToolTip="修改信息" BackColor="WhiteSmoke" BorderColor="Gainsboro" BorderStyle="Solid" BorderWidth="1px">修改</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 160px; height: 22px ">
                    编号：<%#Eval("Sid")%>
                </td>
                <td style="width: 160px; height: 22px ">
                    学号：<%#Eval("Snum")%></td>
                <td style="width: 120px; height: 22px ">
                    入学：<%#Eval("Syear")%>
                </td>
            </tr>
            <tr>
                <td style="width: 160px; height: 22px">
                    年级：<%#Eval("Sgrade")%></td>
                <td style="width: 160px; height: 22px">
                    班级：<%#Eval("Sclass")%></td>
                <td style="width: 120px; height: 22px">
                    主任：<%#Eval("Sheadtheacher")%></td>
            </tr>
            <tr>
                <td style="width: 160px; height: 22px">
                    密码：<%#Eval("Spwd")%></td>
                <td style="width: 160px; height: 22px">
                    性别：<%#Eval("Sex")%></td>
                <td style="width: 120px; height: 22px">
                    父母：<%#  Eval("Sparents")%></td>    
            </tr>
            <tr>
                <td style="width: 160px; height: 22px">
                    表现：<%#Eval("Sattitude")%></td>
                <td style="width: 160px; height: 22px">
                    成绩：<%#Eval("Sscore")%></td>
                <td style="width: 120px; height: 22px">
                    电话：<%#Eval("Sphone")%></td>
            </tr>
            <tr>
                <td style="height: 22px" colspan="3">
                    地址：<%#Eval("Saddress")%></td>
            </tr>
        </table> 
        <div>				
		</ItemTemplate>
 </asp:Repeater>
    </div>
</div>
    </form>
</body>
</html>


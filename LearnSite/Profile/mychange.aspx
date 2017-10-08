<%@ Page Title="" Language="C#" MasterPageFile="~/Profile/Pf.master"  StylesheetTheme="Student" AutoEventWireup="true" CodeFile="mychange.aspx.cs" Inherits="Profile_mychange" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cstu" Runat="Server">
<div>
<br />
<div style="margin: auto; border: 1px solid #E3E3E3; width: 500px" >
    <br />
    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/profile.gif" />
    &nbsp;
    学生基本信息<br />
    <br />
        <asp:Repeater ID="Repeater1" runat="server">   
    <ItemTemplate>
    <table width="480px" style="text-align: left; font-size: 9pt;">
            <tr>                                        
                <td style="width: 160px; height: 22px ">
                    </td>
                <td style="width: 160px; height: 22px ">
                     姓名：<%#Eval("Sname")%></td>
                <td style="width: 120px; height: 22px ; text-align:right">
                    </td>
            </tr>
            <tr>
                <td style="width: 160px; height: 22px ">
                    编号：<%#Eval("Sid")%></td>
                <td style="width: 160px; height: 22px ">
                    学号：<%#Eval("Snum")%></td>
                <td style="width: 120px; height: 22px ">
                    入学：<%#Eval("Syear")%></td>
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
                    密码：*********</td>
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
		</ItemTemplate>
 </asp:Repeater>
    <br />		
</div>
<br />
</div>
</asp:Content>


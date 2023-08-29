<%@ page title="" language="C#" masterpagefile="~/profile/Pf.master" stylesheettheme="Student" autoeventwireup="true" inherits="Profile_mychange, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cstu" Runat="Server">
<div>
<br />
<div style="margin: auto; border: 1px solid #E3E3E3; width: 600px" >
    <br />
    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/profile.gif" />
    &nbsp;
    学生基本信息<br />
    <br />
        <asp:Repeater ID="Repeater1" runat="server">   
    <ItemTemplate>
    <table width="580px" style="text-align: left; font-size: 11pt;">
            <tr>
                <td style="width: 160px; height: 22px ">
                    姓名：<%#Eval("Sname")%></td>
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
                    班主任：<%#Eval("Sheadtheacher")%></td>
            </tr>
            <tr>
                <td style="width: 160px; height: 22px">
                    密码：*********</td>
                <td style="width: 160px; height: 22px">
                    性别：<%#Eval("Sex")%></td>
                <td style="width: 160px; height: 22px">
                    成绩：<%#Eval("Sscore")%></td>   
            </tr>
        </table> 	
		</ItemTemplate>
 </asp:Repeater>
    <br />		
</div>
<br />
</div>
</asp:Content>


﻿<%@ page title="" language="C#" masterpagefile="~/manager/Manage.master" autoeventwireup="true" inherits="Manager_upgrade, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div class="manageplace" >
        <div style="border: 1px solid #CCCCCC; width: 500px;  margin: auto; ">
            <div style="border: 1px solid #EEEEEE; height: 18px; background-color: #EEEEEE;  margin: auto;">
                学年升班</div>
            <br />
            <div style="border: 1px solid #E7E7E7; width: 480px; text-align: left; background-color: #EAEAEA;  margin: auto;">
                <br />
&nbsp;&nbsp;&nbsp; <b>注意事项</b>：班级设置中的班级列表&nbsp;必须为全校完整班级列表，以防缺班而误删升上来的班级，谢谢！！<br />
                &nbsp;&nbsp;&nbsp;
                <br />
                &nbsp;&nbsp;&nbsp; <b>升班效果</b>：学生表所有年级都升一&nbsp;年，然后在学生表中删除班级表中不存
&nbsp;在班级的学生。如果班级未设置，则学
&nbsp;年升班按钮失效。<br />
                <br />
&nbsp; <strong>&nbsp;意外处理</strong>：处理前先进数据备份菜单中备份数据库，如果高年级的班级数缺少，则可以在班级列表中手动添加缺少的班级，不影响数据。<br />
                <br />
               <div style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 11pt; font-weight: bold; text-align: center">
                    注意：请学年升班后再新生导入！</div>
                <br />
            </div>
            <br />
            <asp:TextBox ID="Textthisyear" runat="server" 
                BorderStyle="None" Width="90px" ReadOnly="True"></asp:TextBox>
            <br />
            <br />
                    <asp:Button ID="Btnupgrade" runat="server" BackColor="#E6E6E6" 
                        BorderColor="#D4D4D4" BorderWidth="1px" Font-Names="Arial" 
                Font-Size="11pt" Text="学年升班" Width="80px" Height="20px" 
                onclick="Btnupgrade_Click" />
                    <br />
            <br />
            <div id="Loading" style=" display:none ;text-align: center; font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 11pt; color: #FF0000;">
            <asp:Image ID="Image2" runat="server" ImageUrl="~/images/load2.gif" />
            <input id="Textcmd" style="border-style: none" type="text" /></div>
            <br />
        </div>
        <br />
        <br />
        <asp:Label ID="Labelmsg" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <br />
        <br />
        <br />
</div>
</asp:Content>


<%@ page language="C#" autoeventwireup="true" inherits="UpGrade, LearnSite" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LearnSite信息技术学习平台数据库更新页面</title>
        <link rel="stylesheet" type="text/css" href="../App_Themes/Teacher/StyleSheet.css" />
    <style type="text/css">
        .style1
        {
            width: 100%;
            top: 0px;
            left: 0px;
            position: absolute;
        }
        .style2
        {
            font-family: 宋体, Arial, Helvetica, sans-serif;
            font-size: 11pt;
        }
        .style3
        {
            color: #0033CC;
        }
    </style>
</head>
<body>
       <form id="form1" runat="server" > 
       <div    class="style1">       
        <div style="background-color: #9EA9B1"  >            
            <asp:Image ID="Imagelogo" runat="server" ImageUrl="~/images/learnsite.gif"  ToolTip = "信息技术教学平台 LearnSite &#13;Powered By Asp.net2.0+Sql2005Express &#13;温州水乡设计编写 编程平台：Visual Studio 2008 C#" Height="24px" />
        </div>
        <div class="style2" style="text-align: center;">
           <br />
            <div style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: medium; font-weight: bold">
                <br />
                <br />
             从LearnSite1.03(或1.04、1.05、1.06、1.07、1.08、1.09)到1.10数据库升级更新、以及1.10++更新
            </div>
            <br />
            <br />
            自动判断表和字段是否存在，如果不存在则更新升级<br />
            <br />
            <br />
            <br />
            说明：全新安装的平台也会出现数据库更新页面，是因为要导入en.xls中的英语词典，用于指法练习中的单词检索<br />
            <br />
            ###<span class="style3">更新前请注意备份好数据库，以防万一</span>###<br />
            <br />
            <br />
            <asp:Button ID="Btnupgrade" runat="server" Font-Size="9pt" Text="执行更新" 
                onclick="Btnupgrade_Click" Height="24px" Width="100px" />
            <br />
            <br />
            <asp:Button ID="BtnCreateTable" runat="server" onclick="BtnCreateTable_Click" 
                Text="创建数据表" />
            <br />
            <br />
            <asp:Label ID="Labelmsg" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            <br />
            <br />
            <div style="margin: auto; width: 300px; font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 11pt;">            
            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center" BackColor="#F7C2A6" 
                    Visible="False">
                <br />
                数据库服务器名称：<asp:TextBox ID="TextBoxSqlServer" runat="server" 
                    BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Height="20px"></asp:TextBox>                
                <br />
            <br />
                你的数据库名称&nbsp; ：<asp:TextBox ID="TextBoxDbName" runat="server" BorderColor="Silver" 
                    BorderStyle="Solid" BorderWidth="1px" Height="20px"></asp:TextBox>
                <br />
                <br />
                你的数据库用户&nbsp; ：<asp:TextBox 
                    ID="TextBoxDbUser" runat="server" BorderColor="Silver" BorderStyle="Solid" 
                    BorderWidth="1px" Height="20px" ToolTip="默认为sa，以便管理员后台备份数据库，否则备份不了！">sa</asp:TextBox>                
                <br />
                <br />
                你的数据库密码&nbsp; ：<asp:TextBox 
                    ID="TextBoxDbPwd" runat="server" BorderColor="Silver" BorderStyle="Solid" 
                    BorderWidth="1px" Height="20px" ToolTip="默认为sa"></asp:TextBox>
               
                <br />
                <br />
                <asp:Button ID="Buttonedit" runat="server" Font-Size="9pt" Height="24px" 
                    onclick="Buttonedit_Click" Text="修改" Width="100px" Visible="False" />
                <br />
                <br />
            <br />
            </asp:Panel>
            </div>
            <br />
            <br />
            </div>
            
        </div> 
    </form>
</body>
</html>

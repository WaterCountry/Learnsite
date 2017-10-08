<%@ Page Title="" Language="C#" MasterPageFile="~/Manager/Manage.master" AutoEventWireup="true" CodeFile="setting.aspx.cs" Inherits="Manager_setting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div class="manageplace" >
        <div class="settingdiv">
            <div style="background-color: #EEEEEE; height: 18px;">当前设置</div>
            <br />
            <div  class="setting" >
            <br />
                    &nbsp;网站名称设置：<asp:TextBox ID="TextBoxsite" runat="server" BorderColor="#CCCCCC" 
                BorderStyle="Solid" BorderWidth="1px" Width="200px"></asp:TextBox>
&nbsp;<asp:Button ID="Buttonsite" runat="server" Font-Size="9pt" Height="20px" Text="修改" 
               BorderColor="Silver" BorderStyle="Solid"  BorderWidth="1px"   BackColor="#E8E8E8"  
               onclick="Buttonsite_Click" />
                <br />
                <br />
                &nbsp;学生登录方式：<asp:DropDownList ID="DDLLoginMode" runat="server" Font-Size="9pt" 
                    EnableTheming="True" AutoPostBack="True" 
            onselectedindexchanged="DDLLoginMode_SelectedIndexChanged" ToolTip="选择学生登录方式">
                <asp:ListItem Value="0">个人密码</asp:ListItem>
                <asp:ListItem Value="1">班级密码</asp:ListItem>
        </asp:DropDownList>
                <br />
                <br />
                &nbsp;账号登录限制：<asp:CheckBox ID="CheckBoxSingleLogin" runat="server" AutoPostBack="True" 
                        Font-Size="9pt" oncheckedchanged="CheckBoxSingleLogin_CheckedChanged" 
                    Text="是否设置一个账号只能在一台电脑登录平台" ToolTip="说明：选中则一个学生账号不能在多台电脑登录同个平台！" />
                <br />
                <br />
                    &nbsp;当前学期设置：<asp:DropDownList ID="DDLterm" runat="server" Font-Size="9pt" 
                    EnableTheming="True" AutoPostBack="True" 
            onselectedindexchanged="DDLterm_SelectedIndexChanged" ToolTip="选择当前学期">
                <asp:ListItem Value="1">第一学期</asp:ListItem>
                <asp:ListItem Value="2">第二学期</asp:ListItem>
        </asp:DropDownList>
            <br />
            <br />
                &nbsp;资源下载限制：<asp:CheckBox ID="CheckBoxDownCan" runat="server" AutoPostBack="True" 
                        Font-Size="9pt" oncheckedchanged="CheckBoxDownCan_CheckedChanged" Text="是否限制下载" />
                <br />
                <br />
                &nbsp;资源下载时间：<asp:DropDownList ID="DDLDownTime" runat="server" AutoPostBack="True" Font-Size="9pt" 
                        onselectedindexchanged="DDLDownTime_SelectedIndexChanged">
                        <asp:ListItem Value="10">10分钟</asp:ListItem>
                        <asp:ListItem Value="20">20分钟</asp:ListItem>
                        <asp:ListItem Value="30">30分钟</asp:ListItem>
                        <asp:ListItem Value="40">40分钟</asp:ListItem>
                        <asp:ListItem Value="50">50分钟</asp:ListItem>
                        <asp:ListItem Value="60">60分钟</asp:ListItem>
                    </asp:DropDownList>
                之后可下载<br />
            <br />
                &nbsp;学生网页投票：<asp:CheckBox ID="CheckBoxweblimit" runat="server" Text="是否限制投票" 
                    oncheckedchanged="CheckBoxweblimit_CheckedChanged" AutoPostBack="True" />
            <br />
            <br />
            &nbsp;网页内容类型：<asp:TextBox ID="TextBoxtype" runat="server" BorderColor="#CCCCCC" 
                BorderStyle="Solid" BorderWidth="1px" Width="200px" ToolTip="设置允许的网页内容类型，分隔符&quot;|&quot;"></asp:TextBox>
&nbsp;<asp:Button ID="Buttontype" runat="server" Font-Size="9pt" Height="20px" Text="修改" 
               BorderColor="Silver" BorderStyle="Solid"  BorderWidth="1px"   BackColor="#E8E8E8" 
                    onclick="Buttontype_Click" ToolTip="注意前后都不加分隔符&quot;|&quot;" />
            <br />
            <br />
            &nbsp;网页目录名称：<asp:TextBox ID="TextBoxdir" runat="server" BorderColor="#CCCCCC" 
                BorderStyle="Solid" BorderWidth="1px" Width="200px" ToolTip="设置允许上传的网页目录名称，分隔符&quot;|&quot;"></asp:TextBox>
&nbsp;<asp:Button ID="Buttondir" runat="server" Font-Size="9pt" Height="20px" Text="修改" 
               BorderColor="Silver" BorderStyle="Solid"  BorderWidth="1px"   BackColor="#E8E8E8" 
                    onclick="Buttondir_Click" ToolTip="注意前后都不加分隔符&quot;|&quot;" />
            <br />
            <br />
                &nbsp;作品查看时间：<asp:DropDownList ID="DDLworkdowntime" runat="server" Font-Size="9pt" 
                    EnableTheming="True" AutoPostBack="True" 
            onselectedindexchanged="DDLworkdowntime_SelectedIndexChanged" ToolTip="选择查看天数">
                <asp:ListItem>0</asp:ListItem>
                <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>6</asp:ListItem>
                    <asp:ListItem>7</asp:ListItem>
                    <asp:ListItem>8</asp:ListItem>
                    <asp:ListItem>9</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>11</asp:ListItem>
                    <asp:ListItem>12</asp:ListItem>
        </asp:DropDownList>
                天后可以查看<br />
                <br />
                &nbsp;作品提交限制：<asp:CheckBox ID="CheckBoxWorkIp" runat="server" AutoPostBack="True" 
                        Font-Size="9pt" oncheckedchanged="CheckBoxWorkIp_CheckedChanged" 
                    Text="是否对同班作品提交进行Ip限制" ToolTip="同班一个Ip限制提交一份作品" />
                <br />
                <br />
                &nbsp;Cookies失效设置：<asp:DropDownList ID="DDLCookiesPeriod" runat="server" 
                    AutoPostBack="True" Font-Size="9pt" 
                        onselectedindexchanged="DDLCookiesPeriod_SelectedIndexChanged">
                        <asp:ListItem Value="0">关闭失效</asp:ListItem>
                        <asp:ListItem Value="1">45分钟</asp:ListItem>
                        <asp:ListItem Value="2">1小时</asp:ListItem>
                        <asp:ListItem Value="3">3小时</asp:ListItem>
                        <asp:ListItem Value="4">5小时</asp:ListItem>
                        <asp:ListItem Value="5">永久</asp:ListItem>
                    </asp:DropDownList>
                <br />
                <br />
                <div>
&nbsp;Cookies前缀设置：<asp:Label ID="LabelCookiesFix" runat="server" Width="160px" 
                    BorderColor="#E1E1E1" BorderWidth="1px"></asp:Label>
                &nbsp;<asp:Button ID="ButtonFix" runat="server" Font-Size="9pt" Height="20px" Text="生成" 
               BorderColor="Silver" BorderStyle="Solid"  BorderWidth="1px"   
                    BackColor="#E8E8E8" onclick="ButtonFix_Click"  />
                <br />
                </div>
                <br />
                &nbsp;&nbsp;全部学案收回隐藏：<asp:Button ID="Btnpublish" runat="server" Font-Size="9pt" 
                    Height="20px" Text="一键收回" 
               BorderColor="Silver" BorderStyle="Solid"  BorderWidth="1px"   BackColor="#E8E8E8" 
                    onclick="Btnpublish_Click" ToolTip="收回的学案只是在学生界面不显示，教师界面仍显示并可再设置成发布状态" 
                    Width="80px" />
                <br />
                <br />
                &nbsp;作品上传控件选择：<asp:DropDownList ID="DDLUploadMode" runat="server" 
                    AutoPostBack="True" Font-Size="9pt" 
                    onselectedindexchanged="DDLUploadMode_SelectedIndexChanged">
                    <asp:ListItem Value="0">Swfupload上传控件</asp:ListItem>
                    <asp:ListItem Value="1">普通无刷新方式上传</asp:ListItem>
                    </asp:DropDownList>
                <br />
                <br />
&nbsp;ftp服务端口号配置：<asp:DropDownList ID="DDLftpport" runat="server" Font-Size="9pt" 
                    EnableTheming="True" AutoPostBack="True" 
            onselectedindexchanged="DDLftpport_SelectedIndexChanged" Width="50px">
                <asp:ListItem Selected="True">21</asp:ListItem>
                <asp:ListItem>22</asp:ListItem>
                    <asp:ListItem>23</asp:ListItem>
                    <asp:ListItem>24</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
        </asp:DropDownList>
                <br />
                <br />
&nbsp;教师平台登录限制：<asp:CheckBox ID="CheckBoxLogin" runat="server" 
                    oncheckedchanged="CheckBoxLogin_CheckedChanged" Text="限制为跟服务器同网段才能登录" 
                    AutoPostBack="True" />
                <asp:Image ID="ImageLogin" runat="server" ImageUrl="~/Images/green.gif" />
                <br />
                <br />
                </div>
            <br />
            <asp:Label ID="Labelmsg" runat="server" ForeColor="Red" ></asp:Label>
            <br />
            <br />
         </div>
        <br />

</div>
</asp:Content>


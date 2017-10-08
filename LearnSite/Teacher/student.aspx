<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="student.aspx.cs" Inherits="Teacher_student" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div  class="placehold">
    <div  class="chead">
            学生选择：<asp:DropDownList ID="DDLgrade" runat="server" Font-Size="9pt" 
            Width="50px" EnableTheming="True" AutoPostBack="True" 
                onselectedindexchanged="DDLgrade_SelectedIndexChanged">
        </asp:DropDownList>
            年级 
            <asp:DropDownList ID="DDLclass" runat="server" Font-Size="9pt" 
            Width="50px" EnableTheming="True" AutoPostBack="True" 
                onselectedindexchanged="DDLclass_SelectedIndexChanged">
        </asp:DropDownList>
            班级
            <asp:Label ID="Label1" runat="server" Width="360px" Height="16px"></asp:Label>
            <asp:HyperLink ID="HkaddStu" runat="server" SkinID="HyperLinkBtn">添加学生</asp:HyperLink>
                &nbsp;
                    </div>
                    <div class="centerdiv">
            <asp:GridView ID="GVStudent" runat="server" AutoGenerateColumns="False" Width="100%" 
                            CellPadding="3" PageSize="15" 
             OnRowDataBound="GVStudent_RowDataBound" EnableModelValidation="True" DataKeyNames="Sid" onrowcommand="GVStudent_RowCommand"
              ForeColor="#111111" GridLines="None" AllowPaging="True" 
                            onpageindexchanging="GVStudent_PageIndexChanging" >
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField HeaderText="序号" />
                <asp:BoundField DataField="Snum" HeaderText="学号">
                    <ControlStyle Width="30px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="密码">
                    <ItemTemplate>
                        <asp:Label ID="Labelpwd" runat="server" Text='******' ToolTip='<%# Bind("Spwd") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                            CommandArgument='<%# Eval("Sid") %>' CommandName="ChangePwd" 
                            ImageUrl="~/Images/refresh.gif" Text="更新" ToolTip="自动更新密码" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Sgrade" HeaderText="年级" />
                <asp:BoundField DataField="Sclass" HeaderText="班级" />
                <asp:TemplateField HeaderText="姓名">
                    <ItemTemplate>
                        <asp:HyperLink ID="Hlname" runat="server"  
                            Text='<%# Eval("Sname") %>' ToolTip='<%# Eval("Sid") %>' ForeColor="Blue"></asp:HyperLink>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:BoundField DataField="Sex" HeaderText="性别" />
                <asp:TemplateField ShowHeader="False" HeaderText="小组">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageBtnGroup" runat="server" CausesValidation="False" 
                            CommandArgument='<%# Eval("Sid") %>' CommandName="ChangeGroup" 
                            ImageUrl="~/Images/gcard.gif"  />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="组号" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkBtnQuit" runat="server" CausesValidation="false" 
                            CommandArgument='<%# Eval("Sid") %>'  CommandName="QuitGroup" Text='<%# Eval("Sgroup") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField DataNavigateUrlFields="Snum" 
                    DataNavigateUrlFormatString="studentwork.aspx?Snum={0}" DataTextField="Sscore" 
                    HeaderText="成绩" Target="_blank" />
                <asp:HyperLinkField DataNavigateUrlFields="Snum" 
                    DataNavigateUrlFormatString="~/Workshow/studentworks.aspx?Snum={0}"  Text="浏览"
                    HeaderText="作品" Target="_blank" />
                <asp:BoundField DataField="Sattitude" HeaderText="表现" />
                <asp:HyperLinkField DataNavigateUrlFields="Sid,Sgrade,Sclass" DataNavigateUrlFormatString="StudentDel.aspx?Sid={0}&amp;Sgrade={1}&amp;Sclass={2}"
                    Text="删除" />
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="LabelSleader" runat="server" Text='<%# Bind("Sleader") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle BackColor="#EFEFEF" ForeColor="#111111" HorizontalAlign="Center" />
                            <pagertemplate>
                                <div style="width:100%; height:13px; text-align:right">
                                    第<asp:Label ID="lblPageIndex" runat="server" 
                                        text="<%# ((GridView)Container.Parent.Parent).PageIndex + 1  %>" />
                                    页 共<asp:Label ID="lblPageCount" runat="server" 
                                        text="<%# ((GridView)Container.Parent.Parent).PageCount  %>" />
                                    页 
                                    <asp:LinkButton ID="btnFirst" runat="server" causesvalidation="False" 
                                        commandargument="First" commandname="Page" Font-Underline="False" 
                                        ForeColor="Black" text="首页" />
                                    <asp:LinkButton ID="btnPrev" runat="server" causesvalidation="False" 
                                        commandargument="Prev" commandname="Page" Font-Underline="False" 
                                        ForeColor="Black" text="上一页" />
                                    <asp:LinkButton ID="btnNext" runat="server" causesvalidation="False" 
                                        commandargument="Next" commandname="Page" Font-Underline="False" 
                                        ForeColor="Black" text="下一页" />
                                    <asp:LinkButton ID="btnLast" runat="server" causesvalidation="False" 
                                        commandargument="Last" commandname="Page" Font-Underline="False" 
                                        ForeColor="Black" text="尾页" />
                                </div>
                            </pagertemplate>
                            <FooterStyle BackColor="#E7E7E7" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#9EA9B1" Font-Bold="True" ForeColor="#111111" />
                            <PagerStyle BackColor="#EFEFEF" ForeColor="#111111" HorizontalAlign="Center" />
                            <RowStyle BackColor="#E7E7E7" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />            
        </asp:GridView>
                    </div>
                    <br />
            <asp:Button ID="BtnSpwdInit" runat="server"  OnClick="BtnSpwdInit_Click" 
            Text="初始化密码"  SkinID="BtnNormal" ToolTip="将本班所有学生的密码初始为12345" 
            Width="80px" />
        &nbsp;将学生密码为<asp:TextBox 
            ID="TextBoxPwd" runat="server" SkinID="TextBoxNum" 
            BackColor="#FFFFCC" Width="50px"></asp:TextBox>
            &nbsp;<asp:Button ID="BtnSpell" runat="server"  OnClick="BtnSpell_Click" 
            Text="转拼音缩写"  SkinID="BtnNormal" ToolTip="将当前为原初始化密码的学生密码转换为其姓名拼音缩写" 
            Width="80px" />
        &nbsp;小组上限：<asp:DropDownList ID="DDLgroupMax" runat="server" Font-Size="9pt" 
            Width="40px" EnableTheming="True" AutoPostBack="True" 
                onselectedindexchanged="DDLgroupMax_SelectedIndexChanged">
            <asp:ListItem>0</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            <asp:ListItem>5</asp:ListItem>
            <asp:ListItem Selected="True">6</asp:ListItem>
            <asp:ListItem>7</asp:ListItem>
            <asp:ListItem>8</asp:ListItem>
            <asp:ListItem></asp:ListItem>
        </asp:DropDownList>
            &nbsp;<asp:Button ID="BtnNoGroup" runat="server"  OnClick="BtnNoGroup_Click" 
            Text="解除分组"  SkinID="BtnNormal" ToolTip="一键将本班所有学生解除分组" 
            Width="60px" />
        &nbsp;
                    <asp:Button ID="Btngroups" runat="server" BackColor="#E6E6E6" 
                        BorderColor="#D4D4D4" BorderWidth="1px" Font-Names="Arial" 
                Font-Size="9pt" Text="分组管理" Width="60px" Height="20px" 
                onclick="Btngroups_Click" />
        &nbsp;
            <asp:Button ID="BtnExcel" runat="server"  OnClick="BtnExcel_Click" 
            Text="导出学生"  SkinID="BtnNormal" ToolTip="将所有学生的基本信息导出Excel" 
            Width="60px" />
        &nbsp;
                    <asp:Button ID="BtnRevive" runat="server" BackColor="#E6E6E6" 
                        BorderColor="#D4D4D4" BorderWidth="1px" Font-Names="Arial" 
                Font-Size="9pt" Text="恢复学生" Width="60px" Height="20px" 
                onclick="BtnRevive_Click" />
        <br />
        <br />
        <div style="margin: auto; width: 660px; background-color: #DDDDDD; text-align: center;">
            <asp:CheckBox ID="Ckreg" runat="server" Text="是否允许在线注册" oncheckedchanged="Ckreg_CheckedChanged" 
                AutoPostBack="True"  />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; 学生个人资料权限：
            <asp:CheckBox ID="Ckclass" runat="server" Text="班级修改" 
                ToolTip="允许学生修改个人资料中的班级" oncheckedchanged="Ckclass_CheckedChanged" 
                AutoPostBack="True" />
            &nbsp;
            <asp:CheckBox ID="Ckphoto" runat="server" Text="相片修改" 
                ToolTip="允许学生修改个人资料中的相片" oncheckedchanged="Ckphoto_CheckedChanged" 
                AutoPostBack="True" />
            &nbsp;
            <asp:CheckBox ID="Cksex" runat="server" Text="性别修改" ToolTip="允许学生修改个人资料中的性别" 
                oncheckedchanged="Cksex_CheckedChanged" AutoPostBack="True" />
            &nbsp;
            <asp:CheckBox ID="Ckname" runat="server" Text="姓名修改" ToolTip="允许学生修改个人资料中的姓名" 
                oncheckedchanged="Ckname_CheckedChanged" AutoPostBack="True" />
        </div>
        <br />
        <asp:Label ID="Labelmsg" runat="server"></asp:Label>
        <br />
        <br />        
        <link href="../Js/tinybox.css" rel="stylesheet" type="text/css" />
        <script src="../Js/tinybox.js" type="text/javascript"></script>
        <script type ="text/javascript" >
            function stuShow(d, g, c) {
                var urlat = "../Teacher/StudentShow.aspx?Sid=" + d + "&Sgrade=" + g + "&Sclass=" + c;
                TINY.box.show({ iframe: urlat, boxid: 'frameless', width: 700, height: 240, fixed: false, maskopacity: 40, closejs: function () { closeJS() } })
            }
            function stuAdd(g, c) {
                var urlad = "../Teacher/studentadd.aspx?Sgrade=" + g + "&Sclass=" + c;
                TINY.box.show({ iframe: urlad, boxid: 'frameless', width: 700, height: 240, fixed: false, maskopacity: 40, closejs: function () { closeJS() } })
            }
        </script>
        <br />
        <br />
    </div>
</asp:Content>


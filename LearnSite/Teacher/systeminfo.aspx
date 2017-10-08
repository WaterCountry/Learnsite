<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher" AutoEventWireup="true" enableViewStateMac="false" CodeFile="systeminfo.aspx.cs" Inherits="Teacher_systeminfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div  class="placehold">
    <center>
        <br />
        <table style="border: 1px solid #D8D8D8; width: 300px; font-size: 9pt;font-family: 宋体, Arial, Helvetica, sans-serif;" 
            cellpadding="5" cellspacing="0">
            <tr>
                <td style="background-color: #E6E6E6; font-weight: bold;" colspan="2" 
                    align="center">
                    <asp:HyperLink ID="HlSearching" runat="server" 
                        ImageUrl="~/Workshow/image/zoom.gif" NavigateUrl="~/Workshow/search.aspx" 
                        Target="_blank"></asp:HyperLink>
                    网站分析统计</td>
            </tr>
            <tr>
                <td style="width: 120px">
                    学案总数：</td>
                <td>
                    <asp:Label ID="Label15" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 120px">
                    作品总数：</td>
                <td>
                    <asp:Label ID="Label16" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 120px">
                    学生总数：</td>
                <td>
                    <asp:Label ID="Label17" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 120px">
                    签到次数：</td>
                <td>
                    <asp:Label ID="Label18" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 120px">
                    打字次数：</td>
                <td>
                    <asp:Label ID="Label19" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 120px">
                    资源总数：</td>
                <td>
                    <asp:Label ID="Label20" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
       &nbsp;<asp:HyperLink ID="HLcomputer" runat="server" 
            NavigateUrl="~/Teacher/computers.aspx" CssClass="HyperlinkLong" 
            EnableTheming="False" EnableViewState="False">机器名IP对应表</asp:HyperLink>
        &nbsp;&nbsp;
        <asp:HyperLink ID="HLmythware" runat="server" 
            NavigateUrl="~/Teacher/mythware.aspx" CssClass="HyperlinkLong" 
            EnableTheming="False" EnableViewState="False">极域班级模型</asp:HyperLink>
        <br />
        <br />
<table style="border: 1px solid #D8D8D8; font-size: 9pt; width: 600px;  text-align: left; font-family: 宋体, Arial, Helvetica, sans-serif; " 
            cellpadding="4" cellspacing="0">
            <tr>
                <td style="font-weight: bold; color: black; height: 16px; background-color: #E6E6E6; text-align: center;" 
                    colspan="4">
                    服务器状态</td>
            </tr>
            <tr>
                <td style="width: 90px" >
                    服务器IP：</td>
                <td style="width: 189px" >
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                <asp:Image ID="ImageLogin" runat="server" ImageUrl="~/Images/green.gif" />
                </td>
                <td style="width: 116px" >
                    .NET引擎版本：</td>
                <td >
                    <asp:Label ID="Label8" runat="server" ></asp:Label>
                                </td>
            </tr>
            <tr>
                <td style="width: 90px" >
                    服务器名称：</td>
                <td style="width: 189px" >
                    <asp:Label ID="Label2" runat="server"></asp:Label>
                </td>
                <td style="width: 116px" >
                    脚本超时时间：</td>
                <td >
                    <asp:Label ID="Label9" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 90px" >
                    操作系统：</td>
                <td style="width: 189px" >
                    <asp:Label ID="Label3" runat="server" ></asp:Label>
                </td>
                <td style="width: 116px" >
                    开机运行时长：</td>
                <td >
                    <asp:Label ID="Label10" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 90px" >
                    CPU数：</td>
                <td style="width: 189px" >
                    <asp:Label ID="Label4" runat="server" ></asp:Label>
                </td>
                <td style="width: 116px" >
                    进程开始时间：</td>
                <td >
                    <asp:Label ID="Label11" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 90px" >
                    CPU类型</td>
                <td style="width: 189px" >
                    <asp:Label ID="Label5" runat="server" ></asp:Label>
                </td>
                <td style="width: 116px" >
                    AspNet内存占用：</td>
                <td >
                    <asp:Label ID="Label12" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 90px" >
                    信息服务软件：</td>
                <td style="width: 189px" >
                    <asp:Label ID="Label7" runat="server" ></asp:Label>
                </td>
                <td style="width: 116px" >
                    AspNet CPU时间：</td>
                <td >
                    <asp:Label ID="Label13" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 90px" >
                    服务器区域语言</td>
                <td style="width: 189px" >
                    <asp:Label ID="Label21" runat="server" ></asp:Label>
                </td>
                <td style="width: 116px" >
                    AspNet当前线程数：</td>
                <td >
                    <asp:Label ID="Label14" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 90px" >
                    网站平台版本：</td>
                <td style="width: 189px" >
                    <asp:Label ID="Label6" runat="server" ></asp:Label>
                </td>
                <td style="width: 116px" >
                    Session总数：</td>
                <td >
                    <asp:Label ID="Label22" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 90px" >
                    全局变量数：</td>
                <td style="width: 189px" >
                    <asp:Label ID="Label23" runat="server" ></asp:Label>
                </td>
                <td style="width: 116px" >
                    网站异常记录：</td>
                <td >
        <asp:HyperLink ID="HLsitelog" runat="server" 
            NavigateUrl="~/Teacher/sitelog.aspx" BorderStyle="None" EnableTheming="False" 
                        EnableViewState="False"  Font-Underline="False" 
                        ForeColor="Black" Target="_blank" ToolTip="请及时向温州水乡回复修正！">查询</asp:HyperLink>
                </td>
            </tr>
            </table>
    <br />
    </center>
        </div>
</asp:Content>


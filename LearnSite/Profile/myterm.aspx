<%@ Page Title="" Language="C#" MasterPageFile="~/Profile/Pf.master"   StylesheetTheme="Student"  AutoEventWireup="true" CodeFile="myterm.aspx.cs" Inherits="Profile_myterm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cstu" Runat="Server">
<div>
    <br />
    <strong>我的学习成果</strong><br />
    <div style="text-align: center; margin: auto">
    <asp:DataList ID="DLterm" runat="server" HorizontalAlign="Center" RepeatColumns="2" 
            CellPadding="2">
        <ItemTemplate>
            <table style="border: 2px dotted #0066FF; width: 279px;">
                <tr>
                    <td colspan="3" style="border: 1px solid #6699FF">
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Tgrade") %>'></asp:Label>
                        年级 第<asp:Label ID="Label2" runat="server" Text='<%# Eval("Tterm") %>'></asp:Label>
                        学期 信息技术成绩单</td>
                </tr>
                <tr>
                    <td rowspan="2" style="border: 1px solid #6699FF; width: 130px;">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/term.png" />
                        <br />
                        <asp:Label ID="Label11" runat="server" Text='<%# Eval("Sname") %>'></asp:Label>
                        <br />
                        <br />
                        综合素质：<asp:Label ID="Label10" runat="server" Text='<%# Eval("Tape") %>'></asp:Label>
                        <br />
                    </td>
                    <td >
                        作品分：<br /> <br />测验分：<br /> <br />小组分：<br /> <br />讨论分：<br /> <br />常识分：<br /> <br />打字分：<br /> <br />表现分：</td>
                    <td >
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("Tscore") %>'></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="Label31" runat="server" Text='<%# Eval("Tvscore") %>'></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("Tgscore") %>'></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("Tpscore") %>'></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("Tquiz") %>'></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("Ttscore") %>'></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="Label8" runat="server" Text='<%# Eval("Tattitude") %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="border: 1px solid #6699FF">
                        总评分：</td>
                    <td style="border: 1px solid #6699FF">
                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("Tallscore") %>'></asp:Label>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:DataList>
    </div>
    <br />
    <br />
    <br />
    <br />
    </div>
</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/Student/Scm.master"  AutoEventWireup="true" CodeFile="summary.aspx.cs" Inherits="Student_summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cpcm" Runat="Server">
    <div  id="showcontent">
        <center>
            <table style="border: 1px double #CADEFD; font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt; text-align: left;">
                <tr>
                    <td colspan="3" style="width: 660px">
                        <strong>学案名称</strong>：<asp:Label ID="Label1" runat="server"></asp:Label>
                        <asp:ImageButton ID="BtnEdit" runat="server" ToolTip="提示" 
                       ImageUrl="~/Images/edit.gif" onclick="BtnEdit_Click" style="width: 16px" 
                            Enabled="False" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="border: 1px double #CADEFD;width: 660px;height: 28px;">
                        <strong>总结内容</strong>：<br /><br />
                        <div  id="contents" runat="server" 
                            style="padding: 2px; margin: 2px; border: thin dotted #9CCBF1; overflow: auto;"></div>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 220px;">
                        &nbsp;</td>
                    <td style="width: 220px;">
                        &nbsp;</td>
                    <td style="width: 220px;">
                        撰写日期：<asp:Label ID="Label6" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            </center>
</div>
</asp:Content>
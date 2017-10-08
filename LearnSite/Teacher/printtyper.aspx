<%@ Page Language="C#" AutoEventWireup="true" CodeFile="printtyper.aspx.cs" Inherits="Teacher_printtyper" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>    
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 0px; text-align: center; margin: 0px;">
    <div style="margin: 0px; padding: 0px; font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt;
    border-bottom-style: dotted; border-width: 2px; border-color: #C0C0C0; background-color: #E8F3FF">
        排行榜打印选择：<asp:DropDownList 
            ID="DDLtype" runat="server" 
            Font-Size="9pt" ToolTip="打字选择">
            <asp:ListItem Selected="True" Value="0">中文打字</asp:ListItem>
            <asp:ListItem Value="1">英文指法</asp:ListItem>
        </asp:DropDownList>
&nbsp;年级选择：<asp:DropDownList ID="DDLgrade" runat="server" 
            Font-Size="9pt" ToolTip="年级选择" Width="50px">
        </asp:DropDownList>
        &nbsp;人数选择：<asp:DropDownList ID="DDLtop" runat="server" 
            Font-Size="9pt" ToolTip="人数选择" Width="50px">
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            <asp:ListItem>5</asp:ListItem>
            <asp:ListItem>6</asp:ListItem>
            <asp:ListItem Selected="True">7</asp:ListItem>
            <asp:ListItem>8</asp:ListItem>
            <asp:ListItem>9</asp:ListItem>
            <asp:ListItem>10</asp:ListItem>
            <asp:ListItem>11</asp:ListItem>
            <asp:ListItem>12</asp:ListItem>
            <asp:ListItem>13</asp:ListItem>
            <asp:ListItem>14</asp:ListItem>
            <asp:ListItem>15</asp:ListItem>
            <asp:ListItem>16</asp:ListItem>
        </asp:DropDownList>
&nbsp;<asp:Button ID="Btnbrowse" runat="server" BackColor="#ECF5FF" 
            BorderColor="#E0E0E0" BorderStyle="Solid" BorderWidth="1px" Font-Size="9pt" 
            Text="显示效果" ToolTip="点击显示" onclick="Btnbrowse_Click" Height="18px" />
&nbsp;&nbsp;
        <input id="BtnPrintView" style="border: 1px solid #E0E0E0; font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt; background-color: #ECF5FF; width: 68px; height: 18px;" 
            type="button" value="打印" onclick="preview()" /></div>
    <div style="margin: auto; width: 600px">
    <!--startprint-->
    <div style="text-align: center">
    <asp:DataList ID="DataList_A4" runat="server" RepeatColumns="2" 
                    RepeatDirection="Horizontal" HorizontalAlign="Center" 
            onitemdatabound="DataList_A4_ItemDataBound">
                    <ItemTemplate>
                    <div style="margin: 2px; text-align: center; width: 290px;">
                        <asp:Image ID="Image1" runat="server"   Height="300px" Width="210px" /><br />
                        <div style="margin: auto; text-align: center; font-family: 黑体, Arial, Helvetica, sans-serif; font-size: 11pt;">
                        <table style="width:240px; ">
                            <tr>
                                <td>
                                    <asp:Label ID="sname" runat="server" Text='<%# Eval("Sname") %>' 
                                        Font-Bold="True" Font-Size="20pt" Font-Names="黑体"></asp:Label>                                        
                                    <asp:Label ID="psnum" runat="server" Text='<%# Eval("Psnum") %>' Visible="false"></asp:Label>
                                </td>
                                <td rowspan="2">
                                    <asp:Label ID="ps" runat="server" Text='<%# Eval("Pscore") %>' Font-Bold="True" 
                                        Font-Size="18pt" ForeColor="Red" Font-Names="黑体"></asp:Label>
                                    词/分钟</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="sgrade" runat="server" Text='<%# Eval("Sgrade") %>'></asp:Label>
                                    年级 <asp:Label ID="sclass" runat="server" Text='<%# Eval("Sclass") %>'></asp:Label>
                                    班</td>
                            </tr>
                        </table>
                        </div>
                        <br />
                        <br />
                    </div>
                    </ItemTemplate>
                </asp:DataList>
    </div>
    <!--endprint-->        
    </div>
    <br />
    <script language="javascript" type="text/javascript">
        function preview() {
            bdhtml = window.document.body.innerHTML;
            sprnstr = "<!--startprint-->";
            eprnstr = "<!--endprint-->";
            prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
            prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
            window.document.body.innerHTML = prnhtml;
            window.print();
        }
        </script>
    </div>
    </form>
</body>
</html>

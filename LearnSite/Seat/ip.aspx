<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ip.aspx.cs" Inherits="Seat_ip" %>

<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div style="margin: auto; text-align: center; width: 500px; font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt;">
        <b>电脑编号跟IP对应表</b><br />
        <br />
        请填写网段：<asp:TextBox ID="TextBoxIpGate" runat="server" BorderColor="Silver" 
            BorderStyle="Solid" BorderWidth="1px" Font-Size="9pt" Width="80px">192.168.0</asp:TextBox>
        &nbsp;IP范围从<asp:TextBox ID="TextBoxIpBegin" runat="server" BorderColor="Silver" 
            BorderStyle="Solid" BorderWidth="1px" Font-Size="9pt" Width="30px">11</asp:TextBox>
        到<asp:TextBox ID="TextBoxIpEnd" runat="server" BorderColor="Silver" 
            BorderStyle="Solid" BorderWidth="1px" Font-Size="9pt" Width="30px">50</asp:TextBox>
&nbsp;<asp:Button ID="ButtonIpAdd" runat="server" Font-Size="9pt" Text="根据范围创建IP列表" 
            ToolTip="点击后将清除原机房IP列表，并自动创建新IP列表" onclick="ButtonIp_Click" />
        <br />
        <br />
                <asp:GridView ID="GVip" runat="server" 
                    AutoGenerateColumns="False" BorderColor="#E7E7E7" BorderStyle="Solid" 
                    BorderWidth="1px" CellPadding="2" Font-Size="9pt" GridLines="None" 
            Width="400px" PageSize="15" DataKeyNames="Iid" EnableModelValidation="True" 
            onrowdatabound="GVip_RowDataBound" 
            onrowcancelingedit="GVip_RowCancelingEdit" onrowediting="GVip_RowEditing" 
            onrowupdating="GVip_RowUpdating" HorizontalAlign="Center" >
                    <Columns>
                        <asp:BoundField HeaderText="电脑编号" DataField="Inum" ReadOnly="True" >
                        <HeaderStyle Width="60px" />
                        <ItemStyle Width="60px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="本机房IP列表">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBoxIp" runat="server" Text='<%# Bind("Iip") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelIp" runat="server" Text='<%# Bind("Iip") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="160px" />
                            <ItemStyle Width="160px" />
                        </asp:TemplateField>
                         <asp:CommandField ShowEditButton="True" HeaderText="操作" />
                    </Columns>
                    <RowStyle BorderStyle="None" Font-Names="Arial" Font-Size="9pt" 
                        ForeColor="Black" Height="20px" />
                    <HeaderStyle BackColor="#EEEEEE" Font-Bold="False" Font-Names="Arial" 
                        Font-Size="9pt" />
                    <AlternatingRowStyle BackColor="#E7E7E7" />
                </asp:GridView>   
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Seat/getip.aspx" 
            Target="_blank">右健复制快捷方式将此超链接用极域用远程命令在学生机打开，显示IP后记录参考!</asp:HyperLink>
        <br />
        <hr style="border: 1px dashed #CCCCCC" />
        <br />
        请选择：<anthem:FileUpload 
            ID="FileUploadip" runat="server" Font-Size="9pt" />
&nbsp;<asp:Button ID="ButtonIpExcel" runat="server" Font-Size="9pt" Text="从Excel导入IP列表" 
            ToolTip="点击后将清除原机房IP列表，并自动创建新IP列表" onclick="ButtonIpExcel_Click" />
        <br />
        <br />
            <asp:Label ID="Labelmsg" runat="server" ForeColor="#000099"></asp:Label>
        <br />
        <br />
        Excel导入模板参考<br />
        <table style="margin: auto; width: 200px; ">
            <tr>
                <td style="border: 1px solid #999999">
                    电脑编号</td>
                <td style="border: 1px solid #999999">
                    IP列表</td>
            </tr>
            <tr>
                <td style="border: 1px solid #999999">
                    1</td>
                <td style="border: 1px solid #999999">
                    192.168.0.11</td>
            </tr>
            <tr>
                <td style="border: 1px solid #999999">
                    2</td>
                <td style="border: 1px solid #999999">
                    192.168.0.12</td>
            </tr>
        </table>
        <br />
    </div>
    </div>
    </form>
</body>
</html>

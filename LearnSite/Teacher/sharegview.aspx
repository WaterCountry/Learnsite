<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sharegview.aspx.cs" Inherits="Teacher_sharegview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style=" margin:0px;">
    <form id="form1" runat="server">
    <div style="text-align: center; font-size: 9pt;">
    <div>
        <asp:GridView ID="GVdisk" runat="server" AutoGenerateColumns="False" 
            BorderColor="#EEEEEE" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
            EnableModelValidation="True" GridLines="None" 
            HorizontalAlign="Center" onrowdatabound="GVdisk_RowDataBound" 
            Font-Names="Arial" Width="100%">
            <AlternatingRowStyle BackColor="#F4F5F1" />
            <Columns>
                <asp:BoundField DataField="Sgroup">
                <ItemStyle Width="30px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Sgtitle" HeaderText="小组名称" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="Sname" HeaderText="组长">
                <ItemStyle Width="50px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="网盘文档" >
                    <ItemTemplate>
                        <asp:DataList ID="DLfile" runat="server" RepeatColumns="0" 
                            RepeatDirection="Horizontal" RepeatLayout="Flow" 
                            onitemdatabound="DLfile_ItemDataBound">
                            <ItemTemplate>
                                <asp:Image ID="Imgtype" runat="server" ToolTip='<%# Eval("Kftpe") %>' />
                                <asp:HyperLink ID="Hylkfname" Text='<%# Eval("KfnameShort") %>' ToolTip='<%# Eval("Kfilename") %>' NavigateUrl='<%# Eval("Kfurl") %>' Target="_blank" runat="server"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:DataList>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="总计" >
                <ItemStyle Width="30px" />
                </asp:BoundField>
            </Columns>
            <HeaderStyle BackColor="#888888" />
        </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>

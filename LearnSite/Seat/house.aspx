<%@ Page Title="" Language="C#"  MasterPageFile="~/Manager/Manage.master" AutoEventWireup="true" CodeFile="house.aspx.cs" Inherits="Seat_house" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div>
    
        <br />
        <strong>机房布置</strong><br />
        <br />
        <br />
                <asp:GridView ID="GVHouse" runat="server" 
                    AutoGenerateColumns="False" BorderColor="#E7E7E7" BorderStyle="Solid" 
                    BorderWidth="1px" CellPadding="3" Font-Size="9pt" GridLines="None" Width="500px" 
                    onrowdatabound="GVHouse_RowDataBound" EnableModelValidation="True" 
            HorizontalAlign="Center" onrowcommand="GVHouse_RowCommand">
                    <Columns>
                        <asp:BoundField HeaderText="序号" />
                        <asp:BoundField DataField="Hname" HeaderText="机房名称" />
                        <asp:HyperLinkField DataNavigateUrlFields="Hid" 
                            DataNavigateUrlFormatString="computer.aspx?Hid={0}" HeaderText="电脑" 
                            Text="布置" Target="_blank" />
                        <asp:HyperLinkField DataNavigateUrlFields="Hid" 
                            DataNavigateUrlFormatString="ip.aspx?Hid={0}" HeaderText="IP表" 
                            Text="对应" Target="_blank" />
                        <asp:TemplateField ShowHeader="False" HeaderText="操作">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButtonDel" runat="server" CausesValidation="false" 
                                CommandArgument='<%# Bind("Hid") %>' CommandName="Del" Text="删除"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BorderStyle="None" Font-Names="Arial" Font-Size="9pt" 
                        ForeColor="Black" Height="24px" />
                    <HeaderStyle BackColor="#939CA2" Font-Bold="False" Font-Names="Arial" 
                        Font-Size="9pt" Height="24px" />
                    <AlternatingRowStyle BackColor="#E7E7E7" />
                </asp:GridView>
                <br />
                <br />
        机房名称：<asp:TextBox ID="TextBoxHname" runat="server" BorderColor="#CCCCCC" 
            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
&nbsp;<asp:Button ID="Buttonadd" runat="server" BackColor="#E6E6E6" 
        BorderColor="#D4D4D4" BorderWidth="1px" Font-Size="9pt" Text="添加" 
            onclick="Buttonadd_Click" Width="60px" />
        <br />
        <br />
        <br />
        <br />
        <asp:CheckBox ID="CkBox" runat="server" oncheckedchanged="CkBox_CheckedChanged" 
            Text="启用手工机房布置" AutoPostBack="True" />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    
    </div>
</asp:Content>


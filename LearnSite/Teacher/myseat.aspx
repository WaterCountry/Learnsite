<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" AutoEventWireup="true" CodeFile="myseat.aspx.cs" Inherits="Teacher_myseat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div>  
        <br />
        <asp:DropDownList ID="DDLhouse" runat="server" AutoPostBack="True" 
            Font-Size="9pt" Height="16px" 
            onselectedindexchanged="DDLhouse_SelectedIndexChanged" Width="180px">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="Labelnum" runat="server"></asp:Label>
        <br />
        <br />
        <asp:DataList ID="DataList1" runat="server"  Width="100%" RepeatColumns="6" 
            onitemdatabound="DataList1_ItemDataBound" CellPadding="3" CellSpacing="3">
            <ItemTemplate>
            <div style="padding: 2px; margin: 0px; background-color: #DDEEFF">
                <asp:Label ID="Labelpm" runat="server" Text='<%# Eval("Pmachine") %>' Font-Bold="True"></asp:Label>
                <br />
                <asp:Label ID="Labelpip" runat="server" Text='<%# Eval("Pip") %>'></asp:Label>
                <br />
                <asp:Label ID="Labelpx" runat="server" Text='<%# Eval("Px") %>'></asp:Label>,
                <asp:Label ID="Labelpy" runat="server" Text='<%# Eval("Py") %>'></asp:Label>
             </div>
            </ItemTemplate>
        </asp:DataList>     
    </div>
    <div>
        <br />
    </div>
</asp:Content>


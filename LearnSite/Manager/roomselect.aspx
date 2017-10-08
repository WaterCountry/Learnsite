<%@ Page Title="" Language="C#" MasterPageFile="~/Manager/Manage.master" AutoEventWireup="true" CodeFile="roomselect.aspx.cs" Inherits="Manager_roomselect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div class="manageplace" >
        <div  class="roomcreate">
        <div style="border: 1px solid #EEEEEE; width: 600px; background-color: #EEEEEE; height: 16px;  margin: auto;">
        班级选择</div>
                    <br />
                    <br />
                    <div style=" width: 552px; height: 13px;  margin: auto;">
                        <asp:Label ID="Labelnot" runat="server" 
             BackColor="WhiteSmoke" Width="12px" 
             Height="12px" BorderColor="#E4E4E4" BorderStyle="Solid" BorderWidth="1px"></asp:Label>
                        可选&nbsp; &nbsp;
                        <asp:Label ID="Labelselect" runat="server" BackColor="#D1F8D6" Width="11px" 
             Height="12px" BorderColor="#E4E4E4" BorderStyle="Solid" BorderWidth="1px"></asp:Label>
                        已选&nbsp;&nbsp;
                        <asp:Label ID="Labelother" runat="server" BackColor="Gray" Width="11px" 
             Height="11px" BorderColor="#E4E4E4" BorderStyle="Solid" BorderWidth="1px"></asp:Label>
                        不可选</div>
                    <br />
        <div style="text-align:center; width: 552px;   margin: auto;">
        <asp:DataList ID="DLroom" runat="server" RepeatColumns="6" 
            RepeatDirection="Horizontal" onitemdatabound="DLroom_ItemDataBound" 
            DataKeyField="Rid" CellPadding="3" CellSpacing="3">
                    <ItemTemplate>
                        <div style="vertical-align: middle; width: 80px;
                            height: 50px;  text-align: center; ">                            
                            <asp:HyperLink ID="Rgradeclass" runat="server" Font-Size="9pt" Font-Underline="False"
                                Height="16px"  BackColor="WhiteSmoke" 
                                Width="40px" BorderColor="#E4E4E4" BorderWidth="1px"  ForeColor="Black" BorderStyle="Solid" 
                                Font-Names="Arial" Font-Bold="False" ></asp:HyperLink>
                             <br /> 
                            <asp:CheckBox ID="CheckRoom" runat="server"/>
                            <br />
                            <asp:Label ID="LabelRid" runat="server" Text='<%# Eval("Rid") %>' 
                                Visible="False"></asp:Label>
                            <br />
                            <asp:Label ID="LabelRhid" runat="server" Text='<%# Eval("Rhid") %>' 
                                Visible="False"></asp:Label>
                            
                            <br />
                            <asp:Label ID="LabelRgrade" runat="server" Text='<%# Eval("Rgrade") %>' 
                                Visible="False"></asp:Label>
                            
                            <br />
                            <asp:Label ID="LabelRclass" runat="server" Text='<%# Eval("Rclass") %>' 
                                Visible="False"></asp:Label>
                            <br />
                        </div>
                    </ItemTemplate>
                </asp:DataList>  
                </div> 
                    <br />
                    <br />
                    </div>
                    <br />
        <br />
        <br />
<br />
                    <asp:Button ID="Btnselect" runat="server" BackColor="#E6E6E6" 
                        BorderColor="#D4D4D4" BorderWidth="1px" Font-Names="Arial" 
                Font-Size="9pt" Text="确定" Width="70px" Height="20px" onclick="Btnselect_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Btnreturn" runat="server" BackColor="#E6E6E6" 
                        BorderColor="#D4D4D4" BorderWidth="1px" Font-Names="Arial" 
                Font-Size="9pt" Text="返回" Width="70px" Height="20px" 
            onclick="Btnreturn_Click" />
        <br />
</div>

</asp:Content>


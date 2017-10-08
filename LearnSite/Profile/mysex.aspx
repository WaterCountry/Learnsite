<%@ Page Title="" Language="C#" MasterPageFile="~/Profile/Pf.master" StylesheetTheme="Student"  AutoEventWireup="true" CodeFile="mysex.aspx.cs" Inherits="Profile_mysex" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cstu" Runat="Server">
<br />
        <br />
        <br />
        <div  class="pfdiv">
            <div  class="indexhead">
                性别修改</div>
                <br />
                <br />                
            <br />
            请选择性别：<asp:DropDownList ID="DDLsex" runat="server" Font-Size="9pt" Width="60px" 
                        BackColor="Cornsilk">
    </asp:DropDownList>
            <br />
                    <br />
            <br />
                    <asp:Button ID="Btnsex" runat="server"  OnClick="Btnsex_Click" 
                Text="确定"   TabIndex="2"  SkinID="buttonSkin" Height="20px" Width="80px" />
            <br />
            <br />
        <asp:Label ID="Labelstr" runat="server" SkinID="LabelMsgRed"></asp:Label>
                    <br />                
            <br />                
        </div>
        <br />
        <br />
        <br />
</asp:Content>


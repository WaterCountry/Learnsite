<%@ Page Title="" Language="C#" MasterPageFile="~/Profile/Pf.master" StylesheetTheme="Student" AutoEventWireup="true" CodeFile="myclass.aspx.cs" Inherits="Profile_myclass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cstu" Runat="Server">
 <br />
        <br />
        <br />
        <div  class="pfdiv">
            <div  class="indexhead">
                班级修改</div>
                <br />
                <br />
                
            当前所在班级：<asp:Label ID="Labelclass" runat="server"></asp:Label>
                    <br />
                
            <br />
            请选择新班级：<asp:DropDownList ID="DDLclass" runat="server" Font-Size="9pt" 
                Width="50px">
    </asp:DropDownList>
                    <br />
            <br />
            <br />
                    <asp:Button ID="Btnclass" runat="server"  OnClick="Btnclass_Click" 
                Text="确定"   TabIndex="2"  SkinID="buttonSkin" Height="20px" Width="80px" 
                Enabled="False" />
            <br />
            <br />
        <asp:Label ID="Labelstr" runat="server"  SkinID="LabelMsgRed" ></asp:Label>
                    <br />
                
        </div>
        <br />
        <br />
        <br />
</asp:Content>


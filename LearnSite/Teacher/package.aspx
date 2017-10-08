<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="package.aspx.cs" Inherits="Teacher_package" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div  class="placehold">
        <br />
        <br />
        <div  class="infomation">
            <div  class="phead">学案包（Package）</div>
            <br />
            学案名称：<asp:Label ID="LabelCtitle" runat="server"></asp:Label>
            <br />
            <br />
            学案ID：<asp:Label ID="LabelCid" runat="server"></asp:Label>
            <br />
            <br />
            <div class="packagediv">
            <asp:Panel ID="Panelinfo" runat="server"  Width="280px"  BorderColor="#EAEAEA" 
                BorderStyle="Solid" BorderWidth="1px" Visible="False" >
            <br />
                <asp:Label ID="Labelinfo" runat="server" Width="240px"></asp:Label>
                <br />
                <br />
                <br />
                <asp:Button ID="Btndown" runat="server" onclick="Btndown_Click" 
                    SkinID="BtnSmall" Text="这里下载" ToolTip="点击下载" />
                <br />
                <br />
                <br />            
            </asp:Panel>
            </div>
            <br />
                <asp:Button ID="BtnZip" runat="server"  Text="打包" onclick="BtnZip_Click"  ToolTip="点击开始学案打包" SkinID="BtnNormal" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="Btnreturn" runat="server"  Text="返回" onclick="Btnreturn_Click"  SkinID="BtnNormal" />
                <br />
            <br />
        <asp:Label ID="Labelmsg" runat="server"   SkinID="LabelMsgRed"></asp:Label>
            <br />
            <br />
            <div style="width: 98%; margin: auto" >
                <asp:DataList ID="Dlfilelist" runat="server" 
                    RepeatColumns="2" RepeatDirection="Horizontal" Caption="本学案目录内资源列表"  
                CaptionAlign="Left" CellPadding="3" CellSpacing="3" 
                    onitemdatabound="Dlfilelist_ItemDataBound" Width="100%" >
                    <ItemTemplate>
                        <div  style="border-width: thin; border-color: #E6E8E6; background-color: #F8F8F8; border-bottom-style: solid;text-align: left;">
                            <asp:Label ID="Labelfid" runat="server" Text='<%# Eval("fid") %>' BackColor="#EEF0EF"></asp:Label>&nbsp;
                            <asp:HyperLink ID="HLfname" runat="server" Target="_blank" Text='<%# Eval("fname") %>' ></asp:HyperLink>&nbsp;                            
                            <asp:Label ID="Labelfsize" runat="server" Text='<%# Eval("fsize") %>' ></asp:Label>
                            <asp:Label ID="Labelfread" runat="server" Text='<%#  Eval("fread") %>'  ToolTip="是否只读（T：只读 | F：可写）"  ForeColor="#00A279"></asp:Label>
                            <asp:Label ID="Labelurl" runat="server" Text='<%# Eval("furl") %>' Visible="false" ></asp:Label>                        
                        </div>
                    </ItemTemplate>
                    <SeparatorStyle BorderColor="Silver" BorderStyle="Dotted" BorderWidth="1px" />
                </asp:DataList>
            </div>
            <br />
                <br />
            </div>
        <br />
        <br />
        <br />

</div>
</asp:Content>


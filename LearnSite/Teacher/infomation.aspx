<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="infomation.aspx.cs" Inherits="Teacher_infomation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div  class="placehold">
    <center>
        <br />
         <br />
         <asp:Label ID="Labelwelcome" runat="server" Font-Size="9pt"  ForeColor="Black" Font-Bold="True" ></asp:Label>
        <br />
        <br />
        <br />
        <div  class="infomation">
            <div  class="phead">我的班级列表</div>
            <br />
        <asp:DataList ID="DLmyclass" runat="server" RepeatColumns="5" 
            RepeatDirection="Horizontal" Height="40px" CellPadding="4" CellSpacing="4" 
                onitemdatabound="DLmyclass_ItemDataBound">
                    <ItemTemplate>
                        <div style="vertical-align: middle; width: 60px;text-align: center; ">                            
                            <asp:HyperLink ID="HyperRgradeclass" runat="server" Font-Size="9pt" Font-Underline="False"
                                Height="16px" Text='<%# Eval("Rgradeclass") %>' BackColor="#EFEFEF" 
                                Width="40px" BorderColor="#EEEEEE" BorderWidth="1px" ForeColor="Black" 
                                BorderStyle="Solid" Font-Names="Arial" ></asp:HyperLink>
                             <br />  
                            <asp:Label ID="LabelRset" runat="server" Text='<%# Eval("Rset") %>' Visible="False" ></asp:Label>                            
                            <asp:Label ID="LabelRreg" runat="server" Text='<%# Eval("Rreg") %>' Visible="False" ></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            <br />
         </div>
        <br />
        <asp:HyperLink ID="HyperLinkLession" runat="server" 
            NavigateUrl="~/Lessons/mylesson.aspx" SkinID="HyperLinkBtn" Target="_blank">学案列表</asp:HyperLink>
        <br />
        <br />
        当前学期：<asp:Label ID="Labelterm" runat="server"></asp:Label>
        &nbsp;<br />
        <br />
                    <br />
                    <asp:Button ID="Btnlogout" runat="server" Text="系统退出"  SkinID="BtnNormal" onclick="Btnlogout_Click" />
                    <br />
        <br />
        <br />
        <br />
        <asp:Label ID="Labelmsg" runat="server" SkinID="LabelMsgBlack"></asp:Label>
        <br />
        <br />
        <br />
        <br />
        <br />
</center>
</div>
</asp:Content>


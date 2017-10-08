<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Stud.master"  StylesheetTheme="Student"  AutoEventWireup="true" CodeFile="masterwork.aspx.cs" Inherits="Student_masterwork" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cphs" Runat="Server">
<div id="showcontent">

<div class="leftmaster"> 
    <br />
    <asp:DataList ID="DLworks" runat="server" CellPadding="3" CellSpacing="3" 
        RepeatColumns="6" RepeatDirection="Horizontal" Caption="推荐作品展示区" 
        onitemdatabound="DLworks_ItemDataBound" 
        onitemcommand="DLworks_ItemCommand">
        <ItemTemplate>
        <div class="divgood">
            <asp:Image ID="Image1" runat="server" />
                <asp:LinkButton ID="lBtnSname" runat="server"  CommandArgument='<%# Eval("Wurl") %>' CommandName="S" 
                ForeColor="#0000CC"     ToolTip="瞧瞧我的作品！" Text='<%# Eval("Wname") %>'></asp:LinkButton>
            <br />
            <asp:Label ID="Labelgrade" runat="server" Text='<%# Eval("Wgrade") %>' ></asp:Label> 
            <asp:Label ID="Labelclass" runat="server" Text='<%# Eval("Wclass") %>' ></asp:Label>班
            <asp:Label ID="Labeltype" runat="server" Text='<%# Eval("Wtype") %>' Visible="False"></asp:Label>
            </div>
        </ItemTemplate>
    </asp:DataList>
    <asp:Label ID="Labelmsg" runat="server"></asp:Label>
    <br />
                <div >
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal> 
                </div>
    <br />        
</div>

<div class="rightmaster">
        <strong>优秀作品库</strong><br />
        <asp:DropDownList ID="DDLgrade" runat="server" AutoPostBack="True" 
        Font-Size="9pt" onselectedindexchanged="DDLgrade_SelectedIndexChanged">
    </asp:DropDownList>
        年级&nbsp;
        第<asp:DropDownList ID="DDLterm" runat="server" AutoPostBack="True" 
        Font-Size="9pt" onselectedindexchanged="DDLterm_SelectedIndexChanged">
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
    </asp:DropDownList>
        学期<br />
    <asp:GridView ID="GVcourse" runat="server" AutoGenerateColumns="False" 
        CellPadding="2" SkinID="GridViewMission" Width="99%" 
        DataKeyNames="Cid" CellSpacing="3" EnableModelValidation="True" 
            onrowdatabound="GVcourse_RowDataBound">
             <Columns>
                 <asp:BoundField DataField="Cks" HeaderText="◆">
                 <ItemStyle Width="12px" />
                 </asp:BoundField>
                 <asp:TemplateField HeaderText="课节名称" >
                     <ItemTemplate>
                         <asp:HyperLink ID="HyperLink1" runat="server"  ToolTip='<%# Eval("Ctitle") %>' ></asp:HyperLink>
                     </ItemTemplate>
                     <HeaderStyle HorizontalAlign="Left" />
                     <ItemStyle  HorizontalAlign="Left" />
                 </asp:TemplateField>
                 <asp:TemplateField Visible="False">
                     <ItemTemplate>
                         <asp:Label ID="LabelCid" runat="server" Text='<%# Bind("Cid") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
             </Columns>
             <HeaderStyle BackColor="#E2EDEC"  />
      </asp:GridView> 
    <br />
    <br />
    <br />
    <br />
    
</div>

<br />
</div>
</asp:Content>

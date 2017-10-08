<%@ Page Title="" Language="C#" MasterPageFile="~/Profile/Pf.master"  StylesheetTheme="Student"  AutoEventWireup="true" CodeFile="mygroup.aspx.cs" Inherits="Profile_mygroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cstu" Runat="Server">
<div>
    <div style="margin: auto; width: 580px" >
        <asp:Panel ID="Panelapply" runat="server">
            <asp:GridView ID="GVgroup" runat="server" 
                AutoGenerateColumns="False" Caption="小组申请" CaptionAlign="Left" 
                onrowdatabound="GVgroup_RowDataBound" SkinID="GridViewInfo" 
                Width="98%" EnableModelValidation="True" CellPadding="5" 
                DataKeyNames="Sid" onrowcommand="GVgroup_RowCommand">
                <Columns>
                    <asp:BoundField HeaderText="序号">
                    <ItemStyle Width="30px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="组长">
                        <ItemTemplate>
                            <asp:Image ID="Imageflag" runat="server" ImageUrl="~/Images/gflag.gif" />
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Sname") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="70px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="成员">
                        <ItemTemplate>
                            <asp:Label ID="Labelmember" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="组队" ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" 
                            CommandArgument='<%# Eval("Sid") %>'  CommandName="AddGroup" Text="参加" ToolTip ="注意：组队后不能自行退出！"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                
            </asp:GridView>
        </asp:Panel>
        <asp:Panel ID="PanelSgtitle" runat="server">
        <div>我的小组名称：<asp:TextBox ID="TextBox1" runat="server" BorderColor="#CCCCCC" 
                BorderStyle="Dashed" BorderWidth="1px"></asp:TextBox>
            &nbsp;
            <asp:Button ID="BtnSgtitle" runat="server" onclick="BtnSgtitle_Click" 
                SkinID="buttonSkin" Text="修改" />
            </div>
        </asp:Panel>
        <asp:Panel ID="Panelwork" runat="server">
            <asp:GridView ID="GVwork" runat="server" AutoGenerateColumns="False" 
                Caption="小组作品" CaptionAlign="Left" CellPadding="5" DataKeyNames="Gid" 
                EnableModelValidation="True" onrowdatabound="GVwork_RowDataBound" SkinID="GridViewInfo" Width="98%">
                <Columns>
                    <asp:BoundField HeaderText="序号">
                    <ItemStyle Width="30px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Ggrade" HeaderText="年级" />
                    <asp:BoundField DataField="Gterm" HeaderText="学期" />
                    <asp:BoundField DataField="Mtitle" HeaderText="主题" />
                    <asp:BoundField DataField="Gscore" HeaderText="学分" />
                    <asp:HyperLinkField DataNavigateUrlFields="Gurl" HeaderText="下载" 
                        Target="_blank" Text="点击" />
                    <asp:CheckBoxField DataField="Gcheck" HeaderText="状态" ReadOnly="True" />
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <br />
        <br />
    <br />
    <br />		
</div>
<br />
</div>
</asp:Content>


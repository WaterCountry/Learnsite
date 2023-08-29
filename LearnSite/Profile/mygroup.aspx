<%@ page title="" language="C#" masterpagefile="~/profile/Pf.master" stylesheettheme="Student" autoeventwireup="true" inherits="Profile_mygroup, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cstu" Runat="Server">
    <div>
    <div style="margin: auto; " >
        <asp:Panel ID="Panelapply" runat="server">
            <asp:GridView ID="GVgroup" runat="server" 
                AutoGenerateColumns="False" Caption="小组申请" 
                onrowdatabound="GVgroup_RowDataBound" SkinID="GridViewInfo" 
                Width="98%" EnableModelValidation="True" CellPadding="5" 
                DataKeyNames="Sid" onrowcommand="GVgroup_RowCommand">
                <Columns>
                    <asp:BoundField HeaderText="序号" Visible="false">
                    <ItemStyle Width="40px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Sgtitle" HeaderText="小组名称"   >
                    <ItemStyle Width="120px" Font-Size="11pt" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="组长">
                        <ItemTemplate>
                            <asp:Image ID="Imageflag" runat="server" ImageUrl="~/images/gflag.gif" />
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Sname") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px"   Font-Size="11pt" HorizontalAlign="Left"  />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="成员">
                        <ItemTemplate>
                            <asp:Label ID="Labelmember" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle   Font-Size="11pt" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="组队" ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" 
                            CommandArgument='<%# Eval("Sid") %>'  CommandName="AddGroup" Text="参加" ToolTip ="注意：组队后不能自行退出！"></asp:LinkButton>
                        </ItemTemplate>
                    <ItemStyle Width="40px"   />
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
        <asp:Panel ID="Panelwork" runat="server" >
            <asp:GridView ID="GVwork" runat="server" AutoGenerateColumns="False" 
                Caption="小组作品"  CellPadding="5" DataKeyNames="Gid" 
                EnableModelValidation="True" onrowdatabound="GVwork_RowDataBound" SkinID="GridViewInfo" Width="98%">
                <Columns>
                    <asp:BoundField HeaderText="序号"  Visible="false">
                    <ItemStyle Width="40px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Ggrade" HeaderText="年级" />
                    <asp:BoundField DataField="Gterm" HeaderText="学期" />
                    <asp:BoundField DataField="Mtitle" HeaderText="主题" />
                    <asp:BoundField DataField="Gscore" HeaderText="学分" />
                    <asp:HyperLinkField DataNavigateUrlFields="Gurl" HeaderText="作品" 
                        Target="_blank" Text="下载" />
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


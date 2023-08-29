<%@ page title="" language="C#" masterpagefile="~/manager/Manage.master" autoeventwireup="true" inherits="Manager_teacher, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div class="manageplace" >
        教师管理<br />
        <div  class="teachermg">
                    <asp:Button ID="Btnadd" runat="server" BackColor="#E6E6E6" 
                        BorderColor="#D4D4D4" BorderWidth="1px" Font-Names="Arial" 
                Font-Size="11pt" Text="教师添加" Width="80px" Height="24px" onclick="Btnadd_Click" />
                    </div>
                 <div class="teacherdiv">
                <asp:GridView ID="GVTeacher" runat="server" 
                    AutoGenerateColumns="False" BorderColor="#E7E7E7" BorderStyle="Solid" 
                    BorderWidth="1px" CellPadding="3" Font-Size="11pt" GridLines="None" Width="100%" 
                    onpageindexchanging="GVTeacher_PageIndexChanging" 
                    onrowdatabound="GVTeacher_RowDataBound" EnableModelValidation="True" 
                            onrowcommand="GVTeacher_RowCommand">
                    <Columns>
                        <asp:BoundField HeaderText="序号" />
                        <asp:BoundField DataField="Hname" HeaderText="账号" />
                        <asp:BoundField DataField="Hnick" HeaderText="昵称" />
                        <asp:BoundField DataField="Hpwd" HeaderText="密码" />
                        <asp:TemplateField HeaderText="权限">
                            <ItemTemplate>
                                <asp:Label ID="LabelHpermiss" runat="server" Text='<%# Bind("Hpermiss") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Hnote" HeaderText="备注" />
                        <asp:BoundField DataField="Hcount" HeaderText="学案数" />
                        <asp:TemplateField HeaderText="班级">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLinkRoom" runat="server" 
                                    NavigateUrl='<%# Eval("hid", "roomselect.aspx?hid={0}") %>' Text="选择"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:HyperLinkField DataNavigateUrlFields="hid" 
                            DataNavigateUrlFormatString="teacheredit.aspx?hid={0}" 
                            Text="修改" HeaderText="操作" />
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButtonDel" runat="server" CausesValidation="false" 
                                    CommandName="D" Text="删除" CommandArgument='<%# Bind("hid") %>' ToolTip="如果删除后想恢复，请手动在数据库Teacher表将该账号的删除标志重置为false！"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BorderStyle="None" Font-Names="Arial" Font-Size="11pt" 
                        ForeColor="Black" Height="24px" />
                    <HeaderStyle BackColor="#939CA2" Font-Bold="False" Font-Names="Arial" 
                        Font-Size="11pt" Height="24px" />
                    <AlternatingRowStyle BackColor="#E7E7E7" />
                </asp:GridView>
                </div>
</div>
</asp:Content>


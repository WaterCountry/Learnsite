<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher"
    AutoEventWireup="true" CodeFile="softcategory.aspx.cs" Inherits="Teacher_softcategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <div>
        <div class="centerdiv">
            <div style="margin: auto; width: 700px; font-size:9pt; text-align:center">
                <br />
                <strong>资源的分类设置</strong>：<br />
                <br />
                <asp:GridView ID="GVCategory" runat="server" SkinID="GridViewInfo" AutoGenerateColumns="False"
                    DataKeyNames="Yid" Width="100%" CellPadding="0" Font-Size="9pt" OnRowCommand="GVCategory_RowCommand"
                    EnableModelValidation="True" OnRowDataBound="GVCategory_RowDataBound" OnRowCancelingEdit="GVCategory_RowCancelingEdit"
                    OnRowEditing="GVCategory_RowEditing" 
                    OnRowUpdating="GVCategory_RowUpdating">
                    <Columns>
                        <asp:TemplateField >
                            <ItemTemplate>
                                <asp:Label ID="LabelYid" runat="server" Text='<%# Bind("Yid") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle ForeColor="#EEEEEE" Width="20px"  />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="资源类别">
                            <ItemTemplate>
                                <asp:Label ID="LabelYtitle" runat="server" Text='<%# Bind("Ytitle") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TBoxYtitle" runat="server" Text='<%# Bind("Ytitle") %>' Font-Size="9pt"
                                    Width="200px" Height="12px" BackColor="#FFFFCC"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="ImageBtnTop" runat="server" CausesValidation="False" CommandName="Top"
                                    CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Text="上" ToolTip="向上移"
                                    Font-Underline="False"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="16px" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="ImageBtnBottom" runat="server" CausesValidation="False" CommandName="Bottom"
                                    CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Text="下" ToolTip="向下移"
                                    Font-Underline="False"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="16px" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                    CommandName="Edit" ImageUrl="~/Images/e.gif" Text="编辑" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="True" 
                                    CommandName="Update" ImageUrl="~/Images/u.gif" Text="更新" />
                                &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                                    CommandName="Cancel" ImageUrl="~/Images/c.gif" Text="取消" />
                            </EditItemTemplate>
                            <ItemStyle Width="60px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="BtnDel" runat="server" CausesValidation="false" CommandName="Del" 
                                CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Text="删除"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="30px" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle Height="30px" />
                    <RowStyle Height="30px" />
                </asp:GridView>
            </div>
            <div>
                <br />
                <br />
                类别描述：<asp:TextBox ID="TextBoxNewYtitle" runat="server" SkinID="TextBoxNormal" Width="180px"
                    MaxLength="30"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Btnadd" runat="server" Text="添加" OnClick="Btnadd_Click" SkinID="BtnSmall" />
                &nbsp;&nbsp;&nbsp; &nbsp;<asp:Button ID="Btnreturn" runat="server" Text="返回" OnClick="Btnreturn_Click"
                    SkinID="BtnSmall" Width="60px" />
                <br />
            </div>
        </div>
        <br />
    </div>
</asp:Content>

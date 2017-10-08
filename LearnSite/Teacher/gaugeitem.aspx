<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master"   StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="gaugeitem.aspx.cs" Inherits="Teacher_gaugeitem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div>
<div class="centerdiv">
<div style=" margin: auto; width: 680px; font-size:9pt; text-align:center">
                    <br />
                    自定义评价标准：<asp:Label ID="LabelGtitle" runat="server" Font-Bold="True"></asp:Label>
                    <br />
                    <asp:GridView ID="GVGaugeItem" runat="server"  SkinID="GridViewInfo"
                            AutoGenerateColumns="False"  DataKeyNames="Mid"  Width="100%" CellPadding="6" 
                            Font-Size="9pt"  onrowcommand="GVGaugeItem_RowCommand" 
                        EnableModelValidation="True" onrowdatabound="GVGaugeItem_RowDataBound" 
                        onrowcancelingedit="GVGaugeItem_RowCancelingEdit" 
                        onrowediting="GVGaugeItem_RowEditing" 
                        onrowupdating="GVGaugeItem_RowUpdating" >
                            <Columns>
                                <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                 <asp:Label ID="Label1" runat="server" Text='<%# Bind("Msort") %>'></asp:Label> 
                                </ItemTemplate>
                                    <ItemStyle Width="30px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="评价描述">
								<ItemTemplate>
                                   <asp:Label ID="LabelMitem" runat="server" Text='<%# Bind("Mitem") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBoxMitem" runat="server" Text='<%# Bind("Mitem") %>'     Font-Size="9pt"  Width="200px" Height="12px" BackColor="#FFFFCC"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"  Width="200px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="分值">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelMscore" runat="server" Text='<%# Bind("Mscore") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBoxMscore" runat="server" Text='<%# Bind("Mscore") %>'     Font-Size="9pt"  Width="20px" Height="12px" BackColor="#FFFFCC"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle Width="30px" />
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" >
								<ItemStyle Width="70px" />
                                </asp:CommandField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="BtnDel" runat="server" CausesValidation="false" 
                                          CommandArgument='<%# Eval("Mid") %>'   CommandName="Del" Text="删除"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="30px" />
                                </asp:TemplateField>
                            </Columns>               
                        </asp:GridView>
                    <br />
                    <div >
                            <br />
                        评价描述：<asp:TextBox ID="TextBoxMitem" runat="server" SkinID="TextBoxNormal" 
                        Width="180px"></asp:TextBox>
                        分值<asp:DropDownList ID="DDLscore" runat="server"  Font-Size="9pt">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem Selected="True">2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>-1</asp:ListItem>
                            <asp:ListItem>-2</asp:ListItem>
                            <asp:ListItem>-3</asp:ListItem>
                            <asp:ListItem>-4</asp:ListItem>
                            <asp:ListItem>-5</asp:ListItem>
            </asp:DropDownList>
                        顺序<asp:DropDownList ID="DDLsort" runat="server"  Font-Size="9pt">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
            </asp:DropDownList>
                        &nbsp;<asp:Button ID="Btnadd" runat="server"  Text="添加"  onclick="Btnadd_Click" 
                            SkinID="BtnSmall" />                    
                    &nbsp;<asp:Button ID="Btnreturn" runat="server"  Text="返回"  onclick="Btnreturn_Click" 
                            SkinID="BtnSmall" Width="60px" />                    
                    <br />
                    </div>
                        </div>
                        
                        </div>
    <br />
<br />
</div>

</asp:Content>


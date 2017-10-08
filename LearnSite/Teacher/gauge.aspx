<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master"  StylesheetTheme="Teacher"  AutoEventWireup="true" CodeFile="gauge.aspx.cs" Inherits="Teacher_gauge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div>
<div class="centerdiv">
<div style=" margin: auto; width: 680px; font-size:9pt; text-align:center;">
                    <br />
                    <strong>自定义量化评价标准</strong><br />
                    <br />
                    <asp:GridView ID="GVGauge" runat="server"  SkinID="GridViewInfo"
                            AutoGenerateColumns="False"  DataKeyNames="Gid"  Width="100%" CellPadding="5" 
                            Font-Size="9pt"  onrowcommand="GVGauge_RowCommand" 
                        EnableModelValidation="True" onrowdatabound="GVGauge_RowDataBound" >
                            <Columns>
                                <asp:BoundField HeaderText="序号">
                                <HeaderStyle Width="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Gtype" HeaderText="分类">
                               <HeaderStyle Width="30px" />
                                </asp:BoundField>
                                <asp:HyperLinkField DataNavigateUrlFields="Gid"  
                                    DataNavigateUrlFormatString="~/Teacher/GaugeItem.aspx?Gid={0}" 
                                    DataTextField="Gtitle" HeaderText="标题" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:HyperLinkField>
                                <asp:BoundField DataField="Gcount" HeaderText="使用">
                                <HeaderStyle Width="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Gdate" HeaderText="日期">
                                <HeaderStyle Width="120px" />
                                </asp:BoundField>
                                <asp:TemplateField  HeaderText="操作">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="BtnEdit" runat="server" CausesValidation="false" 
                                          CommandArgument='<%# Eval("Gid") %>' CommandName="Del" Text="删除"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px" />
                                </asp:TemplateField>
                            </Columns>                            
                        </asp:GridView>
                    <br />
                    <div >
                    &nbsp;
                        作品类型：<asp:DropDownList ID="DDLtype" runat="server"  Font-Size="9pt">
            </asp:DropDownList>
                    &nbsp;量规标题：<asp:TextBox ID="TextBoxGtitle" runat="server" SkinID="TextBoxNormal" 
                        Width="300px"></asp:TextBox>
                    &nbsp;
       <asp:Button ID="Btnadd" runat="server"  Text="添加量规"  onclick="Btnadd_Click" SkinID="BtnSmall" />                    
                    <br />
                    </div>
                    <br />
 <br />
    ***评价标准使用后，将无法删除，请慎重填写！***<br />
<br />
                        </div>
                        </div>
</div>
</asp:Content>


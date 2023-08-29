<%@ page title="" language="C#" masterpagefile="~/teacher/Teach.master" stylesheettheme="Teacher" autoeventwireup="true" inherits="Teacher_gauge, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div>
<div class="centerdiv">
<div style=" margin: auto; width: 800px; font-size:11pt; text-align:center;">
                    <br />
                    <strong>自定义量化评价标准</strong><br />
                    <br />
                    <asp:GridView ID="GVGauge" runat="server"  SkinID="GridViewInfo"
                            AutoGenerateColumns="False"  DataKeyNames="Gid"  Width="100%" CellPadding="5" 
                            Font-Size="9pt"  onrowcommand="GVGauge_RowCommand" 
                        EnableModelValidation="True" onrowdatabound="GVGauge_RowDataBound" >
                            <Columns>
                                <asp:BoundField HeaderText="序号">
                                <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Gtype" HeaderText="分类">
                               <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:HyperLinkField DataNavigateUrlFields="Gid"  
                                    DataNavigateUrlFormatString="~/teacher/gaugeitem.aspx?gid={0}" 
                                    DataTextField="Gtitle" HeaderText="标题" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:HyperLinkField>
                                <asp:BoundField DataField="Gcount" HeaderText="使用">
                                <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Gdate" HeaderText="日期">
                                <HeaderStyle Width="160px" />
                                </asp:BoundField>
                                <asp:TemplateField  HeaderText="操作">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="BtnEdit" runat="server" CausesValidation="false" 
                                          CommandArgument='<%# Eval("Gid") %>' CommandName="Del" Text="删除"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="40px" />
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
       <asp:Button ID="Btnadd" runat="server"  Text="添加量规"  onclick="Btnadd_Click" SkinID="BtnNormal" />                    
                    <br />
                    </div>
                    <br />
 <br />
    ***评价标准使用后，将无法删除，请慎重填写！***<br />
                    <br />
                    ***当活动中未指定互评评价标准时，将自动选取相应作品类型中的第一条评价标准***<br />
<br />
                        </div>
                        </div>
</div>
</asp:Content>


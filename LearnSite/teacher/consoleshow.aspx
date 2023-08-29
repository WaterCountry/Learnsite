<%@ page title="" language="C#" masterpagefile="~/teacher/Teach.master" autoeventwireup="true" inherits="Teacher_consoleshow, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div>
<div>
    <br />
    测评名称：<asp:Label runat="server" ID="Lbtitle" Font-Bold="True"></asp:Label>
    <br />
    <br />
    <div style="border-width: 1px; border-color: #808080; border-bottom-style: dashed; padding-bottom: 2px;">
    　日期：<asp:Label runat="server" ID="Lbdate"></asp:Label>
    &nbsp;<asp:ImageButton ID="BtnEdit" runat="server" ToolTip="点击修改" 
                     ImageUrl="~/images/edit.gif" onclick="BtnEdit_Click" />
    &nbsp;&nbsp;&nbsp;                    
    <asp:ImageButton ID="Btnclock" runat="server" ImageUrl="~/images/clock.gif" 
            onclick="Btnclock_Click" ToolTip="测评启用或停止"  />
    </div>
    <br />
    <div id="vcontent" runat="server" 
        style="margin: auto; padding: 6px; text-align: left; width: 800px;"></div>
    <br />
    <div style="margin: auto; width: 600px; ">
         <asp:Button ID="Btnadd" runat="server" onclick="Btnadd_Click" 
             SkinID="BtnNormal" Text="添加试题" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Button ID="Btnreturn" runat="server" onclick="Btnreturn_Click" 
             SkinID="BtnNormal" Text="返回" />
         <br />
         <br />
    </div>
    <div>
                    <asp:GridView ID="GVProblem" runat="server"  SkinID="GridViewInfo"
                            AutoGenerateColumns="False"   Width="980px" CellPadding="5"  Font-Size="11pt" 
                        EnableModelValidation="True" HorizontalAlign="Center" 
                        onrowcommand="GVProblem_RowCommand" 
                        onrowdatabound="GVProblem_RowDataBound" DataKeyNames="Pid" >
                            <Columns>
                                <asp:BoundField HeaderText="序号">
                                <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="试题">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelPtitle" runat="server" Text='<%# HttpUtility.HtmlDecode(DataBinder.Eval(Container.DataItem,"Ptitle").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="600px" />
                                </asp:TemplateField>

                                
                             <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="ImageBtnTop" runat="server" CausesValidation="False" 
                                        CommandName="Top"  CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                        Text="上" ToolTip="向上移" Font-Underline="False"></asp:LinkButton>
                                </ItemTemplate>
                                 <ItemStyle Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="ImageBtnBottom" runat="server" CausesValidation="False" 
                                        CommandName="Bottom"  CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                        Text="下" ToolTip="向下移" Font-Underline="False"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="20px" />
                            </asp:TemplateField>

                                <asp:BoundField DataField="Pscore" HeaderText="分值">
                                <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLinkPid" runat="server"  Text="编辑"></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="BtnDel" runat="server" CausesValidation="false" 
                                          CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="Del" Text="删除"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>
                            </Columns>                            
                        </asp:GridView>
                    &nbsp;</div>
    <br />
    <asp:HyperLink ID="Hkconsole" runat="server" NavigateUrl="#" 
        Target="_blank">预览效果</asp:HyperLink>
    <br />
    <br />
</div>
</div>

</asp:Content>


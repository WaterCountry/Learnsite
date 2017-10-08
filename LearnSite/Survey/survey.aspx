<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher"  AutoEventWireup="true" CodeFile="survey.aspx.cs" Inherits="Survey_survey" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div>
<div>
    <br />
    <asp:ImageButton ID="Btnclock" runat="server" ImageUrl="~/Images/clock.gif" 
            onclick="Btnclock_Click" />
调查名称：<asp:Label runat="server" ID="Lbtitle" Font-Bold="True"></asp:Label>
    <br />
    <br />
    <div style="border-width: 1px; border-color: #808080; border-bottom-style: dashed; padding-bottom: 2px;">
    　类型：<asp:Label runat="server" ID="Lbtype"></asp:Label>
    　总分：<asp:Label runat="server" ID="Lbscore"></asp:Label>
    　平均分：<asp:Label runat="server" ID="Lbave"></asp:Label>
        &nbsp;日期：<asp:Label runat="server" ID="Lbdate"></asp:Label>
    &nbsp;<asp:ImageButton ID="BtnEdit" runat="server" ToolTip="点击修改" 
                     ImageUrl="~/Images/edit.gif" onclick="BtnEdit_Click" />
    </div>
    <br />
    <div id="vcontent" runat="server" 
        style="margin: auto; padding: 6px; text-align: left; width: 700px;"></div>
    <br />
    <div style="margin: auto; width: 500px; ">
         <asp:Button ID="Btnadd" runat="server" onclick="Btnadd_Click" 
             SkinID="BtnNormal" Text="添加试题" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Button ID="Btnreturn" runat="server" onclick="Btnreturn_Click" 
             SkinID="BtnNormal" Text="返回" />
         <br />
         <br />
    </div>
    <div>
                    <asp:GridView ID="GVQuestion" runat="server"  SkinID="GridViewInfo"
                            AutoGenerateColumns="False"  DataKeyNames="Qid"  
            Width="680px" CellPadding="5" 
                            Font-Size="9pt" 
                        EnableModelValidation="True" HorizontalAlign="Center" 
                        onrowcommand="GVQuestion_RowCommand" 
                        onrowdatabound="GVQuestion_RowDataBound" >
                            <Columns>
                                <asp:BoundField HeaderText="序号">
                                <HeaderStyle Width="30px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="调查试题">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelQtitle" runat="server" Text='<%# HttpUtility.HtmlDecode(DataBinder.Eval(Container.DataItem,"Qtitle").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="420px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Qcount" HeaderText="正确率">
                                <HeaderStyle Width="60px" />
                                </asp:BoundField>
                                <asp:HyperLinkField DataNavigateUrlFields="Qid,Qvid,Qcid" 
                                    DataNavigateUrlFormatString="~/Survey/surveyitem.aspx?Qid={0}&amp;Qvid={1}&amp;Qcid={2}" 
                                    HeaderText="操作" Text="选项" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="Labelcount" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:HyperLinkField DataNavigateUrlFields="Qid,Qvid,Qcid" 
                                    DataNavigateUrlFormatString="~/Survey/surveyquestion.aspx?Qid={0}&amp;Vid={1}&amp;Cid={2}" 
                                    Text="编辑" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="BtnDel" runat="server" CausesValidation="false" 
                                          CommandArgument='<%# Eval("Qid") %>' CommandName="Del" Text="删除"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px" />
                                </asp:TemplateField>
                            </Columns>                            
                        </asp:GridView>
                    &nbsp;</div>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</div>
</div>
</asp:Content>


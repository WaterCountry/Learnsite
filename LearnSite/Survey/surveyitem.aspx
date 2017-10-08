<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher"
    AutoEventWireup="true" ValidateRequest="false" CodeFile="surveyitem.aspx.cs"
    Inherits="Survey_surveyitem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <div>
        <div class="centerdiv">
            <div style="margin: auto; width: 600px;">
                <br />
                <div style="margin: auto; text-align: left;">
                    <b>调查题目：</b><asp:Label ID="LabelQtitle" runat="server"></asp:Label>
                </div>
                <br />
                <asp:GridView ID="GVSurveyItem" runat="server" SkinID="GridViewInfo" AutoGenerateColumns="False"
                    DataKeyNames="Mid" Width="100%" CellPadding="6" Font-Size="9pt" EnableModelValidation="True"
                    PageSize="6" OnRowDataBound="GVSurveyItem_RowDataBound" OnRowCommand="GVSurveyItem_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="选项内容">
                            <ItemTemplate>
                                <asp:Label ID="LabelMitem" runat="server" Text='<%# HttpUtility.HtmlDecode(DataBinder.Eval(Container.DataItem,"Mitem").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="分值">
                            <ItemTemplate>
                                <asp:Label ID="LabelMscore" runat="server" Text='<%# Bind("Mscore") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBoxMscore" runat="server" Text='<%# Bind("Mscore") %>' Font-Size="9pt"
                                    Width="30px" Height="12px" BackColor="#FFFFCC"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="BtnEdit" runat="server" CausesValidation="false" CommandArgument='<%# Eval("Mid") %>'
                                    CommandName="Edt" Text="编辑"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="24px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="BtnDel" runat="server" CausesValidation="false" CommandArgument='<%# Eval("Mid") %>'
                                    CommandName="Del" Text="删除"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="24px" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:GridView>
                <br />
                <div style="margin: auto; width: 550px;">
                    <script charset="utf-8" src="../kindeditor/kindeditor-min.js"></script>
                    <script charset="utf-8" src="../kindeditor/lang/zh_CN.js"></script>
                    <script>
		    var editor;
            var cid= <%=myCid() %>;
            var ty="Course";
            var upjs= '../kindeditor/aspnet/upload_json.aspx?Cid='+cid+'&Ty='+ty;
            var fmjs='../kindeditor/aspnet/file_manager_json.aspx?Cid='+cid+'&Ty='+ty;
		    KindEditor.ready(function (K) {
		        editor = K.create('textarea[name="ctl00$Content$mcontent"]', {
		            resizeType: 1,
		            newlineTag: "br",
		            items: [
						'source','|','undo', 'redo','|','fontname', 'fontsize','forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
						'removeformat','justifyleft', 'justifycenter', 'justifyright', 'emoticons','image','clearhtml'],                    
				uploadJson : upjs,
				fileManagerJson : fmjs,
				allowFileManager : true,
                filterMode : false		            
		        });
		    });
                    </script>
                    <br />
                    选项描述：<asp:Label ID="LabelMid" runat="server" Font-Bold="True" Visible="False"></asp:Label>
                    <br />
                    <textarea id="mcontent" runat="server" style="width: 500px; height: 150px"></textarea><br />
                    <br />
                    分值<asp:DropDownList ID="DDLscore" runat="server" Font-Size="9pt">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem Selected="True">0</asp:ListItem>
                        <asp:ListItem>-1</asp:ListItem>
                        <asp:ListItem>-2</asp:ListItem>
                        <asp:ListItem>-3</asp:ListItem>
                        <asp:ListItem>-4</asp:ListItem>
                        <asp:ListItem>-5</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:Button ID="Btnadd" runat="server" Text="添加" OnClick="Btnadd_Click" SkinID="BtnSmall" />
                    &nbsp; &nbsp;<asp:Button ID="Btnreturn" runat="server" Text="返回" OnClick="Btnreturn_Click"
                        SkinID="BtnSmall" Width="60px" />
                    <br />
                    <br />
                </div>
            </div>
        </div>
        <br />
        友情提示：选项一般不超过４个； 如果是调查类型，你可以根据选项设置不同分值；<br />
        <br />
        如果是测验单选项，请将其中一个选项设置分值，其他选项分值设置为0即可。<br />
        <br />
    </div>
</asp:Content>

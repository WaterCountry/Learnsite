<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="quiz.aspx.cs" Inherits="Quiz_quiz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div   class="placehold">
    <div  class="quizleft">
        &nbsp;
        <asp:DropDownList ID="DDLqtype" runat="server" 
            Font-Size="9pt" ToolTip="题型">
            <asp:ListItem Selected="True" Value="0">单选题</asp:ListItem>
            <asp:ListItem Value="1">多选题</asp:ListItem>
            <asp:ListItem Value="2">判断题</asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;<asp:DropDownList ID="DDLclass" runat="server" Width="100px" 
            Font-Size="9pt" ToolTip="学案类型">
            </asp:DropDownList>
            &nbsp;
    <asp:Button ID="Btnlist" runat="server"  Text="浏览"  onclick="Btnlist_Click"  
            SkinID="BtnNormal" />  

    &nbsp;<asp:Label ID="Label1" runat="server" Width="180px"></asp:Label>
&nbsp;<asp:Button ID="Btnadd" runat="server"  Text="添加试题"  onclick="Btnadd_Click"  SkinID="BtnNormal" />  &nbsp;&nbsp;&nbsp;
    <asp:Button ID="Btngradeset" runat="server"  Text="测验设置"  
            onclick="Btngradeset_Click"  SkinID="BtnNormal" />  &nbsp;&nbsp;
    </div>
    <div >            
                    <asp:GridView ID="GVQuiz" runat="server" AllowPaging="True"  SkinID="GridViewInfo"
                            AutoGenerateColumns="False"  DataKeyNames="Qid" Width="96%"  
                            onpageindexchanging="GVQuiz_PageIndexChanging" 
                            onrowdatabound="GVQuiz_RowDataBound" onrowcommand="GVQuiz_RowCommand" 
                            TabIndex="1" CellPadding="3" GridLines="Horizontal" 
                        EnableModelValidation="True" HorizontalAlign="Center">
                            <Columns>
                                <asp:BoundField HeaderText="序号" HeaderStyle-Width="28px">
<HeaderStyle Width="28px"></HeaderStyle>
                                </asp:BoundField>
                                <asp:HyperLinkField DataNavigateUrlFields="Qid"  DataNavigateUrlFormatString="~/Quiz/quizedit.aspx?Qid={0}" 
                                    Text="编辑" >
                                <ItemStyle Width="30px" />
                                </asp:HyperLinkField>
                                <asp:TemplateField HeaderText="试题">
                                    <ItemTemplate>
                                    <div style="overflow:hidden; width: 500px">
                                    <asp:Label ID="Labelquestion" runat="server" Text='<%# HttpUtility.HtmlDecode(DataBinder.Eval(Container.DataItem,"Question").ToString()) %>'></asp:Label>
                                    </div>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="9pt" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Qclass" HeaderText="类型" />
                                <asp:BoundField DataField="Qscore" HeaderText="分值" />
                                <asp:BoundField DataField="Qanswer" HeaderText="答案" />
                                <asp:TemplateField HeaderText="分析">
                                    <ItemTemplate>
                                        <asp:Label ID="Labelqanalyze" runat="server" Text='提示' ToolTip='<%# Bind("Qanalyze") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Qaccuracy" HeaderText="正确" />
                                <asp:ButtonField Text="删除" CommandName="Del" >
                                <ItemStyle Width="40px" />
                                </asp:ButtonField>
                            </Columns>
                            <pagertemplate>
                                <div style="width:100%; height:13px; text-align:right">
                                    第<asp:Label ID="lblPageIndex" runat="server" 
                                        text="<%# ((GridView)Container.Parent.Parent).PageIndex + 1  %>" />
                                    页 共<asp:Label ID="lblPageCount" runat="server" 
                                        text="<%# ((GridView)Container.Parent.Parent).PageCount  %>" />
                                    页 
                                    <asp:LinkButton ID="btnFirst" runat="server" causesvalidation="False" 
                                        commandargument="First" commandname="Page" Font-Underline="False" 
                                        ForeColor="Black" text="首页" />
                                    <asp:LinkButton ID="btnPrev" runat="server" causesvalidation="False" 
                                        commandargument="Prev" commandname="Page" Font-Underline="False" 
                                        ForeColor="Black" text="上一页" />
                                    <asp:LinkButton ID="btnNext" runat="server" causesvalidation="False" 
                                        commandargument="Next" commandname="Page" Font-Underline="False" 
                                        ForeColor="Black" text="下一页" />
                                    <asp:LinkButton ID="btnLast" runat="server" causesvalidation="False" 
                                        commandargument="Last" commandname="Page" Font-Underline="False" 
                                        ForeColor="Black" text="尾页" />
                                </div>
                            </pagertemplate>
                        </asp:GridView>
                    <br />
                    <div >
                        &nbsp;
                        <asp:HyperLink ID="HlkQuizxml" runat="server" Font-Size="9pt">试题包下载</asp:HyperLink>
                        &nbsp;<asp:Button ID="Btnexport"  runat="server"  Text="生成试题包" 
                            SkinID="BtnNormal" TabIndex="2" onclick="Btnexport_Click" /> &nbsp;<asp:Label 
                            ID="Labelmsg" runat="server" Font-Size="9pt" Width="160px" ForeColor="Red"></asp:Label>
&nbsp;<asp:FileUpload ID="FileUploadquiz" runat="server" Font-Size="9pt" />
&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Btnimport"  runat="server"  Text="导入试题包"  onclick="Btnimport_Click" 
                            SkinID="BtnNormal" TabIndex="2" />                    
                    &nbsp;&nbsp;                
                    </div>                  
                    <br />
                    </div>  
    </div>
</asp:Content>


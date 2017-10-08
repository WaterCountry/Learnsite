<%@ Page Title="" Language="C#"  AutoEventWireup="true"  CodeFile="quizview.aspx.cs" Inherits="Student_quizview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <style type="text/css">
*
{
    margin-right: 0px;
    margin-left: 0px;
}
    </style>
</head>
<body>
<form id="form1" runat="server">
<div>
<div style="text-align: center " > 
                    <asp:Label ID="Labeltitle" runat="server" Font-Bold="True" Font-Size="12pt" 
                        BackColor="#006699" ForeColor="White"></asp:Label>
                    <br />
                    <center>
                    <br />
                    <asp:GridView ID="GVQuiz" runat="server"  
                            AutoGenerateColumns="False"  DataKeyNames="Qid" Width="800px" 
                            onrowdatabound="GVQuiz_RowDataBound" 
                            TabIndex="1" CellPadding="3" BackColor="White" BorderColor="#CCCCCC" 
                        BorderStyle="None" BorderWidth="1px" Font-Names="Arial" Font-Size="9pt" 
                            EnableModelValidation="True">
                            <RowStyle ForeColor="#000066" />
                            <Columns>
                                <asp:BoundField DataField="Qid" HeaderText="编号">
                                <ControlStyle Width="20px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Qtype" HeaderText="类型" Visible="False" />
                                <asp:TemplateField HeaderText="试题">
                                    <ItemTemplate>
                                        <asp:Label ID="Labelquestion" runat="server" Text='<%# HttpUtility.HtmlDecode(DataBinder.Eval(Container.DataItem,"Question").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="9pt" HorizontalAlign="Left" Width="420px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Qscore" HeaderText="分值" />
                                <asp:BoundField DataField="Qaccuracy" HeaderText="全校正确率%" />
                                <asp:BoundField DataField="Qanswer" HeaderText="答案" />
                                <asp:TemplateField HeaderText="分析" Visible="True">
                                    <ItemTemplate>
                                        <asp:Label ID="Labelqanalyze" runat="server" Text='提示' ToolTip='<%# Bind("Qanalyze") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />                            
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                    <br />
                        </center>
                    <br />
        <asp:Button ID="Btnreturn" runat="server"  Text="关闭"  SkinID="buttonSkin" Height="20px" 
                    Width="80px" BackColor="#006699" BorderStyle="None" Font-Size="9pt" 
                        ForeColor="White" />
                    <br />
                    <br />
                    <asp:Label ID="Labelmsg" runat="server" Font-Size="9pt"></asp:Label>
                    <br />
                    </div>
          </div>
</form>
</body>
</html>
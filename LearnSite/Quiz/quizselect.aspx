<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="quizselect.aspx.cs" Inherits="Quiz_quizselect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div >
    <div  class="quizdiv">  
                    <b>测验设置</b>
                    <br /><br />
                    <div style="width: 600px" class="centerdiv">
                    选择：<asp:DropDownList ID="DDLgrade" runat="server" Font-Size="9pt" 
                        EnableTheming="True" Font-Names="Arial" AutoPostBack="True" 
                        onselectedindexchanged="DDLgrade_SelectedIndexChanged" >
        </asp:DropDownList>
                    年级<br />
                    <asp:DataList ID="DataListGrade" runat="server" RepeatColumns="5" Width="100%" 
                            CellPadding="3" CellSpacing="3" 
                            onitemdatabound="DataListGrade_ItemDataBound">
                        <ItemTemplate>
                        <div class="typerset">
                            <asp:CheckBox ID="ChkGrade" runat="server" 
                                Text='<%# Container.DataItem.ToString() %>' />
                                </div>
                        </ItemTemplate>
                    </asp:DataList>
                        <br />
                        <br />
                    <asp:CheckBox ID="Quizpower" runat="server" Text="测验开关" 
                        ToolTip="当前年级测验开关：选中表示启用测验，未选表示停用测验" Font-Size="9pt" />
                    &nbsp;&nbsp;
                    <asp:CheckBox ID="Quizanswer" runat="server" Text="参考答案" 
                        ToolTip="当前年级考后参考答案开关：选中表示显示，未选表示隐藏" Font-Size="9pt" />
                    &nbsp;&nbsp;
                    试题数量：单选题<asp:DropDownList ID="DDLOnly" runat="server" Font-Size="9pt" >
                        <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem Value="12"></asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                            <asp:ListItem>18</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;多选题<asp:DropDownList ID="DDLMore" runat="server" Font-Size="9pt">
                            <asp:ListItem>0</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;判断题<asp:DropDownList ID="DDLJudge" runat="server" Font-Size="9pt">
                        <asp:ListItem>0</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem Value="12"></asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                            <asp:ListItem>18</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                    </asp:DropDownList>
                        <br />
                        <br />
                        <br />
                    <asp:Button ID="BtnSelect" runat="server" Text="提交选择" onclick="BtnSelect_Click" 
                        SkinID="BtnNormal" />
                        &nbsp;&nbsp;
                    <asp:Button ID="BtnReturn" runat="server" Text="返回" onclick="BtnReturn_Click" 
                        SkinID="BtnNormal" />
                        <br />
                        <br />
                    <asp:Label ID="Labelmsg" runat="server" SkinID="LabelMsgRed"></asp:Label>
                        <br />
                    <br />
                    </div>
                    <br />
                    <br />
                    <br />
               
                    </div>
                  
    </div>
</asp:Content>


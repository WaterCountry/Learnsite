<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" AutoEventWireup="true" CodeFile="grouping.aspx.cs" Inherits="Teacher_grouping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div style="margin: auto; padding: 2px; font-family: Arial; font-size: 9pt; font-weight: bold; border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #CCCCCC; width: 96%;">
    <asp:Label ID="labelclass" runat="server"></asp:Label>分组管理</div><br />
    <div style="text-align:center; font-size:9pt;">
<div>
        <asp:DataList ID="DLclass" runat="server" RepeatColumns="10" 
            RepeatDirection="Horizontal" DataKeyField="Sid" CellPadding="3" CellSpacing="0"  HorizontalAlign="Center">
                    <ItemTemplate>
                        <div style="vertical-align: middle; width: 62px;
                            height: 50px;  text-align: center; background-color: #F6F6F6;">   
                            <asp:Label ID="LabelSnum" runat="server" Text='<%# Eval("Snum") %>' Font-Size="8"></asp:Label>                         
                             <br />    
                            <asp:Label ID="LabelSname" runat="server" Text='<%# Eval("Sname") %>'></asp:Label>                         
                             <br /> 
                             <asp:Label ID="LabelSscore" runat="server" Text='<%# Eval("Sscore") %>'></asp:Label>
                            <asp:CheckBox ID="SelectStu" runat="server"/>
                             <asp:Label ID="LabelSid" runat="server" Text='<%# Eval("Sid") %>' Visible="false"></asp:Label>
                            <br />
                        </div>
                    </ItemTemplate>
                </asp:DataList>  
        <asp:RadioButtonList ID="RBsort" runat="server" AutoPostBack="True" 
            Font-Size="9pt" onselectedindexchanged="RBsort_SelectedIndexChanged" 
            RepeatDirection="Horizontal" RepeatLayout="Flow">
            <Items>
            <asp:ListItem Value="0" Selected="True">学分排序</asp:ListItem>
            <asp:ListItem Value="1">学号排序</asp:ListItem>
            </Items>
        </asp:RadioButtonList>
                <br />
</div>
<div>
    <br />
    <asp:GridView ID="GVGroups" runat="server" AutoGenerateColumns="False" 
        CellPadding="3" EnableModelValidation="True" 
        GridLines="None" HorizontalAlign="Center" 
        onrowdatabound="GVGroups_RowDataBound" onrowcommand="GVGroups_RowCommand" 
        Width="860px" DataKeyNames="Sid" BorderColor="#D2D2D2" BorderStyle="Solid" 
        BorderWidth="1px" onrowcancelingedit="GVGroups_RowCancelingEdit" 
        onrowediting="GVGroups_RowEditing" onrowupdating="GVGroups_RowUpdating">
        <AlternatingRowStyle BackColor="WhiteSmoke" />
        <Columns>
            <asp:TemplateField HeaderText="小组名称">
                <ItemTemplate>
                    <img alt="" src="../Images/gflag.gif" />
                    <asp:Label ID="LabelSgtitle" runat="server" Text='<%# Bind("Sgtitle") %>'></asp:Label>
                </ItemTemplate>                
                <EditItemTemplate>
                     <asp:TextBox ID="TBoxSgtitle" runat="server" Text='<%# Bind("Sgtitle") %>'></asp:TextBox>
                </EditItemTemplate>                
                <ItemStyle Width="150px" HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:CommandField CancelImageUrl="~/Images/c.gif" EditImageUrl="~/Images/e.gif" 
                ShowEditButton="True" UpdateImageUrl="~/Images/u.gif" ButtonType="Image">
            <ItemStyle Width="60px" />
            </asp:CommandField>
            <asp:TemplateField HeaderText="组长">
                        <ItemTemplate>
                            <asp:Label ID="LabelSname" runat="server" Text='<%# Bind("Sname") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="60px" />
                    </asp:TemplateField>
            <asp:TemplateField HeaderText="成员">
                <ItemTemplate>
                <div>
                    <asp:DataList ID="DLgstu" runat="server"  RepeatColumns="12" CellPadding="3" 
                        onitemcommand="DLgstu_ItemCommand" >
                        <ItemTemplate>
                            <asp:LinkButton ID="Gstu" runat="server" CommandName="Q" CommandArgument='<%# Eval("Sid") %>' Text='<%# Eval("Sname") %>' ToolTip="点击退组"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:DataList>
                    </div>
                </ItemTemplate>
                <ItemStyle Width="480px"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="平均分">
                <ItemTemplate>
                    <asp:Label ID="LabelSscores" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="50px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作" ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkBtnAdd" runat="server" CausesValidation="false" 
                        CommandName="A" CommandArgument='<%# Eval("Sid") %>' Text="参加"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="40px" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle BackColor="#F0F0F0" />
        <RowStyle BorderColor="#CCCCCC" BorderStyle="Dashed" BorderWidth="1px" 
            Height="30px" />
    </asp:GridView>
    <br />
    说明：将学生列表中学生选中后点击参加小组<br />
    点击小组成员姓名退组<asp:CheckBox ID="CkQuit" runat="server" Checked="True" Text="锁定成员退组" 
        ToolTip="默认选中锁定，无法退组；如果要退组请取消" />
    <br />
    <br />
</div>
</div>
</asp:Content>


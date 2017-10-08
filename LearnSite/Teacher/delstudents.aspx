<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master"  StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="delstudents.aspx.cs" Inherits="Teacher_delstudents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <p>
    <strong>
        <asp:Label ID="Labelgradeclass" runat="server"></asp:Label>已删除过的学生列表
    </strong>
    </p>
    <div class="centerdiv">
                        <asp:GridView ID="GVStudent" runat="server" AutoGenerateColumns="False" 
            Width="100%" CellPadding="3" PageSize="24"  SkinID="GridViewInfo"
                            OnRowDataBound="GVStudent_RowDataBound" EnableModelValidation="True" 
                            DataKeyNames="Did" onrowcommand="GVStudent_RowCommand">
            <Columns>
                <asp:BoundField HeaderText="序号" />
                <asp:BoundField DataField="Dnum" HeaderText="学号" />
                <asp:BoundField DataField="Dname" HeaderText="姓名" />
                <asp:BoundField DataField="Dyear" HeaderText="入学年度" />
                <asp:BoundField DataField="Dgrade" HeaderText="年级" />
                <asp:BoundField DataField="Dclass" HeaderText="班级" />
                <asp:BoundField DataField="Dsex" HeaderText="性别" />
                <asp:BoundField DataField="Dheadtheacher" HeaderText="班主任" />
                <asp:BoundField DataField="Dparents" HeaderText="父母" />
                <asp:BoundField DataField="Dphone" HeaderText="联系电话" />
                <asp:TemplateField HeaderText="操作" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="BtnRevive" runat="server" CausesValidation="false" 
                        CommandArgument='<%# Eval("Did") %>' CommandName="Revive" Text="恢复"></asp:LinkButton>
                        <asp:LinkButton ID="LinkBtnDel" runat="server" CausesValidation="false" 
                        CommandArgument='<%# Eval("Did") %>' CommandName="Del" Text="永久删除"></asp:LinkButton>
                    </ItemTemplate>

                </asp:TemplateField>
            </Columns>            
        </asp:GridView>
                    </div>
                    <p>
             <asp:LinkButton ID="LinkBtncancel" runat="server"  OnClick="LinkBtncancel_Click" SkinID="LinkBtn">返回</asp:LinkButton>
             </p>
    <p>
        （点操作列中恢复，可以将对应的已删除学生恢复到学生表中；恢复后的学生个人密码默认为12345）</p>
</asp:Content>


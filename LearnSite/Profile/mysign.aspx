<%@ Page Title="" Language="C#" MasterPageFile="~/Profile/Pf.master"   StylesheetTheme="Student" AutoEventWireup="true" CodeFile="mysign.aspx.cs" Inherits="Profile_mysign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cstu" Runat="Server">
<div>
签到列表：<asp:Label ID="Labelsignin" runat="server" Font-Size="9pt"></asp:Label>
        <br />
<asp:GridView ID="GVSignin" runat="server" AutoGenerateColumns="False"            
            CellPadding="2" PageSize="15"
            Width="100%" ToolTip="签到记录"  SkinID="GridViewInfo"
            onrowdatabound="GVSignin_RowDataBound" AllowPaging="True" 
        onpageindexchanging="GVSignin_PageIndexChanging" 
        EnableModelValidation="True" >
            <FooterStyle BackColor="White" ForeColor="#333333" />
            <Columns>
                <asp:BoundField />
                <asp:BoundField DataField="Qnum" HeaderText="学号" />
                <asp:BoundField DataField="Sgrade" HeaderText="年级" />
                <asp:BoundField DataField="Sclass" HeaderText="班级" />
                <asp:BoundField DataField="Sname" HeaderText="姓名" >
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Qwork" HeaderText="作品" />
                <asp:BoundField DataField="Qattitude" HeaderText="表现" />
                <asp:BoundField DataField="Qnote" HeaderText="备注" />
                <asp:BoundField DataField="Qip" HeaderText="IP地址" />
                <asp:BoundField DataField="Qdate" HeaderText="日期" >
                <ItemStyle Width="120px" HorizontalAlign="Left" />
                </asp:BoundField>
            </Columns>
            <PagerTemplate>
            <div  class="pagediv">
                第<asp:Label ID="lblPageIndex" runat="server" ForeColor="Black" 
                    Text="<%# ((GridView)Container.Parent.Parent).PageIndex + 1 %>"></asp:Label>
                页 共/<asp:Label ID="lblPageCount" runat="server" ForeColor="Black" 
                    Text="<%# ((GridView)Container.Parent.Parent).PageCount %>"></asp:Label>
                页 
                <asp:LinkButton ID="btnFirst" runat="server" CausesValidation="False" 
                    CommandArgument="First" CommandName="Page" Font-Underline="False" 
                    ForeColor="Black" Text="首页"></asp:LinkButton>
                <asp:LinkButton ID="btnPrev" runat="server" CausesValidation="False" 
                    CommandArgument="Prev" CommandName="Page" Font-Underline="False" 
                    ForeColor="Black" Text="上一页"></asp:LinkButton>
                <asp:LinkButton ID="btnNext" runat="server" CausesValidation="False" 
                    CommandArgument="Next" CommandName="Page" Font-Underline="False" 
                    ForeColor="Black" Text="下一页"></asp:LinkButton>
                <asp:LinkButton ID="btnLast" runat="server" CausesValidation="False" 
                    CommandArgument="Last" CommandName="Page" Font-Underline="False" 
                    ForeColor="Black" Text="尾页"></asp:LinkButton>
                &nbsp;&nbsp;
            </div>
        </PagerTemplate>
        </asp:GridView>
        <br />
        </div>
        <br />
<div>
缺席列表：<asp:Label ID="Labelnosign" runat="server" Font-Size="9pt"></asp:Label>
        <br />
        <asp:GridView ID="GVNoSign" runat="server" AutoGenerateColumns="False"
           CellPadding="2"  Width="100%"  ToolTip="缺席记录"  SkinID="GridViewInfo"
            onrowdatabound="GVNoSign_RowDataBound" DataKeyNames="Snum" 
        EnableModelValidation="True">
            <Columns>
                <asp:BoundField />
                <asp:BoundField DataField="Snum" HeaderText="学号" />
                <asp:BoundField DataField="Sgrade" HeaderText="年级" />
                <asp:BoundField DataField="Sclass" HeaderText="班级" />
                <asp:BoundField DataField="Sname" HeaderText="姓名" >
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Sex" HeaderText="性别" />
                <asp:BoundField DataField="Sheadtheacher" HeaderText="班主任" />
                <asp:BoundField HeaderText="缺席原因" DataField="Nnote" />
                <asp:BoundField DataField="Ndate" HeaderText="日期" >
                <ItemStyle HorizontalAlign="Left" Width="120px" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>

</div>
</asp:Content>


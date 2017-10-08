<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Stud.master"   StylesheetTheme="Student"  AutoEventWireup="true" CodeFile="typerclass.aspx.cs" Inherits="Student_typerclass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cphs" Runat="Server">
<div style="width: 860px; font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt;">
<div>
                <br />
                <asp:Label ID="Labeltitle" runat="server" Text="" Font-Bold="True"></asp:Label>
                <br />
                <br />
                年级选择：<asp:DropDownList 
                 ID="DDLgrade" runat="server" Width="50px" AutoPostBack="True" Font-Size="9pt" 
                    onselectedindexchanged="DDLgrade_SelectedIndexChanged" >
                            </asp:DropDownList>
            &nbsp;
            班级选择：<asp:DropDownList ID="DDLclass" runat="server" Width="50px" 
                    AutoPostBack="True" Font-Size="9pt" 
                    onselectedindexchanged="DDLclass_SelectedIndexChanged" ></asp:DropDownList>
                <br />
                </div>
                <div>
                <asp:GridView ID="GVTyper" runat="server" AutoGenerateColumns="False"  CellPadding="2" 
                    Width="100%" PageSize="25" 
                    OnRowDataBound="GVTyper_RowDataBound" AllowPaging="True" 
                    onpageindexchanging="GVTyper_PageIndexChanging" SkinID="GridViewInfo" 
                            EnableModelValidation="True">
                    <Columns>
                        <asp:BoundField HeaderText="名次" />
                        <asp:BoundField DataField="Ptid" HeaderText="文章" >
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Psnum" HeaderText="学号" />
                        <asp:BoundField DataField="Sname" HeaderText="姓名" >
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Sgrade" HeaderText="年级" />
                        <asp:BoundField DataField="Sclass" HeaderText="班级" />
                        <asp:BoundField DataField="Pscore" HeaderText="速度" >
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Font-Bold="True" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Ptype" HeaderText="次数" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Pip" HeaderText=" IP " >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Pdate" HeaderText="日期" >
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="120px" HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                    <PagerTemplate>
                        <div  class="pagediv">
                            第<asp:Label ID="lblPageIndex" runat="server" ForeColor="Black" Text="<%# ((GridView)Container.Parent.Parent).PageIndex + 1 %>"></asp:Label>/
                            <asp:Label ID="lblPageCount" runat="server" ForeColor="Black" Text="<%# ((GridView)Container.Parent.Parent).PageCount %>"></asp:Label>页
                            <asp:LinkButton ID="btnFirst" runat="server" CausesValidation="False" CommandArgument="First"
                                CommandName="Page" Font-Underline="False" ForeColor="Black" Text="首页"></asp:LinkButton>
                            <asp:LinkButton ID="btnPrev" runat="server" CausesValidation="False" CommandArgument="Prev"
                                CommandName="Page" Font-Underline="False" ForeColor="Black" Text="上页"></asp:LinkButton>
                            <asp:LinkButton ID="btnNext" runat="server" CausesValidation="False" CommandArgument="Next"
                                CommandName="Page" Font-Underline="False" ForeColor="Black" Text="下页"></asp:LinkButton>
                            <asp:LinkButton ID="btnLast" runat="server" CausesValidation="False" CommandArgument="Last"
                                CommandName="Page" Font-Underline="False" ForeColor="Black" Text="尾页"></asp:LinkButton>
                            &nbsp;&nbsp;
                        </div>
                    </PagerTemplate>
                </asp:GridView>
                </div>

</div>
</asp:Content>


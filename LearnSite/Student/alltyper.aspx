<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Stud.master"  StylesheetTheme="Student"  AutoEventWireup="true" CodeFile="alltyper.aspx.cs" Inherits="Student_alltyper" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cphs" Runat="Server">
<div id="student">
<div style="width: 800px; text-align: center;">
 <br />
                        <font size="5"><strong>中文输入英雄榜<br />
                        </strong></font>
    <asp:DropDownList ID="DDLselect" runat="server" AutoPostBack="True" 
        onselectedindexchanged="DDLselect_SelectedIndexChanged">
        <asp:ListItem Value="1">全校排行显示</asp:ListItem>
        <asp:ListItem Value="2">年级排行显示</asp:ListItem>
        <asp:ListItem Selected="True" Value="3">班级排行显示</asp:ListItem>
    </asp:DropDownList>
<br />
                    <div class="ccontent">
                <asp:GridView ID="GVTyper" runat="server" AutoGenerateColumns="False"  CellPadding="2" 
                    Width="100%" PageSize="40" 
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
                <br />
                        <asp:HyperLink ID="HLtyperank" runat="server" Font-Bold="True" 
                            Font-Size="X-Large" ForeColor="Black" NavigateUrl="~/Student/typerank.aspx" 
                            Target="_blank">打字擂台榜</asp:HyperLink>
                        <br />
        <br />
    </div>
</div>
</asp:Content>


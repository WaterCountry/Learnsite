<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Stud.master" StylesheetTheme="Student" AutoEventWireup="true" CodeFile="allfinger.aspx.cs" Inherits="Student_allfinger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cphs" Runat="Server">
<div id="student">
<div style="width: 800px; text-align: center;">
<center>
     <div class="ccontent">
                        <br />
                        <font size="4"><strong>英文输入英雄榜
                        </strong></font><br />
                        <asp:DropDownList ID="DDLselect" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="DDLselect_SelectedIndexChanged">
        <asp:ListItem Value="1">全校排行显示</asp:ListItem>
        <asp:ListItem Value="2">年级排行显示</asp:ListItem>
        <asp:ListItem Selected="True"  Value="3">班级排行显示</asp:ListItem>
    </asp:DropDownList>
    <br />
                <asp:GridView ID="GVFinger" runat="server" AutoGenerateColumns="False"  CellPadding="2" 
                    Width="480px" PageSize="40" 
                    OnRowDataBound="GVFinger_RowDataBound" AllowPaging="True" 
                    onpageindexchanging="GVFinger_PageIndexChanging" SkinID="GridViewInfo" 
                            EnableModelValidation="True">
                    <Columns>
                        <asp:BoundField HeaderText="名次" />
                        <asp:BoundField DataField="Psnum" HeaderText="学号" />
                        <asp:BoundField DataField="Sname" HeaderText="姓名" >
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Sgrade" HeaderText="年级" />
                        <asp:BoundField DataField="Sclass" HeaderText="班级" >
                        <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Pspd" HeaderText="速度" >
                        <ItemStyle Font-Bold="True" />
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
                        <br />
                        <asp:HyperLink ID="HLtyperank" runat="server" Font-Bold="True" 
                            Font-Size="Larger" ForeColor="Black" NavigateUrl="~/Student/typerank.aspx" 
                            Target="_blank">打字擂台榜</asp:HyperLink>
                        <br />
                        <br />
                </div>        
        <br />
        </center>
    </div>
</div>
</asp:Content>




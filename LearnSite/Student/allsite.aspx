<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Stud.master"  StylesheetTheme="Student"  CodeFile="allsite.aspx.cs" Inherits="Student_allsite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cphs" Runat="Server">
<div id="student">
<div class="left">
<div class="ccontent">
    <br />
    <asp:Image ID="Imagesite" runat="server" ImageUrl="~/Images/allsite.gif" />
&nbsp;&nbsp;
    <asp:DropDownList ID="DDLgrade" runat="server" AutoPostBack="True" 
        Font-Size="9pt" onselectedindexchanged="DDLgrade_SelectedIndexChanged" 
        Width="50px">
    </asp:DropDownList>
    年级 
    <asp:DropDownList ID="DDLclass" runat="server" AutoPostBack="True" 
        Font-Size="9pt" onselectedindexchanged="DDLclass_SelectedIndexChanged" 
        Width="50px">
    </asp:DropDownList>
    班 <br />
    <br />
     <asp:DataList ID="DataListvote" runat="server" RepeatDirection="Horizontal" 
                    RepeatColumns="8" DataKeyField="Wid" CellPadding="2">
                    <ItemTemplate>
                    <br />
                        <div  class="sitevote"> 
                        <div class="sitevotebg"><asp:HyperLink ID="Hypername" runat="server"  Text='<%# Eval("Sname") %>'  
                                Font-Underline="False" Height="18px"  ForeColor="Black"  Target="_blank" 
                                NavigateUrl='<%# Eval("Wurl") %>' ToolTip="点击查看网站" ></asp:HyperLink>
                                </div>
                                <br />
                            <asp:Label ID="WvoteLabel" runat="server" Text='<%# Eval("Wvote") %>' 
                                Height="16px" ></asp:Label>
                            <asp:Label ID="LabelWscore" runat="server" Text='<%# Eval("Wscore") %>' Visible="False"></asp:Label>
                            <asp:Label ID="LabelSnum" runat="server" Text='<%# Eval("Snum") %>' Visible="False"></asp:Label>
                                </div>
                    </ItemTemplate>
      </asp:DataList>
     <br />
    <asp:Label ID="Labelmsg" runat="server"></asp:Label>
    <br />
    </div>  
</div>
<div class="right">
<div>
    <asp:GridView ID="GVSite" runat="server" AllowPaging="True"  
        Caption="网站投票排行榜" CellPadding="2"         
        onpageindexchanging="GVSite_PageIndexChanging"
        OnRowDataBound="GVSite_RowDataBound" Width="90%" 
        AutoGenerateColumns="False" PageSize="20" SkinID="GridViewInfo">
        <Columns>
<asp:BoundField HeaderText="名次"></asp:BoundField>
            <asp:HyperLinkField DataNavigateUrlFields="Wurl" DataTextField="Sname" 
                HeaderText="姓名" Target="_blank" />
            <asp:BoundField DataField="Wvote" HeaderText="得票">
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
        </Columns>
        <PagerTemplate>
            <div style="color: black;  text-align:center">
            <asp:LinkButton ID="btnFirst" runat="server" CausesValidation="False" 
                    CommandArgument="First" CommandName="Page" Font-Underline="False" 
                    ForeColor="Black" Text="首页"></asp:LinkButton>
                <asp:LinkButton ID="btnPrev" runat="server" CausesValidation="False" 
                    CommandArgument="Prev" CommandName="Page" Font-Underline="False" 
                    ForeColor="Black" Text="上页"></asp:LinkButton>
                <asp:LinkButton ID="btnNext" runat="server" CausesValidation="False" 
                    CommandArgument="Next" CommandName="Page" Font-Underline="False" 
                    ForeColor="Black" Text="下页"></asp:LinkButton>
                <asp:LinkButton ID="btnLast" runat="server" CausesValidation="False" 
                    CommandArgument="Last" CommandName="Page" Font-Underline="False" 
                    ForeColor="Black" Text="尾页"></asp:LinkButton>               
            </div>
        </PagerTemplate>
        <PagerStyle Font-Size="9pt" />
    </asp:GridView>
    </div>
    <br />
    </div>
<br />
</div>
</asp:Content>


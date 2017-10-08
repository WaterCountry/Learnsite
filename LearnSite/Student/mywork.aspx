<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Stud.master" StylesheetTheme="Student" AutoEventWireup="true" CodeFile="mywork.aspx.cs" Inherits="Student_mywork" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cphs" Runat="Server">
    <div id="student">
<div class="left">
<div class="ccontent">
    <asp:GridView ID="GridViewworks" runat="server" AllowPaging="True" OnPageIndexChanging="GridViewworks_PageIndexChanging" 
        PageSize="15" Width="100%" SkinID="GridViewInfo" 
        onrowdatabound="GridViewworks_RowDataBound" AutoGenerateColumns="False" 
        Caption="我的作品" CaptionAlign="Left" EnableModelValidation="True">
        <Columns>
            <asp:BoundField DataField="Cobj" HeaderText="年级" >
            <ItemStyle Width="50px" />
            </asp:BoundField>
            <asp:BoundField DataField="Cterm" HeaderText="学期" >
            <ItemStyle Width="50px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="学案">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server" 
                        NavigateUrl='<%# Eval("Wcid", "~/Student/showcourse.aspx?Cid={0}") %>' 
                        Text='<%# Eval("Ctitle") %>' ></asp:HyperLink>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" Width="220px" />
            </asp:TemplateField>
            <asp:BoundField DataField="Mtitle" HeaderText="活动" >
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle Width="180px" HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:HyperLinkField DataNavigateUrlFields="Wid" 
                DataNavigateUrlFormatString="downwork.aspx?Wid={0}" HeaderText="作品" 
                Text="下载" Target="_blank">
            <ControlStyle Font-Underline="True" />
            <ItemStyle Width="30px" />
            </asp:HyperLinkField>
            <asp:BoundField DataField="Wscore" HeaderText="学分">
            <ItemStyle Width="30px" />
            </asp:BoundField>
            <asp:BoundField DataField="Wdscore" HeaderText="加分">
            <ItemStyle Width="30px" />
            </asp:BoundField>
            <asp:HyperLinkField DataNavigateUrlFields="Wmid,Wcid" 
                DataNavigateUrlFormatString="~/Student/myevaluate.aspx?Mid={0}&amp;Cid={1}" 
                HeaderText="互评" Target="_blank" Text="互评">
            <ItemStyle Width="30px" />
            </asp:HyperLinkField>
            <asp:BoundField DataField="Wvote" HeaderText="鲜花">
            <ItemStyle Width="30px" />
            </asp:BoundField>
            <asp:CheckBoxField DataField="Wcheck" HeaderText="评价" ReadOnly="True" >
            <ItemStyle Width="30px"  Height="24px"  />
            </asp:CheckBoxField >
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
</div>
<div class="right">
<div>
    <asp:GridView ID="GVScore" runat="server" AllowPaging="True"  
        Caption="本班积分排行榜" CellPadding="2"         
        onpageindexchanging="GVScore_PageIndexChanging"
        OnRowDataBound="GVScore_RowDataBound" Width="90%" SkinID="GridViewInfo" 
        AutoGenerateColumns="False" PageSize="20">
        <Columns>
<asp:BoundField HeaderText="名次"></asp:BoundField>
            <asp:BoundField HeaderText="姓名" DataField="Sname" >
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="Sscore" HeaderText="学分">
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
    <br />
    <asp:HyperLink ID="HLworks" runat="server" 
        NavigateUrl="~/Student/masterwork.aspx" Target="_blank" SkinID="HyperLink" 
        Width="120px" CssClass="txtszcenter" Height="20px">作品收藏</asp:HyperLink>
    <br />
    <br />
    </div>
<br />
</div>
</asp:Content>


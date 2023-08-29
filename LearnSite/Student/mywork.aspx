<%@ page title="" language="C#" masterpagefile="~/student/Stud.master" stylesheettheme="Student" autoeventwireup="true" inherits="Student_mywork, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cphs" Runat="Server">
    <div id="student">
<div class="left">
    <asp:GridView ID="GridViewworks" runat="server" AllowPaging="True" OnPageIndexChanging="GridViewworks_PageIndexChanging" 
        PageSize="15" Width="100%" SkinID="GridViewInfo" 
        onrowdatabound="GridViewworks_RowDataBound" AutoGenerateColumns="False" EnableModelValidation="True">
        <Columns>            
            <asp:TemplateField>
            <ItemTemplate>
                <asp:Image ID="ImageLeaf" runat="server" ImageUrl="~/images/fruit.gif" />
            </ItemTemplate>
            <ItemStyle Width="60px" />
            </asp:TemplateField>
               <asp:HyperLinkField DataNavigateUrlFields="cid" 
                   DataNavigateUrlFormatString="~/student/showcourse.aspx?cid={0}" 
                   DataTextField="ctitle" HeaderText="学案">
               <HeaderStyle HorizontalAlign="Left" />
               <ItemStyle HorizontalAlign="Left" />
               </asp:HyperLinkField>            
            <asp:BoundField DataField="Mtitle" HeaderText="活动" >
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle Width="280px" HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:HyperLinkField DataNavigateUrlFields="wid" 
                DataNavigateUrlFormatString="downwork.aspx?Wid={0}" HeaderText="作品" 
                Text="下载" Target="_blank">
            <ControlStyle Font-Underline="True" />
            <ItemStyle Width="40px" />
            </asp:HyperLinkField>
            <asp:BoundField DataField="Wscore" HeaderText="学分">
            <ItemStyle Width="40px" />
            </asp:BoundField>
            <asp:BoundField DataField="Wdscore" HeaderText="加分">
            <ItemStyle Width="40px" />
            </asp:BoundField>            
            <asp:BoundField DataField="Wvote" HeaderText="鲜花">
            <ItemStyle Width="40px" />
            </asp:BoundField>            
            <asp:BoundField DataField="cobj" HeaderText="年级" >
            <ItemStyle Width="40px" />
            </asp:BoundField>
            <asp:BoundField DataField="cterm" HeaderText="学期" >
            <ItemStyle Width="40px" />
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
        <RowStyle Height="32px" />
    </asp:GridView>
    <br />
</div>
<div class="right">
                <asp:GridView ID="Topwork" runat="server" AllowPaging="True" Width="100%" 
        SkinID="GridViewInfo" AutoGenerateColumns="False" Caption="最新优秀作品" 
                    EnableModelValidation="True" EmptyDataText=" " 
                    onrowdatabound="Topwork_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="序号" />
                        <asp:BoundField DataField="Wname" HeaderText="姓名" />
                        <asp:HyperLinkField DataNavigateUrlFields="wid" 
                            DataNavigateUrlFormatString="downwork.aspx?Wid={0}" HeaderText="作品" 
                            Text="查看" Target="_blank" />
                    </Columns>
                </asp:GridView>

    <br />
    <br />
    <asp:HyperLink ID="HLworks" runat="server" 
        NavigateUrl="~/student/masterwork.aspx" Target="_blank" SkinID="HyperLink" 
        Width="120px" CssClass="txtszcenter" Height="20px">作品收藏</asp:HyperLink>
    <br />
    <br />
    </div>
<br />
</div>
</asp:Content>


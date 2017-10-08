<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Stud.master" StylesheetTheme="Student" AutoEventWireup="true" CodeFile="mycourse.aspx.cs" Inherits="Student_mycourse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Cphs" Runat="Server">
<div id="student">
<div class="left">
<div class="ccontent">
    <asp:GridView ID="GridViewnewkc" runat="server" Width="100%" 
        SkinID="GridViewInfo" onrowdatabound="GridViewnewkc_RowDataBound" 
        AutoGenerateColumns="False" Caption="未学学案" CaptionAlign="Left" 
        EnableModelValidation="True" PageSize="5" AllowPaging="True" 
        onpageindexchanging="GridViewnewkc_PageIndexChanging" >
        <Columns>
            <asp:BoundField DataField="Cid">
            <ItemStyle Width="30px" ForeColor="White" />
            </asp:BoundField>
            <asp:BoundField DataField="Cclass" HeaderText="分类" >
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle Width="60px" HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Image ID="ImageLeaf" runat="server" ImageUrl="~/Images/leaf.gif" />
                </ItemTemplate>
                <ItemStyle Width="20px" />
            </asp:TemplateField>
            <asp:HyperLinkField DataNavigateUrlFields="Cid" 
                DataNavigateUrlFormatString="~/Student/showcourse.aspx?Cid={0}" DataTextField="Ctitle" 
                HeaderText="学案" >
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
            </asp:HyperLinkField>
            <asp:BoundField DataField="Cdate" HeaderText="日期" >
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" Width="80px" />
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
        <RowStyle Height="28px" />
    </asp:GridView>
    </div>
   <div class="ccontent">
       <asp:GridView ID="GridViewdonekc" runat="server" AllowPaging="True" 
           AutoGenerateColumns="False" Caption="已学学案" CaptionAlign="Left" 
           EnableModelValidation="True" 
           OnPageIndexChanging="GridViewdonekc_PageIndexChanging" 
           onrowdatabound="GridViewdonekc_RowDataBound" SkinID="GridViewInfo" 
           Width="100%" PageSize="5">
           <Columns>
               <asp:BoundField DataField="Cid">
               <ItemStyle ForeColor="White" Width="30px" />
               </asp:BoundField>
               <asp:BoundField DataField="Cclass" HeaderText="分类">
               <HeaderStyle HorizontalAlign="Left" />
               <ItemStyle HorizontalAlign="Left" Width="60px" />
               </asp:BoundField>
                <asp:TemplateField>
                <ItemTemplate>
                    <asp:Image ID="ImageLeaf" runat="server" ImageUrl="~/Images/fruit.gif" />
                </ItemTemplate>
                <ItemStyle Width="20px" />
            </asp:TemplateField>
               <asp:HyperLinkField DataNavigateUrlFields="Cid" 
                   DataNavigateUrlFormatString="~/Student/showcourse.aspx?Cid={0}" 
                   DataTextField="Ctitle" HeaderText="学案">
               <HeaderStyle HorizontalAlign="Left" />
               <ItemStyle HorizontalAlign="Left" />
               </asp:HyperLinkField>
               <asp:BoundField DataField="Cobj" HeaderText="年级" >
               <ItemStyle Width="40px" />
               </asp:BoundField>
               <asp:BoundField DataField="Cterm" HeaderText="学期" >
               <ItemStyle Width="40px" />
               </asp:BoundField>
           </Columns>
           <PagerTemplate>
               <div class="pagediv">
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
           <RowStyle Height="28px" />
       </asp:GridView>
    </div>
</div>
<div class="right">
 <div>    
    <asp:Image ID="Imageface" runat="server" Height="80px" Width="80px" />
    <div id="DivRank" class="divinfo" >
    <asp:Label ID="LabelRank" runat="server"></asp:Label>
    </div>
    </div> 
    <div class="divinfo">
    <div class="divinfo1">学号:</div>
    <div class="divinfo2"><asp:Label ID="snum" runat="server" ></asp:Label></div>
    </div>
    <div class="divinfo">
    <div class="divinfo1">班级:</div>
    <div class="divinfo2"><asp:Label ID="sclass" runat="server" ></asp:Label></div>
    </div>
    <div class="divinfo">
    <div class="divinfo1">姓名:</div>
    <div class="divinfo2"><asp:Label ID="sname" runat="server" ></asp:Label></div>
    </div>    
    <div class="divinfo">
    <div class="divinfo1">表现:</div>
    <div class="divinfo2">
        <asp:HyperLink ID="sattitude" runat="server" 
            NavigateUrl="~/Student/attituderank.aspx" Target="_blank" ToolTip="点击显示表现排行">[sattitude]</asp:HyperLink>
        </div>
    </div>
    <div class="divinfo">
    <div class="divinfo1">学分:</div>
    <div class="divinfo2"><asp:Label ID="sscore" runat="server" ></asp:Label></div>
    </div>
    <div class="divinfo">
    <div class="divinfo1">组长:</div>
    <div class="divinfo2"><asp:Label ID="sleadername" runat="server" ></asp:Label></div>
    </div>
    <div class="divinfo">
    <div class="divinfo1">MyIP:</div>
    <div class="divinfo2"><asp:Label ID="Labelip" runat="server"  SkinID="LabelSize8" ></asp:Label></div>
    </div> 
    <br />
    <asp:Label ID="LabelCids" runat="server" ForeColor="White"></asp:Label>
    <br />
    </div>
</div>
</asp:Content>


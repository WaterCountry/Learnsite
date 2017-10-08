<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="soft.aspx.cs" Inherits="Teacher_soft" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div  class="placehold">        
            <div>
            资源分类：<asp:DropDownList ID="ddlcategory" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddlcategory_SelectedIndexChanged">
        </asp:DropDownList>
                    <asp:Label ID="Label1" runat="server" Width="400px"></asp:Label>
                    <asp:HyperLink ID="Hlkadd" runat="server" CssClass="HyperlinkNormal" 
                    NavigateUrl="~/Teacher/softadd.aspx" Target="_self">资源添加</asp:HyperLink>
&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:HyperLink ID="Hlkcategory" runat="server" CssClass="HyperlinkNormal" 
                    NavigateUrl="~/Teacher/softcategory.aspx" Target="_self">分类设置</asp:HyperLink>
                    &nbsp;&nbsp;&nbsp;
                <asp:HyperLink ID="Hlkcgscore" runat="server" CssClass="HyperlinkNormal" 
                    NavigateUrl="~/Teacher/softnomic.aspx" Target="_blank">自学评价</asp:HyperLink>
                    </div>
            <div class="softdiv">
                <asp:GridView ID="GVSource" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False"  CellPadding="5" 
                    PageSize="20"  SkinID="GridViewInfo" Width="100%"
                    onpageindexchanging="GVSource_PageIndexChanging" 
                    onrowdatabound="GVSource_RowDataBound" EnableModelValidation="True" 
                    onrowcommand="GVSource_RowCommand">
                    <Columns>
                        <asp:BoundField HeaderText="序号" />
                        <asp:BoundField DataField="Fclass" HeaderText="属性" />
                        <asp:HyperLinkField DataNavigateUrlFields="Fid" 
                            DataNavigateUrlFormatString="~/Teacher/softview.aspx?Fid={0}" 
                            DataTextField="Ftitle" HeaderText="标题" />
                        <asp:BoundField DataField="Ffiletype" HeaderText="格式" />
                        <asp:BoundField DataField="Fhit" HeaderText="次数" />
                        <asp:BoundField DataField="Fopen" HeaderText="学分" />
                        <asp:HyperLinkField DataNavigateUrlFields="Furl" HeaderText="下载" Text="点击" 
                            Target="_blank" />
                        <asp:CheckBoxField DataField="Fhide" HeaderText="隐藏" ReadOnly="True" />
                        <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                            CommandArgument='<%# Eval("Fid") %>' CommandName="Change" 
                            ImageUrl="~/Images/refresh.gif" Text="更新" ToolTip="发布：无或隐藏：√" />
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:BoundField DataField="Fdate" HeaderText="日期" />
                        <asp:HyperLinkField DataNavigateUrlFields="Fid,Furl" 
                            DataNavigateUrlFormatString="~/Teacher/SoftDel.aspx?Fid={0}&amp;&amp;Furl={1}" 
                            Text="删除" />
                    </Columns>
                    <pagertemplate>
                        <div  class="pagediv">
                            第<asp:Label ID="lblPageIndex" runat="server" 
                                text="<%# ((GridView)Container.Parent.Parent).PageIndex + 1  %>" />
                            页  共<asp:Label ID="lblPageCount" runat="server" 
                                text="<%# ((GridView)Container.Parent.Parent).PageCount  %>" />
                            页 
                            <asp:LinkButton ID="btnFirst" runat="server" causesvalidation="False" 
                                commandargument="First" commandname="Page" Font-Underline="False" 
                                ForeColor="Black" text="首页" />
                            <asp:LinkButton ID="btnPrev" runat="server" causesvalidation="False" 
                                commandargument="Prev" commandname="Page" Font-Underline="False" 
                                ForeColor="Black" text="上一页" />
                            <asp:LinkButton ID="btnNext" runat="server" causesvalidation="False" 
                                commandargument="Next" commandname="Page" Font-Underline="False" 
                                ForeColor="Black" text="下一页" />
                            <asp:LinkButton ID="btnLast" runat="server" causesvalidation="False" 
                                commandargument="Last" commandname="Page" Font-Underline="False" 
                                ForeColor="Black" text="尾页" />
                        </div>
                    </pagertemplate>
                </asp:GridView>
            </div>
            <br />
    </div>
</asp:Content>


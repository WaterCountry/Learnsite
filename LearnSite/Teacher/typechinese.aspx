<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master"  StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="typechinese.aspx.cs" Inherits="Teacher_typechinese" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div   class="placehold">        
        <div  class="cheadright">
              <asp:Button ID="BtnTypeSet" runat="server"  Text="打字设置"  
                  onclick="BtnTypeSet_Click" SkinID="BtnNormal" />
        &nbsp;&nbsp;&nbsp;
              <asp:Button ID="BtnAdd" runat="server"  Text="词语添加"  onclick="BtnAdd_Click" 
                  SkinID="BtnNormal" />
        </div>
            <div  class="softdiv">
                <asp:GridView ID="GVType" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" CellPadding="5" SkinID="GridViewInfo"
                    PageSize="20" Width="98%" onpageindexchanging="GVType_PageIndexChanging" 
                    onrowdatabound="GVType_RowDataBound" EnableModelValidation="True">
                    <Columns>
                        <asp:BoundField HeaderText="序号" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                        </asp:BoundField>
                        <asp:HyperLinkField DataNavigateUrlFields="Nid" 
                            DataNavigateUrlFormatString="TypeChineseShow.aspx?Nid={0}" DataTextField="Ntitle" 
                            HeaderText="词语标题">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:HyperLinkField>
                        <asp:HyperLinkField DataNavigateUrlFields="Nid" 
                            DataNavigateUrlFormatString="TypeChineseEdit.aspx?Nid={0}" Text="编辑">
                        <ControlStyle Width="30px" />
                        <ItemStyle Width="40px" />
                        </asp:HyperLinkField>
                        <asp:HyperLinkField DataNavigateUrlFields="Nid" 
                            DataNavigateUrlFormatString="TypeChineseDel.aspx?Nid={0}" Text="删除" >
                        <ItemStyle Width="40px" />
                        </asp:HyperLinkField>
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


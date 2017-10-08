<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="works.aspx.cs" Inherits="Teacher_works" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div  class="placehold">
        <div class="chead" >
            &nbsp;作品选择：<asp:DropDownList ID="DDLgrade" runat="server" Font-Size="9pt" 
                EnableTheming="True" AutoPostBack="True" 
                onselectedindexchanged="DDLgrade_SelectedIndexChanged" Width="60px">
        </asp:DropDownList>
            年级&nbsp;
                    <asp:Label ID="Labelmsg" runat="server" Width="350px" Height="16px"></asp:Label>

                 <span id="pg" onclick="package()">作品打包&nbsp;&nbsp;</span>
        <link href="../Js/tinybox.css" rel="stylesheet" type="text/css" />
        <script src="../Js/tinybox.js" type="text/javascript"></script>
        <script type ="text/javascript" >
            function package() {
                var urlpg = "../Teacher/workpackage.aspx" ;
                TINY.box.show({ iframe: urlpg, boxid: 'frameless', width: 360, height: 240, fixed: false, maskopacity: 40, closejs: function () { closeJS() } })
            }
        </script>
                    <asp:Button ID="Btnterm" runat="server" Text="学期总评"  SkinID="BtnNormal" 
                onclick="Btnterm_Click" ToolTip="跳转到学期总评页面" />
        </div>
        <div>
        <div class="centerdiv">
            <asp:GridView ID="GVCourse" runat="server" AllowPaging="True" 
                AutoGenerateColumns="False"  CellPadding="6" DataKeyNames="Cid"  SkinID="GridViewInfo"
                PageSize="20" Width="100%" 
                onpageindexchanging="GVCourse_PageIndexChanging" 
                onrowdatabound="GVCourse_RowDataBound" EnableModelValidation="True">
                <Columns>
                    <asp:BoundField DataField="Cid" HeaderText="序号" InsertVisible="False" 
                        ReadOnly="True" SortExpression="Cid" >
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="50px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:HyperLinkField 
                        DataTextField="Ctitle" HeaderText="学案" >
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                    </asp:HyperLinkField>
                    <asp:BoundField DataField="Cclass" HeaderText="类型" SortExpression="Cclass" >
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle Width="60px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="未评数">
                        <ItemTemplate>
                            <asp:HyperLink ID="HlNoCheck" runat="server" ></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle Font-Bold="True" Width="60px" />
                    </asp:TemplateField>
                    <asp:HyperLinkField DataNavigateUrlFields="Cid,Cobj" 
                        DataNavigateUrlFormatString="workcheck.aspx?Cid={0}&amp;Grade={1}" 
                        Text="查看" HeaderText="评价" Target="_blank">
                    <ItemStyle Width="60px" />
                    </asp:HyperLinkField>
                    <asp:BoundField DataField="Cdate" HeaderText="日期" SortExpression="Cdate" >
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                    </asp:BoundField>
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
        </div>
        <br />
        <br />
    </div>
</asp:Content>


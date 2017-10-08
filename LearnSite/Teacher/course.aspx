<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master"   StylesheetTheme="Teacher" AutoEventWireup="true"  CodeFile="course.aspx.cs" Inherits="Teacher_course" %>
<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div  >
        <div  class="chead">
            &nbsp;学案选择：<asp:DropDownList ID="DDLgrade" runat="server" Font-Size="9pt" 
            Width="60px" EnableTheming="True" AutoPostBack="True" 
                onselectedindexchanged="DDLgrade_SelectedIndexChanged">
        </asp:DropDownList>
            年级&nbsp;
                    <asp:Label ID="Labelmsg" runat="server" Width="220px" Height="16px"></asp:Label>
                    &nbsp;<asp:DropDownList ID="DDLterm" runat="server" Font-Size="9pt" 
                    EnableTheming="True" AutoPostBack="True" 
            onselectedindexchanged="DDLterm_SelectedIndexChanged" 
                ToolTip="选择要显示学案的学期，不改变后台默认学期设置">
                <asp:ListItem Value="1">第一学期</asp:ListItem>
                <asp:ListItem Value="2">第二学期</asp:ListItem>
        </asp:DropDownList>
            &nbsp;
                    <asp:Label ID="Labelspace" runat="server" Width="120px" Height="16px"></asp:Label>
                    <asp:Button ID="Btnadd" runat="server"  Text="添加学案"  onclick="Btnadd_Click" SkinID="BtnNormal" />                    
                    </div>
                    <div >
                    <div class="centerdiv">
                    <anthem:GridView ID="GVCourse" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False"  DataKeyNames="Cid"  
                            PageSize="20" Width="100%"
                            onpageindexchanging="GVCourse_PageIndexChanging" 
                            onrowdatabound="GVCourse_RowDataBound" CellPadding="6" 
                            EnableModelValidation="True" Font-Size="9pt" 
                            onrowcommand="GVCourse_RowCommand" ForeColor="#111111" GridLines="None" >
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>                                
                                <asp:BoundField DataField="Cks" HeaderText="课节">
                                <ControlStyle Width="20px" />
                                </asp:BoundField>
                                <asp:HyperLinkField DataNavigateUrlFields="Cid" 
                                    DataNavigateUrlFormatString="~/Teacher/CourseShow.aspx?Cid={0}" 
                                    DataTextField="Ctitle" HeaderText="学案" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:HyperLinkField>
                                <asp:BoundField DataField="Cclass" HeaderText="类型" SortExpression="Cclass" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:HyperLinkField DataNavigateUrlFields="Cid"                                     
                                    DataNavigateUrlFormatString="~/Teacher/Package.aspx?Cid={0}" HeaderText="打包" 
                                    Text="下载" />
                                <asp:TemplateField HeaderText="发布" ShowHeader="False">
                                    <ItemTemplate>
                                        <anthem:LinkButton ID="LbtnCpublish" runat="server" CausesValidation="false" 
                                   CommandArgument='<%# Bind("Cid") %>'  CommandName="Cp" Text='<%# Eval("Cpublish") %>'></anthem:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:HyperLinkField DataNavigateUrlFields="Cid" 
                                    DataNavigateUrlFormatString="~/Teacher/courseanalyse.aspx?Cid={0}" Text="分析" />
                                <asp:TemplateField HeaderText="探讨">
                                    <ItemTemplate>                                    
                                        <asp:HyperLink ID="Hl" runat="server" Text="反思" ForeColor="Blue"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="推荐" ShowHeader="False">
                                    <ItemTemplate>
                                        <anthem:LinkButton ID="LbtnCgood" runat="server" CausesValidation="false" 
                                   CommandArgument='<%# Bind("Cid") %>'  CommandName="Cg" ToolTip="默认为True，学生平台作品收藏学案列表中显示；False则不显示!" Text='<%# Eval("Cgood") %>'></anthem:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:HyperLinkField DataNavigateUrlFields="Cid" 
                                    DataNavigateUrlFormatString="~/Teacher/CourseEdit.aspx?Cid={0}" Text="编辑">
                                <ItemStyle Width="30px" />
                                </asp:HyperLinkField>
                                <asp:TemplateField HeaderText="日期" SortExpression="Cdate">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" 
                                            Text='<%# DataBinder.Eval(Container.DataItem,"Cdate","{0:d}")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="70px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="入库" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LbtnCold" runat="server" CausesValidation="false" 
                                          CommandArgument='<%# Bind("Cid") %>'  ToolTip="转移到学案仓库中保留" CommandName="Cu" Text="转移"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ControlStyle Width="30px" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#E7E7E7" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#9EA9B1" Font-Bold="True" ForeColor="#111111" />
                            <PagerStyle BackColor="#EFEFEF" ForeColor="#111111" HorizontalAlign="Center" />
                            <pagertemplate>
                                <div style="width:100%; height:13px; text-align:right">
                                    第<asp:Label ID="lblPageIndex" runat="server" 
                                        text="<%# ((Anthem.GridView)Container.Parent.Parent).PageIndex + 1  %>" />
                                    页 共<asp:Label ID="lblPageCount" runat="server" 
                                        text="<%# ((Anthem.GridView)Container.Parent.Parent).PageCount  %>" />
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
                            <RowStyle BackColor="#E7E7E7" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        </anthem:GridView>
                        </div>
                    </div>
                    <div style="height: 10px" ></div>
                    <div style="text-align: right; ">
                    <asp:Button ID="Btnimport"  runat="server"  Text="导入学案"  onclick="Btnimport_Click" SkinID="BtnNormal" />                    
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Btnold"  runat="server"  Text="学案仓库"  onclick="Btnold_Click" 
                            SkinID="BtnNormal" />                    
                    &nbsp;                    
                    &nbsp;                    
        <link href="../Js/tinybox.css" rel="stylesheet" type="text/css" />
        <script src="../Js/tinybox.js" type="text/javascript"></script>
        <script type ="text/javascript" >
            function tshow(c) {
                var urlat = "../Lessons/ThinkShow.aspx?Cid=" + c;
                TINY.box.show({ iframe: urlat, boxid: 'frameless', width: 800, height: 500, fixed: false, maskopacity: 40, closejs: function () { closeJS() } })
            }
        </script>               
                    </div>
    </div>
</asp:Content>


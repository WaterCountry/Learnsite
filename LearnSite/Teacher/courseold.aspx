<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master"  StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="courseold.aspx.cs" Inherits="Teacher_courseold" %>
<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div  >
        <div  class="chead">
            &nbsp;学案仓库：<asp:DropDownList ID="DDLgrade" runat="server" Font-Size="9pt" 
            Width="60px" EnableTheming="True" AutoPostBack="True" 
                onselectedindexchanged="DDLgrade_SelectedIndexChanged">
        </asp:DropDownList>
            年级&nbsp;
                    <asp:Label ID="Labelmsg" runat="server" Width="220px" Height="16px" 
                Font-Bold="False"></asp:Label>
                    &nbsp;<asp:DropDownList ID="DDLterm" runat="server" Font-Size="9pt" 
                    EnableTheming="True" AutoPostBack="True" 
            onselectedindexchanged="DDLterm_SelectedIndexChanged" 
                ToolTip="选择要显示学案的学期，不改变后台默认学期设置">
                <asp:ListItem Value="1">第一学期</asp:ListItem>
                <asp:ListItem Value="2">第二学期</asp:ListItem>
        </asp:DropDownList>
            &nbsp;
                    <asp:Label ID="Labelspace" runat="server" Width="120px" Height="16px"></asp:Label>
                    <asp:Button ID="Btnreturn" runat="server"  Text="返回"  
                onclick="Btnreturn_Click" SkinID="BtnNormal" BackColor="#A9BCAB" />                    
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
                                <asp:BoundField DataField="Cobj" HeaderText="年级">
                                <ControlStyle Width="20px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Cterm" HeaderText="学期" />
                                <asp:BoundField DataField="Cks" HeaderText="课节">
                                <ControlStyle Width="20px" />
                                </asp:BoundField>
                                <asp:HyperLinkField DataNavigateUrlFields="Cid" 
                                    DataNavigateUrlFormatString="~/Teacher/CourseShow.aspx?Cid={0}&amp;Cold=T" 
                                    DataTextField="Ctitle" HeaderText="仓库学案" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:HyperLinkField>
                                <asp:BoundField DataField="Cclass" HeaderText="类型" SortExpression="Cclass" />
                                <asp:TemplateField HeaderText="操作" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" 
                                        CommandArgument='<%# Bind("Cid") %>'  CommandName="U" ToolTip="将此学案重新启用，在学案列表中显示出来" Text="启用"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="日期" SortExpression="Cdate">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" 
                                            Text='<%# DataBinder.Eval(Container.DataItem,"Cdate","{0:d}")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="70px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:HyperLinkField DataNavigateUrlFields="Cid" 
                                    DataNavigateUrlFormatString="~/teacher/CourseDel.aspx?Cid={0}" Text="删除">
                                <ItemStyle Width="30px" />
                                </asp:HyperLinkField>
                            </Columns>
                            <FooterStyle BackColor="#E7E7E7" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#A9BCAB" Font-Bold="True" ForeColor="#111111" />
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
                            <RowStyle BackColor="#EFF5F0" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        </anthem:GridView>
                        </div>
                    </div>
                    <div style="height: 10px" ></div>
                    <div style="text-align: center; color: #006600;">
                        ***仓库中学案只能浏览，不能编辑！***</div>
    </div>
</asp:Content>


<%@ page title="" language="C#" masterpagefile="~/teacher/Teach.master" stylesheettheme="Teacher" autoeventwireup="true" inherits="Teacher_signin, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div  class="placehold">
        <div  class="chead">
            签到选择：<asp:DropDownList ID="DDLgrade" runat="server" 
            Width="50px" EnableTheming="True" AutoPostBack="True" 
                onselectedindexchanged="DDLgrade_SelectedIndexChanged">
        </asp:DropDownList>
            年级 
            <asp:DropDownList ID="DDLclass" runat="server" 
            Width="50px" EnableTheming="True" AutoPostBack="True" 
                onselectedindexchanged="DDLclass_SelectedIndexChanged">
        </asp:DropDownList>
            班级&nbsp;
            <asp:Button ID="BtnExcel" runat="server"  OnClick="BtnExcel_Click" 
                Text="导出签到Excel"  SkinID="BtnLong" ToolTip="将本学期本班签到以Excel表格导出" 
                Width="120px" />
                    &nbsp;&nbsp;
            <asp:Button ID="BtnExcelNoSign" runat="server"  OnClick="BtnExcelNoSign_Click" 
                Text="导出缺席Excel"  SkinID="BtnLong" ToolTip="将本学期本班缺席以Excel表格导出" 
                Width="120px" />
                    </div>
                    <div>
                    <div class="centerdiv">
                        <asp:GridView ID="GVSignin" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False"  PageSize="20" Width="100%" 
                            onpageindexchanging="GVSignin_PageIndexChanging"  SkinID="GridViewInfo"
                            onrowdatabound="GVSignin_RowDataBound" CellPadding="5"> 
                            <Columns>
                                <asp:BoundField HeaderText="序号" />
                                <asp:BoundField DataField="Sgrade" HeaderText="年级" />
                                <asp:BoundField DataField="Sclass" HeaderText="班级" />
                                <asp:BoundField DataField="Qyear" HeaderText="年份" />
                                <asp:BoundField DataField="Qmonth" HeaderText="月份" />
                                <asp:BoundField DataField="Qday" HeaderText="日份" />
                                <asp:HyperLinkField DataNavigateUrlFields="Sgrade,Sclass,Qyear,Qmonth,Qday" 
                                    DataNavigateUrlFormatString="signshow.aspx?sgrade={0}&amp;&amp;sclass={1}&amp;&amp;qyear={2}&amp;&amp;qmonth={3}&amp;&amp;qday={4}" 
                                    Text="详细" />
                            </Columns>
                            <PagerTemplate>
                                <div style="width: 100%; height: 13px; text-align: right">
                                    第<asp:Label ID="lblPageIndex" runat="server" 
                                        Text="<%# ((GridView)Container.Parent.Parent).PageIndex + 1  %>"></asp:Label>
                                    页 共页 共<asp:Label ID="lblPageCount" runat="server" 
                                        Text="<%# ((GridView)Container.Parent.Parent).PageCount  %>"></asp:Label>
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
                                </div>
                            </PagerTemplate>
                        </asp:GridView>
                        </div>
                        <br />
                    </div>
    </div>
</asp:Content>


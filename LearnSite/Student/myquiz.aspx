<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Stud.master"  StylesheetTheme="Student" AutoEventWireup="true" CodeFile="myquiz.aspx.cs" Inherits="Student_myquiz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cphs" Runat="Server">
    <div id="student">
<div class="left">
<div id="divscore"  class="quizscoreheight">
<div class="divquizscore">
        <asp:GridView ID="GridViewgrade" runat="server" AllowPaging="True" Width="100%"  
        SkinID="GridViewInfo" AutoGenerateColumns="False" PageSize="20" 
        onpageindexchanging="GridViewgrade_PageIndexChanging" 
        onrowdatabound="GridViewgrade_RowDataBound" Caption="年级成绩榜">
                    <Columns>
                        <asp:BoundField HeaderText="编号" />
                        <asp:BoundField DataField="Sgradeclass" HeaderText="班级" />
                        <asp:BoundField DataField="Sname" HeaderText="姓名" />
                        <asp:BoundField DataField="Squiz" HeaderText="学分" />
                    </Columns>
                    <PagerTemplate>
                        <div class="pagediv">
                            第<asp:Label ID="lblPageIndex" runat="server" ForeColor="Black" 
                                Text="<%# ((GridView)Container.Parent.Parent).PageIndex + 1 %>"></asp:Label>
                            页 共<asp:Label ID="lblPageCount" runat="server" ForeColor="Black" 
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
                </div>
                <br />
</div>
<div id="divwork" class="quizscoreheight">
<div class="divquizscore">
        <asp:GridView ID="GridViewclass" runat="server" AllowPaging="True" Width="100%"  
        SkinID="GridViewInfo" AutoGenerateColumns="False" PageSize="20" 
        onpageindexchanging="GridViewclass_PageIndexChanging" 
        onrowdatabound="GridViewclass_RowDataBound" Caption="班级成绩榜">
                    <Columns>
                        <asp:BoundField HeaderText="编号" />
                        <asp:BoundField DataField="Sgradeclass" HeaderText="班级" />
                        <asp:BoundField DataField="Sname" HeaderText="姓名" />
                        <asp:BoundField DataField="Squiz" HeaderText="学分" />
                    </Columns>
                    <PagerTemplate>
                        <div class="pagediv">
                            第<asp:Label ID="lblPageIndex" runat="server" ForeColor="Black" 
                                Text="<%# ((GridView)Container.Parent.Parent).PageIndex + 1 %>"></asp:Label>
                            页 共<asp:Label ID="lblPageCount" runat="server" ForeColor="Black" 
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
                </div>
                <br />
</div>
 <br /> 
</div>
<div class="right">
<div>
    
    <asp:GridView ID="GVmyScore" runat="server" AllowPaging="True"  
        Caption="我的测验记录" CellPadding="2"         
        onpageindexchanging="GVmyScore_PageIndexChanging"
        OnRowDataBound="GVmyScore_RowDataBound" Width="90%" SkinID="GridViewInfo" 
        AutoGenerateColumns="False" EnableModelValidation="True">
        <Columns>
            <asp:BoundField HeaderText="日期" DataField="Rdate" />
            <asp:HyperLinkField DataNavigateUrlFields="Rid" 
                DataNavigateUrlFormatString="quizview.aspx?Rid={0}" Target="_blank" 
                DataTextField="Rscore" HeaderText="成绩"  />
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

    <div class="quizresult">
        <br /> 
        <div class="quizinfo">
            <div  class="quizhead">我的测验平均成绩</div>
                <br />
                <asp:Label ID="LabelSquiz" runat="server" ></asp:Label>
                <br />
                <br />  
                </div>      
        <br />
                <asp:Button ID="Btnquiz" runat="server" OnClick="Btnquiz_Click"
                    Text="开始测验" Width="80px" CausesValidation="False" 
            CssClass="buttonimg" Font-Bold="False" BorderStyle="None"/>
                <br />
        <br />
        <asp:Label ID="Labelmsg" runat="server" Font-Size="9pt"></asp:Label>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" 
            NavigateUrl="~/Student/quizrank.aspx" Target="_blank" SkinID="HyperLink" 
            Width="120px" CssClass="txtszcenter" Height="18px">今天测验排行榜</asp:HyperLink>
        <br />
        <br />
        <br />
    </div>   
    <br />
    <br />
    </div>
<br />
</div>
</asp:Content>


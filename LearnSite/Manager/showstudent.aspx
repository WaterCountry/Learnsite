<%@ Page Title="" Language="C#" MasterPageFile="~/Manager/Manage.master" AutoEventWireup="true" CodeFile="showstudent.aspx.cs" Inherits="Manager_showstudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div class="manageplace" >
           <asp:GridView ID="GVstudent" runat="server" BorderColor="#E0E0E0"
            BorderStyle="Solid" BorderWidth="1px" Font-Size="9pt" OnPageIndexChanging="GVstudent_PageIndexChanging"
            Width="98%" GridLines="None" AllowPaging="True" CellPadding="2" PageSize="25" 
                            OnRowDataBound="GVstudent_RowDataBound" 
               AutoGenerateColumns="False">
               <Columns>
                   <asp:BoundField DataField="Snum" HeaderText="学号" />
                   <asp:BoundField DataField="Syear" HeaderText="入学年度" />
                   <asp:BoundField DataField="Sgrade" HeaderText="年级" />
                   <asp:BoundField DataField="Sclass" HeaderText="班级" />
                   <asp:BoundField DataField="Sname" HeaderText="姓名" />
                   <asp:BoundField DataField="Spwd" HeaderText="密码" />
                   <asp:BoundField DataField="Sex" HeaderText="性别" />
                   <asp:BoundField DataField="Saddress" HeaderText="家庭住址" />
                   <asp:BoundField DataField="Sphone" HeaderText="联系电话" />
                   <asp:BoundField DataField="Sparents" HeaderText="家长姓名" />
                   <asp:BoundField DataField="Sheadtheacher" HeaderText="班主任" />
               </Columns>
            <pagertemplate>
                           <div style="width:100%; height:13px; text-align:right">
                            第<asp:Label id="lblPageIndex" runat="server" text='<%# ((GridView)Container.Parent.Parent).PageIndex + 1  %>' />页
                               共<asp:Label id="lblPageCount" runat="server" text='<%# ((GridView)Container.Parent.Parent).PageCount  %>' />页 
                                <asp:linkbutton id="btnFirst" runat="server" causesvalidation="False" commandargument="First" commandname="Page" text="首页" Font-Underline="False" ForeColor="Black" />
                              <asp:linkbutton id="btnPrev" runat="server" causesvalidation="False" commandargument="Prev" commandname="Page" text="上一页" Font-Underline="False" ForeColor="Black" />
                             <asp:linkbutton id="btnNext" runat="server" causesvalidation="False" commandargument="Next" commandname="Page" text="下一页" Font-Underline="False" ForeColor="Black" />                          
                             <asp:linkbutton id="btnLast" runat="server" causesvalidation="False" commandargument="Last" commandname="Page" text="尾页" Font-Underline="False" ForeColor="Black" />
                               
                           </div>
                    </pagertemplate>
                            <RowStyle Font-Size="9pt" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#EFEFEF" Font-Size="9pt" ForeColor="Black" 
                                HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#EFEFEF" Font-Bold="False" Font-Size="9pt" 
                                ForeColor="#222222" />
                            <AlternatingRowStyle BackColor="#EFEFEF" BorderStyle="None" />
        </asp:GridView>

    <br />
            <asp:Button ID="ButtonReturn" runat="server" BorderColor="Silver" BorderStyle="Solid"
                BorderWidth="1px" Font-Names="Arial" Font-Size="9pt" Height="18px" OnClick="ButtonInsert_Click"
                TabIndex="1" Text="返回" Width="100px" BackColor="#E8E8E8" />
            
            <br />
            
            <br />            
    临时表内容<asp:Label ID="Labelcount" runat="server" Font-Names="Arial"></asp:Label>
            <br />
            <br />

</div>
</asp:Content>


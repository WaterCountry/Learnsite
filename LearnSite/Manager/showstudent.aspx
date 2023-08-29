<%@ page title="" language="C#" masterpagefile="~/manager/Manage.master" autoeventwireup="true" inherits="Manager_showstudent, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div class="manageplace" >
           <asp:GridView ID="GVstudent" runat="server" BorderColor="#E0E0E0"
            BorderStyle="Solid" BorderWidth="1px" Font-Size="9pt" OnPageIndexChanging="GVstudent_PageIndexChanging"
            Width="98%" GridLines="None" AllowPaging="True" CellPadding="2" PageSize="25" 
                            OnRowDataBound="GVstudent_RowDataBound" 
               AutoGenerateColumns="False">
               <Columns>
                   <asp:BoundField DataField="Snum" HeaderText="ѧ��" />
                   <asp:BoundField DataField="Syear" HeaderText="��ѧ���" />
                   <asp:BoundField DataField="Sgrade" HeaderText="�꼶" />
                   <asp:BoundField DataField="Sclass" HeaderText="�༶" />
                   <asp:BoundField DataField="Sname" HeaderText="����" />
                   <asp:BoundField DataField="Spwd" HeaderText="����" />
                   <asp:BoundField DataField="Sex" HeaderText="�Ա�" />
                   <asp:BoundField DataField="Saddress" HeaderText="��ͥסַ" />
                   <asp:BoundField DataField="Sphone" HeaderText="��ϵ�绰" />
                   <asp:BoundField DataField="Sparents" HeaderText="�ҳ�����" />
                   <asp:BoundField DataField="Sheadtheacher" HeaderText="������" />
               </Columns>
            <pagertemplate>
                           <div style="width:100%; height:13px; text-align:right">
                            ��<asp:Label id="lblPageIndex" runat="server" text='<%# ((GridView)Container.Parent.Parent).PageIndex + 1  %>' />ҳ
                               ��<asp:Label id="lblPageCount" runat="server" text='<%# ((GridView)Container.Parent.Parent).PageCount  %>' />ҳ 
                                <asp:linkbutton id="btnFirst" runat="server" causesvalidation="False" commandargument="First" commandname="Page" text="��ҳ" Font-Underline="False" ForeColor="Black" />
                              <asp:linkbutton id="btnPrev" runat="server" causesvalidation="False" commandargument="Prev" commandname="Page" text="��һҳ" Font-Underline="False" ForeColor="Black" />
                             <asp:linkbutton id="btnNext" runat="server" causesvalidation="False" commandargument="Next" commandname="Page" text="��һҳ" Font-Underline="False" ForeColor="Black" />                          
                             <asp:linkbutton id="btnLast" runat="server" causesvalidation="False" commandargument="Last" commandname="Page" text="βҳ" Font-Underline="False" ForeColor="Black" />
                               
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
                TabIndex="1" Text="����" Width="100px" BackColor="#E8E8E8" />
            
            <br />
            
            <br />            
    ��ʱ������<asp:Label ID="Labelcount" runat="server" Font-Names="Arial"></asp:Label>
            <br />
            <br />

</div>
</asp:Content>


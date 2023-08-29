<%@ page title="" language="C#" masterpagefile="~/student/Stud.master" stylesheettheme="Student" autoeventwireup="true" inherits="Student_myfile, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cphs" Runat="Server">
    <div id="student">
<div class="left">
      <asp:GridView ID="GVSoft" runat="server" AllowPaging="True" 
          AutoGenerateColumns="False" 
          OnPageIndexChanging="GVSoft_PageIndexChanging" 
          OnRowDataBound="GVSoft_RowDataBound" Width="100%" SkinID="GridViewInfo" 
          PageSize="20" EnableModelValidation="True" CellPadding="3">
          <AlternatingRowStyle BorderStyle="None" />
          <Columns>
              <asp:BoundField HeaderText="序号" />
              <asp:BoundField DataField="Fclass" HeaderText="属性" />
              <asp:HyperLinkField DataNavigateUrlFields="fid" 
                  DataNavigateUrlFormatString="downfile.aspx?fid={0}" HeaderText="标题" 
                  DataTextField="Ftitle">
                  <HeaderStyle HorizontalAlign="Left" />
              <ItemStyle Width="280px" HorizontalAlign="Left" />
              </asp:HyperLinkField>
              <asp:BoundField DataField="Ffiletype" HeaderText="格式" />
              <asp:BoundField DataField="Fhit" HeaderText="次数" />
              <asp:BoundField DataField="Fdate" HeaderText="日期">
              <ItemStyle Width="120px" />
              </asp:BoundField>
          </Columns>
          <HeaderStyle Height="30px" />
          <pagertemplate>
              <div  class="pagediv">
                  第<asp:Label ID="lblPageIndex" runat="server" 
                      text="<%# ((GridView)Container.Parent.Parent).PageIndex + 1  %>" />
                  页 共页 共<asp:Label ID="lblPageCount" runat="server" 
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
          <RowStyle Height="30px" />
      </asp:GridView><br /><br />        
</div>
<div class="right">
    <div >
        <asp:GridView ID="GVcategory" runat="server" AutoGenerateColumns="False" 
            CellPadding="6" CellSpacing="3" EnableModelValidation="True" 
            HorizontalAlign="Center" ShowHeader="False" 
            SkinID="GridViewMission" Width="100%" DataKeyNames="yid" 
            onrowdatabound="GVcategory_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <img src="../images/book.gif" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField DataNavigateUrlFields="yid" 
                    DataNavigateUrlFormatString="~/student/myfile.aspx?yid={0}" 
                    DataTextField="Ytitle" Target="_self" >
                <ItemStyle HorizontalAlign="Left" />
                </asp:HyperLinkField>
            </Columns>
            <RowStyle BackColor="#CEDFEA" Height="30px" />
        </asp:GridView>
        <img src="../images/soft.png" style="width: 160px; height: 80px; text-align:center;" /><br />
        <br /></div>
</div>   
<br />
</div>
</asp:Content>


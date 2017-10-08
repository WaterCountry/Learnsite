<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="typer.aspx.cs" Inherits="Teacher_typer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div   class="placehold">        
        <div  class="cheadright">
              <asp:Button ID="BtnTypeSet" runat="server"  Text="打字设置"  
                  onclick="BtnTypeSet_Click" SkinID="BtnNormal" />
        &nbsp;&nbsp;&nbsp;
              <asp:Button ID="BtnAdd" runat="server"  Text="文章添加"  onclick="BtnAdd_Click" SkinID="BtnNormal" />
        </div>
            <div  class="softdiv">
                <asp:GridView ID="GVType" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" CellPadding="5" SkinID="GridViewInfo"
                    PageSize="20" Width="98%" onpageindexchanging="GVType_PageIndexChanging" 
                    onrowdatabound="GVType_RowDataBound" EnableModelValidation="True">
                    <Columns>
                        <asp:BoundField HeaderText="序号" />
                        <asp:HyperLinkField DataNavigateUrlFields="Tid" 
                            DataNavigateUrlFormatString="TypeShow.aspx?Tid={0}" DataTextField="Ttitle" 
                            HeaderText="文章标题">
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="Ttype" HeaderText="文章类型" />
                        <asp:BoundField DataField="Tuse" HeaderText="文章范围">
                        <ControlStyle Width="30px" />
                        </asp:BoundField>
                        <asp:HyperLinkField DataNavigateUrlFields="Tid" 
                            DataNavigateUrlFormatString="TypeEdit.aspx?Tid={0}" Text="编辑">
                        <ControlStyle Width="30px" />
                        </asp:HyperLinkField>
                        <asp:HyperLinkField DataNavigateUrlFields="Tid" 
                            DataNavigateUrlFormatString="TypeDel.aspx?Tid={0}" Text="删除" />
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
            <div  >
                <asp:Label ID="labelmsg" runat="server" SkinID="LabelMsgRed" Width="160px"></asp:Label>
                <br />
                <br />
                <asp:DropDownList ID="DDLpscore" runat="server" Font-Size="9pt" Width="60px">
                    <asp:ListItem>200</asp:ListItem>
                    <asp:ListItem Selected="True">100</asp:ListItem>
                    <asp:ListItem>300</asp:ListItem>
                    <asp:ListItem>400</asp:ListItem>
                    <asp:ListItem>500</asp:ListItem>
                </asp:DropDownList>
                以上速度<asp:Button ID="ButtonClearThis" runat="server"  Text="清除"   SkinID="BtnSmall" 
                    onclick="ButtonClearThis_Click" ToolTip="清除超过指定速度的中文打字成绩" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Button ID="ButtonClearType" runat="server"  Text="清除中文打字成绩"   SkinID="BtnLong" 
                    onclick="ButtonClearType_Click" Width="120px" />
                &nbsp;&nbsp;
              <asp:Button ID="ButtonClearFinger" runat="server"  Text="清除指法打字成绩"   SkinID="BtnLong" 
                    onclick="ButtonClearFinger_Click" Width="120px" />
                &nbsp;&nbsp;&nbsp;
                <asp:HyperLink ID="HLprint" runat="server" 
                    NavigateUrl="~/Teacher/printtyper.aspx" Target="_blank" Height="18px">排行榜打印</asp:HyperLink>
                &nbsp;&nbsp;
                <asp:HyperLink ID="HLfinger" runat="server" 
                    NavigateUrl="~/en.aspx" Target="_blank" Height="18px">指法英文字典</asp:HyperLink>
                <br />
                <br />
        </div>
            <br />
    </div>
</asp:Content>


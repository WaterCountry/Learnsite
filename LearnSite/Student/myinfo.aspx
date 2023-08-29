<%@ page title="" language="C#" masterpagefile="~/student/Stud.master" stylesheettheme="Student" autoeventwireup="true" inherits="Student_myinfo, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cphs" Runat="Server">
    <div id="student">
<div class="left">
    <asp:GridView ID="GridViewnewkc" runat="server" Width="100%" 
        SkinID="GridViewInfo" onrowdatabound="GridViewnewkc_RowDataBound" 
        AutoGenerateColumns="False" 
        EnableModelValidation="True" PageSize="5" AllowPaging="True" 
        onpageindexchanging="GridViewnewkc_PageIndexChanging" >
        <Columns>
            <asp:BoundField DataField="cid"  Visible="false">
            <ItemStyle Width="30px" ForeColor="White" />
            </asp:BoundField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Image ID="ImageLeaf" runat="server" ImageUrl="~/images/leaf.gif" />
                </ItemTemplate>
                <ItemStyle Width="60px" />
            </asp:TemplateField>
            <asp:HyperLinkField DataNavigateUrlFields="cid" 
                DataNavigateUrlFormatString="~/student/showcourse.aspx?cid={0}" DataTextField="ctitle" 
                HeaderText="未学学案" >
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
            </asp:HyperLinkField>
            <asp:BoundField DataField="Cdate" HeaderText="日期" >
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" Width="100px" />
            </asp:BoundField>
        </Columns>
       <PagerTemplate>
            <div  class="pagediv">
                第<asp:Label ID="lblPageIndex" runat="server" ForeColor="Black" 
                    Text="<%# ((GridView)Container.Parent.Parent).PageIndex + 1 %>"></asp:Label>
                页 共/<asp:Label ID="lblPageCount" runat="server" ForeColor="Black" 
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
        <RowStyle Height="32px" />
    </asp:GridView>
<br />
       <asp:GridView ID="GridViewdonekc" runat="server" AllowPaging="True" 
           AutoGenerateColumns="False" 
           EnableModelValidation="True" 
           OnPageIndexChanging="GridViewdonekc_PageIndexChanging" 
           onrowdatabound="GridViewdonekc_RowDataBound" SkinID="GridViewInfo" 
           Width="100%" PageSize="5" DataKeyNames="Cid">
           <Columns>
               <asp:BoundField DataField="Cid" Visible="false">
               <ItemStyle ForeColor="White" Width="30px" />
               </asp:BoundField>
                <asp:TemplateField>
                <ItemTemplate>
                    <asp:Image ID="ImageLeaf" runat="server" ImageUrl="~/images/fruit.gif" 
                        Height="16px" />
                </ItemTemplate>
                <ItemStyle Width="60px" />
            </asp:TemplateField>
               <asp:HyperLinkField DataNavigateUrlFields="Cid" 
                   DataNavigateUrlFormatString="~/student/showcourse.aspx?cid={0}" 
                   DataTextField="ctitle" HeaderText="已学学案">
               <HeaderStyle HorizontalAlign="Left" />
               <ItemStyle HorizontalAlign="Left" />
               </asp:HyperLinkField>
               <asp:TemplateField>
                <ItemTemplate>
                    <asp:Literal ID="Process" runat="server"></asp:Literal>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
               </asp:TemplateField>
            <asp:BoundField DataField="Cdate" HeaderText="日期" >
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" Width="100px" />
               </asp:BoundField>
           </Columns>
           <PagerTemplate>
               <div class="pagediv">
                   第<asp:Label ID="lblPageIndex" runat="server" ForeColor="Black" 
                       Text="<%# ((GridView)Container.Parent.Parent).PageIndex + 1 %>"></asp:Label>
                   页 共/<asp:Label ID="lblPageCount" runat="server" ForeColor="Black" 
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
           <RowStyle Height="32px" />
       </asp:GridView>
<br />
    <div  class="ccontent" >       
        <asp:DataList ID="DataListonline" runat="server" DataKeyField="Qid"
                    RepeatColumns="8" RepeatDirection="Horizontal" Caption="今天签到的同学" 
            CaptionAlign="Left" onitemdatabound="DataListonline_ItemDataBound" 
            Width="100%">
                    <ItemTemplate>
                        <div  class="onlinediv">
                        <div class="onlinebg">
                            <asp:HyperLink ID="HyperQname" runat="server" Font-Size="10pt" Font-Underline="False"
                                Height="20px" Text='<%# Eval("Sname") %>'   
                                ToolTip='<%# Eval("Qip") %>' Target="_blank" ></asp:HyperLink>
                        </div>
                            <asp:Image ID="Imageflag" runat="server" BorderStyle="None"  BorderWidth="0" />
                            <asp:Label ID="Labeltime" runat="server" Text='<%# Eval("Qdate") %>' Font-Names="Arial" Font-Size="8pt" Width="40px"></asp:Label></div>
                            <asp:Label ID="LabelSleader" runat="server" Text='<%# Eval("Sleader") %>' Visible="false" ></asp:Label>
                            <asp:Label ID="LabelSgroup" runat="server" Text='<%# Eval("Sgroup") %>' Visible="false" ></asp:Label>
                            <asp:Label ID="LabelQnum" runat="server" Text='<%# Eval("Qnum") %>' Visible="false" ></asp:Label>
                    </ItemTemplate>
                </asp:DataList>
                <br />
        </div>        
        <br />
</div>
<div class="right">
<div>    
    <asp:Image ID="Imageface" runat="server" style=" max-width:160px; max-height:160px;border-width:0px;"  />    
    <div id="DivRank" class="divrank" >
    <asp:Label ID="LabelRank" runat="server"></asp:Label>
    </div>
    </div> 
    <div class="divinfo">
    <div class="divinfo1">学号:</div>
    <div class="divinfo2"><asp:Label ID="snum" runat="server" ></asp:Label></div>
    </div>
    <div class="divinfo">
    <div class="divinfo1">班级:</div>
    <div class="divinfo2"><asp:Label ID="sclass" runat="server" ></asp:Label></div>
    </div>
    <div class="divinfo">
    <div class="divinfo1">姓名:</div>
    <div class="divinfo2"><asp:Label ID="sname" runat="server" ></asp:Label></div>
    </div>  
    <div class="divinfo">
    <div class="divinfo1">组长:</div>
    <div class="divinfo2"><asp:HyperLink ID="HLgroup" runat="server">[HLgroup]</asp:HyperLink></div>
    </div>
    <div class="divnull">
        最新作品评语：<br />
        <br />
        <div style="padding: 4px; width: 90%; border: 1px dashed #ABC1EB; margin:auto;">
        <asp:Label ID="LabelWself" runat="server" ></asp:Label><br />
        <asp:HyperLink ID="Hlwork" runat="server" ForeColor="#3399FF" Target="_blank" >查看</asp:HyperLink>
        </div>
        <br />
        <br />
                <asp:Button ID="BtnProfile" runat="server" OnClick="BtnProfile_Click"
                    Text="我的资料" Width="80px" CausesValidation="False" 
            CssClass="buttonimg" BorderStyle="None"/>
        <br />
        <br />
        <asp:Button ID="BtnExit" runat="server"  onclick="BtnExit_Click" 
            Width="80px" Enabled="False"  Text="" CssClass="buttonnone"  />
        <br />
        <asp:Label ID="LabelCids" runat="server" ForeColor="White" Visible="false"></asp:Label>
        <br />
        <script type="text/javascript">
            var i = 2;//设定退出按钮几秒钟后有效
            function setbar() {
                i--;
                var btnid = "<%= BtnExit.ClientID %>";
                if (document.getElementById(btnid).value != "") {
                    document.getElementById(btnid).value = "平台退出";
                }
                if (i <0) {
                    document.getElementById(btnid).disabled = false;
                    if (document.getElementById(btnid).value != "") {
                        document.getElementById(btnid).value = "平台退出";
                    }
                    return;
                }
                else {
                    document.getElementById(btnid).disabled = true;
                }
                
                setTimeout("setbar()", 1000);
            }
            setbar(); 
          </script>
        <br />
        <br />
    </div>   
    </div>
</div>
</asp:Content>


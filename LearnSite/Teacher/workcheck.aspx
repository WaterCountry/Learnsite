<%@ Page Language="C#"  StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="workcheck.aspx.cs" Inherits="Teacher_workcheck" %>
<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>作品展示</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="top: 0px; left: 0px; position: absolute; width: 100%;">  
    <div style="background-color: #9DA9B0; width: 100%;">         
            <asp:Image ID="Imagelogo" runat="server" ImageUrl="~/images/learnsite.gif"  ToolTip = "信息技术学习平台 LearnSite &#13;Powered By Asp.net2.0+Sql2005Express &#13;温州水乡设计编写" Height="24px" />
    </div> 
    <div style="width: 800px; height: auto; margin: auto">            
            <div  class="placehold">
<br />
      <div  class="divcenter">
          <asp:Label ID="Labelshow" runat="server" Font-Bold="True" Font-Names="Arial" 
              Font-Size="16px"></asp:Label>
          <asp:DropDownList ID="DDLclass" runat="server" Font-Size="9pt" 
                Width="50px" AutoPostBack="True" 
                onselectedindexchanged="DDLclass_SelectedIndexChanged" Font-Bold="True">
    </asp:DropDownList>
          <asp:Label ID="Labeltxt" runat="server" Font-Bold="True" Font-Names="Arial" 
              Font-Size="16px"></asp:Label>
          <br />
          <br />
          学案名称：<asp:Label ID="Labeltitle" 
              runat="server" Font-Bold="False" Font-Names="Arial"></asp:Label>
          <br />       
          <br />
          活动选择：<asp:DropDownList ID="DDLmid" runat="server" Font-Names="Arial" 
              Font-Size="9pt" AutoPostBack="True" 
              onselectedindexchanged="DDLmid_SelectedIndexChanged">
          </asp:DropDownList>
          <br />
          <asp:Label ID="Labelcolor" runat="server" BackColor="#CDE2FE" Width="12px" 
             Height="12px"></asp:Label>
&nbsp;作品标志&nbsp;
          <asp:Label ID="Labelscore" runat="server" BackColor="#FFCC99" Width="11px" 
             Height="11px"></asp:Label>
           评价等级&nbsp;
          作品总数：<asp:Label ID="Labelcounts" runat="server"></asp:Label>
          &nbsp;<asp:Button ID="BtnA" runat="server"  Text="一键评A"  SkinID="BtnSmall" 
              onclick="BtnA_Click" ToolTip="将本班该活动未评的作品，全部评为A"  />
          &nbsp;
    <asp:Button ID="BtnB" runat="server"  Text="一键评B"  SkinID="BtnSmall" 
              onclick="BtnB_Click" ToolTip="将本班该活动未评的作品，全部评为B"  />  
         &nbsp;
    <asp:Button ID="BtnCk" runat="server"  Text="一键已评"  SkinID="BtnSmall" 
              onclick="BtnCk_Click" ToolTip="不用给分的作品，一健全评为０"  />  
          &nbsp;<asp:HyperLink ID="HLautoplay" runat="server"  Target="_blank" 
              ToolTip="个人作品自动展播" >[HLautoplay]</asp:HyperLink>                        
          &nbsp;<asp:HyperLink ID="HLgroupplay" runat="server"  Target="_blank" 
              ToolTip="小组作品自动展播" >[HLgroupplay]</asp:HyperLink>                        
          <br />
          <br />
      <asp:Image ID="ImageType" runat="server" />
      &nbsp;<asp:Label ID="Labelmsg" runat="server"></asp:Label>
          &nbsp;<asp:ImageButton ID="ImgBtnFlasherror" runat="server" 
              ImageUrl="~/Images/flasherror.png" onclick="ImgBtnFlasherror_Click" 
              ToolTip="Office文档转换异常标志清除重新转换" />
          &nbsp;   
        <anthem:RadioButtonList ID="RBsort" runat="server" AutoPostBack="True" 
            Font-Size="9pt" onselectedindexchanged="RBsort_SelectedIndexChanged" 
            RepeatDirection="Horizontal" RepeatLayout="Flow">
            <Items>
            <asp:ListItem Value="0" Selected="True">时间排序</asp:ListItem>
            <asp:ListItem Value="1">学号排序</asp:ListItem>
            <asp:ListItem Value="2">IP 排序</asp:ListItem>
                <asp:ListItem Value="3">小组排序</asp:ListItem>
                <asp:ListItem Value="4">投票排序</asp:ListItem>
            </Items>
        </anthem:RadioButtonList>

          <br />
      </div>               
                <br />  
                 <div  class="divcenter">
                 <center>
                <anthem:DataList ID="DataListworks" runat="server" RepeatDirection="Horizontal" 
                    RepeatColumns="8" DataKeyField="Wid" CellPadding="2" 
        onitemdatabound="DataListworks_ItemDataBound" 
        onitemcommand="DataListworks_ItemCommand" CellSpacing="2">
                    <ItemTemplate>
                        <div  class="divscore" >
                            <div>
                            <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("Sname") %>' 
                                 ToolTip='<%# HttpUtility.HtmlDecode(  Eval("Wself").ToString()) %>' Target="_blank" CssClass="workname"></asp:HyperLink>
                            <anthem:CheckBox ID="CB" runat="server" Checked='<%# Eval("Wcheck") %>' 
                                EnableTheming="True" ToolTip="评价状态：取消则评分为0并可重新提交，选中则初始评分为0并不可重新提交" 
                                    oncheckedchanged="CB_CheckedChanged"  AutoPostBack="True" BorderStyle="None" />
                            </div>
                            <div>
                            <asp:Label ID="Wv" runat="server" Text='<%# Eval("Wvote") %>' ToolTip="票数" ></asp:Label>&nbsp;
                            <asp:Label ID="Wf" runat="server" Text='<%# Eval("Wfscore") %>' ToolTip="互评" ></asp:Label>&nbsp;
                            <asp:Label ID="Wl" runat="server" Text='<%# Eval("Wlscore") %>' ToolTip="组评" ForeColor="#0066FF"></asp:Label>
                            <asp:HyperLink ID="Hlflash" runat="server" Height="12px" Target="_blank" ImageUrl="~/Images/flashview.png" ToolTip="Flash格式预览" Visible="False"></asp:HyperLink>
                            </div>
                            <div >
                                <asp:LinkButton ID="LG" runat="server"
                                CommandArgument="Wid" CommandName="G" ToolTip="推荐到作品收藏" CssClass="wscored" >G</asp:LinkButton>
                              &nbsp;<asp:LinkButton ID="LA" runat="server" 
                                CommandArgument="Wid" CommandName="A" ToolTip="优秀"  CssClass="wscored" >A</asp:LinkButton>
                                 &nbsp;<asp:LinkButton ID="LB" runat="server" 
                                CommandArgument="Wid" CommandName="B" ToolTip="良好"  CssClass="wscored" >B</asp:LinkButton>
                                </div>
                            <div>
                            <asp:LinkButton ID="LC" runat="server" 
                                CommandArgument="Wid" CommandName="C" ToolTip="一般"  CssClass="wscored" >C</asp:LinkButton>                            
                            &nbsp;<asp:LinkButton ID="LD" runat="server" 
                             CommandArgument="Wid" CommandName="D" ToolTip="落后"  CssClass="wscored" >D</asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="LE" runat="server" 
                             CommandArgument="Wid" CommandName="E" ToolTip="不及格"  CssClass="wscored" >E</asp:LinkButton>
                            </div>
                            <asp:Label ID="Labelscore" runat="server" Text='<%# Eval("Wscore") %>' Visible="False"></asp:Label>
                            <asp:Label ID="Labelurl" runat="server" Text='<%# Eval("Wurl") %>' Visible="False"></asp:Label>
                            <asp:Label ID="Labelwid" runat="server" Text='<%# Eval("Wid") %>' Visible="False"></asp:Label>
                            <asp:CheckBox ID="Checkwflash" runat="server" Checked='<%# Eval("Wflash") %>' Visible="False" />
                            <asp:CheckBox ID="Checkwerror" runat="server" Checked='<%# Eval("Werror") %>' Visible="False" />
                            <asp:Label ID="Labelwlemotion" runat="server" Text='<%# Eval("Wlemotion") %>' Visible="False"></asp:Label>
                            </div>
                    </ItemTemplate>
                </anthem:DataList>
    <br />
    <br />
                <anthem:DataList ID="DataListgroup" runat="server" RepeatDirection="Horizontal" 
                    RepeatColumns="6" DataKeyField="Gid" CellPadding="2" 
        CellSpacing="2" onitemcommand="DataListgroup_ItemCommand" 
        onitemdatabound="DataListgroup_ItemDataBound" Caption="小组作品">
                    <ItemTemplate>
                        <div  class="divgroupscore" >
                            <div>
                            <asp:HyperLink ID="HyperLinkg1" runat="server" Text='<%# Eval("Sgtitle") %>' 
                             ToolTip='<%# Eval("Gnote") %>' Target="_blank" CssClass="groupname"></asp:HyperLink>
                            </div>
                            <asp:Label ID="Wvg" runat="server" Text='<%# Eval("Gvote") %>' ToolTip="票数" ></asp:Label>                            
                            <anthem:CheckBox ID="CBg" runat="server" AutoPostBack="True" BorderStyle="None" 
                                Checked='<%# Eval("Gcheck") %>' EnableTheming="True" 
                                oncheckedchanged="CBg_CheckedChanged" 
                                ToolTip="评价状态：取消则评分为0并可重新提交，选中则初始评分为0并不可重新提交" />
                            <div >
                               <asp:LinkButton ID="L20" runat="server" 
                                CommandArgument="Gid" CommandName="20" ToolTip="20学分"  CssClass="wscored" >A+</asp:LinkButton> 
                              &nbsp;<asp:LinkButton ID="L19" runat="server"
                                CommandArgument="Gid" CommandName="19" ToolTip="19学分"  CssClass="wscored" >A </asp:LinkButton>
                                &nbsp;<asp:LinkButton ID="L18" runat="server"
                                CommandArgument="Gid" CommandName="18" ToolTip="18学分"  CssClass="wscored" >A-</asp:LinkButton>
                                </div>
                                <div>
                                <asp:LinkButton ID="L17" runat="server" 
                                CommandArgument="Gid" CommandName="17" ToolTip="17学分"  CssClass="wscored" >B+</asp:LinkButton>
                                &nbsp;<asp:LinkButton ID="L16" runat="server" 
                                CommandArgument="Gid" CommandName="16" ToolTip="16学分"  CssClass="wscored" >B </asp:LinkButton>
                                &nbsp;<asp:LinkButton ID="L15" runat="server"
                                CommandArgument="Gid" CommandName="15" ToolTip="15学分"  CssClass="wscored" >B-</asp:LinkButton>
                                </div>                               
                                <div>
                                <asp:LinkButton ID="L14" runat="server"
                                CommandArgument="Gid" CommandName="14" ToolTip="14学分"  CssClass="wscored" >C+</asp:LinkButton>
                                &nbsp;<asp:LinkButton ID="L13" runat="server"
                                CommandArgument="Gid" CommandName="13" ToolTip="13学分"  CssClass="wscored" >C </asp:LinkButton>
                                &nbsp;<asp:LinkButton ID="L12" runat="server" 
                                CommandArgument="Gid" CommandName="12" ToolTip="12学分"  CssClass="wscored" >C-</asp:LinkButton>
                                </div>
                                <br />
                                <asp:Label ID="Labelgscore" runat="server" Text='<%# Eval("Gscore") %>' Visible="False"></asp:Label>                            
                            <asp:Label ID="Labelgurl" runat="server" Text='<%# Eval("Gurl") %>' Visible="False"></asp:Label>
                            <asp:Label ID="Labelgid" runat="server" Text='<%# Eval("Gid") %>' Visible="False"></asp:Label>
                            </div>
                    </ItemTemplate>
                </anthem:DataList>
                </center>
                </div>
    <asp:ImageButton ID="Btnreflash" runat="server"  
        ImageUrl="~/Images/refresh.gif"   onclick="Btnreflash_Click" />
    <br />
    <br />
    <asp:Button ID="Btnreturn" runat="server"  Text="关闭"  SkinID="BtnSmall"  />
    <br />
    <script type ="text/javascript" >
        function myrefresh() {
            document.getElementById("<%= Btnreflash.ClientID %>").click();
        }
        setTimeout("myrefresh()", 30000); //指定30秒刷新一次作品         
        </script>
    <br />
     <div  class="divcenter">
     <center>
                <asp:DataList ID="DataListNoworks" runat="server" RepeatDirection="Horizontal" 
                    RepeatColumns="8"  CellPadding="1" Caption="未提交作品学生列表">
                    <ItemTemplate>
                        <div  >
                            <asp:Label ID="Label1" runat="server" Height="18px" Text='<%# Eval("Sname") %>' 
                               ToolTip='<%# Eval("Sscore") %>'  Width="80px"></asp:Label>
                            <br />
                            </div>
                    </ItemTemplate>
                </asp:DataList>
                </center>
                </div>
    <br />
</div>
    </div>
    </div>
    
    </form>
</body>
</html>
